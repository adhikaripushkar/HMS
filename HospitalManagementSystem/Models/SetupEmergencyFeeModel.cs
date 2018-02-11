using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpEmergencyFeeModel
    {
        public int FeeId { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Emergency Fee")]
        public decimal? EmergencyFee { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Tax")]
        public decimal? Tax { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Vat")]
        public decimal? Vat { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Total Fee")]
        public decimal? TotalFee { get; set; }
        [Display(Name = "Is Current")]
        public bool Status { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Fiscal Year")]
        public int FiscalYearId { get; set; }



        public List<SetUpEmergencyFeeModel> SetUpEmergencyFeeModelList { get; set; }
    }
}