using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupMedicalChargeProvider
    {

        EHMSEntities ent = new EHMSEntities();



        public List<SetupMedicalChargeModel> GetlistOfMedicalCharge()
        {


            return new List<SetupMedicalChargeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMedicalcharge>, IEnumerable<SetupMedicalChargeModel>>(ent.SetupMedicalcharges));


        }


        public int Insert(SetupMedicalChargeModel model)
        {

            int i = 0;
            var objToSave = AutoMapper.Mapper.Map<SetupMedicalChargeModel, SetupMedicalcharge>(model);
            ent.SetupMedicalcharges.Add(objToSave);
            i = ent.SaveChanges();
            return i;
        }

        public int Update(SetupMedicalChargeModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.SetupMedicalcharges.Where(x => x.MedicalChargeId == model.MedicalChargeId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);
            // ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();

            return i;


        }

    }
}