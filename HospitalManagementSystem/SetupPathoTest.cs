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
    
    public partial class SetupPathoTest
    {
        public int TestId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<bool> IsParent { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PriceForeigner { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> TaxForeigner { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public Nullable<decimal> TaxAmountForeigner { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalAmountForeigner { get; set; }
        public string RefRange { get; set; }
        public string ConvFactor { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> AccountHeadId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> HosPer { get; set; }
        public Nullable<int> DocPer { get; set; }
        public Nullable<decimal> HosPerAmt { get; set; }
        public Nullable<decimal> DocPerAmt { get; set; }
        public Nullable<bool> IsCommision { get; set; }
        public Nullable<decimal> PayablePercentage { get; set; }
        public Nullable<decimal> PayableTaxTotal { get; set; }
        public Nullable<decimal> PayableGrandTotal { get; set; }
        public Nullable<decimal> PathologyComm { get; set; }
        public Nullable<decimal> PathologyCommAmt { get; set; }
        public Nullable<decimal> PathologyCommPer { get; set; }
        public Nullable<bool> IsSpecialTest { get; set; }
    }
}
