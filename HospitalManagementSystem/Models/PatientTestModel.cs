using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    public class PatientTestModel
    {
        public int PatientTestID { get; set; }
        [DisplayName("OPD/Emergency No")]
        public int PatientID { get; set; }
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


        public PatientTestDetailModel PatientTestDetailModel { get; set; }
        //public PatientTestResultModel PatientTestResultModel { get; set; }
        public PathoTestModel PathoTestModel { get; set; }
        public PatientInformationModel PatientInformationModel { get; set; }
        public PathoSectionModel PathoSectionModel { get; set; }
        public BillPaidTestModel BillPaidTestModel { get; set; }


        public List<PatientTestModel> PatientTestModelList { get; set; }
        public List<PatientTestDetailModel> PatientTestDetailModelList { get; set; }
        public List<PatientTestResultModel> PatientTestResultModelList { get; set; }

        public List<PatientInformationModel> PatientInformationModelList { get; set; }
        public List<PathoSectionModel> PathoSectionModelList { get; set; }
        public List<BillPaidTestModel> BillPaidTestModelList { get; set; }



        public List<SampleCollectedViewModel> SampleCollectedViewModelList { get; set; }
        public SampleCollectedViewModel ObjSampleCollectedViewModel { get; set; }


        public GetCBCommissionViewModel ObjGetCBCommissionViewModel { get; set; }
        public List<GetCBCommissionViewModel> GetCBCommissionViewModelList { get; set; }


        public GetPathoCommDetailsViewModel ObjGetPathoCommDetailsViewModel { get; set; }
        public List<GetPathoCommDetailsViewModel> GetPathoCommDetailsViewModelList { get; set; }

        public AddMoreParticularsViewModel ObjAddMoreParticularsViewModel { get; set; }
        public List<AddMoreParticularsViewModel> AddMoreParticularsViewModelList { get; set; }


        public DoctorCommissionRptViewModel ObjDoctorCommissionRptViewModel { get; set; }
        public List<DoctorCommissionRptViewModel> DoctorCommissionRptViewModelList { get; set; }
        public PatientTestModel()
        {
            PathoSectionModelList = new List<Models.PathoSectionModel>();
            TestCheckBoxListModelList = new List<TestCheckBoxListModel>();
            PathoTestModelList = new List<PathoTestModel>();
            PathoOtherTestModelCBList = new List<PathoOtherTestModelCB>();
            BillPaidTestModelList = new List<BillPaidTestModel>();
            SampleCollectedViewModelList = new List<SampleCollectedViewModel>();
            ObjSampleCollectedViewModel = new SampleCollectedViewModel();


            ObjGetCBCommissionViewModel = new GetCBCommissionViewModel();
            GetCBCommissionViewModelList = new List<GetCBCommissionViewModel>();

            ObjGetPathoCommDetailsViewModel = new GetPathoCommDetailsViewModel();
            GetPathoCommDetailsViewModelList = new List<GetPathoCommDetailsViewModel>();

            ObjAddMoreParticularsViewModel = new AddMoreParticularsViewModel();
            AddMoreParticularsViewModelList = new List<AddMoreParticularsViewModel>();


            ObjDoctorCommissionRptViewModel = new DoctorCommissionRptViewModel();
            DoctorCommissionRptViewModelList = new List<DoctorCommissionRptViewModel>();

        }

        public List<TestCheckBoxListModel> TestCheckBoxListModelList { get; set; }
        //added by bibek march 20
        public List<PathoOtherTestModelCB> PathoOtherTestModelCBList { get; set; }




        //public PatientTestModel()
        //{

        //}
        public List<PathoTestModel> PathoTestModelList { get; set; }



    }
    public class PatientTestDetailModel
    {
        public int PatientTestDetailID { get; set; }
        public int PatientTestID { get; set; }
        public int PatientID { get; set; }
        public int DepartmentID { get; set; }
        [DisplayName("Test Section")]
        public int SectionID { get; set; }
        [DisplayName("Test Name")]
        public int TestID { get; set; }
        [DisplayName("Test Date")]
        public DateTime TestDate { get; set; }
        [DisplayName("Test Time")]
        public DateTime? TestTime { get; set; }
        [DisplayName("Delivery Date")]
        public DateTime? DeliveryDate { get; set; }
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [DisplayName("Discount")]
        public decimal? Discount { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Delivery Status")]
        public bool DeliveryStatus { get; set; }

    }
    //public class PatientTestResultModel
    //{
    //    public int PatientTestResultID { get; set; }
    //    public int PatientTestID { get; set; }
    //    public int PatientTestDetailID { get; set; }
    //    public int TestID { get; set; }
    //    [DisplayName("Result A")]
    //    public string Value1 { get; set; }
    //    [DisplayName("Result B")]
    //    public string Value2 { get; set; }
    //    [DisplayName("Status")]
    //    public bool Status { get; set; }   
    //}   

    public class PatientInformationModel
    {

        public int EmergencyMasterId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentID { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public string PhoneNo { get; set; }

    }
    public class PathoTestModel
    {
        public int TestId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int SectionId { get; set; }
        public decimal Price { get; set; }



        public IEnumerable<SelectListItem> GetPathoTestSection()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupSections.ToList(), "SectionId", "Name");
            }
        }

        public IEnumerable<SelectListItem> GetPathoTest()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupPathoTests
                                //join y in ent.SetupDoctorDepartment on x.DoctorID equals y.DoctorID
                                //join z in ent.SetupDepartments on y.DepartmentID equals z.DeptID
                            where x.SectionId == 3
                            select new { x.TestId, x.TestName }).ToList();

                return new SelectList(data, "TestId", "TestName");
                // return new SelectList(ent.SetupDoctorMaster.ToList(), "DoctorID", "FirstName");
            }
        }

    }


    public class PathoSectionModel
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
    }


    //checkboxlist

    public class TestCheckBoxListModel
    {
        public int TestId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int SectionId { get; set; }
        public decimal? Price { get; set; }
        public bool isSelected { get; set; }

        public decimal HospitalFee { get; set; }
        public decimal Rate { get; set; }
        public int Tim { get; set; }
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
        public decimal TaxAmount { get; set; }
        public string BillNumber { get; set; }
        public decimal DiscountPer { get; set; }
        public int BillNo { get; set; }
    }

    //Added by bibek march 20
    public class PathoOtherTestModelCB
    {
        public int OtherTestId { get; set; }
        public string OtherTestName { get; set; }
        public decimal Amount { get; set; }
        public string Narration1 { get; set; }
        public string Narration2 { get; set; }
        [DisplayName("Test Date")]
        public DateTime TestDateR { get; set; }
        [DataType(DataType.Time)]
        public DateTime? TestTimeR { get; set; }
        [DataType(DataType.Time)]
        public DateTime? DeliveryDateR { get; set; }

        public bool isChecked { get; set; }

    }

    public class BillPaidTestModel
    {
        [DisplayName("Department")]
        public string DepartmentName { get; set; }
        [DisplayName("Bill No")]
        public string BillNumber { get; set; }
        public int BillNumberInt { get; set; }
        [DisplayName("Bill Date")]
        public DateTime TransactionDate { get; set; }
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        public int LedgerMasterId { get; set; }
        public int PatientLogId { get; set; }
        [DisplayName("Patient ID")]
        public int PatientId { get; set; }
        public decimal BillAmount { get; set; }
    }

    public class SampleCollectedViewModel
    {
        public int LabNumber { get; set; }
        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public DateTime SampleCollectedDate { get; set; }
        public int ReferDocId { get; set; }
        public int PatientAge { get; set; }
        public string Address { get; set; }
        public string SexType { get; set; }
        public int AgeType { get; set; }

    }

    public class GetCBCommissionViewModel
    {
        public int BillNo { get; set; }
        public decimal TotalAmount { get; set; }
        public int DoctorId { get; set; }

        public int PatientId { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string PatientName { get; set; }
        public DateTime CreatedDate { get; set; }


    }

    public class GetPathoCommDetailsViewModel
    {
        public int CommissionType { get; set; }
        public decimal TotalAmount { get; set; }
        public int BillNo { get; set; }

    }

    public class AddMoreParticularsViewModel
    {
        public int DoctorId { get; set; }
        public decimal CommissionPerAmount { get; set; }
        public decimal CommissionAmout { get; set; }
    }

    public class DoctorCommissionRptViewModel
    {
        public DateTime CommissionDate { get; set; }
        public string DoctorName { get; set; }
        public decimal TotalAmount { get; set; }
        public int FromBillNo { get; set; }
    }



}