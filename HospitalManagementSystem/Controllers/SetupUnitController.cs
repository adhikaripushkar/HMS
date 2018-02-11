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
    public class SetupUnitController : Controller
    {
        //
        // GET: /SetupUnit/
        SetupUnitProviders pro = new SetupUnitProviders();
        public ActionResult Index()
        {
            SetupUnitModel model = new SetupUnitModel();
            model.UnitLists = pro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {

            SetupUnitModel model = new SetupUnitModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupUnitModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Insert(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Created Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Creation Failed !";
                }
                return RedirectToAction("Index");
            }
            return View(model);


        }
        public ActionResult Edit(int id)
        {
            SetupUnitModel model = new SetupUnitModel();
            model = pro.GetList().Where(x => x.UnitId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupUnitModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Update(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Updated Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Updation Failed !";
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {

            int i = pro.Delete(id);
            if (i == 1)
            {
                TempData["success"] = "Record Deleted Successfully !";

            }
            else
            {
                TempData["success"] = "Record Deletion Failed !";
            }
            return RedirectToAction("Index");
        }

    }
}
