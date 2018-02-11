using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class VDCMUNProvider
    {
        VDCMUNModel model = new VDCMUNModel();

        public List<VDCMUNModel> PopulateRecords()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<VDCMUNModel> ResultList = ent.VDCMUNs.Select(x => new VDCMUNModel()
                {
                    VdcMunID = x.VdcMunID,
                    DistrictID = x.DistrictID,
                    VdcMunNameEng = x.VdcMunNameEng,
                    VdcMunNameNep = x.VdcMunNameNep,
                }).ToList();
                return ResultList;
            }
        }
        public bool InsertVdcMun(VDCMUNModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objInsertVdcMun = new VDCMUN()
                {
                    VdcMunNameEng = model.VdcMunNameEng,
                    DistrictID = model.DistrictID
                };
                ent.VDCMUNs.Add(objInsertVdcMun);
                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool EditVdcMun(int id, VDCMUNModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.VDCMUNs.ToList().Where(x => x.VdcMunID == id).FirstOrDefault();
                obj.VdcMunNameEng = model.VdcMunNameEng;
                obj.DistrictID = model.DistrictID;

                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool DeleteVdcmun(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objDeleteVdcMun = ent.VDCMUNs.ToList().Where(x => x.VdcMunID == id).FirstOrDefault();
                ent.VDCMUNs.Remove(objDeleteVdcMun);
                int i = ent.SaveChanges();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}