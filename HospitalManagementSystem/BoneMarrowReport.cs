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
    
    public partial class BoneMarrowReport
    {
        public int BonmarrowRptId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> ReferDocId { get; set; }
        public string IPNumber { get; set; }
        public string Medicine { get; set; }
        public Nullable<System.DateTime> DateOfDispatch { get; set; }
        public string SiteOfAspiration { get; set; }
        public string BoneMarrowNumber { get; set; }
        public string ClinicalFeature { get; set; }
        public string PBSFinding { get; set; }
        public string BMAFinding { get; set; }
        public string Cellularity { get; set; }
        public string MERation { get; set; }
        public string Myelopoiesis { get; set; }
        public string Erythropoiesis { get; set; }
        public string Megakaryopoiesis { get; set; }
        public string Myelogram { get; set; }
        public string PlasmaCells { get; set; }
        public string Hemoparasites { get; set; }
        public string IMPRESSION { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> PatientLogId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<System.DateTime> Registerdate { get; set; }
        public string WardOrOpd { get; set; }
    }
}