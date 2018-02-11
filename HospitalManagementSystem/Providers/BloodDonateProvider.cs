using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class BloodDonateProviders
    {

        public List<BloodDonateModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<BloodDonateModel>(AutoMapper.Mapper.Map<IEnumerable<BloodDonate>, IEnumerable<BloodDonateModel>>(ent.BloodDonates));
            }
        }

        public void Insert(BloodDonateModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<BloodDonateModel, BloodDonate>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.BloodDonates.Add(objToSave);
                model.BloodDonateId = objToSave.BloodDonateId;
                ent.SaveChanges();

                int lastInsertedId = ent.BloodDonates.Max(x => x.BloodDonateId);

                //Insert into BloodStock
                BloodStockManagementModel modelStock = new BloodStockManagementModel();
                modelStock.QuantityUnit = model.Quantity;
                modelStock.BloodType = model.BloodGroup;
                modelStock.QuantityML = model.QuantityML;
                modelStock.PouchNumber = model.PouchNumber;
                var objTosaveStock = AutoMapper.Mapper.Map<BloodStockManagementModel, BloodStockManagement>(modelStock);
                objTosaveStock.CreatedBy = 1;
                objTosaveStock.CreatedDate = DateTime.Now;
                objTosaveStock.SourceFrom = lastInsertedId;
                ent.BloodStockManagements.Add(objTosaveStock);

                ent.SaveChanges();


                //insert into blood bank master

                BloodStockManagementMasterModel modelBloodMaster = new BloodStockManagementMasterModel();
                modelBloodMaster.BloodType = model.BloodGroup;
                modelBloodMaster.QuantityML = model.QuantityML;
                modelBloodMaster.QuantityUnit = model.Quantity;
                //insert if not update blood type
                if (isBLoodGroupExist(model.BloodGroup))
                {
                    //update
                    updateBloodStockMasterTable(modelBloodMaster);
                }
                else
                {
                    //insert
                    insertBloodStockMasterTable(modelBloodMaster);
                }




                //SaveSubjectId(model);
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



        public void Update(BloodDonateModel model)
        {
            RemoveFromBloodManagementMaster(model.BloodDonateId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.BloodDonates.Where(x => x.BloodDonateId == model.BloodDonateId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
                SaveInBloodManagement(model);
            }
        }

        private void SaveInBloodManagement(BloodDonateModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            //Insert into BloodStock
            BloodStockManagementModel modelStock = new BloodStockManagementModel();
            modelStock.QuantityUnit = model.Quantity;
            modelStock.BloodType = model.BloodGroup;
            modelStock.QuantityML = model.QuantityML;
            modelStock.PouchNumber = model.PouchNumber;
            var objTosaveStock = AutoMapper.Mapper.Map<BloodStockManagementModel, BloodStockManagement>(modelStock);
            objTosaveStock.CreatedBy = 1;
            objTosaveStock.CreatedDate = DateTime.Now;
            objTosaveStock.SourceFrom = model.BloodDonateId;
            ent.BloodStockManagements.Add(objTosaveStock);
            ent.SaveChanges();

            //throw new NotImplementedException();
        }

        private void RemoveFromBloodManagementMaster(int BloodDonateId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToStockDelete = ent.BloodStockManagements.Where(y => y.SourceFrom == BloodDonateId).FirstOrDefault();
                ent.BloodStockManagements.Remove(objToStockDelete);
                ent.SaveChanges();

                //RemoveAllFunctions(StudentRecordId);
            }

            //throw new NotImplementedException();
        }





        public void Delete(int BloodDonateID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.BloodDonates.Where(x => x.BloodDonateId == BloodDonateID).FirstOrDefault();
                ent.BloodDonates.Remove(objToDelete);

                var objToStockDelete = ent.BloodStockManagements.Where(y => y.SourceFrom == BloodDonateID).FirstOrDefault();
                ent.BloodStockManagements.Remove(objToStockDelete);


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