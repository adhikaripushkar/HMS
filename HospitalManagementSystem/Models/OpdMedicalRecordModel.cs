using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Models
{
    public class OpdMedicalRecordModel
    {
        public int OpdMedicalRecordMastetId { get; set; }
        [Display(Name = "Patinet Id")]
        public int PatientId { get; set; }
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Display(Name = "Doctor Id")]
        public int DoctorId { get; set; }
        [Display(Name = "Visited Date")]
        public DateTime VisitedDate { get; set; }
        [Display(Name = "Previous Case")]
        public string PreviousCase { get; set; }

        [Display(Name = "Current Case")]
        public string CurrentCase { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Provisional Diagnosis")]
        public string ProvisionalDiagnosis { get; set; }

        public int PatientDoctorDetailID { get; set; }

        public Nullable<int> PatientLogId { get; set; }

        public OpdMedicalRecordModel()
        {
            PatientCommonTestList = new List<OpdMedicalRecordCommonTestModel>();
            PatientFurtherTestList = new List<OpdMedicalRecordFurtherTestModel>();
            PatientMedicineDosesList = new List<OpdMedicalRecordMedicineReferModel>();
            OpdMedicalRecordCommonTestModel CommonTestModel = new OpdMedicalRecordCommonTestModel();
        }

        public List<OpdMedicalRecordCommonTestModel> PatientCommonTestList { get; set; }
        public List<OpdMedicalRecordFurtherTestModel> PatientFurtherTestList { get; set; }
        public List<OpdMedicalRecordMedicineReferModel> PatientMedicineDosesList { get; set; }
        public List<TodayListViewModel> todayList { get; set; }
        public List<OpdMedicalRecordListViewModel> OpdMedicalRecordListViewModelList { get; set; }
        public List<OpdMedicalRecordModel> OpdMedicalRecordModelList { get; set; }

        public OpdMedicalRecordCommonTestModel CommonTestModel { get; set; }
        public TodayListViewModel TodayListViewModel { get; set; }
        public OpdMedicalRecordFurtherTestModel FurtherTestModel { get; set; }
        public OpdMedicalRecordMedicineReferModel MedicineReferModel { get; set; }
        public OpdMedicalRecordListViewModel OpdMedicalRecordListViewModel { get; set; }

        public VitalForOthersModel refOfVitalForOthersModel { get; set; }



    }


    public class TodayListViewModel
    {
        public int? PatinetId { get; set; }
        public int? PatientDoctorId { get; set; }
        public String PatientName { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public int? DoctorId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime PrefDate { get; set; }
        public int PatientlogId { get; set; }
        public int OpdMedicalRecordMasterId { get; set; }

        public List<TodayListViewModel> todayList { get; set; }

        public int count { get; set; }



        //OpdMedicalRecordModel OMRModel = new OpdMedicalRecordModel();

    }

    public class OpdMedicalRecordCommonTestModel
    {
        public int OpdMRCommonTestId { get; set; }
        public int OpdMedicalRecordMastetId { get; set; }
        public int PatientID { get; set; }



        [DisplayName("Sort Description")]
        public string ShortDesc { get; set; }


        [DisplayName("Details")]
        public string Details { get; set; }

        public DateTime InsertedDate { get; set; }

        public List<OpdMedicalRecordCommonTestModel> PatientCommonTestList { get; set; }

    }

    public class OpdMedicalRecordFurtherTestModel
    {
        public int OpdMRFurtherTestId { get; set; }
        public int OpdMedicalRecordMastetId { get; set; }
        public int PatientId { get; set; }
        public string TestName { get; set; }

    }

    public class OpdMedicalRecordMedicineReferModel
    {
        public int OpdMRMedicineReferId { get; set; }
        public int OpdMedicalRecordMastetId { get; set; }
        public int PatientId { get; set; }
        public string MedicineName { get; set; }
        public string Doses { get; set; }
        public string DosesDay { get; set; }
        public string DosesTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }


    }


    public class OpdMedicalRecordListViewModel
    {
        public int OpdMedicalRecordMastetId { get; set; }
        public int OpdId { get; set; }
        public string PatientFullName { get; set; }
        public int? PatientAge { get; set; }
        public string PatientAddress { get; set; }


        public List<OpdMedicalRecordListViewModel> OpdMedicalRecordListViewModelList { get; set; }

    }

    public class opdOldPatientMedicalRecords
    {

        //select opd.OpdID,omrm.PatientDoctorDetailID,opdd.DepartmentID,opdd.DoctorID,opdd.PreferDate,omrm.OpdMedicalRecordMastetId from OpdMaster opd join 
        //OpdPatientDoctorDetails opdd on opdd.OpdID=opd.OpdID join OpdMedicalRecordMaster omrm on opdd.PatientDoctorDetailID=omrm.PatientDoctorDetailID where opd.OpdID=5

        public int OpdId { get; set; }
        public int PatientDoctorDetailID { get; set; }
        public int DepartmentID { get; set; }
        public int DoctorID { get; set; }
        public DateTime PreferDate { get; set; }
        public string PreviousCase { get; set; }
        public string CurrentCase { get; set; }
        public int OpdMedicalRecordMastetId { get; set; }
        public int PatientLogId { get; set; }

        public List<opdOldPatientMedicalRecords> lstOfOpdOldPatientMedicalRecords { get; set; }

        public List<opdOldPatientMedicalRecords> GetOldPatientMedicalRecordsWithPatientId(int id)
        {

            EHMSEntities ent = new EHMSEntities();

            return ent.Database.SqlQuery<opdOldPatientMedicalRecords>("SPOplPatientMedicalRecordsWithPatientId " + id).ToList();

        }



    }


    public class VitalForOthersModel
    {

        public int VitalForOtherId { get; set; }
        public int OpdId { get; set; }
        public string AVPU { get; set; }
        public string Pulse { get; set; }
        public string BP_Right { get; set; }
        public string BP_Left { get; set; }
        public string RR { get; set; }
        public string SPO2 { get; set; }
        public string TPR { get; set; }
        public Nullable<int> Wt { get; set; }
        public Nullable<int> Feet { get; set; }
        public Nullable<int> Inch { get; set; }
        public Nullable<decimal> BMI { get; set; }
        public string Date { get; set; }
        public string VTime { get; set; }
        public string Staff { get; set; }
        public string Department { get; set; }
        public Nullable<int> PatinetLogId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string ElergeryToDrugs { get; set; }
        public string BP { get; set; }


        public string height { get; set; }

        public List<VitalForOthersModel> lstOfVitalForOthersModel { get; set; }


    }





}