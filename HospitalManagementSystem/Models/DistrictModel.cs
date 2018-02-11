using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DistrictModel
    {
        [DisplayName("District ID")]
        public byte DistrictID { get; set; }
        [DisplayName("Zone ID")]
        public Nullable<byte> ZoneID { get; set; }
        [DisplayName("District Title[Nep]")]
        public string District { get; set; }
        [DisplayName("District Title[Eng]")]
        public string DistrictEng { get; set; }

        public List<DistrictModel> DistrictModelList { get; set; }

        public List<ZoneModel> ZoneModelList { get; set; }
    }
}
