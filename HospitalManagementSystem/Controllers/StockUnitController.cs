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
    public class StockUnitController : Controller
    {
        //
        // GET: /StockUnit/
        StockUnitProvider pro = new StockUnitProvider();
        public ActionResult Index()
        {
            StockUnitModel model = new StockUnitModel();
            model.UnitList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockUnit/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StockUnit/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StockUnit/Create

        [HttpPost]
        public ActionResult Create(StockUnitModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    int i = pro.Insert(model);
                    if (i != 0)
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                    }
                    else
                    {
                        TempData["message"] = "Record with this name already exists in database!";
                        return View(model);
                    }

                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /StockUnit/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /StockUnit/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StockUnit/Delete/5

        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /StockUnit/Delete/5

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

        public ActionResult AutocompleteUnit(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> unitnames = new List<string>();
            var units = ent.SetupStockUnits.Where(x => x.StockUnitName.Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                unitnames.Add(item.StockUnitName);
            }
            return Json(unitnames, JsonRequestBehavior.AllowGet);
        }

    }
}
