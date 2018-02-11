using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace HospitalManagementSystem.Providers
{
    public class DepartmentSetupProvider
    {
        // GET: DepartmentSetupProvider
        public List<DepartmentSetupModel> GetList()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<DepartmentSetupModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDepartment>, IEnumerable<DepartmentSetupModel>>(ent.SetupDepartments.Where(x => x.IsAvailable == true)));
        }
        public List<DepartmentSetupModel> GetlistBySearchWord(string Swords)
        {
            EHMSEntities ent = new EHMSEntities();
            string searchWords = Swords.ToLower();
            return new List<DepartmentSetupModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDepartment>, IEnumerable<DepartmentSetupModel>>(ent.SetupDepartments)).Where(x => x.DeptName.ToLower().StartsWith(searchWords)).Take(20).ToList();
        }
        public int Insert(DepartmentSetupModel Model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupDepartments.Where(x => x.DeptName.ToLower() == Model.DeptName.ToLower());
                var DepartmetnCode = ent.SetupDepartments.Where(x => x.DeptCode == Model.DeptCode);

                if (item.Count() == 0 && DepartmetnCode.Count() == 0)
                {
                    var objToSave = AutoMapper.Mapper.Map<DepartmentSetupModel, SetupDepartment>(Model);
                    objToSave.IsAvailable = true;
                    objToSave.AssociateDepartmentID = 1000;//This is for opd
                    ent.SetupDepartments.Add(objToSave);
                    int i = ent.SaveChanges();
                    return i;
                }
                else
                {
                    return 0;
                }
            }

        }
        public int Update(DepartmentSetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var item = ent.SetupDepartments.Where(x => x.DeptName.ToLower() == model.DeptName.ToLower());
            if (item.Count() == 0)
            {
                var objtoedit = ent.SetupDepartments.Where(x => x.DeptID == model.DeptID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoedit);
                objtoedit.IsAvailable = true;
                ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
                int i = ent.SaveChanges();
                return i;

            }
            else
            {
                return 0;
            }

        }
        public int Delete(int preid)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupDepartments.Where(x => x.DeptID == preid).FirstOrDefault();
            objToDelete.IsAvailable = false;
            ent.Entry(objToDelete).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            if (i == 1)
            {
                return i;
            }
            else
                return 0;


        }
    }
}

