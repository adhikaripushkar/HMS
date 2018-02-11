using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class OperationRecordModel
    {
        public int OperationRecordId { get; set; }

        public int OpdId { get; set; }

        public int OperationTheatreMasterId { get; set; }

        public decimal? TotalCharge { get; set; }

        public DateTime OperationDate { get; set; }


        [DisplayName("Pre Operative Diagnosis")]
        public string PreOperativeDiagnosis { get; set; }

        [DisplayName("Post Operative Diagnosis")]
        public string PostOperativeDiagnosis { get; set; }

        [DisplayName("Operative Procedure")]
        public string OperativeProcedure { get; set; }

        public string DescriptionOfOperation { get; set; }

        public string FindingProcedure { get; set; }

        public string Conditions { get; set; }

        public TimeSpan OperationStartTime { get; set; }
        public TimeSpan OperationEndTime { get; set; }

        public List<OperationRecordModel> OperationRecordList { get; set; }

        public OpdMaster OpdMaster { get; set; }

        public List<OperationTheatreDetail> OperationTheatreDetailsList { get; set; }
    }
}