using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class CreditPatientDetailsModel
    {
        [DisplayName("ID")]
        public int CreditPatientID { get; set; }
        [DisplayName("Name")]
        public string CreditPatientName { get; set; }
        [DisplayName("Case")]
        public string CreditPatientCase { get; set; }
        [DisplayName("Refer Name")]
        public string CreditPatientRefer { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Test")]
        public string CreditpatientTest { get; set; }
        [DisplayName("Representative Name")]
        public string CreditPatientRepresentative { get; set; }
        [DisplayName("Brought By Name")]
        public string CreditPatientBroughtBy { get; set; }

        public string ErrorMessage { get; set; }

        public string Successmessage { get; set; }


        public List<CreditPatientDetailsModel> CreditPatientDetailsModelList { get; set; }
    }
}