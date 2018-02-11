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
    public class DemandController : Controller
    {
        //
        // GET: /Demand/
        DemandMasterProvider pro = new DemandMasterProvider();
        public ActionResult Index()
        {
            DemandMasterModel model = new DemandMasterModel();
            model.DemandMasterList = pro.GetList();
            return View(model);
        }

        //
        // GET: /Demand/Details/5

        public ActionResult Details(int id)
        {
            DemandDetailModel model = new DemandDetailModel();
            model.DemandDetailList = pro.GetDemandDetails(id);
            return View(model);
        }

        //
        // GET: /Demand/Create

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();
            DemandMasterModel model = new DemandMasterModel();
            try
            {
                var obj = ent.ItemDemandMasters.Where(x => x.ItemDemandID == ent.ItemDemandMasters.Max(y => y.ItemDemandID)).SingleOrDefault();
                string[] nums = obj.ItemDemandNo.Split('N');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.ItemDemandNo = "DN" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.ItemDemandNo = "DN001";
            }
            model.DemandDate = DateTime.Today;
            return View(model);
        }

        //
        // POST: /Demand/Create

        [HttpPost]
        public ActionResult Create(DemandMasterModel model)
        {
            if (model.StockItemEntryList == null || model.StockItemEntryList.Count == 0)
            {
                TempData["message"] = "Add Items!";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    pro.Insert(model);
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
        // GET: /Demand/Edit/5

        public ActionResult Edit(int id)
        {
            DemandMasterModel model = new DemandMasterModel();
            model = pro.GetDemandForEdit(id);
            return View(model);
        }

        //
        // POST: /Demand/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, DemandMasterModel model)
        {
            if (model.StockItemEntryList == null || model.StockItemEntryList.Count == 0)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    pro.Update(model);
                    return RedirectToAction("Index");
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

        //
        // GET: /Demand/Delete/5

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Demand/Delete/5

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

        [HttpPost]
        public ActionResult DemandItemList()
        {
            DemandMasterModel model = new DemandMasterModel();

            return PartialView("DemandItemList");
        }

        public ActionResult DemandReport(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            DemandDetailModel model = new DemandDetailModel();
            model.DemandDetailList = ent.Database.SqlQuery<DemandDetailModel>("SPDemandMaster 2," + id).ToList();
            var obj = ent.ItemDemandMasters.Where(x => x.ItemDemandID == id).SingleOrDefault();
            model.Remarks = obj.ItemDemandNo;
            model.ItemID = obj.DepartmentID;
            return View(model);
        }



    }
}
