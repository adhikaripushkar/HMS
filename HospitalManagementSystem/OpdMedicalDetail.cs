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
    
    public partial class OpdMedicalDetail
    {
        public int OpdMedicalDetailId { get; set; }
        public int OpdMasterId { get; set; }
        public int ManPowerId { get; set; }
        public int AgentId { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string PreHolo { get; set; }
        public decimal Commission { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual OpdMaster OpdMaster { get; set; }
    }
}
