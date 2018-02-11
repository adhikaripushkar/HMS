using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupManpowerProvider
    {

        EHMSEntities ent = new EHMSEntities();



        public List<SetupManpowerModel> GetlistOfMainpower()
        {


            return new List<SetupManpowerModel>(AutoMapper.Mapper.Map<IEnumerable<SetupManpower>, IEnumerable<SetupManpowerModel>>(ent.SetupManpowers));


        }

        public int Insert(SetupManpowerModel model)
        {

            int i = 0;
            var objToSave = AutoMapper.Mapper.Map<SetupManpowerModel, SetupManpower>(model);
            ent.SetupManpowers.Add(objToSave);
            i = ent.SaveChanges();
            return i;
        }

        public int Update(SetupManpowerModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.SetupManpowers.Where(x => x.ManpowerId == model.ManpowerId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);
            // ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();

            return i;


        }

    }
}