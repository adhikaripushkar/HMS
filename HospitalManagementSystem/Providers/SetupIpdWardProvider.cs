using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Provider
{
    public class SetUpIpdWardProvider
    {
        public List<SetUpIpdWardModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpIpdWardModel>(AutoMapper.Mapper.Map<IEnumerable<SetupIpdWard>, IEnumerable<SetUpIpdWardModel>>(ent.SetupIpdWards));


            }
        }
        public int Insert(SetUpIpdWardModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpIpdWardModel, SetupIpdWard>(model);

                ent.SetupIpdWards.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }
        public int Update(SetUpIpdWardModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupIpdWards.Where(x => x.IpdWardId == model.IpdWardId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }
        public int Delete(int IpdWardId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupIpdWards.Where(x => x.IpdWardId == IpdWardId).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupIpdWards.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }

        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupIpdWard, SetUpIpdWardModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetupIpdWard, SetUpIpdWardModel>();
                var Details = ent.SetupIpdWards.Where(x => x.IpdWardId == id).FirstOrDefault();
                SetUpIpdWardModel det = AutoMapper.Mapper.Map<SetupIpdWard, SetUpIpdWardModel>(Details);


            }


        }


    }
}