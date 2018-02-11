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
    public class FiscalYearController : Controller
    {
        //
        // GET: /FiscalYear/
        FiscalYearProvider pro = new FiscalYearProvider();
        public ActionResult Index()
        {
            FiscalYearModel model = new FiscalYearModel();

            model.FiscalYearList = pro.GetList();

            return View(model);
        }

        //
        // GET: /FiscalYear/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FiscalYear/Create

        public ActionResult Create()
        {
            FiscalYearModel model = new FiscalYearModel();
            return View(model);
        }

        //
        // POST: /FiscalYear/Create

        [HttpPost]
        public ActionResult Create(FiscalYearModel model)
        {
            int flag;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    flag = pro.Insert(model);
                    if (flag == 1)
                    {
                        TempData["success"] = "Record Created Successfully !";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Fiscal year name (in BS or AD) already exists in database.";
                        return View(model);
                    }
                }
                else return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FiscalYear/Edit/5

        public ActionResult Edit(int id)
        {
            FiscalYearModel model = new FiscalYearModel();
            model = pro.GetObject(id);
            return View(model);
        }

        //
        // POST: /FiscalYear/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FiscalYearModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    pro.Update(model);
                    TempData["success"] = "Record Updated Successfully !";
                    return RedirectToAction("Index");
                }
                else return View(model);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ChageStatus(int id)
        {
            pro.ChangToCurrent(id);
            return RedirectToAction("Index");
        }
        //
        // GET: /FiscalYear/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FiscalYear/Delete/5

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
    }
}
