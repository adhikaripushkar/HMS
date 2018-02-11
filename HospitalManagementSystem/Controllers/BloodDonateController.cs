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
    public class BloodDonateController : Controller
    {
        //
        // GET: /BloodDonate/

        BloodDonateProviders bdpro = new BloodDonateProviders();

        public ActionResult Index()
        {

            BloodDonateModel bdModels = new BloodDonateModel();
            bdModels.BloodDonateLists = bdpro.GetList();
            return View(bdModels);
        }
        public ActionResult Create()
        {
            BloodDonateModel model = new BloodDonateModel();
            model.Zone = 1;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(BloodDonateModel model)
        {
            if (ModelState.IsValid)
            {
                bdpro.Insert(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            BloodDonateModel model = new BloodDonateModel();
            model = bdpro.GetList().Where(x => x.BloodDonateId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, BloodDonateModel model)
        {
            if (ModelState.IsValid)
            {
                bdpro.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            bdpro.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
