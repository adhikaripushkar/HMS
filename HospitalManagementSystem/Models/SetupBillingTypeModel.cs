using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetupBilllingTypeModel
    {
        [Key]
        public int SetupBillingTypeID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        public string SetupBillingTypeName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<SetupBilllingTypeModel> SetupBillingTypeList { get; set; }
    }
}