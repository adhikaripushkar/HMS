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
    public class StockItemPurchaseController : Controller
    {

        //
        // GET: /StockItemPurchase/
        StockItemPurchaseProvider pro = new StockItemPurchaseProvider();
        public ActionResult Index()
        {
            ViewBag.rowid = Session["rowid"];
            Session["rowid"] = null;
            StockItemPurchaseModel model = new StockItemPurchaseModel();
            model.ItemPurchaseList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockItemPurchase/Details/5

        public ActionResult Details(int id, int rowid)
        {
            Session["rowid"] = rowid;

            StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
            model.ItemPurchaseDetailList = pro.GetPurchaseDetail(id);
            return View(model.ItemPurchaseDetailList);
        }

        //
        // GET: /StockItemPurchase/Create

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();
            StockItemPurchaseModel model = new StockItemPurchaseModel();
            try
            {
                var obj = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == ent.StockItemPurchases.Max(y => y.ItemPurchaseId)).SingleOrDefault();
                string[] nums = obj.StockEntryNo.Split('E');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.StockEntryNo = "SE" + rcnum.RecNumGen();
            }
            catch
            {
                model.StockEntryNo = "SE001";
            }
            //TempData["id"] = ent.SetupStockCategory.Where(x => x.Status == true).FirstOrDefault().StockCategoryID;
            return View(model);
        }

        //
        // POST: /StockItemPurchase/Create

        [HttpPost]
        public ActionResult Create(StockItemPurchaseModel model)
        {
            int flag = 0;

            if (model.StockItemEntryList == null || model.StockItemEntryList.Count == 0)
            {
                TempData["message"] = "Add Purchase Items!";
                return View(model);
            }
            foreach (var item in model.StockItemEntryList)
            {

                if (item.Quantity > item.QuotQty || item.Rate > item.QuotRate)
                {
                    flag = 1;

                    Response.Redirect("SearchOrder?ipvalue=_" + model.ItemOrderId);
                }
            }
            if (ModelState.IsValid && flag == 0)
            {
                try
                {
                    // TODO: Add insert logic here
                    pro.Insert(model);
                    TempData["message"] = "Successfully Saved!";
                    //return RedirectToAction("Index");
                    //return RedirectToAction("createNewJV", "JVMaster");
                    return View("ShowPurchaseJVDetails", model);
                }
                catch (Exception e)
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /StockItemPurchase/Edit/5

        public ActionResult Edit(int id, int rowid)
        {
            Session["rowid"] = rowid;
            EHMSEntities ent = new EHMSEntities();
            StockItemPurchaseModel model = new StockItemPurchaseModel();

            var obj = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == id).SingleOrDefault();

            AutoMapper.Mapper.Map(obj, model);
            var PurchaseDetailList = ent.StockItemPurchaseDetails.Where(x => x.ItemPurchaseId == id).ToList();
            foreach (var item in PurchaseDetailList)
            {
                StockItemEntry itementry = new StockItemEntry();
                itementry.StockCategoryId = item.SetupStockItemEntry.SetupStockCategory.StockCategoryID;
                itementry.StockSubCategoryId = item.SetupStockItemEntry.SetupStockSubCategory.StockSubCategoryID;
                itementry.Quantity = item.Quantity;
                itementry.Rate = item.Rate;
                itementry.StockItemEntryId = item.StockItemEntryId;
                var orderobj = (from sip in ent.StockItemPurchases
                                join spo in ent.StockPurchaseOrders
                                on sip.ItemOrderId equals spo.PurchaseOrderNo
                                join spod in ent.StockPurchaseOrderDetails
                                on spo.PurchaseOrderId equals spod.PurchaseOrderId
                                where sip.ItemPurchaseId == id && spod.ItemId == item.StockItemEntryId
                                select spod).SingleOrDefault();
                itementry.QuotQty = orderobj.Quantity;
                itementry.QuotRate = orderobj.QuotationPrice;
                model.StockItemEntryList.Add(itementry);
            }
            return View(model);
        }

        //
        // POST: /StockItemPurchase/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StockItemPurchaseModel model)
        {

            if (model.StockItemEntryList == null || model.StockItemEntryList.Count == 0)
            {
                TempData["message"] = "Add Purchase Items!";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    pro.Udate(id, model);
                    TempData["message"] = "Successfully Updated!";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /StockItemPurchase/Delete/5

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == id).SingleOrDefault();
            obj.Status = false;
            foreach (var item in ent.StockItemPurchaseDetails.Where(x => x.ItemPurchaseId == id).ToList())
            {
                ent.StockItemPurchaseDetails.Remove(item);
                var objItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault();
                objItem.Quantity = objItem.Quantity - item.Quantity;
                ent.Entry(objItem).State = System.Data.EntityState.Modified;
            }

            ent.SaveChanges();
            TempData["message"] = "Successfully Deleted!";
            return RedirectToAction("Index");
        }

        //
        // POST: /StockItemPurchase/Delete/5


        public ActionResult DeleteDetail(int id)
        {
            EHMSEntities ent = new EHMSEntities();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult StockItemPurchaseDetail()
        {
            StockItemEntryModel model = new StockItemEntryModel();

            return PartialView("StockItemPurchaseDetail");
        }
        [HttpPost]
        public ActionResult GetSum(StockItemPurchaseModel model)
        {

            decimal amount = 0;
            try
            {
                foreach (var item in model.StockItemEntryList)
                {
                    amount = amount + item.Quantity * item.Rate;
                }

                return Json(amount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TrackId(string id)
        {
            Session["rowid"] = id;
            return null;

        }

        public ActionResult AutocompletePurchaseOrder(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> order = new List<string>();
            var list = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderNo.Contains(s) && x.Status == true).ToList();
            foreach (var item in list)
            {
                order.Add(item.PurchaseOrderNo);
            }
            return Json(order, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchOrder(int id)
        {

            EHMSEntities ent = new EHMSEntities();
            StockItemPurchaseModel model = new StockItemPurchaseModel();
            decimal v = 0;
            var list = (from po in ent.StockPurchaseOrders
                        join pd in ent.StockPurchaseOrderDetails
                            on po.PurchaseOrderId equals pd.PurchaseOrderId
                        where po.PurchaseOrderId == id && pd.Status == true
                        select new { po, pd }).ToList();
            foreach (var item in list)
            {
                StockItemEntry itementry = new StockItemEntry();
                itementry.StockCategoryId = item.pd.SetupStockItemEntry.SetupStockCategory.StockCategoryID;
                itementry.StockSubCategoryId = item.pd.SetupStockItemEntry.SetupStockSubCategory.StockSubCategoryID;
                itementry.QuotQty = Convert.ToDecimal(item.pd.Quantity.ToString().Substring(0, item.pd.Quantity.ToString().IndexOf(".")));


                try
                {
                    v = (from sip in ent.StockItemPurchases
                         join sipd in ent.StockItemPurchaseDetails
                             on sip.ItemPurchaseId equals sipd.ItemPurchaseId
                         where sip.ItemOrderId == item.po.PurchaseOrderNo
                         where sipd.StockItemEntryId == item.pd.ItemId
                         select sipd
                                                             ).Sum(x => x.Quantity);
                }
                catch
                {
                    v = 0;
                }
                itementry.QuotQty = itementry.QuotQty - v;
                itementry.QuotRate = item.pd.QuotationPrice;
                itementry.StockItemEntryId = item.pd.ItemId;

                model.StockItemEntryList.Add(itementry);
            }
            try
            {
                model.StockSupplierId = list[0].pd.SupplierId;
                model.ItemOrderId = list[0].po.PurchaseOrderNo;
            }
            catch
            {
                TempData["message"] = "Items not available!!";
                return RedirectToAction("Index", "StockPurchaseOrder");
            }
            try
            {
                string num = ent.StockItemPurchases.Max(x => x.StockEntryNo);
                string[] nums = num.Split('E');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.StockEntryNo = "SE" + rcnum.RecNumGen();
            }
            catch
            {
                model.StockEntryNo = "SE001";
            }
            return View("Create", model);
        }


        public ActionResult Report(int id)
        {
            StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
            EHMSEntities ent = new EHMSEntities();
            var objpurchase = (from pm in ent.StockItemPurchases
                               join po in ent.StockPurchaseOrders on
                                   pm.ItemOrderId equals po.PurchaseOrderNo
                               where pm.ItemPurchaseId == id
                               select new { pm, po }).SingleOrDefault();
            ViewBag.purodno = objpurchase.po.PurchaseOrderNo;
            ViewBag.billno = objpurchase.pm.ItemBillNo;
            ViewBag.seno = objpurchase.pm.StockEntryNo;
            ViewBag.sup = objpurchase.pm.StockSupplierId;
            ViewBag.poid = objpurchase.po.PurchaseOrderId;
            model.ItemPurchaseDetailList = new List<StockItemPurchaseDetailModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemPurchaseDetail>, IEnumerable<StockItemPurchaseDetailModel>>(ent.StockItemPurchaseDetails).Where(x => x.ItemPurchaseId == id));

            return View(model);
        }

        public ActionResult SupplierWiseReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SupplierWiseReport(string fromdate, string todate, string seno, int? supplier)
        {
            EHMSEntities ent = new EHMSEntities();
            List<StockItemPurchaseDetailModel> modelllist = new List<StockItemPurchaseDetailModel>();

            if (seno == "" && supplier == null && (fromdate == "" || todate == ""))
            {

                var list = (from ip in ent.StockItemPurchases
                            join ipd in ent.StockItemPurchaseDetails
                                on ip.ItemPurchaseId equals ipd.ItemPurchaseId
                            where ip.Status == true
                            select new { ip, ipd }).ToList();



                foreach (var item in list)
                {
                    StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
                    model.StockItemEntryId = item.ipd.StockItemEntryId;
                    model.Quantity = item.ipd.Quantity;
                    model.Rate = item.ipd.Rate;
                    model.TotalAmount = item.ipd.Quantity * item.ipd.Rate;
                    model.SupplierId = item.ipd.SupplierId;
                    model.SeNo = item.ip.StockEntryNo;
                    model.PurchaseDate = item.ip.CreatedDate.ToShortDateString();
                    modelllist.Add(model);
                }
            }

            else if (fromdate != "" && todate != "")
            {
                DateTime fd = Convert.ToDateTime(fromdate).AddDays(-1);
                DateTime td = Convert.ToDateTime(todate).AddDays(1);
                var list = (from ip in ent.StockItemPurchases
                            join ipd in ent.StockItemPurchaseDetails
                                on ip.ItemPurchaseId equals ipd.ItemPurchaseId
                            where ip.Status == true
                            where ip.CreatedDate >= fd && ip.CreatedDate <= td
                            select new { ip, ipd }).ToList();



                foreach (var item in list)
                {
                    StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
                    model.StockItemEntryId = item.ipd.StockItemEntryId;
                    model.Quantity = item.ipd.Quantity;
                    model.Rate = item.ipd.Rate;
                    model.TotalAmount = item.ipd.Quantity * item.ipd.Rate;
                    model.SupplierId = item.ipd.SupplierId;
                    model.SeNo = item.ip.StockEntryNo;
                    model.PurchaseDate = item.ip.CreatedDate.ToShortDateString();
                    modelllist.Add(model);
                }
            }

            else if (seno != "")
            {
                var list = (from ip in ent.StockItemPurchases
                            join ipd in ent.StockItemPurchaseDetails
                                on ip.ItemPurchaseId equals ipd.ItemPurchaseId
                            where ip.Status == true
                            where ip.StockEntryNo == seno

                            select new { ip, ipd }).ToList();



                foreach (var item in list)
                {
                    StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
                    model.StockItemEntryId = item.ipd.StockItemEntryId;
                    model.Quantity = item.ipd.Quantity;
                    model.Rate = item.ipd.Rate;
                    model.TotalAmount = item.ipd.Quantity * item.ipd.Rate;
                    model.SupplierId = item.ipd.SupplierId;
                    model.SeNo = item.ip.StockEntryNo;
                    model.PurchaseDate = item.ip.CreatedDate.ToShortDateString();
                    modelllist.Add(model);
                }
            }

            else if (supplier != null)
            {
                var list = (from ip in ent.StockItemPurchases
                            join ipd in ent.StockItemPurchaseDetails
                                on ip.ItemPurchaseId equals ipd.ItemPurchaseId
                            where ip.Status == true
                            where ip.StockSupplierId == supplier

                            select new { ip, ipd }).ToList();



                foreach (var item in list)
                {
                    StockItemPurchaseDetailModel model = new StockItemPurchaseDetailModel();
                    model.StockItemEntryId = item.ipd.StockItemEntryId;
                    model.Quantity = item.ipd.Quantity;
                    model.Rate = item.ipd.Rate;
                    model.TotalAmount = item.ipd.Quantity * item.ipd.Rate;
                    model.SupplierId = item.ipd.SupplierId;
                    model.SeNo = item.ip.StockEntryNo;
                    model.PurchaseDate = item.ip.CreatedDate.ToShortDateString();
                    modelllist.Add(model);
                }
            }

            return View(modelllist);
        }


        public ActionResult AutocompleteSeNo(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> senos = new List<string>();
            var list = ent.StockItemPurchases.Where(x => x.StockEntryNo.Contains(s) && x.Status == true).ToList();
            foreach (var item in list)
            {
                senos.Add(item.StockEntryNo);
            }
            return Json(senos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowPurchaseJVDetails(StockItemPurchaseModel model)
        {
            return View(model);
        }

        public ActionResult showDD()
        {
            StockItemPurchaseModel model = new StockItemPurchaseModel();
            return View(model);
        }


        public ActionResult PurchaseVoucher(int id)
        {
            StockItemPurchaseModel model = new StockItemPurchaseModel();
            model = pro.GetPurchaseVoucherDetails(id);
            return View(model);

        }

    }
}
