using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class PreRegistrationCommonModel
    {
        public List<PreRegistrationModel> PreRegistrationModel { get; set; }
        public List<PreRegistrationPreferDetailsModel> PreRegistrationPreferDetailsModel { get; set; }

        public PreRegistrationCommonModel(List<PreRegistrationModel> _preregisterationmodel, List<PreRegistrationPreferDetailsModel> _preregistrationpreferdetailsmodel)
        {
            this.PreRegistrationPreferDetailsModel = _preregistrationpreferdetailsmodel;
            this.PreRegistrationModel = _preregisterationmodel;


        }
    }
}