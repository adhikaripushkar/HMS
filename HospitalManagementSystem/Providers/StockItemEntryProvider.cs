using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockItemEntryProvider
    {
        public List<StockItemEntryModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockItemEntryModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockItemEntry>, IEnumerable<StockItemEntryModel>>(ent.SetupStockItemEntries.Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public bool Insert(StockItemEntryModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupStockItemEntries.Where(x => x.StockItemName == model.StockItemName).Any())
                {
                    return false;
                }
                var objToSave = AutoMapper.Mapper.Map<StockItemEntryModel, SetupStockItemEntry>(model);

                objToSave.Status = true;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupStockItemEntries.Add(objToSave);
                ent.SaveChanges();
                //int id = ent.SetupStockItemEntry.Where(x=>x.StockItemEntryId==ent.SetupStockItemEntry.Max(y=>y.StockItemEntryId)).SingleOrDefault().StockItemEntryId;

                var obj = new StockItemMaster();
                obj.StockItemEntryId = objToSave.StockItemEntryId;
                obj.UnitId = model.StockUnitId;
                obj.Quantity = model.OpeningStock;
                obj.Status = true;
                obj.CreatedBy = 1;
                obj.CreatedDate = DateTime.Now;
                ent.StockItemMasters.Add(obj);
                ent.SaveChanges();
                return true;
            }

        }

        public void Update(StockItemEntryModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockItemMasters.Where(x => x.StockItemEntryId == model.StockItemEntryId).SingleOrDefault();
            obj.Quantity = obj.Quantity + model.OpeningStock - obj.SetupStockItemEntry.OpeningStock;
            obj.UnitId = model.StockUnitId;
            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.Status = obj.Status;
            AutoMapper.Mapper.Map(model, obj.SetupStockItemEntry);
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }



    }
}