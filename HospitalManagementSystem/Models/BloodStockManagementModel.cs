using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BloodStockManagementModel
    {
        public int BloodStockId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Blood Group")]
        public int BloodType { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Quantity")]
        public int QuantityUnit { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Quantity in ML")]
        public decimal QuantityML { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Pouch Number")]
        public string PouchNumber { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Stock Date")]
        public DateTime StockDate { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Valid Till")]
        public DateTime ValidUpTo { get; set; }
        //public int SourceFrom { get; set; }
        public List<BloodStockManagementModel> BloodStockList { get; set; }



    }
}