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
    
    public partial class SetupMedicalcharge
    {
        public int MedicalChargeId { get; set; }
        public decimal PreMedicalPirce { get; set; }
        public decimal HologramPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Nullable<decimal> DiscontPrice { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public Nullable<decimal> GrandTotalAmount { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}