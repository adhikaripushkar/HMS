using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockItemPurchaseProvider
    {
        public List<StockItemPurchaseModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockItemPurchaseModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemPurchase>, IEnumerable<StockItemPurchaseModel>>(ent.StockItemPurchases.OrderByDescending(x => x.ItemPurchaseId).Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public void Insert(StockItemPurchaseModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = AutoMapper.Mapper.Map<StockItemPurchaseModel, StockItemPurchase>(model);

            obj.Status = true;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;
            ent.StockItemPurchases.Add(obj);
            ent.SaveChanges();

            int id = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == ent.StockItemPurchases.Max(y => y.ItemPurchaseId)).SingleOrDefault().ItemPurchaseId;
            foreach (var item in model.StockItemEntryList)
            {
                StockItemPurchaseDetail PurchaseDetail = new StockItemPurchaseDetail();
                var StockItemMaster = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault();
                var purchaseorder = (from po in ent.StockPurchaseOrders
                                     join pd in ent.StockPurchaseOrderDetails
                                         on po.PurchaseOrderId equals pd.PurchaseOrderId
                                     where po.PurchaseOrderNo == model.ItemOrderId && pd.ItemId == item.StockItemEntryId
                                     select pd).SingleOrDefault();
                PurchaseDetail.ItemPurchaseId = id;
                PurchaseDetail.StockItemEntryId = item.StockItemEntryId;
                PurchaseDetail.StockUnitId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault().StockUnitId;
                PurchaseDetail.Quantity = item.Quantity;
                PurchaseDetail.Rate = item.Rate;
                PurchaseDetail.TotalAmount = item.Quantity * item.Rate;
                PurchaseDetail.BatchNo = item.BatchNo;
                PurchaseDetail.ExpiryDate = item.ExpiryDate;
                PurchaseDetail.SupplierId = model.StockSupplierId;
                PurchaseDetail.WarrentyDate = item.WarrentyDate;
                PurchaseDetail.ManufacturedDate = item.ManufacturedDate;


                if (item.QuotQty == item.Quantity)
                {
                    purchaseorder.Status = false;
                    ent.Entry(purchaseorder).State = System.Data.EntityState.Modified;
                }
                //else
                //{
                //    purchaseorder.Quantity = purchaseorder.Quantity - item.Quantity;
                //}
                StockItemMaster.Quantity = StockItemMaster.Quantity + item.Quantity;
                ent.Entry(StockItemMaster).State = System.Data.EntityState.Modified;
                ent.StockItemPurchaseDetails.Add(PurchaseDetail);
            }
            ent.SaveChanges();
            //Automatic post jv
            int VoucherNumberInt = HospitalManagementSystem.Utility.getMaxVoucherNumber("PV", 7);
            string VoucherNumber = "PV" + "-" + Utility.GetCurrentFiscalYearNameInBS() + "-" + VoucherNumberInt.ToString();

            var objJVMaster = new JVMaster()
            {

                AccountNumber = "1",
                BillNumber = "123",
                VerifiedBy = 45,
                TransactionDate = DateTime.Today,
                TotalAmount = model.TotalAmount,
                Status = false,
                Narration1 = "From Stock",
                Narration2 = "From Stock",
                JvType = "PV",
                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                CreatedDate = DateTime.Today,
                FiscalYearId = HospitalManagementSystem.Utility.GetCurrentFiscalYearID(),
                JvNumber = VoucherNumber,
                FormName = "Stock"



            };

            ent.JVMasters.Add(objJVMaster);
            ent.SaveChanges();

            int PaymentMode = model.PaymentType;//Cash or bank, credit (paryt name)
            //Dr Amount
            string AccountHeadName = "";
            string AccountHeadNameParty = "";
            int FeeTypeIdInt = Convert.ToInt32(0);
            int FeeTypeSubIDInt = Convert.ToInt32(0);



            if (PaymentMode == 372)//credit
            {
                AccountHeadNameParty = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(1831) + "-" + HospitalManagementSystem.Utility.GetFeeTypeNameFromId(1832);
                AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(1833);
                FeeTypeIdInt = 1831;
                FeeTypeSubIDInt = 1832;

            }
            else if (PaymentMode == 373)//cash
            {
                AccountHeadNameParty = "CASH";
                AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(1833);
                FeeTypeIdInt = 372;
                FeeTypeSubIDInt = 0;
            }
            else//bank
            {
                AccountHeadNameParty = "BANK";
                AccountHeadName = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(1833);
                FeeTypeSubIDInt = 0;
            }




            var ObjJvDetails = new JVDetail()
            {

                BillNumber = "",
                CrAmount = Convert.ToDecimal(0),
                DrAmount = model.TotalAmount,
                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                CreatedDate = DateTime.Today,
                DrOrCr = "Dr",
                FeeTypeId = 1833,
                FeeTypeName = AccountHeadName,
                FeeTypeSubId = 0,
                JVMasterId = objJVMaster.JvMasterId,
                Narration = "Post From stock",
                TransactionDate = DateTime.Today,

            };
            ent.JVDetails.Add(ObjJvDetails);

            //CR Amount

            var ObjJvDetailsCr = new JVDetail()
            {

                BillNumber = "",
                CrAmount = model.TotalAmount,
                DrAmount = Convert.ToDecimal(0),
                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                CreatedDate = DateTime.Today,
                DrOrCr = "Cr",
                FeeTypeId = FeeTypeIdInt,//Party name (suppliers)
                FeeTypeName = AccountHeadNameParty,
                FeeTypeSubId = FeeTypeSubIDInt,
                JVMasterId = objJVMaster.JvMasterId,
                Narration = "Post From stock",
                TransactionDate = DateTime.Today,

            };
            ent.JVDetails.Add(ObjJvDetailsCr);


            SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumbers
                                                where x.JvType == "PV" && x.FiscalYear == 7
                                                select x).First();
            vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;
            ent.SaveChanges();


        }


        public void Udate(int id, StockItemPurchaseModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == id).SingleOrDefault();
            model.ItemPurchaseId = obj.ItemPurchaseId;
            AutoMapper.Mapper.Map(model, obj);

            obj.Status = true;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;


            ent.Entry(obj).State = System.Data.EntityState.Modified;

            foreach (var item in ent.StockItemPurchaseDetails.Where(x => x.ItemPurchaseId == model.ItemPurchaseId).ToList())
            {
                ent.StockItemPurchaseDetails.Remove(item);
                var purchaseorder = (from po in ent.StockPurchaseOrders
                                     join pd in ent.StockPurchaseOrderDetails
                                         on po.PurchaseOrderId equals pd.PurchaseOrderId
                                     where po.PurchaseOrderNo == model.ItemOrderId && pd.ItemId == item.StockItemEntryId
                                     select pd).SingleOrDefault();
                var objItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault();
                objItem.Quantity = objItem.Quantity - item.Quantity;

                purchaseorder.Quantity = purchaseorder.Quantity + item.Quantity;
                ent.Entry(objItem).State = System.Data.EntityState.Modified;
                ent.Entry(purchaseorder).State = System.Data.EntityState.Modified;
            }
            try
            {
                foreach (var item in model.StockItemEntryList)
                {
                    StockItemPurchaseDetail PurchaseDetail = new StockItemPurchaseDetail();
                    var purchaseorder = (from po in ent.StockPurchaseOrders
                                         join pd in ent.StockPurchaseOrderDetails
                                             on po.PurchaseOrderId equals pd.PurchaseOrderId
                                         where po.PurchaseOrderNo == model.ItemOrderId && pd.ItemId == item.StockItemEntryId
                                         select pd).SingleOrDefault();
                    var StockItemMaster = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault();
                    PurchaseDetail.ItemPurchaseId = id;
                    PurchaseDetail.StockItemEntryId = item.StockItemEntryId;
                    PurchaseDetail.StockUnitId = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == item.StockItemEntryId).SingleOrDefault().StockUnitId;
                    PurchaseDetail.Quantity = item.Quantity;
                    PurchaseDetail.Rate = item.Rate;
                    PurchaseDetail.TotalAmount = item.Quantity * item.Rate;
                    if (item.QuotQty == item.Quantity)
                    {
                        purchaseorder.Status = false;
                        ent.Entry(purchaseorder).State = System.Data.EntityState.Modified;
                    }
                    else
                    {
                        purchaseorder.Quantity = purchaseorder.Quantity - item.Quantity;
                    }
                    StockItemMaster.Quantity = StockItemMaster.Quantity + item.Quantity;
                    ent.Entry(StockItemMaster).State = System.Data.EntityState.Modified;
                    ent.StockItemPurchaseDetails.Add(PurchaseDetail);


                }
            }
            catch
            {



            }
            ent.SaveChanges();
        }

        public List<StockItemPurchaseDetailModel> GetPurchaseDetail(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockItemPurchaseDetailModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemPurchaseDetail>, IEnumerable<StockItemPurchaseDetailModel>>(ent.StockItemPurchaseDetails.Where(x => x.ItemPurchaseId == id)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public StockItemPurchaseModel GetPurchaseVoucherDetails(int PurchaseNo)
        {

            StockItemPurchaseModel model = new StockItemPurchaseModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemPurchaseDetails.Where(x => x.ItemPurchaseId == PurchaseNo).ToList();
                foreach (var item in result)
                {
                    var ViewModel = new StockItemEntry()
                    {
                        StockCategoryId = HospitalManagementSystem.Utility.GetCategoryIdFromItemId(item.StockItemEntryId),
                        Quantity = item.Quantity,
                        Rate = item.Rate,

                        StockItemEntryId = item.StockItemEntryId
                    };
                    model.StockItemEntryList.Add(ViewModel);

                }
                var resultModel = ent.StockItemPurchases.Where(x => x.ItemPurchaseId == PurchaseNo);
                model.TotalAmount = resultModel.FirstOrDefault().TotalAmount;
                model.StockSupplierId = resultModel.FirstOrDefault().StockSupplierId;
                model.CreatedDate = resultModel.FirstOrDefault().CreatedDate;
                model.ItemPurchaseId = PurchaseNo;
                model.Amount = resultModel.FirstOrDefault().Amount;
                model.Discount = resultModel.FirstOrDefault().Discount;
                model.VatAmount = resultModel.FirstOrDefault().VatAmount;
            }

            return model;
        }

        public List<ItemPurchaseReportModel> GetItemPurchaseReport()
        {
            EHMSEntities ent = new EHMSEntities();
            List<ItemPurchaseReportModel> IPRmodel = new List<ItemPurchaseReportModel>();
            var dataD = (from x in ent.StockItemPurchases
                         join y in ent.StockItemPurchaseDetails on x.ItemPurchaseId equals y.ItemPurchaseId

                         select new ItemPurchaseReportModel
                         {
                             StockItemPurchaseDetailId = y.StockItemPurchaseDetailId,
                             ItemBillNo = x.ItemBillNo,
                             StockSupplierId = x.StockSupplierId,
                             StockItemEntryId = y.StockItemEntryId,
                             StockUnitId = y.StockUnitId,
                             Quantity = y.Quantity,
                             Rate = y.Rate,
                             TotalAmount = y.TotalAmount,
                             Discount = x.Discount,
                             VatAmount = x.VatAmount,
                             ManufacturedDate = y.ManufacturedDate,
                             ExpiryDate = y.ExpiryDate,
                             WarrentyDate = y.WarrentyDate,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy
                         }).ToList();
            foreach (var itemss in dataD)
            {
                IPRmodel.Add(itemss);
            }
            return IPRmodel;
        }


    }
}