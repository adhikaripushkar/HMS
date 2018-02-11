using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class StockSubCategoryModel
    {
        [Key]
        [Display(Name = "ID")]
        public int StockSubCategoryID { get; set; }

        [Display(Name = "Stock Category")]
        public int StockCategoryID { get; set; }

        [Display(Name = "Stock Sub Category Name")]
        [Required(ErrorMessage = "*")]
        public string StockSubCategoryName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<StockSubCategoryModel> StockSubCategoryList { get; set; }
    }
}