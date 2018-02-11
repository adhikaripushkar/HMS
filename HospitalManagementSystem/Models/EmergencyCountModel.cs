using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class EmergencyCountModel
    {
        public int? EmergencyCountId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
        [DisplayName("Male")]
        public int? MaleNo { get; set; }
        [DisplayName("Female")]
        public int? FemaleNo { get; set; }
        [DisplayName("Other")]
        public int? OtherNo { get; set; }
        [DisplayName("Discharge")]
        public int? DischargeNo { get; set; }
        [DisplayName("On Treatrment")]
        public int? OnTreatrmentNo { get; set; }
        [DisplayName("LAMA")]
        public int? LAMANo { get; set; }
        [DisplayName("Normal Case")]
        public int? NormalCase { get; set; }
        [DisplayName("Police Case")]
        public int? PoliceCase { get; set; }
    }

}