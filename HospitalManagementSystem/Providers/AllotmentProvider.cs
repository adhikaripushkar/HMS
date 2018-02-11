using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class AllotmentProvider
    {
        public string Insert(DemandMasterModel model)
        {
            int flag = 0;
            string s = "";
            EHMSEntities ent = new EHMSEntities();
            ItemAllotmentMaster master = new ItemAllotmentMaster();
            master.ItemDemandID = model.DemandDetailList[0].ItemDemandID;
            master.AllotmentDate = (DateTime)model.AllotmentDate;
            master.Orderer = model.Orderer;
            master.OrdererPost = model.OrdererPost;
            master.CreatedBy = 1;
            master.CreatedDate = DateTime.Now;
            master.Status = true;
            master.ItemAllotmentNo = model.ItemAllotmentNo;
            ent.ItemAllotmentMasters.Add(master);

            ent.SaveChanges();
            foreach (var item in model.DemandDetailList)
            {
                if (item.CancellationStatus == false)
                {
                    ItemAllotmentDetail detail = new ItemAllotmentDetail();
                    DepartmentWiseStock depStock = new DepartmentWiseStock();
                    int did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
                    var stockItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault();
                    detail.ItemAllotmentMasterID = master.ItemAllotmentMasterID;
                    detail.ItemID = item.ItemID;
                    detail.QuantityIssued = item.QuantityIssued;
                    detail.QuantityRemained = item.QuantityDemand - item.QuantityIssued;
                    detail.SourceFrom = item.SourceFrom;
                    detail.CancellationStatus = item.CancellationStatus;
                    detail.Remarks = item.Remarks;
                    detail.Status = true;


                    depStock.DepartmentID = ent.ItemDemandMasters.Where(x => x.ItemDemandID == master.ItemDemandID).SingleOrDefault().DepartmentID;
                    depStock.ItemId = item.ItemID;
                    depStock.Quantity = item.QuantityIssued;
                    depStock.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                    depStock.CreatedDate = DateTime.Today;
                    if ((item.QuantityIssued > item.QuantityDemand) || (item.SourceFrom == 1 && item.QuantityIssued > stockItem.Quantity))
                    {

                        s = "Quantity issued is greater than that availabe in stock OR Quantity Demanded!";
                    }
                    else
                    {
                        flag = 1;
                        var demandDetail = ent.ItemDemandDetails.Where(x => x.ItemDemandDetailId == item.ItemDemandDetailId).SingleOrDefault();
                        demandDetail.DispatchStatus = true;
                        stockItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault();
                        stockItem.Quantity = (item.SourceFrom == 1) ? (stockItem.Quantity - item.QuantityIssued) : stockItem.Quantity;
                        ent.ItemAllotmentDetails.Add(detail);
                        if (ent.DepartmentWiseStocks.Where(x => x.DepartmentID == did && x.ItemId == item.ItemID).Any())
                        {
                            var obj = ent.DepartmentWiseStocks.Where(x => x.DepartmentID == did && x.ItemId == item.ItemID).SingleOrDefault();
                            obj.Quantity = obj.Quantity + item.QuantityIssued;
                            ent.Entry(obj).State = System.Data.EntityState.Modified;
                        }
                        else
                        {
                            ent.DepartmentWiseStocks.Add(depStock);
                        }
                        ent.Entry(demandDetail).State = System.Data.EntityState.Modified;
                        ent.Entry(stockItem).State = System.Data.EntityState.Modified;
                        ent.SaveChanges();
                    }
                }
                else
                {
                    var demandDetail = ent.ItemDemandDetails.Where(x => x.ItemDemandDetailId == item.ItemDemandDetailId).SingleOrDefault();
                    demandDetail.DispatchStatus = item.isSelected == true ? false : true;

                    ent.Entry(demandDetail).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();
                }
            }
            if (flag != 1)
            {

                EHMSEntities ent1 = new EHMSEntities();
                var obj = ent1.ItemAllotmentMasters.OrderByDescending(x => x.ItemAllotmentMasterID).FirstOrDefault();
                ent1.ItemAllotmentMasters.Remove(obj);
                ent1.SaveChanges();
            }

            return s;
        }

        public string Update(List<AllotementDetailModel> allotmentList)
        {
            EHMSEntities ent = new EHMSEntities();
            string s = "";
            int flag = 0;
            foreach (var item in allotmentList)
            {
                var objAllotment = ent.ItemAllotmentDetails.Where(z => z.ItemAllotmentDetailID == ent.ItemAllotmentDetails.Where(x => x.ItemAllotmentMasterID == item.ItemAllotmentMasterID && x.ItemID == item.ItemID).Max(y => y.ItemAllotmentDetailID)).SingleOrDefault();
                var objStockItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemID).SingleOrDefault();

                if (item.CancellationStatus == false && item.QuantityIssued != 0)
                {
                    if (item.QuantityIssued > objAllotment.QuantityRemained)
                    {
                        s = "Quantity issued is greater than that demanded OR that availabe in stock!";
                        flag = 1;

                    }
                    if (item.QuantityIssued > objStockItem.Quantity)
                    {
                        s = "Quantity issued is greater than that demanded OR that availabe in stock!";
                        flag = 1;
                    }
                    if (flag == 0)
                    {
                        objAllotment.QuantityRemained = objAllotment.QuantityRemained - item.QuantityIssued;
                        if (item.SourceFrom == 1)
                        {
                            objStockItem.Quantity = objStockItem.Quantity - item.QuantityIssued;
                            ent.Entry(objStockItem).State = System.Data.EntityState.Modified;
                        }
                        objAllotment.QuantityIssued = item.QuantityIssued;

                        var depid = (from d in ent.ItemDemandMasters
                                     join a in ent.ItemAllotmentMasters
                                         on d.ItemDemandID equals a.ItemDemandID
                                     join ad in ent.ItemAllotmentDetails on a.ItemAllotmentMasterID equals ad.ItemAllotmentMasterID
                                     where ad.ItemAllotmentDetailID == objAllotment.ItemAllotmentDetailID
                                     select d.DepartmentID).SingleOrDefault();

                        var depStock = ent.DepartmentWiseStocks.Where(x => x.DepartmentID == depid && x.ItemId == item.ItemID).SingleOrDefault();

                        depStock.Quantity = depStock.Quantity + item.QuantityIssued;

                        ent.Entry(depStock).State = System.Data.EntityState.Modified;
                        //ent.Entry(objAllotment).State = System.Data.EntityState.Modified;
                        ent.ItemAllotmentDetails.Add(objAllotment);
                        ent.SaveChanges();
                    }
                }
                flag = 0;
            }

            return s;

        }

    }
}