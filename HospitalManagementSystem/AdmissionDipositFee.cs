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
    
    public partial class AdmissionDipositFee
    {
        public int AdmissionDipositFeeID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> FeeTypeID { get; set; }
        public Nullable<decimal> DepositAmount { get; set; }
        public Nullable<decimal> TaxPersentage { get; set; }
        public Nullable<decimal> VatPersentage { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<decimal> TaxAmoutn { get; set; }
        public Nullable<decimal> VatAmoutn { get; set; }
        public Nullable<int> YearID { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
