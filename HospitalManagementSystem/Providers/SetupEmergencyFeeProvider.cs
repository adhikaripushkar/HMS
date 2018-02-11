using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupEmergencyFeeProviders
    {
        public List<SetUpEmergencyFeeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpEmergencyFeeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupEmergencyFee>, IEnumerable<SetUpEmergencyFeeModel>>(ent.SetupEmergencyFees));


            }
        }
        public int Insert(SetUpEmergencyFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpEmergencyFeeModel, SetupEmergencyFee>(model);
                objToSave.Status = false;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupEmergencyFees.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;
        }
        public int Update(SetUpEmergencyFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupEmergencyFees.Where(x => x.FeeId == model.FeeId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.Status = false;
                objToEdit.CreatedDate = DateTime.Now;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }
        public int Delete(int FeeId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupEmergencyFees.Where(x => x.FeeId == FeeId).FirstOrDefault();

                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupEmergencyFees.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }
        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                //AutoMapper.Mapper<SetupEmergencyFee, SetUpEmergencyFeeModel>();noticed
                var Details = ent.SetupEmergencyFees.Where(x => x.FeeId == id).FirstOrDefault();
                SetUpEmergencyFeeModel det = AutoMapper.Mapper.Map<SetupEmergencyFee, SetUpEmergencyFeeModel>(Details);


            }


        }
        public void ChangToCurrent(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.SetupEmergencyFees.Where(x => x.Status.Equals(true)).FirstOrDefault();
            if (objt != null)
            {

                objt.Status = false;
                ent.Entry(objt).State = System.Data.EntityState.Modified;
                ent.SaveChanges();

            }

            objt = ent.SetupEmergencyFees.Where(x => x.FeeId == id).FirstOrDefault();
            objt.Status = true;
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }

    }
}