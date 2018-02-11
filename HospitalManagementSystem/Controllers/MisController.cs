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
    public class MisController : Controller
    {
        //
        // GET: /Mis/

        public ActionResult Index()
        {
            //ViewBag.department = "1";
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }

        public ActionResult AgeReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        //public ActionResult AreaReportIndex()
        //{
        //    EmergecyMasterModel model = new EmergecyMasterModel();
        //    model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
        //    return View(model);
        //}
        public ActionResult GenderReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        public ActionResult Main()
        {
            return View();
        }


        public ActionResult Report(int id)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model = MisPorvider.GetEmergencyDetails(id);

            return View(model);
        }

        public ActionResult GetDistrictReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        public ActionResult GetZoneReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        public ActionResult GetVdcMunReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        public ActionResult GetOutReferReportIndex()
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string FromDate, string ToDate, string departmentlist, string PatientId, string PatientName)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.department = departmentlist;
            if (departmentlist == "3")
            {
                if (PatientId == null && PatientName == null)
                {
                    model.EmergencyMasterModelList = new List<EmergecyMasterModel>();
                    return View(model);
                }
                else
                {
                    ViewBag.Id = PatientId;
                    ViewBag.Name = PatientName;
                    ViewBag.print = 1;
                    return View(model);
                }
            }
            if (departmentlist != "4")
            {
                ViewBag.List = MisPorvider.GetOpdList(FromDate, ToDate, departmentlist);
            }

            return View(model);
        }
        public ActionResult DoctorReportIndex(string FromDate, string ToDate, string doctorlist)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            //ViewBag.department = departmentlist;
            ViewBag.doctor = doctorlist;
            {
                ViewBag.List = MisPorvider.GetDoctorListProvider(FromDate, ToDate, doctorlist);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AgeReportIndex(string agegrouplist)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();


            ViewBag.List = MisPorvider.GetAgerangeListProvider(agegrouplist);

            return View(model);
        }
        [HttpPost]
        public ActionResult GenderReportIndex(string gendertypelist, string FromDate, string ToDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.List = MisPorvider.GetGenderTypeProvider(FromDate, ToDate, gendertypelist);

            return View(model);
        }
        [HttpPost]
        public ActionResult getGenderReport(string genderlist, string FromDate, string ToDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.List = MisPorvider.GetGenderTypeProvider(FromDate, ToDate, genderlist);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetOutReferReportIndex(string referlist, string FromDate, string ToDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.List = MisPorvider.GetOutReferprovider(FromDate, ToDate, referlist);
            return View(model);
        }

        [HttpPost]
        public ActionResult GetVdcMunReportIndex(string vdcmunlist, string FromDate, String ToDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.List = MisPorvider.GetVdcMunProvider(FromDate, ToDate, vdcmunlist);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetDistrictReportIndex(string districtlist, string FromDate, string ToDate)
        {
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.List = MisPorvider.GetDistrictProvider(FromDate, ToDate, districtlist);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetZoneReportIndex(string zonelist, string FromDate, string ToDate)
        {
            //EmergecyMasterModel model = new EmergecyMasterModel();
            //ViewBag.List = MisPorvider.GetZoneProvider(zonelist);
            //return View(model);
            EmergecyMasterModel model = new EmergecyMasterModel();
            ViewBag.fromdate = FromDate;
            ViewBag.todate = ToDate;
            ViewBag.list = MisPorvider.GetZoneProvider(FromDate, ToDate, zonelist);
            return View(model);
        }

        // GET: /Mis/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Mis/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Mis/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mis/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Mis/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Mis/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Mis/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult SearchPatient(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();
            var result = (from r in ent.OpdMasters
                          where (r.FirstName.Trim() + " " + r.MiddleName.Trim() + " " + r.LastName.Trim()).Contains(s)
                          select new { r.FirstName, r.MiddleName, r.LastName }).Distinct();
            foreach (var item in result)
            {
                names.Add(item.FirstName.Trim() + " " + item.MiddleName.Trim() + " " + item.LastName.Trim());
            }
            return Json(names, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Statistics(string id, string df, string dt)
        {
            ViewBag.deps = HospitalManagementSystem.Utility.GetDepartmentsInOpd();
            ViewBag.dafr = df;
            ViewBag.dato = dt;
            return PartialView("Statistics");
        }

        public ActionResult OperationStatistics(string id, string df, string dt)
        {
            ViewBag.deps = HospitalManagementSystem.Utility.GetDepartmentsInOperation();
            ViewBag.dafr = df;
            ViewBag.dato = dt;
            return PartialView("OperationStatistics");
        }

        public ActionResult OtList(string FromDate, string ToDate)
        {
            if (string.IsNullOrEmpty(FromDate))
                FromDate = DateTime.Now.ToShortDateString();
            if (!string.IsNullOrEmpty(ToDate))
                ToDate = DateTime.Now.ToShortDateString();

            EHMSEntities ent = new EHMSEntities();
            OtListModel model = new OtListModel();
            model.otlist = new List<OtListModel>();

            model.otlist = (from m in ent.OperationTheatreMasters
                            join p in ent.OpdMasters on m.SourceID equals p.OpdID
                            where m.Status == true
                            select new OtListModel
                            {
                                PatientName = p.FirstName + " " + ((p.MiddleName == null) ? "" : p.MiddleName) + " " + p.LastName,
                                Age = (int)p.AgeYear,
                                Sex = p.Sex,
                                TypeOfSurgery = m.OperationType,
                                Ward = m.OperationRoomID,
                                OpMasterId = m.OperationTheatreMasterID,
                                OtDate = m.OperationDate

                            }).ToList();
            foreach (var item in model.otlist)
            {
                item.OtDetails = ent.OperationTheatreDetails.Where(x => x.OperationTheatreMasterID == item.OpMasterId).ToList();
            }
            //model.otlist=model.otlist.Where(x=>x.OtDate>FromDate)
            return View(model);
        }


        public ActionResult PathoReportDetails(string JvDate, string JvDateEnd)
        {
            MisModel model = new MisModel();
            MisPorvider pro = new MisPorvider();
            model.PathoTestDetailViewModelList = pro.GetMisPathoTestDetails(JvDate, JvDateEnd);
            return View(model);

        }
        public ActionResult PatientAllFlowRecord()
        {

            return View();
        }

        public ActionResult MRMainPage()
        {
            MisModel model = new MisModel();
            return View(model);
        }
        public ActionResult MRDischarge()
        {
            MisModel model = new MisModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult MRDischarge(string value)
        {
            MisModel model = new MisModel();
            MisPorvider pro = new MisPorvider();
            int IpNubmer = Convert.ToInt32(0);
            if (!string.IsNullOrEmpty(value))
            {
                IpNubmer = Convert.ToInt32(value);
                model = pro.GetIPDPatientDetailsFromIPNumber(IpNubmer);
            }


            return PartialView("_IPsearchValue", model);
        }

        public ActionResult ShowMRDischargeForm(string IpNumber, string OpdNumber)
        {
            MisModel model = new MisModel();
            if (!string.IsNullOrEmpty(IpNumber) && !string.IsNullOrEmpty(OpdNumber))
            {
                //model.ObjIPDetailViewModel.IPId = Convert.ToInt32(IpNumber);
                //model.ObjIPDetailViewModel.OpdId = Convert.ToInt32(OpdNumber);
                MisPorvider pro = new MisPorvider();
                int IpNubmer = Convert.ToInt32(IpNumber);
                model = pro.GetIPDPatientDetailsFromIPNumber(IpNubmer);

            }
            return View(model);
        }


        public ActionResult SaveMedicalRecordDisharge(MisModel model)
        {


            MisPorvider pro = new MisPorvider();
            if (model.ObjMRRecordViewModel.ICDName != null)
            {
                var IcdNumber = model.ObjMRRecordViewModel.ICDName.Substring(model.ObjMRRecordViewModel.ICDName.IndexOf('~') + 1);
                using (EHMSEntities ent = new EHMSEntities())
                {
                    model.ObjMRRecordViewModel.ICDCode = Convert.ToInt32(ent.SetupIcdCodes.Where(m => m.CodeNumber == IcdNumber).Select(m => m.ICDCODEID).FirstOrDefault());
                }
            }

            pro.InsertMRRecordDischarge(model);
            return RedirectToAction("MedicalRecordIndex");
        }
        public ActionResult MedicalRecordIndex()
        {
            MisModel model = new MisModel();
            MisPorvider pro = new MisPorvider();
            model = pro.GetMrDischargeReports();
            return View(model);
        }


        public ActionResult DeptPieChartView()
        {
            return View();
        }
        public ActionResult RenderPieChart()
        {
            return View();
        }

        public ActionResult PayablePatientReport()
        {
            MisModel model = new MisModel();
            return View(model);
        }

        //public ActionResult OpdDepartmentWiseReport(string FromDateString, string TodateString, string DeptId)
        public ActionResult OpdDepartmentWiseReport()
        {
            //MisModel model = new MisModel();
            //MisPorvider prop = new MisPorvider();
            //int DepartmentId = Convert.ToInt32(0);
            //if (string.IsNullOrEmpty(FromDateString))
            //    FromDateString = DateTime.Now.ToShortDateString();
            //if (string.IsNullOrEmpty(TodateString))
            //    TodateString = DateTime.Now.ToShortDateString();

            //if (!string.IsNullOrEmpty(DeptId))
            //    DepartmentId = Convert.ToInt32(DeptId);


            // model.OpdDepartmentWiseReportVMList = prop.GetOpdDepartmentWiseReport(FromDateString, TodateString, DepartmentId);
            //return View(model);
            return View();
        }

        public ActionResult DoctorCommission(string FromDateString, string TodateString)
        {
            MisModel model = new MisModel();
            MisPorvider prop = new MisPorvider();
            model.DoctorCommissionViewModelList = prop.GetDoctorCommissionListForMis(FromDateString, TodateString);
            return View(model);
        }

        public ActionResult PayDoctorCommission(string FromDateString, string TodateString, string DoctorName)
        {
            MisModel model = new MisModel();
            MisPorvider prop = new MisPorvider();
            if (string.IsNullOrEmpty(DoctorName))
            {
                model.DoctorCommissionViewModelList = prop.GetDoctorCommissionListByDocId(0, FromDateString, TodateString);
            }
            else
            {
                int ReferDocId = Convert.ToInt32(DoctorName);
                if (ReferDocId == 1009)
                {
                    model.DoctorCommissionViewModelList = prop.GetDoctorCommissionListByDocId(0, FromDateString, TodateString);
                }
                else
                {
                    model.DoctorCommissionViewModelList = prop.GetDoctorCommissionListByDocId(ReferDocId, FromDateString, TodateString);
                }
            }
            return View(model);

        }

        public ActionResult DoctorDtlById(int id)
        {
            MisModel model = new MisModel();
            MisPorvider pro = new MisPorvider();
            model = pro.GetEHSDoctorDetailsByDocId(id);
            return PartialView("_Details1", model);


        }

        public ActionResult GeneralAndPayableReport(string FromDateString, string TodateString, string Type)
        {
            MisModel model = new MisModel();
            MisPorvider prop = new MisPorvider();
            if (string.IsNullOrEmpty(FromDateString))
                FromDateString = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(TodateString))
                TodateString = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(Type))
                Type = "0";
            model.GeneralPayableReportModelList = prop.GetBillingReportGeneralAndPayable(FromDateString, TodateString, Type);
            return View(model);
        }

        public ActionResult BillAmountDifference(string FromDateString, string TodateString)
        {
            MisModel model = new MisModel();
            MisPorvider prop = new MisPorvider();
            if (string.IsNullOrEmpty(FromDateString))
                FromDateString = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(TodateString))
                TodateString = DateTime.Now.ToShortDateString();
            model.BillAmountDifferenceModelList = prop.GetBillingAmountDifference(FromDateString, TodateString);
            return View(model);
        }

        //public ActionResult CheckBill(int id)
        //{
        //    BillingCounterModel model = new BillingCounterModel();
        //    //BillingCounterProvider Bpro = new BillingCounterProvider();noticed
        //    EHMSEntities ent = new EHMSEntities();
        //    int BillNumberInt = id;
        //    int PatientId = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt).FirstOrDefault().PatientId;
        //    int PatientLogId = Bpro.getPatientLog(Convert.ToInt32(PatientId));

        //    if (PatientLogId != 0)
        //    {
        //        model = Bpro.GetBillDetailForPrintDuplicate(BillNumberInt.ToString());
        //        model.BillingCounterPatientInformationModel = Bpro.GetPatientBasicInformationFromOpd(Convert.ToInt32(PatientId), 0).FirstOrDefault();
        //        model.BalanceDeposit = Bpro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));
        //    }
        //    else
        //    {

        //    }

        //    model.billno = BillNumberInt.ToString();
        //    model.ObjPatientDischageBillDetailsViewModel.BillNumberInt = BillNumberInt;
        //    return View(model);
        //    //return View();
        //}noticed
    }
}
