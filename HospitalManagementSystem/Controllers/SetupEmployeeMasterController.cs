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
    public class SetupEmployeeMasterController : Controller
    {
        public ActionResult Create()
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();

            if (ModelState.IsValid)
            {
                int i = SEMPro.Insert(model);
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
            return View();
        }

        public ActionResult Index(string searchString, string message)
        {
            string searchWords = searchString;
            if (message != null)
            {
                ViewBag.SaruMessage = message;
            }
            else
            {
                ViewBag.SaruMessage = "";
            }
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            model.SetupEmployeeMasterModelList = SEMPro.GetList();
            if (!String.IsNullOrEmpty(searchWords))
            {
                model.SetupEmployeeMasterModelList = SEMPro.GetlistBySearchWord(searchWords);
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            model = SEMPro.GetList().Where(x => x.EmployeeMasterId == id).FirstOrDefault();
            model.EmployeeEducationInfoList = SEMPro.GetEmployeeEducationInfo().Where(x => x.EmployeeMasterId == id).ToList();
            model.EmployeeTrainingInfoList = SEMPro.GetEmployeeTrainingInfo().Where(x => x.EmployeeMasterId == id).ToList();
            model.EmployeeMasterId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            if (ModelState.IsValid)
            {
                int i = SEMPro.Update(model);
                if (i != 0)
                {
                    TempData["success"] = UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult EditEmployeePayrollMaster(int id)
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            if (GetPayrollCount(id) == true)
            {
                return RedirectToAction("Index", new { string.Empty, message = "Employee Payroll Not Created. Please Create Payroll First!!" });
            }
            model = SEMPro.GetEmployeePayrollMaster().Where(x => x.EmployeeMasterId == id).FirstOrDefault();
            model.EmployeeAllowanceSetupModelList = SEMPro.GetEmployeeAllowanceInfo().Where(x => x.EmployeeMasterId == id).ToList();
            model.EmployeeDeductionSetupModelList = SEMPro.GetEmployeeDeductionInfo().Where(x => x.EmployeeMasterId == id).ToList();
            model.EmployeeMasterId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployeePayrollMaster(int id, SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.UpdatePayroll(model);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.edit;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult GenerateSalarySheet()
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateSalarySheet(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SProv = new SetupEmployeeMasterProviders();
            if (model.EmployeeMasterId == 0)
            {
                model.SetupEmployeeMasterModelList = SProv.GetEmployeePayrollMaster().ToList();
                model.EmployeeAllowanceSetupModelList = SProv.GetEmployeeAllowanceInfo().ToList();
                model.EmployeeDeductionSetupModelList = SProv.GetEmployeeDeductionInfo().ToList();
            }
            else
            {
                model.SetupEmployeeMasterModelList = SProv.GetEmployeePayrollMaster().Where(x => x.EmployeeMasterId == model.EmployeeMasterId).ToList();
                model.EmployeeAllowanceSetupModelList = SProv.GetEmployeeAllowanceInfo().Where(x => x.EmployeeMasterId == model.EmployeeMasterId).ToList();
                model.EmployeeDeductionSetupModelList = SProv.GetEmployeeDeductionInfo().Where(x => x.EmployeeMasterId == model.EmployeeMasterId).ToList();
            }
            int i = SProv.GeneratePayroll(model);
            if (i != 0)
            {
                return PartialView("_ViewSalarySheet", model);
            }
            return View(model);
        }

        public ActionResult BankSalaryReport()
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BankSalaryReport(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SProv = new SetupEmployeeMasterProviders();
            return PartialView("_BankSalaryReport", model);
        }

        public ActionResult Delete(int id)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.Delete(id);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.delete;

            }
            else
            {
                TempData["success"] = UtilityMessage.deletefailed;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult _EmployeeEducationInfo()
        {
            EmployeeEducationInfoModel model = new EmployeeEducationInfoModel();
            return PartialView("_EmployeeEducationInfo", model);
        }

        [HttpPost]
        public ActionResult _EmployeeTrainingInfo()
        {
            EmployeeTrainingInfoModel model = new EmployeeTrainingInfoModel();
            return PartialView("_EmployeeTrainingInfo", model);
        }

        [HttpPost]
        public ActionResult _EmployeeAllowanceSetup()
        {
            EmployeeAllowanceSetupModel model = new EmployeeAllowanceSetupModel();
            return PartialView("_EmployeeAllowanceSetup", model);
        }

        [HttpPost]
        public ActionResult _EmployeeDeductionSetup()
        {
            EmployeeDeductionSetupModel model = new EmployeeDeductionSetupModel();
            return PartialView("_EmployeeDeductionSetup", model);
        }

        public ActionResult EmployeePayrollMaster(int id)
        {
            SetupEmployeeMasterProviders prov = new SetupEmployeeMasterProviders();
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            if (GetPayrollCount(id) == false)
            {
                return RedirectToAction("Index", new { string.Empty, message = "Employee Payroll Already Created. Please Edit Payroll !!" });
            }
            int designation = HospitalManagementSystem.Utility.GetDesignationOfEmployeeByID(id);
            model.SetupEmployeeMasterModelList = prov.GetDesignationDetail(designation);
            foreach (var item in model.SetupEmployeeMasterModelList)
            {
                model.BasicSalary = item.BasicSalary;
                model.GradeRate = item.GradeRate;
                model.CITAmount = item.CITAmount;
                model.CITPercentage = item.CITPercentage;
            }
            model.SetupEmployeeMasterModelList = prov.GetDesignationDetail(designation);
            foreach (var item in model.SetupEmployeeMasterModelList)
            {
                model.BasicSalary = item.BasicSalary;
                model.GradeRate = item.GradeRate;
                model.CITAmount = item.CITAmount;
                model.CITPercentage = item.CITPercentage;
            }
            model.EmployeeMasterId = id;
            return View("EmployeePayrollMaster", model);
        }

        [HttpPost]
        public ActionResult EmployeePayrollMaster(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.InsertPayroll(model);
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

        public ActionResult CalculateTotalMonth(int EmpID)
        {
            decimal Ttlmonth = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var JoinDate = ent.Database.SqlQuery<string>("select dbo.GetNepaliDate(EmployeeJoinDate) from SetupEmployeeMaster where EmployeeMasterId='" + EmpID + "'").FirstOrDefault();
                string year = JoinDate.Substring(0, 4);
                string month = JoinDate.Substring(5, 2);
                string day = JoinDate.Substring(8, 2);
                var FyLastDate = ent.Database.SqlQuery<string>("select dbo.GetNepaliDate(FiscalYearEndDate) from SetupFiscalYear where FiscalYearId='" + Utility.GetCurrentFiscalYearID() + "'").FirstOrDefault();
                string year1 = FyLastDate.Substring(0, 4);
                string month1 = FyLastDate.Substring(5, 2);
                string day1 = FyLastDate.Substring(8, 2);
                decimal totalYear = Convert.ToDecimal(year1) - Convert.ToDecimal(year);
                decimal totalMonth = 0;
                if (totalYear < 1)
                {
                    totalMonth = Convert.ToDecimal(month1) - Convert.ToDecimal(month);
                }
                else
                {
                    totalMonth = 12 - Convert.ToDecimal(month) + Convert.ToDecimal(month1);
                }
                var Day2 = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNoOfDaysInMonth('" + year + "','" + month + "')NoOfDays").FirstOrDefault().NoOfDays;
                decimal totalDay = Convert.ToDecimal(Day2) - Convert.ToDecimal(day) + 1;
                if (totalYear > 1)
                {
                    Ttlmonth = 12;
                }
                else
                {
                    decimal DayMnth = totalDay / Day2;
                    Ttlmonth = totalMonth + DayMnth;
                }
            }
            return Json(new { Ttlmonth });
        }

        public ActionResult CalculateTax(decimal Amount, int EmpID)
        {
            decimal SalaryAmount = Amount;
            decimal TaxAmount = 0;
            string IsMarried = "", Gender = "";
            decimal SSTAmount = 0;
            decimal WomenRebade = 0;
            decimal SSTPercentage = 0;
            decimal MedicalRebate = 0;
            DateTime JoinDate = DateTime.Now;
            DateTime FyLastDate = DateTime.Now;
            using (EHMSEntities ent = new EHMSEntities())
            {
                WomenRebade = Utility.GetWomenRebade();
                SSTPercentage = Utility.GetSSTPercentage();
                MedicalRebate = Utility.GetMedicalRebade();
                IsMarried = ent.Database.SqlQuery<string>("select isnull(MaritialStatus,'')MaritialStatus from EmployeeDetails where EmployeeMasterId='" + EmpID + "'").FirstOrDefault();
                Gender = ent.Database.SqlQuery<string>("select isnull(GenderID,'')MaritialStatus from SetupEmployeeMaster where EmployeeMasterId='" + EmpID + "'").FirstOrDefault();
                JoinDate = ent.Database.SqlQuery<DateTime>("select EmployeeJoinDate from SetupEmployeeMaster where EmployeeMasterId='" + EmpID + "'").FirstOrDefault();
                FyLastDate = ent.Database.SqlQuery<DateTime>("select FiscalYearEndDate from SetupFiscalYear where FiscalYearId='" + Utility.GetCurrentFiscalYearID() + "'").FirstOrDefault();
                var data = ent.TaxRangeSetups.Where(x => x.IsMarried == IsMarried).ToList();
                foreach (var item in data)
                {
                    string type = item.Type;
                    decimal percentage = item.Percentage;
                    decimal Amount1 = item.Amount;
                    decimal Amount2 = item.Amount1;
                    if (type == "<")
                    {
                        if (SalaryAmount <= Amount1)
                        {
                            SSTAmount = TaxAmount;
                            TaxAmount += (SalaryAmount * percentage) / 100;
                            SSTAmount = TaxAmount;
                        }
                        else if (SalaryAmount > Amount1)
                        {
                            TaxAmount += (Amount1 * percentage) / 100;
                            SalaryAmount -= Amount1;
                            SSTAmount = TaxAmount;
                        }
                    }
                    else if (type == "R")
                    {
                        if (Amount1 < Amount && Amount <= Amount2)
                        {
                            TaxAmount += (SalaryAmount * percentage) / 100;
                        }
                        else if (Amount > Amount2)
                        {
                            TaxAmount += ((Amount2 - Amount1) * percentage) / 100;
                            SalaryAmount -= (Amount2 - Amount1);
                        }
                    }
                    else if (type == ">")
                    {
                        if (Amount > Amount1)
                        {
                            TaxAmount += (SalaryAmount * percentage) / 100;
                        }
                    }
                }
                TaxAmount = TaxAmount - SSTAmount;
                if (TaxAmount > MedicalRebate)
                { TaxAmount -= MedicalRebate; }

                if (Gender == "Female")
                {
                    if (IsMarried == "Unmarried")
                    {
                        SSTAmount -= (SSTAmount * WomenRebade) / 100;
                        TaxAmount -= (TaxAmount * WomenRebade) / 100;
                    }
                }
            }
            return Json(new { TaxAmount, SSTAmount });
        }

        public ActionResult GetEmployeeDetailFromNew(int EmpID)
        {
            string Designation = "", Gender = "";
            decimal BasicSalary = 0, GradeAmount = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select DesignationSetup.Designation as DesignationName, SetupEmployeeMaster.GenderID, EmployeePayrollMaster.BasicSalary, EmployeePayrollMaster.GradeAmount
                                     from SetupEmployeeMaster 
                                     inner join EmployeePayrollMaster on SetupEmployeeMaster.EmployeeMasterId=EmployeePayrollMaster.EmployeeMasterId
                                     inner join DesignationSetup on DesignationSetup.ID=SetupEmployeeMaster.DesignationID
                                    where SetupEmployeeMaster.EmployeeMasterId=" + EmpID + "";
                var data = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
                foreach (var item in data)
                {
                    Designation = item.DesignationName;
                    Gender = item.GenderID;
                    BasicSalary = item.BasicSalary;
                    GradeAmount = item.GradeAmount;
                }
            }
            return Json(new { Designation, Gender, BasicSalary, GradeAmount });
        }

        public ActionResult GetEmployeeAdvanceAmount(int EmpID)
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
                var AccHeadID = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select isnull(AccountHeadID,0)AccountHeadID from SetupEmployeeMaster where EmployeeMasterId='" + EmpID + "'").FirstOrDefault().AccountHeadID;
                var MainHeadID = Utility.GetAccHeadIDFromDescription("Staff Advance");
                var fyid = Utility.GetCurrentFiscalYearID();
                var OpeningAmount = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select (isnull(OpeningBalance.DrAmount,0)-isnull(OpeningBalance.CrAmount,0))OpeningAmount from OpeningBalance where AccountHeadId='" + AccHeadID + "' and FiscalYear='" + fyid + "'").ToList();
                decimal openingamtnew = 0;
                if (OpeningAmount.Count > 0)
                    openingamtnew = OpeningAmount.FirstOrDefault().OpeningAmount;
                string sql = @"select (isnull(sum(JVDetails.DrAmount),0)-isnull(sum(JVDetails.CrAmount),0))AdvanceDeduction from JVDetails left join
                                  SetupEmployeeMaster on SetupEmployeeMaster.EmployeeMasterId=JVDetails.FeeTypeSubId inner join
                                  JVMaster on JVMaster.JvMasterId=JVDetails.JVMasterId
                                where JVDetails.FeeTypeSubId='" + AccHeadID + "' and JVDetails.FeeTypeId='" + MainHeadID + "' and JVMaster.TransactionDate<='" + EndDate + "'";
                var data = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
                foreach (var item in data)
                {
                    AdvanceAmount = item.AdvanceDeduction + openingamtnew;
                }
            }
            return Json(new { AdvanceAmount });
        }

        public ActionResult GetEmployeeFundDeductAmount(int EmpID, decimal BasicSalary, decimal GradeAmount)
        {
            decimal Amount = 0;
            decimal Percentage = 0;
            decimal FundAmount = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNepaliDate(GETDATE())TodayNepaliDate").FirstOrDefault().TodayNepaliDate;
                string year = date.Substring(0, 4);
                string month = date.Substring(5, 2);
                int yearID = 0;
                var yeardate = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select Id as YearId from tblNepaliYear where Year=N'" + year + "'").ToList(); ;
                foreach (var data1 in yeardate)
                {
                    yearID = data1.YearId;
                }
                string sql = @"select isnull(DeductionAmount,0) as FundAmount , isnull(DeductionPercentage,0) as FundPercentage from tblFundDeductionSetup where MonthID='" + month + "' and YearID='" + yearID + "' and EmployeeID='" + EmpID + "' and Status=1";
                var data = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList();
                foreach (var item in data)
                {
                    Amount = item.FundAmount;
                    Percentage = item.FundPercentage;
                }
                if (Amount > 0)
                    FundAmount = Amount;
                else
                {
                    decimal totalsalary = BasicSalary + GradeAmount;
                    FundAmount = totalsalary * Percentage / 100;
                }
            }
            return Json(new { FundAmount });
        }

        public ActionResult GetAllowanceAmount(decimal ID)
        {
            decimal Amount = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                Amount = ent.Database.SqlQuery<decimal>("select isnull(Amount,0)Amount from AllowanceSetup where AccountHeadID='" + ID + "'").FirstOrDefault();
            }
            return Json(new { Amount });
        }

        public ActionResult GetDeductionAmount(decimal ID)
        {
            decimal Amount = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                Amount = ent.Database.SqlQuery<decimal>("select isnull(Amount,0)Amount from DeductionSetup where AccountHeadID='" + ID + "'").FirstOrDefault();
            }
            return Json(new { Amount });
        }

        public bool GetPayrollCount(int id)
        {
            int count = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                count = ent.Database.SqlQuery<int>("select COUNT(*) from EmployeePayrollMaster where EmployeeMasterId='" + id + "'").FirstOrDefault();
            }
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult GetTotalNoOfDaysInMonth()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var NoOfDays = 0;
                try
                {
                    string sql = @"select dbo.GetNepaliDate(GETDATE())TodayNepaliDate";
                    var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).FirstOrDefault().TodayNepaliDate;
                    string year = date.Substring(0, 4);
                    string month = date.Substring(5, 2);
                    NoOfDays = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetNoOfDaysInMonth('" + year + "','" + month + "')NoOfDays").FirstOrDefault().NoOfDays;
                }
                catch
                {
                    NoOfDays = 0;
                }
                return Json(new { NoOfDays });
            }
        }



        public static int GetEmployeeDesignationFromEmpID(int EmpId)
        {
            using (EHMSEntities enta = new EHMSEntities())
            {
                var data = enta.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == EmpId).FirstOrDefault();

                if (data != null)
                {
                    return Convert.ToInt32(data.DesignationID);
                }
                else
                    return 0;
            }
        }

        public ActionResult FundDeductionSetup()
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult FundDeductionSetup(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.InsertFundDeductionSetup(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return RedirectToAction("FundDeductionIndex");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return RedirectToAction("FundDeductionIndex");
            }
        }

        public ActionResult FundDeductionEdit(int id)
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            model = SEMPro.GetEmployeeFundDeductionSetup().Where(x => x.id == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult FundDeductionEdit(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.UpdateFundDeductionSetup(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return RedirectToAction("FundDeductionIndex");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return RedirectToAction("FundDeductionIndex");
            }
        }

        public ActionResult DeleteFundDeductionSetup(int id)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.DeleteFundDeduction(id);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.delete;

            }
            else
            {
                TempData["success"] = UtilityMessage.deletefailed;
            }
            return RedirectToAction("FundDeductionIndex");
        }

        public ActionResult FundDeductionIndex(string searchString, string message)
        {
            string searchWords = searchString;
            if (message != null)
            {
                ViewBag.SaruMessage = message;
            }
            else
            {
                ViewBag.SaruMessage = "";
            }
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            model.SetupEmployeeMasterModelList = SEMPro.GetFundDeductionList();
            if (!String.IsNullOrEmpty(searchWords))
            {
                model.SetupEmployeeMasterModelList = SEMPro.GetFundDeductionListBySearchWord(searchWords);
            }
            return View(model);
        }

        public ActionResult FinalisePayroll()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinalisePayroll(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders pro = new SetupEmployeeMasterProviders();
            int i = 0;
            decimal SalaryExpenses = 0, PfDR = 0, Festival = 0, Dress = 0, Dearness = 0, OtherAllowance = 0, InsuranceDr = 0;
            decimal SalaryPayable = 0, InsuranceCR = 0, Advance = 0, PfCR = 0, CIT = 0, Fund = 0, SST = 0, Tax = 0, LoanDeduct = 0, LoanInterest = 0, LeaveDeduct = 0, TotalDeduct = 0;
            decimal TotalDebit = 0, TotalCredit = 0;
            model.SetupEmployeeMasterModelList = pro.GeneratePayroll(model.MonthId, model.YearId);
            model.ObjAddMoreParticularsEmployeeModel = new AddMoreParticularsEmployeeModel();
            foreach (var item in model.SetupEmployeeMasterModelList)
            {
                #region DR
                if (item.TotalDebit > 0)
                {
                    TotalDebit += item.TotalDebit;
                }
                if (item.BasicSalary > 0)
                {
                    SalaryExpenses += item.BasicSalary;
                }
                if (item.GradeAmount > 0)
                {
                    SalaryExpenses += item.GradeAmount;
                }
                if (item.OverTimeAmount > 0)
                {
                    SalaryExpenses += item.OverTimeAmount;
                }
                if (item.PFAmount > 0)
                {
                    PfDR += item.PFAmount;
                }
                if (item.FestivalAmount > 0)
                {
                    Festival += item.FestivalAmount;
                }
                if (item.DressAllowance > 0)
                {
                    Dress += item.DressAllowance;
                }
                if (item.DearnessAllowance > 0)
                {
                    Dearness += item.DearnessAllowance;
                }
                if (item.TotalAllowance > 0)
                {
                    OtherAllowance += item.TotalAllowance;
                }
                if (item.InsuranceAmount > 0)
                {
                    InsuranceDr += item.InsuranceAmount;
                }
                #endregion
                #region CR
                if (item.CITAmount > 0)
                {
                    CIT += item.CITAmount;
                }
                if (item.AdvanceDeduction > 0)
                {
                    Advance += item.AdvanceDeduction;
                }
                if (item.InsuranceDeductAmount > 0)
                {
                    InsuranceCR += item.InsuranceDeductAmount;
                }
                if (item.PFDeductAmount > 0)
                {
                    PfCR += item.PFDeductAmount;
                }
                if (item.FundAmount > 0)
                {
                    Fund += item.FundAmount;
                }
                if (item.SSTAmount > 0)
                {
                    SST += item.SSTAmount;
                }
                if (item.TaxAmount > 0)
                {
                    Tax += item.TaxAmount;
                }
                if (item.LoanDeduction > 0)
                {
                    LoanDeduct += item.LoanDeduction;
                }
                if (item.LoanInterest > 0)
                {
                    LoanInterest += item.LoanInterest;
                }
                if (item.LeaveDeduction > 0)
                {
                    LeaveDeduct += item.LeaveDeduction;
                }
                if (item.TotalDeduction > 0)
                {
                    TotalDeduct += item.TotalDeduction;
                }
                if (item.TotalCredit > 0)
                {
                    TotalCredit += item.TotalCredit;
                }
                if (item.SalaryPayable > 0)
                {
                    SalaryPayable += item.SalaryPayable;
                }
                #endregion
            }
            model.ObjAddMoreParticularsEmployeeModel.DrAmountTotal = TotalDebit;
            model.ObjAddMoreParticularsEmployeeModel.CrAmountTotal = TotalCredit;
            model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList = new List<AddMoreParticularsEmployeeModel>();
            #region AssignToListDR
            if (SalaryExpenses > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = SalaryExpenses,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Salary Expenses"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (PfDR > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = PfDR,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("PFAddition"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Festival > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = Festival,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("FestivalAmount"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Dress > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = Dress,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Dress Allowance"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Dearness > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = Dearness,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Dearness Allowance"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (InsuranceDr > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    CrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    DrAmount = InsuranceDr,
                    DrOrCr = 1,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("InsuranceDR"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            #endregion
            #region AssignToListCR
            if (SalaryPayable > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = SalaryPayable,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = model.BankAccHeadID,
                    SubParticulars = model.BankSubAccHeadID,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Advance > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = Advance,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Staff Advance"),
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (InsuranceCR > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = InsuranceCR,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("InsuranceCR"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (PfCR > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = PfCR,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("PFDeduction"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (CIT > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = CIT,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("CIT"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Fund > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = Fund,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Proverty Alivation Fund"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (SST > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = SST,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("SST"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (Tax > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = Tax,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("Tax"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (LoanDeduct > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = LoanDeduct,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("LoanDeduction"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (LoanInterest > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = LoanInterest,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("LoanInterest"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            if (LeaveDeduct > 0)
            {
                var ObjModel = new AddMoreParticularsEmployeeModel()
                {
                    DrAmount = 0,
                    DrAmountTotal = TotalDebit,
                    CrAmountTotal = TotalCredit,
                    CrAmount = LeaveDeduct,
                    DrOrCr = 0,
                    JvType = "JV",
                    Narration = "",
                    Particulars = Utility.GetAccHeadIDFromDescription("LeaveDeduction"),
                    SubParticulars = 0,
                    DrOrCrName = ""
                };
                model.ObjAddMoreParticularsEmployeeModel.AddMoreParticularsEmployeeModelList.Add(ObjModel);
            }
            #endregion
            if (model.ObjAddMoreParticularsEmployeeModel.DrAmountTotal != 0)
            {
                if (model.ObjAddMoreParticularsEmployeeModel.DrAmountTotal == model.ObjAddMoreParticularsEmployeeModel.CrAmountTotal)
                {
                    i = pro.CreateNewJv(model);
                    return RedirectToAction("ShowJournalVoucher", "JVMaster", new { id = i });
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult ResignationDetail(int id)
        {
            SetupEmployeeMasterModel model = new SetupEmployeeMasterModel();
            model.EmployeeMasterId = id;
            return View("ResignationDetail", model);
        }

        [HttpPost]
        public ActionResult ResignationDetail(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders SEMPro = new SetupEmployeeMasterProviders();
            int i = SEMPro.InsertResignationDetail(model);
            if (i != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult GenerateBankPay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateBankPay(SetupEmployeeMasterModel model)
        {
            SetupEmployeeMasterProviders prop = new SetupEmployeeMasterProviders();
            if (string.IsNullOrEmpty(model.YearId.ToString()))
                model.YearId = 0;
            if (string.IsNullOrEmpty(model.MonthId.ToString()))
                model.MonthId = 0;
            model.SetupEmployeeMasterModelList = prop.GetDetailOfSalaryPayable(model.YearId, model.MonthId, model.EmployeeMasterId);
            int i = 0;
            if (model.SetupEmployeeMasterModelList.Count > 0)
            {
                i = prop.CreateSalaryPaidJv(model);
                if (i > 0)
                {
                    return RedirectToAction("ShowJournalVoucher", "JVMaster", new { id = i });
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
    }
}

