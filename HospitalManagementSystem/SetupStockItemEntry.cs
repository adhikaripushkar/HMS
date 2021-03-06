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
    
    public partial class SetupStockItemEntry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SetupStockItemEntry()
        {
            this.StockItemMasters = new HashSet<StockItemMaster>();
            this.StockItemPurchaseDetails = new HashSet<StockItemPurchaseDetail>();
            this.StockPurchaseOrderDetails = new HashSet<StockPurchaseOrderDetail>();
        }
    
        public int StockItemEntryId { get; set; }
        public int StockCategoryId { get; set; }
        public int StockSubCategoryId { get; set; }
        public int StockUnitId { get; set; }
        public string StockItemName { get; set; }
        public Nullable<int> StockSupplierId { get; set; }
        public Nullable<int> StockItemTypeId { get; set; }
        public Nullable<int> PageNumber { get; set; }
        public Nullable<decimal> MinStockQuantity { get; set; }
        public Nullable<decimal> MaxStockQuantity { get; set; }
        public int OpeningStock { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual SetupStockCategory SetupStockCategory { get; set; }
        public virtual SetupStockSubCategory SetupStockSubCategory { get; set; }
        public virtual SetupStockUnit SetupStockUnit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockItemMaster> StockItemMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockItemPurchaseDetail> StockItemPurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockPurchaseOrderDetail> StockPurchaseOrderDetails { get; set; }
    }
}
