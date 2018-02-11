using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpHospitalFeeModel
    {
        public int FeeId { get; set; }


        [Required(ErrorMessage = "Required")]
        [Display(Name = "Fee Type")]
        public int FeeType { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Registration Fee")]
        public decimal? HospitalFee { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Tax(%)")]
        public decimal? Tax { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Vat(%)")]
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
        [Display(Name = "Registration Fee (Foreigner)")]
        public decimal HospitalFeeForeigner { get; set; }
        [Display(Name = "Tax (Foreigner) %")]
        public decimal TaxForeigner { get; set; }
        [Display(Name = "Vat (Foreigner) %")]
        public decimal VatForeigner { get; set; }
        [Display(Name = "Total Fee (Foreigner) %")]
        public decimal TotalFeeForeigner { get; set; }
        [Display(Name = "Consultant Income")]
        public decimal ConsultantIncome { get; set; }
        [Display(Name = "Hospital Income")]
        public decimal HospitalIncome { get; set; }




        public List<SetUpHospitalFeeModel> SetUpHospitalFeeModelList { get; set; }


    }

    public class AdmissionDipositFeeModel
    {
        public int AdmissionDipositFeeID { get; set; }
        public int DepartmentID { get; set; }
        public int FeeTypeID { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal TaxPersentage { get; set; }
        public decimal VatPersentage { get; set; }
        public decimal TaxAmoutn { get; set; }
        public decimal VatAmoutn { get; set; }
        public decimal TotalAmount { get; set; }
        public int YearID { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Boolean Status { get; set; }

        public List<AdmissionDipositFeeModel> AdmissionDipositList { get; set; }
    }



}