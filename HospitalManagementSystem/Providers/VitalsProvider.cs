using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;



namespace HospitalManagementSystem.Providers
{
    public class VitalsProvider
    {
        public int Insertvitals(VitalsModel model)
        {
            try
            {
                int i = 0;
                using (EHMSEntities ent = new EHMSEntities())
                {
                    var objToSave = AutoMapper.Mapper.Map<VitalsModel, Vital>(model);
                    ent.Vitals.Add(objToSave);
                    i = ent.SaveChanges();
                }
                return i;
            }
            catch (Exception e)
            {

                return 0;
            }

        }
        public List<VitalsModel> GetListForvitals()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<VitalsModel>(AutoMapper.Mapper.Map<IEnumerable<Vital>, IEnumerable<VitalsModel>>(ent.Vitals));


            }
        }
        public void UpdateForVitals(VitalsModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.Vitals.Where(x => x.EmergencyVitalId == model.EmergencyVitalId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();

            }
        }
        public void DeleteForVitals(int VitalId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.Vitals.Where(x => x.EmergencyVitalId == VitalId).FirstOrDefault();
                ent.Vitals.Remove(objToDelete);
                ent.SaveChanges();

            }
        }
        public List<VitalsModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<VitalsModel>(AutoMapper.Mapper.Map<IEnumerable<Vital>, IEnumerable<VitalsModel>>(ent.Vitals).Where(m => m.EmergencyMasterId == id));
            }
        }
        public List<StaffForERegisModel> getStaffForERegisModelList(string StaffName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<StaffForERegisModel>("GetStaffNameforE '" + StaffName + "%'").ToList();

            }
        }



    }

}