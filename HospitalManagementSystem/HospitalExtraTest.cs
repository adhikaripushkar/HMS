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
    
    public partial class HospitalExtraTest
    {
        public int HospitalExtraTestId { get; set; }
        public string ExtraTestName { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> VatAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> CommonTestTypeId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
