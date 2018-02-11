using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ItemBlockSetupModel
    {
        public int ItemBlcokSetupID { get; set; }
        [Display(Name = "Block")]
        public int BlockId { get; set; }
        public string Particular { get; set; }
        public int SerialNoFrom { get; set; }
        public int SerialNoTo { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public List<ItemBlockSetupModel> ItemBlockSetupList { get; set; }
    }

    //public class ItemBlockRecordModel 

    //{
    //    public int ItemBlcokRecID { get; set; }
    //    public int ItemBlockSetupID { get; set; }
    //    public string Particular { get; set; }
    //    public int SerialNo { get; set; }
    //    public int ItemId { get; set; }
    //    public int DepId { get; set; }
    //    public string Condition { get; set; }
    //    public string RoomNo { get; set; }
    //    public int SupplierId { get; set; }
    //    public System.DateTime DeliveryDate { get; set; }
    //    public string Remarks { get; set; }
    //    public bool Status { get; set; }
    //    public int CreatedBy { get; set; }
    //    public System.DateTime CreatedDate { get; set; }

    //    public List<ItemBlockRecordModel> ItemBlockRecordList { get; set; }
    //}

}