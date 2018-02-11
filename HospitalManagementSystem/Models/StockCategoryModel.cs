using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockCategoryModel
    {
        [Key]
        [Display(Name = "ID")]
        public int StockCategoryID { get; set; }

        [Display(Name = "Stock Category Name")]
        [Required(ErrorMessage = "*")]
        public string StockCategoryName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public int AccountHeadId { get; set; }

        public List<StockCategoryModel> StockCategoryList { get; set; }
    }
}