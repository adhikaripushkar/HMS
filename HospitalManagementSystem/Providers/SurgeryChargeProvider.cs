using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SurgeryChargeProvider
    {

        public List<SurgeryChargeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<SurgeryChargeModel>(AutoMapper.Mapper.Map<IEnumerable<SurgeryCharge>, IEnumerable<SurgeryChargeModel>>(ent.SurgeryCharges.Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public bool Insert(SurgeryChargeModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            if (ent.SurgeryCharges.Any(x => x.ChargeName == model.ChargeName))
            {
                return false;
            }
            var obj = AutoMapper.Mapper.Map<SurgeryChargeModel, SurgeryCharge>(model);
            obj.Status = true;
            decimal PayabelperTax = Convert.ToDecimal((model.PayableGrandTotal / 100) * Convert.ToDecimal(5));
            obj.PayableTaxTotal = PayabelperTax;
            ent.SurgeryCharges.Add(obj);
            ent.SaveChanges();
            return true;

        }

        public SurgeryChargeModel GetObject(int? id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SurgeryCharges.Where(x => x.SurgeryChargeId == id).SingleOrDefault();
                SurgeryChargeModel model = AutoMapper.Mapper.Map<SurgeryCharge, SurgeryChargeModel>(obj);
                return model;
            }
        }

        public void Update(SurgeryChargeModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SurgeryCharges.Where(x => x.SurgeryChargeId == model.SurgeryChargeId).SingleOrDefault();
            AutoMapper.Mapper.Map(model, obj);
            obj.Status = true;
            decimal PayabelPerTax = Convert.ToDecimal((model.PayableGrandTotal / 100) * Convert.ToDecimal(5));
            obj.PayableTaxTotal = PayabelPerTax;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}