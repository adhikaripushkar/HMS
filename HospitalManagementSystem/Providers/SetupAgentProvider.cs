using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupAgentProvider
    {



        EHMSEntities ent = new EHMSEntities();



        public List<SetupAgentModel> GetlistOfSetupAgents()
        {


            return new List<SetupAgentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupAgent>, IEnumerable<SetupAgentModel>>(ent.SetupAgents));


        }

        public int Insert(SetupAgentModel model)
        {

            int i = 0;
            var objToSave = AutoMapper.Mapper.Map<SetupAgentModel, SetupAgent>(model);
            ent.SetupAgents.Add(objToSave);
            i = ent.SaveChanges();
            return i;
        }

        public int Update(SetupAgentModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.SetupAgents.Where(x => x.AgentId == model.AgentId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);
            // ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();

            return i;




        }
    }
}