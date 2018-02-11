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
    
    public partial class HandoverDetail
    {
        public int ID { get; set; }
        public System.DateTime HandOverDate { get; set; }
        public System.DateTime HandOverDateFrom { get; set; }
        public System.DateTime HandOverDateTo { get; set; }
        public int HandOverFrom { get; set; }
        public int HandOverTo { get; set; }
        public decimal HandOverAmount { get; set; }
        public Nullable<int> Rs1000 { get; set; }
        public Nullable<int> Rs500 { get; set; }
        public Nullable<int> Rs100 { get; set; }
        public Nullable<int> Rs50 { get; set; }
        public Nullable<int> Rs20 { get; set; }
        public Nullable<int> Rs10 { get; set; }
        public Nullable<int> Rs5 { get; set; }
        public Nullable<int> Rs2 { get; set; }
        public Nullable<int> Rs1 { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsHandOver { get; set; }
        public Nullable<decimal> Paisa { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> TotalBankDeposit { get; set; }
        public Nullable<decimal> TotalExpenses { get; set; }
    }
}