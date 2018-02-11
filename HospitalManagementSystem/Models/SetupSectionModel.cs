using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpSectionModel
    {

        public int SectionId { get; set; }

        [Required(ErrorMessage = "Required")]
        [System.ComponentModel.DisplayName("Section Name")]
        public string Name { get; set; }
        [Display(Name = "Section Code")]
        public string SectionCode { get; set; }
        [Display(Name = "Department Name")]
        public int DepartmentId { get; set; }
        public List<SetUpSectionModel> SetUpSectionbModelList { get; set; }




    }
}