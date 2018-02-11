using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupManpowerModel
    {

        public int ManpowerId { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Manpower Name")]
        public string ManpowerName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }


        [DisplayName("Mobile No")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string MobileNo { get; set; }
        [DisplayName("Email Id")]
        public string EmailId { get; set; }

        [DisplayName("Website Url")]
        public string WebsiteUrl { get; set; }
        public Nullable<bool> Status { get; set; }


        public List<SetupManpowerModel> lstofSetupManpowerModel { get; set; }



    }
}