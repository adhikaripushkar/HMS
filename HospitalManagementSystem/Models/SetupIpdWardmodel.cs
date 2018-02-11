using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetUpIpdWardModel
    {

        public int IpdWardId { get; set; }

        [Required(ErrorMessage = "Required")]
        [System.ComponentModel.DisplayName("Ward Name")]
        public string WardName { get; set; }
        public bool Status { get; set; }
        [Display(Name = "Block Name")]
        public int BlockId { get; set; }
        [Display(Name = "Floor")]
        public int FloorId { get; set; }
        [Display(Name = "Ward Type")]
        public string MaleOrFemale { get; set; }


        public List<SetUpIpdWardModel> SetUpIpdWardModelList { get; set; }

    }
}