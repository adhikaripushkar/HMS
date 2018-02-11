using HospitalManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class EmergencyCountProvider
    {
        public int GetMaleCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var malecount = (from b in ent.OpdMasters
                                 where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Male" && b.RegistrationSource == "Emer"
                                 select b).Count();

                return malecount;
            }


        }
        public int GetFemaleCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var Femalecount = (from b in ent.OpdMasters
                                   where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Female" && b.RegistrationSource == "Emer"
                                   select b).Count();

                return Femalecount;
            }


        }
        public int GetOtherCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var Othercount = (from b in ent.OpdMasters
                                  where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Other" && b.RegistrationSource == "Emer"
                                  select b).Count();

                return Othercount;
            }


        }



        public int GetDischargeCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var Dischargecount = (from b in ent.EmergencyMasters
                                      where b.Date >= startdate && b.Date <= Enddate && b.Status <= 0
                                      select b).Count();

                return Dischargecount;
            }


        }
        public int StillOnTreatmentCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var StillOnTreatmentCoun = (from b in ent.EmergencyMasters
                                            where b.Date >= startdate && b.Date <= Enddate && b.OutcomeTypeId == 1
                                            select b).Count();

                return StillOnTreatmentCoun;
            }


        }
        public int LAMACount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var LAMA = (from b in ent.EmergencyMasters
                            where b.Date >= startdate && b.Date <= Enddate && b.OutcomeTypeId == 2
                            select b).Count();

                return LAMA;
            }


        }
        public int DischargeCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var Discharge = (from b in ent.EmergencyMasters
                                 where b.Date >= startdate && b.Date <= Enddate && b.OutcomeTypeId == 6
                                 select b).Count();
                return Discharge;
            }
        }
        public int NormalCaseCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var NC = (from b in ent.EmergencyMasters
                          where b.Date >= startdate && b.Date <= Enddate && b.Case == "Normal Case"
                          select b).Count();

                return NC;
            }


        }
        public int PoliceCaseCount(DateTime startdate, DateTime Enddate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var PC = (from b in ent.EmergencyMasters
                          where b.Date >= startdate && b.Date <= Enddate && b.Case == "Police Case"
                          select b).Count();

                return PC;
            }


        }

        //public int GetMaleCountbetwn(DateTime startdate, DateTime Enddate)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {


        //        var GetMaleCountFromEM = (from b in ent.EmergencyMaster
        //                                  join l in ent.IpdRegistrationMaster on b.EmergencyMasterId equals l.OpdNoEmergencyNo
        //                                  where b.Date >= startdate && b.Date <= Enddate && b.Gender == "Male" && b.Age >= 60 && b.Age <= 64
        //                                  select b).Count();

        //        var GetMaleCountFromOM = (from b in ent.OpdMaster
        //                                  join l in ent.IpdRegistrationMaster on b.OpdID equals l.OpdNoEmergencyNo
        //                                  where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Male" && b.AgeYear >= 60 && b.AgeYear <= 64
        //                                  select b).Count();
        //        var totalcount = GetMaleCountFromEM + GetMaleCountFromOM;


        //        return totalcount;
        //    }


        //}
        //public int GetFemaleCountbetwn(DateTime startdate, DateTime Enddate)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {


        //        var GetFemaleCountFromEM = (from b in ent.EmergencyMaster
        //                                    join l in ent.IpdRegistrationMaster on b.EmergencyMasterId equals l.OpdNoEmergencyNo
        //                                    where b.Date >= startdate && b.Date <= Enddate && b.Gender == "Female" && b.Age >= 60 && b.Age <= 64
        //                                    select b).Count();

        //        var GetFemaleCountFromOM = (from b in ent.OpdMaster
        //                                    join l in ent.IpdRegistrationMaster on b.OpdID equals l.OpdNoEmergencyNo
        //                                    where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Female" && b.AgeYear >= 60 && b.AgeYear <= 64
        //                                    select b).Count();
        //        var totalcount = GetFemaleCountFromOM + GetFemaleCountFromEM;


        //        return totalcount;
        //    }


        //}
        //public int GetMaleCountAbove(DateTime startdate, DateTime Enddate)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {


        //        var GetMaleCountFromEM = (from b in ent.EmergencyMaster
        //                                  join l in ent.IpdRegistrationMaster on b.EmergencyMasterId equals l.OpdNoEmergencyNo
        //                                  where b.Date >= startdate && b.Date <= Enddate && b.Gender == "Male" && b.Age >= 60 && b.Age <= 64
        //                                  select b).Count();

        //        var GetMaleCountFromOM = (from b in ent.OpdMaster
        //                                  join l in ent.IpdRegistrationMaster on b.OpdID equals l.OpdNoEmergencyNo
        //                                  where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Male" && b.AgeYear > 64
        //                                  select b).Count();

        //        var totalcount = GetMaleCountFromEM + GetMaleCountFromOM;


        //        return totalcount;
        //    }


        //}
        //public int GetFemaleCountAbove(DateTime startdate, DateTime Enddate)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {


        //        var GetFemaleCountFromEM = (from b in ent.EmergencyMaster
        //                                    join l in ent.IpdRegistrationMaster on b.EmergencyMasterId equals l.OpdNoEmergencyNo
        //                                    where b.Date >= startdate && b.Date <= Enddate && b.Gender == "Female" && b.Age > 64
        //                                    select b).Count();

        //        var GetFemaleCountFromOM = (from b in ent.OpdMaster
        //                                    join l in ent.IpdRegistrationMaster on b.OpdID equals l.OpdNoEmergencyNo
        //                                    where b.RegistrationDate >= startdate && b.RegistrationDate <= Enddate && b.Sex == "Female" && b.AgeYear > 64
        //                                    select b).Count();

        //        var totalcount = GetFemaleCountFromOM + GetFemaleCountFromEM;
        //        return totalcount;
        //    }


        //}




    }
}