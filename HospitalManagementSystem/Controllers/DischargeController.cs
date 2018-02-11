using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Provider;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DischargeController : Controller
    {
        //
        // GET: /Discharge/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditForDischarge(int id)
        {
            DischargeProvider dprovider = new DischargeProvider();
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = dprovider.GetList().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            return PartialView("_EditForDischarge", model);
        }

        [HttpPost]
        public ActionResult EditForDischarge(EmergecyMasterModel model)
        {
            DischargeProvider dprovider = new DischargeProvider();
            if (ModelState.IsValid)
            {
                dprovider.Update(model);
                model.EmergencyMasterModelList = dprovider.GetSelectedData(model.EmergencyMasterId);



            }
            return PartialView("_IndexForDischarge", model);
        }
        public ActionResult IndexForDischarge(int id)
        {
            DischargeProvider dprovider = new DischargeProvider();
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = dprovider.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForDischarge", model);
        }

    }
}
