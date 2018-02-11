using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupDiagnosisProvider
    {
        public List<SetupDiagnosisModel> GetDiagnosisList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetupDiagnosisModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDiagnosi>, IEnumerable<SetupDiagnosisModel>>(ent.SetupDiagnosis));


            }
        }
        public int Insert(SetupDiagnosisModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupDiagnosisModel, SetupDiagnosi>(model);
                objToSave.Status = true;
                ent.SetupDiagnosis.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }

        public int Update(SetupDiagnosisModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupDiagnosis.Where(x => x.DiagnosisId == model.DiagnosisId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }

        public int Delete(int DiagnosisId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupDiagnosis.Where(x => x.DiagnosisId == DiagnosisId).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupDiagnosis.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


    }
}