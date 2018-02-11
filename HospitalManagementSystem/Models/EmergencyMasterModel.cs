using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;

using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class EmergecyMasterModel
    {
        public int EmergencyMasterId { get; set; }
        [Display(Name = "Emergency Number")]
        public int EmergencyNumber { get; set; }

        [Display(Name = "Serial Number")]
        public int SerialNumber { get; set; }

        public int OpdMasterId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Time in")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm tt}")]
        [Timestamp]
        public TimeSpan TimeIn { get; set; }

        [Display(Name = "Time out")]
        public TimeSpan? TimeOut { get; set; }
        public string Address { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Display(Name = "Brought By")]
        public string BroughtBy { get; set; }
        [Display(Name = "Relationship")]
        public int RelationShip { get; set; }
        [Display(Name = "Cheif Complain and History")]
        [DataType(DataType.MultilineText)]
        public string CheifComplainAndHistory { get; set; }
        [Display(Name = "Clinical Examination")]
        [DataType(DataType.MultilineText)]
        public string ClinicalExamination { get; set; }
        [Display(Name = "Final Diagnosis")]
        [DataType(DataType.MultilineText)]
        public string FinalDiagnosis { get; set; }
        [Display(Name = "Outcome Type")]
        public int OutcomeTypeId { get; set; }
        [Display(Name = "Outcome Text")]
        public string OutcomeText { get; set; }
        [Display(Name = "Discharge Condition and Instruction")]
        public string DischargeConditionAndInst { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        [DisplayName("ICD")]
        public string StringICD { get; set; }
        public int IcdCodeNumber { get; set; }
        public bool dischargeCheck { get; set; }

        [DisplayName("Refer Doctor")]
        public int ReferDoctorID { get; set; }

        public DateTime? DischargeDate { get; set; }


        public string Case { get; set; }
        public OpdEmergencyViewModel OpdEmergencyViewModel { get; set; }
        public OpdEmergencyViewDischargeModel OpdEmergencyViewDischargeModel { get; set; }
        public EmergencyInvestigationListModel EmergencyInvestigationListModel { get; set; }
        public OpdModel ObjOpdMaster { get; set; }
        public List<EmergencyInvestigationListModel> EmergencyInvestigationList { get; set; }
        public List<OpdEmergencyViewModel> OpdEmergencyViewModelList { get; set; }
        public List<OpdEmergencyViewDischargeModel> OpdEmergencyViewDischargeModelList { get; set; }
        public EmergecyMasterModel()
        {
            EmergencyTriageModelList = new List<EmergencyTriageModel>();
            EmergencyInvestigationList = new List<EmergencyInvestigationListModel>();
            ObjOpdMaster = new OpdModel();
            OpdEmergencyViewModel = new OpdEmergencyViewModel();
            OpdEmergencyViewModelList = new List<OpdEmergencyViewModel>();
            OpdEmergencyViewDischargeModel = new OpdEmergencyViewDischargeModel();
            OpdEmergencyViewDischargeModelList = new List<OpdEmergencyViewDischargeModel>();

        }
        public List<ConsulationModel> ListConsulationModels { get; set; }
        public List<EmergecyMasterModel> EmergencyMasterModelList { get; set; }
        public EmergencyTriageModel EmergencyTriageModel { get; set; }
        public List<EmergencyTriageModel> EmergencyTriageModelList { get; set; }
        public List<EmergencyFeeDetailsModel> EmergencyFeeDetailsModelList { get; set; }
        public EmergencyFeeDetailsModel EmergencyFeeDetailsModel { get; set; }
        public List<VitalsModel> ListVitalModels { get; set; }
        public VitalsModel VitalsModel { get; set; }
        public List<EmergencyTreatmentAllergiesModel> ListEmergencyTreatmentAllergiesModel { get; set; }
        public EmergencyTreatmentAllergiesModel EmergencyTreatmentAllergiesModel { get; set; }

        public List<EmergencyTstCaridModel> ListEmergencyTstCaridModel { get; set; }
        public EmergencyTstCaridModel EmergencyTstCaridModel { get; set; }
        public EmergencyInvestigationDetailModel EmergencyInvestigationDetailModel { get; set; }
        public List<EmergencyInvestigationDetailModel> ListEmergencyInvestigationDetailModel { get; set; }
        public List<EmergencyPoliceCaseModel> EmergencyPoliceCaseModelList { get; set; }



    }
    public class EmergencyTriageModel
    {

        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public int EmergencyTriageId { get; set; }

        public int? EmergencyMasterId { get; set; }
        //[DisplayFormat(DataFormatString="{0:dd-MMM-yyyy}",ApplyFormatInEditMode=true)]
        public TimeSpan? Time { get; set; }
        [DisplayName("Medium")]
        public int MediumId { get; set; }
        [DisplayName("Doctor")]
        public int DoctorId { get; set; }
        public int sourceTypeId { get; set; }
        public int SourceId { get; set; }
        public int TriageLevel { get; set; }
        [DisplayName("Medical Officer")]
        public int MedicalOfficer { get; set; }
    }
    public class EmergencyFeeDetailsModel
    {

        public int EmergencyFeeDetailsId { get; set; }
        [DisplayName("Emergency ID")]
        public int EmergencyMasterId { get; set; }
        [DisplayName("Registration Fee")]
        public decimal RegistrationFee { get; set; }
        [DisplayName("Fee Date")]
        public DateTime FeeDate { get; set; }
        [DisplayName("Doctor Fee")]
        public decimal DoctorFee { get; set; }
        [DisplayName("Member Discount %")]
        public decimal MemberDiscountAmount { get; set; }
        [DisplayName("Other Discount %")]
        public decimal OtherDiscountPercentage { get; set; }
        [DisplayName("Other Discount Ammount")]
        public decimal OtherDiscountAmount { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

    }
    public class VitalsModel
    {
        public int EmergencyVitalId { get; set; }
        public int? EmergencyMasterId { get; set; }
        public string AVPU { get; set; }
        public string Pulse { get; set; }
        public string BP { get; set; }
        public string RR { get; set; }
        public string SPO2 { get; set; }
        public string TPR { get; set; }
        public string Wt { get; set; }
        public DateTime? Date { get; set; }
        [DisplayName("Time")]
        public TimeSpan? VTime { get; set; }
        public string Staff { get; set; }
        public List<VitalsModel> ListVitalModels { get; set; }
    }
    public class ConsulationModel
    {
        public int Id { get; set; }
        public int EmergencyMasterId { get; set; }
        [DisplayName("Consulation")]
        [DataType(DataType.MultilineText)]
        public int ConsulationId { get; set; }
        [DisplayName("Text")]
        public string ConsulationText { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Progress Notes")]
        public string ProgressNotes { get; set; }
        public DateTime Time { get; set; }
        public List<ConsulationModel> ListConsulationModels { get; set; }
    }
    public class EmergencyTreatmentAllergiesModel
    {

        public int TreatmentAllergiesId { get; set; }
        public int EmergencyMasterId { get; set; }
        [Display(Name = "Treatment")]
        public string TreatmentText { get; set; }
        [Display(Name = "Allergies")]
        public string AllergiesText { get; set; }
        [Display(Name = "Procedure")]
        public string ProcedureText { get; set; }
        public TimeSpan TreatmentTime { get; set; }
        [Display(Name = "Drugs")]
        public string DrugsName { get; set; }
        public string DoseAndRoute { get; set; }
        [Display(Name = " Doctor")]
        public int DoctorId { get; set; }
        public string Staff { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public List<EmergencyTreatmentAllergiesModel> ListEmergencyTreatmentAllergiesModel { get; set; }
    }

    public class EmergencyTstCaridModel
    {
        public int EmTestd { get; set; }
        public int EmergencyMasterId { get; set; }
        [Display(Name = "Section")]
        public int SectionId { get; set; }
        [Display(Name = "Test")]
        public int TestId { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDecs { get; set; }
        [Display(Name = "Long Description")]
        public string LongDesc { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public List<EmergencyTstCaridModel> ListEmergencyTstCaridModel { get; set; }
    }
    public class EmergencyInvestigationDetailModel
    {
        public int EmergencyInvDetailId { get; set; }
        public int EmergencyMasterId { get; set; }
        [Display(Name = "Test")]
        public int TestId { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        public List<EmergencyInvestigationDetailModel> ListEmergencyInvestigationDetailModel { get; set; }

    }
    public class EmergencyInvestigationListModel
    {
        [DisplayName("Test Name")]
        public int? TestID { get; set; }
        public string Remarks { get; set; }
    }


    public class OpdEmergencyViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool dischargeCheck { get; set; }
        public int? PatientId { get; set; }
        public string PatientTitle { get; set; }
        public string PatientTitleD { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FirstnameD { get; set; }
        public string MiddleNameD { get; set; }
        public string LastNameD { get; set; }
        public string Gender { get; set; }
        public int? AgeYear { get; set; }
        [Timestamp]
        public TimeSpan? TimeIn { get; set; }
        [Timestamp]
        public TimeSpan? TimeOut { get; set; }
        public string GenderD { get; set; }
        public int? AgeYearD { get; set; }
        [Timestamp]
        public TimeSpan? TimeOutD { get; set; }
        public int? EmergencyId { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }

        public List<OpdEmergencyViewModel> OpdEmergencyViewModelList { get; set; }
    }
    public class OpdEmergencyViewDischargeModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool dischargeCheck { get; set; }
        public string PatientTitle { get; set; }

        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }
        public int? AgeYear { get; set; }
        [Timestamp]
        public TimeSpan? TimeIn { get; set; }


        [Timestamp]
        public TimeSpan? TimeOut { get; set; }
        public int? EmergencyId { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }

        public List<OpdEmergencyViewDischargeModel> OpdEmergencyViewDischargeModelList { get; set; }
    }



    public class StaffForERegisModel
    {
        public string EmployeeCode { get; set; }
        public string EmployeeFullName { get; set; }
    }
    public class EmergencyPoliceCaseModel
    {
        public int PCId { get; set; }
        public DateTime Date { get; set; }
        [Timestamp]
        [Display(Name = "Time")]
        public TimeSpan TimeIn { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        [Display(Name = "Case")]
        public int PatientCase { get; set; }
        [Display(Name = "Remarks 1")]
        [DataType(DataType.MultilineText)]
        public string RemarksA { get; set; }
        [Display(Name = "Remarks 2")]
        [DataType(DataType.MultilineText)]
        public string RemarksB { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }

        public List<EmergencyPoliceCaseModel> EmergencyPoliceCaseModelList { get; set; }
    }




}