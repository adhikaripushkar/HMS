using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class CentralizedBillingModel
    {
        public int CentralizedBillingId { get; set; }
        public int AccountHeadId { get; set; }
        public int AmountTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime AmountDate { get; set; }
        public string PaymentType { get; set; }
        public string Narration1 { get; set; }
        public string Narration2 { get; set; }
        public string DepartmentName { get; set; }
        public int SubDepartmentId { get; set; }
        public string BillNumber { get; set; }
        public int LedgerMasterId { get; set; }
        public int PatientLogId { get; set; }
        public int PatientId { get; set; }
        public string JVNumber { get; set; }
        public bool JVStatus { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }



    }
}