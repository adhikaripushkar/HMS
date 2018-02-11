using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockItemPurchaseModel
    {
        public int ItemPurchaseId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Item Order Id")]
        public string ItemOrderId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Item Bill No")]
        public int? ItemBillNo { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Supplier")]
        public int StockSupplierId { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Narration")]
        public string Narration { get; set; }


        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        //[Required(ErrorMessage = "Required")]
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

        [DisplayName("Stock Entry No.")]
        public string StockEntryNo { get; set; }

        public int PaymentType { get; set; }


        public List<StockItemPurchaseModel> ItemPurchaseList { get; set; }

        //public StockItemEntryModel StockItemEntry { get; set; }

        public List<StockItemEntry> StockItemEntryList { get; set; }

        public StockItemPurchaseModel()
        {
            StockItemEntryList = new List<StockItemEntry>();
        }
    }

    public class StockItemEntry
    {
        public int StockCategoryId { get; set; }

        public int StockSubCategoryId { get; set; }

        public int StockItemEntryId { get; set; }

        public decimal QuotQty { get; set; }

        public decimal QuotRate { get; set; }
        public decimal Quantity { get; set; }


        public decimal Rate { get; set; }

        public string BatchNo { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime WarrentyDate { get; set; }
        public DateTime ManufacturedDate { get; set; }

    }

    public class StockItemPurchaseDetailModel
    {
        public int StockItemPurchaseDetailId { get; set; }

        [DisplayName("Item Purchase Id")]
        public int ItemPurchaseId { get; set; }

        [DisplayName("Item")]
        public int StockItemEntryId { get; set; }

        [DisplayName("Unit")]
        public int StockUnitId { get; set; }

        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Rate")]
        public decimal Rate { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Supplier")]
        public int? SupplierId { get; set; }

        public string SeNo { get; set; }

        public string PurchaseDate { get; set; }

        public List<StockItemPurchaseDetailModel> ItemPurchaseDetailList { get; set; }
    }

    public class ItemPurchaseReportModel
    {
        public int StockItemPurchaseDetailId { get; set; }
        public int ItemPurchaseId { get; set; }
        [DisplayName("Item Order Id")]
        public string ItemOrderId { get; set; }
        [DisplayName("Item Bill No")]
        public int ItemBillNo { get; set; }
        [DisplayName("Supplier")]
        public int StockSupplierId { get; set; }
        [DisplayName("Item")]
        public int StockItemEntryId { get; set; }
        [DisplayName("Unit")]
        public int StockUnitId { get; set; }
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }
        [DisplayName("Rate")]
        public decimal Rate { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Discount")]
        public decimal Discount { get; set; }
        [DisplayName("Vat(%)")]
        public decimal VatAmount { get; set; }
        [DisplayName("Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        [DisplayName("Warrenty Date")]
        public DateTime? WarrentyDate { get; set; }
        [DisplayName("Manufactured Date")]
        public DateTime? ManufacturedDate { get; set; }
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public List<ItemPurchaseReportModel> ItemPurchaseReportList { get; set; }

    }



}