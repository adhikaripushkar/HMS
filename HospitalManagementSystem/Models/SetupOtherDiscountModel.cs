using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetupOtherDiscountModel
    {


        [Key]
        public int DiscountID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required Field")]
        public string DiscountName { get; set; }

        [Display(Name = "Percentage (%)")]
        [Required(ErrorMessage = "Required Field")]
        public decimal DiscountPercentage { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<SetupOtherDiscountModel> SetupOtherDiscountList { get; set; }
    }
}