using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockUnitProvider
    {
        public List<StockUnitModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockUnitModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockUnit>, IEnumerable<StockUnitModel>>(ent.SetupStockUnits.Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(StockUnitModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupStockUnits.Where(x => x.StockUnitName == model.StockUnitName).Any())
                {
                    return 0;
                }
                var objToSave = AutoMapper.Mapper.Map<StockUnitModel, SetupStockUnit>(model);
                objToSave.Status = true;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupStockUnits.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;

        }

        public int Delete(int id)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SetupStockUnits.Where(x => x.StockUnitID == id).SingleOrDefault();
            ent.SetupStockUnits.Remove(obj);
            i = ent.SaveChanges();
            return i;
        }

    }
}