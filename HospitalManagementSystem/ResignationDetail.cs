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
    
    public partial class ResignationDetail
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public System.DateTime ResignationDate { get; set; }
        public string ReasonForResignation { get; set; }
        public int ApprovedBy { get; set; }
        public System.DateTime ApprovedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> BranchID { get; set; }
        public bool Status { get; set; }
    }
}