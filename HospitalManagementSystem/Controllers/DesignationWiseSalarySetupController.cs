﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class DesignationWiseSalarySetupController : Controller
    {
        //
        // GET: /Deduction/

        DesignationWiseSalarySetupProvider pro = new DesignationWiseSalarySetupProvider();
        DesignationWiseSalarySetupModel model = new DesignationWiseSalarySetupModel();
        public ActionResult Index()
        {

            model.DesignationWiseSalarySetupModelList = pro.GetAll();
            return View(model);
        }

        public ActionResult Create()
        {


            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DesignationWiseSalarySetupModel model)
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
        public ActionResult Edit(DesignationWiseSalarySetupModel model)
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

