using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class MemberTypeModel
    {

        public int MemberTypeID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Member Type")]
        public string MemberTypeName { get; set; }

        // [Required(ErrorMessage = "Required")]
        [DisplayName("Valid Upto")]
        public DateTime? ValidUpto { get; set; }


        public string date { get; set; }

        public string Status { get; set; }

        public List<MemberTypeModel> memberTypes { get; set; }
    }
}