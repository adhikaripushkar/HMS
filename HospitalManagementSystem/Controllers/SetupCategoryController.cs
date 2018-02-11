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
    public class StockCategoryController : Controller
    {
        StockCategoryProvider pro = new StockCategoryProvider();
        //
        // GET: /StockCategory/

        public ActionResult Index()
        {
            StockCategoryModel model = new StockCategoryModel();
            model.StockCategoryList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StockCategory/Create

        [HttpPost]
        public ActionResult Create(StockCategoryModel model)
        {
            try
            {
                int i = pro.Insert(model);
                if (i != 0)
                    TempData["success"] = UtilityMessage.save;
                else
                    TempData["success"] = UtilityMessage.savefailed;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["success"] = UtilityMessage.savefailed;
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /StockCategory/Edit/5

        public ActionResult Edit(int id)
        {
            StockCategoryModel model = pro.GetList().Where(x => x.StockCategoryID == id).FirstOrDefault();
            return View(model);
        }

        //
        // POST: /StockCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(StockCategoryModel model)
        {
            try
            {
                // TODO: Add update logic here
                int i = pro.Update(model);
                if (i != 0)
                    TempData["success"] = UtilityMessage.edit;
                else
                    TempData["success"] = UtilityMessage.editfailed;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["success"] = UtilityMessage.editfailed;
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /StockCategory/Delete/5

        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);
            if (i != 0)
                TempData["success"] = UtilityMessage.delete;
            else
                TempData["success"] = UtilityMessage.deletefailed;
            return RedirectToAction("Index");
        }


        public ActionResult AutocompleteCategory(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> Category = new List<string>();
            var units = ent.SetupStockCategories.Where(x => x.StockCategoryName.Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                Category.Add(item.StockCategoryName);
            }
            return Json(Category, JsonRequestBehavior.AllowGet);
        }

    }
}
