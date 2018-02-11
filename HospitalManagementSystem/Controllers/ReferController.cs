using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ReferController : Controller
    {
        //
        // GET: /Refer/
        EHMSEntities ent = new EHMSEntities();
        ReferProvider pro = new ReferProvider();
        ReferModel model = new ReferModel();

        public ActionResult Index()
        {
            model.ReferModelList = pro.PopulateRefer();
            return View(model);
        }
        public ActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ReferModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.InsertRefer(model).Successmessage == "Success")
                {
                    TempData["MsgFail"] = "Data Has Not Been saved Successfully";
                }
                else
                {
                    TempData["MsgError"] = model.ErrorMessage;
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            model = pro.PopulateRefer().Where(x => x.Id == id).FirstOrDefault();
            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(int id, ReferModel model)
        {
            if (ModelState.IsValid)
            {
                if (pro.EditRefer(id, model).Successmessage == "Success")
                {
                    TempData["Msg"] = "Data has been Updated Successfully";
                }
                else if (model.Successmessage == "Fail")
                {
                    TempData["MsgFail"] = "Data Has Not Been Saved Successfully";
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
                if (pro.DeleteRefer(id))
                {
                    TempData["Msg"] = "Data Has Been Deleted Successfully";
                }
                else
                    TempData["Msg"] = "Data Has Not Been Deleted Succeessfully";
            }
            return RedirectToAction("Index");
        }
    }
}
