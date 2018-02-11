using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpHospitalDetailModel
    {
        public int DetailsId { set; get; }


        [Display(Name = "Title Name")]
        public string TitleName { get; set; }

        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { set; get; }


        [Display(Name = "Ward Number")]
        public int? WardNo { set; get; }


        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }


        [Display(Name = "Post Box")]
        public string PostBox { get; set; }


        [Display(Name = "Telephone")]
        public string TelPhone { get; set; }

        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "Email")]
        public string HospitalEmail { get; set; }
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Display(Name = "Logo Image")]
        public string ImageLogoName { get; set; }


        public string FilePath { get; set; }

        public string PanNumber { get; set; }


        public List<SetUpHospitalDetailModel> SetUpHospitalDetailList { get; set; }

    }
}