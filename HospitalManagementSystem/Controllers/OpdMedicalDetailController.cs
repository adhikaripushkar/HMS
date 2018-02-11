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
    public class OpdMedicalDetailController : Controller
    {
        //
        // GET: /OpdMedicalDetail/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        PreRegistrationProvider prePro = new PreRegistrationProvider();
        OpdProvider pro = new OpdProvider();
        OpdMedicalProvider OMPro = new OpdMedicalProvider();

        public ActionResult Index()
        {

            //var obj = ent.EmergencyMaster.Where(x => x.EmergencyMasterId == ent.EmergencyMaster.Max(y => y.EmergencyMasterId)).SingleOrDefault();
            OpdModel model = new OpdModel();
            model.OpdMedicalDetailModel = new OpdMedicalDetailModel();
            model.OpdModelList = OMPro.Getlist();

            return View(model);
        }

        public ActionResult Create()
        {
            OpdModel model = new OpdModel();
            model.OpdMedicalDetailModel = new OpdMedicalDetailModel();
            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdDoctorListModel = new OpdDoctorListModel();
            model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
            model.OpdFeeDetailsModel.TotalAmount = Utility.GetGrandtotalamount();
            model.OpdMedicalDetailModel.Amount = HospitalManagementSystem.Utility.GetGrandtotalamount();


            for (int i = 1; i <= 1; i++)
            {

                var mod = new OpdDoctorListModel();

                model.OpdDoctorList.Add(mod);

            }

            return View(model);
        }



        [HttpPost]
        public ActionResult Create(OpdModel model)
        {
            var a = model.OpdMedicalDetailModel.PreHolo;
            var holo = HospitalManagementSystem.Utility.GetHologramAmount();
            var pre = HospitalManagementSystem.Utility.GetPremedicalamount();
            if (model.OpdMedicalDetailModel.PreHolo == "Pre-Medical")
            {
                model.OpdMedicalDetailModel.Commission = model.OpdMedicalDetailModel.Amount - model.OpdMedicalDetailModel.Discount - pre;
            }
            else
            {
                model.OpdMedicalDetailModel.Commission = model.OpdMedicalDetailModel.Amount - model.OpdMedicalDetailModel.Discount - holo;
            }

            int i = OMPro.Insert(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return RedirectToAction("Index");
            }
            else
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return RedirectToAction("Index");
            }




        }



        [HttpPost]
        public ActionResult AddDoctorList()
        {
            OpdDoctorListModel model = new OpdDoctorListModel();
            return PartialView("AddDoctorList", model);
        }
        public ActionResult Edit(int id)
        {
            OpdModel model = new OpdModel();

            model = pro.Getlist().Where(x => x.OpdID == id).FirstOrDefault();
            model.OpdDoctorList = pro.GetPatientDoctorList(id);
            var a = HospitalManagementSystem.Utility.GetGrandtotalamount();
            model.OpdMedicalDetailModel = new OpdMedicalDetailModel();
            model.OpdMedicalDetailModel.Amount = a;
            model.OpdMedicalDetailModel.Commission = Utility.GetCommision(id);
            model.OpdMedicalDetailModel.Discount = Utility.GetDiscount(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, OpdModel model)
        {
            OpdMedicalProvider OMPro = new OpdMedicalProvider();
            if (ModelState.IsValid)
            {
                //update here
                var holo = HospitalManagementSystem.Utility.GetHologramAmount();
                var pre = HospitalManagementSystem.Utility.GetPremedicalamount();
                if (model.OpdMedicalDetailModel.PreHolo == "Pre-Medical")
                {
                    model.OpdMedicalDetailModel.Commission = model.OpdMedicalDetailModel.Amount - pre;
                }
                else
                {
                    model.OpdMedicalDetailModel.Commission = model.OpdMedicalDetailModel.Amount - holo;
                }
                int i = OMPro.Update(model);
                if (i != 0)
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");

                }

            }
            return View(model);

        }
        public ActionResult BillForm(int id)
        {
            OpdModel model = new OpdModel();
            model = OMPro.GetOpdDetails(id);
            return View(model);
        }
        public ActionResult calulateagenttotal()
        {


            OpdModel model = new OpdModel();
            model.OpdMedicalDetailModel = new OpdMedicalDetailModel();
            model.OpdMedicalDetailList = OMPro.GetAgentAndtotalAmount();

            return View(model);
        }
        public ActionResult AgentTotalDetail(int id)
        {

            OpdMedicalProvider OMPro = new OpdMedicalProvider();
            OpdMedicalDetailModel model = new OpdMedicalDetailModel();
            model.OpdMedicalDetailList = OMPro.GetAgentDetail(id);
            return View(model);
        }



    }
}
