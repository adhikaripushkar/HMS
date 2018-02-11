using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Provider
{
    public class SetUpIpdHospitalFeeProvider
    {
        public List<SetUpHospitalFeeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpHospitalFeeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupHospitalFee>, IEnumerable<SetUpHospitalFeeModel>>(ent.SetupHospitalFees));

            }
        }

        public int Insert(SetUpHospitalFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpHospitalFeeModel, SetupHospitalFee>(model);
                objToSave.Status = false;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupHospitalFees.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;
        }

        public int Update(SetUpHospitalFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupHospitalFees.Where(x => x.FeeId == model.FeeId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.Status = false;
                objToEdit.CreatedDate = DateTime.Now;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }


        public int Delete(int IpdRoomId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupHospitalFees.Where(x => x.FeeId == IpdRoomId).FirstOrDefault();

                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupHospitalFees.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupHospitalFee, SetUpHospitalFeeModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetupHospitalFee, SetUpHospitalFeeModel>();
                var Details = ent.SetupHospitalFees.Where(x => x.FeeId == id).FirstOrDefault();
                SetUpHospitalFeeModel det = AutoMapper.Mapper.Map<SetupHospitalFee, SetUpHospitalFeeModel>(Details);


            }


        }
        public void ChangToCurrent(int id, int type)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupHospitalFees.Where(x => x.FeeType == type && x.Status.Equals(true)).FirstOrDefault();
            if (objt != null)
            {

                objt.Status = false;
                ent.Entry(objt).State = System.Data.EntityState.Modified;
                ent.SaveChanges();

            }

            objt = ent.SetupHospitalFees.Where(x => x.FeeId == id).FirstOrDefault();
            objt.Status = true;
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }






    }
}