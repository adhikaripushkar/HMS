using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupOtherDiscountProvider
    {
        public List<SetupOtherDiscountModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetupOtherDiscountModel>(AutoMapper.Mapper.Map<IEnumerable<SetupOtherDiscount>, IEnumerable<SetupOtherDiscountModel>>(ent.SetupOtherDiscounts));
            }
        }

        public int Insert(SetupOtherDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupOtherDiscounts.Where(x => x.DiscountName == model.DiscountName);

                if (item.Count() == 0)
                {
                    var objToSave = AutoMapper.Mapper.Map<SetupOtherDiscountModel, SetupOtherDiscount>(model);
                    objToSave.CreatedBy = 0;
                    objToSave.CreatedDate = DateTime.Now;
                    ent.SetupOtherDiscounts.Add(objToSave);

                    int i = ent.SaveChanges();
                    return i;
                }
                else
                    return 0;
            }
        }

        public int Update(SetupOtherDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupOtherDiscounts.Where(x => x.DiscountName == model.DiscountName && x.DiscountID != model.DiscountID);
                if (item.Count() == 0)
                {
                    var objToUpdate = ent.SetupOtherDiscounts.Where(x => x.DiscountID == model.DiscountID).FirstOrDefault();
                    model.CreatedBy = objToUpdate.CreatedBy;
                    model.CreatedDate = objToUpdate.CreatedDate;

                    AutoMapper.Mapper.Map(model, objToUpdate);
                    ent.Entry(objToUpdate).State = System.Data.EntityState.Modified;
                    int i = ent.SaveChanges();
                    return i;
                }
                else
                    return 0;
            }
        }

        public int Delete(int deleteid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupOtherDiscounts.Where(x => x.DiscountID == deleteid).FirstOrDefault();
                ent.SetupOtherDiscounts.Remove(objToDelete);
                int i = ent.SaveChanges();
                return i;
            }
        }
    }
}