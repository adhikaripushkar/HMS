using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupBlockModel
    {
        public int BlockId { get; set; }

        [Display(Name = "Block Name")]
        [Required(ErrorMessage = "*")]
        public string BlockName { get; set; }

        public Char Status { get; set; }

        public List<SetupBlockModel> SetupBlockModelList { get; set; }
    }
}