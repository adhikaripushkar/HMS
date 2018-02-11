using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupMedicalChargeModel
    {

        public int MedicalChargeId { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Pre Medical Price")]
        public decimal PreMedicalPirce { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Hologram Price")]
        public decimal HologramPrice { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Discount Price")]
        public Nullable<decimal> DiscontPrice { get; set; }
        [DisplayName("Commission Amount")]
        public Nullable<decimal> CommissionAmount { get; set; }

        [DisplayName("Grand Total Amount")]
        public Nullable<decimal> GrandTotalAmount { get; set; }
        public Nullable<bool> Status { get; set; }


        public List<SetupMedicalChargeModel> lstofSetupMedicalChargeModel { get; set; }

    }
}