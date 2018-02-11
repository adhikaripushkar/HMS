using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class EmergencyTreatmentAllergiesProvider
    {


        public int InsertEmergencyTreatmentAllergies(EmergecyMasterModel model)
        {
            try
            {
                int i = 0;
                using (EHMSEntities ent = new EHMSEntities())
                {
                    var objToSave = AutoMapper.Mapper.Map<EmergencyTreatmentAllergiesModel, EmergencyTreatmentAllergy>(model.EmergencyTreatmentAllergiesModel);
                    objToSave.EmergencyMasterId = model.EmergencyMasterId;
                    objToSave.CreatedBy = 1;
                    objToSave.Status = 1;
                    objToSave.CreatedDate = DateTime.Now;
                    ent.EmergencyTreatmentAllergies.Add(objToSave);
                    i = ent.SaveChanges();
                }
                return i;
            }
            catch (Exception e)
            {

                return 0;
            }

        }
        public List<EmergencyTreatmentAllergiesModel> GetListForEmergencyTreatmentAllergies()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<EmergencyTreatmentAllergiesModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTreatmentAllergy>, IEnumerable<EmergencyTreatmentAllergiesModel>>(ent.EmergencyTreatmentAllergies));


            }
        }
        public int UpdateForEmergencyTreatmentAllergies(EmergencyTreatmentAllergiesModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmergencyTreatmentAllergies.Where(x => x.TreatmentAllergiesId == model.TreatmentAllergiesId).FirstOrDefault();
                model.CreatedBy = 1;
                model.Status = 1;
                model.CreatedDate = DateTime.Now;
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();

            }
            return i;
        }
        public List<EmergencyTreatmentAllergiesModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<EmergencyTreatmentAllergiesModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTreatmentAllergy>, IEnumerable<EmergencyTreatmentAllergiesModel>>(ent.EmergencyTreatmentAllergies).Where(m => m.EmergencyMasterId == id));
            }
        }
    }
}