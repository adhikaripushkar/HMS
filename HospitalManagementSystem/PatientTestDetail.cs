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
    
    public partial class PatientTestDetail
    {
        public int PatientTestDetailID { get; set; }
        public int PatientTestID { get; set; }
        public int PatientID { get; set; }
        public int DepartmentID { get; set; }
        public int SectionID { get; set; }
        public int TestID { get; set; }
        public System.DateTime TestDate { get; set; }
        public Nullable<System.DateTime> TestTime { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool DeliveryStatus { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual PatientTest PatientTest { get; set; }
    }
}
