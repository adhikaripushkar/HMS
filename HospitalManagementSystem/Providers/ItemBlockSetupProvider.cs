using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class ItemBlockSetupProvider
    {
        public List<ItemBlockSetupModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<ItemBlockSetupModel> model = new List<ItemBlockSetupModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<ItemBlockSetup>, IEnumerable<ItemBlockSetupModel>>(ent.ItemBlockSetups).ToList();
                return model;
            }
        }
        public int Insert(ItemBlockSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<ItemBlockSetupModel, ItemBlockSetup>(model);
                objToSave.Status = true;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Today;
                ent.ItemBlockSetups.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public int Update(ItemBlockSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.ItemBlockSetups.Where(x => x.ItemBlockSetupID == model.ItemBlcokSetupID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);

                objToEdit.Status = true;
                objToEdit.CreatedBy = 1;
                objToEdit.CreatedDate = DateTime.Today;
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
                var objToDelete = ent.ItemBlockSetups.Where(x => x.ItemBlockSetupID == id).FirstOrDefault();
                ent.ItemBlockSetups.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}