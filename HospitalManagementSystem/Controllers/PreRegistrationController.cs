using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;
using System.Net;
using System.Text;
using System.IO;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class PreRegistrationController : Controller
    {

        PreRegistrationProvider PreRegistrationProvider = new PreRegistrationProvider();


        public ActionResult Main()
        {

            return View();


        }


        public ActionResult Index()
        {

            PreRegistrationModel model = new PreRegistrationModel();
            model.PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();
            model.PreRegistrationModelList = PreRegistrationProvider.GetResults();

            return View(model);
        }

        public ActionResult Create()
        {
            PreRegistrationModel model = new PreRegistrationModel();

            model.PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();
            model.PreregistrationPreferDetailsModel.DepartmentID = 1;

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(PreRegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                int i = PreRegistrationProvider.Insert(model);
                if (i != 0)
                {
                    TempData["success"] = UtilityMessage.save;

                }
                else
                {
                    TempData["success"] = UtilityMessage.savefailed;
                }
            }



            return RedirectToAction("Index");


        }

        public ActionResult Edit(int id)
        {

            PreRegistrationModel model = new PreRegistrationModel();
            model = PreRegistrationProvider.GetList().Where(x => x.PreRegistrationID == id).FirstOrDefault();

            model.PreRegistrationPreferModelList = PreRegistrationProvider.GetEdit(id);



            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(int id, PreRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PreRegistrationPreferModelList == null)
                {
                    TempData["success"] = "Update Failed U must add Doctor and Department";
                    return RedirectToAction("Edit/" + id);

                }

                else
                {
                    int i = PreRegistrationProvider.Update(model, id);
                    if (i != 0)
                    {
                        TempData["success"] = UtilityMessage.edit;

                    }
                    else
                    {
                        TempData["success"] = UtilityMessage.editfailed;
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {

            int i = PreRegistrationProvider.Delete(id);
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

        public ActionResult Print(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationModel model = new PreRegistrationModel();
                model.PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();
                model = PreRegistrationProvider.GetListForPrint(id);
                model.PreRegistrationID = id;
                model.PreregistrationPreferDetailsModel = PreRegistrationProvider.GetListForPrintNext(id);
                return View(model);

            }


        }



        public ActionResult AddMoreDepartmentDoctors()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationPreferDetailsModel models = new PreRegistrationPreferDetailsModel();

                return PartialView("AddMoreDepartmentDoctors", models);
            }

        }
        public ActionResult CheckAvailibility(int doctorid, DateTime datesid)
        {
            PreRegistrationProvider pro = new PreRegistrationProvider();
            string day = datesid.DayOfWeek.ToString();

            PreRegistrationDoctorTime model = new PreRegistrationDoctorTime();
            model.PreRegistrationDoctorTimeList = pro.DoctorTime(doctorid, day);


            return PartialView(model);


        }
        public ActionResult DoctorDisease(int DiseaseID)
        {
            PreRegistrationProvider pro = new PreRegistrationProvider();
            PreRegistrationDoctorDisease model = new PreRegistrationDoctorDisease();
            model.PreRegistrationDoctorDiseaseList = pro.GetDoctorDisease(DiseaseID);
            return PartialView(model);

        }
        public ActionResult ReportPreRegistration()
        {
            ReportModel model = new ReportModel();
            return View();


        }

        public ActionResult PreRegistrationReportRegMode(DateTime FromDateRegMode, DateTime ToDateRegMode, string Regstrationmode)
        {
            PreRegistrationProvider pro = new PreRegistrationProvider();
            ReportModel model = new ReportModel();
            model = pro.countRegistrationmode(FromDateRegMode, ToDateRegMode, Regstrationmode);
            ViewBag.Total = model;
            return View("ReportPreRegistration", model);


        }

    }
}
