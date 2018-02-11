using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class JVMasterModel
    {
        public int JvMasterId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string JvNumber { get; set; }
        public string BillNumber { get; set; }
        public int Count { get; set; }
        public int AccountHeadId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Dr { get; set; }
        public string Cr { get; set; }
        public int FiscalYearId { get; set; }
        public string AccountNumber { get; set; }
        public string Narration1 { get; set; }
        public string Narration2 { get; set; }
        public string JvType { get; set; }
        public int VoucharNum { get; set; }

        public GetAllTrialBalanceModelView objGetAllTrialBalanceModelView { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList1 { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList2 { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList3 { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList4 { get; set; }
        public List<GetAllTrialBalanceModelView> objGetAllTrialBalanceModelViewList5 { get; set; }
        public List<BillingDetailViewModel> BillingDetailViewModelList { get; set; }
        public List<BillingDetailViewModel> BillingDetailViewModelListNew { get; set; }
        public List<JVMasterModel> JVMasterModelList { get; set; }
        public List<JVDetailModel> JVDetailsModelList { get; set; }
        public List<JournalVoucharViewModel> JournalVoucharViewModelList { get; set; }
        public List<LedgerDetailsViewModel> LedgerDetailsViewModelList { get; set; }
        public List<LedgerViewModelAccountHeadWise> LedgerViewModelAccountHeadWiseList { get; set; }
        public BillingDetailViewModel objBillingDetailViewModel { get; set; }
        public JVDetailsViewModel ObjJVDetailsViewModel { get; set; }
        public LedgerDetailsViewModel ObjLedgerDetailsViewModel { get; set; }
        public LedgerViewModelAccountHeadWise ObjLedgerViewModelAccountHeadWise { get; set; }
        public JournalVoucharViewModel ObjJournalVoucharViewModel { get; set; }
        public AddMoreParticularsJVModel ObjAddMoreParticularsJVModel { get; set; }
        public List<AddMoreParticularsJVModel> AddMoreParticularsJVModelList { get; set; }
        public GetTrialBalanceModelView objGetTrialBalanceModelView { get; set; }
        public List<GetTrialBalanceModelView> objGetTrialBalanceModelViewList { get; set; }
        public GetBilldetailByUserModelView objGetBilldetailByUserModelView { get; set; }
        public List<GetBilldetailByUserModelView> objGetBilldetailByUserModelViewList { get; set; }
        public GetBilldetailByDepartmentModelView objGetBilldetailByDepartmentModelView { get; set; }
        public List<GetBilldetailByDepartmentModelView> objGetBilldetailByDepartmentModelViewList { get; set; }
        public GetCashHandoverDetailModelView objGetCashHandoverDetailModelView { get; set; }
        public List<GetCashHandoverDetailModelView> objGetCashHandoverDetailModelViewList { get; set; }
        public HandoverDetailReportModelView objHandoverDetailReportModelView { get; set; }
        public List<HandoverDetailReportModelView> objHandoverDetailReportModelViewList { get; set; }
        public NotificationHandoverModelView objNotificationHandoverModelView { get; set; }
        public List<NotificationHandoverModelView> objNotificationHandoverModelViewList { get; set; }
        public OpeningBalanceModelView objOpeningBalanceModelView { get; set; }
        public List<OpeningBalanceModelView> objOpeningBalanceModelViewList { get; set; }
        public PLScheduleModelView objPLScheduleModelView { get; set; }
        public List<PLScheduleModelView> objPLScheduleModelViewList { get; set; }
        public BSScheduleModelView objBSScheduleModelView { get; set; }
        public List<BSScheduleModelView> objBSScheduleModelViewList { get; set; }
        public List<BSScheduleModelView> objBSScheduleModelViewList1 { get; set; }
        public BalanceSheetModelView objBalanceSheetModelView { get; set; }
        public List<BalanceSheetModelView> objBalanceSheetModelViewList { get; set; }

        public IncomeAndExpenditureModelView objIncomeAndExpenditureModelView { get; set; }
        public List<IncomeAndExpenditureModelView> objIncomeAndExpenditureModelViewList { get; set; }

        public GetBankReconcilationModelView objGetBankReconcilationModelView { get; set; }
        //        public List<GetBankReconcilationModelView> objGetBankReconcilationModelViewList { get; set; }

        public JVMasterModel()
        {
            ObjJournalVoucharViewModel = new JournalVoucharViewModel();
            ObjLedgerDetailsViewModel = new LedgerDetailsViewModel();
            ObjAddMoreParticularsJVModel = new AddMoreParticularsJVModel();
            LedgerDetailsViewModelList = new List<LedgerDetailsViewModel>();
            ObjLedgerViewModelAccountHeadWise = new LedgerViewModelAccountHeadWise();
            LedgerViewModelAccountHeadWiseList = new List<LedgerViewModelAccountHeadWise>();
            AddMoreParticularsJVModelList = new List<AddMoreParticularsJVModel>();
            JVMasterModelList = new List<JVMasterModel>();
            objGetTrialBalanceModelView = new GetTrialBalanceModelView();
            objGetTrialBalanceModelViewList = new List<GetTrialBalanceModelView>();
            objGetBilldetailByUserModelView = new GetBilldetailByUserModelView();
            objGetBilldetailByUserModelViewList = new List<GetBilldetailByUserModelView>();
            objGetCashHandoverDetailModelViewList = new List<GetCashHandoverDetailModelView>();
            objHandoverDetailReportModelViewList = new List<HandoverDetailReportModelView>();
            objNotificationHandoverModelViewList = new List<NotificationHandoverModelView>();
            objOpeningBalanceModelViewList = new List<OpeningBalanceModelView>();
            objGetAllTrialBalanceModelViewList = new List<GetAllTrialBalanceModelView>();
            objGetAllTrialBalanceModelView = new GetAllTrialBalanceModelView();
            objPLScheduleModelViewList = new List<PLScheduleModelView>();
            objBSScheduleModelViewList = new List<BSScheduleModelView>();
            objBalanceSheetModelViewList = new List<BalanceSheetModelView>();
            objIncomeAndExpenditureModelView = new IncomeAndExpenditureModelView();
            objIncomeAndExpenditureModelViewList = new List<IncomeAndExpenditureModelView>();
            objGetBankReconcilationModelView = new GetBankReconcilationModelView();
            //objGetBankReconcilationModelViewList = new List<GetBankReconcilationModelView>();
        }
    }

    public class JVDetailModel
    {
        public int JVDetailId { get; set; }
        public int JVMasterId { get; set; }
        public int
            Id
        { get; set; }
        public string FeeTypeName { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string PaymentType { get; set; }
        public string BillNumber { get; set; }
        public List<JVDetailModel> JVDetailsModelList { get; set; }
    }

    public class BillingDetailViewModel
    {
        public decimal TotalAmount { get; set; }
        public string FeeTypeName { get; set; }
        public int FeeTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string Narration1 { get; set; }
        public int AccountType { get; set; }
        public string DepartmentName { get; set; }
        public string BillingDate { get; set; }
        public decimal TotalIncomeAmount { get; set; }
        public decimal AmountAfterDiscount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string HeadName { get; set; }
        public DateTime BillDate { get; set; }
        public int AccountHeadID { get; set; }
        public int AccountSubHeadID { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string Type { get; set; }
        public List<BillingDetailViewModel> BillingDetailViewModelList { get; set; }
    }

    public class JVDetailsViewModel
    {
        public string JVnumber { get; set; }
        public DateTime JVDate { get; set; }
        public string JVPrepareBy { get; set; }
    }

    public class JournalVoucharViewModel
    {
        public string JVnumber { get; set; }
        public DateTime JvDate { get; set; }
        public int MyProperty { get; set; }
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Narration { get; set; }
    }

    public class LedgerDetailsViewModel
    {
        public decimal TotalAmount { get; set; }
        public string DepartmentName { get; set; }
        public string LedgerName { get; set; }
        public string EmployeeFullName { get; set; }
        public string BillNumber { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DepositedBy { get; set; }
        public decimal DepositedAmount { get; set; }
        public DateTime DepositDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<LedgerDetailsViewModel> LedgerDetailsViewModelList { get; set; }
    }

    public class LedgerViewModelAccountHeadWise
    {
        [Display(Name = "JV Number")]
        public string JVNumber { get; set; }
        public DateTime Date { get; set; }
        public string Particulars { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FeeTypeId { get; set; }
        public int FeeSubTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public int DrOrCrAmountType { get; set; }
        public List<LedgerViewModelAccountHeadWise> LedgerViewModelAccountHeadWiseList { get; set; }
    }

    public class AddMoreParticularsJVModel
    {
        public string DrOrCrName { get; set; }
        public int DrOrCr { get; set; }
        public int Particulars { get; set; }
        public int SubParticulars { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string Narration { get; set; }
        public decimal DrAmountTotal { get; set; }
        public decimal CrAmountTotal { get; set; }
    }

    public class GetTrialBalanceModelView
    {
        public string AccountHeadName { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalOpeningDebit { get; set; }
        public decimal TotalOpeningCredit { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Level { get; set; }
    }

    public class GetBilldetailByUserModelView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillDateTo { get; set; }
        public string NepaliBillDate { get; set; }
    }

    public class GetBilldetailByDepartmentModelView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int DepartmentID { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillDateTo { get; set; }
    }

    public class GetCashHandoverDetailModelView
    {
        public int id { get; set; }
        public int HandoverFrom { get; set; }
        public int HandoverTo { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalBankDeposit { get; set; }
        public decimal TotalExpenses { get; set; }
        public DateTime HandoverDateFrom { get; set; }
        public DateTime HandoverDateTo { get; set; }
        public int Rs1000 { get; set; }
        public int Rs500 { get; set; }
        public int Rs100 { get; set; }
        public int Rs50 { get; set; }
        public int Rs20 { get; set; }
        public int Rs10 { get; set; }
        public int Rs5 { get; set; }
        public int Rs2 { get; set; }
        public int Rs1 { get; set; }
        public decimal Paisa { get; set; }
        public bool IsHandover { get; set; }
        public string HandOverStatus { get; set; }
        public string Remarks { get; set; }
    }

    public class HandoverDetailReportModelView
    {
        public DateTime HandoverDateFrom { get; set; }
        public DateTime HandoverDateTo { get; set; }
        public string HandOverFrom { get; set; }
        public string HandOverTo { get; set; }
        public decimal HandOverAmount { get; set; }
        public DateTime HandOverDate { get; set; }
        public string HandOverStatus { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalBankDeposit { get; set; }
        public string Remarks { get; set; }
    }

    public class NotificationHandoverModelView
    {
        public DateTime HandoverDate { get; set; }
        public string HandOverFrom { get; set; }
        public int HandOverFromId { get; set; }
        public decimal Amount { get; set; }
        public int Rs1000 { get; set; }
        public int Rs500 { get; set; }
        public int Rs100 { get; set; }
        public int Rs50 { get; set; }
        public int Rs20 { get; set; }
        public int Rs10 { get; set; }
        public int Rs5 { get; set; }
        public int Rs2 { get; set; }
        public int Rs1 { get; set; }
        public decimal Paisa { get; set; }
    }

    public class OpeningBalanceModelView
    {
        public int FiscalYear { get; set; }
        public int AccountHeadId { get; set; }
        public int MainAccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public decimal DrAmount { get; set; }
        public decimal DifferenceAmount { get; set; }
        public decimal CrAmount { get; set; }
        public bool Status { get; set; }
    }

    public class PLScheduleModelView
    {
        public int FiscalYear { get; set; }
        public int AccountHeadId { get; set; }
        public string HierarchyCode { get; set; }
        public string AccountHeadName { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public decimal PrevClosingDr { get; set; }
        public decimal PrevClosingCr { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Level { get; set; }
        public int ParentID { get; set; }
    }

    public class BSScheduleModelView
    {
        public int FiscalYear { get; set; }
        public int AccountHeadId { get; set; }
        public string HierarchyCode { get; set; }
        public string AccountHeadName { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public decimal PrevClosingDr { get; set; }
        public decimal PrevClosingCr { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Level { get; set; }
        public int ParentID { get; set; }
        public string firstHierarchy { get; set; }
        public string SecondID { get; set; }
    }

    public class BalanceSheetModelView
    {
        public int FiscalYear { get; set; }
        public int AccountHeadId { get; set; }
        public string HierarchyCode { get; set; }
        public string AccountHeadName { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public decimal PrevClosingDr { get; set; }
        public decimal PrevClosingCr { get; set; }
        public bool Status { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetAllTrialBalanceModelView
    {
        public string MainHead { get; set; }
        public string SecondHead { get; set; }
        public string ThirdHead { get; set; }
        public string ForthHead { get; set; }
        public string FifthHead { get; set; }
        public string HirerchyCode { get; set; }
        public int firstHierarchy { get; set; }
        public int SecondID { get; set; }
        public int ThirdID { get; set; }
        public int fourthID { get; set; }
        public int FifthID { get; set; }
        public int ParentID { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalOpeningDebit { get; set; }
        public decimal TotalOpeningCredit { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Level { get; set; }
        public int UpToLevel { get; set; }
    }

    public class IncomeAndExpenditureModelView
    {
        public int Rono { get; set; }
        public int FiscalYear { get; set; }
        public int AccountHeadId { get; set; }
        public string HierarchyCode { get; set; }
        public string AccountHeadName { get; set; }
        public decimal ClosingDr { get; set; }
        public decimal ClosingCr { get; set; }
        public decimal PrevClosingDr { get; set; }
        public decimal PrevClosingCr { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetBankReconcilationModelView
    {
        public bool CheckValue { get; set; }
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public decimal TotalOpening { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public DateTime StartDate { get; set; }
        public int BankID { get; set; }
        public int BankSubID { get; set; }
        public string BankName { get; set; }
        public string BankSubName { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FiscalYearStartDate { get; set; }
        public string JvNumber { get; set; }
        public string State { get; set; }
        public decimal BalancePerCompanyDr { get; set; }
        public decimal BalancePerCompanyCr { get; set; }
        public decimal BalanceNotReflectedDr { get; set; }
        public decimal BalanceNotReflectedCr { get; set; }
        public decimal BalancePerBankDr { get; set; }
        public decimal BalancePerBankCr { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<GetBankReconcilationModelView> objGetBankReconcilationModelViewList { get; set; }
    }

    public class AccSubLoad
    {
        public int AccSubGruupID { get; set; }
        public string AccSubGroupName { get; set; }
    }
}