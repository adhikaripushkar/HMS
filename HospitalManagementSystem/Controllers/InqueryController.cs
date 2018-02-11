using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class InqueryController : Controller
    {
        DepositMasterModel model = new DepositMasterModel();
        DepositMasterProvider pro = new DepositMasterProvider();
        OpdModel modelOpd = new OpdModel();
        OpdProvider proOpd = new OpdProvider();
        //
        // GET: /Inquery/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPatient()
        {
            OpdModel model = new OpdModel();
            return View(model);
        }
        // [HttpPost]
        public ActionResult SearchPatient1(int srchVal, string value)
        {

            InqueryProvider pro = new InqueryProvider();
            OpdModel model = new OpdModel();
            model.OpdModelList = pro.GetDepositForPatient(srchVal, value);
            return View(model);

        }
        public ActionResult SearchDoctorBYDept()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchDoctorBYDept(int srchDetpId)
        {

            {
                DoctorPartialDetails dcmodel = new DoctorPartialDetails();
                InqueryProvider Ip = new InqueryProvider();
                dcmodel.DoctorPartialDetailsList = Ip.Getlist(srchDetpId);
                return View(dcmodel);
            }



        }

        public ActionResult SearchDoctorAutocomplete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchDoctorAutocomplete(string value)
        {
            {
                DoctorPartialDetails dcmodel = new DoctorPartialDetails();
                InqueryProvider Ip = new InqueryProvider();
                dcmodel.DoctorPartialDetailsList = Ip.GetlistByDName(value);
                return View(dcmodel);
            }


        }
        public ActionResult AutocompleteItem(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> items = new List<string>();
            var units = ent.SetupDoctorMasters.Where(x => (x.FirstName + " " + (x.MiddleName + " " ?? string.Empty) + x.LastName).Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                items.Add(item.FirstName + " " + (item.MiddleName + " " ?? string.Empty) + item.LastName);
            }
            //return Json(new { Text = items, value = items }, JsonRequestBehavior.AllowGet);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocomplereOpdMaster(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> items = new List<string>();
            var units = ent.OpdMasters.Where(x => (x.FirstName + " " + (x.MiddleName + " " ?? string.Empty) + x.LastName).Contains(s) && x.Status == true).ToList();
            foreach (var item in units)
            {
                items.Add(item.FirstName + " " + (item.MiddleName + " " ?? string.Empty) + item.LastName);
            }
            //return Json(new { Text = items, value = items }, JsonRequestBehavior.AllowGet);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IpdSearch()
        {

            return View();
        }

        public ActionResult ShowResult(string sEarchVal, string iNputVal)
        {
            InqueryProvider pro = new InqueryProvider();
            IpdInqueryReportModel model = new IpdInqueryReportModel();
            model.IpdInqueryReportList = pro.IpdSearchByColumnName(Convert.ToInt32(sEarchVal), iNputVal);
            return PartialView(model);
        }
    }

}
