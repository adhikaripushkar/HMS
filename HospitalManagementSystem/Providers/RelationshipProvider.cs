using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class RelationshipProvider
    {
        public List<RelationshipModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<RelationshipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupRelationship>, IEnumerable<RelationshipModel>>(ent.SetupRelationships.Where(x => x.Status.Equals("A"))));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(RelationshipModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupRelationships.Any(s => s.RelationName == model.RelationName))
                {
                    return 0;
                }
                var objToSave = AutoMapper.Mapper.Map<RelationshipModel, SetupRelationship>(model);

                objToSave.Status = "A";
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupRelationships.Add(objToSave);
                ent.SaveChanges();
                return 1;
            }

        }

        public RelationshipModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SetupRelationships.Where(x => x.RelationId == id).FirstOrDefault();
                RelationshipModel model = AutoMapper.Mapper.Map<SetupRelationship, RelationshipModel>(obj);
                return model;
            }
        }

        public void Update(RelationshipModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.SetupRelationships.Where(x => x.RelationId == model.RelationId).FirstOrDefault();
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
            var obj = ent.SetupRelationships.Where(x => x.RelationId == id).FirstOrDefault();
            obj.Status = "I";
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}