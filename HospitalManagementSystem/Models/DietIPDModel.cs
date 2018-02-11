using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DietIPDModel
    {

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DietTypeId { get; set; }

        public DietTypeSetupViewModel ObjDietTypeSetupViewModel { get; set; }
        public List<DietTypeSetupViewModel> DietTypeSetupViewModelList { get; set; }

        public PatientDietDetailsViewModel ObjPatientDietDetailsViewModel { get; set; }
        public List<PatientDietDetailsViewModel> PatientDietDetailsViewModelList { get; set; }

        public List<PatientInformationDetailsViewModel> PatientInformationDetailsViewModeList { get; set; }
        public PatientInformationDetailsViewModel ObjPatientInformationDetailsViewModel { get; set; }

        public DietIPDModel()
        {
            ObjDietTypeSetupViewModel = new DietTypeSetupViewModel();
            DietTypeSetupViewModelList = new List<DietTypeSetupViewModel>();
            PatientDietDetailsViewModelList = new List<PatientDietDetailsViewModel>();
            ObjPatientDietDetailsViewModel = new PatientDietDetailsViewModel();

            ObjPatientInformationDetailsViewModel = new PatientInformationDetailsViewModel();
            PatientInformationDetailsViewModeList = new List<PatientInformationDetailsViewModel>();
        }
    }

    public class DietTypeSetupViewModel
    {
        public int DietTypeSetupId { get; set; }
        public string TypeName { get; set; }
        public int Status { get; set; }

    }

    public class PatientDietDetailsViewModel
    {
        public int PatientDietMasterId { get; set; }
        public int IPDRegistrationId { get; set; }
        public int OPDId { get; set; }
        [Display(Name = "Diet Type")]
        public int DietTypeId { get; set; }
        public string Diet { get; set; }
        [Display(Name = "Date")]
        public DateTime DietDate { get; set; }

        [Display(Name = "Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{hh:mm tt}")]
        [Timestamp]
        public TimeSpan DietTime { get; set; }
        [Display(Name = "Nurse")]
        public int NurseInChargeId { get; set; }
        [Display(Name = "Nurse Name (Others)")]
        public string NurseName { get; set; }
        [Display(Name = "Is Diet Taken")]
        public int IsDietTaken { get; set; }

        public int Status { get; set; }


    }

    public class PatientInformationDetailsViewModel
    {
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int WardNo { get; set; }
        public int RoomNo { get; set; }
        public int BedNo { get; set; }
        public int PatientID { get; set; }
        public int IpdRegisterationID { get; set; }
    }
}