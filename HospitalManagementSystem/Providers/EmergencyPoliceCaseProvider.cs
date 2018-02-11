using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class EmergencyPoliceCaseProvider
    {
        public int Insert(EmergencyPoliceCaseModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<EmergencyPoliceCaseModel, EmergencyPoliceCase>(model);
                objToSave.CreatedDate = DateTime.Now;
                objToSave.CreatedBy = 1;
                objToSave.Status = "Y";
                ent.EmergencyPoliceCases.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<EmergencyPoliceCaseModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<EmergencyPoliceCaseModel> model = new List<EmergencyPoliceCaseModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyPoliceCase>, IEnumerable<EmergencyPoliceCaseModel>>(ent.EmergencyPoliceCases).ToList();
                return model;
            }
        }
        public int Update(EmergencyPoliceCaseModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.EmergencyPoliceCases.Where(x => x.PCId == model.PCId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.CreatedDate = DateTime.Now;
                objToEdit.CreatedBy = 1;
                objToEdit.Status = "Y";

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }
            return i;
        }

    }
}