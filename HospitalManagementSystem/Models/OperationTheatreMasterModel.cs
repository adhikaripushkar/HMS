using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class OperationTheatreMasterModel
    {
        public int OperationTheatreMasterID { get; set; }


        [Required(ErrorMessage = "Required")]
        [DisplayName("Operation Date")]
        public DateTime OperationDate { get; set; }


        [Required(ErrorMessage = "Required")]
        [DisplayName("Operation Start Time")]
        public TimeSpan OperationStartTime { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Operation Room")]
        public int OperationRoomID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Operation End Time")]
        public TimeSpan OperationEndTime { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("OPD ID")]
        public int SourceID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Charge")]
        public decimal? Charge { get; set; }

        [DisplayName("Discount")]
        public decimal? Discount { get; set; }

        [DisplayName("Total Charge")]
        public decimal? TotalCharge { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Operation Type")]
        public string OperationType { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Charge Name")]
        public string ChargeName { get; set; }
        public int? Diagnosis { get; set; }
        public string PatientName { get; set; }


        public int PatientACHeadId { get; set; }
        public decimal PatientDepoAmount { get; set; }
        public int BillNumber { get; set; }

        //public OperationTheatreDetailsModel OperationTheatreDetail { get; set; }

        public List<OperationTheatreMasterModel> OperationTheatreMasterList { get; set; }


        public List<PersonAllocated> PersonAllocatedList { get; set; }

        public FromCBViewDetails ObjFromCBViewDetails { get; set; }
        public List<FromCBViewDetails> FromCBViewDetailsList { get; set; }


        public GetCBCommissionViewModelOT ObjGetCBCommissionViewModelOT { get; set; }
        public List<GetCBCommissionViewModelOT> GetCBCommissionViewModelOTList { get; set; }

        public AddMoreOTCommissionViewModel ObjAddMoreOTCommissionViewModel { get; set; }
        public List<AddMoreOTCommissionViewModel> AddMoreOTCommissionViewModelList { get; set; }

        public GetOTCommDetailsViewModel ObjGetOTCommDetailsViewModel { get; set; }
        public List<GetOTCommDetailsViewModel> GetOTCommDetailsViewModelList { get; set; }

        public OpdModel refOfOpdModel { get; set; }
        public OperationTheatreMasterModel()
        {
            PersonAllocatedList = new List<PersonAllocated>();
            ObjFromCBViewDetails = new FromCBViewDetails();
            FromCBViewDetailsList = new List<FromCBViewDetails>();

            ObjGetCBCommissionViewModelOT = new GetCBCommissionViewModelOT();
            GetCBCommissionViewModelOTList = new List<GetCBCommissionViewModelOT>();

            ObjAddMoreOTCommissionViewModel = new AddMoreOTCommissionViewModel();
            AddMoreOTCommissionViewModelList = new List<AddMoreOTCommissionViewModel>();

            ObjGetOTCommDetailsViewModel = new GetOTCommDetailsViewModel();
            GetOTCommDetailsViewModelList = new List<GetOTCommDetailsViewModel>();
        }
    }

    public class PersonAllocated
    {
        [Required(ErrorMessage = "Required")]
        public int PersonAllocatedTypeId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int PersonAllocateId { get; set; }
    }


    public class FromCBViewDetails
    {
        public int PatientID { get; set; }
        public int BillNo { get; set; }
        public decimal ChargeAmount { get; set; }
        public DateTime OTDate { get; set; }
        public string Particulars { get; set; }
        public int CreatedUser { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Boolean Status { get; set; }
        public int AccHeadId { get; set; }
        public int SubAccHeadId { get; set; }


    }

    public class GetCBCommissionViewModelOT
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

    public class GetOTCommDetailsViewModel
    {
        public int CommissionType { get; set; }
        public decimal TotalAmount { get; set; }
        public int BillNo { get; set; }

    }

    public class AddMoreOTCommissionViewModel
    {
        public int DoctorId { get; set; }
        public decimal CommissionPerAmount { get; set; }
        public decimal CommissionAmout { get; set; }
    }

}