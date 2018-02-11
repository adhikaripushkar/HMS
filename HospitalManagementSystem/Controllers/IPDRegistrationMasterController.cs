using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{

    [Authorize]

    public class IpdRegistrationMasterController : Controller
    {
        // int textdata; int DepartmentId;
        //
        // GET: /IPDRegistration/
        IpdRegistrationMasterProvider Ipdprovider = new IpdRegistrationMasterProvider();
        static int i = 1;
        public ActionResult Index(string FromDateString, string TodateString, string NameOfPatient)
        {
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            DateTime TodateTime = DateTime.Today;
            DateTime FromDateTime = DateTime.Today;

            if (String.IsNullOrEmpty(FromDateString))
            {
                DateTime FromDate = DateTime.Today;
                FromDateString = FromDate.ToShortDateString();
                FromDateTime = Convert.ToDateTime(FromDateString);

            }
            else
            {
                FromDateTime = Convert.ToDateTime(FromDateString);
            }

            if (String.IsNullOrEmpty(TodateString))
            {
                DateTime ToDate = DateTime.Today;
                TodateString = ToDate.ToShortDateString();
                TodateTime = Convert.ToDateTime(TodateString);
            }
            else
            {
                TodateTime = Convert.ToDateTime(TodateString);
            }

            model.IpdRegistrationMasterModelList = Ipdprovider.GetListForIndex().Where(x => x.RegistrationDate >= FromDateTime && x.RegistrationDate <= TodateTime).ToList();
            //model.IpdRegistrationMasterModelList = model.IpdRegistrationMasterModelList.Where(x => x.RegistrationDate >= TodateTime && x.RegistrationDate<=FromDateTime).ToList();

            return View(model);
        }

        public ActionResult PatientSearch()
        {
            return View();
        }

        public ActionResult CreateFormCent(int id)
        {

            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            var data = model.opdModelList;
            model.passvalues = 1;
            model.OpdNoEmergencyNo = id;
            return View("Create", model);

        }
        //[CustomizedAuthorizeAttribute]
        //[Authorize(Roles = "Sundar")]
        public ActionResult Create(int id = 0)
        {
            i = 1;
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            var data = model.opdModelList;
            model.OpdNoEmergencyNo = id;
            model.passvalues = 1;
            //ViewBag.lstOfKaramchariName = Hospital.Utility.GetPatientNameFromOpdMaster();
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(IpdRegistrationMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var opd = ent.OpdMasters.Where(m => m.OpdID == model.OpdNoEmergencyNo && m.Status == true).Select(m => m.OpdID).ToList();

            if (model.ObjIpdPatientDiagnosisDetailsModel.DiagnosisID != 0)
            {
                model.ipdPatientDiagnosisDetailList.Add(model.ObjIpdPatientDiagnosisDetailsModel);
            }

            if (model.StringICD != null)
            {
                var IcdNumber = model.StringICD.Substring(model.StringICD.IndexOf('~') + 1);

                model.IcdCodeNumber = Convert.ToInt32(ent.SetupIcdCodes.Where(m => m.CodeNumber == IcdNumber).Select(m => m.ICDCODEID).FirstOrDefault());
            }


            var ipdidonirm = ent.IpdRegistrationMasters.Where(x => x.OpdNoEmergencyNo == model.OpdNoEmergencyNo && x.Status == true).ToList();
            if (ipdidonirm.Count > 0)
            {

                TempData["existPatientinIpd"] = "Patient Already Exist In IPD";
                return View(model);

            }


            if (model.OpdNoEmergencyNo == 0 || model.RegistrationDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                TempData["Failed"] = "0";
            }

            else if (opd.Count == 0)
            {
                TempData["Failed"] = "0";
            }
            else
            {

                if (ModelState.IsValid)
                {
                    //   model.DepartmentID = (int)TempData["id"];

                    int i = Ipdprovider.Insert(model);
                    if (i == 1)
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        var maxid = ent.IpdRegistrationMasters.Max(x => x.IpdRegistrationID);
                        //return RedirectToAction("BillForm", new { id = maxid, DepartmentID = model.DepartmentID, OpdEmerNo = model.OpdNoEmergencyNo });
                        //return RedirectToAction("Index");
                        //Get patientDepositNumber
                        int DepositId = HospitalManagementSystem.Utility.GetLastDepositMasterIdFromPatientId(model.OpdNoEmergencyNo);
                        if (DepositId > 0)
                        {
                            return RedirectToAction("DepositeReceipt", new { id = DepositId, IPNO = maxid });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        TempData["Failed"] = "0";
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult DepositeReceipt(int id, int IPNO)
        {
            DepositMasterModel obj = new DepositMasterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.PatientDepositMasters.Where(x => x.PatientDepositMasterId == id).FirstOrDefault();
                obj = AutoMapper.Mapper.Map<PatientDepositMaster, DepositMasterModel>(data);
                obj.PatientDepartmentId = IPNO;
            }
            return View(obj);

        }



        //sShow EntryBed Before
        public ActionResult _GetPatientDetails(int OpdNoEmergencyNo)
        {

            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();

            ViewBag.DetailsData = pro.GetOpdModelList(OpdNoEmergencyNo);
            return PartialView("_GetPatientDetails", model);


        }

        public ActionResult Search()
        {
            IpdSearchResults model = new IpdSearchResults();

            model.IpdSearchResultList = ViewBag.List;

            return View(model);

        }
        [HttpPost]
        public ActionResult Search(IpdSearchResults model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.Search(model.WardNo, model.RoomType, model.RoomNo);
            return View(model);

        }


        public JsonResult SearchAutoComplete(string term)
        {
            EHMSEntities ent = new EHMSEntities();
            string searchResult = string.Empty;

            var firstname = (from a in ent.OpdMasters
                             where a.FirstName.Contains(term)
                             select new { a.FirstName }).Distinct();
            return Json(firstname, JsonRequestBehavior.AllowGet);

        }
        //public ActionResult OpdSearchDetails(string FirstName)
        //{
        //    EHMSEntities ent=new EHMSEntities();
        //    IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
        //    IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
        //    ViewBag.data = pro.SearchByPatientName(FirstName);
        //    return PartialView("_OpdDetailsdata", model);
        //}


        public ActionResult SearchByName()
        {
            IpdSearchResults model = new IpdSearchResults();
            return View(model);
        }
        public ActionResult _SearchByNameData(string firstname)
        {
            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.SearchByPatientName(firstname);
            return PartialView("_SearchByName", model);

        }


        public ActionResult ScanfileUploadinFolder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ScanfileUploadinFolder(ScanFileUploadModel model, HttpPostedFileBase file)
        {
            EHMSEntities ent = new EHMSEntities();
            ScanFileUPloadPorvider pro = new ScanFileUPloadPorvider();

            var data = ent.IpdRegistrationMasters.Where(m => m.DepartmentID == model.DepartmentID && m.OpdNoEmergencyNo == model.PatientID && m.Status == true).Select(m => m.OpdNoEmergencyNo).ToList();
            if (data.Count == 0)
            {
                TempData["Failed"] = "0";
            }

            if (file != null && file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/ScanFile/"), filename);
                model.FilePath = path;
                model.FileName = filename;
                if (ModelState.IsValid)
                {
                    pro.insert(model);
                    file.SaveAs(path);
                }

            }
            return RedirectToAction("Create");
        }

        public ActionResult Change(int id, int DepartmentID, int OpdEmerNo)
        {

            EHMSEntities ent = new EHMSEntities();
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            model.OpdNoEmergencyNo = OpdEmerNo;
            model.DepartmentID = DepartmentID;
            model.passvalues = 1;
            int ipdno = ent.IpdRegistrationMasters.Where(x => x.OpdNoEmergencyNo == OpdEmerNo && x.Status == true).SingleOrDefault().IpdRegistrationID;
            model.ipdPatientDiagnosisDetailList = new List<IpdPatientDiagnosisDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<IpdPatientDiagnosisDetail>, IEnumerable<IpdPatientDiagnosisDetailsModel>>(ent.IpdPatientDiagnosisDetails.Where(x => x.IpdRegistrationID == ipdno)));
            return View(model);
        }
        [HttpPost]
        public ActionResult Change(IpdRegistrationMasterModel model)
        {

            if (model.RegistrationDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                TempData["Failed"] = "0";

            }

            else
            {

                if (ModelState.IsValid)
                {
                    int i = Ipdprovider.Insert(model);
                    if (i == 1)
                    {
                        TempData["success"] = "created successfully";

                    }
                    else
                    {
                        TempData["Failed"] = "0";
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(model);


        }


        //sStart With Medical Details Here//


        public ActionResult NurseDailyRecord()
        {

            return View();
        }
        public ActionResult ShowIpdDetailForIpdMRCommonTest(string FirstName)
        {
            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.SearchByPatientName(FirstName);
            return PartialView("_OpdDetailsdata", model);
        }



        public ActionResult PatientForIpd(int id)
        {

            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.PatientForIpd(id);
            return PartialView("_SearchByName", model);


        }

        //sDischarge Here

        public ActionResult PatientForIpdDischarge(int id)
        {

            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.PatientForIpd(id);
            return PartialView("_PatientInfo", model);


        }

        public ActionResult PatientForInfo(int id, int ipdregid, int romid)
        {

            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IpdSearchResultList = pro.PatientForIpdAfterDischarge(id, ipdregid, romid);
            return PartialView("_PatientInfo", model);


        }


        public ActionResult Discharge(int id, int ipdid)
        {
            EHMSEntities ent = new EHMSEntities();
            IpdDischargeModel model = new IpdDischargeModel();

            model.refOfIpdRegistrationMasterModel = new IpdRegistrationMasterModel();

            model.refOfIpdRegistrationMasterModel = Utility.GetIpdRegistrationIdWithPatientId(id);
            model.refOfIpdRegistrationMasterModel.OpdNoEmergencyNo = id;
            //model.DignosisID = model.refOfIpdRegistrationMasterModel.DiagnosisID;

            var dig = ent.IpdPatientDiagnosisDetails.Where(x => x.IpdRegistrationID == ipdid).ToList();


            model.refofDiagnosisDetails = new IpdPatientDiagnosisDetailsModel();


            model.refofDiagnosisDetails.lstOfDiagnosisDetails = new List<IpdPatientDiagnosisDetailsModel>();
            foreach (var item in dig)
            {
                IpdPatientDiagnosisDetailsModel obj = new IpdPatientDiagnosisDetailsModel();

                //obj.DiagnosisID = 0;
                //obj.IpdRegistrationID = 0;
                // obj.DiagnosisID = item.DiagnosisID;

                obj.DiagnosisName = Utility.GetDiagosisList().Where(x => x.Value == @Convert.ToString(item.DiagnosisID)).FirstOrDefault().Text.ToString();

                model.refofDiagnosisDetails.lstOfDiagnosisDetails.Add(obj);

            }



            model.ipdResistrationID = model.refOfIpdRegistrationMasterModel.IpdRegistrationID;
            model.Opdid = model.refOfIpdRegistrationMasterModel.OpdNoEmergencyNo;
            model.IpdPatientRoomDetailId = ent.IpdPatientroomDetails.Where(x => x.IpdRegistrationID == model.ipdResistrationID && x.Status == true).SingleOrDefault().IpdPatientRoomDetailId;
            model.IpdPatientBedDetailId = ent.IpdPatientBedDetails.Where(x => x.IpdRegistrationId == model.ipdResistrationID && x.Status == true).SingleOrDefault().IpdPatientBedDetailId;


            return View(model);


        }

        //public ActionResult Discharge(int id)
        //{
        //    EHMSEntities ent = new EHMSEntities();
        //    IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
        //    IpdDischargeModel model = new IpdDischargeModel();
        //    var data = ent.IpdDischarge.Where(m => m.ipdResistrationID == id).FirstOrDefault();
        //    if (data != null)
        //    {
        //        model.FurtherTreatment = data.FurtherTreatment;
        //        model.BriefHistory = data.BriefHistory;
        //        model.ClinicalFindings = data.ClinicalFindings;
        //        model.FoloowUp = data.FoloowUp;
        //        // model.DignosisID = data.DignosisID;
        //        model.Investigation = data.Investigation;
        //    }

        //    pro.Discharge(id);
        //    model.ipdResistrationID = id;
        //    ViewBag.Details = pro.DischargReport(id);
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult Discharge(IpdDischargeModel model)
        {


            EHMSEntities ent = new EHMSEntities();

            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            int i = pro.InsertInDischarge(model);
            if (i != 0)
            {

                pro.PatientDischargeinIpd(model.ipdResistrationID);

                TempData["success"] = "Discharge Success";
                View(model);
                //return RedirectToAction("Index");

            }
            var data = ent.IpdDischarges.Where(m => m.ipdResistrationID == model.ipdResistrationID).Select(m => m.IpdDischargeID).FirstOrDefault();



            return RedirectToAction("EditDischarged", "PatientDischarged", new { id = data, opdid = model.Opdid, ipdregid = model.ipdResistrationID, bedid = model.IpdPatientRoomDetailId });

        }
        public ActionResult IPdDischargeReport()
        {
            return View();
        }

        public ActionResult GetAllDischargReport(string FirstName)
        {
            IpdSearchResults model = new IpdSearchResults();
            IpdDischcargeProvider pro = new IpdDischcargeProvider();
            model.IpdSearchResultList = pro.SearchByPatientNameToDischarge(FirstName);
            return PartialView("GetAllDischargReport", model);
        }

        public ActionResult BillForm(int id, int DepartmentID, int OpdEmerNo)
        {

            EHMSEntities ent = new EHMSEntities();

            var deptid = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == id).SingleOrDefault().DepartmentID;
            var regdate = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == id).SingleOrDefault().RegistrationDate;
            var CreatedBy = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == id).SingleOrDefault().CreatedtBy;
            var DepoAmount = ent.PatientDepositDetails.Where(x => x.PatientID == OpdEmerNo).FirstOrDefault().TotalAmount;

            if (deptid == 1001)
            {

                IpdRegistrationMasterModel obj = new IpdRegistrationMasterModel();
                obj.refOfEmergecyMasterModel = new EmergecyMasterModel();
                obj.IpdPatientBedDetailsModel = new IpdPatientBedDetailModel();
                obj.IpdPatientroomDetailsModel = new IpdPatientroomDetailsModel();
                var objEm = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == OpdEmerNo).SingleOrDefault();
                var objBed = ent.IpdPatientBedDetails.Where(x => x.IpdRegistrationId == id).FirstOrDefault();
                var objRoom = ent.IpdPatientroomDetails.Where(x => x.IpdRegistrationID == id && x.Status == true).FirstOrDefault();
                AutoMapper.Mapper.Map(objBed, obj.IpdPatientBedDetailsModel);
                AutoMapper.Mapper.Map(objEm, obj.refOfEmergecyMasterModel);
                AutoMapper.Mapper.Map(objRoom, obj.IpdPatientroomDetailsModel);
                obj.RegistrationDate = regdate;
                obj.IpdRegistrationID = id;
                obj.CreatedtBy = (int)CreatedBy;
                return View(obj);


            }
            else
            {

                IpdRegistrationMasterModel objforopd = new IpdRegistrationMasterModel();
                objforopd.refOpdModel = new OpdModel();
                objforopd.IpdPatientBedDetailsModel = new IpdPatientBedDetailModel();
                objforopd.IpdPatientroomDetailsModel = new IpdPatientroomDetailsModel();
                var objopd = ent.OpdMasters.Where(x => x.OpdID == OpdEmerNo).SingleOrDefault();
                var objBed = ent.IpdPatientBedDetails.Where(x => x.IpdRegistrationId == id).FirstOrDefault();
                var objRoom = ent.IpdPatientroomDetails.Where(x => x.IpdRegistrationID == id).FirstOrDefault();
                AutoMapper.Mapper.Map(objBed, objforopd.IpdPatientBedDetailsModel);
                AutoMapper.Mapper.Map(objopd, objforopd.refOpdModel);
                AutoMapper.Mapper.Map(objRoom, objforopd.IpdPatientroomDetailsModel);
                objforopd.RegistrationDate = regdate;
                objforopd.IpdRegistrationID = id;
                objforopd.CreatedtBy = (int)CreatedBy;
                objforopd.DepositeAmount = (decimal)DepoAmount;

                return View("billforIpdforOpd", objforopd);

            }

        }



        public ActionResult CreateNewPatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewPatient(OpdModel model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            if (ModelState.IsValid)
            {
                int i = pro.NewPatientInsert(model);
                return RedirectToAction("Create", new { id = i });
            }
            else
            {
                return View(model);
            }


        }
        public ActionResult PaidPatientResistrationIndex()
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            List<CentralizedBillingModel> query3 = pro.GetAllFromCentrBilling();
            ViewBag.Data = query3;
            return View();
        }

        public ActionResult InsertFroCentralizeBillingInIPDResistrationMaster(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            model.OpdNoEmergencyNo = id;
            //var data=ent.OpdMaster.Where(m=>m.OpdID==id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult InsertFroCentralizeBillingInIPDResistrationMaster(IpdRegistrationMasterModel model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            if (ModelState.IsValid)
            {
                pro.InsertCeltralBillingPatientINIPDResistrationMaster(model);
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("InsertFroCentralizeBillingInIPDResistrationMaster");
            }
        }
        public ActionResult DailyRortOfIPdResistrationMaster()
        {

            return View();
        }
        [HttpPost]
        public PartialViewResult GetDailyIPdReport(DateTime RegistrationDate)
        {

            IpdSearchResults model = new IpdSearchResults();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            EHMSEntities ent = new EHMSEntities();

            model = pro.GetDailyIpdReport(RegistrationDate);
            return PartialView(model);
        }
        //By subin
        public ActionResult SearchIpdPatientByName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchIpdPatientByName(string value)
        {

            EHMSEntities ent = new EHMSEntities();
            IpdPartialDetails IPDModel = new IpdPartialDetails();
            List<IpdPartialDetails> Opdmodel = new List<IpdPartialDetails>();
            List<IpdPartialDetails> Opdmodel1 = new List<IpdPartialDetails>();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            var OpdidData = ent.OpdMasters.Where(m => m.FirstName + " " + (m.MiddleName + " " ?? string.Empty) + m.LastName == value).Select(n => n.OpdID).ToList();

            foreach (var items in OpdidData)
            {
                var a = items.ToString();
                var cc = pro.GetPatientlistByName(a);
                foreach (var itemss in cc)
                {
                    Opdmodel1.Add(itemss);
                }

            }

            IPDModel.IpdPartialDetailsList = Opdmodel1;


            return View(IPDModel);

        }

        [HttpPost]
        public ActionResult IpdPatientDiagnosisDetails()
        {
            //IpdPatientDiagnosisDetailsModel model = new IpdPatientDiagnosisDetailsModel();
            ViewBag.id = i;
            i++;
            return PartialView("IpdPatientDiagnosisDetails");
        }

        public ActionResult AutocompleteDiagnosis(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> items = new List<string>();
            var diagnosis = ent.SetupDiagnosis.Where(x => x.DiagnosisName.Contains(s) && x.Status == true).ToList();
            //foreach (var item in units)
            //{
            //    items.Add(item.StockItemName);
            //}
            return Json(diagnosis, JsonRequestBehavior.AllowGet);
        }

        //by subin

        public ActionResult IpdVitalIndex()
        {

            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();

            return View();

        }
        //subin Vital
        public ActionResult IpdVital(int id)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            int i = Utility.patientlogid(id);
            VitalForOthersModel model = new VitalForOthersModel();
            model.lstOfVitalForOthersModel = pro.GetListOfVitalWithPatientId(id);
            model.PatinetLogId = i;
            model.OpdId = id;

            return View(model);

        }
        [HttpPost]
        public ActionResult IpdVital(VitalForOthersModel model)
        {



            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();

            int i = pro.InsertIpdVital(model);
            if (i != 0)
            {

                TempData["Success"] = UtilityMessage.save;
                return RedirectToAction("IpdVital", new { id = model.OpdId });

            }
            else
            {

                TempData["Success"] = UtilityMessage.editfailed;
                return RedirectToAction("IpdVital", new { id = model.OpdId });

            }




        }
        public ActionResult EditVital(int id)
        {
            VitalForOthersModel model = new VitalForOthersModel();

            using (EHMSEntities ent = new EHMSEntities())
            {

                //var dataOfVital = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientlogid).FirstOrDefault();

                model = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(ent.VitalForOthers.Where(x => x.VitalForOtherId == id).FirstOrDefault());


            }



            return PartialView("_EditVital", model);



        }
        [HttpPost]
        public ActionResult EditVital(VitalForOthersModel model)
        {

            string str = "";
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();

            int i = pro.EditIpdVital(model);
            if (i != 0)
            {
                TempData["Success"] = UtilityMessage.edit;
            }
            else
            {
                TempData["Success"] = UtilityMessage.editfailed;
            }
            return RedirectToAction("IpdVital", new { id = model.OpdId });



        }

        public ActionResult IpdWardDetails()
        {
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            return View(model);

        }

        [HttpPost]
        public ActionResult IpdWardDetails(IpdRegistrationMasterModel model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.IPDWardDetailsViewModelList = new List<IPDWardDetailsViewModel>();
            model.IPDWardDetailsViewModelList = pro.GetIpdWardDetails(model);
            return PartialView("_WardDetails", model);

        }
        public ActionResult ShiftFromIPD(int id, int ipdid)
        {
            int OpdId = id;
            int IpdRegistrationID = ipdid;
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            model.ObjShiftFromIPDViewModel = new ShiftFromIPDViewModel();
            model.OpdNoEmergencyNo = id;
            model.IpdRegistrationID = ipdid;

            if (pro.IsPatientAlreadyShiftedFromIPD(model.OpdNoEmergencyNo, model.IpdRegistrationID))
            {
                model = pro.GetIpdShiftDetailsOfPatient(id, ipdid);
                model.ObjShiftFromIPDViewModel.Status = true;

            }
            else
            {
                model.ObjShiftFromIPDViewModel.Status = false;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult ShiftFromIPD(IpdRegistrationMasterModel model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            pro.InsertIPDShiftDetails(model);
            return RedirectToAction("Index");
        }
        public ActionResult ActionToOT(int id, int ipdid)
        {
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();

            model.OpdNoEmergencyNo = id;
            model.IpdRegistrationID = ipdid;

            return View(model);
        }



    }
}
