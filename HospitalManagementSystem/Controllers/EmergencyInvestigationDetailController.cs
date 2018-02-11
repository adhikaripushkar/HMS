using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class EmergencyInvestigationDetailController : Controller
    {
        //
        // GET: /EmergencyInvestigationDetail/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexForEI(int id)
        {
            EmergencyInvestigationDetailModel model = new EmergencyInvestigationDetailModel();
            EmergencyInvestigationDetailProvider pro = new EmergencyInvestigationDetailProvider();
            model.ListEmergencyInvestigationDetailModel = pro.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForEI", model);
        }
        public ActionResult CreateEI(int id)
        {

            EmergecyMasterModel obj = new EmergecyMasterModel();
            obj.EmergencyInvestigationListModel = new EmergencyInvestigationListModel();
            obj.EmergencyMasterId = id;
            for (int i = 1; i <= 1; i++)
            {

                var mod = new EmergencyInvestigationListModel();

                obj.EmergencyInvestigationList.Add(mod);

            }

            return PartialView("_CreateEI", obj);
        }
        [HttpPost]
        public ActionResult CreateEI(EmergecyMasterModel model)
        {
            EmergencyInvestigationDetailProvider pro = new EmergencyInvestigationDetailProvider();


            int id = (int)model.EmergencyMasterId;
            pro.InsertEI(model, id);

            EmergencyInvestigationDetailModel model1 = new EmergencyInvestigationDetailModel();

            model1.ListEmergencyInvestigationDetailModel = pro.GetSelectedData(id);
            return PartialView("_IndexForEI", model1);
        }
        public ActionResult EditEI(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            EmergecyMasterModel model = new EmergecyMasterModel();
            var EmergencyInvList = ent.EmergencyInvestigationDetails.Where(x => x.EmergencyMasterId == id).ToList();
            foreach (var item in EmergencyInvList)
            {
                EmergencyInvestigationListModel EIDM = new EmergencyInvestigationListModel();
                //EIDM.EmergencyMasterId =id;
                EIDM.TestID = item.TestId;
                EIDM.Remarks = item.Remarks;
                model.EmergencyInvestigationList.Add(EIDM);
            }
            model.EmergencyMasterId = id;
            return PartialView("_EditEI", model);
        }
        [HttpPost]
        public ActionResult EditEI(EmergecyMasterModel model)
        {
            EmergencyInvestigationDetailProvider pro = new EmergencyInvestigationDetailProvider();
            int id = (int)model.EmergencyMasterId;
            pro.Udate(id, model);
            TempData["message"] = "Successfully Updated!";
            EmergencyInvestigationDetailModel model1 = new EmergencyInvestigationDetailModel();

            model1.ListEmergencyInvestigationDetailModel = pro.GetSelectedData(id);
            return PartialView("_IndexForEI", model1);
        }
        [HttpPost]
        public ActionResult AddInvestigationList()
        {
            EmergencyInvestigationListModel model = new EmergencyInvestigationListModel();
            return PartialView("addInvestigation", model);
        }

    }
}
