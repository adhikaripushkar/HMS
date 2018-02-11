using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class IpdMRCommonTestModel
    {
        public int IpdMRCommonTestID { get; set; }
        public int? IpdRegistrationID { get; set; }
        public int? OpdID { get; set; }
        public string CommonTestName { get; set; }
        public string Details { get; set; }
        public DateTime IpdMRcCommonTesDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateByDate { get; set; }
        //public DateTime IpdMRcCommonTesDate { get; set; }


        public int maxid { get; set; }
        public DateTime Date { get; set; }
        public virtual List<IpdMRCommonTestModel> IpdMRCommonTestModeList { get; set; }


        public List<IpdSearchResults> IpdSearchResultList { get; set; }

    }
    public class IpdMedicalRecord : IpdSearchResults
    {
        public int IpdMrMedicalRecordID { get; set; }
        public int IpdRegisterationID { get; set; }
        public int PatientID { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string Doses { get; set; }
        [Required]
        public string DosesTimes { get; set; }
        [Required]
        public string DosesTotalDays { get; set; }
        [DisplayName("Insert Date")]
        [Required]
        public DateTime InsertedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }
        public bool status { get; set; }
        public int RegistrationId { get; set; }


        public IpdMedicalRecord()
        {
            IpdMedicalRecordList = new List<IpdMedicalRecord>();
            IpdMedicalRecordValue = new List<IpdMedicalRecord>();
            IpdMedicalRecordDataList = new List<IpdMedicalRecord>();
        }
        public virtual List<IpdMedicalRecord> IpdMedicalRecordList { get; set; }
        public List<IpdMedicalRecord> IpdMedicalRecordValue { get; set; }
        public List<IpdMedicalRecord> IpdMedicalRecordDataList { get; set; }


        public int Staff { get; set; }
        public string Shift { get; set; }
        public TimeSpan Time { get; set; }

    }
    public class IpdMrMainTestModel
    {
        public int IpdMrMainTestID { get; set; }

        public int IpdRegistrationID { get; set; }

        public string FullName { get; set; }

        public DateTime ResistrationDate { get; set; }
        public int BedNo { get; set; }

        public int DepartmentId { get; set; }

        public int RoomNo { get; set; }

        public int WardNo { get; set; }


        public int PatientID { get; set; }

        public int TestID { get; set; }

        public String TestName { get; set; }

        public String SectionName { get; set; }

        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }
        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InsertedDate { get; set; }
        public int SectionId { get; set; }
        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public bool status { get; set; }
        public bool isSelected { get; set; }
        public IpdMrMainTestModel()
        {

            IpdMrMainTestModelList = new List<IpdMrMainTestModel>();
            IpdMedicalRecordList = new List<IpdMedicalRecord>();
            //IpdSearchResultList = new List<IpdSearchResults>();
            SectionTestCheckBoxList = new List<SectionTestCheckBox>();

        }

        public List<IpdMrMainTestModel> IpdMrMainTestModelList { get; set; }
        public virtual List<IpdMedicalRecord> IpdMedicalRecordList { get; set; }
        public List<SectionTestCheckBox> SectionTestCheckBoxList { get; set; }
        public List<SectionTestCheckBox> SearchSectionCheckBoxValues { get; set; }
        //  public List<IpdSearchResults>IpdSearchResultList{get;set;}


    }
    public class SectionTestCheckBox
    {
        public int id { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string TestName { get; set; }
        public int TestID { get; set; }
        public bool isSelected { get; set; }
    }





}