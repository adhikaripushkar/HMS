using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem;
using HospitalManagementSystem.Models;

namespace HospitalManagmentSystem.Providers
{
    public class MemberTypeSetupProvider
    {
        public List<MemberTypeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<MemberTypeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberType>, IEnumerable<MemberTypeModel>>(ent.SetupMemberTypes.Where(x => x.Status.Equals("A"))));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(MemberTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupMemberTypes.Any(s => s.MemberTypeName == model.MemberTypeName))
                {
                    return 0;
                }
                var objToSave = AutoMapper.Mapper.Map<MemberTypeModel, SetupMemberType>(model);
                objToSave.Status = "A";
                //objToSave.ValidUpto = Convert.ToDateTime(model.date);
                objToSave.ValidUpto = DateTime.Now;
                ent.SetupMemberTypes.Add(objToSave);
                ent.SaveChanges();
                return 1;
            }

        }

        public MemberTypeModel GetObject(int memberTypeId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SetupMemberTypes.Where(x => x.MemberTypeID == memberTypeId).FirstOrDefault();
                MemberTypeModel model = AutoMapper.Mapper.Map<SetupMemberType, MemberTypeModel>(obj);
                return model;
            }
        }

        public void Update(MemberTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.SetupMemberTypes.Where(x => x.MemberTypeID == model.MemberTypeID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoedit);
                objtoedit.Status = "A";
                //objtoedit.ValidUpto = Convert.ToDateTime(model.date);
                objtoedit.ValidUpto = DateTime.Now;
                ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupMemberTypes.Where(x => x.MemberTypeID == id).FirstOrDefault();
            objToDelete.Status = "I";
            ent.Entry(objToDelete).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}