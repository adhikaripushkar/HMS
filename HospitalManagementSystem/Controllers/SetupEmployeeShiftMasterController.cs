using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Provider;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class SetupEmployeeShiftMasterController : Controller
    {
        //
        // GET: /SetupEmployeeShiftMaster/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            SetupEmployeeShiftMasterProviders EmShiftPro = new SetupEmployeeShiftMasterProviders();
            SetupEmployeeShiftMasterModel model = new SetupEmployeeShiftMasterModel();
            model.SetupEmployeeShiftMasterModelList = EmShiftPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            SetupEmployeeShiftMasterModel model = new SetupEmployeeShiftMasterModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetupEmployeeShiftMasterModel model)
        {
            SetupEmployeeShiftMasterProviders EmShiftPro = new SetupEmployeeShiftMasterProviders();

            if (ModelState.IsValid)
            {
                int i = EmShiftPro.Insert(model);
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


            return View();

        }
        public ActionResult Edit(int id)
        {
            SetupEmployeeShiftMasterProviders EmShiftPro = new SetupEmployeeShiftMasterProviders();
            SetupEmployeeShiftMasterModel model = new SetupEmployeeShiftMasterModel();
            model = EmShiftPro.GetList().Where(x => x.EmployeeShiftMasterId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupEmployeeShiftMasterModel model)
        {
            if (ModelState.IsValid)
            {
                SetupEmployeeShiftMasterProviders EmShiftPro = new SetupEmployeeShiftMasterProviders();
                int i = EmShiftPro.Update(model);
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
            SetupEmployeeShiftMasterProviders EmShiftPro = new SetupEmployeeShiftMasterProviders();
            int i = EmShiftPro.Delete(id);
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
