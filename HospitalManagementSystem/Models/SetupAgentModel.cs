using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetupAgentModel
    {

        public int AgentId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("First Name")]
        public string AgentFirstName { get; set; }

        [DisplayName("Middle Name")]
        public string AgentMiddleName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Last Name")]
        public string AgentLastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Address")]
        public string AgentAddress { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Contact No")]
        public string AgentContactNo { get; set; }
        [DisplayName("Mobile No")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string AgentMobileNo { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Manpower")]
        public Nullable<int> ManpowerId { get; set; }
        public Nullable<bool> Status { get; set; }

        public List<SetupAgentModel> lstOfSetupAgentModel { get; set; }

    }
}