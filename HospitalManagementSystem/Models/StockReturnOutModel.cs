using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockReturnOutModel
    {
        public int ReturnOutId { get; set; }

        public int PurchaseId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public int ItemId { get; set; }

        public decimal ReturnOutQty { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnOutDate { get; set; }

        public int SupplierId { get; set; }

        public string Remarks { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ReturnOutNo { get; set; }

        public List<StockReturnOutModel> ReturnOutList { get; set; }
    }

    public class StockReturnInModel
    {
        public int ReturnInId { get; set; }
        public int ReturnOutId { get; set; }
        public int PurchaseId { get; set; }
        public int ItemId { get; set; }
        public decimal ReturnInQty { get; set; }
        public DateTime ReturnInDate { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<StockReturnInModel> ReturnInList { get; set; }
    }

}