using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class VDCMUNModel
    {
        [DisplayName("District ID")]
        public int VdcMunID { get; set; }
        [DisplayName("District ID")]
        public int DistrictID { get; set; }
        [DisplayName("VDCMUN Title[Eng]")]
        public string VdcMunNameEng { get; set; }
        [DisplayName("District Title[Nep]")]
        public string VdcMunNameNep { get; set; }


        public List<VDCMUNModel> VdcMunModelList { get; set; }
    }
}
