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
    public class ViatalController : Controller
    {
        //
        // GET: /Viatal/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViatalForm(int id)
        {

            VitalsModel obj = new VitalsModel();
            obj.EmergencyMasterId = id;

            return PartialView("_ViatalForm", obj);
        }
        [HttpPost]
        public ActionResult ViatalForm(VitalsModel model)
        {
            VitalsProvider vp = new VitalsProvider();
            var staffname = model.Staff;
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
                model.Staff = id;

            }

            if (ModelState.IsValid)
            {
                vp.Insertvitals(model);
                VitalsProvider pro = new VitalsProvider();
                int id = (int)model.EmergencyMasterId;
                model.ListVitalModels = pro.GetSelectedData(id);
            }
            return PartialView("_IndexForVitals", model);
        }
        public ActionResult IndexForVitals(int id)
        {
            VitalsModel model = new VitalsModel();
            VitalsProvider pro = new VitalsProvider();
            model.ListVitalModels = pro.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_IndexForVitals", model);
        }
        public ActionResult EditForvitals(int id)
        {

            VitalsModel model = new VitalsModel();
            VitalsProvider vp = new VitalsProvider();


            model = vp.GetListForvitals().Where(x => x.EmergencyVitalId == id).FirstOrDefault();
            return PartialView("_EditForvitals", model);
        }
        [HttpPost]
        public ActionResult EditForvitals(VitalsModel model)
        {
            VitalsProvider vp = new VitalsProvider();
            var staffname = model.Staff;
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
                model.Staff = id;

            }
            if (ModelState.IsValid)
            {
                vp.UpdateForVitals(model);
                model.ListVitalModels = vp.GetSelectedData((int)model.EmergencyMasterId);
            }
            return PartialView("_IndexForVitals", model);
        }
        public ActionResult DeleteForvitals(int id)
        {
            VitalsProvider vp = new VitalsProvider();
            vp.DeleteForVitals(id);
            return RedirectToAction("IndexForVitals");
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
