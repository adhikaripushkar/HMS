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
    
    public partial class StockDistributionDetail
    {
        public int DistributionDetailId { get; set; }
        public int DistributionMasterId { get; set; }
        public int ItemEntryId { get; set; }
        public decimal Quantity { get; set; }
        public decimal SalesRate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> BranchId { get; set; }
    }
}
