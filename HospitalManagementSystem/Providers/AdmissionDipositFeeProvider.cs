using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class AdmissionDipositFeeProvider
    {
        public List<AdmissionDipositFeeModel> GetAll()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<AdmissionDipositFeeModel>(AutoMapper.Mapper.Map<IEnumerable<AdmissionDipositFee>, IEnumerable<AdmissionDipositFeeModel>>(ent.AdmissionDipositFees)).ToList();
        }
        public int Insert(AdmissionDipositFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ToSaveAdmissionDipositFee = AutoMapper.Mapper.Map<AdmissionDipositFeeModel, AdmissionDipositFee>(model);
                ToSaveAdmissionDipositFee.CreateDate = DateTime.Now;
                ToSaveAdmissionDipositFee.CreateBy = 1;
                ToSaveAdmissionDipositFee.YearID = 1;
                ToSaveAdmissionDipositFee.TaxAmoutn = ToSaveAdmissionDipositFee.DepositAmount * ToSaveAdmissionDipositFee.TaxPersentage / 100;
                ToSaveAdmissionDipositFee.VatAmoutn = ToSaveAdmissionDipositFee.DepositAmount * ToSaveAdmissionDipositFee.VatPersentage / 100;
                ent.AdmissionDipositFees.Add(ToSaveAdmissionDipositFee);
                ent.SaveChanges();
                return 1;
            }
        }

        public int Update(AdmissionDipositFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var Toupdatedata = ent.AdmissionDipositFees.Where(m => m.AdmissionDipositFeeID == model.AdmissionDipositFeeID).SingleOrDefault();
                model.TaxAmoutn = model.DepositAmount * model.TaxPersentage / 100;
                model.VatAmoutn = model.DepositAmount * model.VatPersentage / 100;
                model.CreateBy = 1;
                model.YearID = 1;
                model.CreateDate = Toupdatedata.CreateDate;
                AutoMapper.Mapper.Map(model, Toupdatedata);
                ent.Entry(Toupdatedata).State = EntityState.Modified;
                ent.SaveChanges();
                return 1;
            }


        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.AdmissionDipositFees.Where(x => x.AdmissionDipositFeeID == id).FirstOrDefault();
                ent.AdmissionDipositFees.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}