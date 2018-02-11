using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HospitalManagementSystem.Providers
{
    public class DesignationWiseSalarySetupProvider
    {
        public List<DesignationWiseSalarySetupModel> GetAll()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<DesignationWiseSalarySetupModel>(AutoMapper.Mapper.Map<IEnumerable<DesignationWiseSalarySetup>, IEnumerable<DesignationWiseSalarySetupModel>>(ent.DesignationWiseSalarySetups));
            }

        }

        public int Insert(DesignationWiseSalarySetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var Tosave = AutoMapper.Mapper.Map<DesignationWiseSalarySetupModel, DesignationWiseSalarySetup>(model);
            Tosave.CreatedBy = 1;
            Tosave.CreatedDate = DateTime.Now;
            Tosave.Status = true;
            Tosave.BranchId = 1;
            ent.DesignationWiseSalarySetups.Add(Tosave);
            ent.SaveChanges();
            return 1;
        }

        public int Update(DesignationWiseSalarySetupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.DesignationWiseSalarySetups.Where(x => x.ID == model.ID).FirstOrDefault();
                model.CreatedBy = objToEdit.CreatedBy;
                model.CreatedDate = objToEdit.CreatedDate;
                model.Status = true;
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
                var objToDelete = ent.DesignationWiseSalarySetups.Where(x => x.ID == id).FirstOrDefault();
                ent.DesignationWiseSalarySetups.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }

    }

}
