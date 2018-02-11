using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DoctorSetupController : Controller
    {
        //
        // GET: /DoctorSetup/
        DoctorSetupProvider pro = new DoctorSetupProvider();
        public ActionResult Index(string searchString)
        {
            string searchWords = searchString;
            DoctorSetupModel model = new DoctorSetupModel();
            model.DoctorSetupModelList = pro.Getlist();
            if (!String.IsNullOrEmpty(searchWords))
            {
                model.DoctorSetupModelList = pro.GetlistBySearchWord(searchWords);
            }
            return View(model);
        }

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();
            DoctorSetupModel model = new DoctorSetupModel();
            List<CheckBoxList> CheckBoxListTemp = new List<CheckBoxList>();
            var data = ent.SetupDepartments.ToList();
            foreach (var item in data)
            {
                CheckBoxList chk = new CheckBoxList();
                chk.DeptID = item.DeptID;
                chk.DeptName = item.DeptName;
                chk.isSelected = false;
                CheckBoxListTemp.Add(chk);
            }
            model.checkBoxlistModel = CheckBoxListTemp;
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(DoctorSetupModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Insert(model);
                if (i != 0)
                {
                    TempData["success"] = "Record Created Successfully !";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = "Record Creation Failed !";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            DoctorSetupModel model = new DoctorSetupModel();
            model = pro.Getlist().Where(x => x.DoctorID == id).FirstOrDefault();

            List<CheckBoxList> CheckBoxListTemp = new List<CheckBoxList>();
            var data = ent.SetupDepartments.ToList();
            foreach (var item in data)
            {

                CheckBoxList chk = new CheckBoxList();
                chk.DeptID = item.DeptID;
                chk.DeptName = item.DeptName;
                if (Utility.isExists(item.DeptID, id))
                {
                    chk.isSelected = true;
                }
                else
                {
                    chk.isSelected = false;
                }
                CheckBoxListTemp.Add(chk);
            }
            model.checkBoxlistModel = CheckBoxListTemp;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, DoctorSetupModel model)
        {
            if (ModelState.IsValid)
            {
                int i = pro.Update(model);
                if (i != 0)
                {
                    TempData["success"] = "Record Updated Successfully !";
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = "Record Updation Failed !";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);

            if (i != 0)
            {
                TempData["success"] = "Record Deleted Successfully !";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Record Deletion Failed !";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TestToCheck(DoctorSetupModel model)
        {

            return View();
        }




        public ActionResult test(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            DoctorSetupModel model = new DoctorSetupModel();
            model = pro.Getlist().Where(x => x.DoctorID == id).FirstOrDefault();

            List<CheckBoxList> CheckBoxListTemp = new List<CheckBoxList>();
            var data = ent.SetupDepartments.ToList();
            foreach (var item in data)
            {

                CheckBoxList chk = new CheckBoxList();
                chk.DeptID = item.DeptID;
                chk.DeptName = item.DeptName;
                if (Utility.isExists(item.DeptID, id))
                {
                    chk.isSelected = true;
                }
                else
                {
                    chk.isSelected = false;
                }
                CheckBoxListTemp.Add(chk);
            }
            model.checkBoxlistModel = CheckBoxListTemp;

            //return View(model);

            return PartialView("_test", model);

        }
    }
}
