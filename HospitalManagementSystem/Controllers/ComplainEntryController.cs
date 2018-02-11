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
    public class ComplainEntryController : Controller
    {
        //
        // GET: /ComplainEntry/


        public ActionResult EditForComplainEntry(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            EmergencyMasterProvider emProvider = new EmergencyMasterProvider();
            model = emProvider.GetList().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            return PartialView("_EditForComplainEntry", model);
        }

        [HttpPost]
        public ActionResult EditForComplainEntry(EmergecyMasterModel model)
        {
            if (ModelState.IsValid)
            {
                ComplainEntryProviders cmProvider = new ComplainEntryProviders();
                cmProvider.UpdateEmasterForComplainEntry(model);
                model.EmergencyMasterModelList = cmProvider.GetSelectedData(model.EmergencyMasterId);
            }
            return PartialView("_IndexForComplainEntry", model);
        }
        public ActionResult ShowDashBoard()
        {
            return PartialView("_ShowDashBoardForEmergency");
        }

        public ActionResult IndexForComplainEntry(int id)
        {
            ComplainEntryProviders cmProvider = new ComplainEntryProviders();
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = cmProvider.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForComplainEntry", model);
        }

    }
}
