using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SurgeryChargeModel
    {
        public int SurgeryChargeId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Charge Name")]
        public string ChargeName { get; set; }


        [DisplayName("Charge Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Required")]
        public decimal ChargeAmount { get; set; }
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
        public bool IsCommision { get; set; }
        public bool Status { get; set; }

        [DisplayName("Tax(%)")]
        //[Range(1, int.MaxValue, ErrorMessage = "Required")]
        public decimal Tax { get; set; }

        [DisplayName("Tax Amount")]
        //[Range(1, int.MaxValue, ErrorMessage = "Required")]
        public decimal TaxAmount { get; set; }

        [DisplayName("Vat(%)")]
        public decimal Vat { get; set; }

        [DisplayName("Total Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Payable Income(%)")]
        public decimal PayablePercentage { get; set; }
        public decimal PayableTaxTotal { get; set; }
        [DisplayName("Payable Income Amount")]
        public decimal PayableGrandTotal { get; set; }
        public decimal SurgeonComm { get; set; }
        public decimal SurgeonCommAmount { get; set; }
        [DisplayName("Anesthia Income(%)")]
        public decimal AnesthiaComm { get; set; }
        [DisplayName("Anesthia Income Amount")]
        public decimal AnesthiaCommAmount { get; set; }






        public List<SurgeryChargeModel> SurgerChargeList { get; set; }
    }
}