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
    
    public partial class SetupMemberDependent
    {
        public int SetupMemberDetailID { get; set; }
        public int SetupMemberID { get; set; }
        public int MemberTypeID { get; set; }
        public int RelationID { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string MoibleNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public Nullable<int> BloodGroupID { get; set; }
        public string Gender { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual SetupMemberShip SetupMemberShip { get; set; }
    }
}
