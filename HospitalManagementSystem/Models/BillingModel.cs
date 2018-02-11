using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BillingModel
    {

        public int BillingID { get; set; }
        [DisplayName("Department Name")]
        public int DepartmentID { get; set; }

        [DisplayName("Narration")]
        public string Narration1 { get; set; }

        public decimal Amount { get; set; }

        public string Dr_Cr { get; set; }

        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndTransactionDate { get; set; }
        [DisplayName("Ledger Name")]
        public string LedgerName { get; set; }
        [DisplayName("OPD ID")]
        public int OpdId { get; set; }

        public string PainName { get; set; }
        public DateTime TransectionDate { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        public int RoomType { get; set; }
        public int PatientLogId { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public List<CentralizeBillingViewModel> CentralizeBillingViewModelList { get; set; }
        public List<BillingModel> BillingModelList { get; set; }

        public List<DischargeBillingDeposite> DischargeBillingDepositeList { get; set; }
        public List<DischargeBillingSummaryPatho> DischargeBillingSummaryPathoList { get; set; }
        public List<DischargeBillingSummaryOT> DischargeBillingSummaryOTist { get; set; }
        public List<DischargeBillingSummaryOpd> DischargeBillingSummaryOpdist { get; set; }
        public List<DischargeBillingSummaryDental> DischargeBillingSummaryDentalist { get; set; }
        public List<DischargeBillingSummaryEmergency> DischargeBillingSummaryEmergencyist { get; set; }
        public List<DischargeBillingSummaryEye> DischargeBillingSummaryEyeList { get; set; }
        public List<DischargeBillingSummaryIpd> DischargeBillingSummaryIpdist { get; set; }
        public List<GetBillDetailByBillNoViewModel> GetBillDetailByBillNoViewModelList { get; set; }

        public GetBillDetailByBillNoViewModel ObjGetBillDetailByBillNoViewModel { get; set; }



        public DischargeBillingDeposite ObjDischargeBillingDeposite { get; set; }
        public DischargeBillingSummaryPatho ObjDischargeBillingSummaryPatho { get; set; }
        public DischargeBillingSummaryDental ObjDischargeBillingSummaryDental { get; set; }
        public DischargeBillingSummaryOT ObjDischargeBillingSummaryOT { get; set; }
        public DischargeBillingSummaryOpd ObjDischargeBillingSummaryOpd { get; set; }
        public DischargeBillingSummaryEmergency ObjDischargeBillingSummaryEmergency { get; set; }
        public DischargeBillingSummaryIpd ObjDischargeBillingSummaryIpd { get; set; }
        public DischargeBillingSummaryEye ObjDischargeBillingSummaryEye { get; set; }



        public List<DepositUsedRemainViewModel> DepositUsedRemainViewModelList { get; set; }
        public DepositUsedRemainViewModel ObjDepositUsedRemainViewModel { get; set; }


        public List<DepositUsedViewModel> DepositUsedViewModelList { get; set; }
        public DepositUsedViewModel ObjDepositUsedViewModel { get; set; }
        public CentralizeBillingViewModel ObjCentralizeBillingViewModel { get; set; }
        public List<CentralizeBillingViewModel> CentralizedBillingDetailsList { get; set; }






        public BillingModel()
        {
            ObjDischargeBillingDeposite = new DischargeBillingDeposite();
            ObjDischargeBillingSummaryPatho = new DischargeBillingSummaryPatho();
            ObjDischargeBillingSummaryDental = new DischargeBillingSummaryDental();
            ObjDischargeBillingSummaryOT = new DischargeBillingSummaryOT();
            ObjDischargeBillingSummaryOpd = new DischargeBillingSummaryOpd();
            ObjDischargeBillingSummaryEmergency = new DischargeBillingSummaryEmergency();
            ObjDischargeBillingSummaryIpd = new DischargeBillingSummaryIpd();
            ObjDischargeBillingSummaryEye = new DischargeBillingSummaryEye();
            ObjGetBillDetailByBillNoViewModel = new GetBillDetailByBillNoViewModel();


            DischargeBillingSummaryIpdist = new List<DischargeBillingSummaryIpd>();
            DischargeBillingSummaryEyeList = new List<DischargeBillingSummaryEye>();
            DischargeBillingSummaryEmergencyist = new List<DischargeBillingSummaryEmergency>();
            DischargeBillingSummaryDentalist = new List<DischargeBillingSummaryDental>();
            DischargeBillingSummaryOpdist = new List<DischargeBillingSummaryOpd>();
            DischargeBillingSummaryOTist = new List<DischargeBillingSummaryOT>();
            DischargeBillingSummaryPathoList = new List<DischargeBillingSummaryPatho>();
            DischargeBillingDepositeList = new List<DischargeBillingDeposite>();
            GetBillDetailByBillNoViewModelList = new List<GetBillDetailByBillNoViewModel>();

            ObjDepositUsedRemainViewModel = new DepositUsedRemainViewModel();
            DepositUsedRemainViewModelList = new List<DepositUsedRemainViewModel>();

            ObjDepositUsedViewModel = new DepositUsedViewModel();
            DepositUsedViewModelList = new List<DepositUsedViewModel>();

            ObjCentralizeBillingViewModel = new CentralizeBillingViewModel();
            CentralizeBillingViewModelList = new List<CentralizeBillingViewModel>();
        }


    }

    public class CentralizeBillingViewModel
    {
        public decimal TotalAmount { get; set; }
        public int PatientId { get; set; }
        public string LedgerName { get; set; }
        public DateTime AmountDate { get; set; }
        public string BillNumber { get; set; }
        public string Narration1 { get; set; }
        public string Narration2 { get; set; }
        public int PatientLogId { get; set; }
        public string DepartmentName { get; set; }
        public int BillNumberInteger { get; set; }
        public int CreatedBy { get; set; }

        public int Age { get; set; }


        public List<CentralizeBillingViewModel> CentralizedBillingDetailsList { get; set; }

    }

    public class DischargeBillingDeposite
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingDeposite> DischargeBillingDepositeList { get; set; }

    }

    public class DischargeBillingSummaryPatho
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryPatho> DischargeBillingSummaryPathoList { get; set; }

    }

    public class DischargeBillingSummaryOT
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryOT> DischargeBillingSummaryOTist { get; set; }

    }

    public class DischargeBillingSummaryIpd
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryIpd> DischargeBillingSummaryIpdist { get; set; }

    }

    public class DischargeBillingSummaryOpd
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryOpd> DischargeBillingSummaryOpdist { get; set; }

    }
    public class DischargeBillingSummaryEmergency
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryEmergency> DischargeBillingSummaryEmergencyist { get; set; }

    }

    public class DischargeBillingSummaryDental
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryDental> DischargeBillingSummaryDentalist { get; set; }


    }

    public class DischargeBillingSummaryEye
    {
        public int PatientId { get; set; }
        public string DepartmentId { get; set; }
        public int PatientLogId { get; set; }
        public string PatientFullName { get; set; }
        public int AccountHeadId { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<DischargeBillingSummaryEye> DischargeBillingSummaryEyeList { get; set; }

    }

    public class GetBillDetailByBillNoViewModel
    {
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public List<GetBillDetailByBillNoViewModel> GetBillDetailByBillNoViewModelList { get; set; }

    }
    public class DepositUsedRemainViewModel
    {
        public int AccountSubHeadId { get; set; }
        public decimal Deposit { get; set; }
        public decimal UsedDeposit { get; set; }
        public decimal RemDepositAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DepositUsedRemainViewModel> DepositUsedRemainViewModelList { get; set; }
    }

    public class DepositUsedViewModel
    {
        public string DepoParticulars { get; set; }
        public DateTime DepoBillDate { get; set; }
        public decimal DepoBillAmount { get; set; }
        public List<DepositUsedViewModel> DepositUsedViewModelList { get; set; }
    }


}