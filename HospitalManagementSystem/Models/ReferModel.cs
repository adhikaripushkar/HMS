using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ReferModel
    {
        [DisplayName("Refer ID")]
        public int Id { get; set; }
        [DisplayName("Refer Name")]
        public string Refername { get; set; }


        public string ErrorMessage { get; set; }

        public string Successmessage { get; set; }


        public List<ReferModel> ReferModelList { get; set; }
    }
}