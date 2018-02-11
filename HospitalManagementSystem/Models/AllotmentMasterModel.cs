using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class AllotmentMasterModel
    {
        public int ItemAllotmentMasterID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Demand Id")]
        public int ItemDemandID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Date")]
        public DateTime AllotmentDate { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Officer")]
        public string Orderer { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Post")]
        public string OrdererPost { get; set; }

        public string ItemAllotmentNo { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public List<AllotmentMasterModel> AllotmentMasterList { get; set; }


    }
}