using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Providers
{
    public class IpdCountProviders
    {

        public int GetTotalIpdPatient()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var total = (from b in ent.IpdRegistrationMasters select b).Count();
                return total;
            }


        }
        public int GetTotalIpdMalePatient()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {


                var GetMaleCountFromEM = (from b in ent.OpdMasters
                                          join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                          where b.Sex == "Male" && l.DepartmentID == 1
                                          select b).Count();

                var GetMaleCountFromOM = (from b in ent.OpdMasters
                                          join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                          where b.Sex == "Male" && l.DepartmentID == 2
                                          select b).Count();

                var totalcount = GetMaleCountFromEM + GetMaleCountFromOM;


                return totalcount;
            }


        }
        public int GetTotalIpdFemalePatient()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {


                var GetFemaleCountFromEM = (from b in ent.OpdMasters
                                            join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                            where b.Sex == "Female" && l.DepartmentID == 1
                                            select b).Count();

                var GetFemaleCountFromOM = (from b in ent.OpdMasters
                                            join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                            where b.Sex == "Female" && l.DepartmentID == 2
                                            select b).Count();

                var totalcount = GetFemaleCountFromEM + GetFemaleCountFromOM;


                return totalcount;
            }


        }
        public IpdCountModel getjanuarycount(string year, int Month)
        {

            DateTime startdate;
            DateTime enddate;
            int y = Convert.ToInt32(year);
            EHMSEntities ent = new EHMSEntities();
            IpdCountModel model = new IpdCountModel();



            //for (int i = 1; i <= 12; i++)
            //{
            int lastdate = 0;
            if (Month == 2)
            {
                lastdate = 28;
            }

            //else { lastdate = 30; }
            else if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 10 || Month == 12)
            {
                lastdate = 31;
            }
            else
            {
                lastdate = 30;
            }

            if (Month == 0)
            {
                startdate = Convert.ToDateTime(y + "/" + 1 + "/" + "1");

                enddate = Convert.ToDateTime(y + "/" + 12 + "/" + "31");
            }
            else
            {

                startdate = Convert.ToDateTime(y + "/" + Month + "/" + "1");

                enddate = Convert.ToDateTime(y + "/" + Month + "/" + lastdate);
            }





            var GetMaleCountFromEM = (from b in ent.OpdMasters
                                      join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                      where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Male" && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 1
                                      select b).Count();

            var GetMaleCountFromOM = (from b in ent.OpdMasters
                                      join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                      where b.RegistrationDate >= startdate && b.RegistrationDate <= enddate && b.Sex == "Male" && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 2
                                      select b).Count();

            var totalMalecountbetween = GetMaleCountFromEM + GetMaleCountFromOM;
            model.MaleNobetn = totalMalecountbetween;

            var GetFemaleCountFromEM = (from b in ent.OpdMasters
                                        join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                        where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Female" && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 1
                                        select b).Count();


            var GetFemaleCountFromOM = (from b in ent.OpdMasters
                                        join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                        where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Female" && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 2
                                        select b).Count();
            var totalFemalecountBetn = GetFemaleCountFromOM + GetFemaleCountFromEM;
            model.FemaleNobetn = totalFemalecountBetn;

            var GetMaleCountFromEMAbove = (from b in ent.OpdMasters
                                           join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                           where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Male" && b.AgeYear > 64 && l.DepartmentID == 1
                                           select b).Count();

            var GetMaleCountFromOMAbove = (from b in ent.OpdMasters
                                           join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                           where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Male" && b.AgeYear > 64 && l.DepartmentID == 2
                                           select b).Count();

            var totalMaleCountAbove = GetMaleCountFromEMAbove + GetMaleCountFromOMAbove;
            model.MaleNoAbove = totalMaleCountAbove;

            var GetFemaleCountFromEMAbove = (from b in ent.OpdMasters
                                             join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                             where l.RegistrationDate >= startdate.Date && l.RegistrationDate <= enddate && b.Sex == "Female" && b.AgeYear > 64 && l.DepartmentID == 1
                                             select b).Count();

            var GetFemaleCountFromOMAbove = (from b in ent.OpdMasters
                                             join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                             where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.Sex == "Female" && b.AgeYear > 64 && l.DepartmentID == 2
                                             select b).Count();

            var totalcountFemaleAbove = GetFemaleCountFromEMAbove + GetFemaleCountFromOMAbove;
            model.FemaleNoAbove = totalcountFemaleAbove;

            var GetTotalCountFromEM = (from b in ent.OpdMasters
                                       join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                       where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 1
                                       select b).Count();

            var GetTotalCountFromOM = (from b in ent.OpdMasters
                                       join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                       where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.AgeYear >= 60 && b.AgeYear <= 64 && l.DepartmentID == 2
                                       select b).Count();
            var totalcountBetween = GetTotalCountFromOM + GetTotalCountFromEM;




            model.TotalNoBetn = totalcountBetween;

            var GetTotalCountFromEMAbove = (from b in ent.OpdMasters
                                            join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                            where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.AgeYear > 64 && l.DepartmentID == 1
                                            select b).Count();

            var GetTotalCountFromOMAbove = (from b in ent.OpdMasters
                                            join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                            where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && b.AgeYear > 64 && l.DepartmentID == 2
                                            select b).Count();

            var totalcountAbove = GetTotalCountFromEM + GetTotalCountFromOM;
            model.TotalNoAbove = totalMaleCountAbove;


            var GetGrandTotalCountFromEM = (from b in ent.OpdMasters
                                            join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                            where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && l.DepartmentID == 1 && b.AgeYear >= 60
                                            select b).Count();

            var GetGrandotalCountFromOM = (from b in ent.OpdMasters
                                           join l in ent.IpdRegistrationMasters on b.OpdID equals l.OpdNoEmergencyNo
                                           where l.RegistrationDate >= startdate && l.RegistrationDate <= enddate && l.DepartmentID == 2 && b.AgeYear >= 60
                                           select b).Count();



            var totalGrandcount = GetGrandTotalCountFromEM + GetGrandotalCountFromOM;
            model.GrandTotalElder = totalGrandcount;
            //}


            return model;
        }
    }
}