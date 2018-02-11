using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmergencyInvestigationMasterProvider
    {
        public int Insert(SetupEmergencyInvestigationMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupEmergencyInvestigationMasterModel, EmergencyInvestigationMaster>(model);
                objToSave.Status = "Y";
                ent.EmergencyInvestigationMasters.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<SetupEmergencyInvestigationMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmergencyInvestigationMasterModel> model = new List<SetupEmergencyInvestigationMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyInvestigationMaster>, IEnumerable<SetupEmergencyInvestigationMasterModel>>(ent.EmergencyInvestigationMasters).ToList();
                return model;
            }
        }
        public int Update(SetupEmergencyInvestigationMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmergencyInvestigationMasters.Where(x => x.EInvId == model.EInvId).FirstOrDefault();
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
                var objToDelete = ent.EmergencyInvestigationMasters.Where(x => x.EInvId == id).FirstOrDefault();
                ent.EmergencyInvestigationMasters.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}