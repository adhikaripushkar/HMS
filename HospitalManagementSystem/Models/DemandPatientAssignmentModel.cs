using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DemandPatientAssignmentModel
    {
        public int DemandPatientAssignmentId { get; set; }

        public int OpdId { get; set; }

        public bool Status { get; set; }

        public string Remarks { get; set; }

        public int DemandId { get; set; }

        public List<DemandPatientAssignmentModel> DemandPatientAssignmentList { get; set; }

        public List<OpdModel> OpdModelList { get; set; }
    }
}