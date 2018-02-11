using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class DeductionProvider
    {
        public List<DeductionSetupModel> GetAll()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<DeductionSetupModel>(AutoMapper.Mapper.Map<IEnumerable<DeductionSetup>, IEnumerable<DeductionSetupModel>>(ent.DeductionSetups));
            }

        }

        public int Insert(DeductionSetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var Tosave = AutoMapper.Mapper.Map<DeductionSetupModel, DeductionSetup>(model);
            Tosave.CreatedBy = 1;
            Tosave.CreatedDate = DateTime.Now;
            Tosave.Status = true;
            Tosave.DeductionName = HospitalManagementSystem.Utility.GetAccountHeadName(model.AccountHeadID);
            Tosave.BranchId = 1;
            ent.DeductionSetups.Add(Tosave);
            ent.SaveChanges();


            return 1;
        }

        public int Update(DeductionSetupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.DeductionSetups.Where(x => x.ID == model.ID).FirstOrDefault();
                model.CreatedBy = objToEdit.CreatedBy;
                model.Status = true;
                model.DeductionName = HospitalManagementSystem.Utility.GetAccountHeadName(model.AccountHeadID);
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
                var objToDelete = ent.DeductionSetups.Where(x => x.ID == id).FirstOrDefault();
                ent.DeductionSetups.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }

    }

}
