using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupBankProviders
    {
        public List<SetupBankModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetupBankModel>(AutoMapper.Mapper.Map<IEnumerable<SetupBank>, IEnumerable<SetupBankModel>>(ent.SetupBanks));


            }
        }
        public int Insert(SetupBankModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupBankModel, SetupBank>(model);

                ent.SetupBanks.Add(objToSave);

                ent.SaveChanges();
            }
            return 1;

        }
        public int Update(SetupBankModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupBanks.Where(x => x.BankSetupId == model.BankSetupId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }

        public int Delete(int BankSetupId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupBanks.Where(x => x.BankSetupId == BankSetupId).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupBanks.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }
    }
}