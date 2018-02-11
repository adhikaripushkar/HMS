using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class VDCMUNController : Controller
    {
        //
        // GET: /VDCMUN/
        EHMSEntities ent = new EHMSEntities();
        VDCMUNModel model = new VDCMUNModel();
        VDCMUNProvider pro = new VDCMUNProvider();
        public ActionResult Index()
        {
            model.VdcMunModelList = pro.PopulateRecords();
            return View(model);
        }
        public ActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(VDCMUNModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.InsertVdcMun(model))
                {
                    TempData["Msg"] = "Data has been Saved succeessfully";
                }
                else
                {
                    TempData["MsgError"] = "Data has Not been Saved succeessfully";
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            model = pro.PopulateRecords().Where(x => x.VdcMunID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, VDCMUNModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.EditVdcMun(id, model))
                {
                    TempData["Msg"] = "Data has been Updated succeessfully";
                }
                else
                    TempData["MsgError"] = "Data has Not been Updated succeessfully";

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (pro.DeleteVdcmun(id))
                {
                    TempData["Msg"] = "Data has been Deleted succeessfully";
                }
                else
                    TempData["Msg"] = "Data has Not been Deleted succeessfully";
            }
            return RedirectToAction("Index");
        }
    }
}
