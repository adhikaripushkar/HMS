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
    public class EmergencyTstCaridController : Controller
    {
        //
        // GET: /EmergencyTstCarid/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateEmergencyTestCarid(int id)
        {

            EmergecyMasterModel obj = new EmergecyMasterModel();
            obj.EmergencyMasterId = id;

            return PartialView("_CreateEmergencyTestCarid", obj);
        }
        [HttpPost]
        public ActionResult CreateEmergencyTestCarid(EmergecyMasterModel model)
        {
            EmergencyTstCaridProvider ETCP = new EmergencyTstCaridProvider();




            int i = ETCP.InsertEPatientTstCarried(model);
            if (i != null)
            {


                int id = (int)model.EmergencyMasterId;
                model.ListEmergencyTstCaridModel = ETCP.GetSelectedData(id);
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return PartialView("_IndexForEmergencyTestCaried", model);
            }
            else
            {

                int id = (int)model.EmergencyMasterId;
                model.ListEmergencyTstCaridModel = ETCP.GetSelectedData(id);
                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return PartialView("_IndexForEmergencyTestCaried", model);

            }

            //return PartialView("_IndexForVitals", model);




        }
        public ActionResult EditForEmergencyTestCarreid(int id)
        {
            EmergencyTstCaridModel model = new EmergencyTstCaridModel();

            EmergencyTstCaridProvider ETCP = new EmergencyTstCaridProvider();
            model = ETCP.GetListForEmergencyPatientTestCarried().Where(x => x.TestId == id).FirstOrDefault();
            return PartialView("_EditForEmergencyTestCarreid", model);
        }
        [HttpPost]
        public ActionResult EditForEmergencyTestCarreid(EmergencyTstCaridModel modell)
        {

            EmergencyTstCaridProvider ETCP = new EmergencyTstCaridProvider();
            int i = ETCP.UpdateForEmergencyTestCarried(modell);
            if (i != null)
            {
                modell.ListEmergencyTstCaridModel = ETCP.GetSelectedData(modell.EmergencyMasterId);

                EmergecyMasterModel model = new EmergecyMasterModel();
                model.ListEmergencyTstCaridModel = modell.ListEmergencyTstCaridModel;

                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return PartialView("_IndexForEmergencyTestCaried", model);
            }
            else
            {
                modell.ListEmergencyTstCaridModel = ETCP.GetSelectedData(modell.EmergencyMasterId);

                EmergecyMasterModel model = new EmergecyMasterModel();
                model.ListEmergencyTstCaridModel = modell.ListEmergencyTstCaridModel;
                TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                return PartialView("_IndexForEmergencyTestCaried", model);

            }

        }
        public ActionResult IndexForEmergencyTestCaried(int id)
        {

            EmergecyMasterModel model = new EmergecyMasterModel();
            //VitalsModel model = new VitalsModel();
            EmergencyTstCaridProvider ETCP = new EmergencyTstCaridProvider();
            model.ListEmergencyTstCaridModel = ETCP.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForEmergencyTestCaried", model);
        }
        public ActionResult AddMoreSectionandTest()
        {
            EmergencyTstCaridModel model = new EmergencyTstCaridModel();
            return PartialView("AddMoreSectionTest", model);
        }


    }
}
