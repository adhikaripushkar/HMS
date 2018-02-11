using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockCategoryProvider
    {
        public List<StockCategoryModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<StockCategoryModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockCategory>, IEnumerable<StockCategoryModel>>(ent.SetupStockCategories));
            }
        }

        public int Insert(StockCategoryModel model)
        {
            EHMSEntities ent = new EHMSEntities();

            var item = ent.SetupStockCategories.Where(x => x.StockCategoryName == model.StockCategoryName);

            if (item.Count() == 0)
            {
                var objToSave = AutoMapper.Mapper.Map<StockCategoryModel, SetupStockCategory>(model);
                objToSave.CreatedBy = 0;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.Status = true;
                ent.SetupStockCategories.Add(objToSave);
                int i = ent.SaveChanges();
                return i;
            }
            else
            {
                return 0;
            }

        }

        public int Update(StockCategoryModel model)
        {
            EHMSEntities ent = new EHMSEntities();

            var item = ent.SetupStockCategories.Where(x => x.StockCategoryName == model.StockCategoryName && x.StockCategoryID != model.StockCategoryID);

            if (item.Count() == 0)
            {
                var objToEdit = ent.SetupStockCategories.Where(x => x.StockCategoryID == model.StockCategoryID).FirstOrDefault();
                model.CreatedBy = objToEdit.CreatedBy;
                model.CreatedDate = objToEdit.CreatedDate;
                model.Status = objToEdit.Status;
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                int i = ent.SaveChanges();
                return i;
            }
            else
            {
                return 0;
            }

        }

        public int Delete(int deleteid)
        {
            EHMSEntities ent = new EHMSEntities();
            var objtodelete = ent.SetupStockCategories.Where(x => x.StockCategoryID == deleteid).FirstOrDefault();
            //ent.Entry(objtodelete).State = System.Data.EntityState.Deleted;
            ent.SetupStockCategories.Remove(objtodelete);
            int i = ent.SaveChanges();
            return i;
        }
    }
}