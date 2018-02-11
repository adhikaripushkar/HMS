using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockSubCategoryProvider
    {
        public List<StockSubCategoryModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<StockSubCategoryModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockSubCategory>, IEnumerable<StockSubCategoryModel>>(ent.SetupStockSubCategories));
            }
        }

        public int Insert(StockSubCategoryModel model)
        {
            EHMSEntities ent = new EHMSEntities();

            var item = ent.SetupStockSubCategories.Where(x => x.StockSubCategoryName == model.StockSubCategoryName);

            if (item.Count() == 0)
            {
                var objToSave = AutoMapper.Mapper.Map<StockSubCategoryModel, SetupStockSubCategory>(model);
                objToSave.CreatedBy = 0;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.Status = true;
                ent.SetupStockSubCategories.Add(objToSave);
                int i = ent.SaveChanges();
                return i;
            }
            else
            {
                return 0;
            }

        }

        public int Update(StockSubCategoryModel model)
        {
            EHMSEntities ent = new EHMSEntities();

            var item = ent.SetupStockSubCategories.Where(x => x.StockSubCategoryName == model.StockSubCategoryName && x.StockSubCategoryID != model.StockSubCategoryID);

            if (item.Count() == 0)
            {
                var objToEdit = ent.SetupStockSubCategories.Where(x => x.StockSubCategoryID == model.StockSubCategoryID).FirstOrDefault();
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
            var objtodelete = ent.SetupStockSubCategories.Where(x => x.StockSubCategoryID == deleteid).FirstOrDefault();
            //ent.Entry(objtodelete).State = System.Data.EntityState.Deleted;
            ent.SetupStockSubCategories.Remove(objtodelete);
            int i = ent.SaveChanges();
            return i;
        }
    }
}