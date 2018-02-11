using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace Hospital.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        //
        // GET: /Setting/

        DoctorTypeSetupProvider pro = new DoctorTypeSetupProvider();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DoctorSpecialist()
        {
            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            model = pro.GetDoctorSpecialistList();
            return View(model);
        }
        public ActionResult CreateDoctorSpecialist()
        {
            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            return View(model);
        }
    }
}
