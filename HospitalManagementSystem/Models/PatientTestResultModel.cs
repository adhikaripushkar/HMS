using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class PatientTestResultModel
    {

        //public int PatientTestResultID { get; set; }
        [DisplayName("Patient Test ID")]
        public int PatientTestID { get; set; }
        [DisplayName("Patient Test Detail ID")]
        public int PatientTestDetailID { get; set; }
        [DisplayName("Test ID")]
        public int TestID { get; set; }
        public int SectionID { get; set; }
        [DisplayName("Test Name")]
        public string TestName { get; set; }
        [DisplayName("Value 1")]
        public string Value1 { get; set; }
        [DisplayName("Value 2")]
        public string Value2 { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        public string RefRange { get; set; }
        public string ConvFactor { get; set; }
        public string UnitName { get; set; }

        public List<PatientTestResultModel> PatientTestResultModelList { get; set; }
        public List<PopulatePatientTestDetailsModel> PopulatePatientTestDetailsModelList { get; set; }
        public PopulatePatientTestDetailsModel PopulatePatientTestDetailsModel { get; set; }
        public PathoSectionModelResult PathoSectionModelResult { get; set; }
        public PopulatePatientTestDetailModel PopulatePatientTestDetailModel { get; set; }
        public List<PathoSectionModelResult> PathoSectionModelResultList { get; set; }
        public List<PopulatePatientTestDetailModel> PopulatePatientTestDetailModelList { get; set; }
        public PopulatePatientTestResultReportModel PopulatePatientTestResultReportModel { get; set; }
        public List<PopulatePatientTestResultReportModel> PopulatePatientTestResultReportModelList { get; set; }
        public List<PopulatePatientTestModel> PopulatePatientTestModelList { get; set; }


        public OpdModel ObjOpdMaster { get; set; }
        public List<OpdModel> OpdModelList { get; set; }
        public CytologyReportsModel ObjCytologyReportsModel { get; set; }
        public List<CytologyReportsModel> CytologyReportsModelList { get; set; }

        public BoneMarrowReportsModel ObjBoneMarrowReportsModel { get; set; }
        public List<BoneMarrowReportsModel> BoneMarrowReportsModelList { get; set; }



        public List<PathoSelectedDeptDoctViewModel> PathoSelectedDeptDoctViewModelList { get; set; }
        public PathoSelectedDeptDoctViewModel ObjPathoSelectedDeptDoctViewModel { get; set; }


        public PatientTestResultModel()
        {
            ObjOpdMaster = new OpdModel();
            OpdModelList = new List<OpdModel>();

            ObjCytologyReportsModel = new CytologyReportsModel();
            CytologyReportsModelList = new List<CytologyReportsModel>();


            ObjBoneMarrowReportsModel = new BoneMarrowReportsModel();
            BoneMarrowReportsModelList = new List<BoneMarrowReportsModel>();
        }
    }


    public class PopulatePatientTestDetailsModel
    {
        [DisplayName("OpdID")]
        public int OpdID { get; set; }
        public int DepartmentID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("LastName")]
        public string LastName { get; set; }
        [DisplayName("Gender")]
        public string Sex { get; set; }
        [DisplayName("Age")]
        public int AgeYear { get; set; }
    }

    public class PathoSectionModelResult
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
    }

    public class PopulatePatientTestDetailModel
    {
        public int PatientTestID { get; set; }
        [DisplayName("Patient ID")]
        public int PatientID { get; set; }
        [DisplayName("Refer Doctor")]
        public int ReferDoctorID { get; set; }
        [DisplayName("Test Registration Date")]
        public DateTime TestRegistrationDate { get; set; }
        [DisplayName("Payable Amount")]
        public decimal PayableAmount { get; set; }
        public string DoctorName { get; set; }
        public int DepartmentID { get; set; }
    }
    //report 
    public class PopulatePatientTestResultReportModel
    {
        public string PatientName { get; set; }
        public int PatientID { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string DoctorName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string UnitName { get; set; }
        public string Value1 { get; set; }
        [DisplayFormat(NullDisplayText = "--")]
        public string Value2 { get; set; }
        [DisplayFormat(NullDisplayText = "--")]
        public string RefRange { get; set; }
        [DisplayFormat(NullDisplayText = "--")]
        public string ConvFactor { get; set; }
        public DateTime TestRegisterdDate { get; set; }
        public int SectionId { get; set; }
    }

    public class PopulatePatientTestModel
    {
        public int PatientTestID { get; set; }
        public int PatientID { get; set; }
        public DateTime TestRegistrationDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }

    public class BoneMarrowReportsModel
    {
        public int BonmarrowRptId { get; set; }
        public int PatientId { get; set; }
        public int ReferDocId { get; set; }
        public string IPNumber { get; set; }
        public string Medicine { get; set; }
        public DateTime DateOfDispatch { get; set; }
        public DateTime Registerdate { get; set; }
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
        public Boolean Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PatientLogId { get; set; }
        public int BranchId { get; set; }
        public string WardOrOpd { get; set; }
        public List<BoneMarrowReportsModel> BoneMarrowReportsModelList { get; set; }


    }

    public class CytologyReportsModel
    {
        public int CytologyId { get; set; }
        [Required]
        public string CytopathNo { get; set; }
        public int PatientId { get; set; }
        public int RefDocId { get; set; }
        [Required]
        public int RegId { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public DateTime DateOfDispatch { get; set; }
        public string WardOrOpd { get; set; }
        public string ClinicalFeatures { get; set; }
        public string Speciman { get; set; }
        public string Site { get; set; }
        public string ProcedureNote { get; set; }
        public string MiicroDescription { get; set; }
        public string Diagnosis { get; set; }
        public string Comments { get; set; }
        public string Pathologists { get; set; }
        public string Resident { get; set; }
        public string Remarks { get; set; }
        public Boolean Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<CytologyReportsModel> CytologyReportsModelList { get; set; }


    }

    public class PathoSelectedDeptDoctViewModel
    {
        public int DoctDeptId { get; set; }
        public string DepartmentDoctorName { get; set; }
        public bool isSelected { get; set; }
    }


}