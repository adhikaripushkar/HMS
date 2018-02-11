using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class StockPurchaseOrderController : Controller
    {
        //
        // GET: /StockPurchaseOrder/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            ViewBag.rowid = Session["rowid"];
            Session["rowid"] = null;
            StockPurchaseOrderModel model = new StockPurchaseOrderModel();
            StockPurchaseOrderProvider pro = new StockPurchaseOrderProvider();
            model.StockPurchaseOrderModelList = pro.GetList();
            return View(model);
        }
        public ActionResult Create(int? id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockPurchaseOrderModel model = new StockPurchaseOrderModel();
            try
            {
                var obj = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == ent.StockPurchaseOrders.Max(y => y.PurchaseOrderId)).SingleOrDefault();
                string[] nums = obj.PurchaseOrderNo.Split('O');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.PurchaseOrderNo = "PO" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.PurchaseOrderNo = "PO001";
            }
            model.DemandId = (int)id;
            var demandlist = ent.ItemDemandDetails.Where(x => x.ItemDemandID == id).ToList();
            foreach (var item in demandlist)
            {
                StockPurchaseItemEntry itementry = new StockPurchaseItemEntry();
                try
                {
                    itementry.StockCategoryId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault().StockCategoryId;
                    itementry.StockSubCategoryId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault().StockSubCategoryId;
                    itementry.StockItemEntryId = item.ItemID;
                    itementry.Quantity = item.QuantityDemand;
                }
                catch { }
                model.StockPurchaseItemEntryList.Add(itementry);
            }
            return View(model);
        }

        public ActionResult CreateView()
        {
            EHMSEntities ent = new EHMSEntities();
            StockPurchaseOrderModel model = new StockPurchaseOrderModel();
            if (ent.StockPurchaseOrders.Any())
            {
                model.PurchaseOrderNo = "PO" + ent.StockPurchaseOrders.Max(x => x.PurchaseOrderId + 1);
            }
            else
            {
                model.PurchaseOrderNo = "PO1";
            }
            model.StockPurchaseItemEntryList = Session["purchaseitementrylist"] as List<StockPurchaseItemEntry>;
            Session["purchaseitementrylist"] = null;
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(StockPurchaseOrderModel model)
        {
            StockPurchaseOrderProvider pro = new StockPurchaseOrderProvider();

            if (model.StockPurchaseItemEntryList == null || model.StockPurchaseItemEntryList.Count == 0)
            {
                TempData["message"] = "Add Purchase Items!";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    int i = pro.Insert(model);
                    TempData["message"] = "Successfully Saved!";

                    model.PurchaseOrderId = i;
                    return RedirectToAction("OrderReport", model);
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult StockItemPurchaseOrderDetail()
        {
            StockPurchaseItemEntry model = new StockPurchaseItemEntry();

            return PartialView("StockIPurchaseOrderDetail");
        }
        public ActionResult Edit(int id)
        {
            //Session["rowid"] = rowid;
            EHMSEntities ent = new EHMSEntities();
            StockPurchaseOrderModel model = new StockPurchaseOrderModel();

            var obj = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == id).SingleOrDefault();

            AutoMapper.Mapper.Map(obj, model);
            var PurchaseDetailList = ent.StockPurchaseOrderDetails.Where(x => x.PurchaseOrderId == id).ToList();
            foreach (var item in PurchaseDetailList)
            {
                StockPurchaseItemEntry itementry = new StockPurchaseItemEntry();
                itementry.StockCategoryId = item.SetupStockItemEntry.SetupStockCategory.StockCategoryID;
                itementry.StockSubCategoryId = item.SetupStockItemEntry.SetupStockSubCategory.StockSubCategoryID;
                itementry.StockItemEntryId = item.ItemId;
                itementry.Quantity = item.Quantity;
                itementry.QuatationPrice = item.QuotationPrice;
                model.StockPurchaseItemEntryList.Add(itementry);
            }

            return View(model);
        }

        //
        // POST: /StockItemPurchase/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StockPurchaseOrderModel model)
        {
            StockPurchaseOrderProvider pro = new StockPurchaseOrderProvider();

            //if (model.StockPurchaseItemEntryList == null || model.StockPurchaseItemEntryList.Count == 0)
            //{
            //    TempData["message"] = "Add Purchase Items!";
            //    return View(model);
            //}
            if (ModelState.IsValid)
            {

                // TODO: Add update logic here
                pro.Udate(id, model);
                TempData["message"] = "Successfully Updated!";
                return RedirectToAction("Index");



            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == id).SingleOrDefault();
            obj.Status = false;
            foreach (var item in ent.StockPurchaseOrderDetails.Where(x => x.PurchaseOrderId == id).ToList())
            {
                ent.StockPurchaseOrderDetails.Remove(item);

            }

            ent.SaveChanges();
            TempData["message"] = "Successfully Deleted!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {

            StockPurchaseOrderProvider pro = new StockPurchaseOrderProvider();
            StockPurchaseOrderDetailsModel model = new StockPurchaseOrderDetailsModel();
            model.StockPurchaseOrderDetailsModelList = pro.GetPurchaseDetail(id);
            return View(model.StockPurchaseOrderDetailsModelList);
        }

        public ActionResult OrderReport(StockPurchaseOrderModel model)
        {
            StockPurchaseOrderProvider pro = new StockPurchaseOrderProvider();
            model.StockPurchaseOrderDetailsModel = new StockPurchaseOrderDetailsModel();
            model.StockPurchaseOrderDetailsModel.StockPurchaseOrderDetailsModelList = new List<StockPurchaseOrderDetailsModel>();
            model.StockPurchaseOrderDetailsModel.StockPurchaseOrderDetailsModelList = pro.GetPurchaseDetail(model.PurchaseOrderId);

            return View(model);
        }

        public ActionResult Print(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockPurchaseOrderModel model = new StockPurchaseOrderModel();
            var obj = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == id).SingleOrDefault();
            AutoMapper.Mapper.Map(obj, model);
            return RedirectToAction("OrderReport", model);
        }



    }
}
