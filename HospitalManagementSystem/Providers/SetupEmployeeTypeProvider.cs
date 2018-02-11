using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmployeeTypeProvider
    {
        public List<SetupEmployeeTypeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetupEmployeeTypeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupEmployeeType>, IEnumerable<SetupEmployeeTypeModel>>(ent.SetupEmployeeTypes));
            }
        }

        public int Insert(SetupEmployeeTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupEmployeeTypes.Where(x => x.SetupEmployeeTypeName == model.SetupEmployeeTypeName);

                if (item.Count() == 0)
                {
                    var objToSave = AutoMapper.Mapper.Map<SetupEmployeeTypeModel, SetupEmployeeType>(model);
                    objToSave.CreatedBy = 0;
                    objToSave.CreatedDate = DateTime.Now;

                    ent.SetupEmployeeTypes.Add(objToSave);
                    int i = ent.SaveChanges();

                    return i;
                }
                else
                    return 0;
            }
        }

        public int Update(SetupEmployeeTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupEmployeeTypes.Where(x => x.SetupEmployeeTypeName == model.SetupEmployeeTypeName && x.SetupEmployeeTypeID != model.SetupEmployeeTypeID);

                if (item.Count() == 0)
                {
                    var objToUpdate = ent.SetupEmployeeTypes.Where(x => x.SetupEmployeeTypeID == model.SetupEmployeeTypeID).FirstOrDefault();
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
                var objToDelete = ent.SetupEmployeeTypes.Where(x => x.SetupEmployeeTypeID == deleteid).FirstOrDefault();

                ent.SetupEmployeeTypes.Remove(objToDelete);

                int i = ent.SaveChanges();

                return i;
            }
        }
    }
}