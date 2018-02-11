using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class BloodTransectionProvider
    {
        public List<BloodTransectionModel> GetBloodTransectionLists()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<BloodTransectionModel>(AutoMapper.Mapper.Map<IEnumerable<BloodTransectionMaster>, IEnumerable<BloodTransectionModel>>(ent.BloodTransectionMasters));
            }
        }



        public void Insert(BloodTransectionModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<BloodTransectionModel, BloodTransectionMaster>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.BloodTransectionMasters.Add(objToSave);

                //int lastInsertedId = objToSave.BloodTransectionMasterId;
                //model.ObjBTDetails = new BloodTransectionDetailModel();
                var objToSaveDetails = AutoMapper.Mapper.Map<BloodTransectionDetailModel, BloodTransectionDetail>(model.ObjBTDetails);
                objToSaveDetails.BloodTransectionMasterId = objToSave.BloodTransectionMasterId;
                ent.BloodTransectionDetails.Add(objToSaveDetails);
                //update master blood bank table
                updateMasterBloodBankTable(model);
                ent.SaveChanges();


            }
        }

        private void updateMasterBloodBankTable(BloodTransectionModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                BloodStockManagementMaster e = (from e1 in ent.BloodStockManagementMasters
                                                where e1.BloodType == model.BloodTypeId
                                                select e1).First();

                e.QuantityUnit = e.QuantityUnit - model.ObjBTDetails.QuantityUnit;
                e.QuantityML = e.QuantityML - model.ObjBTDetails.QuantityML;
                ent.SaveChanges();

            }


            //throw new NotImplementedException();
        }




    }
}