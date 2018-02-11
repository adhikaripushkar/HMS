using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class TrialAuditModel
    {
        public int TrialAuditId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int FiscalYearId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public string Remarks { get; set; }


        public TrialAuditModel ObjTrialAuditModel { get; set; }
        public List<TrialAuditModel> TrialAuditLists { get; set; }
    }
}
