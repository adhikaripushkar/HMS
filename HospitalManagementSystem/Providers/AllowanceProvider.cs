using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class AllowanceProvider
    {


        public List<AllowanceSetupModel> GetAll()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<AllowanceSetupModel>(AutoMapper.Mapper.Map<IEnumerable<AllowanceSetup>, IEnumerable<AllowanceSetupModel>>(ent.AllowanceSetups));
            }
        }

        public int Insert(AllowanceSetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var Tosave = AutoMapper.Mapper.Map<AllowanceSetupModel, AllowanceSetup>(model);
            Tosave.CreatedBy = 1;
            Tosave.CreatedDate = DateTime.Now;
            Tosave.Status = true;
            Tosave.BranchId = 1;
            Tosave.AlowanceName = HospitalManagementSystem.Utility.GetAccountHeadName(model.AccountHeadID);
            ent.AllowanceSetups.Add(Tosave);
            ent.SaveChanges();


            return 1;
        }

        public int Update(AllowanceSetupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.AllowanceSetups.Where(x => x.ID == model.ID).FirstOrDefault();
                model.CreatedBy = objToEdit.CreatedBy;
                model.Status = true;
                model.AlowanceName = HospitalManagementSystem.Utility.GetAccountHeadName(model.AccountHeadID);
                model.CreatedDate = objToEdit.CreatedDate;
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.AllowanceSetups.Where(x => x.ID == id).FirstOrDefault();
                ent.AllowanceSetups.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }

    }

}
