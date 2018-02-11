using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
namespace HospitalManagementSystem.Providers
{
    public class StockPurchaseOrderProvider
    {
        public List<StockPurchaseOrderModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<StockPurchaseOrderModel>(AutoMapper.Mapper.Map<IEnumerable<StockPurchaseOrder>, IEnumerable<StockPurchaseOrderModel>>(ent.StockPurchaseOrders.Where(x => x.Status == true)));


            }
        }

        public int Insert(StockPurchaseOrderModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = AutoMapper.Mapper.Map<StockPurchaseOrderModel, StockPurchaseOrder>(model);

            obj.Status = true;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;


            ent.StockPurchaseOrders.Add(obj);
            ent.SaveChanges();
            int id = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == ent.StockPurchaseOrders.Max(y => y.PurchaseOrderId)).SingleOrDefault().PurchaseOrderId;
            foreach (var item in model.StockPurchaseItemEntryList)
            {
                decimal VatAmountTotal = Convert.ToDecimal(0);
                if (item.VatAmount > 0)
                {
                    VatAmountTotal = ((item.QuatationPrice * item.Quantity) * item.VatAmount / 100);
                }

                StockPurchaseOrderDetail PurchaseDetail = new StockPurchaseOrderDetail();
                PurchaseDetail.PurchaseOrderId = id;
                PurchaseDetail.ItemId = item.StockItemEntryId;
                PurchaseDetail.Quantity = Convert.ToInt32(item.Quantity);
                PurchaseDetail.QuotationPrice = item.QuatationPrice;
                PurchaseDetail.TotalAmount = (item.Quantity * item.QuatationPrice) + VatAmountTotal;
                PurchaseDetail.SupplierId = model.StockPurchaseOrderDetailsModel.SupplierId;
                PurchaseDetail.ManufactorerId = model.StockPurchaseOrderDetailsModel.SupplierId;
                PurchaseDetail.VendorId = model.StockPurchaseOrderDetailsModel.SupplierId;
                PurchaseDetail.Status = true;
                PurchaseDetail.VatAmount = ((item.QuatationPrice * item.Quantity) * item.VatAmount / 100);
                ent.StockPurchaseOrderDetails.Add(PurchaseDetail);
            }
            ent.SaveChanges();
            return obj.PurchaseOrderId;
        }
        public void Udate(int id, StockPurchaseOrderModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockPurchaseOrders.Where(x => x.PurchaseOrderId == id).SingleOrDefault();
            model.PurchaseOrderId = obj.PurchaseOrderId;

            AutoMapper.Mapper.Map(model, obj);

            obj.Status = true;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Now;


            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            foreach (var item in ent.StockPurchaseOrderDetails.Where(x => x.PurchaseOrderId == model.PurchaseOrderId).ToList())
            {
                ent.StockPurchaseOrderDetails.Remove(item);

                ent.SaveChanges();
            }
            try
            {
                foreach (var item in model.StockPurchaseItemEntryList)
                {
                    StockPurchaseOrderDetail PurchaseDetail = new StockPurchaseOrderDetail();
                    PurchaseDetail.PurchaseOrderId = id;
                    PurchaseDetail.ItemId = item.StockItemEntryId;
                    PurchaseDetail.Quantity = Convert.ToInt32(item.Quantity);
                    PurchaseDetail.QuotationPrice = item.QuatationPrice;
                    PurchaseDetail.TotalAmount = item.Quantity * item.QuatationPrice;
                    PurchaseDetail.SupplierId = model.StockPurchaseOrderDetailsModel.SupplierId;
                    PurchaseDetail.ManufactorerId = model.StockPurchaseOrderDetailsModel.SupplierId;
                    PurchaseDetail.VendorId = model.StockPurchaseOrderDetailsModel.SupplierId;
                    PurchaseDetail.Status = true;
                    ent.StockPurchaseOrderDetails.Add(PurchaseDetail);
                }
            }
            catch
            {
            }
            ent.SaveChanges();
        }
        public List<StockPurchaseOrderDetailsModel> GetPurchaseDetail(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<StockPurchaseOrderDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<StockPurchaseOrderDetail>, IEnumerable<StockPurchaseOrderDetailsModel>>(ent.StockPurchaseOrderDetails.Where(x => x.PurchaseOrderId == id)));


            }
        }
    }
}