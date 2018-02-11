using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpMemberShipModel
    {


        public int SetupMemberID { get; set; }


        public int CountryID { get; set; }

        [Required(ErrorMessage = "MemerId Is Required")]
        public string MemberID { get; set; }

        [Required(ErrorMessage = "MemberTypeId Is Required")]
        public int MemberTypeID { get; set; }

        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string FirstName { get; set; }


        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Is Required")]
        public string LastName { get; set; }

        public System.DateTime DateofBirth { get; set; }

        public int MaximumDependent { get; set; }

        //[Required(ErrorMessage = "Address Is Required")]
        public string Address { get; set; }


        // [Required(ErrorMessage = "Please Enter Mobile No")]
        [Display(Name = "Mobile")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string MobileNumber { get; set; }

        public string ContactNumber { get; set; }

        public string Gender { get; set; }

        public int? BloodGroupID { get; set; }


        //[Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Remarks { get; set; }

        public bool Status { get; set; }

        public int cnt { get; set; }

        public List<SetUpMemberShipModel> lstOfsetSetUpMemberShipModel { get; set; }

    }

}