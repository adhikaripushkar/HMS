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
    public class DoctorFeeSetupController : Controller
    {
        //
        // GET: /DoctorFeeSetup/
        DoctorFeeSetupProvider pro = new DoctorFeeSetupProvider();
        public ActionResult Index()
        {
            DoctorFeeSetupModel model = new DoctorFeeSetupModel();
            model.DoctorFeeSetupModelList = pro.GetDoctorFeeList();
            return View(model);
        }

        public ActionResult Create()
        {
            DoctorFeeSetupModel model = new DoctorFeeSetupModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DoctorFeeSetupModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Insert(model);
                if (i != 0)
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
            DoctorFeeSetupModel model = new DoctorFeeSetupModel();
            model = pro.GetDoctorFeeList().Where(x => x.DoctorFeeID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, DoctorFeeSetupModel model)
        {
            if (ModelState.IsValid)
            {
                //update
                int i = pro.Update(model);
                if (i != 0)
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                    return RedirectToAction("Index");

                }

            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);

            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
                return RedirectToAction("Index");


            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
                return RedirectToAction("Index");

            }
            //return RedirectToAction("Index");
        }
    }
}
