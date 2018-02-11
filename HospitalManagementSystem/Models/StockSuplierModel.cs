using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockSupplierModel
    {
        public int StockSupplierID { get; set; }

        [DisplayName("Supplier Name")]
        [Required(ErrorMessage = "*")]
        public string StockSupplierName { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "*")]
        public string StockSupplierAddress { get; set; }

        [DisplayName("Contact Number")]
        [Required(ErrorMessage = "*")]
        public string StockSupplierConNumber { get; set; }

        [DisplayName("Vat Number")]
        [Required(ErrorMessage = "*")]
        public string StockSupplierVatNumber { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "*")]
        public string Type { get; set; }

        [DisplayName("Email")]
        //[Required(ErrorMessage = "*")]
        public string StockSupplierEmail { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<StockSupplierModel> StockSupplierList { get; set; }
    }
}