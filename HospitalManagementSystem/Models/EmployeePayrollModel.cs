using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace HospitalManagementSystem.Models
{

    public class DesignationSetupModel
    {
        public int ID { get; set; }
        public int LevelID { get; set; }
        public string Designation { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }
        public List<DesignationSetupModel> DesignationList { get; set; }

    }
    public class AllowanceSetupModel
    {
        public int ID { get; set; }
        public string AlowanceName { get; set; }
        public decimal Amount { get; set; }
        public int AccountHeadID { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }
        public List<AllowanceSetupModel> AllowanceSetupList { get; set; }
    }
    public class DeductionSetupModel
    {
        public int ID { get; set; }
        public string DeductionName { get; set; }
        public decimal Amount { get; set; }
        public int AccountHeadID { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }
        public List<DeductionSetupModel> DecuDeductionList { get; set; }
    }
    public class DesignationWiseSalarySetupModel
    {
        public int ID { get; set; }
        public int DesignationID { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Grade { get; set; }
        public decimal LaganiKoshPercent { get; set; }
        public decimal LaganiKoshAmount { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> BranchId { get; set; }
        public List<DesignationWiseSalarySetupModel> DesignationWiseSalarySetupModelList { get; set; }
    }

    public class IncrementSetupModel
    {
        public int ID { get; set; }
        [Display(Name = "Employee Name")]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "*")]
        public int? MonthID { get; set; }
        [Required(ErrorMessage = "*")]
        public int? YearID { get; set; }
        public decimal? IncrementPercent { get; set; }
        public decimal? IncrementAmount { get; set; }
        [Required(ErrorMessage = "*")]
        public Nullable<decimal> BasicSalary { get; set; }
        public Nullable<decimal> GradeRate { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> BranchId { get; set; }
        public List<IncrementSetupModel> IncrementSetupModelList { get; set; }
    }
}