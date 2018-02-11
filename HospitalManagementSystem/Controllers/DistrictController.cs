using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class DistrictController : Controller
    {
        //
        // GET: /District/
        EHMSEntities ent = new EHMSEntities();
        DistrictModel model = new DistrictModel();
        DistrictProvider pro = new DistrictProvider();

        public ActionResult Index()
        {
            model.DistrictModelList = pro.PopulateDistrict();
            return View(model);
        }
        public ActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DistrictModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.Insertdistrict(model))
                {
                    TempData["Msg"] = "Data has been Saved successfully";
                }
                else
                    TempData["MsgError"] = "Data has not been succeessfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            model = pro.PopulateDistrict().Where(x => x.DistrictID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, DistrictModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.Editdistrict(id, model))
                {
                    TempData["Msg"] = "Data has been Updated Succeessfully";
                }
                else
                    TempData["Msg"] = "Data has not been Updated succeessfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (pro.Deletedistrict(id))
                {
                    TempData["Msg"] = "Data has been Deleted Succeessfully";
                }
                else
                    TempData["Msg"] = "Data has not been Saved Succeessfully";
            }
            return RedirectToAction("Index");
        }
    }
}