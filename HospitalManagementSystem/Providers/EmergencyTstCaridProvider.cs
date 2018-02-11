using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class EmergencyTstCaridProvider
    {
        public int InsertEPatientTstCarried(EmergecyMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.ListEmergencyTstCaridModel)
                {
                    model.EmergencyTstCaridModel = new EmergencyTstCaridModel();
                    var objToSave = AutoMapper.Mapper.Map<EmergencyTstCaridModel, EmergencyTstCarid>(model.EmergencyTstCaridModel);
                    objToSave.EmergencyMasterId = model.EmergencyMasterId;
                    objToSave.SectionId = item.SectionId;
                    objToSave.TestId = item.TestId;
                    objToSave.ShortDecs = item.ShortDecs;
                    objToSave.LongDesc = item.LongDesc;
                    objToSave.Remarks = item.Remarks;
                    objToSave.Status = "Y";
                    ent.EmergencyTstCarids.Add(objToSave);
                    i = ent.SaveChanges();
                }
                return i;
            }


        }
        public List<EmergencyTstCaridModel> GetListForEmergencyPatientTestCarried()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<EmergencyTstCaridModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTstCarid>, IEnumerable<EmergencyTstCaridModel>>(ent.EmergencyTstCarids));


            }
        }
        public List<EmergencyTstCaridModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<EmergencyTstCaridModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTstCarid>, IEnumerable<EmergencyTstCaridModel>>(ent.EmergencyTstCarids).Where(m => m.EmergencyMasterId == id));
            }
        }
        public int UpdateForEmergencyTestCarried(EmergencyTstCaridModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmergencyTstCarids.Where(x => x.EmTestd == model.EmTestd).FirstOrDefault();
                model.Status = "Y";
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();

            }
            return i;
        }

    }
}