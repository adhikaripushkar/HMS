using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupDiagnosisModel
    {
        public int DiagnosisId { get; set; }
        [Display(Name = "Diagnosis Name")]
        public string DiagnosisName { get; set; }

        public List<SetupDiagnosisModel> DiagnosisLists { get; set; }
    }
}