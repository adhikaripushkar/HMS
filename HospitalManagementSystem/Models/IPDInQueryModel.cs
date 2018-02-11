using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class IpdInqueryReportModel
    {
        public int opdid { get; set; }
        public int IpdRegistrationId { get; set; }
        public int BedNumber { get; set; }
        public int RoomNumber { get; set; }
        public int IpdWordNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int RoomType { get; set; }
        public List<IpdInqueryReportModel> IpdInqueryReportList { get; set; }
    }
}