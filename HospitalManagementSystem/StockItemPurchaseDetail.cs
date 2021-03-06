//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockItemPurchaseDetail
    {
        public int StockItemPurchaseDetailId { get; set; }
        public int ItemPurchaseId { get; set; }
        public int StockItemEntryId { get; set; }
        public int StockUnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public string BatchNo { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<System.DateTime> WarrentyDate { get; set; }
        public Nullable<System.DateTime> ManufacturedDate { get; set; }
    
        public virtual SetupStockItemEntry SetupStockItemEntry { get; set; }
        public virtual SetupStockUnit SetupStockUnit { get; set; }
        public virtual StockItemPurchase StockItemPurchase { get; set; }
    }
}
