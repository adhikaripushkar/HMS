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
    
    public partial class StockItemMaster
    {
        public int StockItemMasterId { get; set; }
        public int StockItemEntryId { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual SetupStockItemEntry SetupStockItemEntry { get; set; }
        public virtual SetupStockUnit SetupStockUnit { get; set; }
    }
}
