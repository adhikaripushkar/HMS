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
    public class SetupOtherTestController : Controller
    {
        // GET: /SetupOtherTest/
        SetupOtherTestModel model = new SetupOtherTestModel();
        SetupOtherTestProvider pro = new SetupOtherTestProvider();

        public ActionResult Index(string DptName)
        {
            int DeptIdInt = 0;
            SetupOtherTestModel model = new SetupOtherTestModel();
            model.SetupOtherTestModelList = pro.Getlist();
            if (!String.IsNullOrEmpty(DptName))
            {
                DeptIdInt = Convert.ToInt32(DptName);
                model.SetupOtherTestModelList = pro.GetlistByDepartmentName(DeptIdInt);
            }
            model.TestTypeID = DeptIdInt;
            return View(model);
        }



        public ActionResult SearchIndex(SetupOtherTestModel model)
        {
            string TestName = string.Empty;
            string DepartmentId = string.Empty;

            if (!string.IsNullOrEmpty(model.OtherTestName))
            {
                TestName = model.OtherTestName.ToLower();
                model.SetupOtherTestModelList = pro.Getlist().Where(x => x.OtherTestName.ToLower().StartsWith(TestName)).ToList();

            }


            return View("Index", model);
        }


        public ActionResult Create(int id)
        {
            if (id > 0)
            {
                if (id >= 3)
                {
                    model.TestTypeID = id;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupOtherTestModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.SetupOtherTests.Where(m => m.OtherTestName == model.OtherTestName).Select(m => m.SetupOtherTestId).ToList();
            if (data.Count() > 0)
            {
                TempData["success"] = "Test Name Already Exist !";
                return View(model);
            }
            else
            {
                int i = pro.insert(model);
                if (i != 0)
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            model = pro.Getlist().Where(x => x.SetupOtherTestId == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(SetupOtherTestModel model)
        {
            int i = pro.Update(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return RedirectToAction("Index");

            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
                return RedirectToAction("Index");

            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
                return RedirectToAction("Index");
            }

        }

        public ActionResult DentalTest()
        {

            return RedirectToAction("Index", new { @DptName = "3" });
        }

        public ActionResult EyeTest()
        {
            return RedirectToAction("Index", new { @DptName = "4" });
        }

        public ActionResult EntTest()
        {
            return RedirectToAction("Index", new { @DptName = "6" });
        }


        public ActionResult PhisiyoTest()
        {
            return RedirectToAction("Index", new { @DptName = "5" });
        }

    }
}
