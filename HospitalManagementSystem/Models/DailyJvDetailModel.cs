using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DailyJvDetailModel
    {
        public int DailyJvDetailID { get; set; }

        public string Narration { get; set; }

        public string JvNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string DrCr { get; set; }

        public string AccountHead { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public decimal DrAmount { get; set; }

        public decimal CrAmount { get; set; }

        public int? VerifiedBy { get; set; }

        public bool Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public List<DailyJvDetailModel> JvDetailList { get; set; }

        public SetUpHospitalDetailModel HospitalDetail { get; set; }

        public SetupVoucherNumber VoucherNumber { get; set; }
    }
}