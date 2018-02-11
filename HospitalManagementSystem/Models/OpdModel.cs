using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Models
{
    public class OpdModel
    {
        public int OpdID { get; set; }
        public int DepartMentID { get; set; }

        public int IsMember { get; set; }

        [DisplayName("Title")]
        public string PatientTitle { get; set; }

        [Required(ErrorMessage = "First Name Mandatory")]
        [DisplayName("First Name")]

        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name Mandatory")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        [DisplayName("Gender")]
        public string Sex { get; set; }
        [DisplayName("Contact Number")]
        public string ContactName { get; set; }
        [DisplayName("Mobile Number")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]

        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Registration Date Required")]
        [DisplayName("Registration Date")]


        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }


        [DisplayName("Registration Mode")]
        public string RegistrationMode { get; set; }

        public int CreatedBy { get; set; }



        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [DisplayName("Age")]
        public int? AgeYear { get; set; }
        public int AgeType { get; set; }
        [DisplayName("Month")]
        public int? AgeMonth { get; set; }
        [DisplayName("Day")]
        public int? AgeDay { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        [DisplayName("Blood Group")]
        public string BloodGroup { get; set; }
        [DisplayName("Member Type")]
        public int? MemberTypeID { get; set; }
        public int DepartmentPatientId { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Address Required")]
        [DisplayName("Address")]
        public string Address { get; set; }


        public string RegistrationSource { get; set; }
        [DisplayName("Country")]
        public int CountryID { get; set; }


        public int? id { get; set; }

        [DisplayName("Doctor Name")]
        public int DoctorID { get; set; }


        [DisplayName("Prefer Date")]
        [DataType(DataType.Date)]

        public DateTime? PreferDate { get; set; }

        [DisplayName("Prefer Time")]
        public TimeSpan? PreferTime { get; set; }



        public int? OtherDiscountID { get; set; }

        public int OldOrNewRegistration { get; set; }

        public int? ReferId { get; set; }
        public int? DistrictID { get; set; }
        public int? vdcID { get; set; }
        public int? ZoneId { get; set; }
        public string opdtype { get; set; }
        public int? vd { get; set; }
        public int COA { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime toDate { get; set; }
        public string NameofPatent { get; set; }
        public string NameofDoctor { get; set; }




        public OpdPatientOtherDetailsModel OpdPatientOtherDetailsModel { get; set; }
        public OpdFeeDetailsModel OpdFeeDetailsModel { get; set; }
        public OpdPatientDoctorDetailsModel OpdPatientDoctorDetailsModel { get; set; }
        public OpdDoctorListModel OpdDoctorListModel { get; set; }
        public List<OpdModel> OpdModelList { get; set; }
        public List<OpdPatientOtherDetailsModel> OpdPatientOtherDetailsModelList { get; set; }
        public List<OpdFeeDetailsModel> OpdFeeDetailsModelList { get; set; }


        public OpdMedicalDetailModel OpdMedicalDetailModel { get; set; }
        public List<OpdMedicalDetailModel> OpdMedicalDetailList { get; set; }

        //public IEnumerable<OpdIpdModel> IenumerableOpdModel { get; set; }

        //public OpdMedicalDetailModel OpdMedicalDetailModel { get; set; }

        //public List<BillingModel> BillingModelList { get; set; }
        //public List<CentralizeBillingViewModel> CentralizeBillingModelList { get; set; } noticed

        public OpdModel()
        {
            OpdDoctorList = new List<OpdDoctorListModel>();
            OpdFeeDetailsModelList = new List<OpdFeeDetailsModel>();
        }
        public List<OpdDoctorListModel> OpdDoctorList { get; set; }

        public List<OpdPatientDoctorDetailsModel> PatientDoctorList { get; set; }

    }
    public class OpdPatientOtherDetailsModel
    {
        public int OtherDetailsID { get; set; }
        [DisplayName("OPD ID")]
        public int OpdID { get; set; }
        [DisplayName("Refered By")]
        public string ReferedBy { get; set; }
        [DisplayName("History")]
        public string History { get; set; }
        [DisplayName("Reason")]
        public string Reason { get; set; }
        [DisplayName("Guardain Relation")]
        public int GurdainID { get; set; }
        [DisplayName("Guardain Fist Name")]
        public string GurdainFirstName { get; set; }
        [DisplayName("Guardain Middle Name")]
        public string GurdainMiddleName { get; set; }
        [DisplayName("Guardain Last Name")]
        public string GurdainLastName { get; set; }
        [DisplayName("Guardain Address")]
        public string GurdainAddress { get; set; }
        [DisplayName("Guardain Contact Number")]
        public string GurdainContactNumber { get; set; }
    }
    public class OpdFeeDetailsModel
    {

        public int OpdFeeDetailsID { get; set; }
        [DisplayName("OPD ID")]
        public int OpdID { get; set; }
        [DisplayName("Registration Fee")]
        public decimal RegistrationFee { get; set; }
        public decimal FeeTaxAmount { get; set; }
        [DisplayName("Fee Date")]
        public DateTime FeeDate { get; set; }
        [DisplayName("Doctor Fee")]
        public decimal DoctorFee { get; set; }
        public decimal DoctorFeeTax { get; set; }
        [DisplayName("Member Discount Amount")]
        public decimal MemberDiscountAmount { get; set; }
        [DisplayName("Other Discount %")]
        public decimal OtherDiscountPercentage { get; set; }
        [DisplayName("Other Discount Amount")]
        public decimal OtherDiscountAmount { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Tender Amount")]
        public decimal Tender { get; set; }
        [DisplayName("Return Amount")]
        public decimal ReturnAmount { get; set; }
        public decimal RegistrationFeeOnly { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmountWithOutTax { get; set; }
        //public decimal TaxAmount { get; set; }
    }
    public class OpdPatientDoctorDetailsModel
    {
        public int PatientDoctorDetailID { get; set; }
        [DisplayName("OPD ID")]
        public int OpdID { get; set; }
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        [DisplayName("Doctor")]
        public int DoctorID { get; set; }
        [DisplayName("Prefer Time")]
        public TimeSpan PreferTime { get; set; }
        [DisplayName("Prefer Date")]
        public DateTime PreferDate { get; set; }
    }
    public class OpdDoctorListModel
    {
        public int DepartmentID { get; set; }
        public int DoctorID { get; set; }
        [DisplayName("Prefer Time")]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true)]
        public TimeSpan? PreferTime { get; set; }

        public int PreRegistrationID { get; set; }

        [DisplayName("Prefer Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime? PreferDate { get; set; }



        public IEnumerable<SelectListItem> GetDepartmentList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new SelectList(ent.SetupDepartments.ToList(), "DeptID", "DeptName");
            }
        }

        public IEnumerable<SelectListItem> GetDepartmentListOnlyPrimaryDept()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new SelectList(ent.SetupDepartments.Where(x => x.AssociateDepartmentID == 1001).ToList(), "DeptID", "DeptName");
            }
        }


        public IEnumerable<SelectListItem> GetDoctorList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                if (DepartmentID == 0)
                {
                    var collection = (from c in ent.SetupDoctorMasters
                                      join b in ent.SetupDoctorDepartments on c.DoctorID equals b.DoctorID
                                      where b.DepartmentID == 1
                                      select new { c.DoctorID, c.FirstName, c.MiddleName, c.LastName }).Distinct();

                    ddlList.Add(new SelectListItem { Text = "--Select doctor--", Value = null });
                    ddlList.Add(new SelectListItem { Text = "Unit", Value = "1009" });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
                    }
                    return ddlList;
                }
                else
                {

                    var collection = (from c in ent.SetupDoctorMasters
                                      join b in ent.SetupDoctorDepartments on c.DoctorID equals b.DoctorID
                                      where b.DepartmentID == DepartmentID
                                      select new { c.DoctorID, c.FirstName, c.MiddleName, c.LastName }).Distinct();

                    ddlList.Add(new SelectListItem { Text = "--Select doctor--", Value = null });
                    ddlList.Add(new SelectListItem { Text = "Unit", Value = "1009" });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
                    }
                    return ddlList;


                }

            }
        }

    }


    public class OpdMedicalDetailModel
    {
        public int OpdMedicalDetailId { get; set; }
        public int OpdMasterId { get; set; }
        [DisplayName("Man Power")]
        public int ManPowerId { get; set; }
        [DisplayName("Agent")]
        public int AgentId { get; set; }
        [DisplayName("Test Type")]
        public string PreHolo { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Commission { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? status { get; set; }
        public List<OpdMedicalDetailModel> OpdMedicalDetailList { get; set; }
    }
    public class DoctorCommisionModel
    {
        public int Id { get; set; }
        [DisplayName("Doctor")]
        public int DoctorID { get; set; }
        [DisplayName("Test")]
        public int TestID { get; set; }
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        [DisplayName("Doctor Commision")]
        public decimal DoctorCom { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public List<DoctorCommisionModel> DoctorCommisionModelList { get; set; }


    }


}




