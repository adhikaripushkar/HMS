using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{



    //public BillingCounterNewTestListModel BillingCounterNewTestListModel { get; set; }
    //   public List<BillingCounterNewTestListModel> BillingCounterNewTestListModelList { get; set; }


    //   public BillingCounterNewModel()
    //   {
    //       BillingCounterNewTestListModel = new BillingCounterNewTestListModel();
    //       BillingCounterNewTestListModelList = new List<BillingCounterNewTestListModel>();


    //   }













    public class BillingCounterModel
    {
        public int PatientTestID { get; set; }

        [DisplayName("OPD/Emergency No")]
        public int PatientID { get; set; }
        public string MembershipId { get; set; }

        [DisplayName("Department")]
        public int DepartmentID { get; set; }

        [DisplayName("Refer Doctor")]
        public int ReferDoctorID { get; set; }

        [DisplayName("Test Date")]
        public DateTime TestRegistrationDate { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Discount")]
        public decimal Discount { get; set; }

        [DisplayName("Payable Amount")]
        public decimal PayableAmount { get; set; }

        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }


        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        public decimal Deposit { get; set; }
        public decimal DoctorFee { get; set; }
        public decimal DoctorFeeTax { get; set; }
        public decimal BalanceDeposit { get; set; }

        public decimal? TenderAmount { get; set; }
        public decimal ReturnedAmount { get; set; }

        public int PaymentMode { get; set; }


        public string CancelBillRemarks { get; set; }

        public decimal GrandTotalDiscount { get; set; }

        public decimal FlatDiscountAmount { get; set; }
        public String CurrentSession { get; set; }


        public BillingCounterIndexViewModel ObjBillingCounterIndexViewModel { get; set; }
        public BillingCounterPaymentDetails ObjBillingCounterPaymentDetails { get; set; }
        public AdvancedSearchViewModel ObjAdvancedSearchViewModel { get; set; }
        public PatientDischageBillDetailsViewModel ObjPatientDischageBillDetailsViewModel { get; set; }

        //list
        public List<BillingCounterModel> BillingCounterModelList { get; set; }
        public List<BillingCounterPatientInformationModel> BillingCounterPatientInformationModelList { get; set; }
        public List<BillingCounterTestListModel> BillingCounterTestListModelList { get; set; }
        public BillingCounterPatientInformationModel BillingCounterPatientInformationModel { get; set; }
        public BillingCounterTestListModel BillingCounterTestListModel { get; set; }
        public List<BillingCounterAutoCompleteModel> BillingCounterAutoCompleteModelList { get; set; }
        public List<BillingCounterIndexViewModel> BillingCounterIndexViewModelList { get; set; }
        public List<BillingCounterPaymentDetails> BillingCounterPaymentDetailsList { get; set; }
        public List<AdvancedSearchViewModel> AdvancedSearchViewModelList { get; set; }

        public List<PatientDischageBillDetailsViewModel> PatientDischageBillDetailsViewModelList { get; set; }
        public List<PatientDischageBillDetailsViewModel> PatientDischageBillDetailsListDeposit { get; set; }
        public List<PatientDischageBillDetailsViewModel> PatientDischageBillDetailsListByDeposit { get; set; }
        public List<PatientDischageBillDetailsViewModel> PatientDischageBillDetailsListByCash { get; set; }
        public List<PatientDischageBillDetailsByDepoViewModel> PatientDischageBillDetailsByDepoViewModelList { get; set; }
        public PatientDischageBillDetailsByDepoViewModel PatientDischageBillDetailsByDeposit { get; set; }

        public string billno { get; set; }


        public BillingCounterNewTestListModel BillingCounterNewTestListModel { get; set; }
        public List<BillingCounterNewTestListModel> BillingCounterNewTestListModelList { get; set; }

        public DepositRefundViewModel ObjDepositRefundViewModel { get; set; }
        public List<DepositRefundViewModel> DepositRefundViewModelList { get; set; }

        public BillingCounterModel()
        {
            BillingCounterPaymentDetailsList = new List<BillingCounterPaymentDetails>();
            ObjBillingCounterPaymentDetails = new BillingCounterPaymentDetails();
            BillingCounterPatientInformationModel = new BillingCounterPatientInformationModel();
            BillingCounterNewTestListModel = new BillingCounterNewTestListModel();
            BillingCounterNewTestListModelList = new List<BillingCounterNewTestListModel>();
            AdvancedSearchViewModelList = new List<AdvancedSearchViewModel>();
            ObjAdvancedSearchViewModel = new AdvancedSearchViewModel();
            PatientDischageBillDetailsViewModelList = new List<PatientDischageBillDetailsViewModel>();
            ObjPatientDischageBillDetailsViewModel = new PatientDischageBillDetailsViewModel();
            PatientDischageBillDetailsListDeposit = new List<PatientDischageBillDetailsViewModel>();
            PatientDischageBillDetailsListByDeposit = new List<PatientDischageBillDetailsViewModel>();
            PatientDischageBillDetailsListByCash = new List<PatientDischageBillDetailsViewModel>();

            PatientDischageBillDetailsByDeposit = new PatientDischageBillDetailsByDepoViewModel();
            PatientDischageBillDetailsByDepoViewModelList = new List<PatientDischageBillDetailsByDepoViewModel>();

            DepositRefundViewModelList = new List<DepositRefundViewModel>();
            ObjDepositRefundViewModel = new DepositRefundViewModel();



        }

    }



    public class BillingCounterPatientInformationModel
    {

        public int EmergencyMasterId { get; set; }
        public string MembershipId { get; set; }


        public int DistrictID { get; set; }
        public int vdcID { get; set; }
        public int PatientLog { get; set; }


        [Required]

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentID { get; set; }

        //[Required]
        public string Address { get; set; }


        [DisplayName("Moblile No.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string PhoneNo { get; set; }

        [DisplayName("Nationality")]
        public int CountryID { get; set; }
        [DisplayName("Zone")]
        public int ZoneId { get; set; }
        //[DisplayName("District")]


        public int AgeType { get; set; }

        public string DoctorName { get; set; }
        //public int? AccountHeadId { get; set; }

        public Nullable<long> AccountHeadId { get; set; }
    }
    #region 'comment'
    //public class PathoTestModel
    //{
    //    public int TestId { get; set; }
    //    public string TestCode { get; set; }
    //    public string TestName { get; set; }
    //    public int SectionId { get; set; }
    //    public decimal Price { get; set; }



    //    public IEnumerable<SelectListItem> GetPathoTestSection()
    //    {
    //        using (EHMSEntities ent = new EHMSEntities())
    //        {
    //            return new SelectList(ent.SetupSections.ToList(), "SectionId", "Name");
    //        }
    //    }

    //    public IEnumerable<SelectListItem> GetPathoTest()
    //    {
    //        using (EHMSEntities ent = new EHMSEntities())
    //        {
    //            var data = (from x in ent.SetupPathoTest
    //                        //join y in ent.SetupDoctorDepartment on x.DoctorID equals y.DoctorID
    //                        //join z in ent.SetupDepartments on y.DepartmentID equals z.DeptID
    //                        where x.SectionId == 3
    //                        select new { x.TestId, x.TestName }).ToList();

    //            return new SelectList(data, "TestId", "TestName");
    //            // return new SelectList(ent.SetupDoctorMaster.ToList(), "DoctorID", "FirstName");
    //        }
    //    }

    //}

    #endregion


    //checkboxlist

    public class BillingCounterTestListModel
    {
        public int TestId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int SectionId { get; set; }
        public decimal? Price { get; set; }
        public bool isSelected { get; set; }
        public decimal HospitalFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Rate { get; set; }
        public int Tim { get; set; }
        public int UserId { get; set; }

        //testdetails

        [DisplayName("Test Date")]
        public DateTime TestDate { get; set; }
        [DisplayName("Test Time")]
        [DataType(DataType.Time)]
        public DateTime? TestTime { get; set; }
        [DisplayName("Delivery Date")]
        [DataType(DataType.Time)]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [DisplayName("Discount")]
        public decimal? Discount { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Delivery Status")]
        public bool DeliveryStatus { get; set; }
        public int DepartmentID { get; set; }
        //
        public decimal? DiscountPer { get; set; }
    }

    public class BillingCounterAutoCompleteModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }



    //Added By Bibek 
    public class BillingCounterIndexViewModel
    {
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        [DisplayName("Bill Number")]
        public string BillNumber { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        public int BillnumberInt { get; set; }
        public decimal totalAmount { get; set; }
        public string PatientAddress { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int HospitalType { get; set; }
        //public List<BillingCounterIndexViewModel> BillingCounterIndexViewModelList { get; set; }


    }

    public class BillingCounterPaymentDetails
    {
        public string Particulars { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal GrandTotal { get; set; }
        public string DepartmentName { get; set; }
        public string BillNumber { get; set; }
        public int Tim { get; set; }
        [DataType(DataType.Date)]
        public DateTime AmountDate { get; set; }
        public string AccountHead { get; set; }

        public string PatientFullName { get; set; }
        public int Age { get; set; }
        public string Gendar { get; set; }
        public string Address { get; set; }
        public int PatientId { get; set; }
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }


        public List<BillingCounterPaymentDetails> BillingCounterPaymentDetailsList { get; set; }

    }


    public class BillingCounterNewTestListModel
    {
        public int TestId { get; set; }
        public string TestCode { get; set; }

        [Required]
        public string TestName { get; set; }
        public int SectionId { get; set; }
        public decimal Price { get; set; }
        public bool isSelected { get; set; }
        public decimal HospitalFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Rate { get; set; }
        public int Tim { get; set; }
        public int UserId { get; set; }

        //testdetails

        [DisplayName("Test Date")]
        public DateTime TestDate { get; set; }
        //[Range(1, 99, ErrorMessage = "Value must be between 1 and 99")]
        [DisplayName("Test Time")]
        public int TestTime { get; set; }


        [DisplayName("Delivery Date")]
        [DataType(DataType.Time)]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [DisplayName("Discount")]
        public decimal? Discount { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Delivery Status")]
        public bool DeliveryStatus { get; set; }
        public int DepartmentID { get; set; }

        public int ReferDoctorID { get; set; }

        public decimal DiscountPer { get; set; }


        public int SubTestId { get; set; }//For Level 6
        public int TestTimes { get; set; }


        //For Showin only Rate and tax
        public decimal ShowingRateOnly { get; set; }
        public decimal ShowingTaxOnly { get; set; }

        public int CBDID { get; set; }

        // public List<BillingCounterNewTestListModel> BillingCounterNewTestListModelList { get; set; }
    }

    public class AdvancedSearchViewModel
    {
        public int PatientId { get; set; }
        public string patientName { get; set; }
        public string MemberName { get; set; }
        public int MemebershipId { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int BloodGropId { get; set; }
        public string Sex { get; set; }
        public string ContactNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }


    }

    public class PatientDischageBillDetailsViewModel
    {

        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int BillNumberInt { get; set; }
        public string BillNumberStr { get; set; }
        public decimal BillAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal TotalDepositAmonnt { get; set; }
        public string TestName { get; set; }
        public int TestId { get; set; }
        public string Particulars { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TaxOnlyByCash { get; set; }
        public int AccountMainHeadId { get; set; }
        public int CreatedUser { get; set; }
        public int CreatedBy { get; set; }


    }

    public class PatientDischageBillDetailsByDepoViewModel
    {
        //Transaction By Deposit
        public string DepoParticulars { get; set; }
        public DateTime DepoBillDate { get; set; }
        public decimal DepoBillAmount { get; set; }
        public decimal DepoTotalTaxAmt { get; set; }
        public decimal DepositTotalTaxAmount { get; set; }
        public int AccountMainHeadId { get; set; }
        public int CreatedUser { get; set; }
    }

    public class DepositRefundViewModel
    {
        public int DepositRefundId { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public int PatientId { get; set; }
        public int PatientLogId { get; set; }
        public DateTime RefundDate { get; set; }
        public int RefundBy { get; set; }
        public Boolean status { get; set; }
        public string Remarks { get; set; }
        public int BranchId { get; set; }
        public int FiscalYearId { get; set; }
        public int AccountSubHeadId { get; set; }


    }

    public class IPDPatientDischargeViewModel
    {
        public int PatientIPDID { get; set; }
        public int OpdID { get; set; }
        public int DepartmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public string Particulars { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime DischargedDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrandTotalAmount { get; set; }

    }




}