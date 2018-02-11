using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BreakageReturnReportModel
    {
        public int StockItemEntryId { get; set; }

        public int OpeningStock { get; set; }

        public int brqty { get; set; }

        public decimal roqty { get; set; }

        public decimal riqty { get; set; }

        public decimal prcqty { get; set; }

        public decimal altqty { get; set; }

        public List<BreakageReturnReportModel> BrkRetReportList { get; set; }
    }
}