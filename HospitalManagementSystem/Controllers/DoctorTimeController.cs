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
    public class DoctorTimeController : Controller
    {
        //
        // GET: /DoctorTime/
        DoctorTimeProvider pro = new DoctorTimeProvider();
        public ActionResult Index()
        {
            DoctorTimeModel model = new DoctorTimeModel();
            model.DoctorTimeList = pro.GetDoctosrNameList();
            return View(model);
        }

        public ActionResult Create()
        {
            DoctorTimeModel model = new DoctorTimeModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DoctorTimeModel model)
        {


            if (model.DoctorTimeList == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                foreach (var item in model.DoctorTimeList)
                {
                    if (item.StartTime == null || item.EndTime == null)
                    {
                        TempData["notSave"] = "0";
                        return RedirectToAction("Create");
                    }
                }
            }


            EHMSEntities ent = new EHMSEntities();
            if (ModelState.IsValid)
            {
                ViewBag.Data = ent.SetupDoctorAvailableTimes.Where(m => m.DoctorID == model.DoctorID).ToList();
                int i = 0;

                i = pro.Insert(model);

                if (i != 0)
                {
                    TempData["success"] = UtilityMessage.save;
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }


                //ViewBag.Data = ent.SetupDoctorAvailableTime.Where(m => m.DoctorID == model.DoctorID).ToList();


            }
            return RedirectToAction("Index");
        }

        public ActionResult AddMoreDoctorTime()
        {
            DoctorTimeModel model = new DoctorTimeModel();
            return PartialView("AddMoreDoctorTime", model);
        }
        public ActionResult DoctorTimeSearh()
        {
            EHMSEntities ent = new EHMSEntities();
            DoctorTimeModel model = new DoctorTimeModel();
            //model.selectList=new SelectList (ent.SetupDoctorMaster.ToList(),"","");
            return View(model);
        }
        public ActionResult _DoctorTimeDetails(int DoctorID, string DoctorDays)
        {
            DoctorTimeModel model = new DoctorTimeModel();
            model.DoctorTimeList = pro.GetDoctorTimeDetails(DoctorID, DoctorDays);
            if (model.DoctorTimeList.Count == 0)
            {
                TempData["NOTFOUND"] = "0";
            }
            return PartialView("_DoctorTimeDetails", model);

        }
        public ActionResult ShowAllData(int id)
        {
            DoctorTimeModel model = new DoctorTimeModel();
            model.DoctorTimeList = pro.GetById(id);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            DoctorTimeModel model = new DoctorTimeModel();
            model.DoctorTimeList = pro.EditData(id);
            return PartialView("Edit", model);
        }

        public ActionResult GetData(TimeSpan stime, TimeSpan etime, int cp, int wp, int did)
        {


            DoctorTimeModel model = new DoctorTimeModel();
            model.StartTime = stime;
            model.EndTime = etime;
            model.CheckPatientPerDay = cp;
            model.WebPatientPerDay = wp;
            model.DoctorAvailableTimeId = did;
            DoctorTimeProvider pro = new DoctorTimeProvider();
            pro.Update(model);

            return null;
        }
        public ActionResult IndividualDoctorTimeDetails(int id)
        {
            DoctorTimeModel model = new DoctorTimeModel();
            model.DoctorTimeList = pro.GetById(id);
            return PartialView(model);
        }



    }
}
