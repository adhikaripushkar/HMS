using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class SetupEmergencyCaseController : Controller
    {
        //
        // GET: /SetupEmergencyCase/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            SetupEmergencyCaseProvider SECPro = new SetupEmergencyCaseProvider();
            SetupEmergencyCaseModel model = new SetupEmergencyCaseModel();
            model.SetupBlockModelList = SECPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            SetupEmergencyCaseModel model = new SetupEmergencyCaseModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetupEmergencyCaseModel model)
        {
            SetupEmergencyCaseProvider SECPro = new SetupEmergencyCaseProvider();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                var data = ent.SetupEmergencyCases.Where(m => m.CaseName == model.CaseName).Select(m => m.CaseId).ToList();
                if (data.Count == 0)
                {
                    int i = SECPro.Insert(model);
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
                    TempData["success"] = "Case Name Already Exist !";
                    return RedirectToAction("Index");

                }

            }


            return View();

        }
        public ActionResult Edit(int id)
        {
            SetupEmergencyCaseProvider SECPro = new SetupEmergencyCaseProvider();
            SetupEmergencyCaseModel model = new SetupEmergencyCaseModel();
            model = SECPro.GetList().Where(x => x.CaseId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupEmergencyCaseModel model)
        {
            if (ModelState.IsValid)
            {
                SetupEmergencyCaseProvider SECPro = new SetupEmergencyCaseProvider();
                int i = SECPro.Update(model);
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
            SetupEmergencyCaseProvider SECPro = new SetupEmergencyCaseProvider();
            int i = SECPro.Delete(id);
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
