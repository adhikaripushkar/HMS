using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class SetupEmergencyInvestigationMasterController : Controller
    {
        //
        // GET: /SetupEmergencyInvestigationMaster/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            SetupEmergencyInvestigationMasterProvider SEIPro = new SetupEmergencyInvestigationMasterProvider();
            SetupEmergencyInvestigationMasterModel model = new SetupEmergencyInvestigationMasterModel();
            model.SetupEmergencyInvestigationMasterModelList = SEIPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            SetupEmergencyInvestigationMasterModel model = new SetupEmergencyInvestigationMasterModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetupEmergencyInvestigationMasterModel model)
        {
            SetupEmergencyInvestigationMasterProvider SEIPro = new SetupEmergencyInvestigationMasterProvider();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                var data = ent.EmergencyInvestigationMasters.Where(m => m.TestName == model.TestName).Select(m => m.EInvId).ToList();
                if (data.Count == 0)
                {
                    int i = SEIPro.Insert(model);
                    if (i != null)
                    {

                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    TempData["success"] = "Test Name Already Exist !";
                    return RedirectToAction("Index");

                }

            }


            return View();

        }
        public ActionResult Edit(int id)
        {
            SetupEmergencyInvestigationMasterProvider SEIPro = new SetupEmergencyInvestigationMasterProvider();
            SetupEmergencyInvestigationMasterModel model = new SetupEmergencyInvestigationMasterModel();
            model = SEIPro.GetList().Where(x => x.EInvId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupEmergencyInvestigationMasterModel model)
        {
            if (ModelState.IsValid)
            {
                SetupEmergencyInvestigationMasterProvider SEIPro = new SetupEmergencyInvestigationMasterProvider();
                int i = SEIPro.Update(model);
                if (i != null)
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                    return RedirectToAction("Index");

                }
                //return View(model);
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            SetupEmergencyInvestigationMasterProvider SEIPro = new SetupEmergencyInvestigationMasterProvider();
            int i = SEIPro.Delete(id);
            if (i != null)
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
                return RedirectToAction("Index");

            }
        }
    }
}
