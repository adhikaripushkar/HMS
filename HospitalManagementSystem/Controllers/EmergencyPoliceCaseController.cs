using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class EmergencyPoliceCaseController : Controller
    {
        //
        // GET: /EmergencyPoliceCase/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            EmergencyPoliceCaseProvider EPCPro = new EmergencyPoliceCaseProvider();
            EmergencyPoliceCaseModel model = new EmergencyPoliceCaseModel();
            model.EmergencyPoliceCaseModelList = EPCPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            EmergencyPoliceCaseModel model = new EmergencyPoliceCaseModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(EmergencyPoliceCaseModel model)
        {
            EmergencyPoliceCaseProvider EPCPro = new EmergencyPoliceCaseProvider();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                //var data = ent.EmergencyPoliceCase.Where(m => m == model.BlockName).Select(m => m.BlockId).ToList();
                //if (data.Count == 0)
                //{
                int i = EPCPro.Insert(model);
                return RedirectToAction("Index");
                //    if (i != null)
                //    {

                //        TempData["success"] = Hospital.UtilityMessage.save;
                //        return RedirectToAction("Index");
                //    }
                //    else
                //    {
                //        TempData["success"] = Hospital.UtilityMessage.savefailed;
                //        return RedirectToAction("Index");

                //    }
                //}
                //else
                //{
                //    TempData["success"] = "Block Name Already Exist !";
                //    return RedirectToAction("Index");

                //}

            }


            return View();

        }
        public ActionResult Edit(int id)
        {
            EmergencyPoliceCaseProvider EPCPro = new EmergencyPoliceCaseProvider();
            EmergencyPoliceCaseModel model = new EmergencyPoliceCaseModel();
            model = EPCPro.GetList().Where(x => x.PCId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EmergencyPoliceCaseModel model)
        {
            if (ModelState.IsValid)
            {
                EmergencyPoliceCaseProvider EPCPro = new EmergencyPoliceCaseProvider();
                int i = EPCPro.Update(model);
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
        public ActionResult BillForm(int id)
        {
            EmergencyPoliceCaseProvider EPCPro = new EmergencyPoliceCaseProvider();
            EmergencyPoliceCaseModel model = new EmergencyPoliceCaseModel();
            model = EPCPro.GetList().Where(x => x.PCId == id).FirstOrDefault();
            return View(model);
        }


    }
}
