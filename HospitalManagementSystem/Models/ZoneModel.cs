using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ZoneModel
    {
        [DisplayName("Zone ID")]
        public byte ZoneID { get; set; }
        [DisplayName("Zone Title[Nep]")]
        public string Zone { get; set; }

        [DisplayName("Zone Title[Eng]")]
        public string ZoneInEng { get; set; }


        public string ErrorMessage { get; set; }

        public string Successmessage { get; set; }


        public List<ZoneModel> ZoneRecordsModelList { get; set; }
    }
}