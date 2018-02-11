using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupMemberDependentModel
    {


        public SetupMemberDependentModel()
        {
            lstOfDependentModelRel = new List<SetupMemberDependentModelRel>();
        }

        public int SetupMemberDetailID { get; set; }

        public int SetupMemberID { get; set; }

        public int MemberTypeID { get; set; }

        public int RelationID { get; set; }


        public string FullName { get; set; }

        public DateTime? DateofBirth { get; set; }


        [Display(Name = "Mobile")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string MoibleNumber { get; set; }

        public string ContactNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public int? BloodGroupID { get; set; }

        public string Gender { get; set; }



        public List<SetupMemberDependentModelRel> lstOfDependentModelRel { get; set; }

        public List<SetupMemberDependentModel> lstOfSetupMemberDependentModel { get; set; }

        public SetUpMemberShipModel refSetUpMemberShipModel { get; set; }

    }


    public class SetupMemberDependentModelRel
    {
        [Display(Name = "Relation")]
        public int RelationID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "DOB")]
        public DateTime? DateofBirth { get; set; }


        [Display(Name = "Mobile")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string MoibleNumber { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Blood Group")]
        public int? BloodGroupID { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }
}