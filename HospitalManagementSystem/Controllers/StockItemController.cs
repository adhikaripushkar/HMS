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
    public class StockItemController : Controller
    {
        //
        // GET: /StockItem/
        StockItemProvider pro = new StockItemProvider();
        public ActionResult Index()
        {
            StockItemModel model = new StockItemModel();
            model.StockItemList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockItem/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StockItem/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StockItem/Create

        [HttpPost]
        public ActionResult Create(StockItemModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    int i = pro.Insert(model);
                    if (i == 1)
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;

                    }
                    else
                    {
                        TempData["success"] = "Record with this name already exists in database!";
                        return View(model);
                    }
                    // return RedirectToAction("Index");
                }
                catch (Exception e)
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
        // GET: /StockItem/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /StockItem/Edit/5

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
        // GET: /StockItem/Delete/5

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
        // POST: /StockItem/Delete/5

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

        public ActionResult AutocompleteItemType(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> types = new List<string>();
            var list = ent.SetupStockItemTypes.Where(x => x.StockItemTypeName.Contains(s) && x.Status == true).ToList();
            foreach (var item in list)
            {
                types.Add(item.StockItemTypeName);
            }
            return Json(types, JsonRequestBehavior.AllowGet);
        }

    }
}
