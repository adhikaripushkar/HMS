using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class MemberTypeDiscountProvider
    {
        public List<MemberTypeDiscountModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<MemberTypeDiscountModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDiscount>, IEnumerable<MemberTypeDiscountModel>>(ent.SetupMemberDiscounts.Where(x => x.Status.Equals("A"))));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public void Insert(MemberTypeDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<MemberTypeDiscountModel, SetupMemberDiscount>(model);

                objToSave.Status = "A";
                objToSave.CreatedDate = DateTime.Now;
                objToSave.CreatedBy = 1;
                ent.SetupMemberDiscounts.Add(objToSave);
                ent.SaveChanges();
            }

        }

        public MemberTypeDiscountModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SetupMemberDiscounts.Where(x => x.MemberDiscountID == id).FirstOrDefault();
                MemberTypeDiscountModel model = AutoMapper.Mapper.Map<SetupMemberDiscount, MemberTypeDiscountModel>(obj);
                return model;
            }
        }

        public void Update(MemberTypeDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.SetupMemberDiscounts.Where(x => x.MemberDiscountID == model.MemberDiscountID).FirstOrDefault();
                model.CreatedDate = objtoedit.CreatedDate;
                model.CreatedBy = objtoedit.CreatedBy;
                AutoMapper.Mapper.Map(model, objtoedit);
                objtoedit.Status = "A";
                ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupMemberDiscounts.Where(x => x.MemberDiscountID == id).FirstOrDefault();
            objToDelete.Status = "I";
            ent.Entry(objToDelete).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}