using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpHospitalFeeDiscountModel
    {

        public int FeeDiscountId { get; set; }

        [Required(ErrorMessage = "Required")]
        [System.ComponentModel.DisplayName("Maximum Day")]
        public int? MaxDay { get; set; }
        [Required(ErrorMessage = "Required")]
        [System.ComponentModel.DisplayName("Percentage")]
        public decimal? Percentage { get; set; }
        public int? CreatedBy { get; set; }
        [System.ComponentModel.DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
        [Display(Name = "Fiscal Year")]
        public int FiscalYearId { get; set; }

        public List<SetUpHospitalFeeDiscountModel> SetUpHospitalFeeDiscountModelList { get; set; }
    }
}