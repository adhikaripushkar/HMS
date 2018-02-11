using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ZoneController : Controller
    {
        //
        // GET: /Zone/
        EHMSEntities ent = new EHMSEntities();
        ZoneProvider pro = new ZoneProvider();
        ZoneModel model = new ZoneModel();

        public ActionResult Index()
        {
            model.ZoneRecordsModelList = pro.PopulateRecords();
            return View(model);
        }
        public ActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ZoneModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.InsetZone(model).Successmessage == "Success")
                {
                    TempData["MsgFail"] = "Data has Not been saved succeessfully";
                }
                else
                {
                    TempData["MsgError"] = model.ErrorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            model = pro.PopulateRecords().Where(x => x.ZoneID == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, ZoneModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.EditZone(id, model).Successmessage == "Success")
                {
                    TempData["Msg"] = "Data has been Updated Successfully";
                }
                else if (model.Successmessage == "Fail")
                {
                    TempData["MsgFail"] = "Data has not been Saved Succeessfully";
                }
                else
                {
                    TempData["MsgError"] = model.ErrorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (pro.DeleteZone(id))
                {
                    TempData["Msg"] = "Data has been Deleted successfully";
                }
                else
                    TempData["Msg"] = "Data has Not been Deleted succeessfully";
            }
            return RedirectToAction("Index");
        }
    }
}
