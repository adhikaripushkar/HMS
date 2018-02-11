using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Provider
{
    public class SetUpHospitalDetailProvider
    {


        public List<SetUpHospitalDetailModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpHospitalDetailModel>(AutoMapper.Mapper.Map<IEnumerable<SetupHospitalDetail>, IEnumerable<SetUpHospitalDetailModel>>(ent.SetupHospitalDetails));


            }
        }
        public int Insert(SetUpHospitalDetailModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpHospitalDetailModel, SetupHospitalDetail>(model);
                ent.SetupHospitalDetails.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }
        public int Update(SetUpHospitalDetailModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupHospitalDetails.Where(x => x.DetailsId == model.DetailsId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }


        public int UpdatewithoutImage(SetUpHospitalDetailModel model)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupHospitalDetails.Where(x => x.DetailsId == model.DetailsId).FirstOrDefault();
                model.FilePath = objToEdit.FilePath;
                model.ImageLogoName = objToEdit.ImageLogoName;
                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;

                i = ent.SaveChanges();
            }
            return i;

        }


        public int Delete(int RoomId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupHospitalDetails.Where(x => x.DetailsId == RoomId).FirstOrDefault();

                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupHospitalDetails.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupHospitalDetail, SetUpHospitalDetailModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetupHospitalDetails, SetUpHospitalDetailModel>();
                var Details = ent.SetupHospitalDetails.Where(x => x.DetailsId == id).FirstOrDefault();
                SetUpHospitalDetailModel det = AutoMapper.Mapper.Map<SetupHospitalDetail, SetUpHospitalDetailModel>(Details);


            }


        }



    }
}