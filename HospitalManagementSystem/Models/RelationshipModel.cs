using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class RelationshipModel
    {
        public int RelationId { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Relationship")]
        public string RelationName { get; set; }


        public char Status { get; set; }



        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        public List<RelationshipModel> RelationshipList { get; set; }
    }
}