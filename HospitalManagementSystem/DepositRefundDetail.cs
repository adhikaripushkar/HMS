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
    
    public partial class DepositRefundDetail
    {
        public int DepositRefundId { get; set; }
        public Nullable<decimal> RemainingAmount { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> PatientLogId { get; set; }
        public Nullable<System.DateTime> RefundDate { get; set; }
        public Nullable<int> RefundBy { get; set; }
        public Nullable<bool> status { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
    }
}