using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class ZoneProvider
    {
        ZoneModel model = new ZoneModel();
        public List<ZoneModel> PopulateRecords()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<ZoneModel> ResultList = ent.TblZones.Select(x => new ZoneModel()
                {
                    ZoneID = x.ZoneID,
                    Zone = x.Zone,
                    ZoneInEng = x.ZoneInEng,
                }).ToList();
                return ResultList;
            }
        }
        public ZoneModel InsetZone(ZoneModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objInsertZone = new TblZone()
                {
                    ZoneInEng = model.ZoneInEng,
                };
                int i = 0;
                try
                {
                    ent.TblZones.Add(objInsertZone);
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
        public ZoneModel EditZone(int id, ZoneModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.TblZones.ToList().Where(x => x.ZoneID == id).SingleOrDefault();

                obj.ZoneInEng = model.ZoneInEng;
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

        public bool DeleteZone(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objDeleteZone = ent.TblZones.ToList().Where(x => x.ZoneID == id).SingleOrDefault();
                ent.TblZones.Remove(objDeleteZone);
                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
