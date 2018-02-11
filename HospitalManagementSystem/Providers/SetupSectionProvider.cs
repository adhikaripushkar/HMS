using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Provider
{
    public class SetUpSectionProvider
    {

        public List<SetUpSectionModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpSectionModel>(AutoMapper.Mapper.Map<IEnumerable<SetupSection>, IEnumerable<SetUpSectionModel>>(ent.SetupSections));


            }
        }
        public int Insert(SetUpSectionModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpSectionModel, SetupSection>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.Status = 1;
                ent.SetupSections.Add(objToSave);

                ent.SaveChanges();
            }
            return 1;

        }

        public int Update(SetUpSectionModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupSections.Where(x => x.SectionId == model.SectionId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }

        public int Delete(int sectionId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupSections.Where(x => x.SectionId == sectionId).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupSections.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }

        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupSection, SetUpSectionModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetupSections, SetUpSectionModel>();
                var Details = ent.SetupSections.Where(x => x.SectionId == id).FirstOrDefault();
                SetUpSectionModel det = AutoMapper.Mapper.Map<SetupSection, SetUpSectionModel>(Details);


            }


        }


    }
}