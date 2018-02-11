using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class FiscalYearModel
    {
        public int FiscalYearId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Fiscal Year in BS")]
        public string FiscalYearBS { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Fiscal Year in AD")]
        public string FiscalYearAD { get; set; }

        public char IsCurrent { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Start Date")]
        public DateTime? FiscalYearStartDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("End Date")]
        public DateTime? FiscalYearEndDate { get; set; }

        public List<FiscalYearModel> FiscalYearList { get; set; }
    }
}