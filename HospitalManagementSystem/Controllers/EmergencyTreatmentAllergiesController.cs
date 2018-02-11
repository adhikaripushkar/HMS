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
    public class EmergencyTreatmentAllergiesController : Controller
    {
        //
        // GET: /EmergencyTreatmentAllergies/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateForEmergencyTreatmentAllergies(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterId = id;
            return PartialView("_CreateForEmergencyTreatmentAllergies", model);

        }
        [HttpPost]
        public ActionResult CreateForEmergencyTreatmentAllergies(EmergecyMasterModel model)
        {
            EmergencyTreatmentAllergiesProvider ep = new EmergencyTreatmentAllergiesProvider();
            var staffname = model.EmergencyTreatmentAllergiesModel.Staff;
            if (string.IsNullOrWhiteSpace(staffname) == false)
            {
                // string str = Regex.Replace(ipValue, "[^0-9]+", string.Empty);
                string Valuenew = staffname.Trim();
                int BracIndex = staffname.IndexOf('{') + 1;
                int TotalLen = staffname.IndexOf('}') - 1;
                int Len = staffname.Length;
                int toval = Len - BracIndex;
                string value = staffname.Substring(BracIndex, toval - 1);
                int aa = value.IndexOf('^') + 1;
                int bb = value.Length;
                int lenth = bb - aa;
                string id = value.Substring(aa, lenth);
                model.EmergencyTreatmentAllergiesModel.Staff = id;

            }
            int i = ep.InsertEmergencyTreatmentAllergies(model);
            if (i != null)
            {


                int id = (int)model.EmergencyMasterId;
                model.ListEmergencyTreatmentAllergiesModel = ep.GetSelectedData(id);
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return PartialView("_IndexForEmergencyTreatmentAllergies", model);
            }
            else
            {

                int id = (int)model.EmergencyMasterId;
                model.ListEmergencyTreatmentAllergiesModel = ep.GetSelectedData(id);
                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return PartialView("_IndexForEmergencyTreatmentAllergies", model);

            }

        }
        public ActionResult IndexForEmergencyTreatmentAllergies(int id)
        {
            EmergencyTreatmentAllergiesProvider EmTAPro = new EmergencyTreatmentAllergiesProvider();
            EmergecyMasterModel model = new EmergecyMasterModel();

            model.ListEmergencyTreatmentAllergiesModel = EmTAPro.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForEmergencyTreatmentAllergies", model);

            //using (EHMSEntities ent = new EHMSEntities())
            //{

            //    var obj = ent.EmergencyTreatmentAllergies.Where(x => x.TreatmentAllergiesId == ent.EmergencyTreatmentAllergies.Max(y => y.TreatmentAllergiesId)).SingleOrDefault();


            //}
            //model.EmergencyMasterListsModel = emProvider.GetList();

        }
        public ActionResult EditForEmergencyTreatmentAllergies(int id)
        {
            EmergencyTreatmentAllergiesModel model = new EmergencyTreatmentAllergiesModel();
            EmergencyTreatmentAllergiesProvider ep = new EmergencyTreatmentAllergiesProvider();
            //model = ep.GetListForEmergencyTreatmentAllergies().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            model = ep.GetListForEmergencyTreatmentAllergies().Where(x => x.TreatmentAllergiesId == id).FirstOrDefault();
            return PartialView("_EditForEmergencyTreatmentAllergies", model);
        }
        [HttpPost]
        public ActionResult EditForEmergencyTreatmentAllergies(EmergencyTreatmentAllergiesModel modell)
        {
            EmergencyTreatmentAllergiesProvider ep = new EmergencyTreatmentAllergiesProvider();
            var staffname = modell.Staff;
            if (string.IsNullOrWhiteSpace(staffname) == false)
            {
                // string str = Regex.Replace(ipValue, "[^0-9]+", string.Empty);
                string Valuenew = staffname.Trim();
                int BracIndex = staffname.IndexOf('{') + 1;
                int TotalLen = staffname.IndexOf('}') - 1;
                int Len = staffname.Length;
                int toval = Len - BracIndex;
                string value = staffname.Substring(BracIndex, toval - 1);
                int aa = value.IndexOf('^') + 1;
                int bb = value.Length;
                int lenth = bb - aa;
                string id = value.Substring(aa, lenth);
                modell.Staff = id;

            }

            int i = ep.UpdateForEmergencyTreatmentAllergies(modell);
            if (i != null)
            {
                modell.ListEmergencyTreatmentAllergiesModel = ep.GetSelectedData(modell.EmergencyMasterId);

                EmergecyMasterModel model = new EmergecyMasterModel();
                model.ListEmergencyTreatmentAllergiesModel = modell.ListEmergencyTreatmentAllergiesModel;

                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return PartialView("_IndexForEmergencyTreatmentAllergies", model);
            }
            else
            {
                modell.ListEmergencyTreatmentAllergiesModel = ep.GetSelectedData(modell.EmergencyMasterId);

                EmergecyMasterModel model = new EmergecyMasterModel();
                model.ListEmergencyTreatmentAllergiesModel = modell.ListEmergencyTreatmentAllergiesModel;
                TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                return PartialView("_IndexForEmergencyTreatmentAllergies", model);

            }

        }
        public ActionResult AutocompleteItem(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> items = new List<string>();
            var units = ent.SetupEmployeeMasters.Where(x => (x.EmployeeFullName).Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                items.Add(item.EmployeeFullName);
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchStaffName(string s)
        {
            VitalsModel model = new VitalsModel();
            VitalsProvider vp = new VitalsProvider();
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();



            List<StaffForERegisModel> templist = new List<StaffForERegisModel>();
            templist = vp.getStaffForERegisModelList(s).ToList();
            foreach (var item in templist)
            {
                //names.Add(item.EmployeeCode + "{" + item.EmployeeFullName + "}");
                names.Add("{" + item.EmployeeFullName + "^" + item.EmployeeCode + "}");
            }
            return Json(names, JsonRequestBehavior.AllowGet);


        }


    }

}
