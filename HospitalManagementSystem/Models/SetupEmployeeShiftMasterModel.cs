using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupEmployeeShiftMasterModel
    {
        public int EmployeeShiftMasterId { get; set; }
        [Required(ErrorMessage = "*")]
        public string EmployeeShiftName { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
        [Display(Name = "Valid From")]
        public DateTime ValidFromDate { get; set; }
        [Display(Name = "Valid To")]
        public DateTime ValidTillDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
        public List<SetupEmployeeShiftMasterModel> SetupEmployeeShiftMasterModelList { get; set; }
    }
}