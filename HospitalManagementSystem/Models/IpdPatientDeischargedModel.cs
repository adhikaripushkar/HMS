using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class IpdPatientDeischargedModel
    {

        public int OpdID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string ContactName { get; set; }
        public int IpdRegistrationID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int IpdDischargeID { get; set; }
        public DateTime LeaveDate { get; set; }
        public int StayTotalDay { get; set; }
        // public int DiagnosisID { get; set; }
        public int IpdPatientRoomDetailId { get; set; }
        public int IpdPatientBedDetailId { get; set; }
        public int BedNumber { get; set; }
        public int RoomNo { get; set; }

        public List<IpdPatientDeischargedModel> lstIpdPatientDeischarged { get; set; }
        //public IpdPatientDeischarged IpdPatientDeischarged { get; set; }
        public PatientBillDetailsViewModelNew ObjPatientBillDetailsViewModelNew { get; set; }
        public List<PatientBillDetailsViewModelNew> PatientBillDetailsViewModelNewList { get; set; }

        public IpdPatientDeischargedModel()
        {
            ObjPatientBillDetailsViewModelNew = new PatientBillDetailsViewModelNew();
            PatientBillDetailsViewModelNewList = new List<PatientBillDetailsViewModelNew>();
        }





        public List<IpdPatientDeischargedModel> GetList()
        {
            EHMSEntities ent = new EHMSEntities();
            IpdPatientDeischargedModel obj = new IpdPatientDeischargedModel();
            obj.lstIpdPatientDeischarged = ent.Database.SqlQuery<IpdPatientDeischargedModel>("sp_IpdPatientDeischarged").ToList();
            return obj.lstIpdPatientDeischarged;
        }

        public int UpdateIpdDischargeDetails(IpdDischargeModel model)
        {

            int i = 0;

            EHMSEntities ent = new EHMSEntities();
            var edit2save = ent.IpdDischarges.Where(x => x.IpdDischargeID == model.IpdDischargeID).SingleOrDefault();
            edit2save.BriefHistory = model.BriefHistory;
            edit2save.ClinicalFindings = model.ClinicalFindings;
            edit2save.Investigation = model.Investigation;
            edit2save.Medicinetreatment = model.Medicinetreatment;
            edit2save.FurtherTreatment = model.FurtherTreatment;
            edit2save.FoloowUp = model.FoloowUp;
            ent.Entry(edit2save).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();
            return i;


        }




    }
    public class PatientBillDetailsViewModelNew
    {
        public int IPDId { get; set; }
        public int OpdId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime DischargedDate { get; set; }

        public decimal TenderAmount { get; set; }
        public decimal ReturnedAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int BedNo { get; set; }
        public decimal BalanceDeposit { get; set; }
        public int AccsubgroupHeadId { get; set; }
        public int PaymentMode { get; set; }
        public decimal AmountWithOutTax { get; set; }
        public decimal TaxAmountOnly { get; set; }

        //public List<PatientBillDetailsViewModel> PatientBillDetailsViewModelList { get; set; }

    }
}