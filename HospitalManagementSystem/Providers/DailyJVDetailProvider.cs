using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class DailyJvDetailProvider
    {
        public void Insert(DailyJvDetailModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            DailyJvDetail obj = new DailyJvDetail();
            obj.JvNo = model.JvNo;
            obj.Narration = model.Narration;
            obj.Date = model.Date;
            obj.DrCr = "Dr";
            obj.AccountHead = "Bank or Cash";
            obj.DrAmount = model.DrAmount;
            obj.CrAmount = 0;
            obj.Status = true;
            obj.VerifiedBy = 1;
            obj.CreatedBy = 1;
            obj.CreatedDate = DateTime.Today;
            ent.DailyJvDetails.Add(obj);
            //ent.SaveChanges();
            foreach (var item in model.JvDetailList)
            {
                obj = new DailyJvDetail();
                obj.JvNo = model.JvNo;
                obj.Narration = model.Narration;
                obj.Date = model.Date;
                obj.DrCr = "Cr";
                obj.AccountHead = item.AccountHead;
                obj.CrAmount = item.CrAmount;
                obj.DrAmount = 0;
                obj.Status = true;
                obj.VerifiedBy = 1;
                obj.CreatedBy = 1;
                obj.CreatedDate = DateTime.Today;
                ent.DailyJvDetails.Add(obj);

            }


            var objvn = ent.SetupVoucherNumbers.FirstOrDefault();
            objvn.VoucherNo = objvn.VoucherNo + 1;
            ent.Entry(objvn).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}