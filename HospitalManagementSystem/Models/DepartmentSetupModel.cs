using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DepartmentSetupModel
    {
        [DisplayName("Dept ID")]
        public int DeptID { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Department Code")]
        public string DeptCode { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Department Name")]
        public string DeptName { get; set; }
        [DisplayName("Block Name")]
        public string Block { get; set; }

        [DisplayName("Floor Name")]
        public string Floor { get; set; }

        public Boolean IsAvailable { get; set; }
        public List<DepartmentSetupModel> DepartmentSetupList { get; set; }
    }
}
