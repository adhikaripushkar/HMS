using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupEmergencyInvestigationMasterModel
    {
        public int EInvId { get; set; }
        [Display(Name = "Test Name")]
        [Required(ErrorMessage = "*")]
        public string TestName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public List<SetupEmergencyInvestigationMasterModel> SetupEmergencyInvestigationMasterModelList { get; set; }
    }
}