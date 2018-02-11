using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BloodStockManagementMasterModel
    {
        //public int BloodStockMasterId { get; set; }
        public int BloodType { get; set; }
        public int QuantityUnit { get; set; }
        public decimal QuantityML { get; set; }
    }
}