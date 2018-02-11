using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DemandMasterProvider
    {
        public List<DemandMasterModel> GetList()
        {
            EHMSEntities ent = new EHMSEntities();

            return ent.Database.SqlQuery<DemandMasterModel>("SPDemandMaster 1,0").ToList();
        }

        public void Insert(DemandMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToSave = AutoMapper.Mapper.Map<DemandMasterModel, ItemDemandMaster>(model);
            objToSave.CreatedBy = 1;
            objToSave.CreatedDate = DateTime.Now;
            objToSave.Status = true;
            objToSave.DepartmentID = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
            ent.ItemDemandMasters.Add(objToSave);
            ent.SaveChanges();
            foreach (var item in model.StockItemEntryList)
            {
                ItemDemandDetail demandDetail = new ItemDemandDetail();
                demandDetail.ItemDemandID = objToSave.ItemDemandID;
                demandDetail.ItemID = item.StockItemEntryId;
                demandDetail.QuantityDemand = item.Quantity;
                demandDetail.DispatchStatus = false;
                demandDetail.Remarks = "";
                ent.ItemDemandDetails.Add(demandDetail);
            }
            ent.SaveChanges();
        }

        public List<DemandDetailModel> GetDemandDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            return ent.Database.SqlQuery<DemandDetailModel>("SPDemandMaster 2," + id).ToList();
        }

        public List<DemandDetailModel> GetCDemandDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            return ent.Database.SqlQuery<DemandDetailModel>("SPDemandMaster 4," + id).ToList();
        }

        public void Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();

            ent.Database.ExecuteSqlCommand("exec SPDemandMaster 3," + id);

        }

        public List<DemandMasterModel> GetCustomDemandList(int depid, string datef, string datet)
        {
            EHMSEntities ent = new EHMSEntities();
            List<DemandMasterModel> llist = new List<DemandMasterModel>();
            if (datef == "" || datet == "")
            {
                return ent.Database.SqlQuery<DemandMasterModel>("SPCustomDemandMaster 1," + depid + ",null,null").ToList();
            }
            else if (depid == 0)
            {
                return ent.Database.SqlQuery<DemandMasterModel>("SPCustomDemandMaster 2,null," + "'" + datef + "','" + datet + "'").ToList();
            }
            else
            {
                return ent.Database.SqlQuery<DemandMasterModel>("SPCustomDemandMaster 3," + depid + ",'" + datef + "','" + datet + "'").ToList();
            }

        }

        public List<AllotmentMasterModel> GetAllotments()
        {
            EHMSEntities ent = new EHMSEntities();
            return ent.Database.SqlQuery<AllotmentMasterModel>("SPDemandMaster 5,0").ToList();
        }
        public List<AllotementDetailModel> GetAllotmentsDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            return ent.Database.SqlQuery<AllotementDetailModel>("SPDemandMaster 6," + id).ToList();
        }

        public DemandMasterModel GetDemandForEdit(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            DemandMasterModel model = new DemandMasterModel();
            var obj = ent.ItemDemandMasters.Where(x => x.ItemDemandID == id).SingleOrDefault();
            AutoMapper.Mapper.Map(obj, model);
            var demandDetails = ent.ItemDemandDetails.Where(x => x.ItemDemandID == id).ToList();
            foreach (var item in demandDetails)
            {
                if (item.DispatchStatus == false)
                {
                    StockItemEntry itemEntry = new StockItemEntry();
                    itemEntry.StockCategoryId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault().StockCategoryId;
                    itemEntry.StockSubCategoryId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault().StockSubCategoryId;
                    itemEntry.StockItemEntryId = item.ItemID;
                    itemEntry.Quantity = item.QuantityDemand;
                    model.StockItemEntryList.Add(itemEntry);
                }
            }
            return model;

        }

        public void Update(DemandMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.ItemDemandMasters.Where(x => x.ItemDemandID == model.ItemDemandID).SingleOrDefault();
            model.CreatedBy = obj.CreatedBy;
            model.CreatedDate = obj.CreatedDate;
            model.Status = obj.Status;
            AutoMapper.Mapper.Map(model, obj);
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            foreach (var item in ent.ItemDemandDetails.Where(x => x.ItemDemandID == obj.ItemDemandID && x.DispatchStatus == false).ToList())
            {
                ent.ItemDemandDetails.Remove(item);

            }
            foreach (var item in model.StockItemEntryList)
            {
                ItemDemandDetail demandDetail = new ItemDemandDetail();
                demandDetail.ItemDemandID = obj.ItemDemandID;
                demandDetail.ItemID = item.StockItemEntryId;
                demandDetail.QuantityDemand = item.Quantity;
                demandDetail.DispatchStatus = false;
                demandDetail.Remarks = "";
                ent.ItemDemandDetails.Add(demandDetail);
            }
            ent.SaveChanges();
        }

    }

}