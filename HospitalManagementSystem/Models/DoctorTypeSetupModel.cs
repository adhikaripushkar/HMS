using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DoctorTypeSetupModel
    {

        public int DoctorTypeId { get; set; }

        [DisplayName("Doctor Type")]
        public string DoctorTypeName { get; set; }

        [DisplayName("Status")]
        public Nullable<bool> Status { get; set; }

        public List<DoctorTypeSetupModel> lstOfDoctorTypeSetupModel { get; set; }


        public DoctorSpecialistViewModel ObjDoctorSpecialistViewModel { get; set; }
        public List<DoctorSpecialistViewModel> DoctorSpecialistViewModelList { get; set; }
        public DoctorTypeSetupModel()
        {
            ObjDoctorSpecialistViewModel = new DoctorSpecialistViewModel();
            DoctorSpecialistViewModelList = new List<DoctorSpecialistViewModel>();
        }
    }


    public class DoctorSpecialistViewModel
    {
        public int DoctorDiseaseID { get; set; }
        public int DoctorID { get; set; }
        public int DepartmentID { get; set; }
        public Boolean Status { get; set; }
        public string DiseaseName { get; set; }
        public int DiseaseID { get; set; }
        public int BranchId { get; set; }


    }
}