using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class AllotementDetailModel
    {
        public int ItemAllotmentDetailID { get; set; }

        public int ItemAllotmentMasterID { get; set; }

        public int ItemID { get; set; }

        public decimal QuantityIssued { get; set; }

        public decimal QuantityRemained { get; set; }

        public int SourceFrom { get; set; }

        public bool CancellationStatus { get; set; }

        public string Remarks { get; set; }

        public bool Status { get; set; }

        public List<AllotementDetailModel> AllotmentDetailList { get; set; }
    }
}