using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
namespace HospitalManagementSystem.Controllers
{
    public class IpdNurseReportController : Controller
    {
        //
        // GET: /IpdNurseReport/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult NurseDailyReport(int OpdId)
        {
            IpdSearchResults model = new IpdSearchResults();
            EHMSEntities ent = new EHMSEntities();

            IpdNurseDailyReportProvider pro = new IpdNurseDailyReportProvider();

            int IpdResistrationID = ent.IpdRegistrationMasters.Where(m => m.OpdNoEmergencyNo == OpdId && m.Status == true).Select(m => m.IpdRegistrationID).FirstOrDefault();
            model = pro.GetNurseDailyReport(IpdResistrationID);
            model.OpdId = OpdId;
            model.IpdRegistrationNumber = IpdResistrationID;

            return PartialView(model);
        }

    }
}
