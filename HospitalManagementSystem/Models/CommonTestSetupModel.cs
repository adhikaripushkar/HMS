using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class CommonTestSetupModel
    {




        public int HospitalExtraTestId { get; set; }

        [Required(ErrorMessage = "Insert Test Name")]
        [Display(Name = "Test Name")]
        public string ExtraTestName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Required")]
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Vat { get; set; }
        [Display(Name = "Tax Amount")]
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> VatAmount { get; set; }
        [Display(Name = "Total Amount")]
        public Nullable<decimal> TotalAmount { get; set; }
        [Display(Name = "Fiscal Year")]
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> CommonTestTypeId { get; set; }

        [Required(ErrorMessage = "Required")]
        public Nullable<bool> Status { get; set; }

        public List<CommonTestSetupModel> CommonTestList { get; set; }

    }
}