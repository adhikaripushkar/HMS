using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class JVMasterProvider
    {
        public List<JVMasterModel> GetlistFromJVMaster()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<JVMasterModel>(AutoMapper.Mapper.Map<IEnumerable<JVMaster>, IEnumerable<JVMasterModel>>(ent.JVMasters)).Take(20).ToList();
            }
        }

        public List<JVDetailModel> GetListFromJVDetails(int JvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<JVDetailModel>(AutoMapper.Mapper.Map<IEnumerable<JVDetail>, IEnumerable<JVDetailModel>>(ent.JVDetails.Where(x => x.JVMasterId == JvMasterId))).OrderBy(x => x.CrAmount).ToList();
            }

        }

        //Get Today Total Transaction
        public List<BillingDetailViewModel> GetTodayTransaction(string jvDate, string jvDateEnd)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingDetailViewModel>("BillJvDetail '" + jvDate + "', '" + jvDateEnd + "'").ToList();
            }
        }

        public List<BillingDetailViewModel> GetComissionTransaction(string jvDate, string jvDateEnd)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingDetailViewModel>("CommissionJvDetail '" + jvDate + "', '" + jvDateEnd + "'").ToList();
            }
        }

        public int CreateNewJv(JVMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                int fiscalyearid = Utility.GetCurrentFiscalYearID();
                int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber(model.JvType, fiscalyearid);
                string VoucherNumber = model.JvType + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();
                var JVMasterObj = new JVMaster()
                {
                    AccountNumber = "ac001",
                    BillNumber = "BN-001",
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    FiscalYearId = Utility.GetCurrentFiscalYearID(),
                    JvNumber = VoucherNumber,
                    Narration1 = model.ObjAddMoreParticularsJVModel.Narration,
                    Narration2 = model.ObjAddMoreParticularsJVModel.Narration,
                    Status = false,
                    JvType = model.JvType,
                    TotalAmount = model.ObjAddMoreParticularsJVModel.CrAmountTotal,
                    TransactionDate = DateTime.Now,
                    VerifiedBy = Utility.GetCurrentLoginUserId(),
                    FormName = "JV"
                };

                ent.JVMasters.Add(JVMasterObj);

                //JVMasterId Id
                int? JvMasterId = ent.JVMasters.Max(u => (int?)u.JvMasterId);
                if (JvMasterId == null)
                {
                    JvMasterId = 1;
                }
                else
                {
                    JvMasterId = JvMasterId + 1;
                }
                foreach (var item in model.AddMoreParticularsJVModelList)
                {
                    decimal totalDrAmount = 0;
                    decimal totalCrAmount = 0;
                    string DrOrCrValue = string.Empty;
                    if (item.DrOrCr == 1)
                    {
                        totalDrAmount = item.DrAmount;
                        DrOrCrValue = "Dr";
                    }
                    else
                    {
                        totalCrAmount = item.CrAmount;
                        DrOrCrValue = "Cr";
                    }
                    string AccountHeadName = "";

                    if (item.SubParticulars > 0)
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.Particulars) + "-" + HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.SubParticulars);
                    }
                    else
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.Particulars);
                    }

                    var jvdetailObj = new JVDetail()
                    {
                        BillNumber = "BL-001",
                        CrAmount = totalCrAmount,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DrAmount = totalDrAmount,
                        FeeTypeId = item.Particulars,
                        FeeTypeSubId = item.SubParticulars,
                        FeeTypeName = AccountHeadName,
                        JVMasterId = JVMasterObj.JvMasterId,
                        LinkToDr = 1,
                        Narration = model.ObjAddMoreParticularsJVModel.Narration,
                        PaymentType = "Cash",
                        Status = false,
                        TransactionDate = DateTime.Now,
                        DrOrCr = DrOrCrValue

                    };
                    ent.JVDetails.Add(jvdetailObj);
                }

                SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                    where x.JvType == model.JvType && x.FiscalYear == fiscalyearid
                                                    select x).First();
                vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;

                ent.SaveChanges();

                return ent.JVMasters.Max(item => item.JvMasterId);

            }

        }

        public int InsertHandoverDetail(JVMasterModel model)
        {
            int i = 0;
            int UserID = Utility.GetCurrentLoginUserId();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var HandoverDetailObj = new HandoverDetail()
                {
                    HandOverDate = DateTime.Today,
                    HandOverDateFrom = model.objGetCashHandoverDetailModelView.HandoverDateFrom,
                    HandOverDateTo = model.objGetCashHandoverDetailModelView.HandoverDateTo,
                    HandOverFrom = Utility.GetCurrentLoginUserId(),
                    HandOverTo = model.objGetCashHandoverDetailModelView.HandoverTo,
                    HandOverAmount = model.objGetCashHandoverDetailModelView.Amount,
                    Rs1000 = model.objGetCashHandoverDetailModelView.Rs1000,
                    Rs500 = model.objGetCashHandoverDetailModelView.Rs500,
                    Rs100 = model.objGetCashHandoverDetailModelView.Rs100,
                    Rs50 = model.objGetCashHandoverDetailModelView.Rs50,
                    Rs20 = model.objGetCashHandoverDetailModelView.Rs20,
                    Rs10 = model.objGetCashHandoverDetailModelView.Rs10,
                    Rs5 = model.objGetCashHandoverDetailModelView.Rs5,
                    Rs2 = model.objGetCashHandoverDetailModelView.Rs2,
                    Rs1 = model.objGetCashHandoverDetailModelView.Rs1,
                    Status = "R",
                    IsHandOver = false,
                    Remarks = model.objGetCashHandoverDetailModelView.Remarks,
                    TotalBankDeposit = model.objGetCashHandoverDetailModelView.TotalBankDeposit,
                    TotalExpenses = model.objGetCashHandoverDetailModelView.TotalExpenses
                };
                ent.HandoverDetails.Add(HandoverDetailObj);

                var CBMaster = (from x in ent.CentralizedBillingMasters
                                where (x.BillDate.Year >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Year && x.BillDate.Month >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Month && x.BillDate.Day >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Day && (x.BillDate.Year <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Year && x.BillDate.Month <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Month && x.BillDate.Day <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Day)) && x.CreatedBy == UserID
                                select x);
                foreach (CentralizedBillingMaster cb in CBMaster)
                {
                    cb.IsHandover = true;
                }

                var HandOver = (from x in ent.HandoverDetails
                                where (x.HandOverDateFrom.Year >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Year && x.HandOverDateFrom.Month >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Month && x.HandOverDateFrom.Day >= model.objGetCashHandoverDetailModelView.HandoverDateFrom.Day && (x.HandOverDateTo.Year <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Year && x.HandOverDateTo.Month <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Month && x.HandOverDateTo.Day <= model.objGetCashHandoverDetailModelView.HandoverDateTo.Day)) && x.HandOverTo == UserID
                                select x);
                foreach (HandoverDetail ho in HandOver)
                {
                    ho.IsHandOver = true;
                }
                i = ent.SaveChanges();
                return i;
            }
        }

        public int SaveJournnalVoucher(JVMasterModel model, string jvDate, string jvEndDate, string Narration)
        {
            int i = 0;
            int fiscalyearid = Utility.GetCurrentFiscalYearID();
            DateTime JVTransactionDate = Convert.ToDateTime(jvDate);
            DateTime JvTransactionEndDate = Convert.ToDateTime(jvEndDate);
            int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber(model.JvType, fiscalyearid);
            string VoucherNumber = model.JvType + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var JVMasterObj = new JVMaster()
                {
                    AccountNumber = "ac001",
                    BillNumber = "BN-001",
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    FiscalYearId = Utility.GetCurrentFiscalYearID(),
                    JvNumber = VoucherNumber,
                    Narration1 = Narration,
                    Narration2 = Narration,
                    Status = false,
                    JvType = model.JvType,
                    //TotalAmount = model.objBillingDetailViewModel.TotalAmount,
                    TotalAmount = model.objBillingDetailViewModel.AmountAfterDiscount,
                    TransactionDate = DateTime.Now,
                    VerifiedBy = Utility.GetCurrentLoginUserId(),
                    FormName = "JV"
                };
                ent.JVMasters.Add(JVMasterObj);
                int? JvMasterId = ent.JVMasters.Max(u => (int?)u.JvMasterId);
                if (JvMasterId == null)
                {
                    JvMasterId = 1;
                }
                else
                {
                    JvMasterId = JvMasterId + 1;
                }
                var JVDetails = new JVDetail();
                foreach (var item in model.BillingDetailViewModelList)
                {
                    decimal totalDrAmount = 0;
                    decimal totalCrAmount = 0;
                    string DrOrCrValue = string.Empty;
                    if (item.Type == "DR" || item.Type == "DDR")
                    {
                        totalDrAmount = item.Amount;
                        DrOrCrValue = "Dr";
                    }
                    else
                    {
                        totalCrAmount = item.Amount;
                        DrOrCrValue = "Cr";
                    }
                    if (totalCrAmount > 0 || totalDrAmount > 0)
                    {

                        JVDetails = new JVDetail()
                        {
                            BillNumber = "BL001",
                            CrAmount = totalCrAmount,
                            DrAmount = totalDrAmount,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Now,
                            FeeTypeId = item.AccountHeadID,
                            FeeTypeSubId = item.AccountSubHeadID,
                            FeeTypeName = item.HeadName,
                            JVMasterId = JVMasterObj.JvMasterId,
                            TransactionDate = DateTime.Now,
                            PaymentType = "",
                            Status = false,
                            DrOrCr = DrOrCrValue
                        };
                        ent.JVDetails.Add(JVDetails);
                    }

                }

                //UpdateCentralizeBillingDetails(VoucherNumber, JVTransactionDate);
                var CBDetails = (from x in ent.CentralizedBillings
                                 where (x.AmountDate.Year >= JVTransactionDate.Year && x.AmountDate.Month >= JVTransactionDate.Month && x.AmountDate.Day >= JVTransactionDate.Day && (x.AmountDate.Year <= JvTransactionEndDate.Year && x.AmountDate.Month <= JvTransactionEndDate.Month && x.AmountDate.Day <= JvTransactionEndDate.Day)) && x.JVStatus == false
                                 select x);
                foreach (CentralizedBilling cb in CBDetails)
                {
                    cb.JVNumber = VoucherNumber;
                    cb.JVStatus = true;

                }


                var CBMaster = (from x in ent.CentralizedBillingMasters
                                where (x.BillDate.Year >= JVTransactionDate.Year && x.BillDate.Month >= JVTransactionDate.Month && x.BillDate.Day >= JVTransactionDate.Day && (x.BillDate.Year <= JvTransactionEndDate.Year && x.BillDate.Month <= JvTransactionEndDate.Month && x.BillDate.Day <= JvTransactionEndDate.Day)) && x.JVStatus == false
                                select x);
                foreach (CentralizedBillingMaster cb in CBMaster)
                {
                    cb.JVNumber = VoucherNumber;
                    cb.JVStatus = true;
                }


                //Account Department.........static number 1505 for jv only
                SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                    where x.JvType == model.JvType
                                                    && x.FiscalYear == fiscalyearid
                                                    select x).First();
                vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;


                i = ent.SaveChanges();
                return i;
            }

        }

        public int SaveComissionJV(JVMasterModel model, string jvDate, string jvEndDate, string Narration)
        {
            int i = 0;
            int fiscalyearid = Utility.GetCurrentFiscalYearID();
            DateTime JVTransactionDate = Convert.ToDateTime(jvDate);
            DateTime JvTransactionEndDate = Convert.ToDateTime(jvEndDate);
            int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber(model.JvType, fiscalyearid);
            string VoucherNumber = model.JvType + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();
            using (EHMSEntities ent = new EHMSEntities())
            {
                decimal totalDrAmount = 0;
                decimal totalCrAmount = 0;
                foreach (var item in model.BillingDetailViewModelList)
                {
                    if (item.Type == "DR")
                    {
                        totalDrAmount += item.Amount;
                    }
                    else
                    {
                        totalCrAmount += item.Amount;
                    }
                }
                var JVMasterObj = new JVMaster()
                {
                    AccountNumber = "ac001",
                    BillNumber = "BN-001",
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    FiscalYearId = Utility.GetCurrentFiscalYearID(),
                    JvNumber = VoucherNumber,
                    Narration1 = Narration,
                    Narration2 = Narration,
                    Status = false,
                    JvType = model.JvType,
                    TotalAmount = totalDrAmount,
                    TransactionDate = DateTime.Now,
                    VerifiedBy = Utility.GetCurrentLoginUserId(),
                    FormName = "Comission"
                };
                ent.JVMasters.Add(JVMasterObj);
                int? JvMasterId = ent.JVMasters.Max(u => (int?)u.JvMasterId);
                if (JvMasterId == null)
                {
                    JvMasterId = 1;
                }
                else
                {
                    JvMasterId = JvMasterId + 1;
                }
                var JVDetails = new JVDetail();
                foreach (var item in model.BillingDetailViewModelList)
                {
                    totalDrAmount = 0;
                    totalCrAmount = 0;
                    string DrOrCrValue = string.Empty;
                    if (item.Type == "DR")
                    {
                        totalDrAmount = item.Amount;
                        DrOrCrValue = "Dr";
                    }
                    else
                    {
                        totalCrAmount = item.Amount;
                        DrOrCrValue = "Cr";
                    }
                    if (totalCrAmount > 0 || totalDrAmount > 0)
                    {
                        JVDetails = new JVDetail()
                        {
                            BillNumber = "BL001",
                            CrAmount = totalCrAmount,
                            DrAmount = totalDrAmount,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Now,
                            FeeTypeId = item.AccountHeadID,
                            FeeTypeSubId = item.AccountSubHeadID,
                            FeeTypeName = item.HeadName,
                            JVMasterId = JVMasterObj.JvMasterId,
                            TransactionDate = DateTime.Now,
                            PaymentType = "",
                            Status = false,
                            DrOrCr = DrOrCrValue
                        };
                        ent.JVDetails.Add(JVDetails);
                    }
                }

                var ComDetail = (from x in ent.CommissionDetaislByTypes
                                 where (x.CommissionDate.Year >= JVTransactionDate.Year && x.CommissionDate.Month >= JVTransactionDate.Month && x.CommissionDate.Day >= JVTransactionDate.Day && (x.CommissionDate.Year <= JvTransactionEndDate.Year && x.CommissionDate.Month <= JvTransactionEndDate.Month && x.CommissionDate.Day <= JvTransactionEndDate.Day)) && x.JvStatus == false
                                 select x);
                foreach (CommissionDetaislByType cb in ComDetail)
                {
                    cb.JvStatus = true;
                }

                SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                    where x.JvType == model.JvType
                                                    && x.FiscalYear == fiscalyearid
                                                    select x).First();
                vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;

                i = ent.SaveChanges();
                return i;
            }

        }

        private void UpdateCentralizeBillingDetails(string VoucherNumber, DateTime jvDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var CBDetails = (from x in ent.CentralizedBillings
                                 where (x.AmountDate.Year == jvDate.Year && x.AmountDate.Month == jvDate.Month && x.AmountDate.Day == jvDate.Day) && x.JVStatus == false
                                 select x);
                foreach (CentralizedBilling cb in CBDetails)
                {
                    cb.JVNumber = VoucherNumber;
                    cb.Status = true;
                }
            }
        }

        //public decimal GetTotalIncomeAmount(string departmentName)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {

        //        System.Nullable<decimal> iReturnValue = ent.TotalIncomeDepatWise(departmentName).FirstOrDefault();
        //        if (iReturnValue.HasValue)
        //        {
        //            return Convert.ToDecimal(iReturnValue);
        //        }
        //        else
        //        {
        //            return Convert.ToDecimal(0);
        //        }
        //    }

        //}noticedf

        public int GetLastInsertedJVNumber()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.JVMasters.Count();
                if (count > 0)
                {
                    return ent.JVMasters.Max(x => x.JvMasterId);
                }
                else
                {
                    return 0;
                }
            }

        }

        public string GetJVNumberFromJVMasterId(int JvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).Count();
                if (count > 0)
                {
                    return ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).FirstOrDefault().JvNumber;
                }
                else
                {
                    return "";
                }
            }
        }

        public string GetJVTypeFromJVMasterId(int JvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).Count();
                if (count > 0)
                {
                    return ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).FirstOrDefault().JvType;
                }
                else
                {
                    return "";
                }
            }
        }

        public DateTime GetJVTransactionDate(int jvmId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.JVMasters.Where(x => x.JvMasterId == jvmId).Count();
                if (count > 0)
                {
                    var Tdate = ent.JVMasters.Where(x => x.JvMasterId == jvmId).FirstOrDefault().CreatedDate;
                    return Convert.ToDateTime(Tdate);
                }
                else
                {
                    return DateTime.Now;
                }
            }

        }

        public string GetJVNarration(int JvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).Count();
                if (count > 0)
                {
                    var NarrationString = ent.JVMasters.Where(x => x.JvMasterId == JvMasterId);
                    if (NarrationString != null)
                    {
                        return ent.JVMasters.Where(x => x.JvMasterId == JvMasterId).FirstOrDefault().Narration1;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public List<LedgerDetailsViewModel> GetLedgerDeatilList(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<LedgerDetailsViewModel>("PatientDepositDetail '" + model.ObjLedgerDetailsViewModel.StartDate + "', '" + model.ObjLedgerDetailsViewModel.EndDate + "'").ToList();
            }

        }

        public List<LedgerViewModelAccountHeadWise> GetLeadgerAccountHeadwise(JVMasterModel model, int DrOrCr)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.ObjLedgerViewModelAccountHeadWise.FeeSubTypeId > 0)
                {
                    model.ObjLedgerViewModelAccountHeadWise.FeeTypeId = model.ObjLedgerViewModelAccountHeadWise.FeeSubTypeId;
                }
                return ent.Database.SqlQuery<LedgerViewModelAccountHeadWise>("GetLedgerAccountHeadWise '" + model.ObjLedgerViewModelAccountHeadWise.StartDate + "', '" + model.ObjLedgerViewModelAccountHeadWise.EndDate + "', '" + model.ObjLedgerViewModelAccountHeadWise.FeeTypeId + "','" + model.ObjLedgerViewModelAccountHeadWise.FeeSubTypeId + "'").ToList();
            }
        }

        public List<GetTrialBalanceModelView> GetTrialBalance(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetTrialBalanceModelView>("GetTrialBalance '" + model.objGetTrialBalanceModelView.StartDate + "', '" + model.objGetTrialBalanceModelView.EndDate + "', '" + model.objGetTrialBalanceModelView.Level + "'").ToList();
            }
        }

        public List<PLScheduleModelView> GetPLSchedule(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PLScheduleModelView>("GetSchedule '" + model.objPLScheduleModelView.StartDate + "', '" + model.objPLScheduleModelView.EndDate + "', '" + model.objPLScheduleModelView.HierarchyCode + "', '" + model.objPLScheduleModelView.Level + "'").ToList();
            }
        }

        public List<BSScheduleModelView> GetBSSchedule(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BSScheduleModelView>("GetSchedule '" + model.objBSScheduleModelView.StartDate + "', '" + model.objBSScheduleModelView.EndDate + "', '" + model.objBSScheduleModelView.HierarchyCode + "', '" + model.objBSScheduleModelView.Level + "'").ToList();
            }
        }

        public List<BalanceSheetModelView> GetBalanceSheetAssets(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BalanceSheetModelView>("GetBalanceSheetAssets '" + model.objBalanceSheetModelView.EndDate + "', '" + model.objBalanceSheetModelView.FiscalYear + "'").ToList();
            }
        }

        public List<GetBilldetailByUserModelView> GetBillDetailByUser(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetBilldetailByUserModelView>("BillDetailByUser '" + model.objGetBilldetailByUserModelView.CreatedBy + "', '" + model.objGetBilldetailByUserModelView.BillDate + "','" + model.objGetBilldetailByUserModelView.BillDateTo + "'").ToList();
            }
        }

        public List<GetBilldetailByDepartmentModelView> GetBillDetailByDepartment(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetBilldetailByDepartmentModelView>("BillDetailByDepartment '" + model.objGetBilldetailByDepartmentModelView.BillDate + "','" + model.objGetBilldetailByDepartmentModelView.BillDateTo + "'").ToList();
            }
        }

        public List<GetCashHandoverDetailModelView> GetCashDetailByUser(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetCashHandoverDetailModelView>("HandOverCashDetail '" + Utility.GetCurrentLoginUserId() + "','" + model.objGetCashHandoverDetailModelView.HandoverDateFrom + "','" + model.objGetCashHandoverDetailModelView.HandoverDateTo + "'").ToList();
            }
        }

        public List<HandoverDetailReportModelView> HandOverReport(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<HandoverDetailReportModelView>("ReportOfHandOverDetail '" + model.objHandoverDetailReportModelView.HandoverDateFrom + "','" + model.objHandoverDetailReportModelView.HandoverDateTo + "'").ToList();
            }
        }

        public JVMasterModel GetJVListBySearchParameter(JVMasterModel model, string StartingDate, string EndDate)
        {
            string JvNumber = string.Empty;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.JvNumber != null)
                {
                    JvNumber = "JV-" + model.JvNumber;

                }

                var result = ent.Database.SqlQuery<JVMasterModel>("ListJournalVoucherBySearch '" + JvNumber + "', '" + StartingDate + "', '" + EndDate + "', '" + model.JvType + "'").ToList();
                foreach (var item in result)
                {
                    var Viewmodel = new JVMasterModel()
                    {
                        JvNumber = item.JvNumber,
                        TransactionDate = item.TransactionDate,
                        Narration1 = item.Narration1,
                        JvMasterId = item.JvMasterId,
                        TotalAmount = item.TotalAmount
                    };
                    model.JVMasterModelList.Add(Viewmodel);

                }
                return model;
            }
        }

        public int UpdateHandoverStatus()
        {
            int i = 0;
            int UserID = Utility.GetCurrentLoginUserId();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var HandOver = (from x in ent.HandoverDetails
                                where x.HandOverTo == UserID
                                select x);
                foreach (HandoverDetail ho in HandOver)
                {
                    ho.Status = "A";
                }
                i = ent.SaveChanges();
                return i;
            }
        }

        public List<NotificationHandoverModelView> NotifyHandOver(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<NotificationHandoverModelView>("NotifyHandOverAmount '" + id + "'").ToList();
            }
        }

        public List<OpeningBalanceModelView> GetLeafLevelByID(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccSubGruupID as AccountHeadId, AccSubGroupName as AccountHeadName,isnull(DrAmount,0)DrAmount,isnull(CrAmount,0)CrAmount from GL_AccSubGroups left join
                                                                         OpeningBalance on GL_AccSubGroups.AccSubGruupID=OpeningBalance.AccountHeadId where AccGroupID='" + id + "' and IsLeafLevel=1";
                return ent.Database.SqlQuery<OpeningBalanceModelView>(sql).ToList();
            }
        }

        public int InsertOpeningBalance(JVMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                int fyid = Utility.GetCurrentFiscalYearID();
                foreach (var item in model.objOpeningBalanceModelViewList)
                {
                    int count = ent.OpeningBalances.Where(x => x.AccountHeadId == item.AccountHeadId && x.FiscalYear >= fyid).Count();
                    if (count > 0)
                    {
                        var OpeningBal = (from x in ent.OpeningBalances
                                          where (x.AccountHeadId == item.AccountHeadId && x.FiscalYear == fyid)
                                          select x);
                        foreach (OpeningBalance opng in OpeningBal)
                        {
                            opng.DrAmount = item.DrAmount;
                            opng.CrAmount = item.CrAmount;
                        }
                    }
                    else
                    {
                        if (item.DrAmount + item.CrAmount != 0)
                        {
                            var objOpening = new OpeningBalance()
                            {
                                FiscalYear = fyid,
                                AccountHeadId = item.AccountHeadId,
                                DrAmount = item.DrAmount,
                                CrAmount = item.CrAmount,
                                Status = true
                            };
                            ent.OpeningBalances.Add(objOpening);
                        }
                    }
                    i = ent.SaveChanges();
                }
                return i;
            }
        }

        public List<GetTrialBalanceModelView> NewTrialBalance(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetTrialBalanceModelView>("GetNewTrialBalance '" + model.objGetTrialBalanceModelView.StartDate + "', '" + model.objGetTrialBalanceModelView.EndDate + "', '" + model.objGetTrialBalanceModelView.Level + "'").ToList();
            }
        }

        public List<JVDetailModel> GetJvDisplayReport(int jvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<JVDetailModel>("GetJvReportDisplay'" + jvMasterId + "'").ToList();
            }
        }

        public List<BillingDetailViewModel> GetBillDetailDisplayReport(string jvDate, string jvDateEnd)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingDetailViewModel>("GetBillDetailDisplayReport'" + jvDate + "', '" + jvDateEnd + "'").ToList();
            }
        }

        public List<GetAllTrialBalanceModelView> GetAllTrialBalance(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetAllTrialBalanceModelView>("GetTrialBalanceAll '" + model.objGetAllTrialBalanceModelView.StartDate + "', '" + model.objGetAllTrialBalanceModelView.EndDate + "', '" + model.objGetAllTrialBalanceModelView.Level + "','" + model.objGetAllTrialBalanceModelView.ParentID + "'").ToList();
            }
        }

        public List<BalanceSheetModelView> GetBalanceSheetLiability(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BalanceSheetModelView>("GetBalanceSheetLiability '" + model.objBalanceSheetModelView.EndDate + "', '" + model.objBalanceSheetModelView.FiscalYear + "'").ToList();
            }
        }

        public List<IncomeAndExpenditureModelView> GetIncomeAndExpenditure(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<IncomeAndExpenditureModelView>("GetIncomeAndExpenditure '" + model.objIncomeAndExpenditureModelView.StartDate + "', '" + model.objIncomeAndExpenditureModelView.EndDate + "'").ToList();
            }
        }

        public List<GetBankReconcilationModelView> GetBankReconcilation(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = "";
                if (model.objGetBankReconcilationModelView.CheckValue)
                {
                    if (model.objGetBankReconcilationModelView.BankSubID == 0)
                    {
                        sql = @"select dbo.GetNepaliDate(JVMaster.TransactionDate)TransactionDate, JVMaster.JvNumber, JVMaster.Narration1 as Narration,JVDetails.DrAmount as TotalDebit, JVDetails.CrAmount as TotalCredit, BankReconcilation.BankDate as State from JVDetails 
                                                                                    inner join JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId
                                                                                    left join BankReconcilation on JVMaster.JvNumber=BankReconcilation.JvNo
                                                                                    where JVDetails.FeeTypeId='" + model.objGetBankReconcilationModelView.BankID + "' and JVMaster.TransactionDate Between '" + model.objGetBankReconcilationModelView.StartDate + "' and '" + model.objGetBankReconcilationModelView.EndDate + "' and isnull(BankReconcilation.BankDate,'')!=''";

                    }
                    else
                    {
                        sql = @"select dbo.GetNepaliDate(JVMaster.TransactionDate)TransactionDate, JVMaster.JvNumber, JVMaster.Narration1 as Narration,JVDetails.DrAmount as TotalDebit, JVDetails.CrAmount as TotalCredit, BankReconcilation.BankDate as State from JVDetails 
                                                                                    inner join JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId
                                                                                    left join BankReconcilation on JVMaster.JvNumber=BankReconcilation.JvNo
                                                                                    where JVDetails.FeeTypeId='" + model.objGetBankReconcilationModelView.BankID + "' and JVDetails.FeeTypeSubId='" + model.objGetBankReconcilationModelView.BankSubID + "' and JVMaster.TransactionDate Between '" + model.objGetBankReconcilationModelView.StartDate + "' and '" + model.objGetBankReconcilationModelView.EndDate + "' and isnull(BankReconcilation.BankDate,'')!=''";
                    }
                }
                else
                {
                    if (model.objGetBankReconcilationModelView.BankSubID == 0)
                    {
                        sql = @"select dbo.GetNepaliDate(JVMaster.TransactionDate)TransactionDate, JVMaster.JvNumber, JVMaster.Narration1 as Narration,JVDetails.DrAmount as TotalDebit, JVDetails.CrAmount as TotalCredit, BankReconcilation.BankDate as State from JVDetails 
                                                                                    inner join JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId
                                                                                    left join BankReconcilation on JVMaster.JvNumber=BankReconcilation.JvNo
                                                                                    where JVDetails.FeeTypeId='" + model.objGetBankReconcilationModelView.BankID + "' and JVMaster.TransactionDate Between '" + model.objGetBankReconcilationModelView.StartDate + "' and '" + model.objGetBankReconcilationModelView.EndDate + "' and isnull(BankReconcilation.BankDate,'')=''";

                    }
                    else
                    {
                        sql = @"select dbo.GetNepaliDate(JVMaster.TransactionDate)TransactionDate, JVMaster.JvNumber, JVMaster.Narration1 as Narration,JVDetails.DrAmount as TotalDebit, JVDetails.CrAmount as TotalCredit, BankReconcilation.BankDate as State from JVDetails 
                                                                                    inner join JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId
                                                                                    left join BankReconcilation on JVMaster.JvNumber=BankReconcilation.JvNo
                                                                                    where JVDetails.FeeTypeId='" + model.objGetBankReconcilationModelView.BankID + "' and JVDetails.FeeTypeSubId='" + model.objGetBankReconcilationModelView.BankSubID + "' and JVMaster.TransactionDate Between '" + model.objGetBankReconcilationModelView.StartDate + "' and '" + model.objGetBankReconcilationModelView.EndDate + "' and isnull(BankReconcilation.BankDate,'')=''";
                    }
                }
                return ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).ToList();
            }
        }

        public List<JVDetailModel> GetJvPayrollDisplayReport(int jvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<JVDetailModel>(@"Select FeeTypeName, DrAmount, CrAmount from JVDetails inner join JVMaster
                                                                on JVMaster.JvMasterId=JVDetails.JVMasterId
                                                                where JVMaster.JvMasterId='" + jvMasterId + @"'
                                                                order by CrAmount asc").ToList();
            }
        }

        public List<JVDetailModel> GetJvCommisionDisplayReport(int jvMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<JVDetailModel>(@"Select case when isnull(AccSubGroupName,'')='' then FeeTypeName
                                                                else FeeTypeName+'-'+isnull(AccSubGroupName,'') end FeeTypeName, DrAmount, CrAmount from JVDetails 
                                                                inner join JVMaster on JVMaster.JvMasterId=JVDetails.JVMasterId
                                                                left join GL_AccSubGroups on GL_AccSubGroups.AccSubGruupID=JVDetails.FeeTypeSubId
                                                                where JVMaster.JvMasterId='" + jvMasterId + @"'
                                                                order by CrAmount asc").ToList();
            }
        }

        public bool CreateBankReconcilation(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int fiscalyearid = Utility.GetCurrentFiscalYearID();
                foreach (var item in model.objGetBankReconcilationModelView.objGetBankReconcilationModelViewList)
                {
                    RemoveBankReconcilationJv(item.JvNumber);
                    var BankReconObj = new BankReconcilation()
                    {
                        BankID = model.objGetBankReconcilationModelView.BankID,
                        BankSubID = model.objGetBankReconcilationModelView.BankSubID,
                        FromDate = model.objGetBankReconcilationModelView.StartDate,
                        ToDate = model.objGetBankReconcilationModelView.EndDate,
                        JvNo = item.JvNumber,
                        JvDate = item.TransactionDate,
                        Narration = item.Narration,
                        DrAmount = item.TotalDebit,
                        CrAmount = item.TotalCredit,
                        BankDate = item.State,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    ent.BankReconcilations.Add(BankReconObj);
                }
                ent.SaveChanges();
            }
            return true;
        }

        private void RemoveBankReconcilationJv(string JvNumber)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.BankReconcilations.Where(x => x.JvNo == JvNumber);
                foreach (var item in collection)
                {
                    var objToDelete = ent.BankReconcilations.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.BankReconcilations.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        public int CountifComisionPosted(string JvDate, string JvEndDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int count = ent.CommissionDetails.Count();
                if (count > 0)
                {
                    string sql = @"select COUNT(*)Count from CommissionDetails where CreatedDate between '" + JvDate + "' and '" + JvEndDate + "' and Status=0";
                    return ent.Database.SqlQuery<JVMasterModel>(sql).FirstOrDefault().Count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}