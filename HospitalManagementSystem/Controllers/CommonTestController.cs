using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace Hospital.Controllers
{
    public class CommonTestController : Controller
    {
        //
        // GET: /CommonTest/

        public ActionResult Index()
        {
            CommonTestSetProvider pro = new CommonTestSetProvider();
            CommonTestSetupModel model = new CommonTestSetupModel();
            model.CommonTestList = pro.GetList().Where(x => x.Status == true).ToList();

            return View(model);


        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(CommonTestSetupModel model)
        {
            CommonTestSetProvider pro = new CommonTestSetProvider();
            int i = pro.Insert(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            CommonTestSetupModel model = new CommonTestSetupModel();
            CommonTestSetProvider pro = new CommonTestSetProvider();
            model = pro.GetList().Where(x => x.HospitalExtraTestId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(CommonTestSetupModel model)
        {

            CommonTestSetProvider pro = new CommonTestSetProvider();
            int i = pro.Update(model);
            if (i != 0)
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return RedirectToAction("index");

            }
            return View(model);

        }


        public ActionResult Delete(int id)
        {
            CommonTestSetProvider pro = new CommonTestSetProvider();
            int i = pro.delete(id);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.delete;



            }
            else
            {
                TempData["success"] = UtilityMessage.deletefailed;
            }
            return RedirectToAction("Index");
        }
    }
}
