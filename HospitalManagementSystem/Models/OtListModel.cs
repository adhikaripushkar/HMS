using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class OtListModel
    {
        public string PatientName { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public int Ward { get; set; }

        public int BedNo { get; set; }

        public string Diagnosis { get; set; }

        public string TypeOfSurgery { get; set; }

        public string Anesthesia { get; set; }

        public string Consultant { get; set; }

        public int OpMasterId { get; set; }

        public List<OperationTheatreDetail> OtDetails { get; set; }

        public string Remarks { get; set; }
        public DateTime OtDate { get; set; }

        public List<OtListModel> otlist { get; set; }

    }
}