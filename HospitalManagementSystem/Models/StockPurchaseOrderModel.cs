using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockPurchaseOrderModel
    {
        public int PurchaseOrderId { get; set; }
        [DisplayName("Purchase No")]
        public string PurchaseOrderNo { get; set; }
        [DisplayName("Order Date")]
        public DateTime PurchaseOrderDate { get; set; }
        [DisplayName("Derlivery Date")]
        public DateTime OrderDerliveryDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int DemandId { get; set; }
        public List<StockPurchaseOrderModel> StockPurchaseOrderModelList { get; set; }
        public List<StockPurchaseItemEntry> StockPurchaseItemEntryList { get; set; }

        public StockPurchaseOrderModel()
        {
            StockPurchaseItemEntryList = new List<StockPurchaseItemEntry>();
        }

        public StockPurchaseOrderDetailsModel StockPurchaseOrderDetailsModel { get; set; }
    }
    public class StockPurchaseItemEntry
    {
        public int StockCategoryId { get; set; }

        public int StockSubCategoryId { get; set; }

        public int StockItemEntryId { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal QuatationPrice { get; set; }
        public decimal VatAmount { get; set; }

    }

    public class StockPurchaseOrderDetailsModel
    {
        public int PurchaseOrderDetailId { get; set; }
        [DisplayName("Purchase Order Id")]
        public int PurchaseOrderId { get; set; }
        [DisplayName("Item Id")]
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Quotation Price")]
        public decimal QuotationPrice { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }
        [DisplayName("Manufacturer")]
        public int ManufactorerId { get; set; }
        [DisplayName("Vendor")]
        public int VendorId { get; set; }
        public decimal VatAmountTotal { get; set; }

        public List<StockPurchaseOrderDetailsModel> StockPurchaseOrderDetailsModelList { get; set; }

    }
}