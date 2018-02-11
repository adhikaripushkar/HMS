using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupFloorModel
    {
        public int FloorId { get; set; }
        [Display(Name = "Floor Name")]
        [Required(ErrorMessage = "*")]
        public string FloorName { get; set; }
        public char Status { get; set; }
        public List<SetupFloorModel> SetupFloorModelList { get; set; }
    }
}