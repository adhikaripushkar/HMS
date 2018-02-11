using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    public class SetupEmployeeMasterModel
    {
        public int AccountHeadID { get; set; }
        public decimal OpeningAmount { get; set; }
        public int id { get; set; }
        public int EmployeeMasterId { get; set; }
        public decimal Noofjoinmonth { get; set; }
        [Display(Name = "Code")]
        public string EmployeeCode { get; set; }
        [Display(Name = "Type")]
        public int EmployeeTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        public string EmployeeFullName { get; set; }
        [Display(Name = "D.O.B")]
        public DateTime EmployeeDOB { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Temporary Address")]
        public string TemporaryAddress { get; set; }
        [Display(Name = "Phone No.")]
        [StringLength(10, ErrorMessage = "*")]
        public string EmployeePhone { get; set; }

        [Display(Name = "Mobile No.")]
        [StringLength(10, ErrorMessage = "*")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string EmployeeMobile { get; set; }

        [Display(Name = "Email Id.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "*")]
        public string EmployeeEmail { get; set; }
        [Display(Name = "Join Date")]
        public DateTime EmployeeJoinDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
        public int LoginDepartmentIdnt { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string CountryID { get; set; }
        public string CityName { get; set; }
        public string GenderID { get; set; }
        public string TypeOfEmployee { get; set; }
        public DateTime DateOfRetirement { get; set; }
        public int BranchId { get; set; }
        public char IsVolunteer { get; set; }


        public int BankAccHeadID { get; set; }
        public int BankSubAccHeadID { get; set; }
        //Employee Payroll
        public decimal BasicSalary { get; set; }
        public int GradeNo { get; set; }
        public decimal GradeRate { get; set; }
        public decimal GradeAmount { get; set; }
        public int CITNo { get; set; }
        public decimal CITPercentage { get; set; }
        public decimal CITAmount { get; set; }
        public int PFNo { get; set; }
        public decimal PFAmount { get; set; }
        public int FundNo { get; set; }
        public decimal FundAmount { get; set; }
        public decimal FundPercentage { get; set; }
        public int BankID { get; set; }
        public string BankAccountNo { get; set; }
        public decimal LoanDeduction { get; set; }
        public decimal LoanInterest { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public int AdvanceID { get; set; }
        public int PayrollInsuranceNo { get; set; }
        public decimal InsuranceAmount { get; set; }
        public decimal PFDeductAmount { get; set; }
        public decimal InsuranceDeductAmount { get; set; }
        public decimal SSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FestivalAmount { get; set; }
        public decimal EmpInsurancePaid { get; set; }
        public decimal RemoteAreaDeduction { get; set; }
        public decimal PhysicalDisability { get; set; }
        public int BudgetID { get; set; }
        public int BudgetSubID { get; set; }
        public int FundID { get; set; }
        public decimal LeaveDeduction { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal TotalDeduction { get; set; }
        public string OverTimeDuration { get; set; }
        public decimal OverTimeAmount { get; set; }
        public decimal DressAllowance { get; set; }
        public decimal DearnessAllowance { get; set; }
        public string PresentDay { get; set; }
        public string OTHrDay { get; set; }
        public string Remarks { get; set; }

        public bool CheckAll { get; set; }

        public int MonthId { get; set; }
        public int YearId { get; set; }
        public string MonthName { get; set; }
        public string YearName { get; set; }

        public int Number { get; set; }
        //EMPLOYEE DETAIL
        public int EmployeeID { get; set; }
        public string CitizenshipNo { get; set; }
        public int ReleaseDistrict { get; set; }
        public string MaritialStatus { get; set; }
        public string PanNo { get; set; }
        public string InsuranceNo { get; set; }
        public string BloodGroup { get; set; }
        public string GrandFatherName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string WifeHusbandName { get; set; }
        public string NomineeName { get; set; }
        public string NomineeAddress { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineePhoneNo { get; set; }

        public int NoOfDays { get; set; }
        public string TodayNepaliDate { get; set; }

        //SalarySheet
        public string BankName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalTaxableAmount { get; set; }
        public decimal NetTaxableAmount { get; set; }
        public decimal SalaryPayable { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public int ParentID { get; set; }

        //ResignationDetail
        public DateTime ResignationDate { get; set; }
        public string ReasonForResignation { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }

        public List<SetupEmployeeMasterModel> SetupEmployeeMasterModelList { get; set; }
        public List<SetupEmployeeDepartmentModel> ListSetupEmployeeDepartmentModel { get; set; }
        public SetupEmployeeDepartmentModel SetupEmployeeDepartmentModel { get; set; }
        public List<SetupEmployeeShiftModel> ListSetupEmployeeShiftModel { get; set; }
        public SetupEmployeeShiftModel SetupEmployeeShiftModel { get; set; }
        public AddMoreParticularsEmployeeModel ObjAddMoreParticularsEmployeeModel { get; set; }

        public EmployeeEducationInfoModel EmployeeEducationInfoModel { get; set; }
        public EmployeeTrainingInfoModel EmployeeTrainingInfoModel { get; set; }
        public List<EmployeeEducationInfoModel> EmployeeEducationInfoList { get; set; }
        public List<EmployeeTrainingInfoModel> EmployeeTrainingInfoList { get; set; }

        public EmployeeAllowanceSetupModel EmployeeAllowanceSetupModel { get; set; }
        public EmployeeDeductionSetupModel EmployeeDeductionSetupModel { get; set; }
        public List<EmployeeAllowanceSetupModel> EmployeeAllowanceSetupModelList { get; set; }
        public List<EmployeeDeductionSetupModel> EmployeeDeductionSetupModelList { get; set; }

        public EmployeePayrollMasterModel EmployeePayrollMasterModel { get; set; }
        public List<EmployeePayrollMasterModel> EmployeePayrollMasterModelList { get; set; }


        //public List<AddMoreParticularsEmployeeModel> AddMoreParticularsEmployeeModelList { get; set; }
        //public SetupEmployeeMasterModel()
        //{
        //    AddMoreParticularsEmployeeModelList = new List<AddMoreParticularsEmployeeModel>();
        //}

        public IEnumerable<SelectListItem> GetDepatmentList()
        {
            return new SelectList(Utility.GetDeptList(), "DeptID", "DeptName");
        }

        public IEnumerable<SelectListItem> GetsfttList()
        {
            return new SelectList(Utility.GetShiftListforemployee(), "EmployeeShiftMasterId", "EmployeeShiftName");
        }

        public string GetShiftTime(int id)
        {
            try
            {
                var obj = Utility.GetShiftListforemployee().Where(s => s.EmployeeShiftMasterId == id);
                string sttime = obj.Select(x => x.StartTime).FirstOrDefault().ToString();
                string endtime = obj.Select(x => x.EndTime).FirstOrDefault().ToString();

                return sttime = "(" + sttime + " " + "To" + " " + endtime + ")";
            }
            catch
            {

                return "00:00:00";
            }

        }

        public List<int> SelectedDepartmentIDs { get; set; }
        public List<int> SelectedShiftIDs { get; set; }
    }

    public class AddMoreParticularsEmployeeModel
    {
        public string DrOrCrName { get; set; }
        public int DrOrCr { get; set; }
        public int Particulars { get; set; }
        public int ParentID { get; set; }
        public int SubParticulars { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string Narration { get; set; }
        public decimal DrAmountTotal { get; set; }
        public decimal CrAmountTotal { get; set; }
        public string JvType { get; set; }
        public List<AddMoreParticularsEmployeeModel> AddMoreParticularsEmployeeModelList { get; set; }
    }

    public class SetupEmployeeDepartmentModel
    {
        public int EmployeeDepartmentId { get; set; }
        public int EmployeeMasterId { get; set; }
        public int DepartmentId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class SetupEmployeeShiftModel
    {
        public int EmployeeShiftId { get; set; }
        public int EmployeeMasterId { get; set; }
        public int EmployeeShiftMasterId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class EmployeeEducationInfoModel
    {
        public int EmployeeMasterId { get; set; }
        public string LevelID { get; set; }
        public string UniversityName { get; set; }
        public string Grade { get; set; }
        public string Percentage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class EmployeeTrainingInfoModel
    {
        public int EmployeeMasterId { get; set; }
        public string Course { get; set; }
        public string TrainingCenter { get; set; }
        public string TGrade { get; set; }
        public string Duration { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class EmployeeAllowanceSetupModel
    {
        public int EmployeeMasterId { get; set; }
        public int AllowanceID { get; set; }
        public decimal AllowanceAmount { get; set; }
        public int AllowanceFundProviderID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class EmployeeDeductionSetupModel
    {
        public int EmployeeMasterId { get; set; }
        public int DeductionID { get; set; }
        public decimal DeductionAmount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }

    public class EmployeePayrollMasterModel
    {
        public int EmployeeMasterId { get; set; }
        public decimal BasicSalary { get; set; }
        public int GradeNo { get; set; }
        public decimal GradeRate { get; set; }
        public decimal GradeAmount { get; set; }
        public int CITNo { get; set; }
        public decimal CITAmount { get; set; }
        public int PFNo { get; set; }
        public decimal PFAmount { get; set; }
        public int FundNo { get; set; }
        public decimal FundAmount { get; set; }
        public int BankID { get; set; }
        public string BankAccountNo { get; set; }
        public decimal LoanDeduction { get; set; }
        public decimal LoanInterest { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public int PayrollInsuranceNo { get; set; }
        public decimal InsuranceAmount { get; set; }
        public decimal PFDeductAmount { get; set; }
        public decimal InsuranceDeductAmount { get; set; }
        public decimal SSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FestivalAmount { get; set; }
        public int BudgetID { get; set; }
        public int BudgetSubID { get; set; }
        public int FundID { get; set; }
        public decimal LeaveDeduction { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal TotalDeduction { get; set; }
        public string OverTimeDuration { get; set; }
        public decimal OverTimeAmount { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string InsuranceNo { get; set; }
    }
}

