using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class IpdCountModel
    {

        public int? IpdCountId { get; set; }
        public string Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Male(60-64)")]
        public int? MaleNobetn { get; set; }
        [DisplayName("Female(60-64)")]
        public int? FemaleNobetn { get; set; }
        [DisplayName("Male(>64)")]
        public int? MaleNoAbove { get; set; }
        [DisplayName("Female(>64)")]
        public int? FemaleNoAbove { get; set; }
        public int? TotalNoBetn { get; set; }
        public int? TotalNoAbove { get; set; }
        public int? GrandTotalElder { get; set; }
        public int? TotalIpdPatient { get; set; }
        public int? TotalIpdMalePatient { get; set; }
        public int? TotalIpdFemalePatient { get; set; }
        public List<IpdCountModel> ListIpdCountModelForJanuary { get; set; }


    }

}