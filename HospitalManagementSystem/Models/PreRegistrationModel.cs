using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects.DataClasses;

namespace HospitalManagementSystem.Models
{
    public class PreRegistrationModel : OpdModel
    {
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public int PreRegistrationID { get; set; }
        public int PrefailID { get; set; }
        public int? Age { get; set; }
        public string ContactNumber { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public PreRegistrationModel()
        {
            PreRegistrationPreferModelList = new List<PreRegistrationPreferDetailsModel>();
            OpdDoctorList = new List<OpdDoctorListModel>();
            PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();

        }
        public PreRegistrationDoctorDisease PreRegistrationDoctorDisease { get; set; }
        public List<PreRegistrationModel> PreRegistrationModelList { get; set; }
        public PreRegistrationPreferDetailsModel PreregistrationPreferDetailsModel { get; set; }
        public List<PreRegistrationPreferDetailsModel> PreRegistrationPreferModelList { get; set; }


    }
    public class ReportModel
    {
        [DisplayName("Start Date")]
        [Required]
        public DateTime? FromDateRegMode { get; set; }
        [DisplayName("EndDate")]
        [Required]
        public DateTime? ToDateRegMode { get; set; }

        public int CountMale { get; set; }
        public int CountFemale { get; set; }
        public int AbsTotal { get; set; }
        public int Absent { get; set; }

        public int Total { get; set; }

        public string Regstrationmode { get; set; }


        public List<ReportModel> ReportModelList { get; set; }


    }
    public class PreRegistrationDoctorTime
    {
        [DisplayName("Doctor Routine Start Time")]
        public TimeSpan StartTime { get; set; }
        [DisplayName("Doctor Routine End Time")]
        public TimeSpan EndTime { get; set; }
        [DisplayName("Sorry, Doctor is not avalilable at this Date ")]
        public string Error { get; set; }
        public List<PreRegistrationDoctorTime> PreRegistrationDoctorTimeList { get; set; }

    }
    public class PreRegistrationDoctorDisease
    {
        public int DoctorDiseaseID { get; set; }
        public int DoctorID { get; set; }
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public List<PreRegistrationDoctorDisease> PreRegistrationDoctorDiseaseList { get; set; }

    }
    public class PreRegistrationPreferDetailsModel
    {
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public int PreferDetailsID { get; set; }
        public int PreRegistrationID { get; set; }
        [Required]
        [DisplayName("Department Name")]
        public int DepartmentID { get; set; }


        [DisplayName("Doctor Name")]
        public int DoctorID { get; set; }

        [Required]
        [DisplayName("Prefer Date")]
        [DataType(DataType.Date)]

        public DateTime? PreferDate { get; set; }
        [Required]
        [DisplayName("Prefer Time")]
        public TimeSpan? PreferTime { get; set; }


        public IEnumerable<SelectListItem> GetDepartmentList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupDepartments.ToList(), "DeptID", "DeptName");
            }
        }

        public IEnumerable<SelectListItem> GetDoctorsList()
        {


            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                if (DepartmentID == 0)
                {
                    var collection = (from c in ent.SetupDoctorMasters
                                      join b in ent.SetupDoctorDepartments on c.DoctorID equals b.DoctorID
                                      where b.DepartmentID == 1
                                      select new { c.DoctorID, c.FirstName, c.MiddleName, c.LastName }).Distinct();

                    ddlList.Add(new SelectListItem { Text = "--Select doctor", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
                    }
                    return ddlList;
                }
                else
                {

                    var collection = (from c in ent.SetupDoctorMasters
                                      join b in ent.SetupDoctorDepartments on c.DoctorID equals b.DoctorID
                                      where b.DepartmentID == DepartmentID
                                      select new { c.DoctorID, c.FirstName, c.MiddleName, c.LastName }).Distinct();

                    ddlList.Add(new SelectListItem { Text = "--Select doctor", Value = null });
                    foreach (var item in collection)
                    {
                        ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
                    }
                    return ddlList;




                }

            }
        }
        public DateTime getdate(int a)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                return (ent.PreRegistrationPreferDetails.Where(b => b.PreferDetailsID == a).FirstOrDefault().PreferDate);

            }


        }


    }
}