using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class OperationRecordProvider
    {


        public static void Insert(OperationRecordModel model)
        {
            EHMSEntities ent = new EHMSEntities();

            try
            {
                var objToEdit = ent.OperationRecords.Where(x => x.OperationTheatreMasterId == model.OperationTheatreMasterId).SingleOrDefault();
                model.OperationRecordId = objToEdit.OperationRecordId;
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            catch (Exception e)
            {
                var objToSave = AutoMapper.Mapper.Map<OperationRecordModel, OperationRecord>(model);
                ent.OperationRecords.Add(objToSave);
                ent.SaveChanges();
            }
        }

        public static OperationRecordModel GetOperationRecords(int opdid, int otmid)
        {
            EHMSEntities ent = new EHMSEntities();
            OperationRecordModel model = new OperationRecordModel();
            model.OpdMaster = ent.OpdMasters.Where(x => x.OpdID == opdid).SingleOrDefault();
            model.OperationTheatreDetailsList = ent.OperationTheatreDetails.Where(x => x.OperationTheatreMasterID == otmid).ToList();
            var OpRec = ent.OperationRecords.Where(x => x.OperationTheatreMasterId == otmid).SingleOrDefault();
            AutoMapper.Mapper.Map(OpRec, model);

            model.TotalCharge = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == otmid).SingleOrDefault().TotalCharge;
            model.OperationDate = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == otmid).SingleOrDefault().OperationDate;
            model.OperationStartTime = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == otmid).SingleOrDefault().OperationStartTime;
            model.OperationEndTime = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == otmid).SingleOrDefault().OperationEndTime;
            return model;
        }

    }
}