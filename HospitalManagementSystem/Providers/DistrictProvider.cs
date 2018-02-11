using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DistrictProvider
    {
        DistrictModel model = new DistrictModel();

        public List<DistrictModel> PopulateDistrict()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<DistrictModel> DistrictList = ent.TblDistricts.Select(x => new DistrictModel()
                {
                    DistrictID = x.DistrictID,
                    DistrictEng = x.DistrictEng,
                    District = x.District,
                    ZoneID = x.ZoneID
                }).ToList();
                return DistrictList;
            }
        }
        public bool Insertdistrict(DistrictModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objInserdistrict = new TblDistrict
                {
                    DistrictEng = model.DistrictEng,
                    ZoneID = model.ZoneID
                };
                ent.TblDistricts.Add(objInserdistrict);
                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Editdistrict(int id, DistrictModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objeditDistrict = ent.TblDistricts.ToList().Where(x => x.DistrictID == id).FirstOrDefault();
                objeditDistrict.DistrictEng = model.DistrictEng;
                objeditDistrict.ZoneID = model.ZoneID;

                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Deletedistrict(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objDeletedistrict = ent.TblDistricts.ToList().Where(x => x.DistrictID == id).SingleOrDefault();
                ent.TblDistricts.Remove(objDeletedistrict);

                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
