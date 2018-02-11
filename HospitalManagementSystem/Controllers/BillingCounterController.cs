using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using System.Text.RegularExpressions;
using System.Data;
using System.Threading;

namespace HospitalManagementSystem.Controllersbil
{
    public class BillingCounterController : Controller
    {

        // GET: /BillingCounter/
        Providers.BillingCounterProvider pro = new Providers.BillingCounterProvider();

        //[CustomAuthorize(Roles = "BillingView, BillingAdmin, BillingCreate,superadmin, admin")]
        public ActionResult Index()
        {
            BillingCounterModel model = new BillingCounterModel();
            model.BillingCounterIndexViewModelList = pro.getBillCounterList();
            return View(model);
        }
        //[CustomAuthorize(Roles = "BillingAdmin, BillingCreate,superadmin, admin")]
        public ActionResult Create(string ipValue, string memebershipId)
        {
            Session.Clear();

            BillingCounterModel model = new BillingCounterModel();
            int n;
            bool isNumeric = int.TryParse(ipValue, out n);

            if (string.IsNullOrWhiteSpace(ipValue) == false && isNumeric == true)
            {

                int patLog = pro.getPatientLog(Convert.ToInt32(ipValue));

                if (patLog != 0)
                {
                    model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipValue), 0).FirstOrDefault();
                    model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));

                }
                else
                {

                    //ViewBag.message = "Patient Not Found..!";
                    model.BalanceDeposit = Convert.ToDecimal(0);
                    model.BillingCounterPatientInformationModel.CountryID = 153;
                    int Mn;
                    bool isNumericMember = int.TryParse(memebershipId, out Mn);
                    if (!string.IsNullOrWhiteSpace(memebershipId) && isNumericMember == true)
                    {
                        //check is member exist or not

                        int MembershipIdInt = Convert.ToInt32(isNumericMember);
                        bool CheckIfMemberExistOrnot = pro.CheckIsMemberExistOrNot(memebershipId);
                        if (CheckIfMemberExistOrnot)
                        {
                            model.BillingCounterPatientInformationModel = pro.GetMemberDetailsFromMembership(memebershipId, 0).FirstOrDefault();
                            model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));
                        }

                        else
                        {
                            ViewBag.message = "Patient Not Found..!";
                            model.BalanceDeposit = Convert.ToDecimal(0);
                            model.BillingCounterPatientInformationModel.CountryID = 153;
                        }


                    }
                    else
                    {
                        ViewBag.message = "Patient Not Found..!";
                        model.BalanceDeposit = Convert.ToDecimal(0);
                        model.BillingCounterPatientInformationModel.CountryID = 153;
                    }
                }

            }

            else
            {
                model.BillingCounterPatientInformationModel.CountryID = 153;
            }

            tempList.Clear();


            // model.BillingCounterNewTestListModelList = new List<BillingCounterNewTestListModel>();

            BillingCounterNewTestListModel obj = new BillingCounterNewTestListModel();
            model.BillingCounterNewTestListModelList.Add(obj);
            return View(model);
        }


        public ActionResult AddMoreParticulars()
        {
            BillingCounterNewTestListModel model = new BillingCounterNewTestListModel();

            return PartialView("AddMoreParticulars", model);
        }

        public ActionResult ParticularsDetails(string id, string id2)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                try
                {
                    if (id.Contains("^"))
                    {
                        string Valuenew = id.Trim();
                        int BracIndex = id.IndexOf('{') + 1;
                        int TotalLen = id.IndexOf('}') - 1;
                        int Len = id.Length;
                        int toval = Len - BracIndex;
                        string value = id.Substring(BracIndex, toval - 1);
                        int aa = value.IndexOf('^') + 1;
                        int bb = value.Length;
                        int lenth = bb - aa;
                        string str = value.Substring(aa, lenth);

                        var particular = pro.getTestDetailTestIDWiseNew(str, id2).FirstOrDefault();
                        string conParticular = particular.Rate + "~" + particular.TaxAmount + "~" + particular.TestId;
                        return Json(conParticular, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("0" + "/" + "0", JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json("0" + "/" + "0", JsonRequestBehavior.AllowGet);
                }
            }

        }


        [HttpPost]
        public ActionResult Create(BillingCounterModel model, int PaymentMode)
        {

            BillingCounterModel obj = new BillingCounterModel();

            var CurrSession = System.Web.HttpContext.Current.Session["OpdTypeIdInt"];
            if (CurrSession != null)
            {
                model.CurrentSession = CurrSession.ToString();
            }
            else
            {
                model.CurrentSession = "1";
            }

            decimal TotalModelAmount = Convert.ToDecimal(0);
            foreach (var item in model.BillingCounterNewTestListModelList)
            {
                TotalModelAmount += item.TotalAmount;
                if (item.TestName == "" || item.TestName == null)
                {

                    ViewBag.message = "Please Complete Form Properly !";
                    model.BillingCounterNewTestListModelList.RemoveRange(1, model.BillingCounterNewTestListModelList.Count - 1);
                    model.TotalAmount = TotalModelAmount;
                    //ModelState.Clear();
                    return View(model);

                }

            }

            if (ModelState.IsValid)
            {
                pro.Insert(model, PaymentMode);

                if (model.BillingCounterPatientInformationModel.EmergencyMasterId == 0)
                {
                    model.BillingCounterPatientInformationModel.EmergencyMasterId = Utility.getMaxPatientID();
                }


                int patLog = pro.getPatientLog(Convert.ToInt32(model.BillingCounterPatientInformationModel.EmergencyMasterId));

                if (patLog != 0)
                {
                    //model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipValue), 0).FirstOrDefault();
                    model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));

                }


                using (EHMSEntities ent = new EHMSEntities())
                {
                    model.billno = "Bill No:" + ent.CentralizedBillingMasters.Where(x => x.PatientId == model.BillingCounterPatientInformationModel.EmergencyMasterId).ToList().LastOrDefault().BillNo.ToString();

                }

                obj = model;
                obj.PaymentMode = PaymentMode;

                return View("PrintBillingCounter", obj);

            }

            else
            {
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateVal.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        var exception = error.Exception;
                        // You may log the errors if you want
                    }
                }
            }

            return View(model);
        }
        public ActionResult PrintBillingCounter(string BillNumber)
        {
            BillingCounterModel model = new BillingCounterModel();
            if (string.IsNullOrWhiteSpace(BillNumber) == false)
            {
                EHMSEntities ent = new EHMSEntities();
                int BillNumberInt = Convert.ToInt32(BillNumber);
                int PatientId = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt).FirstOrDefault().PatientId;
                int PatientLogId = pro.getPatientLog(Convert.ToInt32(PatientId));

                if (PatientLogId != 0)
                {
                    model = pro.GetBillDetailForPrintDuplicate(BillNumber);
                    model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(PatientId), 0).FirstOrDefault();
                    model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));
                }
                else
                {

                }
            }
            model.billno = BillNumber;
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchTestName(string s, string deptid)
        {
            BillingCounterModel model = new BillingCounterModel();
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();

            #region
            //if (Convert.ToInt32(deptid) == 1000)
            //{

            //}

            //var result = (from r in ent.SetupPathoTest
            //              join sc in ent.SetupSections on r.SectionId equals sc.SectionId
            //              where (r.TestName).Contains(s)
            //              select new { r.TestId, r.TestName, sc.SectionId, sc.Name }).Distinct();
            #endregion

            List<BillingCounterAutoCompleteModel> templist = new List<BillingCounterAutoCompleteModel>();
            templist = pro.getBillingCounterAutoCompleteModelList(s, Convert.ToInt32(deptid)).ToList();
            foreach (var item in templist)
            {
                names.Add(item.ItemName + "{" + item.CategoryName + "^" + item.ItemID + "}");
            }
            return Json(names, JsonRequestBehavior.AllowGet);


        }

        public ActionResult SeacrchOtherTest(string s, string deptid)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();
            var result = (from r in ent.SetupOtherTests
                          where (r.OtherTestName).Contains(s)
                          select new { r.SetupOtherTestId, r.OtherTestName }).Distinct();
            foreach (var item in result)
            {
                names.Add(item.OtherTestName + "(Other Test/" + item.SetupOtherTestId + ")");
            }
            return Json(names, JsonRequestBehavior.AllowGet);
        }

        //static List<List<BillingCounterTestListModel>> tempList = new List<List<BillingCounterTestListModel>>();


        static List<BillingCounterTestListModel> tempList = new List<BillingCounterTestListModel>();



        public ActionResult PopulateTestDetail(string ipValue, string ipTestID, string ipid, string ipDept, string fn, string ln, string mn, string add, string Age, string gen, string phno, string nation, string disin, string per, string tim, string dep, string docfee, string doctax, string refdoc, string userid)
        {
            //List<BillingCounterTestListModel> tempList = new List<BillingCounterTestListModel>();

            BillingCounterModel model = new BillingCounterModel();
            model.BillingCounterPatientInformationModel = new BillingCounterPatientInformationModel();

            if (string.IsNullOrWhiteSpace(ipid) == false && string.IsNullOrWhiteSpace(ipDept) == false)
            {
                int DropDownListDepartmentId = Convert.ToInt32(ipDept);

                if (DropDownListDepartmentId == 1001)//emergency
                {
                    model.DepartmentID = 1001;
                    if (ipid != null && ipid != "" && (pro.CheckEmergencyIdExistOrNot(Convert.ToInt32(ipid)) != false))
                    {
                        model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromEmergency(Convert.ToInt32(ipid)).FirstOrDefault();
                    }

                }
                else
                {
                    if (ipid != null && ipid != "" && (pro.CheckOpdIdExistOrNot(Convert.ToInt32(ipid)) != false))
                    {
                        model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();

                    }

                }
            }
            if (string.IsNullOrWhiteSpace(ipValue) == false)
            {
                // string str = Regex.Replace(ipValue, "[^0-9]+", string.Empty);
                string Valuenew = ipValue.Trim();
                int BracIndex = ipValue.IndexOf('{') + 1;
                int TotalLen = ipValue.IndexOf('}') - 1;
                int Len = ipValue.Length;
                int toval = Len - BracIndex;
                string value = ipValue.Substring(BracIndex, toval - 1);
                int aa = value.IndexOf('^') + 1;
                int bb = value.Length;
                int lenth = bb - aa;
                string str = value.Substring(aa, lenth);

                ////int startindex = ipValue.IndexOf('(') + 1;
                ////int Endindex = ipValue.IndexOf(')') - 1;
                ////int value = ipValue.Length;
                ////string txt = ipValue.Substring(startindex, ipValue.Length-1);

                foreach (var itemL in tempList)
                {
                    int countTestNameLength = itemL.TestName.Length;
                    int ipValueCount = ipValue.Length;

                    if (countTestNameLength <= ipValueCount)
                    {
                        if (ipValue.Substring(0, itemL.TestName.Length) == itemL.TestName)
                        {
                            TempData["failed"] = "Test Name already exists";
                            model.BillingCounterTestListModelList = tempList;

                            if (model.PatientID == 0)
                            {
                                int agg = 0;
                                int.TryParse(Age, out agg);

                                model.BillingCounterPatientInformationModel.FirstName = fn;
                                model.BillingCounterPatientInformationModel.MiddleName = mn;
                                model.BillingCounterPatientInformationModel.LastName = ln;
                                model.BillingCounterPatientInformationModel.Age = agg;
                                model.BillingCounterPatientInformationModel.Gender = gen;
                                model.BillingCounterPatientInformationModel.Address = add;
                                model.BillingCounterPatientInformationModel.PhoneNo = phno;
                                model.BillingCounterPatientInformationModel.CountryID = Convert.ToInt32(nation);
                                model.Deposit = Convert.ToDecimal(dep);
                                model.DoctorFee = Convert.ToDecimal(docfee);
                                model.DoctorFeeTax = Convert.ToDecimal(doctax);
                                model.ReferDoctorID = Convert.ToInt32(refdoc);


                            }
                            else
                            {
                                model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();
                                model.BillingCounterPatientInformationModel.EmergencyMasterId = (Convert.ToInt32(ipid));

                            }
                            return View("Create", model);
                        }
                    }

                }
                //changes made for discountper/amount
                model.BillingCounterTestListModelList = pro.getTestDetailTestIDWise(str, Convert.ToInt32(ipDept), disin, per, tim, userid);

                foreach (var item in model.BillingCounterTestListModelList)
                {
                    if (userid == item.UserId.ToString())
                    {

                        //do something here but what to do, no idea 
                        decimal total = item.Rate * Convert.ToInt32(tim);
                        if (disin == "1" && Convert.ToInt32(per) > 100)
                        {
                            TempData["failed"] = "Invalid Percentage";
                            model.BillingCounterTestListModelList = tempList;
                            return View("Create", model);
                        }
                        else if (disin != "1" && Convert.ToDecimal(per) > total)
                        {
                            TempData["failed"] = "Invalid Amount";
                            model.BillingCounterTestListModelList = tempList;
                            return View("Create", model);
                        }
                        else
                        {
                            tempList.Add(item);
                        }
                    }

                }

            }
            if (string.IsNullOrWhiteSpace(ipTestID) == false && string.IsNullOrWhiteSpace(ipValue) == true)
            {
                //for patientinformation
                //model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();
                var itemToRemove = tempList.Single(x => x.TestId == Convert.ToInt32(ipTestID));
                tempList.Remove(itemToRemove);
            }
            ViewBag.deptCode = ipDept;
            //ViewBag.DocID = model.ReferDoctorID;
            if (model.PatientID == 0)
            {
                int agg = 0;
                int.TryParse(Age, out agg);

                model.BillingCounterPatientInformationModel.FirstName = fn;
                model.BillingCounterPatientInformationModel.MiddleName = mn;
                model.BillingCounterPatientInformationModel.LastName = ln;
                model.BillingCounterPatientInformationModel.Age = agg;
                model.BillingCounterPatientInformationModel.Gender = gen;
                model.BillingCounterPatientInformationModel.Address = add;
                model.BillingCounterPatientInformationModel.PhoneNo = phno;
                model.BillingCounterPatientInformationModel.CountryID = Convert.ToInt32(nation);
                model.Deposit = Convert.ToDecimal(dep);
                model.DoctorFee = Convert.ToDecimal(docfee);
                model.DoctorFeeTax = Convert.ToDecimal(doctax);
                model.ReferDoctorID = Convert.ToInt32(refdoc);
            }
            else
            {
                model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();
                model.BillingCounterPatientInformationModel.EmergencyMasterId = (Convert.ToInt32(ipid));
            }
            model.BillingCounterTestListModelList = tempList;
            return View("Create", model);

        }



        //Added by bibek
        public ActionResult PaymentDetails(string DeptName, string BillNo)
        {
            BillingCounterModel model = pro.GetPaymentDetails(DeptName, BillNo);

            model.ObjBillingCounterPaymentDetails = pro.GetPatientGeneralInformation(DeptName, BillNo);
            model.ObjBillingCounterPaymentDetails.BillNumber = BillNo;
            model.ObjBillingCounterPaymentDetails.DepartmentName = DeptName;
            //Get Patient Information

            return View(model);
        }
        [HttpPost]
        public ActionResult PaymentDetails(BillingCounterModel model)
        {
            //update centralizebilling
            pro.UpdatePaymentUnpaidToPaid(model);
            return RedirectToAction("index");
        }
        public ActionResult ViewUnpaidBillDetail(string DeptName, string BillNo)
        {
            BillingCounterModel model = pro.GetPaymentDetails(DeptName, BillNo);
            model.ObjBillingCounterPaymentDetails = pro.GetPatientGeneralInformation(DeptName, BillNo);
            model.ObjBillingCounterPaymentDetails.BillNumber = BillNo;
            model.ObjBillingCounterPaymentDetails.DepartmentName = DeptName;
            //Get Patient Information

            return View(model);
        }



        //
        public ActionResult SearchPatient()
        {
            return PartialView();
        }


        public ActionResult GetPatientDetailsByName(string id)
        {
            BillingCounterModel model = new BillingCounterModel();
            model.BillingCounterPatientInformationModelList = pro.getPatientDetailByName(id);
            return PartialView("PatientListView", model);
        }

        public ActionResult getDoctorFeeTax(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                try
                {
                    var feetax = ent.SetupDoctorFees.Where(x => x.DoctorID == id).Select(x => new { x.DoctorFee, x.Tax }).FirstOrDefault();
                    string doctorfeetax = feetax.DoctorFee + "~" + feetax.Tax;
                    return Json(doctorfeetax, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("0" + "/" + "0", JsonRequestBehavior.AllowGet);
                }
            }

        }

        public ActionResult PreviousBill()
        {
            BillingCounterModel model = new BillingCounterModel();
            //model.BillingCounterIndexViewModelList = pro.GetPreviousBillList();
            return View(model);
        }


        public ActionResult CentralBillingIndex(string FromDateString, string TodateString, string NameOfPatient)
        {

            BillingCounterModel model = new BillingCounterModel();
            if (String.IsNullOrEmpty(FromDateString))
            {
                DateTime FromDate = DateTime.Today;
                FromDateString = FromDate.ToShortDateString();

            }

            if (String.IsNullOrEmpty(TodateString))
            {
                DateTime ToDate = DateTime.Today;
                TodateString = ToDate.ToShortDateString();
            }

            model.BillingCounterIndexViewModelList = pro.getCentralBillingIndex(FromDateString, TodateString, NameOfPatient);

            if (!String.IsNullOrEmpty(NameOfPatient))
            {
                NameOfPatient = NameOfPatient.Trim();
                int pos = NameOfPatient.IndexOf(" ");
                if (pos > 0)
                {
                    NameOfPatient = NameOfPatient.Substring(0, pos);
                }
                model.BillingCounterIndexViewModelList = model.BillingCounterIndexViewModelList.Where(x => x.PatientName.StartsWith(NameOfPatient)).ToList();
                // model.BillingCounterIndexViewModelList.Where(x => x.PatientName.StartsWith(NameOfPatient)).ToList();

            }

            //ViewBag.startDate = FromDateString;
            //ViewBag.EndDate = TodateString;

            return View(model);

        }

        public ActionResult CentrailBillingIndexDetails(string BillNumber)
        {
            BillingCounterModel model = new BillingCounterModel();
            model.BillingCounterPaymentDetailsList = pro.getCentralBillingDetailsBillNoWise(BillNumber);
            return View(model);
        }

        public ActionResult AdvancedSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdvancedSearch(string selectOptions, string value, int? bloodid)
        {
            BillingCounterModel model = new BillingCounterModel();
            switch (selectOptions)
            {
                case "1":
                    int OpdId = Convert.ToInt32(value);
                    model = pro.GetPatientsDetails(1, OpdId, "");
                    break;

                case "2":
                    model = pro.GetPatientsDetails(2, bloodid, value);
                    break;

                case "3":
                    model = pro.GetPatientsDetails(3, bloodid, "");
                    break;

                case "4":
                    int memberId = Convert.ToInt32(value);
                    model = pro.GetPatientsDetails(4, memberId, "");
                    break;

                case "5":
                    model = pro.GetPatientsDetails(5, bloodid, value);
                    break;

                default:

                    break;
            }


            return PartialView("_AdvancedSearch", model);

        }
        public ActionResult PatientDischargeRpt()
        {
            BillingCounterModel model = new BillingCounterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PatientDischargeRpt(string selectOptions, string value, string ReportOrRefund)
        {
            BillingCounterModel model = new BillingCounterModel();
            switch (selectOptions)
            {
                case "1":
                    //int OpdId = Convert.ToInt32(value);
                    model = pro.GetPatientsDetailsByIdAndName(1, value);
                    break;

                case "2":
                    model = pro.GetPatientsDetailsByIdAndName(2, value);
                    break;

                default:

                    break;
            }

            if (ReportOrRefund == "Report")
            {
                return PartialView("_DischargeSearch", model);
            }
            else
            {
                return PartialView("_RefundSearch", model);
            }



        }

        public ActionResult PatientDscrgh(int id)
        {
            BillingCounterModel model = new BillingCounterModel();
            model = pro.GetAllDischargeBillSummary(id);

            int AccountHeadID = Convert.ToInt32(0);
            EHMSEntities ent = new EHMSEntities();
            var AccHeadId = ent.OpdMasters.Where(x => x.OpdID == id).FirstOrDefault().AccountHeadId;
            if (AccHeadId != null)
            {
                AccountHeadID = (int)AccHeadId;
                model.ObjDepositRefundViewModel.RemainingAmount = pro.getBalanceDeposit(AccountHeadID);
                //model.ObjDepositRefundViewModel.AccountSubHeadId = AccountHeadID;
            }
            else
            {
                model.ObjDepositRefundViewModel.RemainingAmount = Convert.ToDecimal(0);
            }

            return View(model);
        }

        public ActionResult DepositRefund()
        {
            BillingCounterModel model = new BillingCounterModel();
            return View(model);
        }


        public ActionResult RefundIndex(int id)
        {
            BillingCounterModel model = new BillingCounterModel();
            model = pro.GetDepositRefundList();
            return View(model);
        }


        public ActionResult ShowRefundReceipt(int id)
        {
            DepositRefundViewModel model = new DepositRefundViewModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var data = ent.DepositRefundDetails.Where(x => x.DepositRefundId == id).FirstOrDefault();
                model = AutoMapper.Mapper.Map<DepositRefundDetail, DepositRefundViewModel>(data);

            }
            //return View(obj);


            return View(model);
        }

        public ActionResult RefundForm(int id)
        {
            BillingCounterModel model = new BillingCounterModel();
            model = pro.GetAllDischargeBillSummary(id);
            int AccountHeadID = Convert.ToInt32(0);
            EHMSEntities ent = new EHMSEntities();
            var AccHeadId = ent.OpdMasters.Where(x => x.OpdID == id).FirstOrDefault().AccountHeadId;
            if (AccHeadId != null)
            {
                AccountHeadID = (int)AccHeadId;
                model.ObjDepositRefundViewModel.RemainingAmount = pro.getBalanceDeposit(AccountHeadID);
                model.ObjDepositRefundViewModel.AccountSubHeadId = AccountHeadID;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RefundForm(BillingCounterModel model)
        {
            pro.InsertDepositRefund(model);
            return RedirectToAction("RefundIndex", new { @id = 1 });
        }

        //[CustomAuthorize(Roles = "BillingAdmin, BillingCreate,superadmin, admin")]
        public ActionResult DeleteCBBill(string Billnumer)
        {
            BillingCounterModel model = new BillingCounterModel();
            if (string.IsNullOrWhiteSpace(Billnumer) == false)
            {
                EHMSEntities ent = new EHMSEntities();
                int BillNumberInt = Convert.ToInt32(Billnumer);
                int PatientId = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt).FirstOrDefault().PatientId;
                int PatientLogId = pro.getPatientLog(Convert.ToInt32(PatientId));

                if (PatientLogId != 0)
                {
                    model = pro.GetBillDetailForPrintDuplicate(Billnumer);
                    model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(PatientId), 0).FirstOrDefault();
                    model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));
                }
                else
                {

                }
            }
            model.billno = Billnumer;
            return View(model);

        }
        [HttpPost]
        public ActionResult DeleteCBBillDetials(BillingCounterModel model)
        {
            string BillNumber = model.billno;
            pro.DeleteCBBillNumberByBillId(BillNumber, model);
            return View(model);

        }

        public ActionResult PrintBillCB(string BillNumber)
        {
            BillingCounterModel model = new BillingCounterModel();
            if (string.IsNullOrWhiteSpace(BillNumber) == false)
            {
                EHMSEntities ent = new EHMSEntities();
                int BillNumberInt = Convert.ToInt32(BillNumber);
                int PatientId = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt).FirstOrDefault().PatientId;
                int PatientLogId = pro.getPatientLog(Convert.ToInt32(PatientId));

                if (PatientLogId != 0)
                {
                    model = pro.GetBillDetailForPrintDuplicate(BillNumber);
                    model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(PatientId), 0).FirstOrDefault();
                    model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));
                }
                else
                {

                }
            }
            model.billno = BillNumber;
            return View(model);
        }

        public ActionResult IPDPatientDischarge()
        {
            BillingCounterModel model = new BillingCounterModel();
            return View();
        }

    }
}
