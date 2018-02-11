using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class OpdProvider
    {
        public List<OpdModel> Getlist()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters).ToList());
            }
        }

        public int GetTotalItemCount(int departmentId)
        {
            if (departmentId == 0)
                return 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (departmentId > 0)
                {
                    return ent.OpdMasters.Where(x => x.DepartmentId == departmentId && x.RegistrationSource == "Opd").Count();
                }

                else//For superadmin
                {
                    return ent.OpdMasters.Where(x => x.RegistrationSource == "Opd").Count();
                }
            }


        }
        public List<OpdModel> Getlist(int page, int pagesize)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<OpdModel> model = new List<OpdModel>();
                var collection = ent.OpdMasters.OrderByDescending(x => x.OpdID).Where(x => x.RegistrationSource == "Opd").Skip((page - 1) * pagesize).Take(pagesize);
                foreach (var item in collection)
                {
                    OpdModel data = new OpdModel()
                    {
                        OpdID = item.OpdID,
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        Sex = item.Sex,
                        ContactName = item.ContactName,
                        RegistrationDate = (DateTime)item.RegistrationDate,
                        DepartMentID = (int)item.DepartmentId,
                        AgeYear = item.AgeYear,
                        Address = item.Address,
                        MobileNumber = item.MobileNumber
                    };

                    model.Add(data);
                }
                return model;
            }
        }
        public List<OpdModel> GetlistByDepartmentId(int departmentId, int page, int pagesize)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<OpdModel> model = new List<OpdModel>();
                var collection = ent.OpdMasters.OrderBy(x => x.OpdID).Where(x => x.DepartmentId == departmentId && x.RegistrationSource == "Opd").Skip((page - 1) * pagesize).Take(pagesize);
                foreach (var item in collection)
                {
                    OpdModel data = new OpdModel()
                    {
                        OpdID = item.OpdID,
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        Sex = item.Sex,
                        ContactName = item.ContactName,
                        RegistrationDate = (DateTime)item.RegistrationDate,
                        DepartMentID = (int)item.DepartmentId,
                        Address = item.Address
                    };

                    model.Add(data);
                }
                return model;
            }

        }


        public int CountTodayPatient()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string date = DateTime.Now.ToShortDateString();
                DateTime d = @Convert.ToDateTime(date);
                int i = ent.OpdMasters.Where(x => x.RegistrationDate == d && x.RegistrationSource == "Opd").Count();
                return i;
            }

        }

        public List<OpdDoctorListModel> GetPatientDoctorList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdDoctorListModel>(AutoMapper.Mapper.Map<IEnumerable<OpdPatientDoctorDetail>, IEnumerable<OpdDoctorListModel>>(ent.OpdPatientDoctorDetails.Where(x => x.OpdID == id)));
            }
        }

        public List<OpdFeeDetailsModel> GetPatientFeeDetailsList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdFeeDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<OpdFeeDetail>, IEnumerable<OpdFeeDetailsModel>>(ent.OpdFeeDetails.Where(x => x.OpdID == id)));
            }
        }

        public int Insert(OpdModel model)
        {
            int i = 0;
            //Paid On Paid 
            bool PaidOrOnPaidPatient;
            bool IsSelfBilling = HospitalManagementSystem.Utility.IsSelfBillingDepartmentTrue();
            if (IsSelfBilling)
            {
                PaidOrOnPaidPatient = true;
            }
            else
            {
                PaidOrOnPaidPatient = false;
            }
            int HospitalType = 1;
            if (model.opdtype == "2")
            {
                HospitalType = 2;
            }

            using (EHMSEntities ent = new EHMSEntities())
            {

                //Insert into OpdMaster Table
                model.COA = 0;
                var objTosaveOpdMaster = AutoMapper.Mapper.Map<OpdModel, OpdMaster>(model);
                objTosaveOpdMaster.CreatedBy = Utility.GetCurrentLoginUserId();
                objTosaveOpdMaster.CreatedDate = DateTime.Now;
                objTosaveOpdMaster.Status = true;
                objTosaveOpdMaster.DepartmentId = Utility.GetCurrentUserDepartmentId();
                objTosaveOpdMaster.DepartmentPatientId = null;
                //objTosaveOpdMaster.RegistrationDate = DateTime.Now;
                objTosaveOpdMaster.RegistrationDate = model.RegistrationDate;
                objTosaveOpdMaster.RegistrationMode = "Visit";
                objTosaveOpdMaster.RegistrationSource = "Opd";
                objTosaveOpdMaster.PaidOnPaid = PaidOrOnPaidPatient;
                objTosaveOpdMaster.DistrictId = model.DistrictID;
                objTosaveOpdMaster.ZoneID = model.ZoneId;
                objTosaveOpdMaster.ReferId = model.ReferId;
                objTosaveOpdMaster.RegistrationDate = DateTime.Now;
                ent.OpdMasters.Add(objTosaveOpdMaster);

                //Insert into OpdpatientDoctorDetails Table
                foreach (var item in model.OpdDoctorList)
                {
                    model.OpdPatientDoctorDetailsModel = new OpdPatientDoctorDetailsModel();
                    var objTosaveOpdDoctorList = AutoMapper.Mapper.Map<OpdPatientDoctorDetailsModel, OpdPatientDoctorDetail>(model.OpdPatientDoctorDetailsModel);
                    objTosaveOpdDoctorList.DoctorID = item.DoctorID;
                    objTosaveOpdDoctorList.DepartmentID = item.DepartmentID;
                    objTosaveOpdDoctorList.OpdID = objTosaveOpdMaster.OpdID;
                    objTosaveOpdDoctorList.PreferDate = item.PreferDate;
                    objTosaveOpdDoctorList.PreferTime = item.PreferTime;
                    objTosaveOpdDoctorList.RegistrationDate = DateTime.Now;
                    ent.OpdPatientDoctorDetails.Add(objTosaveOpdDoctorList);
                }



                //Insert into opdfeedetails Table
                decimal RegFeeOnlyOpd = model.OpdFeeDetailsModel.RegistrationFee;
                //decimal RegFeeOnlyOpd = model.OpdFeeDetailsModel.RegistrationFeeOnly;
                ////decimal RegFeeOpd = model.OpdFeeDetailsModel.RegistrationFee;
                //Edited onn 9/5
                //decimal RegTaxOnly = model.OpdFeeDetailsModel.FeeTaxAmount;
                decimal RegTaxOnly = Convert.ToDecimal(19);
                if (model.OtherDiscountID != 23 && model.OtherDiscountID != 24 && model.OtherDiscountID != 20)
                {
                    RegTaxOnly = Convert.ToDecimal(15.20);
                }
                else if (model.OtherDiscountID == 24)
                {
                    RegTaxOnly = Convert.ToDecimal(0.00);
                }
                else if (model.OtherDiscountID == 20)
                {
                    RegTaxOnly = Convert.ToDecimal(1.90);
                }


                if (model.OldOrNewRegistration == 3)
                {
                    RegFeeOnlyOpd = Convert.ToDecimal(404.00);
                    RegTaxOnly = Convert.ToDecimal(20.20);
                    if (model.OtherDiscountID != 23 && model.OtherDiscountID != 24 && model.OtherDiscountID != 20)
                    {
                        RegTaxOnly = Convert.ToDecimal(16.16);
                    }
                    else if (model.OtherDiscountID == 24)
                    {
                        RegTaxOnly = Convert.ToDecimal(0.00);
                    }
                    else if (model.OtherDiscountID == 20)
                    {
                        RegTaxOnly = Convert.ToDecimal(2.02);
                    }

                }

                var objTosaveFeedetails = AutoMapper.Mapper.Map<OpdFeeDetailsModel, OpdFeeDetail>(model.OpdFeeDetailsModel);
                objTosaveFeedetails.OpdID = objTosaveOpdMaster.OpdID;
                objTosaveFeedetails.FeeDate = DateTime.Now;
                //objTosaveFeedetails.RegistrationFee = model.OpdFeeDetailsModel.RegistrationFeeOnly;
                //objTosaveFeedetails.FeeTaxAmount = model.OpdFeeDetailsModel.FeeTaxAmount;

                //objTosaveFeedetails.RegistrationFee = RegFeeOpd;
                objTosaveFeedetails.RegistrationFee = RegFeeOnlyOpd;
                objTosaveFeedetails.FeeTaxAmount = RegTaxOnly;

                objTosaveFeedetails.DoctorFeeTax = Convert.ToDecimal(0);
                objTosaveFeedetails.OtherDiscountPercentage = 0;
                ent.OpdFeeDetails.Add(objTosaveFeedetails);

                //Legder&Transaction
                string LedgerName = "A/C " + model.FirstName + " " + (model.MiddleName + " " ?? string.Empty) + model.LastName;

                //Opd master max id
                int? intIdt = ent.OpdMasters.Max(u => (int?)u.OpdID);
                if (intIdt == null)
                {
                    intIdt = 1;
                }
                else
                {
                    intIdt = intIdt + 1;
                }

                //Insert Patient Log into Log Table
                var objToSavePatientLogMaster = new PatientLogMaster()
                {
                    PatientId = intIdt.GetValueOrDefault(),
                    RegistrationDate = DateTime.Now,
                    DepartmentId = Utility.GetCurrentLoginUserId(),
                    OpdTypeID = Utility.GetOpdTypeFromSession(),//Payable or free
                    Status = true
                };
                ent.PatientLogMasters.Add(objToSavePatientLogMaster);

                //Patient logmaster max id
                int? intPatientLogID = ent.PatientLogMasters.Max(u => (int?)u.OpdMasterLogId);
                if (intPatientLogID == null)
                {
                    intPatientLogID = 1;
                }
                else
                {
                    intPatientLogID = intPatientLogID + 1;

                }

                //Insert into Ledger Table
                var objtoInsertLedger = new GL_LedgerMaster()
                {
                    AccountGroupID = 1,
                    AccountSubGroupID = 1,
                    AccountTypeID = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    DepartmentID = Utility.GetCurrentUserDepartmentId(),
                    LedgerName = LedgerName,
                    //SourceID = ent.OpdMaster.Max(a => a.OpdID+1),
                    SourceID = intIdt,
                    LedgerSourceType = "Patient",
                    Status = 1
                };

                ent.GL_LedgerMaster.Add(objtoInsertLedger);

                //LedgerMasterId Max Id
                int? LedgetMasterId = ent.GL_LedgerMaster.Max(u => (int?)u.LedgerMasterID);
                if (LedgetMasterId == null)
                {
                    LedgetMasterId = 1;
                }
                else
                {
                    LedgetMasterId = LedgetMasterId + 1;

                }

                //Calcuate registration fee and tax
                //Edited 9/5
                decimal RegFeeOnly = RegFeeOnlyOpd;
                //decimal RegFeeOnly = Hospital.Utility.GetCurrentRegistrationFeeOnly();
                //decimal RegFeeWithTaxAmount = Hospital.Utility.GetCurrentRegistrationTaxFee();
                decimal RegFeeWithTaxAmount = RegTaxOnly;

                decimal RegFeeOnlyFromModel = model.OpdFeeDetailsModel.RegistrationFee;


                //Get Current Bill Number from Table
                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();

                //Check If discount or not
                decimal TotalDiscountAmount = Convert.ToDecimal(0);
                int TotalDiscountHeadId = 0;
                //Discount Calculation
                if (model.OpdFeeDetailsModel.OtherDiscountAmount > 0)
                {
                    TotalDiscountAmount = model.OpdFeeDetailsModel.OtherDiscountAmount;
                    TotalDiscountHeadId = 369;

                }


                //Add Details in new table
                var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                {
                    BillNo = BillNumberInt,
                    TotalBillAmount = model.OpdFeeDetailsModel.TotalAmount + model.OpdFeeDetailsModel.OtherDiscountAmount,
                    Narration1 = "Narraion",
                    Narration2 = "",
                    DepartmentName = "Opd",
                    SubDepartmentId = 1,
                    PatientId = intIdt.GetValueOrDefault(),
                    PatientLogId = (int)intPatientLogID,
                    TotalDiscountID = TotalDiscountHeadId,
                    TotalDiscountAmount = TotalDiscountAmount,
                    BillDate = DateTime.Now,

                    //JVNumber=1
                    JVStatus = false,
                    CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Opd",
                    Status = true,
                    BranchId = 1,
                    ReceiptId = 0,
                    IsHandover = false,
                    ReturnedAmount = model.OpdFeeDetailsModel.ReturnAmount,
                    TenderAmount = model.OpdFeeDetailsModel.Tender,
                    PayableType = HospitalType

                };
                ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);

                var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                {
                    BillNo = BillNumberInt,
                    PaymentTypeID = 372,
                    PaymentSubTypeID = 0,
                    Amount = model.OpdFeeDetailsModel.TotalAmount,
                    Status = true
                };
                ent.CentralizedBillingPaymentTypes.Add(ObjCentralizedBillingPaymentType);




                int AccHeadId = 362;
                //Edited 9/5
                decimal FeeTypeAmount = RegFeeOnlyOpd;
                //decimal FeeTypeAmount = model.OpdFeeDetailsModel.RegistrationFeeOnly;
                decimal RegFeeWithTaxAmtDynamic = RegFeeWithTaxAmount;
                // turnok




                //if (model.OldOrNewRegistration == 2)
                //{
                //    AccHeadId = 364;
                //    FeeTypeAmount = Hospital.Utility.GetCurrentOldRegistrationFee();
                //    RegFeeWithTaxAmtDynamic = Convert.ToDecimal(1.90);
                //}

                //Reg fee and tax in new table

                //var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                //{
                //    BillNo = BillNumberInt,
                //    AccountHeadID = 362,//New Registration coa
                //    Amount = model.OpdFeeDetailsModel.RegistrationFeeOnly,
                //    Status = true,
                //    DepartmentId = 1000

                //};
                var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = AccHeadId,//New Registration coa
                    Amount = FeeTypeAmount,
                    Status = true,
                    DepartmentId = 1000,
                    Times = 1,
                    Narration2 = Convert.ToString(model.OpdFeeDetailsModel.TotalAmount)

                };

                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 1261,//tax from coa
                    //Amount = RegFeeWithTaxAmount,
                    Amount = RegFeeWithTaxAmtDynamic,
                    Status = true,
                    DepartmentId = 1000,
                    Times = 1


                };
                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                //Insert into Centralized Billing table
                var objtoSaveCentralizedBilling = new CentralizedBilling()
                {
                    AccountHeadId = 1,
                    Amount = model.OpdFeeDetailsModel.RegistrationFeeOnly,
                    AmountDate = DateTime.Now,
                    PaymentType = "Cash",
                    Narration1 = "Fee Details",
                    DepartmentName = "Opd",
                    SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    BillNumber = BillNumberStr,
                    LedgerMasterId = (int)LedgetMasterId,
                    PatientLogId = (int)intPatientLogID,
                    PatientId = intIdt.GetValueOrDefault(),
                    JVStatus = false,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Opd",
                    PaidOnPaid = PaidOrOnPaidPatient,
                    Status = true,
                    ItemDiscountPercentage = 0
                };

                ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                //Reg Tax
                objtoSaveCentralizedBilling = new CentralizedBilling()
                {
                    AccountHeadId = 2,
                    Amount = RegFeeWithTaxAmount,
                    AmountDate = DateTime.Now,
                    PaymentType = "Cash",
                    Narration1 = "Fee Details",
                    DepartmentName = "Opd",
                    SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    BillNumber = BillNumberStr,
                    LedgerMasterId = (int)LedgetMasterId,
                    PatientLogId = (int)intPatientLogID,
                    PatientId = intIdt.GetValueOrDefault(),
                    JVStatus = false,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Opd",
                    PaidOnPaid = PaidOrOnPaidPatient,
                    Status = true,
                    ItemDiscountPercentage = 0
                };
                ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                //Doctor Fee
                if (model.OpdFeeDetailsModel.DoctorFee > 0)
                {
                    foreach (var item in model.OpdDoctorList)
                    {
                        //Doctor Fee New
                        ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                        {
                            BillNo = BillNumberInt,
                            AccountHeadID = 368,
                            AccountSubHeadID = item.DoctorID,//Doctor Id
                            Amount = model.OpdFeeDetailsModel.DoctorFee,
                            Status = true,
                            DepartmentId = 1000

                        };

                        ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                        //Doctor Fee Tax only
                        ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                        {
                            BillNo = BillNumberInt,
                            AccountHeadID = 1261,
                            AccountSubHeadID = item.DoctorID,//Doctor Id
                            Amount = model.OpdFeeDetailsModel.DoctorFeeTax,
                            Status = true,
                            DepartmentId = 1000

                        };
                        ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);



                        objtoSaveCentralizedBilling = new CentralizedBilling()
                        {
                            AccountHeadId = 5,
                            Amount = model.OpdFeeDetailsModel.DoctorFee,
                            AmountDate = DateTime.Now,
                            PaymentType = "Cash",
                            Narration1 = "Fee Details",
                            DepartmentName = "Opd",
                            SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            BillNumber = BillNumberStr,
                            LedgerMasterId = (int)LedgetMasterId,
                            PatientLogId = (int)intPatientLogID,
                            PatientId = intIdt.GetValueOrDefault(),
                            JVStatus = false,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            CreatedDate = DateTime.Now,
                            Remarks = "Opd",
                            PaidOnPaid = PaidOrOnPaidPatient,
                            Status = true,
                            ItemDiscountPercentage = 0
                        };
                        ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                        objtoSaveCentralizedBilling = new CentralizedBilling()
                        {
                            AccountHeadId = 6,
                            Amount = model.OpdFeeDetailsModel.DoctorFeeTax,
                            AmountDate = DateTime.Now,
                            PaymentType = "Cash",
                            Narration1 = "Fee Details",
                            DepartmentName = "Opd",
                            SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            BillNumber = BillNumberStr,
                            LedgerMasterId = (int)LedgetMasterId,
                            PatientLogId = (int)intPatientLogID,
                            PatientId = intIdt.GetValueOrDefault(),
                            JVStatus = false,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            CreatedDate = DateTime.Now,
                            Remarks = "Opd",
                            PaidOnPaid = PaidOrOnPaidPatient,
                            Status = true,
                            ItemDiscountPercentage = 0
                        };
                        ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                    }
                }



                //other Discount
                if (model.OpdFeeDetailsModel.OtherDiscountAmount > 0)
                {
                    objtoSaveCentralizedBilling = new CentralizedBilling()
                    {
                        AccountHeadId = 22,
                        Amount = model.OpdFeeDetailsModel.OtherDiscountAmount,
                        AmountDate = DateTime.Now,
                        PaymentType = "Cash",
                        Narration1 = "Fee Details",
                        DepartmentName = "Opd",
                        SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        BillNumber = BillNumberStr,
                        LedgerMasterId = (int)LedgetMasterId,
                        PatientLogId = (int)intPatientLogID,
                        PatientId = intIdt.GetValueOrDefault(),
                        JVStatus = false,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        CreatedDate = DateTime.Now,
                        Remarks = "Opd",
                        PaidOnPaid = PaidOrOnPaidPatient,
                        Status = true,
                        ItemID = model.OtherDiscountID,//old age, blind, others......
                        ItemDiscountPercentage = 0
                    };
                    ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);
                }




                if (model.opdtype == "2")
                {

                    if (model.OpdDoctorList != null)
                    {
                        int DoctorIdInt = model.OpdDoctorList.FirstOrDefault().DoctorID;
                        //Get Doctor Commission
                        decimal DoctorCommission = 200;
                        if (DoctorIdInt != 1009)
                        {
                            var CommDetails = new CommissionDetail()
                            {
                                //TestID = 362,
                                //DepartmentID = 1000,
                                //CreatedDate = DateTime.Now,
                                //CreatedBy = Hospital.Utility.GetCurrentLoginUserId(),
                                //Status = 1,
                                BillNumber = BillNumberInt,
                                CommissionAmount = DoctorCommission,
                                CommissionName = "Doctor Commission",
                                CommissionType = 1,
                                CreatedBy = Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                DepartmentId = 1000,
                                DocId = DoctorIdInt,
                                JVStatus = false,
                                Status = true,
                                TestId = 362,

                            };
                            ent.CommissionDetails.Add(CommDetails);
                            ent.SaveChanges();
                            int LastInsertdId = CommDetails.CommissionDetailsId;

                            var commissionObj = new CommissionDetaislByType()
                            {
                                AccountHeadId = 2000,
                                Amount = 200,
                                BillNo = BillNumberInt,
                                CommissionDate = DateTime.Now,
                                commissionDetailsId = LastInsertdId,
                                CommissionTypeId = 1,
                                CommissionTypeName = "Doctor Commission",
                                CreatedBy = Utility.GetCurrentLoginUserId(),
                                Status = true,
                                CreatedDate = DateTime.Now,
                                DoctorId = DoctorIdInt,
                                Remarks = "",
                                JvStatus = false


                            };
                            ent.CommissionDetaislByTypes.Add(commissionObj);
                        }


                    }
                }

                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();

                billNumber.BillNumber = billNumber.BillNumber + 1;


                i = ent.SaveChanges();
                return objTosaveOpdMaster.OpdID;

            }

        }


        public int Update(OpdModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                //opdMaster
                var objtoeditOpdMaster = ent.OpdMasters.Where(x => x.OpdID == model.OpdID).FirstOrDefault();


                var objtoeditPatientFee = ent.OpdFeeDetails.Where(x => x.OpdID == model.OpdID && x.FeeDate == model.RegistrationDate).FirstOrDefault();
                //var objtoeditPatientFee = ent.OpdFeeDetails.Where(x => x.OpdID == model.OpdID).FirstOrDefault();
                model.RegistrationSource = objtoeditOpdMaster.RegistrationSource;
                AutoMapper.Mapper.Map(model, objtoeditOpdMaster);
                ent.Entry(objtoeditOpdMaster).State = System.Data.EntityState.Modified;

                objtoeditPatientFee.RegistrationFee = model.OpdFeeDetailsModel.RegistrationFee;
                objtoeditPatientFee.DoctorFee = model.OpdFeeDetailsModel.DoctorFee;
                objtoeditPatientFee.MemberDiscountAmount = model.OpdFeeDetailsModel.MemberDiscountAmount;
                objtoeditPatientFee.OtherDiscountAmount = model.OpdFeeDetailsModel.OtherDiscountAmount;
                objtoeditPatientFee.TotalAmount = model.OpdFeeDetailsModel.TotalAmount;
                objtoeditPatientFee.Remarks = model.OpdFeeDetailsModel.Remarks;

                var dataDoctorToDelete = ent.OpdPatientDoctorDetails.Where(x => x.OpdID == model.OpdID && x.RegistrationDate == model.RegistrationDate).ToList();
                foreach (var item in dataDoctorToDelete)
                {
                    var objDoctorToDelete = ent.OpdPatientDoctorDetails.Where(y => y.OpdID == item.OpdID && y.RegistrationDate == item.RegistrationDate).FirstOrDefault();
                    ent.OpdPatientDoctorDetails.Remove(objDoctorToDelete);
                    ent.SaveChanges();
                }

                foreach (var item in model.OpdDoctorList)
                {
                    model.OpdPatientDoctorDetailsModel = new OpdPatientDoctorDetailsModel();
                    var objTosaveOpdDoctorList = AutoMapper.Mapper.Map<OpdPatientDoctorDetailsModel, OpdPatientDoctorDetail>(model.OpdPatientDoctorDetailsModel);
                    objTosaveOpdDoctorList.DoctorID = item.DoctorID;
                    objTosaveOpdDoctorList.DepartmentID = item.DepartmentID;
                    objTosaveOpdDoctorList.OpdID = objtoeditOpdMaster.OpdID;
                    objTosaveOpdDoctorList.PreferDate = item.PreferDate;
                    objTosaveOpdDoctorList.PreferTime = item.PreferTime;
                    objTosaveOpdDoctorList.RegistrationDate = objtoeditOpdMaster.RegistrationDate;
                    ent.OpdPatientDoctorDetails.Add(objTosaveOpdDoctorList);
                }

                //Update Ledger Name too...........
                string LedgerName = "A/C " + model.FirstName + " " + (model.MiddleName + " " ?? string.Empty) + model.LastName;
                GL_LedgerMaster ledgerMaster = (from x in ent.GL_LedgerMaster
                                                where x.SourceID == model.OpdID
                                                select x).First();
                //vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;
                ledgerMaster.LedgerName = LedgerName;

                i = ent.SaveChanges();
                return i;
            }
        }

        public int OldPatientInsert(OpdModel model)
        {
            bool PaidOrOnPaidPatient;
            bool IsSelfBilling = HospitalManagementSystem.Utility.IsSelfBillingDepartmentTrue();
            if (IsSelfBilling)
            {
                PaidOrOnPaidPatient = true;
            }
            else
            {
                PaidOrOnPaidPatient = false;
            }

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.OpdDoctorList)
                {
                    model.OpdPatientDoctorDetailsModel = new OpdPatientDoctorDetailsModel();
                    var objTosaveOpdDoctorList = AutoMapper.Mapper.Map<OpdPatientDoctorDetailsModel, OpdPatientDoctorDetail>(model.OpdPatientDoctorDetailsModel);
                    objTosaveOpdDoctorList.DoctorID = item.DoctorID;
                    objTosaveOpdDoctorList.DepartmentID = item.DepartmentID;
                    objTosaveOpdDoctorList.OpdID = model.OpdID;
                    objTosaveOpdDoctorList.PreferDate = item.PreferDate;
                    objTosaveOpdDoctorList.PreferTime = item.PreferTime;
                    objTosaveOpdDoctorList.RegistrationDate = model.RegistrationDate;
                    ent.OpdPatientDoctorDetails.Add(objTosaveOpdDoctorList);
                }

                DateTime TodayDate = DateTime.Now;
                DateTime PreviousRegDate = model.RegistrationDate;
                decimal totalDays = Math.Round(Convert.ToDecimal((TodayDate - PreviousRegDate).TotalDays));

                decimal RegFeeOld = HospitalManagementSystem.Utility.GetCurrentOldRegistrationFee();
                decimal RegFeeOldTax = HospitalManagementSystem.Utility.GetCurrentOldRegistrationTaxFee();

                if (totalDays > 7)
                {
                    RegFeeOld = HospitalManagementSystem.Utility.GetCurrentRegistrationFeeOnly();
                    RegFeeOldTax = HospitalManagementSystem.Utility.GetCurrentRegistrationTaxFee();
                }

                //opdfeedetails
                var objTosaveFeedetails = AutoMapper.Mapper.Map<OpdFeeDetailsModel, OpdFeeDetail>(model.OpdFeeDetailsModel);
                // decimal? aa = objTosaveFeedetails.DoctorFee;
                objTosaveFeedetails.OpdID = model.OpdID;
                objTosaveFeedetails.FeeDate = DateTime.Now;
                objTosaveFeedetails.OtherDiscountPercentage = 0;
                objTosaveFeedetails.RegistrationFee = RegFeeOld;
                objTosaveFeedetails.FeeTaxAmount = RegFeeOldTax;
                objTosaveFeedetails.DoctorFeeTax = Convert.ToDecimal(0);
                ent.OpdFeeDetails.Add(objTosaveFeedetails);


                var objToSavePatientLogMaster = new PatientLogMaster()
                {
                    PatientId = model.OpdID,
                    RegistrationDate = DateTime.Now,
                    DepartmentId = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    OpdTypeID = Utility.GetOpdTypeFromSession(),
                    Status = true

                };
                ent.PatientLogMasters.Add(objToSavePatientLogMaster);

                //Patient logmaster max id
                int? intPatientLogID = ent.PatientLogMasters.Max(u => (int?)u.OpdMasterLogId);
                if (intPatientLogID == null)
                {
                    intPatientLogID = 1;
                }
                else
                {
                    intPatientLogID = intPatientLogID + 1;

                }


                //Legder&Transaction
                string LedgerName = "A/C " + model.FirstName + " " + (model.MiddleName + " " ?? string.Empty) + model.LastName;
                int LedgerMasterId = ent.GL_LedgerMaster.Where(x => x.SourceID == model.OpdID).FirstOrDefault().LedgerMasterID;


                //Calcuate registration fee and tax
                decimal RegFeeOnly = HospitalManagementSystem.Utility.GetCurrentRegistrationFeeOnly();
                decimal RegFeeWithTaxAmount = HospitalManagementSystem.Utility.GetCurrentRegistrationTaxFee();
                decimal RegFeeOnlyFromModel = model.OpdFeeDetailsModel.RegistrationFee;

                //Get Current Bill Number from Table
                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();

                //Check If discount or not
                decimal TotalDiscountAmount = Convert.ToDecimal(0);
                int TotalDiscountHeadId = 0;
                //Discount Calculation
                if (model.OpdFeeDetailsModel.OtherDiscountAmount > 0)
                {
                    TotalDiscountAmount = model.OpdFeeDetailsModel.OtherDiscountAmount;
                    TotalDiscountHeadId = 369;

                }


                //Add Details in new table
                var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                {
                    BillNo = BillNumberInt,
                    TotalBillAmount = model.OpdFeeDetailsModel.TotalAmount + model.OpdFeeDetailsModel.OtherDiscountAmount,
                    Narration1 = "Narraion",
                    Narration2 = "",
                    DepartmentName = "Opd",
                    SubDepartmentId = 1,
                    PatientId = model.OpdID,
                    PatientLogId = (int)intPatientLogID,
                    TotalDiscountID = TotalDiscountHeadId,
                    TotalDiscountAmount = TotalDiscountAmount,
                    BillDate = DateTime.Now,

                    //JVNumber=1
                    JVStatus = false,
                    CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Opd",
                    Status = true,
                    BranchId = 1,
                    ReceiptId = 0,
                    IsHandover = false,
                    ReturnedAmount = model.OpdFeeDetailsModel.ReturnAmount,
                    TenderAmount = model.OpdFeeDetailsModel.Tender

                };
                ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);

                var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                {
                    BillNo = BillNumberInt,
                    PaymentTypeID = 372,
                    PaymentSubTypeID = 0,
                    Amount = model.OpdFeeDetailsModel.TotalAmount,
                    Status = true
                };
                ent.CentralizedBillingPaymentTypes.Add(ObjCentralizedBillingPaymentType);

                int AccHeadId = 364;
                decimal FeeTypeAmount = model.OpdFeeDetailsModel.RegistrationFeeOnly;
                decimal RegFeeWithTaxAmtDynamic = RegFeeWithTaxAmount;


                if (totalDays <= 7)
                {

                    FeeTypeAmount = HospitalManagementSystem.Utility.GetCurrentOldRegistrationFee();
                    RegFeeWithTaxAmtDynamic = Convert.ToDecimal(1.90);
                }




                var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = AccHeadId,//New Registration coa
                    Amount = FeeTypeAmount,
                    Status = true,
                    DepartmentId = 1000,
                    Times = 1,
                    Narration2 = Convert.ToString(model.OpdFeeDetailsModel.TotalAmount)

                };

                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 1261,//tax from coa
                    //Amount = RegFeeWithTaxAmount,
                    Amount = RegFeeWithTaxAmtDynamic,
                    Status = true,
                    DepartmentId = 1000,
                    Times = 1


                };
                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);


                //Doctor Fee
                if (model.OpdFeeDetailsModel.DoctorFee > 0)
                {
                    foreach (var item in model.OpdDoctorList)
                    {
                        //Doctor Fee New
                        ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                        {
                            BillNo = BillNumberInt,
                            AccountHeadID = 368,
                            AccountSubHeadID = item.DoctorID,//Doctor Id
                            Amount = model.OpdFeeDetailsModel.DoctorFee,
                            Status = true,
                            DepartmentId = 1000

                        };

                        ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                        //Doctor Fee Tax only
                        ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                        {
                            BillNo = BillNumberInt,
                            AccountHeadID = 1261,
                            AccountSubHeadID = item.DoctorID,//Doctor Id
                            Amount = model.OpdFeeDetailsModel.DoctorFeeTax,
                            Status = true,
                            DepartmentId = 1000

                        };
                        ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);


                    }




                }


                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;

                if (model.opdtype == "2")
                {

                    if (model.OpdDoctorList != null)
                    {
                        int DoctorIdInt = model.OpdDoctorList.FirstOrDefault().DoctorID;
                        if (DoctorIdInt != 1009)
                        {
                            var DocCommision = new DoctorCommision()
                            {
                                DoctorID = DoctorIdInt,
                                TestID = 362,
                                DepartmentID = 1000,
                                DoctorCom = HospitalManagementSystem.Utility.getdoctorcommisionbydocid(DoctorIdInt),
                                CreatedDate = DateTime.Now,
                                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                                Status = 1

                            };
                            ent.DoctorCommisions.Add(DocCommision);
                        }


                    }
                }


                i = ent.SaveChanges();

                return i;
            }
        }

        public OpdModel GetOpdDetails(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();
                model.PatientDoctorList = new List<OpdPatientDoctorDetailsModel>();
                var objOpdMaster = ent.OpdMasters.Where(x => x.OpdID == id).SingleOrDefault();
                AutoMapper.Mapper.Map(objOpdMaster, model);
                var objOpdDoctorDetails = ent.OpdPatientDoctorDetails.OrderByDescending(x => x.PatientDoctorDetailID).Where(x => x.OpdID == id).ToList();
                //var objOpdDoctorDetails = ent.OpdPatientDoctorDetails.OrderByDescending(x => x.PatientDoctorDetailID).Where(x => x.OpdID == id).ToList().FirstOrDefault();
                AutoMapper.Mapper.Map(objOpdDoctorDetails, model.PatientDoctorList);
                model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
                var objOpdFeeDetails = ent.OpdFeeDetails.OrderByDescending(x => x.OpdFeeDetailsID).Where(x => x.OpdID == id).FirstOrDefault();
                AutoMapper.Mapper.Map(objOpdFeeDetails, model.OpdFeeDetailsModel);
                return model;
            }

        }
        public OpdModel GetOpdDetailsForMultipleBillCase(int FeeDetailId, int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();
                model.PatientDoctorList = new List<OpdPatientDoctorDetailsModel>();
                var objOpdMaster = ent.OpdMasters.Where(x => x.OpdID == PatientId).SingleOrDefault();
                AutoMapper.Mapper.Map(objOpdMaster, model);
                var objOpdDoctorDetails = ent.OpdPatientDoctorDetails.OrderByDescending(x => x.PatientDoctorDetailID).Where(x => x.OpdID == PatientId).ToList();
                //var objOpdDoctorDetails = ent.OpdPatientDoctorDetails.OrderByDescending(x => x.PatientDoctorDetailID).Where(x => x.OpdID == id).ToList().FirstOrDefault();
                AutoMapper.Mapper.Map(objOpdDoctorDetails, model.PatientDoctorList);
                model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
                var objOpdFeeDetails = ent.OpdFeeDetails.OrderByDescending(x => x.OpdID).Where(x => x.OpdID == PatientId && x.OpdFeeDetailsID == FeeDetailId).FirstOrDefault();
                AutoMapper.Mapper.Map(objOpdFeeDetails, model.OpdFeeDetailsModel);
                return model;
            }
        }


        public List<OpdModel> SearchOPD(int opdid, string name)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                name = name.Trim();
                if (opdid == 0 && name == "")
                {
                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters)).Take(20).ToList();
                }
                else if (opdid == 0 && name != "")
                {
                    var query = from opd in ent.OpdMasters
                                where opd.FirstName.ToLower().Contains(name.ToLower()) || opd.MiddleName.ToLower().Contains(name.ToLower()) || opd.LastName.ToLower().Contains(name.ToLower())
                                select opd;

                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
                else if (opdid != 0 && name == "")
                {
                    var query = from opd in ent.OpdMasters
                                    //let fullName = opd.FirstName.ToLower() + " " + opd.MiddleName.ToLower() + " " + opd.LastName.ToLower()
                                where opd.OpdID == opdid
                                //where opd.OpdID == opdid && opd.RegistrationSource == "Opd"
                                select opd;

                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
                else
                {
                    var query = from opd in ent.OpdMasters
                                let fullName = opd.FirstName.ToLower() + " " + opd.MiddleName.ToLower() + " " + opd.LastName.ToLower()
                                where opd.OpdID == opdid && fullName.Contains(name.ToLower()) && opd.RegistrationSource == "Opd"
                                select opd;
                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
            }

        }

        public List<OpdModel> SearchOPDWithArress(string address)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var res = from opd in ent.OpdMasters
                          where opd.Address.Contains(address.ToLower())
                          select opd;
                return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(res)).ToList();
            }

        }


        public List<OpdModel> SearchOPDWithPhone(string phone)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from opd in ent.OpdMasters
                             where opd.RegistrationSource == "Opd" && opd.MobileNumber.Contains(phone.ToLower())
                             select opd;
                return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(result)).ToList();
            }
        }

        public List<OpdModel> SearchByBloodGroup(string id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from opd in ent.OpdMasters
                             where opd.BloodGroup == id && opd.RegistrationSource == "Opd"
                             select opd;

                return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(result)).ToList();
            }


        }

        public List<BillingModel> GetTransacationDetailsById(int PatientLogMasterId)//This is not patientlogmasterid its bill number
        {
            int billNumber = PatientLogMasterId;
            List<BillingModel> lisBillingdata = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                //        select cbd.AccountHeadID as AccountHeadId,subgroup.AccSubGroupName as AccountHeadName, isnull(cbd.Amount,0) as TotalAmount, isnull(cbd.DiscountAmount,0) as Dic  from CentralizedBillingDetail as cbd inner join GL_AccSubGroups as subgroup
                //on subgroup.AccSubGruupID=cbd.AccountHeadID
                //where cbd.BillNo=@BillNumber
                var DetailsData = (from o in ent.CentralizedBillingDetails
                                   join l in ent.GL_AccSubGroups on o.AccountHeadID equals l.AccSubGruupID
                                   where o.BillNo == billNumber
                                   select new BillingModel
                                   {
                                       PainName = "",
                                       Narration1 = "",
                                       LedgerName = "",
                                       Amount = o.Amount,
                                       TransectionDate = DateTime.Now,
                                       FeeTypeName = l.AccSubGroupName

                                   }
                                   ).ToList();

                //var DetailsData = (from o in ent.OpdMaster
                //                   join l in ent.GL_LedgerMaster on o.OpdID equals l.SourceID
                //                   join t in ent.CentralizedBilling on o.OpdID equals t.PatientId
                //                   join s in ent.SetupFeeType on t.AccountHeadId equals s.SetupFeeTypeId
                //                   where
                //                   t.PatientLogId == PatientLogMasterId && l.DepartmentID != 1001 && t.DepartmentName == "Opd"
                //                   select new BillingModel
                //                   {
                //                       PainName = o.FirstName,
                //                       Narration1 = t.Narration1,
                //                       LedgerName = l.LedgerName,
                //                       Amount = t.Amount,
                //                       TransectionDate = t.AmountDate,
                //                       FeeTypeName = s.FeeTypeName
                //                   }).ToList();




                foreach (var item in DetailsData)
                {
                    lisBillingdata.Add(item);
                }

                return lisBillingdata;
            }

        }



        public List<CentralizeBillingViewModel> GetOpdBill(DateTime sdates, DateTime edate, string name, int id)
        {
            DateTime SdateOnly = sdates.AddDays(-1);
            DateTime EDateOlny = edate.AddDays(1);

            List<BillingModel> lisBillingdata = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (name == "Default" && id == 0)
                {
                    return ent.Database.SqlQuery<CentralizeBillingViewModel>("CentralizeBillingDetail '" + sdates + "','" + edate + "', 0, 'Opd', '" + name + "'").ToList();

                }
                else if (id == 0 && name != "Default")
                {
                    return ent.Database.SqlQuery<CentralizeBillingViewModel>("CentralizeBillingDetail '" + sdates + "','" + edate + "', 0, 'Opd', '" + name + "'").ToList();
                }
                else if (id != 0 && name == "Default")
                {
                    return ent.Database.SqlQuery<CentralizeBillingViewModel>("CentralizeBillingDetail '" + sdates + "','" + edate + "', '" + id + "', 'Opd', '" + name + "'").ToList();

                }
            }
            return null;

        }


        public int GetLastInsertedId()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int TotalCount = ent.OpdMasters.Count();
                if (TotalCount > 0)
                {
                    int LastInsertedId = ent.OpdMasters.OrderByDescending(x => x.OpdID).FirstOrDefault().OpdID;
                    return LastInsertedId;
                }

                else
                {
                    return 1;
                }
            }

        }

        public int GetNumberOfVisitOfPatientInHospital(int id)
        {
            int TotalVisit = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                TotalVisit = ent.OpdFeeDetails.Where(x => x.OpdID == id).Count();
            }
            return TotalVisit;
        }

        public OpdModel GetAllBillDate(int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();

                var result = ent.OpdFeeDetails.Where(x => x.OpdID == PatientId);
                foreach (var item in result)
                {
                    var Viewmodel = new OpdFeeDetailsModel()
                    {
                        FeeDate = (DateTime)item.FeeDate,
                        OpdID = item.OpdID,
                        OpdFeeDetailsID = item.OpdFeeDetailsID
                    };

                    model.OpdFeeDetailsModelList.Add(Viewmodel);

                }
                return model;
            }


        }

        public int InsertOPdVital(VitalForOthersModel model)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                //VitalForOthers objtosaveinvital = new VitalForOthers();
                var objtosaveinvital = AutoMapper.Mapper.Map<VitalForOthersModel, VitalForOther>(model);
                //objtosaveinvital.AVPU = model.AVPU;
                //objtosaveinvital.BMI = @Convert.ToDecimal(model.BMI);
                //objtosaveinvital.BP_Left = model.BP_Left;
                //objtosaveinvital.BP_Right = model.BP_Right;
                //objtosaveinvital.Department = model.Department;
                //objtosaveinvital.Feet = model.Feet;
                //objtosaveinvital.Inch = model.Inch;
                //objtosaveinvital.Pulse = model.Pulse;
                //objtosaveinvital.RR = model.RR;
                //objtosaveinvital.SPO2 = model.SPO2;
                //objtosaveinvital.PatinetLogId = model.PatinetLogId;
                //objtosaveinvital.OpdId = model.OpdId;
                //objtosaveinvital.TPR = model.TPR;
                //objtosaveinvital.VTime = model.VTime;
                //objtosaveinvital.Wt = model.Wt;
                //objtosaveinvital.VitalForOtherId = model.VitalForOtherId;
                //objtosaveinvital.Date = model.Date;
                //objtosaveinvital.ElergeryToDrugs = model.ElergeryToDrugs;




                objtosaveinvital.Staff = Utility.GetCurrentLoginUserId().ToString();
                objtosaveinvital.Department = "opd";
                ent.VitalForOthers.Add(objtosaveinvital);
                i = ent.SaveChanges();

            }

            return i;

        }


        public int EditOpdVital(VitalForOthersModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtoeditVital = ent.VitalForOthers.Where(x => x.VitalForOtherId == model.VitalForOtherId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoeditVital);

                //objtoeditVital.AVPU = model.AVPU;
                //objtoeditVital.BMI = @Convert.ToDecimal(model.BMI);
                //objtoeditVital.BP_Left = model.BP_Left;
                //objtoeditVital.BP_Right = model.BP_Right;
                //objtoeditVital.Department = model.Department;
                //objtoeditVital.Feet = model.Feet;
                //objtoeditVital.Inch = model.Inch;
                //objtoeditVital.Pulse = model.Pulse;
                //objtoeditVital.RR = model.RR;
                //objtoeditVital.SPO2 = model.SPO2;
                //objtoeditVital.PatinetLogId = model.PatinetLogId;
                //objtoeditVital.OpdId = model.OpdId;
                //objtoeditVital.TPR = model.TPR;
                //objtoeditVital.VTime = model.VTime;
                //objtoeditVital.Wt = model.Wt;
                //objtoeditVital.VitalForOtherId = model.VitalForOtherId;
                //objtoeditVital.Date = model.Date;
                //objtoeditVital.ElergeryToDrugs = model.ElergeryToDrugs;


                objtoeditVital.Staff = Utility.GetCurrentLoginUserId().ToString();
                i = ent.SaveChanges();

            }

            return i;
        }

        public List<VitalForOthersModel> GetListOfVitalWithPatientId(int patientid)
        {

            List<VitalForOthersModel> lstOfVital = new List<VitalForOthersModel>();

            VitalForOthersModel model = new VitalForOthersModel();

            int patientlogid = Utility.getPatientLogID(patientid);

            using (EHMSEntities ent = new EHMSEntities())
            {

                lstOfVital = new List<VitalForOthersModel>(AutoMapper.Mapper.Map<IEnumerable<VitalForOther>, IEnumerable<VitalForOthersModel>>(ent.VitalForOthers.Where(x => x.OpdId == patientid && x.PatinetLogId == patientlogid && x.Department == "opd").ToList()));


            }

            //foreach (var item in lstOfVital)
            //{

            //    item.height = item.Feet.ToString() + "'" + item.Inch.ToString() + "''";


            //}

            return lstOfVital;





        }

        public List<DoctorCommisionModel> GetDoctorCommision(DateTime sdates, DateTime edate)
        {
            //DateTime SdateOnly = sdates.AddDays(-1);
            DateTime EDateOlny = edate.AddDays(1);

            //List<BillingModel> lisBillingdata = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.Database.SqlQuery<DoctorCommisionModel>("GetDoctorCommisionDetails '" + sdates + "','" + EDateOlny + "'").ToList();


            }


        }




    }
}