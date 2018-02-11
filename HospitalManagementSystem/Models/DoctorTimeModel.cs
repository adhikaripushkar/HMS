using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    public class DoctorTimeModel
    {
        public int DoctorAvailableTimeId { get; set; }
        [DisplayName("Doctor Name")]
        public int DoctorID { get; set; }
        [DisplayName("Day of Week")]
        public string DoctorDays { get; set; }
        public string Shift { get; set; }
        [DisplayName("Start Time")]
        public TimeSpan? StartTime { get; set; }
        [DisplayName("End Time")]
        public TimeSpan? EndTime { get; set; }
        [DisplayName("Check Patient Per Day")]
        public int CheckPatientPerDay { get; set; }
        [DisplayName("Web Patient Per Day")]
        public int? WebPatientPerDay { get; set; }
        public decimal? DoctorFee { get; set; }
        public int? MaxTokenNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Remarks { get; set; }
        public Boolean? Status { get; set; }

        [NotMapped]
        public int DepartmentID { get; set; }
        [NotMapped]
        public string DoctorName { get; set; }
        [NotMapped]
        public int TimeId { get; set; }
        //public DoctorTimeModel()
        //{
        //    DoctorTimeList = new List<DoctorTimeModel>();
        //}
        public List<DoctorTimeModel> DoctorTimeList { get; set; }

        public IEnumerable<DoctorTimeModel> GetIEnumereble { get; set; }

        public SelectList selectList { get; set; }
    }
}