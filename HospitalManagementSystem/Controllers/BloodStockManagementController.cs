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
    public class BloodStockManagementController : Controller
    {
        //
        // GET: /BloodStockManagement/
        BloodStockManagementProviders stockProviders = new BloodStockManagementProviders();

        public ActionResult Index()
        {
            BloodStockManagementModel model = new BloodStockManagementModel();
            model.BloodStockList = stockProviders.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            BloodStockManagementModel model = new BloodStockManagementModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(BloodStockManagementModel model)
        {
            if (ModelState.IsValid)
            {
                stockProviders.Insert(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            BloodStockManagementModel model = new BloodStockManagementModel();
            model = stockProviders.GetList().Where(x => x.BloodStockId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, BloodStockManagementModel model)
        {
            if (ModelState.IsValid)
            {
                stockProviders.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            stockProviders.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
