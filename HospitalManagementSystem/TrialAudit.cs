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
    
    public partial class TrialAudit
    {
        public int TrialAuditId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int FiscalYearId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public System.DateTime CurrentDateTime { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}
