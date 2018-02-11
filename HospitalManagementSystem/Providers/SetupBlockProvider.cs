using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class SetupBlockProviders
    {
        public int Insert(SetupBlockModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupBlockModel, SetupBlock>(model);
                objToSave.Status = "Y";
                ent.SetupBlocks.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<SetupBlockModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupBlockModel> model = new List<SetupBlockModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<SetupBlock>, IEnumerable<SetupBlockModel>>(ent.SetupBlocks).ToList();
                return model;
            }
        }
        public int Update(SetupBlockModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupBlocks.Where(x => x.BlockId == model.BlockId).FirstOrDefault();
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
                var objToDelete = ent.SetupBlocks.Where(x => x.BlockId == id).FirstOrDefault();
                ent.SetupBlocks.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
        public void ChangToInactive(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupBlocks.Where(x => x.BlockId == id).FirstOrDefault();
            objt.Status = "N";
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
        public void ChangToActive(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupBlocks.Where(x => x.BlockId == id).FirstOrDefault();
            objt.Status = "Y";
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}