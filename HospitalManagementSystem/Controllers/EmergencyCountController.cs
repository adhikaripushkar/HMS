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
    public class EmergencyCountController : Controller
    {
        //
        // GET: /EmergencyCount/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult EmergencyCountDetails()
        {
            EmergencyCountModel model = new EmergencyCountModel();
            EmergencyCountProvider ECpro = new EmergencyCountProvider();
            return View(model);

        }

        [HttpPost]
        public ActionResult EmergencyCountDetails(DateTime StartDate, DateTime EndDate)
        {
            EmergencyCountModel model = new EmergencyCountModel();
            EmergencyCountProvider ECpro = new EmergencyCountProvider();
            model.MaleNo = ECpro.GetMaleCount(StartDate, EndDate);
            model.FemaleNo = ECpro.GetFemaleCount(StartDate, EndDate);
            model.OtherNo = ECpro.GetOtherCount(StartDate, EndDate);
            model.DischargeNo = ECpro.DischargeCount(StartDate, EndDate);
            //model.DischargeNo = ECpro.GetDischargeCount(StartDate, EndDate);
            model.OnTreatrmentNo = ECpro.StillOnTreatmentCount(StartDate, EndDate);
            model.LAMANo = ECpro.LAMACount(StartDate, EndDate);
            model.NormalCase = ECpro.NormalCaseCount(StartDate, EndDate);
            model.PoliceCase = ECpro.PoliceCaseCount(StartDate, EndDate);

            return View(model);
        }

    }
}
