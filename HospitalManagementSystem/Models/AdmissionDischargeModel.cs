using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class AdmissionDischargeModel
    {
        public string oname { get; set; }

        public string ename { get; set; }

        public int OpdNoEmergencyNo { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string oaddress { get; set; }

        public string eaddress { get; set; }

        public int DepartmentID { get; set; }

        public int? oage { get; set; }

        public int? eage { get; set; }

        public string osex { get; set; }

        public string esex { get; set; }

        public int? AgeMonth { get; set; }

        public int? AgeDay { get; set; }

        public string odoctor { get; set; }

        public string edoctor { get; set; }

        public string WardName { get; set; }
    }
}