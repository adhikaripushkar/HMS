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
    
    public partial class CommissionDetail
    {
        public int CommissionDetailsId { get; set; }
        public Nullable<int> CommissionType { get; set; }
        public string CommissionName { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<bool> Status { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> TestId { get; set; }
        public Nullable<int> BillNumber { get; set; }
        public Nullable<bool> JVStatus { get; set; }
    }
}