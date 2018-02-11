using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Provider
{
    public class SetUpHospitalFeeDiscountProvider
    {
        public List<SetUpHospitalFeeDiscountModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpHospitalFeeDiscountModel>(AutoMapper.Mapper.Map<IEnumerable<SetupHospitalFeeDiscount>, IEnumerable<SetUpHospitalFeeDiscountModel>>(ent.SetupHospitalFeeDiscounts));


            }
        }
        public int Insert(SetUpHospitalFeeDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpHospitalFeeDiscountModel, SetupHospitalFeeDiscount>(model);
                objToSave.CreatedBy = "1";
                objToSave.CreatedDate = DateTime.Now;
                objToSave.Status = true;
                ent.SetupHospitalFeeDiscounts.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }
        public int Update(SetUpHospitalFeeDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupHospitalFeeDiscounts.Where(x => x.FeeDiscountId == model.FeeDiscountId).FirstOrDefault();

                AutoMapper.Mapper.Map(model, objToEdit);

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }
        public int Delete(int IpdRoomId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupHospitalFeeDiscounts.Where(x => x.FeeDiscountId == IpdRoomId).FirstOrDefault();

                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupHospitalFeeDiscounts.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupHospitalFeeDiscount, SetUpHospitalFeeDiscountModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetupHospitalFeeDiscount, SetUpHospitalFeeDiscountModel>();
                var Details = ent.SetupHospitalFeeDiscounts.Where(x => x.FeeDiscountId == id).FirstOrDefault();
                SetUpHospitalFeeDiscountModel det = AutoMapper.Mapper.Map<SetupHospitalFeeDiscount, SetUpHospitalFeeDiscountModel>(Details);


            }


        }









    }
}