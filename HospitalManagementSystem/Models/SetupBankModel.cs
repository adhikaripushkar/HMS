using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class SetupBankModel
    {
        public int BankSetupId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string Address { get; set; }
        public int AccountHeadID { get; set; }
        [Display(Name = "Contact No.")]
        public string Contact { get; set; }
        public List<SetupBankModel> SetupBankModelList { get; set; }
    }
}