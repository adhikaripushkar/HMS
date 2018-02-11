using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockDistributionMasterModel
    {
        public int DistributionMasterId { get; set; }

        public int PatientId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Bill NO")]
        public string BillNo { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Bill Date")]
        [DataType(DataType.Date)]
        public DateTime BillDate { get; set; }

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Name")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Sex")]
        public string Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string Remarks { get; set; }

        public bool Status { get; set; }

        public string Type { get; set; }

        public List<StockItemEntry> StockItemEntryList { get; set; }

        [DisplayName("Amount")]
        //[Range(1,int.MaxValue,ErrorMessage="Required")]
        public decimal Amount { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Discount")]
        public decimal Discount { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Vat(%)")]
        public decimal VatAmount { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        public List<StockDistributionMasterModel> DistributionMasterList { get; set; }

        public List<StockDistributionDetail> lstOfStockDistributionDetail { get; set; }
    }


    //public class StockDistributionDetailModel
    //{
    //    public int DistributionDetailId { get; set; }
    //    public int DistributionMasterId { get; set; }
    //    public int ItemEntryId { get; set; }
    //    public decimal Quantity { get; set; }
    //    public decimal SalesRate { get; set; }
    //    public decimal TotalAmount { get; set; }
    //    public bool Status { get; set; }
    //    public int CreatedBy { get; set; }
    //    public System.DateTime CreatedDate { get; set; }
    //    public Nullable<decimal> BranchId { get; set; }
    //}
}