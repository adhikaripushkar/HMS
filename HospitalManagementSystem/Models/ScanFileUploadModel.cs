using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ScanFileUploadModel
    {

        public int ScanFileUploadsID { get; set; }
        [DisplayName("Patient Number")]
        public int PatientID { get; set; }
        [DisplayName("Department Name")]
        public int DepartmentID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateDateBy { get; set; }
        public int CreateBy { get; set; }
        public Boolean Status { get; set; }

        public List<ScanFileUploadModel> ListScanFIleUploadModle { get; set; }
    }
}