using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class MisModel
    {
        //Opd Begin
        public string OpdId { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string Email { get; set; }

        public decimal? Amount { get; set; }
        //Opd End

        //Operation theatre

        public int OperationTheatreId { get; set; }

        public DateTime OperationDate { get; set; }

        public TimeSpan OperationTime { get; set; }

        public string OperationRoom { get; set; }

        public TimeSpan? OperationEndTime { get; set; }

        public string PatientName { get; set; }

        public decimal? TotalCharge { get; set; }

        public DateTime? FeeDate { get; set; }
        //End Operation theatre
        //Emergency

        public int EmergencyNumber { get; set; }

        public int SerialNumber { get; set; }

        public string NameEr { get; set; }

        public int Age { get; set; }

        public string SexEr { get; set; }

        public DateTime Date { get; set; }

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }

        public string Address { get; set; }

        public string BroughtBy { get; set; }

        public string RelationShip { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public List<PathoTestDetailViewModel> PathoTestDetailViewModelList { get; set; }
        public PathoTestDetailViewModel ObjPathoTestDetailViewModel { get; set; }


        public IPDetailViewModel ObjIPDetailViewModel { get; set; }
        public List<IPDetailViewModel> IPDetailViewModelList { get; set; }


        public MRRecordViewModel ObjMRRecordViewModel { get; set; }
        public List<MRRecordViewModel> MRRecordViewModelList { get; set; }


        public PieChartDepartmentswiseViewModel ObjPieChartDepartmentswiseViewModel { get; set; }
        public List<PieChartDepartmentswiseViewModel> PieChartDepartmentswiseViewModelList { get; set; }


        public PayablePatientReportViewModel ObjPayablePatientReportViewModel { get; set; }
        public List<PayablePatientReportViewModel> PayablePatientReportViewModelList { get; set; }


        public DoctorCommissionViewModel ObjDoctorCommissionViewModel { get; set; }
        public List<DoctorCommissionViewModel> DoctorCommissionViewModelList { get; set; }

        public GeneralPayableReportModel ObjGeneralPayableReportModel { get; set; }
        public List<GeneralPayableReportModel> GeneralPayableReportModelList { get; set; }


        public EHSDoctorDetailViewModel ObjEHSDoctorDetailViewModel { get; set; }
        public List<EHSDoctorDetailViewModel> EHSDoctorDetailViewModelList { get; set; }


        public BillAmountDifference ObjBillAmountDifferenceModelList { get; set; }
        public List<BillAmountDifference> BillAmountDifferenceModelList { get; set; }

        public OpdDepartmentWiseReportVM ObjOpdDepartmentWiseReportVM { get; set; }
        public List<OpdDepartmentWiseReportVM> OpdDepartmentWiseReportVMList { get; set; }


        public MisModel()
        {
            PathoTestDetailViewModelList = new List<PathoTestDetailViewModel>();
            ObjPathoTestDetailViewModel = new PathoTestDetailViewModel();
            ObjIPDetailViewModel = new IPDetailViewModel();
            IPDetailViewModelList = new List<IPDetailViewModel>();

            ObjMRRecordViewModel = new MRRecordViewModel();
            MRRecordViewModelList = new List<MRRecordViewModel>();

            ObjPieChartDepartmentswiseViewModel = new PieChartDepartmentswiseViewModel();
            PieChartDepartmentswiseViewModelList = new List<PieChartDepartmentswiseViewModel>();


            ObjPayablePatientReportViewModel = new PayablePatientReportViewModel();
            PayablePatientReportViewModelList = new List<PayablePatientReportViewModel>();


            ObjDoctorCommissionViewModel = new DoctorCommissionViewModel();
            DoctorCommissionViewModelList = new List<DoctorCommissionViewModel>();

            ObjEHSDoctorDetailViewModel = new EHSDoctorDetailViewModel();
            EHSDoctorDetailViewModelList = new List<EHSDoctorDetailViewModel>();

            ObjOpdDepartmentWiseReportVM = new OpdDepartmentWiseReportVM();
            OpdDepartmentWiseReportVMList = new List<OpdDepartmentWiseReportVM>();

        }


        //Emergency
    }

    public class PathoTestDetailViewModel
    {
        public string TestName { get; set; }
        public string SectionName { get; set; }
        public int AccountSubHeadId { get; set; }
        public int Total { get; set; }


    }
    public class IPDetailViewModel
    {
        public int IPId { get; set; }
        public int OpdId { get; set; }
        public DateTime RegisterDate { get; set; }
        public int ReferDocId { get; set; }

    }

    public class MRRecordViewModel
    {
        public int MedicalRecordId { get; set; }
        public int OpdId { get; set; }
        public int IPId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public Boolean Status { get; set; }
        public string ConditionAtDischarge { get; set; }
        public int ConditionAtDischargeId { get; set; }
        public int ICDCode { get; set; }
        public int DiagnosisCode { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public int BranchId { get; set; }
        public int DischargeDocId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ICDName { get; set; }

    }

    public class PieChartDepartmentswiseViewModel
    {
        public int TotalPatient { get; set; }
        public string DeptName { get; set; }
        public int DepartmentID { get; set; }

    }

    public class PayablePatientReportViewModel
    {
        public int TotalPatient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal GrandTotalAmount { get; set; }
        public string PatientFullName { get; set; }

    }

    public class DoctorCommissionViewModel
    {
        public int DoctorId { get; set; }
        public int CommisionType { get; set; }
        public decimal DoctorCommission { get; set; }
        public decimal AnesthiaCommission { get; set; }
        public decimal SurgeonCommission { get; set; }
        public decimal PathologistCommission { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CommissionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedUser { get; set; }
        public string CommissionName { get; set; }
        public int BillNo { get; set; }
        public string DoctorName { get; set; }


    }

    public class EHSDoctorDetailViewModel
    {
        public int DoctorId { get; set; }
        public int TestId { get; set; }
        public string Particulars { get; set; }
        public decimal? FeeAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? Billnumber { get; set; }
        public string CommissionName { get; set; }
    }

    public class GeneralPayableReportModel
    {
        public int BillNo { get; set; }
        public string BillDate { get; set; }
        public string Patient { get; set; }
        public string Type { get; set; }
        public decimal TotalBillAmount { get; set; }
    }
    public class BillAmountDifference
    {
        public int BillNo { get; set; }
        public string BillDate { get; set; }
        public decimal DetailAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal DifferenceAmount { get; set; }
    }
    public class OpdDepartmentWiseReportVM
    {
        public string DeptName { get; set; }
        public int DepartmentID { get; set; }
        public int Total { get; set; }
    }
}