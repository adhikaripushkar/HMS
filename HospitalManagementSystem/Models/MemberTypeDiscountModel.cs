using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class MemberTypeDiscountModel
    {
        public int MemberDiscountID { get; set; }

        [Required(ErrorMessage = "*")]

        [DisplayName("Member Type")]
        public int MemberShipType { get; set; }

        [Required(ErrorMessage = "Require field")]
        [DisplayName("Discount(%)")]
        public decimal? DiscountPercent { get; set; }



        [DisplayName("Others")]
        public string Others { get; set; }


        [DisplayName("Created By")]
        public int? CreatedBy { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }


        public char? Status { get; set; }


        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public List<MemberTypeDiscountModel> memberDiscountTypes { get; set; }
    }
}