using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupUnitModel
    {
        public int UnitId { get; set; }
        [DisplayName("Unit Code")]
        public string UnitCode { get; set; }
        [DisplayName("Unit Name")]
        [Required(ErrorMessage = "*")]
        public string UnitName { get; set; }

        public List<SetupUnitModel> UnitLists { get; set; }

    }
}