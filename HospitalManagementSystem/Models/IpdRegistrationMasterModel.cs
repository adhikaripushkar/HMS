using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class IpdRegistrationMasterModel
    {

        public int IpdRegistrationID { get; set; }
        [Required(ErrorMessage = "Requreds")]
        [DisplayName("Patient Id:")]
        public int OpdNoEmergencyNo { get; set; }
        [Required(ErrorMessage = "Registration Date Mandatory")]
        [DisplayName("Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        public Boolean Status { get; set; }
        public int CreatedtBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [DisplayName("ICD")]
        public string StringICD { get; set; }
        public int IcdCodeNumber { get; set; }
        public int passvalues { get; set; }
        public int PassDefaultValue { get; set; }

        public int ajaxOPdEmNumner { get; set; }
        public int ajaxSelectedVlue { get; set; }

        public string patientName { get; set; }

        //sNotMapped This Proparties
        public int? AgeYear { get; set; }
        public int? AgeMont { get; set; }
        public int? AgeDay { get; set; }
        public string Gender { get; set; }
        public int BedNo { get; set; }
        public string Birth { get; set; }

        public int id { get; set; }

        public decimal DepositeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Diagnosisid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ReferDoctorId { get; set; }

        [Required(ErrorMessage = "***")]
        public int RoomTypeRequired { get; set; }


        public List<IpdRegistrationMasterModel> IpdRegistrationMasterModelList { get; set; }
        public List<IpdPatientroomDetailsModel> IpdPatientroomDetailsModelList { get; set; }
        public List<IpdPatientBedDetailModel> IpdPatientbedDetialModelList { get; set; }
        public IpdRegistrationMasterModel IpdRegistrationMasterModelObj { get; set; }
        public IpdPatientroomDetailsModel IpdPatientroomDetailsModel { get; set; }
        public IpdPatientBedDetailModel IpdPatientBedDetailsModel { get; set; }
        public List<OpdModel> opdModelList { get; set; }
        public List<EmergecyMasterModel> EmergencyModelList { get; set; }

        public IPDWardDetailsViewModel ObjIPDWardDetailsViewModel { get; set; }
        public List<IPDWardDetailsViewModel> IPDWardDetailsViewModelList { get; set; }



        public OpdModel refOpdModel { get; set; }
        public EmergecyMasterModel refOfEmergecyMasterModel { get; set; }

        public IpdSearchResults refOfIpdSearchResults { get; set; }

        public IpdPartialDetails objIpdPartialDetails { get; set; }
        public List<IpdPartialDetails> IpdPartialDetailsList { get; set; }


        public ShiftFromIPDViewModel ObjShiftFromIPDViewModel { get; set; }
        public List<ShiftFromIPDViewModel> ShiftFromIPDViewModelList { get; set; }

        public List<IpdPatientDiagnosisDetailsModel> ipdPatientDiagnosisDetailList { get; set; }
        public IpdPatientDiagnosisDetailsModel ObjIpdPatientDiagnosisDetailsModel { get; set; }

        public IpdRegistrationMasterModel()
        {
            //IpdRegistrationMasterModelObj = new Models.IpdRegistrationMasterModel();
            //IpdRegistrationMasterModelList = new List<IpdRegistrationMasterModel>();

            ObjIpdPatientDiagnosisDetailsModel = new IpdPatientDiagnosisDetailsModel();
            ipdPatientDiagnosisDetailList = new List<IpdPatientDiagnosisDetailsModel>();

        }


    }

    public class IpdPatientBedDetailModel
    {
        public int IpdPatientBedDetailId { get; set; }
        [Required(ErrorMessage = "Requreds")]
        public int IpdRegistrationId { get; set; }
        public int OpdEmergencyNumber { get; set; }
        [DisplayName("Bed Number")]
        public int BedNumber { get; set; }
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Requred Date")]
        [DisplayName("Adminted Date")]
        public DateTime AdmitedDate { get; set; }
        [DisplayName("Leave Date")]
        public DateTime? LeaveDate { get; set; }
        public Boolean Status { get; set; }
    }
    public class IpdPatientroomDetailsModel
    {
        public int IpdPatientRoomDetailId { get; set; }
        public int IpdRegistrationID { get; set; }
        [Required(ErrorMessage = "Requreds")]
        public int OpdNoEmergencyNo { get; set; }
        [DisplayName("Ward No:")]
        public int WardNo { get; set; }
        [Required(ErrorMessage = "***")]
        [DisplayName("Room Type:")]
        public int RoomType { get; set; }
        [DisplayName("Room No:")]
        public int RoomNo { get; set; }
        [DisplayName("Bed No:")]
        public int BedNo { get; set; }
        public Boolean Status { get; set; }
    }
    public class OpdIpdModel
    {
        public int? OpdIpdId { get; set; }
        [DisplayName("OPDId/EmergencyID")]
        public int? OpdEmergencyId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }

        public List<OpdIpdModel> OpdIpdModelList { get; set; }

    }


    public class IpdSearchResults
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistrationDate { get; set; }

        public int OpdId { get; set; }

        public int BedNo { get; set; }
        public string FullName { get; set; }

        public int DepartmentId { get; set; }

        public int RoomNo { get; set; }

        public int WardNo { get; set; }

        public int RoomType { get; set; }
        public int IpNo { get; set; }
        public int Values { get; set; }
        public int Unit { get; set; }
        public string sex { get; set; }
        public int? Age { get; set; }
        public int? PatientName { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public int Diagnosisid { get; set; }

        public DateTime? DateOfDischarge { get; set; }


        public int OpdEmrNumber { get; set; }
        public int IpdRegistrationNumber { get; set; }

        //public int? AgeYear { get; set; }
        //public int? AgeMont { get; set; }
        //public int? AgeDay { get; set; }
        //public string Gender { get; set; }
        //public int BedNo { get; set; }
        public string Birth { get; set; }

        public List<IpdSearchResults> IpdSearchResultList { get; set; }
        public List<IpdMedicalRecord> IpdMedecalRecordList { get; set; }
        public List<IpdMRCommonTestModel> IpdMRCommontestList { get; set; }
        public List<IpdMrMainTestModel> IpdMrMainTestList { get; set; }
        public List<IpdRegistrationMasterModel> IpdRegistrationMasterModelList { get; set; }
        public List<IpdDischargeModel> IpdDischargeModelList { get; set; }
        public List<OperationTheatreMasterModel> OperationTheatreMasterModelList { get; set; }

        public IpdMRCommonTestModel ObjIpdMRCommonTestModel { get; set; }
        public IpdMrMainTestModel ObjIpdMRMainTestModel { get; set; }
        public IpdMrMedicineRecord ObjIpdMRMedicalRecord { get; set; }
        public IpdRegistrationMasterModel IpdRegistrationMasterModel { get; set; }
        public IpdDischargeModel ObjDischargeModel { get; set; }
        public OperationTheatreMasterModel ObjOperationTheatreMasterModel { get; set; }


        public IpdSearchResults()
        {
            ObjIpdMRCommonTestModel = new IpdMRCommonTestModel();
            IpdMRCommontestList = new List<IpdMRCommonTestModel>();
            ObjIpdMRMainTestModel = new IpdMrMainTestModel();
            IpdMrMainTestList = new List<IpdMrMainTestModel>();
            ObjIpdMRMedicalRecord = new IpdMrMedicineRecord();
            IpdMedecalRecordList = new List<IpdMedicalRecord>();
            IpdRegistrationMasterModelList = new List<IpdRegistrationMasterModel>();
            IpdDischargeModelList = new List<IpdDischargeModel>();
            ObjDischargeModel = new IpdDischargeModel();
            OperationTheatreMasterModelList = new List<OperationTheatreMasterModel>();
            ObjOperationTheatreMasterModel = new OperationTheatreMasterModel();

        }


    }
    public class IpdDischargeModel
    {
        public int IpdDischargeID { get; set; }
        public int ipdResistrationID { get; set; }
        public int? DignosisID { get; set; }
        public string BriefHistory { get; set; }
        public string ClinicalFindings { get; set; }
        public string Investigation { get; set; }
        public string Medicinetreatment { get; set; }
        public string FurtherTreatment { get; set; }
        public string FoloowUp { get; set; }

        public int? AgeYear { get; set; }
        public int? AgeMont { get; set; }
        public int? AgeDay { get; set; }
        public string Gender { get; set; }
        public int BedNo { get; set; }
        public string Birth { get; set; }
        public string patientName { get; set; }
        public DateTime? LeaveDate { get; set; }

        public IpdPatientDiagnosisDetailsModel refofDiagnosisDetails { get; set; }
        public Nullable<int> IpdPatientRoomDetailId { get; set; }
        public Nullable<int> IpdPatientBedDetailId { get; set; }

        public string Diagnosis { get; set; }
        public int Opdid { get; set; }

        public IpdRegistrationMasterModel refOfIpdRegistrationMasterModel { get; set; }

        public List<IpdDischargeModel> IpdDischargeList { get; set; }
    }

    public class IpdPartialDetails
    {
        public int? OpdID { get; set; }
        public int? OpdNoEmergencyNo { get; set; }
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Admitted date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Admitdate { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Ward No")]
        public int WardNo { get; set; }
        [DisplayName("Room Type")]
        public int RoomType { get; set; }
        [DisplayName("Room No")]
        public int? RoomNo { get; set; }
        [DisplayName("Bed No")]
        public int? BedNo { get; set; }
        public string Address { get; set; }
        public List<IpdPartialDetails> IpdPartialDetailsList { get; set; }
    }

    public class IpdPatientDiagnosisDetailsModel
    {
        public int IpdPatientDiagnosisDetailID { get; set; }
        public int IpdRegistrationID { get; set; }
        public int DiagnosisID { get; set; }
        public string DiagnosisName { get; set; }


        public List<IpdPatientDiagnosisDetailsModel> lstOfDiagnosisDetails { get; set; }
    }

    public class IPDWardDetailsViewModel
    {
        public int IpdRegistrationID { get; set; }
        public int OpdNoEmergencyNo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int RoomType { get; set; }
        public int BedNo { get; set; }
        public int WardNo { get; set; }
        public int RoomNo { get; set; }

        public List<IPDWardDetailsViewModel> IPDWardDetailsViewModelList { get; set; }

    }

    public class ShiftFromIPDViewModel
    {
        public int ShiftFromIPDId { get; set; }
        public int OpdID { get; set; }
        public int IPDRegistrationID { get; set; }
        public int ShiftedDepartmentId { get; set; }
        public Boolean Status { get; set; }
        public DateTime ShiftedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remarks { get; set; }
        public int DepartmentID { get; set; }
        public string Narration { get; set; }

    }

}