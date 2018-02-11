using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class TrialAuditProviders
    {
        public List<TrialAuditModel> Getlist()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<TrialAuditModel>(AutoMapper.Mapper.Map<IEnumerable<TrialAudit>, IEnumerable<TrialAuditModel>>(ent.TrialAudits)).Take(30).ToList();
        }
    }
}