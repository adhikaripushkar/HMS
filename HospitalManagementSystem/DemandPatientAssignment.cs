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
    
    public partial class DemandPatientAssignment
    {
        public int DemandPatientAssignmentId { get; set; }
        public int OpdId { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; }
        public int DemandId { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
