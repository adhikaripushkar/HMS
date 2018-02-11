using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockUnitModel
    {
        public int StockUnitID { get; set; }

        [DisplayName("Unit Name")]
        [Required(ErrorMessage = "*")]
        public string StockUnitName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<StockUnitModel> UnitList { get; set; }

    }
}