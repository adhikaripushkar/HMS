using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmergencyCaseProvider
    {
        public int Insert(SetupEmergencyCaseModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupEmergencyCaseModel, SetupEmergencyCase>(model);
                objToSave.Status = "Y";
                ent.SetupEmergencyCases.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<SetupEmergencyCaseModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmergencyCaseModel> model = new List<SetupEmergencyCaseModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<SetupEmergencyCase>, IEnumerable<SetupEmergencyCaseModel>>(ent.SetupEmergencyCases).ToList();
                return model;
            }
        }
        public int Update(SetupEmergencyCaseModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupEmergencyCases.Where(x => x.CaseId == model.CaseId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);

                objToEdit.Status = "Y";

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }
            return i;
        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupEmergencyCases.Where(x => x.CaseId == id).FirstOrDefault();
                ent.SetupEmergencyCases.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}