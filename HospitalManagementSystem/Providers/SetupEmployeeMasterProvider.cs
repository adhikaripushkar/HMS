using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmployeeMasterProviders
    {
        public int Insert(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                int maxemerid;
                var query = ent.SetupEmployeeMasters.Where(m => m.EmployeeMasterId == ent.SetupEmployeeMasters.Max(n => n.EmployeeMasterId)).SingleOrDefault();
                if (query == null)
                {
                    maxemerid = 1;
                }
                else
                {
                    maxemerid = query.EmployeeMasterId + 1;
                }
                var objToSave = AutoMapper.Mapper.Map<SetupEmployeeMasterModel, SetupEmployeeMaster>(model);

                objToSave.CreatedBy = Utility.GetCurrentLoginUserId();
                objToSave.EmployeeCode = maxemerid.ToString();
                objToSave.Status = true;
                objToSave.CreatedDate = DateTime.Today;
                objToSave.SPAccountHeadID = 0;
                ent.SetupEmployeeMasters.Add(objToSave);
                i = ent.SaveChanges();
                model.EmployeeMasterId = objToSave.EmployeeMasterId;
                SaveDepartmentId(model);
                SaveShiftId(model);
                SaveEmployeeDetails(model);
                SaveEmployeeEducationDetail(model);
                SaveEmployeeTrainingDetails(model);
            }
            return i;
        }

        public int InsertPayroll(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new EmployeePayrollMaster()
                {
                    EmployeeMasterId = model.EmployeeMasterId,
                    BasicSalary = model.BasicSalary,
                    GradeNo = model.GradeNo,
                    GradeRate = model.GradeRate,
                    GradeAmount = model.GradeAmount,
                    CITNo = model.CITNo,
                    CITAmount = model.CITAmount,
                    CITPercentage = model.CITPercentage,
                    PFNo = model.PFNo,
                    PFAmount = model.PFAmount,
                    FundNo = model.FundNo,
                    FundAmount = model.FundAmount,
                    BankID = model.BankID,
                    BankAccountNo = model.BankAccountNo,
                    LoanDeduction = model.LoanDeduction,
                    LoanInterest = model.LoanInterest,
                    AdvanceDeduction = model.AdvanceDeduction,
                    InsuranceNo = model.InsuranceNo,
                    InsuranceAmount = model.InsuranceAmount,
                    PFDeductAmount = model.PFDeductAmount,
                    InsuranceDeductAmount = model.InsuranceDeductAmount,
                    SSTAmount = model.SSTAmount,
                    TaxAmount = model.TaxAmount,
                    FestivalAmount = model.FestivalAmount,
                    BudgetID = model.BudgetID,
                    BudgetSubID = model.BudgetSubID,
                    FundID = model.FundID,
                    LeaveDeduction = model.LeaveDeduction,
                    TotalAllowance = model.TotalAllowance,
                    TotalDeduction = model.TotalDeduction,
                    OverTimeDuration = model.OverTimeDuration,
                    OverTimeAmount = model.OverTimeAmount,
                    Remarks = model.Remarks,
                    EmpInsurancePaid = model.EmpInsurancePaid,
                    RemoteAreaDeduction = model.RemoteAreaDeduction,
                    PhysicalDisability = model.PhysicalDisability,
                    Status = true,
                    BranchID = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today,
                    PresentDay = model.PresentDay,
                    OTHrDay = model.OTHrDay,
                    DearnessAllowance = model.DearnessAllowance,
                    DressAllowance = model.DressAllowance,
                    TotalIncome = model.TotalIncome / model.Noofjoinmonth,
                    TotalTaxableAmount = model.TotalTaxableAmount / model.Noofjoinmonth,
                    NetTaxableAmount = model.NetTaxableAmount / model.Noofjoinmonth
                };
                ent.EmployeePayrollMasters.Add(objToSave);
                i = ent.SaveChanges();
                SaveEmployeeAllowanceDetail(model);
                SaveEmployeeDeductionDetails(model);
                return i;
            }
        }

        public int InsertFundDeductionSetup(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new tblFundDeductionSetup()
                {
                    EmployeeID = model.EmployeeMasterId,
                    YearID = model.YearId,
                    MonthID = model.MonthId,
                    DeductionPercentage = model.FundPercentage,
                    DeductionAmount = model.FundAmount,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today,
                    Status = true
                };
                ent.tblFundDeductionSetups.Add(objToSave);
                i = ent.SaveChanges();
                return i;

            }
        }

        public int GeneratePayroll(SetupEmployeeMasterModel model)
        {
            int i = 0;
            RemoveAllPayrollInfoByMonth(model.MonthId, model.YearId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.SetupEmployeeMasterModelList)
                {
                    var objToSave = new PayrollMaster()
                    {
                        EmployeeID = item.EmployeeMasterId,
                        BasicSalary = item.BasicSalary,
                        GradeNo = item.GradeNo,
                        GradeRate = item.GradeRate,
                        GradeAmount = item.GradeAmount,
                        CITNo = item.CITNo,
                        CITAmount = item.CITAmount,
                        CITPercentage = item.CITPercentage,
                        PFNo = item.PFNo,
                        PFAmount = item.PFAmount,
                        FundNo = item.FundNo,
                        FundAmount = item.FundAmount,
                        BankID = item.BankID,
                        BankAccountNo = item.BankAccountNo,
                        LoanDeduction = item.LoanDeduction,
                        LoanInterest = item.LoanInterest,
                        AdvanceDeduction = item.AdvanceDeduction,
                        InsuranceNo = item.InsuranceNo,
                        InsuranceAmount = item.InsuranceAmount,
                        PFDeductAmount = item.PFDeductAmount,
                        InsuranceDeductAmount = item.InsuranceDeductAmount,
                        SSTAmount = item.SSTAmount,
                        TaxAmount = item.TaxAmount,
                        FestivalAmount = item.FestivalAmount,
                        BudgetID = item.BudgetID,
                        BudgetSubID = item.BudgetSubID,
                        FundID = item.FundID,
                        LeaveDeduction = item.LeaveDeduction,
                        TotalAllowance = item.TotalAllowance,
                        TotalDeduction = item.TotalDeduction,
                        OverTimeDuration = item.OverTimeDuration,
                        OverTimeAmount = item.OverTimeAmount,
                        Remarks = item.Remarks,
                        EmpInsurancePaid = item.EmpInsurancePaid,
                        RemoteAreaDeduction = item.RemoteAreaDeduction,
                        PhysicalDisability = item.PhysicalDisability,
                        PresentDay = item.PresentDay,
                        OTHrDay = item.OTHrDay,
                        DearnessAllowance = item.DearnessAllowance,
                        DressAllowance = item.DressAllowance,
                        MonthID = model.MonthId,
                        YearID = model.YearId,
                        Status = true,
                        BranchID = 1,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Today,
                        IsPosted = false,
                        TotalIncome = item.TotalIncome,
                        TotalTaxableAmount = item.TotalTaxableAmount,
                        NetTaxableAmount = item.NetTaxableAmount,
                        IsPaid = false
                    };
                    ent.PayrollMasters.Add(objToSave);
                    i = ent.SaveChanges();
                }
                SaveEmployeeAllowanceDetailByMnth(model);
                SaveEmployeeDeductionDetailsByMnth(model);
                return i;
            }
        }

        private void RemoveAllPayrollInfoByMonth(int monthid, int yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.PayrollMasters.Where(x => x.MonthID == monthid && x.YearID == yearid);
                foreach (var item in collection)
                {
                    var objToDelete = ent.PayrollMasters.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.PayrollMasters.Remove(objToDelete);
                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllMonthlyAllowanceInfoByMonth(int monthid, int yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.MonthlyAllowances.Where(x => x.MonthID == monthid && x.YearID == yearid);
                foreach (var item in collection)
                {
                    var objToDelete = ent.MonthlyAllowances.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.MonthlyAllowances.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllMonthlyDeductByMonth(int monthid, int yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.MonthlyDeductions.Where(x => x.MonthID == monthid && x.YearID == yearid);
                foreach (var item in collection)
                {
                    var objToDelete = ent.MonthlyDeductions.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.MonthlyDeductions.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        public int SaveEmployeeAllowanceDetailByMnth(SetupEmployeeMasterModel model)
        {
            int i = 0;
            RemoveAllMonthlyAllowanceInfoByMonth(model.MonthId, model.YearId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.EmployeeAllowanceSetupModelList)
                {
                    var objToSave = new MonthlyAllowance()
                    {
                        EmployeeMasterId = item.EmployeeMasterId,
                        AllowanceID = item.AllowanceID,
                        Amount = item.AllowanceAmount,
                        Status = true,
                        BranchId = 1,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Today,
                        MonthID = model.MonthId,
                        YearID = model.YearId,
                        IsPosted = false
                    };
                    ent.MonthlyAllowances.Add(objToSave);
                    i = ent.SaveChanges();
                }
            }
            return i;
        }

        public void SaveEmployeeDeductionDetailsByMnth(SetupEmployeeMasterModel model)
        {
            RemoveAllMonthlyDeductByMonth(model.MonthId, model.YearId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.EmployeeDeductionSetupModelList)
                {
                    var objToSave = new MonthlyDeduction()
                    {
                        EmployeeMasterId = item.EmployeeMasterId,
                        DeductionID = item.DeductionID,
                        Amount = item.DeductionAmount,
                        Status = true,
                        BranchId = 1,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Today,
                        MonthID = model.MonthId,
                        YearID = model.YearId,
                        IsPosted = false
                    };
                    ent.MonthlyDeductions.Add(objToSave);
                    ent.SaveChanges();
                }
            }
        }

        public int UpdatePayroll(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmployeePayrollMasters.Where(x => x.EmployeeMasterId == model.EmployeeMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.CreatedBy = Utility.GetCurrentLoginUserId();
                objToEdit.Status = true;
                objToEdit.CreatedDate = DateTime.Today;
                objToEdit.TotalIncome = model.TotalIncome / model.Noofjoinmonth;
                objToEdit.TotalTaxableAmount = model.TotalTaxableAmount / model.Noofjoinmonth;
                objToEdit.NetTaxableAmount = model.NetTaxableAmount / model.Noofjoinmonth;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
                SaveEmployeeAllowanceDetail(model);
                SaveEmployeeDeductionDetails(model);
                return i;
            }
        }

        public int UpdateFundDeductionSetup(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.tblFundDeductionSetups.Where(x => x.ID == model.id).FirstOrDefault();
                objToEdit.EmployeeID = model.EmployeeMasterId;
                objToEdit.YearID = model.YearId;
                objToEdit.MonthID = model.MonthId;
                objToEdit.DeductionPercentage = model.FundPercentage;
                objToEdit.DeductionAmount = model.FundAmount;
                objToEdit.CreatedBy = Utility.GetCurrentLoginUserId();
                objToEdit.CreatedDate = DateTime.Today;
                objToEdit.Status = true;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
                return i;
            }
        }

        public List<SetupEmployeeMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"SELECT     dbo.SetupEmployeeMaster.EmployeeMasterId, dbo.SetupEmployeeMaster.EmployeeCode, dbo.SetupEmployeeMaster.EmployeeTypeId, 
                                          dbo.SetupEmployeeMaster.EmployeeFullName, dbo.SetupEmployeeMaster.EmployeeDOB, dbo.SetupEmployeeMaster.EmployeePhone, 
                                          dbo.SetupEmployeeMaster.EmployeeMobile, dbo.SetupEmployeeMaster.EmployeeEmail, dbo.SetupEmployeeMaster.EmployeeJoinDate, 
                                          dbo.SetupEmployeeMaster.PermanentAddress, dbo.SetupEmployeeMaster.TemporaryAddress, isnull(dbo.SetupEmployeeMaster.DepartmentID,0)DepartmentID, 
                                          isnull(dbo.SetupEmployeeMaster.DesignationID,0)DesignationID, dbo.SetupEmployeeMaster.CountryID, dbo.SetupEmployeeMaster.CityName, dbo.SetupEmployeeMaster.GenderID, 
                                          case when isnull(SetupEmployeeMaster.DateOfRetirement,'')=''
                                          then convert(date,GETDate())
                                          else SetupEmployeeMaster.DateOfRetirement end DateOfRetirement, dbo.SetupEmployeeMaster.CreatedDate, dbo.SetupEmployeeMaster.CreatedBy, dbo.SetupEmployeeMaster.Status, 
                                          dbo.SetupEmployeeMaster.LoginDepartmentIdnt, isnull(dbo.SetupEmployeeMaster.BranchId,0)BranchId, dbo.SetupEmployeeMaster.IsVolunteer,  
                                          dbo.EmployeeDetails.CitizenshipNo, dbo.EmployeeDetails.MaritialStatus, isnull(dbo.EmployeeDetails.ReleaseDistrict,0)ReleaseDistrict, 
                                          dbo.EmployeeDetails.PanNo, dbo.EmployeeDetails.InsuranceNo, dbo.EmployeeDetails.BloodGroup, dbo.EmployeeDetails.GrandFatherName, 
                                          dbo.EmployeeDetails.FatherName, dbo.EmployeeDetails.MotherName, dbo.EmployeeDetails.WifeHusbandName, dbo.EmployeeDetails.NomineeName, 
                                          dbo.EmployeeDetails.NomineeAddress, dbo.EmployeeDetails.NomineeRelation, dbo.EmployeeDetails.NomineePhoneNo, TypeOfEmployee
                    FROM         dbo.SetupEmployeeMaster LEFT OUTER JOIN
                                          dbo.EmployeeDetails ON dbo.SetupEmployeeMaster.EmployeeMasterId = dbo.EmployeeDetails.EmployeeMasterId
                    WHERE     (dbo.SetupEmployeeMaster.Status = 1)").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetFundDeductionList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select tblFundDeductionSetup.ID as id, SetupEmployeeMaster.EmployeeMasterId, SetupEmployeeMaster.EmployeeFullName, tblNepaliYear.Year as YearName, tblNepaliMonth.MonthName,
                                                                            tblFundDeductionSetup.DeductionAmount as FundAmount, tblFundDeductionSetup.DeductionPercentage as FundPercentage
                                                                             from tblFundDeductionSetup inner join tblNepaliYear
                                                                            on tblNepaliYear.id=tblFundDeductionSetup.YearID inner join
                                                                            tblNepaliMonth on tblNepaliMonth.Id=tblFundDeductionSetup.MonthID inner join
                                                                            SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=tblFundDeductionSetup.EmployeeID
                                                                            where tblFundDeductionSetup.Status=1").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetEmployeeDetails()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMasterModel> model = new List<SetupEmployeeMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmployeeDetail>, IEnumerable<SetupEmployeeMasterModel>>(ent.EmployeeDetails.Where(x => x.Status == true)).ToList();
                return model;
            }
        }

        public List<EmployeeEducationInfoModel> GetEmployeeEducationInfo()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<EmployeeEducationInfoModel> model = new List<EmployeeEducationInfoModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmployeeEducationInfo>, IEnumerable<EmployeeEducationInfoModel>>(ent.EmployeeEducationInfoes.Where(x => x.Status == true)).ToList();
                return model;
            }
        }

        public List<EmployeeTrainingInfoModel> GetEmployeeTrainingInfo()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<EmployeeTrainingInfoModel> model = new List<EmployeeTrainingInfoModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmployeeTrainingInfo>, IEnumerable<EmployeeTrainingInfoModel>>(ent.EmployeeTrainingInfoes.Where(x => x.Status == true)).ToList();
                return model;
            }
        }

        public List<SetupEmployeeMasterModel> GetEmployeePayrollMaster()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMasterModel> model = new List<SetupEmployeeMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmployeePayrollMaster>, IEnumerable<SetupEmployeeMasterModel>>(ent.EmployeePayrollMasters.Where(x => x.Status == true)).ToList();
                return model;
            }
        }

        public List<SetupEmployeeMasterModel> GetEmployeeFundDeductionSetup()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select ID as id, EmployeeID as EmployeeMasterId, YearID, MonthID, DeductionPercentage as FundPercentage, DeductionAmount as FundAmount from tblFundDeductionSetup where Status=1").ToList();
            }
        }

        public List<EmployeeAllowanceSetupModel> GetEmployeeAllowanceInfo()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<EmployeeAllowanceSetupModel>(@"select ID, EmployeeMasterId, AllowanceID, Amount as AllowanceAmount, FundProviderID as AllowanceFundProviderID from EmployeeAllowanceSetup where Status=1").ToList();
            }
        }

        public List<EmployeeDeductionSetupModel> GetEmployeeDeductionInfo()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<EmployeeDeductionSetupModel>(@"select ID, EmployeeMasterId, DeductionID, Amount as DeductionAmount from EmployeeDeductionSetup where Status=1").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetDesignationDetail(int DesignationID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select BasicSalary, Grade as GradeRate, LaganiKoshPercent as CITPercentage, LaganiKoshAmount as CITAmount from dbo.DesignationWiseSalarySetup where DesignationID='" + DesignationID + "'";
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetEmployeeDesignationFromEmpID(int EmployeeID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select DesignationSetup.Designation as DesignationID, SetupEmployeeMaster.GenderID, EmployeePayrollMaster.BasicSalary, EmployeePayrollMaster.GradeAmount
                                     from SetupEmployeeMaster 
                                     inner join EmployeePayrollMaster on SetupEmployeeMaster.EmployeeMasterId=EmployeePayrollMaster.EmployeeMasterId
                                     inner join DesignationSetup on DesignationSetup.ID=SetupEmployeeMaster.DesignationID
                                    where SetupEmployeeMaster.EmployeeMasterId=" + EmployeeID + "";
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetlistBySearchWord(string SearchWord)
        {
            string SearchWordLower = SearchWord.ToLower();
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMasterModel> model = new List<SetupEmployeeMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<SetupEmployeeMaster>, IEnumerable<SetupEmployeeMasterModel>>(ent.SetupEmployeeMasters.Where(x => x.Status == true && x.EmployeeFullName.ToLower().StartsWith(SearchWordLower))).ToList();
                return model;
            }

        }

        public List<SetupEmployeeMasterModel> GetFundDeductionListBySearchWord(string SearchWord)
        {
            string SearchWordLower = SearchWord.ToLower();
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select SetupEmployeeMaster.EmployeeMasterId, SetupEmployeeMaster.EmployeeFullName, tblNepaliYear.Year as YearName, tblNepaliMonth.MonthName,
                                                                            tblFundDeductionSetup.DeductionAmount as FundAmount, tblFundDeductionSetup.DeductionPercentage as FundPercentage
                                                                             from tblFundDeductionSetup inner join tblNepaliYear
                                                                            on tblNepaliYear.id=tblFundDeductionSetup.YearID inner join
                                                                            tblNepaliMonth on tblNepaliMonth.Id=tblFundDeductionSetup.MonthID inner join
                                                                            SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=tblFundDeductionSetup.EmployeeID
                                                                            where tblFundDeductionSetup.Status=1 and SetupEmployeeMaster.EmployeeFullName='" + SearchWord + "'";
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
            }

        }

        private void RemoveAllFunctionsForDepartment(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.SetupEmployeeDepartments.Where(x => x.EmployeeMasterId == EmployeeMasterId);

                foreach (var item in collection)
                {
                    var objToDelete = ent.SetupEmployeeDepartments.Where(x => x.EmployeeDepartmentId == item.EmployeeDepartmentId).FirstOrDefault();
                    ent.SetupEmployeeDepartments.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForShift(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.SetupEmployeeShifts.Where(x => x.EmployeeMasterId == EmployeeMasterId);

                foreach (var item in collection)
                {
                    var objToDelete = ent.SetupEmployeeShifts.Where(x => x.EmployeeShiftId == item.EmployeeShiftId).FirstOrDefault();
                    ent.SetupEmployeeShifts.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForEmployeeDetails(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.EmployeeDetails.Where(x => x.EmployeeMasterId == EmployeeMasterId);

                foreach (var item in collection)
                {
                    var objToDelete = ent.EmployeeDetails.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.EmployeeDetails.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForEducationInfo(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.EmployeeEducationInfoes.Where(x => x.EmployeeMasterId == EmployeeMasterId);

                foreach (var item in collection)
                {
                    var objToDelete = ent.EmployeeEducationInfoes.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.EmployeeEducationInfoes.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForTrainingInfo(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.EmployeeTrainingInfoes.Where(x => x.EmployeeMasterId == EmployeeMasterId);

                foreach (var item in collection)
                {
                    var objToDelete = ent.EmployeeTrainingInfoes.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.EmployeeTrainingInfoes.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForEmployeeAllowanceInfo(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.EmployeeAllowanceSetups.Where(x => x.EmployeeMasterId == EmployeeMasterId);
                foreach (var item in collection)
                {
                    var objToDelete = ent.EmployeeAllowanceSetups.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.EmployeeAllowanceSetups.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void RemoveAllFunctionsForEmployeeDeductionInfo(int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var collection = ent.EmployeeDeductionSetups.Where(x => x.EmployeeMasterId == EmployeeMasterId);
                foreach (var item in collection)
                {
                    var objToDelete = ent.EmployeeDeductionSetups.Where(x => x.ID == item.ID).FirstOrDefault();
                    ent.EmployeeDeductionSetups.Remove(objToDelete);

                }
                ent.SaveChanges();
            }
        }

        private void addFunctionsForDepartment(int DeptId, int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new SetupEmployeeDepartment()
                {
                    DepartmentId = DeptId,
                    EmployeeMasterId = EmployeeMasterId,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    Status = true,
                    CreatedDate = DateTime.Today

                };

                ent.SetupEmployeeDepartments.Add(objToSave);
                ent.SaveChanges();
            }
        }

        private void addFunctionsForshift(int ShifttId, int EmployeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new SetupEmployeeShift()
                {
                    EmployeeShiftMasterId = ShifttId,
                    EmployeeMasterId = EmployeeMasterId,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    Status = true,
                    CreatedDate = DateTime.Today
                };

                ent.SetupEmployeeShifts.Add(objToSave);
                ent.SaveChanges();
            }
        }

        private void SaveDepartmentId(SetupEmployeeMasterModel model)
        {
            RemoveAllFunctionsForDepartment(model.EmployeeMasterId);
            if (model.SelectedDepartmentIDs != null)
            {
                foreach (var item in model.SelectedDepartmentIDs)
                {
                    addFunctionsForDepartment(item, model.EmployeeMasterId);
                }
            }
        }

        private int SaveEmployeeDetails(SetupEmployeeMasterModel model)
        {
            int i = 0;
            RemoveAllFunctionsForEmployeeDetails(model.EmployeeMasterId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new EmployeeDetail()
                {
                    EmployeeMasterId = model.EmployeeMasterId,
                    CitizenshipNo = model.CitizenshipNo,
                    ReleaseDistrict = model.ReleaseDistrict,
                    MaritialStatus = model.MaritialStatus,
                    PanNo = model.PanNo,
                    InsuranceNo = model.InsuranceNo,
                    BloodGroup = model.BloodGroup,
                    GrandFatherName = model.GrandFatherName,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    WifeHusbandName = model.WifeHusbandName,
                    NomineeName = model.NomineeName,
                    NomineeAddress = model.NomineeAddress,
                    NomineeRelation = model.NomineeRelation,
                    NomineePhoneNo = model.NomineePhoneNo,

                    Status = true,
                    BranchId = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today,

                };
                //ent.EmployeeDetails.Add(objToSave);
                //i = ent.SaveChanges();
                return i;
            }
        }

        public int SaveEmployeeEducationDetail(SetupEmployeeMasterModel model)
        {
            int i = 0;
            RemoveAllFunctionsForEducationInfo(model.EmployeeMasterId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.EmployeeEducationInfoList != null)
                {
                    foreach (var item in model.EmployeeEducationInfoList)
                    {
                        var objToSave = new EmployeeEducationInfo()
                        {
                            EmployeeMasterId = model.EmployeeMasterId,
                            LevelID = item.LevelID,
                            UniversityName = item.UniversityName,
                            Grade = item.Grade,
                            Percentage = item.Percentage,
                            Status = true,
                            BranchId = 1,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Today
                        };
                        ent.EmployeeEducationInfoes.Add(objToSave);
                        i = ent.SaveChanges();
                    }
                }
            }
            return i;
        }

        public void SaveEmployeeTrainingDetails(SetupEmployeeMasterModel model)
        {
            RemoveAllFunctionsForTrainingInfo(model.EmployeeMasterId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.EmployeeTrainingInfoList != null)
                {
                    foreach (var item in model.EmployeeTrainingInfoList)
                    {
                        var objToSave = new EmployeeTrainingInfo()
                        {
                            EmployeeMasterId = model.EmployeeMasterId,
                            Course = item.Course,
                            TrainingCenter = item.TrainingCenter,
                            TGrade = item.TGrade,
                            Duration = item.Duration,
                            Status = true,
                            BranchId = 1,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Today
                        };
                        ent.EmployeeTrainingInfoes.Add(objToSave);
                        ent.SaveChanges();
                    }
                }
            }
        }

        public int SaveEmployeeAllowanceDetail(SetupEmployeeMasterModel model)
        {
            int i = 0;
            RemoveAllFunctionsForEmployeeAllowanceInfo(model.EmployeeMasterId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.EmployeeAllowanceSetupModelList != null)
                {
                    foreach (var item in model.EmployeeAllowanceSetupModelList)
                    {
                        var objToSave = new EmployeeAllowanceSetup()
                        {
                            EmployeeMasterId = model.EmployeeMasterId,
                            AllowanceID = item.AllowanceID,
                            Amount = item.AllowanceAmount,
                            Status = true,
                            BranchId = 1,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Today
                        };
                        ent.EmployeeAllowanceSetups.Add(objToSave);
                        i = ent.SaveChanges();
                    }
                }
            }
            return i;
        }

        public void SaveEmployeeDeductionDetails(SetupEmployeeMasterModel model)
        {
            RemoveAllFunctionsForEmployeeDeductionInfo(model.EmployeeMasterId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.EmployeeDeductionSetupModelList != null)
                {
                    foreach (var item in model.EmployeeDeductionSetupModelList)
                    {
                        var objToSave = new EmployeeDeductionSetup()
                        {
                            EmployeeMasterId = model.EmployeeMasterId,
                            DeductionID = item.DeductionID,
                            Amount = item.DeductionAmount,
                            Status = true,
                            BranchId = 1,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Today
                        };
                        ent.EmployeeDeductionSetups.Add(objToSave);
                        ent.SaveChanges();
                    }
                }
            }
        }

        private void SaveShiftId(SetupEmployeeMasterModel model)
        {
            RemoveAllFunctionsForShift(model.EmployeeMasterId);
            if (model.SelectedDepartmentIDs != null)
            {
                foreach (var item in model.SelectedShiftIDs)
                    addFunctionsForshift(item, model.EmployeeMasterId);

            }
        }

        public int Update(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == model.EmployeeMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.CreatedBy = Utility.GetCurrentLoginUserId(); ;
                objToEdit.Status = true;
                objToEdit.CreatedDate = DateTime.Today;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
                SaveDepartmentId(model);
                SaveShiftId(model);
                SaveEmployeeDetails(model);
                SaveEmployeeEducationDetail(model);
                SaveEmployeeTrainingDetails(model);
            }
            return i;
        }

        public int Delete(int EmployeeMasterId)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == EmployeeMasterId).Any())
                {


                    var updateQuery4 = ent.EmployeeDetails.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery4)
                    {
                        var objToDelete = ent.EmployeeDetails.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeeDetails.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery5 = ent.EmployeeEducationInfoes.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery5)
                    {
                        var objToDelete = ent.EmployeeEducationInfoes.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeeEducationInfoes.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery6 = ent.EmployeeTrainingInfoes.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery6)
                    {
                        var objToDelete = ent.EmployeeTrainingInfoes.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeeTrainingInfoes.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery1 = ent.EmployeePayrollMasters.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery1)
                    {
                        var objToDelete = ent.EmployeePayrollMasters.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeePayrollMasters.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery2 = ent.EmployeeAllowanceSetups.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery2)
                    {
                        var objToDelete = ent.EmployeeAllowanceSetups.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeeAllowanceSetups.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery3 = ent.EmployeeDeductionSetups.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery3)
                    {
                        var objToDelete = ent.EmployeeDeductionSetups.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.EmployeeDeductionSetups.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery7 = ent.SetupEmployeeDepartments.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery7)
                    {
                        var objToDelete = ent.SetupEmployeeDepartments.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.SetupEmployeeDepartments.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery8 = ent.SetupEmployeeShifts.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery7)
                    {
                        var objToDelete = ent.SetupEmployeeShifts.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.SetupEmployeeShifts.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var collection = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in collection)
                    {
                        var objToDelete = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == item.SPAccountHeadID).FirstOrDefault();
                        if (item.SPAccountHeadID != 0)
                            ent.GL_AccSubGroups.Remove(objToDelete);
                        ent.SaveChanges();
                    }

                    var updateQuery = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == EmployeeMasterId).ToList();
                    foreach (var item in updateQuery)
                    {
                        var objToDelete = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == item.EmployeeMasterId).FirstOrDefault();
                        ent.SetupEmployeeMasters.Remove(objToDelete);
                        i = ent.SaveChanges();
                    }

                }
            }
            return i;
        }

        public int DeleteFundDeduction(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.tblFundDeductionSetups.Where(x => x.ID == id).Any())
                {
                    var updateQuery = ent.tblFundDeductionSetups.Where(x => x.ID == id).FirstOrDefault();
                    updateQuery.Status = false;
                    i = ent.SaveChanges();
                }
            }
            return i;
        }

        public decimal GetEmployeeAdvanceAmount(int EmpID)
        {
            decimal AdvanceAmount = 0;
            DateTime EndDate = DateTime.Now;
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNepaliDate(GETDATE())TodayNepaliDate").FirstOrDefault().TodayNepaliDate;
                    string year = date.Substring(0, 4);
                    string month = date.Substring(5, 2);
                    var Day = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNoOfDaysInMonth('" + year + "','" + month + "')NoOfDays").FirstOrDefault().NoOfDays;
                    EndDate = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetEnglishDate('" + year + "','" + month + "','" + Day + "')CreatedDate").FirstOrDefault().CreatedDate;
                }
                catch
                {
                    EndDate = DateTime.Now;
                }
                string sql = @"select (isnull(OpeningBalance.DrAmount,0)-isnull(OpeningBalance.CrAmount,0)+isnull(JVDetails.DrAmount,0)-isnull(JVDetails.CrAmount,0))*-1 as Advance from JVDetails left join
                                    OpeningBalance on OpeningBalance.AccountHeadId=JVDetails.FeeTypeSubId left join
                                    SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=JVDetails.FeeTypeSubId inner join
                                    JVMaster on JVMaster.JvMasterId=JVDetails.JVMasterId
                                where JVDetails.FeeTypeSubId='" + EmpID + "' and JVMaster.TransactionDate<='" + EndDate + "'";
                var data = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
                foreach (var item in data)
                {
                    AdvanceAmount = item.AdvanceDeduction;
                }
            }

            return AdvanceAmount;
        }

        public decimal GetEmployeeFundDeductAmount(int EmpID)
        {
            decimal FundAmount = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {
                var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNepaliDate(GETDATE())TodayNepaliDate").FirstOrDefault().TodayNepaliDate;
                string year = date.Substring(0, 4);
                string month = date.Substring(5, 2);
                string sql = @"select isnull(DeductionAmount,0) as FundAmount  from tblFundDeductionSetup where MonthID='" + month + "' and YearID='" + year + "' and EmployeeID='" + EmpID + "' and Status=1";
                var data = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
                foreach (var item in data)
                {
                    FundAmount = item.FundAmount;
                }
            }

            return FundAmount;
        }

        public List<SetupEmployeeMasterModel> GetTaxRangeDetail(string Married)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMasterModel> model = new List<SetupEmployeeMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<TaxRangeSetup>, IEnumerable<SetupEmployeeMasterModel>>(ent.TaxRangeSetups.Where(x => x.IsMarried == Married)).ToList();
                return model;
            }
        }

        public List<SetupEmployeeMasterModel> GetSalarySheet(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select SetupEmployeeMaster.EmployeeFullName, SetupBank.BankName, BankAccountNo, isnull(BasicSalary,0)BasicSalary, isnull(GradeAmount,0)GradeAmount, 
                                        isnull(PFAmount,0)PFAmount, isnull(TotalAllowance,0)TotalAllowance, isnull(InsuranceAmount,0)InsuranceAmount,isnull(FestivalAmount,0)FestivalAmount,isnull(DressAllowance,0)DressAllowance,isnull(DearnessAllowance,0)DearnessAllowance,
                                        isnull(OverTimeAmount,0)OverTimeAmount, 
                                        (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))TotalIncome,
                                        isnull(CITAmount,0)CITAmount, isnull(FundAmount,0)FundAmount, isnull(LoanDeduction,0)LoanDeduction, isnull(LoanInterest,0)LoanInterest,
                                        isnull(AdvanceDeduction,0) AdvanceDeduction, isnull(PFDeductAmount,0)PFDeductAmount, isnull(InsuranceDeductAmount,0)InsuranceDeductAmount, 
                                        isnull(LeaveDeduction,0)LeaveDeduction, isnull(TotalDeduction,0)TotalDeduction, 
                                        (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))
                                        -
                                        (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+isnull(AdvanceDeduction,0)+
                                         isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ isnull(LeaveDeduction,0)+isnull(TotalDeduction,0))TotalTaxableAmount,
                                        cast((isnull(EmpInsurancePaid,0)/12)as decimal(18,2))EmpInsurancePaid,cast((isnull(RemoteAreaDeduction,0)/12) as decimal(18,2))RemoteAreaDeduction,cast((isnull(PhysicalDisability,0)/12) as decimal(18,2))PhysicalDisability,
                                        ((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))
                                        -
                                        (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+isnull(AdvanceDeduction,0)+
                                         isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ isnull(LeaveDeduction,0)+isnull(TotalDeduction,0))-(cast((isnull(EmpInsurancePaid,0)/12)as decimal(18,2))+cast((isnull(RemoteAreaDeduction,0)/12) as decimal(18,2))+cast((isnull(PhysicalDisability,0)/12) as decimal(18,2)))) NetTaxableAmount,
                                        isnull(SSTAmount,0)SSTAmount, isnull(TaxAmount,0)TaxAmount,
                                        (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                        ( isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                        isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                        isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                        isnull(SSTAmount,0)+isnull(TaxAmount,0))SalaryPayable
                                        from PayrollMaster inner join
                                        SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID inner join
                                        SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                        where PayrollMaster.Status=1 and PayrollMaster.MonthID='" + monthid + "' and PayrollMaster.YearID='" + Yearid + "' ").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GeneratePayroll(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select SetupEmployeeMaster.EmployeeFullName, SetupBank.BankName, BankAccountNo, isnull(BasicSalary,0)BasicSalary, isnull(GradeAmount,0)GradeAmount, 
                                        isnull(PFAmount,0)PFAmount, isnull(TotalAllowance,0)TotalAllowance, isnull(InsuranceAmount,0)InsuranceAmount,isnull(FestivalAmount,0)FestivalAmount,isnull(DressAllowance,0)DressAllowance,isnull(DearnessAllowance,0)DearnessAllowance,
                                        isnull(OverTimeAmount,0)OverTimeAmount, 
                                        (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))TotalDebit,
                                        isnull(CITAmount,0)CITAmount, isnull(FundAmount,0)FundAmount, isnull(LoanDeduction,0)LoanDeduction, isnull(LoanInterest,0)LoanInterest,
                                        isnull(AdvanceDeduction,0) AdvanceDeduction, isnull(PFDeductAmount,0)PFDeductAmount, isnull(InsuranceDeductAmount,0)InsuranceDeductAmount, 
                                        isnull(LeaveDeduction,0)LeaveDeduction, isnull(TotalDeduction,0)TotalDeduction,                                         
                                        isnull(SSTAmount,0)SSTAmount, isnull(TaxAmount,0)TaxAmount,
                                         (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                        ( isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                        isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                        isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                        isnull(SSTAmount,0)+isnull(TaxAmount,0))SalaryPayable,
                                        ((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                        ( isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                        isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                        isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                        isnull(SSTAmount,0)+isnull(TaxAmount,0)))+( isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                        isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                        isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                        isnull(SSTAmount,0)+isnull(TaxAmount,0))TotalCredit, isnull(SetupEmployeeMaster.AccountHeadID,0) as AccountHeadID
                                        from PayrollMaster inner join
                                        SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID inner join
                                        SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                        where PayrollMaster.Status=1 and PayrollMaster.MonthID='" + monthid + "' and PayrollMaster.YearID='" + Yearid + "' and PayrollMaster.IsPosted=0").ToList();
            }
        }

        public int CreateNewJv(SetupEmployeeMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int fiscalyearid = Utility.GetCurrentFiscalYearID();
                int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber("JV", fiscalyearid);
                string VoucherNumber = "JV" + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();
                var JVMasterObj = new JVMaster()
                {
                    AccountNumber = "ac001",
                    BillNumber = "BN-001",
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    FiscalYearId = Utility.GetCurrentFiscalYearID(),
                    JvNumber = VoucherNumber,
                    Narration1 = model.ObjAddMoreParticularsEmployeeModel.Narration,
                    Narration2 = model.ObjAddMoreParticularsEmployeeModel.Narration,
                    Status = false,
                    JvType = "JV",
                    TotalAmount = model.ObjAddMoreParticularsEmployeeModel.CrAmountTotal,
                    TransactionDate = DateTime.Now,
                    VerifiedBy = Utility.GetCurrentLoginUserId(),
                    FormName = "Payroll"
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

                model.EmployeeAllowanceSetupModelList = GetAllowanceDetail(model.MonthId, model.YearId);
                foreach (var adv in model.EmployeeAllowanceSetupModelList)
                {
                    var jvdetailObj = new JVDetail()
                    {
                        BillNumber = "BL-001",
                        CrAmount = 0,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DrAmount = adv.AllowanceAmount,
                        FeeTypeId = adv.AllowanceID,
                        FeeTypeSubId = 0,
                        FeeTypeName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(adv.AllowanceID),
                        JVMasterId = JVMasterObj.JvMasterId,
                        LinkToDr = 1,
                        Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                        PaymentType = "Cash",
                        Status = false,
                        TransactionDate = DateTime.Now,
                        DrOrCr = "Dr"
                    };
                    ent.JVDetails.Add(jvdetailObj);
                }
                model.EmployeeDeductionSetupModelList = GetDeductionDetail(model.MonthId, model.YearId);
                foreach (var adv in model.EmployeeDeductionSetupModelList)
                {
                    var jvdetailObj = new JVDetail()
                    {
                        BillNumber = "BL-001",
                        CrAmount = adv.DeductionAmount,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DrAmount = 0,
                        FeeTypeId = adv.DeductionID,
                        FeeTypeSubId = 0,
                        FeeTypeName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(adv.DeductionID),
                        JVMasterId = JVMasterObj.JvMasterId,
                        LinkToDr = 1,
                        Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                        PaymentType = "Cash",
                        Status = false,
                        TransactionDate = DateTime.Now,
                        DrOrCr = "Cr"
                    };
                    ent.JVDetails.Add(jvdetailObj);
                }
                foreach (var item in model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList)
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
                    int AdvID = Utility.GetAccHeadIDFromDescription("Staff Advance");
                    int SalaryPayID = Utility.GetAccHeadIDFromDescription("Salary Payable");
                    if (AdvID == item.Particulars)
                    {
                        model.SetupEmployeeMasterModelList = GetAdvanceDetail(model.MonthId, model.YearId);
                        foreach (var adv in model.SetupEmployeeMasterModelList)
                        {
                            item.SubParticulars = adv.AdvanceID;
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
                                CrAmount = adv.AdvanceDeduction,
                                CreatedBy = Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                DrAmount = model.AdvanceDeduction,
                                FeeTypeId = item.Particulars,
                                FeeTypeSubId = item.SubParticulars,
                                FeeTypeName = AccountHeadName,
                                JVMasterId = JVMasterObj.JvMasterId,
                                LinkToDr = 1,
                                Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                                PaymentType = "Cash",
                                Status = false,
                                TransactionDate = DateTime.Now,
                                DrOrCr = DrOrCrValue
                            };
                            ent.JVDetails.Add(jvdetailObj);
                        }
                    }
                    else if (SalaryPayID == item.Particulars)
                    {
                        model.SetupEmployeeMasterModelList = GetSalaryPayableDetail(model.MonthId, model.YearId);
                        foreach (var adv in model.SetupEmployeeMasterModelList)
                        {
                            item.SubParticulars = adv.AccountHeadID;
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
                                CrAmount = adv.SalaryPayable,
                                CreatedBy = Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                DrAmount = model.AdvanceDeduction,
                                FeeTypeId = item.Particulars,
                                FeeTypeSubId = item.SubParticulars,
                                FeeTypeName = AccountHeadName,
                                JVMasterId = JVMasterObj.JvMasterId,
                                LinkToDr = 1,
                                Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                                PaymentType = "Cash",
                                Status = false,
                                TransactionDate = DateTime.Now,
                                DrOrCr = DrOrCrValue
                            };
                            ent.JVDetails.Add(jvdetailObj);
                        }
                    }
                    else if (item.Particulars == 1)
                    {
                        model.SetupEmployeeMasterModelList = GetBankPayableDetail(model.MonthId, model.YearId);
                        foreach (var item1 in model.SetupEmployeeMasterModelList)
                        {
                            if (item1.ParentID > 0)
                            {
                                AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item1.ParentID) + "-" + HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item1.BankID);
                            }
                            else
                            {
                                AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item1.BankID);
                            }
                            var jvdetailObj = new JVDetail()
                            {
                                BillNumber = "BL-001",
                                CrAmount = item1.SalaryPayable,
                                CreatedBy = Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                DrAmount = 0,
                                FeeTypeId = item1.ParentID,
                                FeeTypeSubId = item1.BankID,
                                FeeTypeName = AccountHeadName,
                                JVMasterId = JVMasterObj.JvMasterId,
                                LinkToDr = 1,
                                Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                                PaymentType = "Bank",
                                Status = false,
                                TransactionDate = DateTime.Now,
                                DrOrCr = DrOrCrValue
                            };
                            ent.JVDetails.Add(jvdetailObj);
                        }
                        var PayMaster = (from x in ent.PayrollMasters
                                         where x.MonthID == model.MonthId && x.YearID == model.YearId
                                         select x);
                        foreach (PayrollMaster PM in PayMaster)
                        {
                            PM.IsPaid = true;
                        }
                    }
                    else
                    {
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
                            Narration = model.ObjAddMoreParticularsEmployeeModel.Narration,
                            PaymentType = "Cash",
                            Status = false,
                            TransactionDate = DateTime.Now,
                            DrOrCr = DrOrCrValue
                        };
                        ent.JVDetails.Add(jvdetailObj);
                    }
                }
                SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                    where x.JvType == "JV" && x.FiscalYear == fiscalyearid
                                                    select x).First();
                vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;
                var PMaster = (from x in ent.PayrollMasters
                               where x.MonthID == model.MonthId && x.YearID == model.YearId
                               select x);
                foreach (PayrollMaster paymaster in PMaster)
                {
                    paymaster.IsPosted = true;
                }
                var MonthlyAllow = (from x in ent.MonthlyAllowances
                                    where x.MonthID == model.MonthId && x.YearID == model.YearId
                                    select x);
                foreach (MonthlyAllowance MAllow in MonthlyAllow)
                {
                    MAllow.IsPosted = true;
                }
                var MonthlyDeduct = (from x in ent.MonthlyDeductions
                                     where x.MonthID == model.MonthId && x.YearID == model.YearId
                                     select x);
                foreach (MonthlyDeduction MDeduct in MonthlyDeduct)
                {
                    MDeduct.IsPosted = true;
                }
                ent.SaveChanges();
                return ent.JVMasters.Max(item => item.JvMasterId);
            }
        }

        public List<SetupEmployeeMasterModel> GetAdvanceDetail(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select isnull(AdvanceDeduction,0) AdvanceDeduction, isnull(SetupEmployeeMaster.AccountHeadID,0) as AdvanceID
                                                                        from PayrollMaster inner join
                                                                        SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID                                        
                                                                        where PayrollMaster.Status=1 and PayrollMaster.MonthID='" + monthid + "' and PayrollMaster.YearID='" + Yearid + "' and AdvanceDeduction!=0").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetSalaryPayableDetail(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select (isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                                                        (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                                                        isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                                                        isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                                                        isnull(SSTAmount,0)+isnull(TaxAmount,0))SalaryPayable,
                                                                        isnull(SetupEmployeeMaster.SPAccountHeadID,0) as AccountHeadID
                                                                            from PayrollMaster inner join
                                                                            SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID                                        
                                                                            where PayrollMaster.Status=1 and PayrollMaster.MonthID='" + monthid + "' and PayrollMaster.YearID='" + Yearid + "'").ToList();
            }
        }

        public List<SetupEmployeeMasterModel> GetBankPayableDetail(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(@"select sum((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                                                                (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                                                                isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                                                                isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                                                                isnull(SSTAmount,0)+isnull(TaxAmount,0)))SalaryPayable,                                                                                
                                                                                GL_AccSubGroups.ParentID, SetupBank.AccountHeadID as BankID
                                                                          from PayrollMaster
                                                                        inner join SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                                                        inner join SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID
                                                                        inner join GL_AccSubGroups  on GL_AccSubGroups.AccSubGruupID=SetupBank.AccountHeadID
                                                                       where PayrollMaster.IsPosted=0 and MonthID='" + monthid + "' and YearID='" + Yearid + "' and IsPaid=0 and PayrollMaster.Status=1 group by GL_AccSubGroups.ParentID, SetupBank.AccountHeadID").ToList();
            }
        }

        public List<EmployeeAllowanceSetupModel> GetAllowanceDetail(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<EmployeeAllowanceSetupModel>(@" select AllowanceID, ISNULL(sum(Amount),0)AllowanceAmount from MonthlyAllowance where MonthID='" + monthid + "' and YearID='" + Yearid + "' group by AllowanceID").ToList();
            }
        }

        public List<EmployeeDeductionSetupModel> GetDeductionDetail(int monthid, int Yearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<EmployeeDeductionSetupModel>(@" select DeductionID, ISNULL(sum(Amount),0)DeductionAmount from MonthlyDeduction where MonthID='" + monthid + "' and YearID='" + Yearid + "' group by DeductionID").ToList();
            }
        }

        public static IEnumerable<SelectListItem> GetEmployeeType()
        {
            return new SelectList(new[]
            {
                new {Id="FullTime",Value="FullTime"},
                new {Id="PartTime",Value="PartTime"}
            }, "Id", "Value");
        }

        public int InsertResignationDetail(SetupEmployeeMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = new ResignationDetail()
                {
                    EmployeeID = model.EmployeeMasterId,
                    ResignationDate = model.ResignationDate,
                    ReasonForResignation = model.ReasonForResignation,
                    ApprovedBy = model.ApprovedBy,
                    ApprovedDate = model.ApprovedDate,
                    Status = true,
                    BranchID = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today
                };
                ent.ResignationDetails.Add(objToSave);
                i = ent.SaveChanges();
                return i;
            }
        }

        public List<SetupEmployeeMasterModel> GetDetailOfSalaryPayable(int Yearid, int monthid, int EmployeeID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = "";
                if (Yearid == 0 && monthid == 0 && EmployeeID == 0)
                {
                    sql = @"select SetupEmployeeMaster.EmployeeMasterId, SetupEmployeeMaster.EmployeeFullName,SetupBank.BankName, tblNepaliYear.Year, tblNepaliMonth.MonthName, sum((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                                                                (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                                                                isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                                                                isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                                                                isnull(SSTAmount,0)+isnull(TaxAmount,0)))SalaryPayable,GL_AccSubGroups.ParentID,
                                                                                isnull(SetupEmployeeMaster.SPAccountHeadID,0) as AccountHeadID,
                                                                            GL_AccSubGroups1.ParentID as BankAccHeadID, SetupBank.AccountHeadID as BankSubAccHeadID, PayrollMaster.YearID as YearId, PayrollMaster.MonthID as MonthId
                                                                            from PayrollMaster
                                                                            inner join SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                                                            inner join SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID
                                                                            inner join GL_AccSubGroups on GL_AccSubGroups.AccSubGruupID=SetupEmployeeMaster.SPAccountHeadID
                                                                            inner join GL_AccSubGroups as GL_AccSubGroups1 on GL_AccSubGroups1.AccSubGruupID=SetupBank.AccountHeadID
                                                                            where isnull(IsPaid,0)=0 and IsPosted=1 and PayrollMaster.Status=1
                                                                            group by SetupEmployeeMaster.SPAccountHeadID, tblNepaliYear.Year, tblNepaliMonth.MonthName, SetupEmployeeMaster.EmployeeFullName,GL_AccSubGroups.ParentID,
                                                                            GL_AccSubGroups1.ParentID,SetupBank.AccountHeadID,SetupBank.BankName, SetupEmployeeMaster.EmployeeMasterId, PayrollMaster.YearID, PayrollMaster.MonthID";
                }
                else if (Yearid == 0 && monthid == 0 && EmployeeID != 0)
                {
                    sql = @"select SetupEmployeeMaster.EmployeeMasterId, SetupEmployeeMaster.EmployeeFullName,SetupBank.BankName, tblNepaliYear.Year, tblNepaliMonth.MonthName, sum((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                                                                (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                                                                isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                                                                isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                                                                isnull(SSTAmount,0)+isnull(TaxAmount,0)))SalaryPayable,GL_AccSubGroups.ParentID,
                                                                                isnull(SetupEmployeeMaster.SPAccountHeadID,0) as AccountHeadID,
                                                                            GL_AccSubGroups1.ParentID as BankAccHeadID, SetupBank.AccountHeadID as BankSubAccHeadID, PayrollMaster.YearID as YearId, PayrollMaster.MonthID as MonthId
                                                                            from PayrollMaster
                                                                            inner join SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                                                            inner join SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID
                                                                            inner join GL_AccSubGroups on GL_AccSubGroups.AccSubGruupID=SetupEmployeeMaster.SPAccountHeadID
                                                                            inner join GL_AccSubGroups as GL_AccSubGroups1 on GL_AccSubGroups1.AccSubGruupID=SetupBank.AccountHeadID
                                                                            where MonthID='" + monthid + @"' and YearID='" + Yearid + @"' and SetupEmployeeMaster.EmployeeMasterId='" + EmployeeID + @"' and isnull(IsPaid,0)=0 and IsPosted=1 and PayrollMaster.Status=1
                                                                            group by SetupEmployeeMaster.SPAccountHeadID, tblNepaliYear.Year, tblNepaliMonth.MonthName, SetupEmployeeMaster.EmployeeFullName,GL_AccSubGroups.ParentID,
                                                                            GL_AccSubGroups1.ParentID,SetupBank.AccountHeadID,SetupBank.BankName, SetupEmployeeMaster.EmployeeMasterId, PayrollMaster.YearID, PayrollMaster.MonthID";
                }
                else if (Yearid != 0 && monthid != 0 && EmployeeID == 0)
                {
                    sql = @"select SetupEmployeeMaster.EmployeeMasterId, SetupEmployeeMaster.EmployeeFullName,SetupBank.BankName, tblNepaliYear.Year, tblNepaliMonth.MonthName, sum((isnull(BasicSalary,0)+ ISNULL(GradeAmount,0)+ isnull(PFAmount,0)+ isnull(TotalAllowance,0) + isnull(OverTimeAmount,0) + isnull(InsuranceAmount,0)+isnull(FestivalAmount,0)+isnull(DressAllowance,0)+isnull(DearnessAllowance,0))-
                                                                                (isnull(CITAmount,0)+ isnull(FundAmount,0)+ isnull(LoanDeduction,0)+ isnull(LoanInterest,0)+
                                                                                isnull(AdvanceDeduction,0) + isnull(PFDeductAmount,0)+ isnull(InsuranceDeductAmount,0)+ 
                                                                                isnull(LeaveDeduction,0)+ isnull(TotalDeduction,0)+
                                                                                isnull(SSTAmount,0)+isnull(TaxAmount,0)))SalaryPayable,GL_AccSubGroups.ParentID,
                                                                                isnull(SetupEmployeeMaster.SPAccountHeadID,0) as AccountHeadID,
                                                                            GL_AccSubGroups1.ParentID as BankAccHeadID, SetupBank.AccountHeadID as BankSubAccHeadID, PayrollMaster.YearID as YearId, PayrollMaster.MonthID as MonthId
                                                                            from PayrollMaster
                                                                            inner join SetupBank on SetupBank.BankSetupId=PayrollMaster.BankID
                                                                            inner join SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=PayrollMaster.EmployeeID
                                                                            inner join GL_AccSubGroups on GL_AccSubGroups.AccSubGruupID=SetupEmployeeMaster.SPAccountHeadID
                                                                            inner join GL_AccSubGroups as GL_AccSubGroups1 on GL_AccSubGroups1.AccSubGruupID=SetupBank.AccountHeadID
                                                                            where MonthID='" + monthid + @"' and YearID='" + Yearid + @"' and isnull(IsPaid,0)=0 and IsPosted=1 and PayrollMaster.Status=1
                                                                            group by SetupEmployeeMaster.SPAccountHeadID, tblNepaliYear.Year, tblNepaliMonth.MonthName, SetupEmployeeMaster.EmployeeFullName,GL_AccSubGroups.ParentID,
                                                                            GL_AccSubGroups1.ParentID,SetupBank.AccountHeadID,SetupBank.BankName, SetupEmployeeMaster.EmployeeMasterId, PayrollMaster.YearID, PayrollMaster.MonthID";
                }
                return ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
            }
        }

        public int CreateSalaryPaidJv(SetupEmployeeMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int fiscalyearid = Utility.GetCurrentFiscalYearID();
                int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber("JV", fiscalyearid);
                string VoucherNumber = "JV" + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();
                decimal TotalAmount = 0;
                foreach (var item in model.SetupEmployeeMasterModelList)
                {
                    TotalAmount += item.SalaryPayable;
                }
                var JVMasterObj = new JVMaster()
                {
                    AccountNumber = "ac001",
                    BillNumber = "BN-001",
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    FiscalYearId = Utility.GetCurrentFiscalYearID(),
                    JvNumber = VoucherNumber,
                    Narration1 = "Auto Voucher of Salary Pay",
                    Narration2 = "Auto Voucher of Salary Pay",
                    Status = false,
                    JvType = "JV",
                    TotalAmount = TotalAmount,
                    TransactionDate = DateTime.Now,
                    VerifiedBy = Utility.GetCurrentLoginUserId(),
                    FormName = "Payroll"
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

                foreach (var item in model.SetupEmployeeMasterModelList)
                {
                    string AccountHeadName = "";
                    if (item.AccountHeadID > 0)
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.ParentID) + "-" + HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.AccountHeadID);
                    }
                    else
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.ParentID);
                    }
                    var jvdetailObj = new JVDetail()
                    {
                        BillNumber = "BL-001",
                        CrAmount = 0,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DrAmount = item.SalaryPayable,
                        FeeTypeId = item.ParentID,
                        FeeTypeSubId = item.AccountHeadID,
                        FeeTypeName = AccountHeadName,
                        JVMasterId = JVMasterObj.JvMasterId,
                        LinkToDr = 1,
                        Narration = "Auto Voucher of Salary Pay",
                        PaymentType = "Cash",
                        Status = false,
                        TransactionDate = DateTime.Now,
                        DrOrCr = "Dr"
                    };
                    ent.JVDetails.Add(jvdetailObj);


                    if (item.BankSubAccHeadID > 0)
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.BankAccHeadID) + "-" + HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.BankSubAccHeadID);
                    }
                    else
                    {
                        AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.BankAccHeadID);
                    }
                    var jvdetailObj1 = new JVDetail()
                    {
                        BillNumber = "BL-001",
                        CrAmount = item.SalaryPayable,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Now,
                        DrAmount = 0,
                        FeeTypeId = item.BankAccHeadID,
                        FeeTypeSubId = item.BankSubAccHeadID,
                        FeeTypeName = AccountHeadName,
                        JVMasterId = JVMasterObj.JvMasterId,
                        LinkToDr = 1,
                        Narration = "Auto Voucher of Salary Pay",
                        PaymentType = "Cash",
                        Status = false,
                        TransactionDate = DateTime.Now,
                        DrOrCr = "Cr"
                    };
                    ent.JVDetails.Add(jvdetailObj1);

                    var PMaster = (from x in ent.PayrollMasters
                                   where x.MonthID == item.MonthId && x.YearID == item.YearId && x.EmployeeID == item.EmployeeMasterId
                                   select x);
                    foreach (PayrollMaster paymaster in PMaster)
                    {
                        paymaster.IsPaid = true;
                    }
                }
                SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                    where x.JvType == "JV" && x.FiscalYear == fiscalyearid
                                                    select x).First();
                vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;

                ent.SaveChanges();
                return ent.JVMasters.Max(item => item.JvMasterId);
            }
        }

    }
}