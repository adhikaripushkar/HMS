using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DoctorSetupModel
    {
        public int DoctorID { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Required")]
        public DateTime? DOB { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Sex")]
        public string Sex { get; set; }
        [DisplayName("Joining Date")]
        public DateTime? JoiningDate { get; set; }
        //[Required(ErrorMessage = "Required")]
        [DisplayName("E-Mail")]

        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Type")]
        public int DoctorType { get; set; }


        public DoctorDepartmentSetupModel DoctorDepartmentSetupModel { get; set; }
        public List<DoctorSetupModel> DoctorSetupModelList { get; set; }
        public List<CheckBoxList> checkBoxlistModel { get; set; }
        public DoctorPartialDetails objDoctorPartialDetail { get; set; }
        public List<DoctorPartialDetails> DoctorPartialDetailsList { get; set; }

    }

    public class DoctorDepartmentSetupModel
    {
        public int DoctorID { get; set; }
        public int DepartmentID { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; }
    }

    public class CheckBoxList
    {
        public decimal DeptID { get; set; }
        public string DeptName { get; set; }
        public bool isSelected { get; set; }
    }
    public class DoctorPartialDetails
    {
        public int? DoctorID { get; set; }
        public int? DeptId { get; set; }
        public string Title { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public List<DoctorPartialDetails> DoctorPartialDetailsList { get; set; }


    }


}

