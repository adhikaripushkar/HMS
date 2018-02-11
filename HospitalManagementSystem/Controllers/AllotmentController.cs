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
    public class AllotmentController : Controller
    {
        //
        // GET: /Allotment/
        DemandMasterProvider pro = new DemandMasterProvider();
        AllotmentProvider proA = new AllotmentProvider();
        public ActionResult Index(int? id)
        {
            EHMSEntities ent = new EHMSEntities();
            DemandMasterModel model = new DemandMasterModel();
            model.ItemDemandNo = ent.ItemDemandMasters.Where(x => x.ItemDemandID == id).SingleOrDefault().ItemDemandNo;
            model.DemandMasterList = new List<DemandMasterModel>();
            model.DemandDetailList = new List<DemandDetailModel>(AutoMapper.Mapper.Map<IEnumerable<ItemDemandDetail>, IEnumerable<DemandDetailModel>>(ent.ItemDemandDetails.Where(x => x.ItemDemandID == id && x.DispatchStatus == false)));
            if (model.DemandDetailList.Count == 0)
            {
                TempData["message"] = "Items not available!!";
                return RedirectToAction("Index", "Demand");
            }
            try
            {
                var obj = ent.ItemAllotmentMasters.Where(x => x.ItemAllotmentMasterID == ent.ItemAllotmentMasters.Max(y => y.ItemAllotmentMasterID)).SingleOrDefault();
                string[] nums = obj.ItemAllotmentNo.Split('N');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.ItemAllotmentNo = "AN" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.ItemAllotmentNo = "AN001";
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DemandMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            string s = "";
            //ViewBag.datef = datefrom;
            //ViewBag.datet = dateto;
            //ViewBag.dmdno = demandno;

            //if (saveDetails != null)
            //{
            //    if (demandno == "")
            //    {
            //        model.DemandMasterList = pro.GetCustomDemandList(model.DepartmentID, datefrom, dateto);
            //    }
            foreach (var item in model.DemandDetailList)
            {
                if (item.CancellationStatus == false && item.isSelected == false && item.QuantityIssued == 0)
                {

                    TempData["message"] = "Enter quantity to be dispatched!";

                    return View(model);
                }
            }

            s = proA.Insert(model);

            if (s != "")
            {
                TempData["message"] = s;
                foreach (var item in model.DemandDetailList)
                {
                    if (ent.ItemDemandDetails.Any(x => x.ItemID == item.ItemID && x.ItemDemandID == item.ItemDemandID && x.DispatchStatus == true))
                    {
                        model.DemandDetailList = model.DemandDetailList.Where(x => x.ItemID != item.ItemID && x.ItemDemandID != item.ItemDemandID).ToList();
                    }
                }
                return View(model);
            }
            if (model.isSelected == true)
            {
                StockPurchaseOrderModel purchasemodel = new StockPurchaseOrderModel();
                purchasemodel.StockPurchaseItemEntryList = new List<StockPurchaseItemEntry>();
                foreach (var item in model.DemandDetailList)
                {
                    if (item.isSelected == true)
                    {
                        StockPurchaseItemEntry itementry = new StockPurchaseItemEntry();
                        var obj = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault();
                        itementry.StockCategoryId = obj.StockCategoryId;
                        itementry.StockSubCategoryId = obj.StockSubCategoryId;
                        itementry.StockItemEntryId = obj.StockItemEntryId;
                        purchasemodel.StockPurchaseItemEntryList.Add(itementry);
                    }
                }
                Session["purchaseitementrylist"] = purchasemodel.StockPurchaseItemEntryList;
                return RedirectToAction("CreateView", "StockPurchaseOrder");
            }
            //}

            //model.DemandDetailList = new List<DemandDetailModel>();
            //if (demandno != "")
            //{

            //    var objList = (from dd in ent.ItemDemandDetail
            //                   join dm in ent.ItemDemandMaster
            //                       on dd.ItemDemandID equals dm.ItemDemandID
            //                   where dm.ItemDemandNo == demandno && dd.DispatchStatus == false
            //                   select dd).ToList();
            //    try
            //    {
            //        int i = objList[0].ItemDemandID;
            //        model.ItemDemandNo = ent.ItemDemandMaster.Where(x => x.ItemDemandID == i).SingleOrDefault().ItemDemandNo;
            //        AutoMapper.Mapper.Map(objList, model.DemandDetailList);
            //    }
            //    catch { }
            //        try
            //    {
            //        string num = ent.ItemAllotmentMaster.Max(x => x.ItemAllotmentNo);
            //        string[] nums = num.Split('N');
            //        RecordNumGen rcnum = new RecordNumGen(nums[1]);

            //        model.ItemAllotmentNo = "AN" + rcnum.RecNumGen();
            //    }
            //    catch (Exception e)
            //    {
            //        model.ItemAllotmentNo = "AN001";
            //    }

            //}
            //else
            //{
            //    model.DemandMasterList = pro.GetCustomDemandList(model.DepartmentID, datefrom, dateto);
            //    if (DemandId != "")
            //    {

            //        model.DemandDetailList = pro.GetCDemandDetails(Convert.ToInt32(DemandId));
            //        try
            //        {
            //            int i = model.DemandDetailList[0].ItemDemandID;
            //            model.ItemDemandNo = ent.ItemDemandMaster.Where(x => x.ItemDemandID == i).SingleOrDefault().ItemDemandNo;
            //        }
            //        catch { }
            //            try
            //        {
            //            string num = ent.ItemAllotmentMaster.Max(x => x.ItemAllotmentNo);
            //            string[] nums = num.Split('N');
            //            RecordNumGen rcnum = new RecordNumGen(nums[1]);

            //            model.ItemAllotmentNo = "AN" + rcnum.RecNumGen();
            //        }
            //        catch (Exception e)
            //        {
            //            model.ItemAllotmentNo = "AN001";
            //        }
            //    }

            //}





            return RedirectToAction("AllotmentMaster");
        }
        public ActionResult DemandDetails(int id)
        {
            DemandMasterModel model = new DemandMasterModel();
            model.DemandDetailList = pro.GetDemandDetails(id);

            return View("Index", model);
        }
        //
        // GET: /Allotment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Allotment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Allotment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Allotment/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Allotment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Allotment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Allotment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AllotmentMaster()
        {

            AllotmentMasterModel model = new AllotmentMasterModel();
            model.AllotmentMasterList = pro.GetAllotments();

            return View(model);
        }

        public ActionResult AllotmentDetails(int id)
        {
            AllotementDetailModel model = new AllotementDetailModel();
            model.AllotmentDetailList = pro.GetAllotmentsDetails(id);
            return View(model);
        }

        public ActionResult AllotmentReport(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            AllotementDetailModel model = new AllotementDetailModel();
            model.AllotmentDetailList = pro.GetAllotmentsDetails(id);
            model.Remarks = ent.ItemAllotmentMasters.Where(x => x.ItemAllotmentMasterID == id).SingleOrDefault().ItemAllotmentNo;
            foreach (var item in model.AllotmentDetailList)
            {
                try
                {
                    item.QuantityRemained = (from am in ent.ItemAllotmentMasters
                                             join dd in ent.ItemDemandDetails
                                                 on am.ItemDemandID equals dd.ItemDemandID
                                             where am.ItemAllotmentMasterID == id
                                             where dd.ItemID == item.ItemID
                                             select dd).SingleOrDefault().QuantityDemand;
                }
                catch { item.QuantityRemained = 0; }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AllotmentDetails(AllotementDetailModel model)
        {
            string s = "";
            s = proA.Update(model.AllotmentDetailList);
            if (s != "")
            {
                TempData["message"] = s;
                return RedirectToAction("AllotmentDetails");
            }
            return RedirectToAction("AllotmentMaster");
        }


        public ActionResult AutocompleteDemandNo(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> demand = new List<string>();
            var list = ent.ItemDemandMasters.Where(x => x.ItemDemandNo.Contains(s) && x.Status == true).ToList();
            foreach (var item in list)
            {
                demand.Add(item.ItemDemandNo);
            }
            return Json(demand, JsonRequestBehavior.AllowGet);
        }
    }
}
