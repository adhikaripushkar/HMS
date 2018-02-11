using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupUnitProviders
    {
        public List<SetupUnitModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetupUnitModel>(AutoMapper.Mapper.Map<IEnumerable<SetupUnit>, IEnumerable<SetupUnitModel>>(ent.SetupUnits));


            }
        }
        public int Insert(SetupUnitModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupUnitModel, SetupUnit>(model);
                objToSave.Status = 1;
                ent.SetupUnits.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }

        public int Update(SetupUnitModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupUnits.Where(x => x.UnitId == model.UnitId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }

        public int Delete(int unitId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupUnits.Where(x => x.UnitId == unitId).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupUnits.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


    }
}