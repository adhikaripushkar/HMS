using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmployeeShiftMasterProviders
    {
        public int Insert(SetupEmployeeShiftMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupEmployeeShiftMasterModel, SetupEmployeeShiftMaster>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Today;
                objToSave.Status = true;
                ent.SetupEmployeeShiftMasters.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<SetupEmployeeShiftMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeShiftMasterModel> model = new List<SetupEmployeeShiftMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<SetupEmployeeShiftMaster>, IEnumerable<SetupEmployeeShiftMasterModel>>(ent.SetupEmployeeShiftMasters).ToList();
                return model;
            }
        }
        public int Update(SetupEmployeeShiftMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupEmployeeShiftMasters.Where(x => x.EmployeeShiftMasterId == model.EmployeeShiftMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.CreatedBy = 1;
                objToEdit.Status = true;
                objToEdit.CreatedDate = DateTime.Today;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }
            return i;
        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupEmployeeShiftMasters.Where(x => x.EmployeeShiftMasterId == id).FirstOrDefault();
                ent.SetupEmployeeShiftMasters.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}