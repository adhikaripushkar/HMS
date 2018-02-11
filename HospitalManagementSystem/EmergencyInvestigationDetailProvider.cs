using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{

    public class EmergencyInvestigationDetailProvider
    {
        public void InsertEI(EmergecyMasterModel model, int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.EmergencyInvestigationList)
                {
                    EmergencyInvestigationDetail EIDM = new EmergencyInvestigationDetail();
                    EIDM.EmergencyMasterId = id;
                    EIDM.TestId = item.TestID;
                    EIDM.Remarks = item.Remarks;
                    ent.EmergencyInvestigationDetails.Add(EIDM);
                }
                ent.SaveChanges();
            }

        }
        public List<EmergencyInvestigationDetailModel> GetSelectedData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<EmergencyInvestigationDetailModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyInvestigationDetail>, IEnumerable<EmergencyInvestigationDetailModel>>(ent.EmergencyInvestigationDetails).Where(m => m.EmergencyMasterId == id));
            }
        }
        public void Udate(int id, EmergecyMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();


            foreach (var item in ent.EmergencyInvestigationDetails.Where(x => x.EmergencyMasterId == model.EmergencyMasterId).ToList())
            {
                ent.EmergencyInvestigationDetails.Remove(item);


            }
            try
            {
                foreach (var item in model.EmergencyInvestigationList)
                {
                    EmergencyInvestigationDetail EIDM = new EmergencyInvestigationDetail();
                    EIDM.EmergencyMasterId = id;
                    EIDM.TestId = item.TestID;
                    EIDM.Remarks = item.Remarks;
                    ent.EmergencyInvestigationDetails.Add(EIDM);
                }
            }
            catch
            {
            }
            ent.SaveChanges();
        }


    }
}