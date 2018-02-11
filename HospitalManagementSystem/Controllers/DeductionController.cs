using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class DeductionController : Controller
    {
        //
        // GET: /Deduction/

        DeductionProvider pro = new DeductionProvider();
        DeductionSetupModel model = new DeductionSetupModel();
        public ActionResult Index()
        {

            model.DecuDeductionList = pro.GetAll();
            return View(model);
        }

        public ActionResult Create()
        {


            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DeductionSetupModel model)
        {
            if (ModelState.IsValid)
            {
                pro.Insert(model);
                TempData["success"] = "Record Created Successfully !";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        public ActionResult Edit(int id)
        {
            model = pro.GetAll().Where(m => m.ID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(DeductionSetupModel model)
        {
            pro.Update(model);
            TempData["success"] = "Record Updated Successfully !";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            TempData["success"] = "Record Deleted Successfully !";
            return RedirectToAction("Index");
        }
    }

}

