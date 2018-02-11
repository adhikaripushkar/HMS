using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DischargeProvider
    {
        public List<EmergecyMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return new List<EmergencyTriageModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTriage>, IEnumerable<emer>>(ent.PreRegistration));

                List<EmergecyMasterModel> model = new List<EmergecyMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<EmergecyMasterModel>>(ent.EmergencyMasters).ToList();

                foreach (var item in model)
                {
                    var data = ent.EmergencyTriages.Where(x => x.EmergencyMasterId == item.EmergencyMasterId).FirstOrDefault();

                    if (data != null)
                    {

                        EmergencyTriageModel m = new EmergencyTriageModel()
                        {

                            Time = data.Time,
                            DoctorId = (int)data.DoctorId,
                            MediumId = (int)data.MediumId,
                            sourceTypeId = (int)data.sourceTypeId,
                            SourceId = (int)data.SourceId,
                            TriageLevel = (int)data.TriageLevel
                        };
                        item.EmergencyTriageModel = m;
                    }
                }
                return model;
            }
        }
        public void Update(EmergecyMasterModel model)
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
        //public EmergecyMasterModel GetSelectedData(int id)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var data = (from a in ent.EmergencyMaster
        //                    where a.EmergencyMasterId == id
        //                    select new { a.EmergencyNumber, a.FirstName, a.LastName }).ToList();
        //        return data;
        //    }
        //}
    }
}