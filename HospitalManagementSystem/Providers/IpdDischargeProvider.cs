using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class IpdDischcargeProvider
    {
        public List<IpdSearchResults> SearchByPatientNameToDischarge(string name)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DetailsData = (from o in ent.OpdMasters
                                   join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                   join d in ent.IpdDischarges on r.IpdRegistrationID equals d.ipdResistrationID
                                   join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                   where o.Status == true && o.FirstName.Contains(name)
                                   select new IpdSearchResults
                                   {
                                       FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                       DepartmentId = r.DepartmentID,
                                       RoomType = room.RoomType,
                                       RoomNo = room.RoomNo,
                                       WardNo = room.WardNo,
                                       BedNo = room.BedNo,
                                       sex = o.Sex,
                                       Age = o.AgeYear,
                                       Address = o.Address,
                                       OpdEmrNumber = r.OpdNoEmergencyNo,
                                       IpdRegistrationNumber = r.IpdRegistrationID,
                                       RegistrationDate = o.RegistrationDate

                                   }).ToList();

                foreach (var items in DetailsData)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }

    }
}