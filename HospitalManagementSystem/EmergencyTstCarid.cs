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
    
    public partial class EmergencyTstCarid
    {
        public int EmTestd { get; set; }
        public Nullable<int> EmergencyMasterId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> TestId { get; set; }
        public string ShortDecs { get; set; }
        public string LongDesc { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual EmergencyMaster EmergencyMaster { get; set; }
    }
}
