using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class ComplainEntryProviders
    {
        public void UpdateEmasterForComplainEntry(EmergecyMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == model.EmergencyMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }

        }
        public List<EmergecyMasterModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<EmergecyMasterModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<EmergecyMasterModel>>(ent.EmergencyMasters).Where(m => m.EmergencyMasterId == id));
            }
        }
    }
}