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
    
    public partial class VitalForOther
    {
        public int VitalForOtherId { get; set; }
        public int OpdId { get; set; }
        public string AVPU { get; set; }
        public string Pulse { get; set; }
        public string BP_Right { get; set; }
        public string BP_Left { get; set; }
        public string RR { get; set; }
        public string SPO2 { get; set; }
        public string TPR { get; set; }
        public Nullable<int> Wt { get; set; }
        public Nullable<int> Feet { get; set; }
        public Nullable<int> Inch { get; set; }
        public Nullable<decimal> BMI { get; set; }
        public string Date { get; set; }
        public string VTime { get; set; }
        public string Staff { get; set; }
        public string Department { get; set; }
        public Nullable<int> PatinetLogId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string ElergeryToDrugs { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
