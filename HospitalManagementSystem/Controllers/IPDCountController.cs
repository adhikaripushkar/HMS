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
    public class IpdCountController : Controller
    {
        public ActionResult IpdCountElderly()
        {
            IpdCountModel model = new IpdCountModel();
            IpdCountProviders IpdPro = new IpdCountProviders();
            model.TotalIpdPatient = IpdPro.GetTotalIpdPatient();
            model.TotalIpdMalePatient = IpdPro.GetTotalIpdMalePatient();
            model.TotalIpdFemalePatient = IpdPro.GetTotalIpdFemalePatient();
            return View(model);

        }

        [HttpPost]
        public ActionResult IpdCountElderly(string year, int month)
        {
            IpdCountModel model = new IpdCountModel();
            IpdCountProviders IpdPro = new IpdCountProviders();

            model = IpdPro.getjanuarycount(year, month);

            model.TotalIpdPatient = IpdPro.GetTotalIpdPatient();
            model.Year = year;

            model.Month = month;
            model.TotalIpdMalePatient = IpdPro.GetTotalIpdMalePatient();
            model.TotalIpdFemalePatient = IpdPro.GetTotalIpdFemalePatient();


            return View(model);
        }


    }
}
