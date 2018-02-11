using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class MedicalTestController : Controller
    {
        //
        // GET: /MedicalText/
        DemandPatientAssignmentProvider pro = new DemandPatientAssignmentProvider();
        public ActionResult Index()
        {
            EHMSEntities ent = new EHMSEntities();
            DemandMasterModel model = new DemandMasterModel();
            model.DemandMasterList = pro.GetList();
            foreach (var item in model.DemandMasterList)
            {
                var c = ent.DemandPatientAssignments.Where(x => x.DemandId == item.ItemDemandID && x.Status == true).Count();
                if (c == 0) item.Orderer = "Assign Patients";
                else item.Orderer = c + " Patient(s) Assigned";
            }
            return View(model);
        }

        //
        // GET: /MedicalText/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MedicalText/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MedicalText/Create

        [HttpPost]
        public ActionResult Create(DemandMasterModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
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
        // GET: /MedicalText/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MedicalText/Edit/5

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
        // GET: /MedicalText/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MedicalText/Delete/5

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

        public ActionResult AssignPatients(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            DemandPatientAssignmentModel model = new DemandPatientAssignmentModel();
            model.OpdModelList = new List<OpdModel>();
            model.DemandId = id;

            var list = (from om in ent.OpdMasters
                        join
                        pa in ent.DemandPatientAssignments on om.OpdID equals pa.OpdId
                        join
dm in ent.ItemDemandMasters on pa.DemandId equals dm.ItemDemandID
                        where pa.DemandId == model.DemandId && pa.Status == true
                        select new { om, pa, dm }).ToList();
            foreach (var item in list)
            {
                OpdModel omodel = new OpdModel();
                omodel.FirstName = item.om.FirstName;
                omodel.MiddleName = item.om.MiddleName;
                omodel.LastName = item.om.LastName;
                omodel.MobileNumber = item.pa.Remarks;
                omodel.RegistrationMode = item.dm.ItemDemandNo;
                omodel.OpdID = item.om.OpdID;
                model.OpdModelList.Add(omodel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AssignPatients(DemandPatientAssignmentModel model)
        {
            pro.AssignPatients(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult PatientList()
        {
            DemandPatientAssignmentModel model = new DemandPatientAssignmentModel();
            model.OpdModelList = new List<OpdModel>();

            return PartialView("PatientList");
        }

        public ActionResult RemovePatient(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.DemandPatientAssignments.Where(x => x.OpdId == id && x.Status == true).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }




    }
}
