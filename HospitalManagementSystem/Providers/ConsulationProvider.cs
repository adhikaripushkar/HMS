using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class ConsulationProvider
    {
        public int Insertvitals(ConsulationModel model)
        {
            try
            {
                int i = 0;
                using (EHMSEntities ent = new EHMSEntities())
                {
                    var objToSave = AutoMapper.Mapper.Map<ConsulationModel, Consulation>(model);
                    ent.Consulations.Add(objToSave);
                    i = ent.SaveChanges();
                }
                return i;
            }
            catch (Exception e)
            {

                return 0;
            }

        }
        public List<ConsulationModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<ConsulationModel>(AutoMapper.Mapper.Map<IEnumerable<Consulation>, IEnumerable<ConsulationModel>>(ent.Consulations).Where(m => m.EmergencyMasterId == id));
            }
        }
        public List<ConsulationModel> GetListForConsulation()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<ConsulationModel>(AutoMapper.Mapper.Map<IEnumerable<Consulation>, IEnumerable<ConsulationModel>>(ent.Consulations));


            }
        }
        public void Update(ConsulationModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.Consulations.Where(x => x.Id == model.Id).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();

            }
        }
    }
}