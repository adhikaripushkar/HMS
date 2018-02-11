using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class BloodStockManagementProviders
    {
        public List<BloodStockManagementModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<BloodStockManagementModel>(AutoMapper.Mapper.Map<IEnumerable<BloodStockManagement>, IEnumerable<BloodStockManagementModel>>(ent.BloodStockManagements));
            }
        }

        public void Insert(BloodStockManagementModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<BloodStockManagementModel, BloodStockManagement>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.SourceFrom = 0;//direct entry from stock entry
                ent.BloodStockManagements.Add(objToSave);
                ent.SaveChanges();
                //insert into blood bank master

                BloodStockManagementMasterModel modelBloodMaster = new BloodStockManagementMasterModel();
                modelBloodMaster.BloodType = model.BloodType;
                modelBloodMaster.QuantityML = model.QuantityML;
                modelBloodMaster.QuantityUnit = model.QuantityUnit;
                if (isBLoodGroupExist(model.BloodType))
                {
                    //update
                    updateBloodStockMasterTable(modelBloodMaster);
                }
                else
                {
                    //insert
                    insertBloodStockMasterTable(modelBloodMaster);
                }

            }
        }
        private void updateBloodStockMasterTable(BloodStockManagementMasterModel modelBloodMaster)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                BloodStockManagementMaster e = (from e1 in ent.BloodStockManagementMasters
                                                where e1.BloodType == modelBloodMaster.BloodType
                                                select e1).First();

                e.QuantityUnit = e.QuantityUnit + modelBloodMaster.QuantityUnit;
                e.QuantityML = e.QuantityML + modelBloodMaster.QuantityML;
                ent.SaveChanges();

            }

        }


        private void insertBloodStockMasterTable(BloodStockManagementMasterModel modelBloodMaster)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToSave = AutoMapper.Mapper.Map<BloodStockManagementMasterModel, BloodStockManagementMaster>(modelBloodMaster);
            ent.BloodStockManagementMasters.Add(objToSave);
            ent.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Update(BloodStockManagementModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.BloodStockManagements.Where(x => x.BloodStockId == model.BloodStockId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();

                //SaveSubjectId(model);
            }
        }

        public void Delete(int BloodStockID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.BloodStockManagements.Where(x => x.BloodStockId == BloodStockID).FirstOrDefault();
                ent.BloodStockManagements.Remove(objToDelete);
                ent.SaveChanges();

                //RemoveAllFunctions(StudentRecordId);
            }
        }
        public static bool isBLoodGroupExist(int bloodGroupType)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.BloodStockManagementMasters.Where(x => x.BloodType == bloodGroupType).Any();
            }
        }
    }
}