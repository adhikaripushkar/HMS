using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class DepositMasterModel
    {
        public int PatientDepositMasterId { get; set; }
        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Id")]
        public int PatientDepartmentId { get; set; }
        [Display(Name = "Deposit By")]
        public string DepostedBy { get; set; }
        [Display(Name = "Relation")]
        public int RelationId { get; set; }
        [Display(Name = "Deposit Amount")]
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 99999999.99)]
        public decimal DepositedAmount { get; set; }
        //public string SwipeCardId { get; set; }
        //public string SwipeCardDetail { get; set; }

        [Display(Name = "Deposit Date")]
        public DateTime DepositedDate { get; set; }
        public Boolean Status { get; set; }
        [Display(Name = "Deposit Type")]
        public int DepositeType { get; set; }
        public int CreatedBy { get; set; }
        public List<DepositMasterModel> DepositMasterModelList { get; set; }
        public PatientPartialDetails objPatientPartialDetail { get; set; }
        public List<PatientPartialDetails> PatientPartialDetailsList { get; set; }

        public DepositMasterModel()
        {
            PatientDepositDetail objPatientPartialDetail = new PatientDepositDetail();
        }

    }

    public class PatientPartialDetails
    {
        public int? PatientId { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientFullName { get; set; }

        public int? Age { get; set; }
        public string Sex { get; set; }
        [Display(Name = "Department Id")]
        public int? DepartmentID { get; set; }
        [Display(Name = "Department Id")]
        public int? PatientDepartmentId { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RegistrationDate { get; set; }


        public List<PatientPartialDetails> PatientPartialDetailsList { get; set; }


    }
}