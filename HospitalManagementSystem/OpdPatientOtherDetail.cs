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
    
    public partial class OpdPatientOtherDetail
    {
        public int OtherDetailsID { get; set; }
        public Nullable<int> OpdID { get; set; }
        public string ReferedBy { get; set; }
        public string History { get; set; }
        public string Reason { get; set; }
        public Nullable<int> GurdainID { get; set; }
        public string GurdainFirstName { get; set; }
        public string GurdainMiddleName { get; set; }
        public string GurdainLastName { get; set; }
        public string GurdainAddress { get; set; }
        public string GurdainContactNumber { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual OpdMaster OpdMaster { get; set; }
    }
}
