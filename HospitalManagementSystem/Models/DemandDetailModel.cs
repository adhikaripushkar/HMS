using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DemandDetailModel
    {
        public int ItemDemandDetailId { get; set; }

        [DisplayName("Demand Id")]
        public int ItemDemandID { get; set; }

        [DisplayName("Item")]
        public int ItemID { get; set; }

        [DisplayName("Quantity")]
        public decimal QuantityDemand { get; set; }

        [DisplayName("Dispatch Status")]
        public bool DispatchStatus { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public bool isSelected { get; set; }
        public List<DemandDetailModel> DemandDetailList { get; set; }


        public decimal QuantityIssued { get; set; }

        public decimal QuantityRemained { get; set; }

        public int SourceFrom { get; set; }

        public bool CancellationStatus { get; set; }

        public string RemarksA { get; set; }

    }
}