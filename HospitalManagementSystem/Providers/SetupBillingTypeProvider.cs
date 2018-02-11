using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupBillingTypeProvider
    {
        public List<SetupBilllingTypeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetupBilllingTypeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupBillingType>, IEnumerable<SetupBilllingTypeModel>>(ent.SetupBillingTypes));
            }
        }

        public int Insert(SetupBilllingTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupBillingTypes.Where(x => x.SetupBillingTypeName == model.SetupBillingTypeName);

                if (item.Count() == 0)
                {
                    var objToSave = AutoMapper.Mapper.Map<SetupBilllingTypeModel, SetupBillingType>(model);
                    objToSave.CreatedBy = 0;
                    objToSave.CreatedDate = DateTime.Now;
                    ent.SetupBillingTypes.Add(objToSave);

                    int i = ent.SaveChanges();
                    return i;
                }
                else
                    return 0;
            }
        }

        public int Update(SetupBilllingTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupBillingTypes.Where(x => x.SetupBillingTypeName == model.SetupBillingTypeName && x.SetupBillingTypeID != model.SetupBillingTypeID);
                if (item.Count() == 0)
                {
                    var objToUpdate = ent.SetupBillingTypes.Where(x => x.SetupBillingTypeID == model.SetupBillingTypeID).FirstOrDefault();
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
                var objToDelete = ent.SetupBillingTypes.Where(x => x.SetupBillingTypeID == deleteid).FirstOrDefault();
                ent.SetupBillingTypes.Remove(objToDelete);
                int i = ent.SaveChanges();
                return i;
            }
        }
    }
}