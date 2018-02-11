using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupEmergencyCaseModel
    {
        public int CaseId { get; set; }

        [Display(Name = "Case Name")]
        [Required(ErrorMessage = "*")]
        public string CaseName { get; set; }

        public Char Status { get; set; }

        public List<SetupEmergencyCaseModel> SetupBlockModelList { get; set; }
    }
}