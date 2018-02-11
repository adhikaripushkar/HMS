using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ItemBlockRecordsModel
    {

        public int ItemBlcokRecID { get; set; }
        public int ItemBlockSetupID { get; set; }
        public string Particular { get; set; }
        public int SerialNo { get; set; }
        public string ItemName { get; set; }
        public int DepId { get; set; }
        public string Condition { get; set; }
        public string RoomNo { get; set; }
        public int SupplierId { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }



    }
}