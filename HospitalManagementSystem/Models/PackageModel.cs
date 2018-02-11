using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class PackageModel
    {
        public int PackageId { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Package Price")]
        public decimal Price { get; set; }
        [Display(Name = "Tax (%)")]
        public decimal Tax { get; set; }
        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal VatAmount { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }


        public List<PackageModel> PackageModelList { get; set; }
        public List<PackageDetailModel> PackageDetailList { get; set; }
        //subin
        //public List<PackageDetailAutoCompleteModel> PackageDetailAutoCompleteModelList { get; set; }
    }

    public class PackageDetailModel
    {
        public int PackageDetailId { get; set; }

        public int PackageId { get; set; }

        public int TestId { get; set; }

        public int SectionId { get; set; }
        public int DeptId { get; set; }
        public string TestName { get; set; }
        public bool Status { get; set; }

        public List<PackageDetailModel> PackageDetailList { get; set; }
    }
    //subin
    //public class PackageDetailAutoCompleteModel
    //{
    //    public int ItemID { get; set; }
    //    public string ItemName { get; set; }
    //    public int CategoryID { get; set; }
    //    public string CategoryName { get; set; }
    //}
}