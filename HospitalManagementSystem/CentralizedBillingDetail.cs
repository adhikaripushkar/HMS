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
    
    public partial class CentralizedBillingDetail
    {
        public int CBDID { get; set; }
        public int BillNo { get; set; }
        public int AccountHeadID { get; set; }
        public Nullable<int> AccountSubHeadID { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public bool Status { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> Times { get; set; }
        public string Narration { get; set; }
        public string Narration2 { get; set; }
    }
}
