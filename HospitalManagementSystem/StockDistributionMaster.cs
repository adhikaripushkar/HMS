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
    
    public partial class StockDistributionMaster
    {
        public int DistributionMasterId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string BillNo { get; set; }
        public System.DateTime BillDate { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
