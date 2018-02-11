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
    public class AdmissionDipositFeeController : Controller
    {
        //
        // GET: /AdmissionDipositFee/
        AdmissionDipositFeeProvider pro = new AdmissionDipositFeeProvider();
        public ActionResult Index()
        {
            AdmissionDipositFeeModel model = new AdmissionDipositFeeModel();
            model.AdmissionDipositList = pro.GetAll();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdmissionDipositFeeModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Insert(model);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public ActionResult Edit(int id)
        {
            AdmissionDipositFeeModel model = new AdmissionDipositFeeModel();
            model = pro.GetAll().Where(m => m.AdmissionDipositFeeID == id).SingleOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AdmissionDipositFeeModel model)
        {
            if (model.DepositAmount == 0)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                int i = pro.Update(model);
                return RedirectToAction("Index");

            }
            else
            {
                RedirectToAction("Index");

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
            return RedirectToAction("Index");
        }


    }
}
