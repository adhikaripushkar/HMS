using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DistributionProvider
    {
        public int Insert(StockDistributionMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
                var obj = AutoMapper.Mapper.Map<StockDistributionMasterModel, StockDistributionMaster>(model);
                obj.BillNo = "BN" + obj.BillNo;
                obj.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                obj.CreatedDate = DateTime.Today;
                obj.Status = true;
                GL_Transaction tra = new GL_Transaction();
                if (model.Type == "OUT")
                {
                    GL_LedgerMaster ldg = new GL_LedgerMaster();
                    ldg.DepartmentID = 0;
                    ldg.AccountGroupID = 1;
                    ldg.AccountSubGroupID = 1;
                    ldg.AccountTypeID = 1;
                    ldg.SourceID = 0;
                    ldg.LedgerName = "A/C " + model.PatientName;
                    ldg.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                    ldg.CreatedDate = DateTime.Today;
                    ldg.Status = 1;

                    ent.GL_LedgerMaster.Add(ldg);
                }
                ent.StockDistributionMasters.Add(obj);
                ent.SaveChanges();

                tra.DepartmentID = model.DepartmentId;
                //tra.VoucherNo = Hospital.Utility.getMaxVoucherNumber(1, 1);
                tra.TransactionDate = DateTime.Today;
                if (model.Type == "OUT") tra.LedgerMasterID = ent.GL_LedgerMaster.Max(x => x.LedgerMasterID);
                if (model.Type == "IN") tra.LedgerMasterID = ent.GL_LedgerMaster.Where(x => x.SourceID == model.PatientId).SingleOrDefault().LedgerMasterID;
                tra.Narration1 = "MEDICINE-FEE";
                tra.Dr_Cr = "CR";
                tra.Amount = model.TotalAmount;
                tra.TransactionTypeID = 1;
                tra.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                tra.CreatedDate = DateTime.Today;
                tra.RefNo = tra.LedgerMasterID;
                tra.PatientLogId = tra.LedgerMasterID;
                ent.GL_Transaction.Add(tra);
                ent.SaveChanges();
                foreach (var item in model.StockItemEntryList)
                {
                    StockDistributionDetail objDetail = new StockDistributionDetail();
                    var objDepStock = ent.DepartmentWiseStocks.Where(x => x.DepartmentID == did && x.ItemId == item.StockItemEntryId).SingleOrDefault();
                    objDetail.DistributionMasterId = ent.StockDistributionMasters.Max(x => x.DistributionMasterId);
                    objDetail.ItemEntryId = item.StockItemEntryId;
                    objDetail.Quantity = item.Quantity;
                    objDetail.SalesRate = item.Rate;
                    objDetail.TotalAmount = item.Rate * item.Quantity;
                    objDetail.Status = true;
                    objDetail.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                    objDetail.CreatedDate = DateTime.Today;
                    objDepStock.Quantity = objDepStock.Quantity - item.Quantity;
                    ent.Entry(objDepStock).State = System.Data.EntityState.Modified;
                    ent.StockDistributionDetails.Add(objDetail);


                }
                ent.SaveChanges();

                return obj.DistributionMasterId;
            }
        }
    }
}