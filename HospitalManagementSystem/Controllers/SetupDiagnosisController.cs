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
    public class SetupDiagnosisController : Controller
    {
        //
        // GET: /SetupDiagnosis/

        SetupDiagnosisProvider pro = new SetupDiagnosisProvider();
        public ActionResult Index()
        {
            SetupDiagnosisModel model = new SetupDiagnosisModel();
            model.DiagnosisLists = pro.GetDiagnosisList();
            return View(model);
        }
        public ActionResult Create()
        {

            SetupDiagnosisModel model = new SetupDiagnosisModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupDiagnosisModel model)
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
            SetupDiagnosisModel model = new SetupDiagnosisModel();
            model = pro.GetDiagnosisList().Where(x => x.DiagnosisId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupDiagnosisModel model)
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
