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
    
    public partial class PreRegistrationPreferDetail
    {
        public int PreferDetailsID { get; set; }
        public int PreRegistrationID { get; set; }
        public int DepartmentID { get; set; }
        public int DoctorID { get; set; }
        public System.DateTime PreferDate { get; set; }
        public System.TimeSpan PreferTime { get; set; }
        public Nullable<int> BranchId { get; set; }
    }
}