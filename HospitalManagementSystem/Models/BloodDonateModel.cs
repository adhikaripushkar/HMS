using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BloodDonateModel
    {
        public int BloodDonateId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Gender")]
        public int Gender { get; set; }
        public int Age { get; set; }
        public int District { get; set; }
        public int Zone { get; set; }
        public string Vdc { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public string Mobile { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Blood Group")]
        public int BloodGroup { get; set; }
        public int Quantity { get; set; }
        public decimal QuantityML { get; set; }
        public string PouchNumber { get; set; }
        public DateTime DonateDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public List<BloodDonateModel> BloodDonateLists { get; set; }
        public BloodStockManagementModel objBSMM { get; set; }





    }
}