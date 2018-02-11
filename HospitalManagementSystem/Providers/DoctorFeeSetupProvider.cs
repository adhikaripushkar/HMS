using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Providers
{
    public class DoctorFeeSetupProvider
    {
        public List<DoctorFeeSetupModel> GetDoctorFeeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<DoctorFeeSetupModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDoctorFee>, IEnumerable<DoctorFeeSetupModel>>(ent.SetupDoctorFees)).Take(20).ToList();

            }
        }

        public int Insert(DoctorFeeSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objTosaveDoctorFee = AutoMapper.Mapper.Map<DoctorFeeSetupModel, SetupDoctorFee>(model);
                objTosaveDoctorFee.CreatedBy = Utility.GetCurrentLoginUserId();
                objTosaveDoctorFee.CreatedDate = DateTime.Now;
                ent.SetupDoctorFees.Add(objTosaveDoctorFee);
                i = ent.SaveChanges();
            }
            return i;
        }

        public int Update(DoctorFeeSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoeditDoctorFeeSetup = ent.SetupDoctorFees.Where(x => x.DoctorFeeID == model.DoctorFeeID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoeditDoctorFeeSetup);
                i = ent.SaveChanges();
            }
            return i;

        }

        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupDoctorFees.Where(x => x.DoctorFeeID == id).FirstOrDefault();
                ent.SetupDoctorFees.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
    }
}
