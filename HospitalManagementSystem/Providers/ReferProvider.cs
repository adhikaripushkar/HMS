using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class ReferProvider
    {
        ReferModel model = new ReferModel();
        public List<ReferModel> PopulateRefer()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<ReferModel> ResultList = ent.ReferTables.Select(x => new ReferModel()
                {
                    Id = x.Id,
                    Refername = x.Refername
                }).ToList();
                return ResultList;
            }
        }

        public ReferModel InsertRefer(ReferModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objInsertRefer = new ReferTable()
                {
                    Refername = model.Refername,
                };
                int i = 0;
                try
                {
                    ent.ReferTables.Add(objInsertRefer);
                    i = ent.SaveChanges();
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.ToString();
                    model.Successmessage = "Error";
                    return model;
                }
                if (i > 0)
                {
                    model.Successmessage = "Fail";
                    return model;
                }
                else
                {
                    model.Successmessage = "Fail";
                    return model;
                }
            }
        }
        public ReferModel EditRefer(int id, ReferModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.ReferTables.ToList().Where(x => x.Id == id).SingleOrDefault();
                obj.Refername = model.Refername;
                int i = 0;
                try
                {
                    i = ent.SaveChanges();
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.ToString();
                    model.Successmessage = "Error";
                    return model;
                }
                if (i > 0)
                {
                    model.Successmessage = "Success";
                    return model;
                }
                else
                {
                    model.Successmessage = "Fail";
                    return model;
                }
            }
        }

        public bool DeleteRefer(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objDeleteRefer = ent.ReferTables.ToList().Where(x => x.Id == id).SingleOrDefault();
                ent.ReferTables.Remove(objDeleteRefer);
                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
