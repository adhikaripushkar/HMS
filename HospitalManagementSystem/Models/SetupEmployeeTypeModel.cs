using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetupEmployeeTypeModel
    {
        [Key]
        public int SetupEmployeeTypeID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        public string SetupEmployeeTypeName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<SetupEmployeeTypeModel> SetupEmployeeTypeList { get; set; }
    }
}