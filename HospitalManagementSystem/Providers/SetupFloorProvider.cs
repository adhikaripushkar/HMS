using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupFloorProviders
    {
        public int Insert(SetupFloorModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupFloorModel, SetupFloor>(model);
                objToSave.Status = "Y";
                ent.SetupFloors.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<SetupFloorModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupFloorModel> model = new List<SetupFloorModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<SetupFloor>, IEnumerable<SetupFloorModel>>(ent.SetupFloors).ToList();
                return model;
            }
        }
        public int Update(SetupFloorModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupFloors.Where(x => x.FloorId == model.FloorId).FirstOrDefault();
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
                var objToDelete = ent.SetupFloors.Where(x => x.FloorId == id).FirstOrDefault();
                ent.SetupFloors.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
        public void ChangToInactive(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupFloors.Where(x => x.FloorId == id).FirstOrDefault();
            objt.Status = "N";
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
        public void ChangToActive(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupFloors.Where(x => x.FloorId == id).FirstOrDefault();
            objt.Status = "Y";
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}