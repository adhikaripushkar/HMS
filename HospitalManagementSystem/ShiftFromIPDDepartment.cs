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
    
    public partial class ShiftFromIPDDepartment
    {
        public int ShiftFromIPDId { get; set; }
        public int OpdID { get; set; }
        public int IPDRegistrationID { get; set; }
        public int ShiftedDepartmentId { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime ShiftedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string Narration { get; set; }
    }
}
