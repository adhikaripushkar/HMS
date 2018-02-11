using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class BillingCounterNewProvider
    {
        public List<BillingCounterNewTestListModel> getTestDetailTestIDWiseNew(string id, string dept)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillingCounterNewTestListModel>("GetTestDetailTestIDWiseNew " + Convert.ToInt32(id) + ", " + Convert.ToInt32(dept) + "").ToList();
                //return ent.Database.SqlQuery<BillingCounterTestListModel>("GetTestDetailTestIDWise 9, 1003").ToList();
            }
        }
    }
}