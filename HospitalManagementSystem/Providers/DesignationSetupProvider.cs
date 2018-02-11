using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;


namespace HospitalManagementSystem.Providers
{
    public class DesignationSetupProvider
    {
        public List<DesignationSetupModel> GetAll()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<DesignationSetupModel>(AutoMapper.Mapper.Map<IEnumerable<DesignationSetup>, IEnumerable<DesignationSetupModel>>(ent.DesignationSetups));
            }
        }

        public int Insert(DesignationSetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var Tosave = AutoMapper.Mapper.Map<DesignationSetupModel, DesignationSetup>(model);
            Tosave.CreatedBy = 1;
            Tosave.CreatedDate = DateTime.Now;
            Tosave.Status = true;
            Tosave.BranchId = 1;
            ent.DesignationSetups.Add(Tosave);
            ent.SaveChanges();


            return 1;
        }

        public int Update(DesignationSetupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.ID != 2)
                {
                    var objToEdit = ent.DesignationSetups.Where(x => x.ID == model.ID).FirstOrDefault();
                    model.CreatedBy = objToEdit.CreatedBy;
                    model.Status = true;
                    model.CreatedDate = objToEdit.CreatedDate;
                    AutoMapper.Mapper.Map(model, objToEdit);
                    ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();
                }
            }
            return 1;
        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (id != 2)
                {
                    var objToDelete = ent.DesignationSetups.Where(x => x.ID == id).FirstOrDefault();
                    ent.DesignationSetups.Remove(objToDelete);
                    i = ent.SaveChanges();
                }
            }
            return i;
        }

    }

}