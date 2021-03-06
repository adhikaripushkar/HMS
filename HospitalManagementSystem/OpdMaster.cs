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
    
    public partial class OpdMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpdMaster()
        {
            this.OpdFeeDetails = new HashSet<OpdFeeDetail>();
            this.OpdMedicalDetails = new HashSet<OpdMedicalDetail>();
            this.OpdPatientDoctorDetails = new HashSet<OpdPatientDoctorDetail>();
            this.OpdPatientOtherDetails = new HashSet<OpdPatientOtherDetail>();
        }
    
        public int OpdID { get; set; }
        public string PatientTitle { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactName { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string RegistrationMode { get; set; }
        public string Email { get; set; }
        public Nullable<int> AgeYear { get; set; }
        public Nullable<int> AgeMonth { get; set; }
        public Nullable<int> AgeDay { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public Nullable<int> MemberTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> DepartmentPatientId { get; set; }
        public string Address { get; set; }
        public string RegistrationSource { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<bool> PaidOnPaid { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<long> AccountHeadId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> vdcID { get; set; }
        public string Sex { get; set; }
        public Nullable<int> ReferId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpdFeeDetail> OpdFeeDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpdMedicalDetail> OpdMedicalDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpdPatientDoctorDetail> OpdPatientDoctorDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpdPatientOtherDetail> OpdPatientOtherDetails { get; set; }
    }
}
