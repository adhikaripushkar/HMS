using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class JournalVFormatModel
    {
        public int JVId { get; set; }
        public string Prefix { get; set; }
        public string FiscalYear { get; set; }
        public string Postfix { get; set; }
        public string JVFormat { get; set; }
        public Char IsCurrent { get; set; }
        public List<JournalVFormatModel> ListJournalVFormatModel { get; set; }
    }
}