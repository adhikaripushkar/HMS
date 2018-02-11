using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockBreakageModel
    {

        public int StockBreakgaeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime BreakageEntryDate { get; set; }

        public int ItemId { get; set; }

        public int BreakageQuantity { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Remarks { get; set; }

        public bool Status { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int TotalQuantity { get; set; }

        public List<StockBreakageModel> BreakageModelList { get; set; }



    }



}