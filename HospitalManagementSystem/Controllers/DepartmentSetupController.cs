using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DepartmentSetupController : Controller
    {
        DepartmentSetupProvider pro = new DepartmentSetupProvider();
        // GET: DepartmentSetup
        public ActionResult Main()
        {

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeptMain(string searchString)
        {
            string searchWords = searchString;

            DepartmentSetupProvider proDept = new DepartmentSetupProvider();
            DepartmentSetupModel deptModlObj = new DepartmentSetupModel();

            deptModlObj.DepartmentSetupList = proDept.GetList();

            if (!String.IsNullOrEmpty(searchWords))
            {
                deptModlObj.DepartmentSetupList = proDept.GetlistBySearchWord(searchWords);
            }
            return View("DeptMain", deptModlObj);

        }
        public ActionResult Create()
        {
            DepartmentSetupModel model = new DepartmentSetupModel();
            return PartialView("_VUC_Add", model);
        }
        [HttpPost]
        public ActionResult Create(DepartmentSetupModel model)
        {
            DepartmentSetupProvider pro = new DepartmentSetupProvider();

            if (ModelState.IsValid)
            {
                int i = pro.Insert(model);

                if (i == 1)
                {
                    TempData["Success"] = "Record Created Succussfully !";
                    return RedirectToAction("DeptMain");
                }
                if (i == 0)
                {

                    TempData["Exist"] = "Department Name Already Exist !";
                    return RedirectToAction("DeptMain");
                }

            }

            return View("_VUC_Add");
        }
        public ActionResult Edit(int id)
        {
            DepartmentSetupProvider pro = new DepartmentSetupProvider();

            DepartmentSetupModel model = new DepartmentSetupModel();
            model = pro.GetList().Where(x => x.DeptID == id).FirstOrDefault();
            return View("_Edit", model);
        }


        [HttpPost]
        public ActionResult _Edit(DepartmentSetupModel model)
        {
            DepartmentSetupProvider pro = new DepartmentSetupProvider();
            if (ModelState.IsValid)
            {
                int i = pro.Update(model);
                if (i == 1)
                {
                    TempData["Update"] = "Record Updated Successfully !";
                    return RedirectToAction("DeptMain");
                }
            }
            return View("_VUC_Add", model);
        }


        public ActionResult Delete(int id)
        {
            //Don't delete department

            int i = pro.Delete(id);
            if (i == 1)
            {
                TempData["delete"] = " Record Deleted Successfully !";
                return RedirectToAction("DeptMain");
            }
            return RedirectToAction("DeptMain");
        }
    }
}


