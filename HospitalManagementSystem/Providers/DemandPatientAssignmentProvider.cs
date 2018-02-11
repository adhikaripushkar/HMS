using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DemandPatientAssignmentProvider
    {
        public List<DemandMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<DemandMasterModel>(AutoMapper.Mapper.Map<IEnumerable<ItemDemandMaster>, IEnumerable<DemandMasterModel>>(ent.ItemDemandMasters.Where(x => x.Status == true && x.DepartmentID == 1)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }


        public void Insert(DemandMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToSave = AutoMapper.Mapper.Map<DemandMasterModel, ItemDemandMaster>(model);
            objToSave.CreatedBy = 1;
            objToSave.CreatedDate = DateTime.Now;
            objToSave.Status = true;
            objToSave.DepartmentID = 1;
            ent.ItemDemandMasters.Add(objToSave);
            ent.SaveChanges();
            foreach (var item in model.StockItemEntryList)
            {
                ItemDemandDetail demandDetail = new ItemDemandDetail();
                demandDetail.ItemDemandID = ent.ItemDemandMasters.OrderByDescending(x => x.ItemDemandID).FirstOrDefault().ItemDemandID;
                demandDetail.ItemID = item.StockItemEntryId;
                demandDetail.QuantityDemand = item.Quantity;
                demandDetail.DispatchStatus = false;
                demandDetail.Remarks = "";
                ent.ItemDemandDetails.Add(demandDetail);
            }
            ent.SaveChanges();
        }

        public void AssignPatients(DemandPatientAssignmentModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            DemandPatientAssignment obj = new DemandPatientAssignment();
            try
            {
                foreach (var item in model.OpdModelList)
                {
                    if (ent.DemandPatientAssignments.Where(x => x.OpdId == item.OpdID && x.Status == true).Any() == false)
                    {
                        obj.DemandId = model.DemandId;
                        obj.OpdId = item.OpdID;
                        obj.Remarks = model.Remarks;
                        obj.Status = true;
                        ent.DemandPatientAssignments.Add(obj);
                        ent.SaveChanges();
                    }
                }
            }
            catch { }

        }


        public static int CheckIfAllotmentExistOrNot(int demandId)
        {
            EHMSEntities ent = new EHMSEntities();
            var totalCount = ent.ItemAllotmentMasters.Where(x => x.ItemDemandID == demandId).Count();

            return Convert.ToInt32(totalCount);
        }
    }
}