using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class GlAccountSubGroupProvider
    {

        public List<GlAccountSubGroupModel> GetAccountGroup()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GlAccountSubGroupModel>("GetGlAccountGroup").ToList();
            }
        }

        public List<GlAccountSubGroupModel> GetAllAccountGroup()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<GlAccountSubGroupModel>(AutoMapper.Mapper.Map<IEnumerable<GL_AccSubGroups>, IEnumerable<GlAccountSubGroupModel>>(ent.GL_AccSubGroups)).ToList();
            }
        }

        public void Delete(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data1 = ent.GL_AccSubGroups.Where(x => x.ParentID == id).FirstOrDefault();
                if (data1 == null)
                {
                    var data = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == id).FirstOrDefault();
                    ent.GL_AccSubGroups.Remove(data);
                    ent.SaveChanges();
                }
            }
        }

        public void Insert(GlAccountSubGroupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int Level = 0;
                string Hierachy = "";
                var data = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == model.ParentID).FirstOrDefault();
                if (model.ParentID == 0)
                {
                    Level = 2;
                    Hierachy = model.AccGroupID.ToString();
                }
                else
                {
                    Hierachy = data.HierarchyCode + "." + model.ParentID.ToString();
                    Level = data.HeadLevel + 1;
                }
                var objGlAccount = new GL_AccSubGroups()
                {
                    AccGroupID = model.AccGroupID,
                    ParentID = model.ParentID,
                    Remarks = model.Remarks,
                    IsLeafLevel = model.IsLeafLevel,
                    CreatedDate = DateTime.Now,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    HeadLevel = Level,
                    HierarchyCode = Hierachy,
                    Status = true,
                    AccSubGroupName = model.AccSubGroupName
                };
                ent.GL_AccSubGroups.Add(objGlAccount);
                ent.SaveChanges();
                int id = objGlAccount.AccSubGruupID;
                int parentnewid = Utility.GetAccHeadIDFromDescription("Staff Advance");
                if (model.ParentID == parentnewid)
                {
                    SetupEmployeeMaster SEMP = (from x in ent.SetupEmployeeMasters
                                                where x.EmployeeMasterId == model.EmployeeMasterId && x.Status == true
                                                select x).First();
                    SEMP.AccountHeadID = objGlAccount.AccSubGruupID;
                    ent.SaveChanges();
                }

                int SalaryParentid = Utility.GetAccHeadIDFromDescription("Salary Payable");
                if (model.ParentID == SalaryParentid)
                {
                    SetupEmployeeMaster SEMP = (from x in ent.SetupEmployeeMasters
                                                where x.EmployeeMasterId == model.EmployeeMasterId && x.Status == true
                                                select x).First();
                    SEMP.SPAccountHeadID = objGlAccount.AccSubGruupID;
                    ent.SaveChanges();
                }
            }
        }

        public void Edit(int id, GlAccountSubGroupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int Level = 0;
                string Hierachy = "";
                var data = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == model.ParentID).FirstOrDefault();
                if (model.ParentID == 0)
                {
                    Level = 2;
                    Hierachy = model.AccGroupID.ToString();
                }
                else
                {
                    Hierachy = data.HierarchyCode + "." + model.ParentID.ToString();
                    Level = data.HeadLevel + 1;
                }
                var data1 = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == id).FirstOrDefault();
                data1.AccGroupID = model.AccGroupID;
                data1.ParentID = model.ParentID;
                data1.Remarks = model.Remarks;
                data1.IsLeafLevel = model.IsLeafLevel;
                data1.HeadLevel = Level;
                data1.HierarchyCode = Hierachy;
                data1.AccSubGroupName = model.AccSubGroupName;
                ent.SaveChanges();
            }
        }

    }
}