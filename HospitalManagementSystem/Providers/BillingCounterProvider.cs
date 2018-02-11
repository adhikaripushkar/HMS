using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagementSystem.Models;
using System.Data.Entity.Validation;
using System.Text;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class BillingCounterProvider
    {

        public List<BillingCounterNewTestListModel> getTestDetailTestIDWiseNew(string id, string dept)
        {
            var CurrSession = System.Web.HttpContext.Current.Session["OpdTypeIdInt"];
            string CurrentSession = "1";
            if (CurrSession != null)
            {
                CurrentSession = CurrSession.ToString();
            }
            else
            {
                CurrentSession = "1";
            }


            using (EHMSEntities ent = new EHMSEntities())
            {
                if (CurrentSession == "1")
                {
                    return ent.Database.SqlQuery<BillingCounterNewTestListModel>("GetTestDetailTestIDWiseNew " + Convert.ToInt32(id) + ", " + Convert.ToInt32(dept) + "").ToList();
                }
                else
                {
                    return ent.Database.SqlQuery<BillingCounterNewTestListModel>("GetTestDetailTestIDWiseNewPayable " + Convert.ToInt32(id) + ", " + Convert.ToInt32(dept) + "").ToList();
                }
                //return ent.Database.SqlQuery<BillingCounterTestListModel>("GetTestDetailTestIDWise 9, 1003").ToList();
            }
        }



        public List<BillingCounterIndexViewModel> getBillCounterList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingCounterIndexViewModel>("getBillCounterList").ToList();
            }
        }


        public List<BillingCounterPatientInformationModel> GetPatientBasicInformationFromEmergency(int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //var singleValue = ent.EmergencyMaster.Where(x => x.EmergencyMasterId == PatientID).SingleOrDefault();
                List<BillingCounterPatientInformationModel> myList = new List<BillingCounterPatientInformationModel>();
                //BillingCounterPatientInformationModel objPatientInf = new BillingCounterPatientInformationModel();
                //objPatientInf.EmergencyMasterId = singleValue.EmergencyMasterId;
                //objPatientInf.FirstName = singleValue.FirstName;
                //objPatientInf.MiddleName = singleValue.MiddleName;
                //objPatientInf.LastName = singleValue.LastName;
                //objPatientInf.Age = singleValue.Age.GetValueOrDefault();
                //objPatientInf.Gender = singleValue.Gender;
                //objPatientInf.DepartmentID = 10;
                //myList.Add(objPatientInf);
                return myList;
            }
        }
        public bool CheckOpdIdExistOrNot(int opdId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.OpdMasters.Where(x => x.OpdID == opdId).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int checkIsMemberAlreadyInOpd(int MembershipId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.OpdMasters.Where(x => x.MemberTypeID == MembershipId).Count();
            if (isExist > 0)
            {
                return ent.OpdMasters.Where(x => x.MemberTypeID == MembershipId).FirstOrDefault().OpdID;
            }
            else
            {
                return 0;
            }

        }

        public bool CheckIsMemberExistOrNot(string MemeberId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.SetupMemberShips.Where(x => x.MemberID == MemeberId).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }




        public bool CheckEmergencyIdExistOrNot(int EmergencyId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == EmergencyId).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<BillingCounterPatientInformationModel> GetPatientBasicInformationFromOpd(int PatientID, int DepartmentID)
        {
            bool CheckOpdIdExit = CheckOpdIdExistOrNot(PatientID);
            using (EHMSEntities ent = new EHMSEntities())
            {

                List<BillingCounterPatientInformationModel> myList = new List<BillingCounterPatientInformationModel>();

                if (CheckOpdIdExit)
                {
                    var singleValue = ent.OpdMasters.Where(x => x.OpdID == PatientID).FirstOrDefault();
                    BillingCounterPatientInformationModel objPatientInf = new BillingCounterPatientInformationModel();
                    int AgeTypeValue = 1;
                    if (singleValue.AgeMonth != null)
                    {
                        AgeTypeValue = (int)singleValue.AgeMonth;
                    }
                    objPatientInf.EmergencyMasterId = singleValue.OpdID;
                    objPatientInf.FirstName = singleValue.FirstName;
                    objPatientInf.MiddleName = singleValue.MiddleName;
                    objPatientInf.LastName = singleValue.LastName;
                    objPatientInf.Age = singleValue.AgeYear.GetValueOrDefault();
                    objPatientInf.Gender = singleValue.Sex;
                    objPatientInf.DepartmentID = singleValue.DepartmentId.GetValueOrDefault();
                    objPatientInf.Address = singleValue.Address;
                    objPatientInf.CountryID = (int)singleValue.CountryID;
                    objPatientInf.AccountHeadId = singleValue.AccountHeadId;
                    objPatientInf.AgeType = AgeTypeValue;

                    myList.Add(objPatientInf);
                }

                return myList;
            }
        }

        public List<BillingCounterPatientInformationModel> GetMemberDetailsFromMembership(string membershipId, int DepartmentID)
        {

            bool CheckIfMemberExistOrnot = CheckIsMemberExistOrNot(membershipId);
            int CheckIfMemberInOpdOrNot = checkIsMemberAlreadyInOpd(Convert.ToInt32(membershipId));

            //bool CheckIfMemberExistOrnot = true;
            using (EHMSEntities ent = new EHMSEntities())
            {

                List<BillingCounterPatientInformationModel> myList = new List<BillingCounterPatientInformationModel>();

                if (CheckIfMemberExistOrnot)
                {
                    //var singleValue = ent.OpdMaster.Where(x => x.OpdID == PatientID).FirstOrDefault();
                    var singleValue = ent.SetupMemberShips.Where(x => x.MemberID == membershipId).FirstOrDefault();
                    BillingCounterPatientInformationModel objPatientInf = new BillingCounterPatientInformationModel();
                    objPatientInf.EmergencyMasterId = CheckIfMemberInOpdOrNot;
                    objPatientInf.FirstName = singleValue.FirstName;
                    objPatientInf.MiddleName = singleValue.MiddleName;
                    objPatientInf.LastName = singleValue.LastName;
                    objPatientInf.Age = 0;
                    objPatientInf.Gender = "sex";
                    objPatientInf.DepartmentID = 1111;
                    objPatientInf.Address = singleValue.Address;
                    objPatientInf.CountryID = (int)singleValue.CountryID;
                    objPatientInf.AccountHeadId = null;
                    objPatientInf.MembershipId = singleValue.MemberID;

                    myList.Add(objPatientInf);
                }

                return myList;
            }
        }



        public List<BillingCounterTestListModel> getTestDetailTestIDWise(string TestID, int DeptID, string disIN, string PerAmount, string tim, string UserId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingCounterTestListModel>("GetTestDetailTestIDWise " + Convert.ToInt32(TestID) + ", " + DeptID + "," + Convert.ToInt32(disIN) + "," + Convert.ToDecimal(PerAmount) + "," + Convert.ToInt32(tim) + " ," + Convert.ToInt32(UserId) + "").ToList();
                //return ent.Database.SqlQuery<BillingCounterTestListModel>("GetTestDetailTestIDWise 9, 1003").ToList();
            }
        }


        public List<BillingCounterAutoCompleteModel> getBillingCounterAutoCompleteModelList(string ItemName, int DeptID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingCounterAutoCompleteModel>("BillingCounterAutoComplete '" + DeptID + "','%" + ItemName + "%'").Take(15).ToList();

            }
        }


        public void Insert(BillingCounterModel model, int PaymentMode)
        {
            int discountamount = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                int LedgerMasterId = 0;
                int PatientLogId = 0;
                int HospitalPatientId = 0;
                int patientid = model.BillingCounterPatientInformationModel.EmergencyMasterId;
                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();
                int TestTimes = Convert.ToInt32(1);

                if (patientid > 0)//old patient
                {
                    PatientLogId = ent.PatientLogMasters.Where(x => x.PatientId == patientid).Max(x => x.OpdMasterLogId);
                    LedgerMasterId = Utility.GetLedgerID(patientid);
                    HospitalPatientId = patientid;

                }
                else//new patient
                {

                    string RegistrationSource = "opd";

                    string patho = "";
                    string opd = "";

                    foreach (var item in model.BillingCounterNewTestListModelList)
                    {

                        if (item.DepartmentID == 1000)
                        {
                            opd = "opd";
                        }
                        if (item.DepartmentID == 1006)
                        {

                            patho = "patho";

                        }

                    }

                    if (patho == "patho" && opd == "opd")
                    {

                        RegistrationSource = "opd";


                    }

                    if (patho == "patho" && opd == "")
                    {

                        RegistrationSource = "patho";


                    }

                    var objOpdMaster = new OpdMaster()//insert to opdmaster
                    {
                        FirstName = model.BillingCounterPatientInformationModel.FirstName,
                        MiddleName = model.BillingCounterPatientInformationModel.MiddleName,
                        LastName = model.BillingCounterPatientInformationModel.LastName,
                        AgeYear = model.BillingCounterPatientInformationModel.Age,
                        Sex = model.BillingCounterPatientInformationModel.Gender,
                        MobileNumber = model.BillingCounterPatientInformationModel.PhoneNo,
                        RegistrationDate = DateTime.Now,
                        RegistrationMode = "Visit",
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DepartmentId = Utility.GetCurrentUserDepartmentId(),
                        Address = model.BillingCounterPatientInformationModel.Address,
                        RegistrationSource = RegistrationSource,
                        Status = true,
                        ContactName = model.BillingCounterPatientInformationModel.PhoneNo,
                        CountryID = model.BillingCounterPatientInformationModel.CountryID,
                        ZoneID = model.BillingCounterPatientInformationModel.ZoneId,
                        DistrictId = model.BillingCounterPatientInformationModel.DistrictID,
                        PaidOnPaid = true,
                        MemberTypeID = Convert.ToInt32(model.BillingCounterPatientInformationModel.MembershipId),
                        AgeMonth = model.BillingCounterPatientInformationModel.AgeType

                    };

                    ent.OpdMasters.Add(objOpdMaster);
                    ent.SaveChanges();
                    HospitalPatientId = objOpdMaster.OpdID;


                    //PatientLogMaster
                    var objPatientLogMaster = new PatientLogMaster()
                    {
                        PatientId = objOpdMaster.OpdID,
                        RegistrationDate = (DateTime)objOpdMaster.RegistrationDate,
                        DepartmentId = (int)objOpdMaster.DepartmentId,
                        Status = true,
                        OpdTypeID = 1
                    };
                    ent.PatientLogMasters.Add(objPatientLogMaster);
                    //ent.SaveChanges();
                    //Create Ledger for Patient
                    string LedgerName = "A/C " + model.BillingCounterPatientInformationModel.FirstName + " " + (model.BillingCounterPatientInformationModel.MiddleName + " " ?? string.Empty) + model.BillingCounterPatientInformationModel.LastName;
                    var objLedgerMaster = new GL_LedgerMaster()
                    {
                        DepartmentID = objOpdMaster.DepartmentId,
                        AccountGroupID = 1,
                        AccountSubGroupID = 1,
                        AccountTypeID = 1,
                        SourceID = objOpdMaster.OpdID,
                        LedgerName = LedgerName,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        Status = 1,
                        LedgerSourceType = "Patient"
                        //LedgerType
                    };

                    ent.GL_LedgerMaster.Add(objLedgerMaster);
                    ent.SaveChanges();
                    PatientLogId = objPatientLogMaster.OpdMasterLogId;
                    LedgerMasterId = objLedgerMaster.LedgerMasterID;
                }


                //Edited by bibek aug 31

                //centralized billing for deposit Amount
                if (model.Deposit > 0)
                {
                    var AdddepositMaster = new PatientDepositMaster()
                    {
                        PatientId = HospitalPatientId,
                        DepartmentId = 1001,
                        PatientDepartmentId = 35,
                        DepostedBy = "",
                        DepositedAmount = model.Deposit,
                        DepositedDate = DateTime.Now,
                        SwipeCardId = "NP001",
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        DepositeType = 1,
                        Status = true,
                        //ReceiptID=Utility.GetMaxDepositeNumber()+1
                        SwipeCardDetail = "Not implement yet"

                    };
                    ent.PatientDepositMasters.Add(AdddepositMaster);

                    var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                    {
                        BillNo = BillNumberInt,
                        BillDate = DateTime.Now,
                        TotalBillAmount = model.Deposit,
                        Narration1 = "Deposit",
                        DepartmentName = "CB",
                        SubDepartmentId = 1,
                        PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(HospitalPatientId),
                        PatientId = HospitalPatientId,
                        CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                        CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        Remarks = "Deposit",
                        Status = true,
                        BranchId = 1,
                        JVStatus = false,
                        ReceiptId = Utility.GetMaxDepositeNumber(),
                        IsHandover = false
                    };

                    ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);

                    int UserAccountHeadIdInt = 0;
                    var userAccountHeadId = ent.OpdMasters.Where(x => x.OpdID == HospitalPatientId).FirstOrDefault();
                    if (userAccountHeadId.AccountHeadId != null)
                    {
                        UserAccountHeadIdInt = (int)userAccountHeadId.AccountHeadId;
                    }

                    var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                    {
                        BillNo = BillNumberInt,
                        AccountHeadID = 1258,//deposit
                        AccountSubHeadID = UserAccountHeadIdInt,//patientId
                        Amount = model.Deposit,
                        Status = true
                    };
                    ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                    var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                    {
                        BillNo = BillNumberInt,
                        PaymentTypeID = 372,
                        Amount = model.Deposit,
                        Status = true,
                        PaymentSubTypeID = 0
                    };
                    ent.CentralizedBillingPaymentTypes.Add(ObjCentralizedBillingPaymentType);
                    SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                          where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                          select x).First();
                    billNumber.BillNumber = billNumber.BillNumber + 1;

                    ent.SaveChanges();
                }

                int BillNumberIntWithoutDep = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                //Centralized Billing
                foreach (var item in model.BillingCounterNewTestListModelList)
                {
                    int VarAmountTypeId = 0;
                    int VarAmountTypeTaxId = 0;
                    decimal VarAmount = Convert.ToDecimal(0);
                    decimal VarDoctorFee = Convert.ToDecimal(0);
                    decimal VarDoctorFeeTax = Convert.ToDecimal(0);


                    if (item.DepartmentID == 1000)//For opd case
                    {
                        //int VarAmountTypeId = 0;
                        //int VarAmountTypeTaxId = 0;
                        //decimal VarAmount = Convert.ToDecimal(0);
                        //decimal VarDoctorFee = Convert.ToDecimal(0);
                        //decimal VarDoctorFeeTax = Convert.ToDecimal(0);
                        string Valuenew = item.TestName.Trim();
                        int BracIndex = item.TestName.IndexOf('{') + 1;
                        int TotalLen = item.TestName.IndexOf('}') - 1;
                        int Len = item.TestName.Length;
                        int toval = Len - BracIndex;
                        string value = item.TestName.Substring(BracIndex, toval - 1);
                        int aa = value.IndexOf('^') + 1;
                        int bb = value.Length;
                        int lenth = bb - aa;
                        string str = value.Substring(aa, lenth);

                        //Old registration new registraion
                        if (str == "1")//New registraion
                        {
                            VarAmountTypeId = 362;//362 47.62
                            VarAmountTypeTaxId = 1261;//366 2.38


                            //update registration source
                            var RegistrationSource = ent.OpdMasters.Where(x => x.OpdID == HospitalPatientId).FirstOrDefault();
                            RegistrationSource.RegistrationSource = "Opd";

                            var OpdfeeDetailVar = new OpdFeeDetail()
                            {
                                OpdID = HospitalPatientId,
                                FeeDate = DateTime.Now,
                                RegistrationFee = Convert.ToDecimal(47.62),
                                FeeTaxAmount = Convert.ToDecimal(2.38),
                                DoctorFee = model.DoctorFee,
                                DoctorFeeTax = model.DoctorFeeTax,
                                MemberDiscountAmount = Convert.ToDecimal(0),
                                OtherDiscountAmount = Convert.ToDecimal(0),
                                OtherDiscountPercentage = Convert.ToDecimal(0),
                                Tender = Convert.ToDecimal(0),
                                ReturnAmount = Convert.ToDecimal(0),
                                TotalAmount = Convert.ToDecimal(50.00),
                                Remarks = "From centralized billing"

                            };
                            ent.OpdFeeDetails.Add(OpdfeeDetailVar);
                            ent.SaveChanges();

                        }
                        else
                        {
                            //old registration
                            //*patient log*


                            var objPatientLogMaster = new PatientLogMaster()
                            {
                                PatientId = model.BillingCounterPatientInformationModel.EmergencyMasterId,
                                RegistrationDate = DateTime.Now,
                                DepartmentId = Utility.GetCurrentUserDepartmentId(),
                                Status = true,
                                OpdTypeID = 1
                            };
                            ent.PatientLogMasters.Add(objPatientLogMaster);
                            ent.SaveChanges();

                            VarAmountTypeId = 364;// check table is glaccount sub group
                            VarAmountTypeTaxId = 1261;//366

                            var OpdfeeDetailVar = new OpdFeeDetail()
                            {
                                OpdID = HospitalPatientId,
                                FeeDate = DateTime.Now,
                                RegistrationFee = Convert.ToDecimal(37.10),
                                FeeTaxAmount = Convert.ToDecimal(1.90),
                                DoctorFee = model.DoctorFee,
                                DoctorFeeTax = model.DoctorFeeTax,
                                MemberDiscountAmount = Convert.ToDecimal(0),
                                OtherDiscountAmount = Convert.ToDecimal(0),
                                OtherDiscountPercentage = Convert.ToDecimal(0),
                                Tender = Convert.ToDecimal(0),
                                ReturnAmount = Convert.ToDecimal(0),
                                TotalAmount = Convert.ToDecimal(40.00),
                                Remarks = "From centralized billing"

                            };
                            ent.OpdFeeDetails.Add(OpdfeeDetailVar);
                            ent.SaveChanges();

                        }

                        ent.SaveChanges();
                    }
                    else
                    {

                        int amounttype = 0;
                        int amounttaxtype = 0;
                        int discounttype = 22;
                        int AccountHeadId = 0;
                        int AccountSubHeadID = 0;
                        string deptname = string.Empty;
                        string RemarksForCentralizedBilling = string.Empty;
                        int ItemTestID = item.TestId;
                        int TestTime = 1;
                        //string Remarkstest = string.Empty;
                        if (item.DepartmentID == 1006)//path
                        {
                            amounttype = 15;
                            amounttaxtype = 16;
                            deptname = "Patho";
                            ItemTestID = item.TestId;
                            TestTime = item.TestTime;
                            TestTimes = item.TestTime;

                        }
                        else if (item.DepartmentID == 1004)//others
                        {
                            amounttype = 26;
                            amounttaxtype = 27;
                            deptname = "Others";
                            ItemTestID = item.TestId;
                            TestTimes = item.TestTime;


                        }
                        else if (item.DepartmentID == 1003)//ipd
                        {
                            amounttype = 7;
                            amounttaxtype = 8;
                            deptname = "Ipd";
                            RemarksForCentralizedBilling = "CB";
                            TestTimes = item.TestTime;
                        }
                        else if (item.DepartmentID == 1005)//surgery/ot
                        {
                            amounttype = 11;
                            amounttaxtype = 12;
                            deptname = "Ot";
                            RemarksForCentralizedBilling = "CB";
                            TestTimes = item.TestTime;
                        }
                        else if (item.DepartmentID == 1001)//emergency
                        {
                            amounttype = 19;
                            amounttaxtype = 20;
                            deptname = "Emergency";
                            RemarksForCentralizedBilling = "CB";
                            TestTimes = item.TestTime;
                        }
                        else if (item.DepartmentID == 1007)//for dental
                        {
                            amounttype = 28;
                            amounttaxtype = 29;
                            deptname = "Dental";
                            TestTimes = item.TestTime;
                        }

                        else if (item.DepartmentID == 1008)//for eye
                        {
                            amounttype = 34;
                            amounttaxtype = 35;
                            deptname = "Eye";
                            TestTimes = item.TestTime;
                        }
                        else if (item.DepartmentID == 1009)//for physiotherapy
                        {
                            amounttype = 30;
                            amounttaxtype = 31;
                            deptname = "Physiotherapy";
                            TestTimes = item.TestTime;
                        }

                        else if (item.DepartmentID == 1010)//for Ent
                        {
                            amounttype = 32;
                            amounttaxtype = 33;
                            deptname = "Ent";
                            TestTimes = item.TestTime;
                        }
                    }

                    ///For table CentralizedBillingDetail
                    // for rate
                    var objtocCentralizedBillingDetail = new CentralizedBillingDetail();

                    objtocCentralizedBillingDetail.BillNo = BillNumberIntWithoutDep;
                    //objtocCentralizedBillingDetail.AccountHeadID = VarAmountTypeId;

                    if (item.DepartmentID == 1006)
                    {
                        objtocCentralizedBillingDetail.AccountHeadID = 3;//InHouse Lab Test
                        objtocCentralizedBillingDetail.AccountSubHeadID = item.TestId;
                        objtocCentralizedBillingDetail.Times = TestTimes;
                    }
                    else
                    {
                        objtocCentralizedBillingDetail.AccountHeadID = item.TestId;
                        objtocCentralizedBillingDetail.AccountSubHeadID = null;
                        objtocCentralizedBillingDetail.Times = TestTimes;
                    }


                    objtocCentralizedBillingDetail.Amount = item.Rate;

                    if (item.Discount > 0)
                    {
                        objtocCentralizedBillingDetail.DiscountID = 369;
                        objtocCentralizedBillingDetail.DiscountAmount = item.Discount;

                        discountamount += Convert.ToInt32(item.Discount);

                    }


                    objtocCentralizedBillingDetail.Status = true;
                    objtocCentralizedBillingDetail.DepartmentId = item.DepartmentID;
                    objtocCentralizedBillingDetail.Narration2 = Convert.ToString(item.TotalAmount);

                    ent.CentralizedBillingDetails.Add(objtocCentralizedBillingDetail);


                    // for tax

                    if (item.TaxAmount > 0)
                    {

                        var objtocCentralizedBillingDetailForTax = new CentralizedBillingDetail();

                        objtocCentralizedBillingDetailForTax.BillNo = BillNumberIntWithoutDep;
                        objtocCentralizedBillingDetailForTax.AccountHeadID = 1261;
                        objtocCentralizedBillingDetailForTax.AccountSubHeadID = null;

                        objtocCentralizedBillingDetailForTax.Amount = item.TaxAmount;

                        objtocCentralizedBillingDetailForTax.DiscountID = null;
                        objtocCentralizedBillingDetailForTax.DiscountAmount = null;

                        objtocCentralizedBillingDetailForTax.Status = true;
                        objtocCentralizedBillingDetailForTax.DepartmentId = item.DepartmentID;
                        objtocCentralizedBillingDetailForTax.Times = TestTimes;

                        ent.CentralizedBillingDetails.Add(objtocCentralizedBillingDetailForTax);


                        //ent.SaveChanges();
                    }

                }

                /// for CentralizedBillingMaster table 
                string Narration2 = string.Empty;
                decimal ReturnedAmount = Convert.ToDecimal(0);
                decimal TenderAmount = Convert.ToDecimal(0);

                if (PaymentMode == 315)
                {
                    Narration2 = "ByDeposit";
                    //ReturnedAmount = ReturnedAmount;

                }
                else
                {
                    ReturnedAmount = model.ReturnedAmount;
                    TenderAmount = (decimal)model.TenderAmount;
                }
                int HospitalType = 1;
                if (model.CurrentSession == "2")
                {
                    HospitalType = 2;
                }

                var objCentralizedBillingMaster = new CentralizedBillingMaster();

                objCentralizedBillingMaster.BillNo = BillNumberIntWithoutDep;
                objCentralizedBillingMaster.BillDate = DateTime.Now;
                objCentralizedBillingMaster.TotalBillAmount = model.TotalAmount + discountamount;
                objCentralizedBillingMaster.TotalDiscountID = 0;
                objCentralizedBillingMaster.TotalDiscountAmount = 0;
                objCentralizedBillingMaster.Narration1 = "Narraion";
                objCentralizedBillingMaster.Narration2 = Narration2;
                objCentralizedBillingMaster.DepartmentName = "Cb";
                objCentralizedBillingMaster.SubDepartmentId =
                objCentralizedBillingMaster.PatientId = HospitalPatientId;
                objCentralizedBillingMaster.PatientLogId = PatientLogId;
                objCentralizedBillingMaster.BillDate = DateTime.Now;

                // objCentralizedBillingMaster.JVNumber=1;
                objCentralizedBillingMaster.JVStatus = false;
                objCentralizedBillingMaster.CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
                objCentralizedBillingMaster.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                objCentralizedBillingMaster.CreatedDate = DateTime.Now;
                objCentralizedBillingMaster.Remarks = "cb";
                objCentralizedBillingMaster.Status = true;
                objCentralizedBillingMaster.BranchId = 1;
                objCentralizedBillingMaster.ReceiptId = 0;
                objCentralizedBillingMaster.IsHandover = false;
                objCentralizedBillingMaster.ReturnedAmount = ReturnedAmount;
                objCentralizedBillingMaster.TenderAmount = TenderAmount;
                objCentralizedBillingMaster.PayableType = HospitalType;

                ent.CentralizedBillingMasters.Add(objCentralizedBillingMaster);

                //For CentralizedBillingPaymentType table
                var objCentralizedBillingPaymentType = new CentralizedBillingPaymentType();

                if (PaymentMode == 315)
                {

                    objCentralizedBillingPaymentType.BillNo = BillNumberIntWithoutDep;
                    objCentralizedBillingPaymentType.PaymentTypeID = 1258;
                    objCentralizedBillingPaymentType.PaymentSubTypeID = (int)model.BillingCounterPatientInformationModel.AccountHeadId;

                    objCentralizedBillingPaymentType.Amount = model.TotalAmount; // if there is flat discount then it will be totalamout-discountamout
                    objCentralizedBillingPaymentType.Status = true;
                    ent.CentralizedBillingPaymentTypes.Add(objCentralizedBillingPaymentType);
                }
                else
                {

                    objCentralizedBillingPaymentType.BillNo = BillNumberIntWithoutDep;
                    objCentralizedBillingPaymentType.PaymentTypeID = @Convert.ToInt32(PaymentMode);
                    objCentralizedBillingPaymentType.PaymentSubTypeID = 0;
                    objCentralizedBillingPaymentType.Amount = model.TotalAmount; // if there is flat discount then it will be totalamout-discountamout
                    objCentralizedBillingPaymentType.Status = true;
                    ent.CentralizedBillingPaymentTypes.Add(objCentralizedBillingPaymentType);
                }

                if (model.CurrentSession == "2")
                {
                    if (model.ReferDoctorID != 0)
                    {
                        foreach (var item in model.BillingCounterNewTestListModelList)
                        {
                            decimal SurgeonCharge = Convert.ToDecimal(0);
                            decimal AnesthiaCharge = Convert.ToDecimal(0);
                            decimal DoctorCharge = Convert.ToDecimal(0);
                            decimal PathologistCharge = Convert.ToDecimal(0);

                            int CommissionType = 0;
                            int CommissionSubType = 0;
                            string CommissionName = string.Empty;
                            string CommissionSubName = string.Empty;

                            if (item.DepartmentID == 1006)//Pathology
                            {
                                DoctorCharge = HospitalManagementSystem.Utility.GetDoctorCommissionFromPatho(item.TestId, 1);
                                PathologistCharge = HospitalManagementSystem.Utility.GetDoctorCommissionFromPatho(item.TestId, 2);
                                CommissionType = 1;
                                CommissionSubType = 2;
                                CommissionName = "Doctor Commission";
                                CommissionSubName = "Pathology Commission";
                            }

                            if (item.DepartmentID == 1005)//Surgery
                            {
                                DoctorCharge = HospitalManagementSystem.Utility.GeSurgeonCommissionFromSurgery(item.TestId, 1);
                                PathologistCharge = HospitalManagementSystem.Utility.GeSurgeonCommissionFromSurgery(item.TestId, 2);
                                CommissionType = 3;
                                CommissionSubType = 4;
                                CommissionName = "Surgeon Commission";
                                CommissionSubName = "Anesthesisa Commission";

                            }

                            if (item.DepartmentID == 1004 || item.DepartmentID == 1010 || item.DepartmentID == 1009 || item.DepartmentID == 1008 || item.DepartmentID == 1007)//Other Test
                            {
                                DoctorCharge = HospitalManagementSystem.Utility.GetDoctorCommissionFromOtherTest(item.TestId, 1);
                                PathologistCharge = HospitalManagementSystem.Utility.GetDoctorCommissionFromOtherTest(item.TestId, 2);
                                CommissionType = 1;
                                CommissionSubType = 2;
                                CommissionName = "Doctor Commission";
                                CommissionSubName = "Pathology Commission";
                            }

                            var ObjCommDoct = new CommissionDetail()
                            {
                                CommissionType = CommissionType,
                                CommissionName = CommissionName,
                                CommissionAmount = DoctorCharge,
                                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                                DepartmentId = item.DepartmentID,
                                DocId = model.ReferDoctorID,
                                Status = false,
                                CreatedDate = DateTime.Now,
                                TestId = item.TestId,
                                BillNumber = BillNumberInt,
                                JVStatus = false


                            };
                            ent.CommissionDetails.Add(ObjCommDoct);

                            var ObjCommDoctobj = new CommissionDetail()
                            {
                                CommissionType = CommissionSubType,
                                CommissionName = CommissionSubName,
                                CommissionAmount = PathologistCharge,
                                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                                DepartmentId = item.DepartmentID,
                                DocId = model.ReferDoctorID,
                                Status = false,
                                CreatedDate = DateTime.Now,
                                TestId = item.TestId,
                                BillNumber = BillNumberInt,
                                JVStatus = false


                            };
                            ent.CommissionDetails.Add(ObjCommDoctobj);



                        }
                    }
                }

                //update Bill Number
                SetupHospitalBillNumber billNumberDeopo = (from x in ent.SetupHospitalBillNumbers
                                                           where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                           select x).First();
                billNumberDeopo.BillNumber = billNumberDeopo.BillNumber + 1;


                try
                {
                    ent.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }

            }

        }


        public BillingCounterPaymentDetails GetPatientGeneralInformation(string DepartmentName, string BillNumber)
        {
            BillingCounterModel model = new BillingCounterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.CentralizedBillings.Where(x => x.DepartmentName == DepartmentName && x.BillNumber == BillNumber).FirstOrDefault();
                if (result != null)
                {
                    int PatientId = result.PatientId;
                    var query = ent.OpdMasters.Where(x => x.OpdID == PatientId).FirstOrDefault();
                    model.ObjBillingCounterPaymentDetails.PatientFullName = query.FirstName + ' ' + query.MiddleName + ' ' + query.LastName;
                    model.ObjBillingCounterPaymentDetails.Address = query.Address;
                    model.ObjBillingCounterPaymentDetails.Age = (int)query.AgeYear;
                    //model.ObjBillingCounterPaymentDetails.Age = query.AgeYear;
                    model.ObjBillingCounterPaymentDetails.Gendar = query.Sex;
                    model.ObjBillingCounterPaymentDetails.PatientId = PatientId;
                    return model.ObjBillingCounterPaymentDetails;

                }
                return model.ObjBillingCounterPaymentDetails;

            }
        }

        public BillingCounterModel GetDepositRefundList()
        {
            BillingCounterModel model = new BillingCounterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var query = ent.DepositRefundDetails.ToList();
                foreach (var item in query)
                {
                    var DepoModel = new DepositRefundViewModel()
                    {
                        DepositRefundId = item.DepositRefundId,
                        PatientId = (int)item.PatientId,
                        RefundAmount = (decimal)item.RefundAmount,
                        RefundBy = (int)item.RefundBy,
                        RefundDate = (DateTime)item.RefundDate
                    };
                    model.DepositRefundViewModelList.Add(DepoModel);

                }
            }

            return model;
        }

        //Edite by bibek
        public BillingCounterModel GetAllDischargeBillSummary(int PatientId)
        {
            BillingCounterModel model = new BillingCounterModel();
            int AccountSubHeadId = Convert.ToInt32(0);
            using (EHMSEntities ent = new EHMSEntities())
            {
                int PatienLogId = HospitalManagementSystem.Utility.getPatientLogID(PatientId);
                var DepositDetail = ent.CentralizedBillingMasters.Where(x => x.PatientLogId == PatienLogId && x.Narration1 == "Deposit");

                var DepositedBy = ent.PatientDepositMasters.Where(x => x.PatientId == PatientId).ToList();
                string DepoByStr = string.Empty;
                if (DepositedBy != null && DepositedBy.Count() > 0)
                {
                    DepoByStr = DepositedBy.FirstOrDefault().DepostedBy;
                }


                foreach (var item in DepositDetail)
                {
                    var ViewModelDeposit = new PatientDischageBillDetailsViewModel()
                    {
                        DepositAmount = (decimal)item.TotalBillAmount,
                        BillDate = item.BillDate,
                        PatientFullName = "",
                        FirstName = DepoByStr,
                        CreatedUser = item.CreatedBy

                    };
                    model.PatientDischageBillDetailsListDeposit.Add(ViewModelDeposit);
                }

                var PatienDetails = ent.OpdMasters.Where(x => x.OpdID == PatientId).FirstOrDefault();
                model.ObjPatientDischageBillDetailsViewModel.FirstName = PatienDetails.FirstName;
                model.ObjPatientDischageBillDetailsViewModel.Age = (int)PatienDetails.AgeYear;
                model.ObjPatientDischageBillDetailsViewModel.Address = PatienDetails.Address;
                model.ObjPatientDischageBillDetailsViewModel.PatientId = PatienDetails.OpdID;


                //Fee only  By Cash
                var TranByCash = (from CBM in ent.CentralizedBillingMasters
                                  join CBD in ent.CentralizedBillingDetails
                                  on CBM.BillNo equals CBD.BillNo
                                  where CBM.PatientLogId == PatienLogId && CBM.Narration1 != "Deposit" && CBM.Narration2 != "ByDeposit" && CBM.Narration1 != "DepositRefund"
                                  select new
                                  {
                                      CBM.PatientLogId,
                                      CBD.AccountHeadID,
                                      CBD.AccountSubHeadID,
                                      CBD.Amount,
                                      CBM.BillDate,
                                      CBM.CreatedBy
                                  }).ToList();


                foreach (var item in TranByCash)
                {
                    if (item.AccountSubHeadID != null)
                    {
                        AccountSubHeadId = (int)item.AccountSubHeadID;
                    }
                    else
                    {
                        AccountSubHeadId = item.AccountHeadID;
                    }
                    var ViewmodelByCash = new PatientDischageBillDetailsViewModel()
                    {
                        //Particulars = Hospital.Utility.GetFeeTypeNameFromId(item.AccountHeadID),
                        Particulars = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(AccountSubHeadId),
                        BillDate = item.BillDate,
                        BillAmount = item.Amount,
                        AccountMainHeadId = AccountSubHeadId,
                        CreatedBy = item.CreatedBy
                    };

                    model.PatientDischageBillDetailsListByCash.Add(ViewmodelByCash);
                }



                ////Tax only  By Cash
                //decimal TaxOnlyBycash = Convert.ToDecimal(0);
                //var TranByCashTaxonly = (from CBM in ent.CentralizedBillingMaster
                //                         join CBD in ent.CentralizedBillingDetail
                //                         on CBM.BillNo equals CBD.BillNo
                //                         where CBM.PatientLogId == PatienLogId && CBD.AccountHeadID == 366 && CBM.Narration1 != "Deposit" && CBM.Narration2 != "ByDeposit"
                //                         select new
                //                         {
                //                             CBM.PatientLogId,
                //                             CBD.AccountHeadID,
                //                             CBD.AccountSubHeadID,
                //                             CBD.Amount,
                //                             CBM.BillDate
                //                         }).ToList();
                //foreach (var item in TranByCash)
                //{
                //    var ViewmodelByCash = new PatientDischageBillDetailsViewModel()
                //    {
                //        TotalTaxAmount = item.Amount

                //    };


                //    //model.PatientDischageBillDetailsListByCash.Add(ViewmodelByCash);
                //}





                //By deposit Transaction


                var TranByDepo = (from CBM in ent.CentralizedBillingMasters
                                  join CBD in ent.CentralizedBillingDetails
                                  on CBM.BillNo equals CBD.BillNo
                                  where CBM.PatientLogId == PatienLogId && CBM.Narration1 != "Deposit" && CBM.Narration2 == "ByDeposit" && CBM.Narration1 != "DepositRefund"
                                  select new
                                  {
                                      CBM.PatientLogId,
                                      CBD.AccountHeadID,
                                      CBD.AccountSubHeadID,
                                      CBD.Amount,
                                      CBM.BillDate,
                                      CBM.CreatedBy
                                  }).ToList();
                foreach (var item in TranByDepo)
                {
                    if (item.AccountSubHeadID != null)
                    {
                        AccountSubHeadId = (int)item.AccountSubHeadID;
                    }
                    else
                    {
                        AccountSubHeadId = item.AccountHeadID;
                    }
                    var ViewmodelByDepo = new PatientDischageBillDetailsByDepoViewModel()
                    {
                        //DepoParticulars = Hospital.Utility.GetFeeTypeNameFromId(item.AccountHeadID),
                        DepoParticulars = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(AccountSubHeadId),
                        DepoBillDate = item.BillDate,
                        DepoBillAmount = item.Amount,
                        AccountMainHeadId = AccountSubHeadId,
                        CreatedUser = item.CreatedBy


                    };
                    model.PatientDischageBillDetailsByDepoViewModelList.Add(ViewmodelByDepo);
                }


            }
            return model;
        }


        public BillingCounterModel GetPatientsDetailsByIdAndName(int TypeOfsearch, string SearchV)
        {
            BillingCounterModel model = new BillingCounterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                switch (TypeOfsearch)
                {
                    case 1:
                        int OpdId = Convert.ToInt32(SearchV);
                        var result = ent.OpdMasters.Where(x => x.OpdID == OpdId);
                        foreach (var item in result)
                        {
                            var Viewmodel = new PatientDischageBillDetailsViewModel()
                            {
                                PatientFullName = Utility.GetPatientNameWithIdFromOpd(item.OpdID),
                                PatientId = item.OpdID,
                                Age = (int)item.AgeYear,
                                Address = item.Address

                            };

                            model.PatientDischageBillDetailsViewModelList.Add(Viewmodel);

                        }


                        break;
                    case 2:

                        int pos = SearchV.IndexOf(" ");
                        if (pos > 0)
                        {
                            SearchV = SearchV.Substring(0, pos);
                        }

                        var resultName = ent.OpdMasters.Where(x => x.FirstName.Contains(SearchV));
                        foreach (var item in resultName)
                        {
                            var Viewmodel = new PatientDischageBillDetailsViewModel()
                            {
                                PatientFullName = Utility.GetPatientNameWithIdFromOpd(item.OpdID),
                                PatientId = item.OpdID,
                                Age = (int)item.AgeYear,
                                Address = item.Address

                            };

                            model.PatientDischageBillDetailsViewModelList.Add(Viewmodel);

                        }
                        break;
                    default:
                        break;
                }
                return model;
            }
        }

        public BillingCounterModel GetPatientsDetails(int TypeOfSearch, int? BloodGroupid, string PatientName)
        {
            BillingCounterModel model = new BillingCounterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                switch (TypeOfSearch)
                {
                    case 1:
                        var result = ent.OpdMasters.Where(x => x.OpdID == BloodGroupid);
                        foreach (var item in result)
                        {
                            int MemeberShipId = 0;
                            if (item.MemberTypeID != null)
                                MemeberShipId = (int)item.MemberTypeID;
                            var Viewmodel = new AdvancedSearchViewModel()
                            {
                                patientName = Utility.GetPatientNameWithIdFromOpd(item.OpdID),
                                PatientId = item.OpdID,
                                MemebershipId = MemeberShipId,
                                MemberName = "",
                                Age = (int)item.AgeYear,
                                Address = item.Address,
                                Sex = item.Sex,
                                ContactNumber = item.ContactName

                            };

                            model.AdvancedSearchViewModelList.Add(Viewmodel);

                        }


                        break;

                    case 2:

                        string FirstName = PatientName;
                        int position = Convert.ToInt32(0);
                        if (PatientName.Contains(" "))
                        {
                            position = PatientName.IndexOf(' ');
                            FirstName = PatientName.Substring(0, position);
                        }

                        var result2 = ent.OpdMasters.Where(x => x.FirstName.StartsWith(FirstName));
                        foreach (var item in result2)
                        {
                            int MemebeshipId = 0;
                            if (item.MemberTypeID != null)
                                MemebeshipId = (int)item.MemberTypeID;
                            var Viewmodel = new AdvancedSearchViewModel()
                            {
                                patientName = item.FirstName,
                                PatientId = item.OpdID,
                                MemebershipId = MemebeshipId,
                                MemberName = item.FirstName,
                                Age = (int)item.AgeYear,
                                Address = item.Address,
                                Sex = item.Sex,
                                ContactNumber = item.ContactName,
                                FirstName = item.FirstName,
                                MiddleName = item.MiddleName,
                                LastName = item.LastName

                            };

                            model.AdvancedSearchViewModelList.Add(Viewmodel);

                        }


                        break;

                    case 3:
                        string bloodGroupString = Convert.ToString(BloodGroupid);
                        var result3 = ent.OpdMasters.Where(x => x.BloodGroup == bloodGroupString);
                        foreach (var item in result3)
                        {
                            var Viewmodel = new AdvancedSearchViewModel()
                            {
                                patientName = item.FirstName,
                                PatientId = item.OpdID,
                                MemebershipId = (int)item.MemberTypeID,
                                MemberName = item.FirstName,
                                Age = (int)item.AgeYear,
                                Address = item.Address,
                                Sex = item.Sex,
                                ContactNumber = item.ContactName
                            };

                            model.AdvancedSearchViewModelList.Add(Viewmodel);

                        }


                        break;

                    case 4:
                        string MemeberIdStr = Convert.ToString(BloodGroupid);
                        int count = ent.SetupMemberShips.Where(x => x.MemberID.Contains(MemeberIdStr)).Count();
                        if (count > 0)
                        {
                            var result4 = ent.SetupMemberShips.Where(x => x.MemberID == MemeberIdStr);
                            foreach (var item in result4)
                            {
                                int StringMembershipId = Convert.ToInt32(item.MemberID);
                                var Viewmodel = new AdvancedSearchViewModel()
                                {
                                    patientName = item.FirstName,
                                    PatientId = 0,
                                    MemebershipId = StringMembershipId,
                                    MemberName = item.FirstName,
                                    Age = Convert.ToInt32(0),
                                    Address = item.Address,
                                    Sex = item.Gender,
                                    ContactNumber = item.ContactNumber
                                };

                                model.AdvancedSearchViewModelList.Add(Viewmodel);

                            }
                        }
                        else
                        {
                            string BloodGrpStr = Convert.ToString(BloodGroupid);
                            var result44 = ent.SetupMemberShips.Where(x => x.MemberID == BloodGrpStr);
                            foreach (var item in result44)
                            {
                                int MemberIdInt = Convert.ToInt32(item.MemberID);
                                var Viewmodel = new AdvancedSearchViewModel()
                                {
                                    patientName = item.FirstName,
                                    PatientId = MemberIdInt,
                                    MemebershipId = MemberIdInt,
                                    MemberName = item.FirstName,
                                    Age = Convert.ToInt32(0),
                                    Address = item.Address,
                                    Sex = item.Gender,
                                    ContactNumber = item.ContactNumber
                                };

                                model.AdvancedSearchViewModelList.Add(Viewmodel);

                            }
                        }

                        break;

                    case 5:
                        var result5 = ent.SetupMemberShips.Where(x => x.FirstName.Contains(PatientName));
                        foreach (var item in result5)
                        {
                            int MemmberIdInt = Convert.ToInt32(item.MemberID);
                            var Viewmodel = new AdvancedSearchViewModel()
                            {
                                patientName = item.FirstName,
                                PatientId = Convert.ToInt32(0),
                                MemebershipId = MemmberIdInt,
                                MemberName = item.FirstName,
                                Age = Convert.ToInt32(0),
                                Address = item.Address,
                                Sex = item.Gender,
                                ContactNumber = item.ContactNumber

                            };

                            model.AdvancedSearchViewModelList.Add(Viewmodel);

                        }

                        break;

                    default:
                        break;
                }
                return model;

            }
        }


        public BillingCounterModel GetPaymentDetails(string DepartmentName, string BillNumber)
        {
            decimal TotalAmount = 0;
            decimal TotalDiscount = 0;
            BillingCounterModel model = new BillingCounterModel();

            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.CentralizedBillings.Where(x => x.DepartmentName == DepartmentName && x.BillNumber == BillNumber);
                foreach (var item in result)
                {
                    var Viewmodel = new BillingCounterPaymentDetails()
                    {
                        Particulars = Utility.GetFeeTypeNameFromId(item.AccountHeadId),
                        TotalAmount = item.Amount,
                        PatientId = item.PatientId
                    };

                    model.BillingCounterPaymentDetailsList.Add(Viewmodel);
                    if (item.AccountHeadId != 22)
                    {
                        TotalAmount += item.Amount;
                    }
                    else
                    {
                        TotalDiscount += item.Amount;
                    }

                }
                model.TotalAmount = TotalAmount;
                model.Discount = TotalDiscount;
                return model;
            }
        }

        public bool UpdatePaymentUnpaidToPaid(BillingCounterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var query = ent.CentralizedBillings.Where(x => x.BillNumber == model.ObjBillingCounterPaymentDetails.BillNumber && x.DepartmentName == model.ObjBillingCounterPaymentDetails.DepartmentName && x.PaidOnPaid == false);
                foreach (var item in query)
                {
                    item.PaidOnPaid = true;

                }

                var ChangePaidInOpdModel = ent.OpdMasters.Where(x => x.OpdID == model.ObjBillingCounterPaymentDetails.PatientId).FirstOrDefault();
                ChangePaidInOpdModel.PaidOnPaid = true;

                ent.SaveChanges();

            }
            return true;
        }

        public List<BillingCounterPatientInformationModel> getPatientDetailByName(string PatientName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingCounterPatientInformationModel>("GetPatientDetailsByName '" + PatientName + "%'").ToList();
            }
        }

        public List<BillingCounterIndexViewModel> getCentralBillingIndex(string FromDate, string Todate, string PatientName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.Database.SqlQuery<BillingCounterIndexViewModel>("CentralBillingIndex '" + FromDate + "', '" + Todate + "'").ToList();
                if (!String.IsNullOrEmpty(FromDate) && !String.IsNullOrEmpty(Todate))
                {
                    return ent.Database.SqlQuery<BillingCounterIndexViewModel>("CentralBillingIndex '" + FromDate + "', '" + Todate + "'").ToList();
                }
                else
                {
                    return ent.Database.SqlQuery<BillingCounterIndexViewModel>("CentralBillingIndex '" + FromDate + "', '" + Todate + "'").ToList();
                }

            }

        }
        public List<BillingCounterPaymentDetails> getCentralBillingDetailsBillNoWise(string BillNumber)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int BillNumberInt = Convert.ToInt32(BillNumber);
                return ent.Database.SqlQuery<BillingCounterPaymentDetails>("GetBillDetailByBillNo '" + BillNumberInt + "'").ToList();
            }
        }

        public decimal getBalanceDeposit(int patientlog)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<decimal>("getBalanceDeposit '" + patientlog + "'").FirstOrDefault<decimal>();
            }
        }


        public int getPatientLog(int patientid)
        {
            if (patientid == 0)
                return 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var data = ent.PatientLogMasters.Where(x => x.PatientId == patientid).Max(x => x.OpdMasterLogId);
                    return data;
                }
                catch
                {

                    return 0;
                }

            }
        }

        public BillingCounterModel GetBillDetailForPrintDuplicate(string BillnumberStr)
        {
            BillingCounterModel model = new BillingCounterModel();
            int BillNumberInt = Convert.ToInt32(BillnumberStr);
            decimal GrandTotalAmount = Convert.ToDecimal(0);
            decimal AmountTimes = Convert.ToDecimal(1);
            int AccSubHeadId = Convert.ToInt32(0);
            decimal DiscountPer = Convert.ToDecimal(0);


            using (EHMSEntities ent = new EHMSEntities())
            {
                model.Discount = Convert.ToDecimal(0);
                var resultOne = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt).FirstOrDefault();

                model.ReturnedAmount = (decimal)resultOne.ReturnedAmount;
                model.TenderAmount = resultOne.TenderAmount;
                model.Discount = (decimal)resultOne.TotalDiscountAmount;
                var Result2 = ent.CentralizedBillingPaymentTypes.Where(x => x.BillNo == BillNumberInt).FirstOrDefault();
                model.ObjBillingCounterPaymentDetails.GrandTotal = Result2.Amount;

                var result = ent.CentralizedBillingDetails.Where(x => x.BillNo == BillNumberInt);
                foreach (var item in result)
                {
                    if (item.AccountSubHeadID != null)
                    {
                        AccSubHeadId = (int)item.AccountSubHeadID;
                    }
                    if (item.DiscountAmount != null)
                    {
                        DiscountPer = (decimal)item.DiscountAmount;
                    }
                    var ViewModel = new BillingCounterNewTestListModel()
                    {
                        Amount = item.Amount,
                        DepartmentID = (int)item.DepartmentId,
                        TestTime = (int)item.Times,
                        Rate = item.Amount,
                        TaxAmount = item.Amount,
                        TestId = item.AccountHeadID,
                        SubTestId = AccSubHeadId,
                        TestTimes = (int)item.Times,
                        DiscountPer = DiscountPer,
                        CBDID = item.CBDID


                    };

                    AccSubHeadId = Convert.ToInt32(0);
                    GrandTotalAmount += (decimal)(item.Amount * item.Times);
                    model.BillingCounterNewTestListModelList.Add(ViewModel);

                    GrandTotalAmount = GrandTotalAmount - model.Discount;
                    model.TotalAmount = GrandTotalAmount;
                    model.ObjPatientDischageBillDetailsViewModel.BillNumberInt = BillNumberInt;

                }

            }

            return model;
        }
        public bool InsertDepositRefund(BillingCounterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var ObjDepoRefund = new DepositRefundDetail()
                {
                    RemainingAmount = model.ObjDepositRefundViewModel.RemainingAmount,
                    RefundAmount = model.ObjDepositRefundViewModel.RefundAmount,
                    status = true,
                    RefundBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    RefundDate = DateTime.Now,
                    Remarks = model.ObjDepositRefundViewModel.Remarks,
                    FiscalYearId = HospitalManagementSystem.Utility.GetCurrentFiscalYearID(),
                    BranchId = 1,
                    PatientId = model.ObjPatientDischageBillDetailsViewModel.PatientId,
                    PatientLogId = model.ObjPatientDischageBillDetailsViewModel.PatientId

                };

                ent.DepositRefundDetails.Add(ObjDepoRefund);
                ent.SaveChanges();

                int BillNumberIntWithoutDep = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                var ObjCBMaster = new CentralizedBillingMaster()
                {
                    BillNo = BillNumberIntWithoutDep,
                    BillDate = DateTime.Now,
                    TotalBillAmount = model.ObjDepositRefundViewModel.RefundAmount,
                    TotalDiscountID = 0,
                    TotalDiscountAmount = 0,
                    Narration1 = "DepositRefund",
                    Narration2 = "DepositRefund",
                    DepartmentName = "Cb",
                    PatientId = model.ObjPatientDischageBillDetailsViewModel.PatientId,
                    PatientLogId = model.ObjPatientDischageBillDetailsViewModel.PatientId,
                    JVStatus = false,
                    CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "CB",
                    Status = true,
                    BranchId = 1,
                    ReceiptId = 0,
                    IsHandover = false

                };
                ent.CentralizedBillingMasters.Add(ObjCBMaster);

                var ObjCBDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberIntWithoutDep,
                    AccountHeadID = 372,//New Registration coa
                    Amount = model.ObjDepositRefundViewModel.RefundAmount,
                    Status = true,
                    DepartmentId = 1000
                };
                ent.CentralizedBillingDetails.Add(ObjCBDetails);

                var ObjCBPtype = new CentralizedBillingPaymentType()
                {
                    BillNo = BillNumberIntWithoutDep,
                    PaymentTypeID = 1258,//Patient Deposit
                    PaymentSubTypeID = model.ObjDepositRefundViewModel.AccountSubHeadId,
                    Amount = model.ObjDepositRefundViewModel.RefundAmount,
                    Status = true

                };
                ent.CentralizedBillingPaymentTypes.Add(ObjCBPtype);


                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;



                ent.SaveChanges();

            }
            return true;
        }

        public bool DeleteCBBillNumberByBillId(string BillNumber, BillingCounterModel model)
        {
            if (!string.IsNullOrEmpty(BillNumber))
            {
                int BillNumberInt = Convert.ToInt32(BillNumber);
                using (EHMSEntities ent = new EHMSEntities())
                {

                    //var objtoUpdateCentralizedBilling = ent.CentralizedBillingDetail.Where(x => x.BillNo == BillNumberInt && x.Status == true);
                    var objtoUpdateCentralizedBilling = ent.CentralizedBillingMasters.Where(x => x.BillNo == BillNumberInt && x.Status == true);
                    foreach (var item2 in objtoUpdateCentralizedBilling)
                    {
                        item2.Status = false;
                        item2.Narration2 = "CancelBill";
                    }

                    //Insert into cancel bill detail table for records
                    var ObjBillSummary = new BillCancelSummary()
                    {
                        BillNumber = BillNumberInt,
                        CancelDate = DateTime.Today,
                        CancelBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                        CancelByDate = DateTime.Today,
                        Status = true,
                        ApprovedBy = 1,
                        VerifiedBy = 1,
                        Branch = 1,
                        FiscalYearId = Utility.GetCurrentFiscalYearID(),
                        BillTotalAmount = model.ObjBillingCounterPaymentDetails.GrandTotal,
                        Remarks = model.CancelBillRemarks,
                        ApprovedStatus = 1//pending
                    };
                    ent.BillCancelSummaries.Add(ObjBillSummary);

                    ent.SaveChanges();

                }

            }

            return true;
        }


    }
}