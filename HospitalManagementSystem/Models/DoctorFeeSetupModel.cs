using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DoctorFeeSetupModel
    {
        [DisplayName("Doctor Fee ID")]
        public int DoctorFeeID { get; set; }
        [DisplayName("Doctor ID")]
        public int DoctorID { get; set; }
        [DisplayName("Doctor Fee")]
        public int DoctorFee { get; set; }

        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Tax")]
        public decimal? Tax { get; set; }
        //[Required(ErrorMessage = "Required")]
        [Display(Name = "Vat")]
        public decimal? Vat { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Total Fee")]
        public decimal? TotalFee { get; set; }
        [Display(Name = "Hospital Percentage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(typeof(int), "0", "100",
         ErrorMessage = "{0} can only be between {1} and {2}")]
        public int? HosPer { get; set; }
        [Display(Name = "Doctor Precentage")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(typeof(int), "0", "100",
        ErrorMessage = "{0} can only be between {1} and {2}")]
        public int? DocPer { get; set; }
        [Display(Name = "Hospital Amount")]
        public decimal? HosPerAmt { get; set; }
        [Display(Name = "Doctor Amount")]
        public decimal? DocPerAmt { get; set; }
        [Display(Name = "Is Commision")]
        public bool IsCommision { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        [DisplayName("Year")]
        public int FiscalYear { get; set; }

        public List<DoctorFeeSetupModel> DoctorFeeSetupModelList { get; set; }
    }
}