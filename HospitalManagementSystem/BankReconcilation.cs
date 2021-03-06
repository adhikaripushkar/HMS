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
    
    public partial class BankReconcilation
    {
        public int ID { get; set; }
        public int BankID { get; set; }
        public int BankSubID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string JvDate { get; set; }
        public string JvNo { get; set; }
        public string Narration { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string BankDate { get; set; }
        public Nullable<decimal> BalancePerCompanyDr { get; set; }
        public Nullable<decimal> BalancePerCompanyCr { get; set; }
        public Nullable<decimal> BalanceNotReflectedDr { get; set; }
        public Nullable<decimal> BalanceNotReflectedCr { get; set; }
        public Nullable<decimal> BalancePerBankDr { get; set; }
        public Nullable<decimal> BalancePerBankCr { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> BranchID { get; set; }
        public bool Status { get; set; }
    }
}
