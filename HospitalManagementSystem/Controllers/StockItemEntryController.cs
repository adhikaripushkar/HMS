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
    public class StockItemEntryController : Controller
    {
        //
        // GET: /StockItemEntry/
        StockItemEntryProvider pro = new StockItemEntryProvider();
        public ActionResult Index()
        {
            StockItemEntryModel model = new StockItemEntryModel();
            model.StockItemEntryList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockItemEntry/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StockItemEntry/Create

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();
            StockItemEntryModel model = new StockItemEntryModel();
            try
            {
                model.StockCategoryId = ent.SetupStockCategories.Where(x => x.Status == true).FirstOrDefault().StockCategoryID;
            }
            catch
            {


            }
            return View(model);
        }

        //
        // POST: /StockItemEntry/Create

        [HttpPost]
        public ActionResult Create(StockItemEntryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    if (pro.Insert(model))
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["success"] = "Record with this item name already exists in database!";
                        return View(model);
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /StockItemEntry/Edit/5

        public ActionResult Edit(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).SingleOrDefault();
            StockItemEntryModel model = new StockItemEntryModel();
            AutoMapper.Mapper.Map(obj, model);
            return View(model);
        }

        //
        // POST: /StockItemEntry/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StockItemEntryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    pro.Update(model);
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
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
        }

        //
        // GET: /StockItemEntry/Delete/5

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockItemMasters.Where(x => x.StockItemEntryId == id).SingleOrDefault();
            obj.Status = false;
            obj.SetupStockItemEntry.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
            return RedirectToAction("Index");
        }

        //
        // POST: /StockItemEntry/Delete/5

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

        public ActionResult AutocompleteItem(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> items = new List<string>();
            var units = ent.SetupStockItemEntries.Where(x => x.StockItemName.Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                items.Add(item.StockItemName);
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
