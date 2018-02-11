using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DemandMasterModel
    {
        public int ItemDemandID { get; set; }


        [DisplayName("Demand No")]
        public string ItemDemandNo { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Department")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Date")]
        public DateTime DemandDate { get; set; }


        [DisplayName("Officer")]
        public string DemandOfficer { get; set; }


        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public bool isSelected { get; set; }

        public List<StockItemEntry> StockItemEntryList { get; set; }

        public List<DemandDetailModel> DemandDetailList { get; set; }

        public List<DemandMasterModel> DemandMasterList { get; set; }



        public DemandMasterModel()
        {
            StockItemEntryList = new List<StockItemEntry>();
        }

        public string ItemAllotmentNo { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Date")]
        public DateTime? AllotmentDate { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Officer")]
        public string Orderer { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Post")]
        public string OrdererPost { get; set; }
    }
}