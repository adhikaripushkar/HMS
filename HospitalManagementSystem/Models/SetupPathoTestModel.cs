using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupPathoTestModel
    {
        public int TestId { get; set; }
        [DisplayName("Test Code")]
        [Required(ErrorMessage = "*")]
        public string TestCode { get; set; }
        [DisplayName("Test Name")]
        [Required(ErrorMessage = "*")]
        public string TestName { get; set; }
        [DisplayName("Section Name")]
        [Required(ErrorMessage = "*")]
        public int SectionId { get; set; }
        [DisplayName("Is Parent")]
        [Required(ErrorMessage = "*")]
        public bool IsParent { get; set; }
        [DisplayName("Parent Id")]
        [Required(ErrorMessage = "*")]
        public int ParentId { get; set; }
        [DisplayName("Unit Name")]
        [Required(ErrorMessage = "*")]
        public int UnitId { get; set; }
        [DisplayName("Amount")]
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
        [Required(ErrorMessage = "*")]
        public decimal Price { get; set; }
        public bool IsCommision { get; set; }
        [DisplayName("Amount (Foriegner)")]
        public decimal PriceForeigner { get; set; }
        [DisplayName("Tax (Foriegner)")]
        public decimal TaxForeigner { get; set; }
        [DisplayName("Tax Amount (Foriegner)")]
        public decimal TaxAmountForeigner { get; set; }
        [DisplayName("Total Amount (Foriegner)")]
        public decimal TotalAmountForeigner { get; set; }

        [DisplayName("Reference Range")]
        public string RefRange { get; set; }
        [DisplayName("Conv Factor")]
        public string ConvFactor { get; set; }


        public Nullable<decimal> Tax { get; set; }

        public Nullable<decimal> TaxAmount { get; set; }

        public Nullable<decimal> TotalAmount { get; set; }

        public Nullable<decimal> PayablePercentage { get; set; }
        public Nullable<decimal> PayableTaxTotal { get; set; }
        public Nullable<decimal> PayableGrandTotal { get; set; }


        public Nullable<decimal> PathologyComm { get; set; }
        public Nullable<decimal> PathologyCommAmt { get; set; }
        public Nullable<decimal> PathologyCommPer { get; set; }



        public List<SetupPathoTestModel> PathoTestList { get; set; }


    }



    public class SetupOtherTestModel
    {
        [Display(Name = "Test Id")]
        public int SetupOtherTestId { get; set; }
        [Display(Name = "Test Name")]
        public string OtherTestName { get; set; }
        [Display(Name = "Remarks1")]
        public string TestNameRemarks1 { get; set; }
        [Display(Name = "Remarks1")]
        public string TestNameRemarks2 { get; set; }
        [Display(Name = "Amount")]
        public decimal TestAmount { get; set; }
        [Display(Name = "Tax %")]
        public decimal TestTax { get; set; }
        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Fiscal Year")]
        public int FiscalYearId { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeID { get; set; }
        public Boolean Status { get; set; }

        [Display(Name = "Amount (Foreigner) ")]
        public decimal TestAmountForeigner { get; set; }
        [Display(Name = "Tax (Foreigner) ")]
        public decimal TestTaxForeigner { get; set; }
        [Display(Name = "Tax Amount (Foreigner) ")]
        public decimal TaxAmountForeigner { get; set; }
        [Display(Name = "Total Amount (Foreigner) ")]
        public decimal TotalAmountForeigner { get; set; }



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



        public Nullable<decimal> PayablePercentage { get; set; }
        public Nullable<decimal> PayableTaxTotal { get; set; }
        public Nullable<decimal> PayableGrandTotal { get; set; }

        [DisplayName("Is Commision")]
        public bool IsCommision { get; set; }

        public List<SetupOtherTestModel> SetupOtherTestModelList { get; set; }

    }
}