using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class DesignationSetupController : Controller
    {
        //
        // GET: /DesignationSetup/
        DesignationSetupProvider pro = new DesignationSetupProvider();
        DesignationSetupModel model = new DesignationSetupModel();
        public ActionResult Index()
        {

            model.DesignationList = pro.GetAll();
            return View(model);
        }

        public ActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Create(DesignationSetupModel model)
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
        public ActionResult Edit(DesignationSetupModel model)
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
