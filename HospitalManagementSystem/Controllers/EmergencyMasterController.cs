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
    public class EmergencyMasterController : Controller
    {
        //
        // GET: /EmergencyMaster/
        EmergencyMasterProvider emProvider = new EmergencyMasterProvider();



        public ActionResult Main()
        {
            return View();
        }


        public ActionResult Index(string searchString)
        {
            string searchWords = searchString;
            //var obj = ent.EmergencyMaster.Where(x => x.EmergencyMasterId == ent.EmergencyMaster.Max(y => y.EmergencyMasterId)).SingleOrDefault();
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyTriageModel = new EmergencyTriageModel();

            model.OpdEmergencyViewModelList = emProvider.GetEmergencyList();

            // Hospital.Utility.funForDischarge(24);

            foreach (var item in model.OpdEmergencyViewModelList)
            {

                item.dischargeCheck = HospitalManagementSystem.Utility.funForDischarge(item.EmergencyId);

            }
            if (!String.IsNullOrEmpty(searchWords))
            {
                model.OpdEmergencyViewModelList = emProvider.GetlistBySearchWord(searchWords);
            }




            return View(model);
        }

        public ActionResult DischargePatientList(string searchString)
        {
            string searchWords = searchString;

            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyTriageModel = new EmergencyTriageModel();

            model.OpdEmergencyViewModelList = emProvider.GetEmergencyDischargeList();

            // Hospital.Utility.funForDischarge(24);

            foreach (var item in model.OpdEmergencyViewModelList)
            {


                item.dischargeCheck = HospitalManagementSystem.Utility.funForDischarge(item.EmergencyId);

            }
            if (!String.IsNullOrEmpty(searchWords))
            {
                model.OpdEmergencyViewModelList = emProvider.GetlistBySearchWord(searchWords);
            }




            return View(model);
        }
        public ActionResult Create()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyTriageModel = new EmergencyTriageModel();
            model.EmergencyFeeDetailsModel = new EmergencyFeeDetailsModel();
            model.ObjOpdMaster = new OpdModel();
            model.EmergencyFeeDetailsModel.RegistrationFee = HospitalManagementSystem.Utility.GetEmergencyFees();
            //model.EmergencyFeeDetailsModel.RegistrationFee = Hospital.Utility.GetEmergencyFeesFromSetupHospitalFee();

            model.ObjOpdMaster.CountryID = 153;
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(EmergecyMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            //var data = ent.EmergencyMaster.Where(m => m.SerialNumber == model.SerialNumber || m.EmergencyNumber == model.EmergencyNumber).Select(m => m.EmergencyMasterId).ToList();
            //if (data.Count == 0)
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (model.StringICD != null)
            {
                var IcdNumber = model.StringICD.Substring(model.StringICD.IndexOf('~') + 1);

                model.IcdCodeNumber = Convert.ToInt32(ent.SetupIcdCodes.Where(m => m.CodeNumber == IcdNumber).Select(m => m.ICDCODEID).FirstOrDefault());
            }
            if (ModelState.IsValid)
            {
                int i = emProvider.Insert(model);
                if (i != 0)
                {
                    var maxemermstid = ent.EmergencyMasters.Max(x => x.EmergencyMasterId);
                    model.OpdEmergencyViewModel = new OpdEmergencyViewModel();
                    model.OpdEmergencyViewModel = emProvider.GetEmegrencyDetails(Convert.ToInt32(maxemermstid));

                    return View("BillForm", model);


                    //return View();
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }


            }
            return View(model);

        }


        public ActionResult Edit(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = emProvider.GetList().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            model.OpdEmergencyViewModel = emProvider.GetEmergencyList().Where(x => x.EmergencyId == id).FirstOrDefault();
            model.EmergencyFeeDetailsModel = emProvider.GetEmergencyPatientFeeDetailsList(id).LastOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EmergecyMasterModel model)
        {
            //if (ModelState.IsValid)
            //{
            int i = emProvider.Update(model);
            if (i != null)
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                return RedirectToAction("Index");

            }
            //return View(model);
            //}
            //return View(model);
        }
        public ActionResult Delete(int id)
        {
            emProvider.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult More(int id)
        {
            EmergecyMasterModel obj = new EmergecyMasterModel();
            obj.EmergencyMasterId = id;
            obj.ObjOpdMaster.FirstName = HospitalManagementSystem.Utility.getEmergencyPatientName(id);
            return View(obj);
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult _EmergencySearch(int EmergencyMasterid, string name)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();

            return PartialView(model);
        }
        public ActionResult BillForm(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.OpdEmergencyViewModel = new OpdEmergencyViewModel();

            model.OpdEmergencyViewModel = emProvider.GetEmegrencyDetails(id);
            return View(model);
        }
        public ActionResult DischargeReport(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = emProvider.GetEmergencyDetailsForDischargeReport(id);
            model.OpdEmergencyViewModel = emProvider.GetEmergencyList().Where(x => x.EmergencyId == id).FirstOrDefault();
            return View(model);
        }
        public ActionResult DischargePatientReport(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = emProvider.GetEmergencyDetailsForDischargeReport(id);
            model.OpdEmergencyViewModel = emProvider.GetEmergencyDischargeList().Where(x => x.EmergencyId == id).FirstOrDefault();
            return View(model);
        }
        public ActionResult UpdateEmergencyStatus(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            emProvider.UpdateStatus(id);
            model.EmergencyTriageModel = new EmergencyTriageModel();
            model.OpdEmergencyViewModelList = emProvider.GetEmergencyList();
            foreach (var item in model.OpdEmergencyViewModelList)
            {


                item.dischargeCheck = HospitalManagementSystem.Utility.funForDischarge(item.EmergencyId);

            }
            TempData["success"] = HospitalManagementSystem.UtilityMessage.Discharge;
            return View("Index", model);
        }
        public ActionResult Details(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = emProvider.GetList().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            model.OpdEmergencyViewModel = emProvider.GetEmergencyList().Where(x => x.EmergencyId == id).FirstOrDefault();
            model.EmergencyFeeDetailsModel = emProvider.GetEmergencyPatientFeeDetailsList(id).LastOrDefault();
            return PartialView("_EmergencyDetails", model);
        }
        public ActionResult DischargeDetails(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = emProvider.GetDischargeList().Where(x => x.EmergencyMasterId == id).FirstOrDefault();
            model.OpdEmergencyViewModel = emProvider.GetEmergencyDischargeList().Where(x => x.EmergencyId == id).FirstOrDefault();
            model.EmergencyFeeDetailsModel = emProvider.GetEmergencyPatientFeeDetailsList(id).LastOrDefault();
            return PartialView("_EmergencyDetails", model);
        }

        public ActionResult CensusFormEmergency()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.OpdEmergencyViewModel = new OpdEmergencyViewModel();
            model.OpdEmergencyViewDischargeModel = new OpdEmergencyViewDischargeModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CensusFormEmergency(EmergecyMasterModel model)
        {
            model.OpdEmergencyViewModelList = emProvider.GetEmergencylistByRegistrationDate(model.OpdEmergencyViewModel.StartDate, model.OpdEmergencyViewModel.EndDate);

            model.OpdEmergencyViewDischargeModelList = emProvider.GetEmergencylistByRegistrationDateDischarge(model.OpdEmergencyViewDischargeModel.StartDate, model.OpdEmergencyViewDischargeModel.EndDate);

            return View(model);
        }
        public ActionResult CensusFormEmergencyDischarge()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.OpdEmergencyViewDischargeModel = new OpdEmergencyViewDischargeModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CensusformEmergencyDischarge(EmergecyMasterModel model)
        {
            model.OpdEmergencyViewDischargeModelList = emProvider.GetEmergencylistByRegistrationDateDischarge(model.OpdEmergencyViewDischargeModel.StartDate, model.OpdEmergencyViewDischargeModel.EndDate);

            return View(model);
        }
        public ActionResult EmergencyDailyCensus(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            EmergencyMasterProvider emProvider = new EmergencyMasterProvider();
            //This is only for report purpose ie change dynamically shared layout pages
            if (id == 1)
            {
                model.SerialNumber = 100;
            }
            else
            {
                model.SerialNumber = 101;
            }
            return View(model);

        }
        [HttpPost]
        public ActionResult EmergencyDailyCensus(DateTime CreatedDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            EmergencyMasterProvider emProvider = new EmergencyMasterProvider();
            //model.EmergencyMasterModelList = emProvider.GetList().Where(x => x.Date == CreatedDate).ToList();

            model.OpdEmergencyViewModelList = emProvider.GetEmergencyListForCencus(CreatedDate).ToList();
            return View(model);
        }


    }

}
