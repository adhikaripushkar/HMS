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
    public class StockSubCategoryController : Controller
    {
        StockSubCategoryProvider pro = new StockSubCategoryProvider();
        //
        // GET: /StockSubCategory/

        public ActionResult Index()
        {
            StockSubCategoryModel model = new StockSubCategoryModel();
            model.StockSubCategoryList = pro.GetList();
            return View(model);
        }

        //Get /StockSubCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post /StockSubCategory/Create
        [HttpPost]
        public ActionResult Create(StockSubCategoryModel model)
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

        // GET: /StockSubCategory/Edit/5

        public ActionResult Edit(int id)
        {
            StockSubCategoryModel model = pro.GetList().Where(x => x.StockSubCategoryID == id).FirstOrDefault();
            return View(model);
        }

        //
        // POST: /StockCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(StockSubCategoryModel model)
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

        public ActionResult AutocompleteSubCategory(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> SubCategory = new List<string>();
            var units = ent.SetupStockSubCategories.Where(x => x.StockSubCategoryName.Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                SubCategory.Add(item.StockSubCategoryName);
            }
            return Json(SubCategory, JsonRequestBehavior.AllowGet);
        }

    }
}
