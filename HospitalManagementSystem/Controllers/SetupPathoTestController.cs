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
    public class SetupPathoTestController : Controller
    {
        //
        // GET: /SetupPathoTest/
        SetupPathoTestProviders pro = new SetupPathoTestProviders();

        public ActionResult Index()
        {
            SetupPathoTestModel model = new SetupPathoTestModel();
            model.PathoTestList = pro.GetList();

            return View(model);
        }

        public ActionResult SearchIndex(string TestName)
        {
            SetupPathoTestModel model = new SetupPathoTestModel();
            string PathoTestName = string.Empty;
            if (!string.IsNullOrEmpty(TestName))
            {
                TestName = TestName.ToLower();
                model.PathoTestList = pro.GetList().Where(x => x.TestName.ToLower().StartsWith(TestName)).ToList();

            }
            return View("Index", model);
        }



        public ActionResult Create()
        {

            SetupPathoTestModel model = new SetupPathoTestModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupPathoTestModel model)
        {
            if (ModelState.IsValid)
            {
                //int i = pro.Insert(model);
                //if (i == 1)
                //{
                //    TempData["success"] = "Record Created Successfully !";

                //}
                //else
                //{
                //    TempData["success"] = "Record Creation Failed !";
                //}
                //return RedirectToAction("Index");

                EHMSEntities ent = new EHMSEntities();
                var data = ent.SetupPathoTests.Where(m => m.TestName == model.TestName).Select(m => m.TestId).ToList();
                if (data.Count == 0)
                {
                    int i = pro.Insert(model);
                    if (i == 1)
                    {

                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    TempData["success"] = "Test Name Already Exist !";
                    return RedirectToAction("Index");

                }

            }

            return View(model);


        }
        public ActionResult Edit(int id)
        {
            SetupPathoTestModel model = new SetupPathoTestModel();
            model = pro.GetList().Where(x => x.TestId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupPathoTestModel model)
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
