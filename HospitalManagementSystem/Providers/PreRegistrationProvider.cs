using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class PreRegistrationProvider
    {
        public List<PreRegistrationModel> GetResults()
        {
            List<PreRegistrationModel> model = new List<PreRegistrationModel>();
            PreRegistrationPreferDetailsModel models = new PreRegistrationPreferDetailsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                DateTime today = DateTime.Today;
                var result = (from a in ent.PreRegistrations
                              join b in ent.PreRegistrationPreferDetails

                              on a.PreRegistrationID equals b.PreRegistrationID
                              join c in ent.SetupDepartments on b.DepartmentID equals c.DeptID
                              join d in ent.SetupDoctorMasters on b.DoctorID equals d.DoctorID
                              where b.PreferDate >= (today)
                              select new PreRegistrationModel
                              {

                                  FirstName = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,

                                  RegistrationDate = a.RegistrationDate,
                                  Sex = a.Sex,
                                  MobileNumber = a.MobileNumber,

                                  AgeYear = (int)a.AgeYear,
                                  Address = a.Address,
                                  RegistrationMode = a.RegistrationMode,
                                  DepartmentName = c.DeptName,
                                  DoctorName = d.FirstName + " " + (d.MiddleName + " " ?? string.Empty) + d.LastName,
                                  PreferDate = b.PreferDate,
                                  PreferTime = b.PreferTime,
                                  PreRegistrationID = a.PreRegistrationID


                              }).ToList();

                foreach (var item in result)
                {

                    model.Add(item);

                }
                return model;

            }
        }
        public List<PreRegistrationModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                List<PreRegistrationModel> model = new List<PreRegistrationModel>();

                model = AutoMapper.Mapper.Map<IEnumerable<PreRegistration>, IEnumerable<PreRegistrationModel>>(ent.PreRegistrations).Where(x => x.PreregistrationPreferDetailsModel.PreferDate >= DateTime.Today).ToList();

                foreach (var item in model)
                {
                    var data = ent.PreRegistrationPreferDetails.Where(x => x.PreRegistrationID == item.PreRegistrationID).FirstOrDefault();

                    if (data != null)
                    {
                        var temp = from C in ent.SetupDepartments where C.DeptID == data.DepartmentID select C.DeptName;
                        PreRegistrationPreferDetailsModel m = new PreRegistrationPreferDetailsModel()
                        {
                            DoctorID = data.DoctorID,

                            DepartmentID = data.DepartmentID,

                            PreferDetailsID = data.PreferDetailsID
                        };
                        item.PreregistrationPreferDetailsModel = m;
                    }
                }
                return model;
            }
        }
        public PreRegistrationModel GetListForPrint(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationModel model = new PreRegistrationModel();
                model = AutoMapper.Mapper.Map<IEnumerable<PreRegistration>, IEnumerable<PreRegistrationModel>>(ent.PreRegistrations).Where(x => x.PreRegistrationID == id).FirstOrDefault();


                return model;
            }
        }
        public PreRegistrationPreferDetailsModel GetListForPrintNext(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationPreferDetailsModel models = new PreRegistrationPreferDetailsModel();
                models = AutoMapper.Mapper.Map<IEnumerable<PreRegistrationPreferDetail>, IEnumerable<PreRegistrationPreferDetailsModel>>(ent.PreRegistrationPreferDetails).Where(x => x.PreRegistrationID == id).FirstOrDefault();

                return models;
            }
        }
        public List<PreRegistrationPreferDetailsModel> GetEdit(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PreRegistrationPreferDetailsModel> models = new List<PreRegistrationPreferDetailsModel>();
                models = AutoMapper.Mapper.Map<IEnumerable<PreRegistrationPreferDetail>, IEnumerable<PreRegistrationPreferDetailsModel>>(ent.PreRegistrationPreferDetails).Where(x => x.PreRegistrationID == id).ToList();

                return models;
            }
        }


        public int Insert(PreRegistrationModel model)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<PreRegistrationModel, PreRegistration>(model);
                objToSave.Status = true;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.RegistrationDate = DateTime.Now;
                ent.PreRegistrations.Add(objToSave);
                ent.SaveChanges();

                model.PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();
                var objtosavedetails = AutoMapper.Mapper.Map<PreRegistrationPreferDetailsModel, PreRegistrationPreferDetail>(model.PreregistrationPreferDetailsModel);

                objtosavedetails.PreRegistrationID = objToSave.PreRegistrationID;

                foreach (var item in model.PreRegistrationPreferModelList)
                {
                    objtosavedetails.PreferDate = (DateTime)item.PreferDate;
                    objtosavedetails.PreferTime = (TimeSpan)item.PreferTime;
                    objtosavedetails.DoctorID = item.DoctorID;
                    objtosavedetails.DepartmentID = item.DepartmentID;
                    ent.PreRegistrationPreferDetails.Add(objtosavedetails);
                    i = ent.SaveChanges();
                }

                // i= ent.SaveChanges(); 

            }
            try
            {

                i = Utility.SendEMail(model.Email, model);
            }
            catch
            {

            }
            return i;

        }

        public int Update(PreRegistrationModel model, int id)
        {
            int i = 0;
            {

                using (EHMSEntities ent = new EHMSEntities())
                {

                    var objToSave = AutoMapper.Mapper.Map<PreRegistrationModel, PreRegistration>(model);
                    objToSave.CreatedBy = 1;
                    objToSave.CreatedDate = DateTime.Now;
                    objToSave.Status = true;
                    objToSave.RegistrationDate = DateTime.Now;
                    ent.Entry(objToSave).State = System.Data.EntityState.Modified;
                    model.PreregistrationPreferDetailsModel = new PreRegistrationPreferDetailsModel();
                    var objtosavedetails = AutoMapper.Mapper.Map<PreRegistrationPreferDetailsModel, PreRegistrationPreferDetail>(model.PreregistrationPreferDetailsModel);

                    objtosavedetails.PreRegistrationID = objToSave.PreRegistrationID;
                    var objToDelete = ent.PreRegistrationPreferDetails.Where(x => x.PreRegistrationID == id).ToList();

                    foreach (var deptItem in objToDelete)
                    {
                        var preferdetailsid = ent.PreRegistrationPreferDetails.Where(x => x.PreRegistrationID == id).FirstOrDefault();

                        ent.PreRegistrationPreferDetails.Remove(preferdetailsid);

                        ent.SaveChanges();
                    }

                    if (model.PreRegistrationPreferModelList != null)
                    {
                        foreach (var item in model.PreRegistrationPreferModelList)
                        {
                            objtosavedetails.PreferDate = (DateTime)item.PreferDate;
                            objtosavedetails.PreferTime = (TimeSpan)item.PreferTime;
                            objtosavedetails.DoctorID = item.DoctorID;
                            objtosavedetails.DepartmentID = item.DepartmentID;
                            ent.PreRegistrationPreferDetails.Add(objtosavedetails);
                            i = ent.SaveChanges();

                        }
                    }


                }

            }
            return i;
        }



        public int Delete(int del)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.PreRegistrationPreferDetails.Where(x => x.PreRegistrationID == del).ToList();


                foreach (var deptItem in objToDelete)
                {
                    var preferdetailsid = ent.PreRegistrationPreferDetails.Where(x => x.PreRegistrationID == del).FirstOrDefault();

                    ent.PreRegistrationPreferDetails.Remove(preferdetailsid);

                    ent.SaveChanges();
                }
                var objdelete = ent.PreRegistrations.Where(x => x.PreRegistrationID == del).FirstOrDefault();
                ent.PreRegistrations.Remove(objdelete);
                i = ent.SaveChanges();



            }
            return i;
        }
        public List<PreRegistrationModel> GetResult()
        {
            List<PreRegistrationModel> model = new List<PreRegistrationModel>();
            PreRegistrationPreferDetailsModel models = new PreRegistrationPreferDetailsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                DateTime today = DateTime.Today;
                var result = (from a in ent.PreRegistrations
                              join b in ent.PreRegistrationPreferDetails
                              on a.PreRegistrationID equals b.PreRegistrationID
                              where b.PreferDate.Equals(today) && a.Status == true
                              select new PreRegistrationModel
                              {


                                  FirstName = a.FirstName,
                                  MiddleName = a.MiddleName,
                                  LastName = a.LastName,
                                  RegistrationDate = a.RegistrationDate,
                                  Sex = a.Sex,
                                  MobileNumber = a.MobileNumber,
                                  ContactName = a.ContactNumber,
                                  AgeYear = (int)a.AgeYear,
                                  Address = a.Address,
                                  //ZoneId = a.ZoneId,
                                  //DistrictID = a.DistrictID,
                                  //vdcID = a.vdcID,
                                  RegistrationMode = a.RegistrationMode,
                                  DepartMentID = b.DepartmentID,
                                  DoctorID = b.DoctorID,
                                  PreferDate = b.PreferDate,
                                  PreferTime = b.PreferTime,
                                  PrefailID = b.PreferDetailsID,
                                  PreRegistrationID = a.PreRegistrationID,
                                  Email = a.Email

                              }).GroupBy(x => x.PreRegistrationID).Select(g => g.FirstOrDefault());

                foreach (var item in result)
                {

                    model.Add(item);

                }
                return model;

            }
        }
        public void UpdateUserStatus(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                PreRegistration pre = (from a in ent.PreRegistrations
                                       where a.PreRegistrationID == id
                                       select a).First();
                pre.Status = false;

                ent.SaveChanges();


            }
        }
        public List<OpdDoctorListModel> Value(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<OpdDoctorListModel> models = new List<OpdDoctorListModel>();
                models = AutoMapper.Mapper.Map<IEnumerable<PreRegistrationPreferDetail>, IEnumerable<OpdDoctorListModel>>(ent.PreRegistrationPreferDetails).Where(x => x.PreRegistrationID == id).ToList();

                return models;

            }


        }
        public List<PreRegistrationDoctorDisease> GetDoctorDisease(int id)
        {
            List<PreRegistrationDoctorDisease> DoctorList = new List<PreRegistrationDoctorDisease>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = (from a in ent.DoctorsDiseases
                              join b in ent.SetupDoctorMasters on a.DoctorID equals b.DoctorID
                              join d in ent.SetupDepartments on a.DepartmentID equals d.DeptID
                              where a.DiseaseID == id

                              select new PreRegistrationDoctorDisease
                              {
                                  DoctorName = b.FirstName + " " + (b.MiddleName + " " ?? string.Empty) + b.LastName,
                                  DoctorID = a.DoctorID,
                                  DepartmentName = d.DeptName

                              }).Distinct().ToList();
                foreach (var item in result)
                {
                    DoctorList.Add(item);

                }
                return DoctorList;

            }

        }
        public List<PreRegistrationDoctorTime> DoctorTime(int id, string day)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PreRegistrationDoctorTime> doctortime = new List<PreRegistrationDoctorTime>();
                PreRegistrationDoctorTime model = new PreRegistrationDoctorTime();
                var result = (from a in ent.SetupDoctorAvailableTimes
                              where a.DoctorID == id && a.DoctorDays == day
                              select new PreRegistrationDoctorTime
                              {

                                  StartTime = (TimeSpan)a.StartTime,
                                  EndTime = (TimeSpan)a.EndTime

                              }).ToList();

                foreach (var item in result)
                {
                    doctortime.Add(item);

                }

                return doctortime;


            }


        }


        public int countopd(DateTime StartTime, DateTime EndTime)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                ReportModel model = new ReportModel();
                var data = (from a in ent.PreRegistrations
                            select a);

                int count = data.Where(x => x.RegistrationDate >= StartTime && x.RegistrationDate <= EndTime && x.Status == false).Count();
                return count;

            }

        }
        public ReportModel countRegistrationmode(DateTime StartTime, DateTime EndTime, string RegistrationModeId)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                //  string registrationmode = Utility.RegistrationModeLists(RegistrationModeId);
                ReportModel model = new ReportModel();
                var data = (from a in ent.PreRegistrations where a.RegistrationMode == RegistrationModeId && a.RegistrationDate >= StartTime && a.RegistrationDate <= EndTime select a).ToList();

                model.CountMale = data.Where(x => x.Sex == "Male").Count();
                model.CountFemale = data.Where(x => x.Sex == "Female").Count();
                model.Total = data.Count();
                model.AbsTotal = data.Where(x => x.Status == false).Count();
                model.Absent = model.Total - model.AbsTotal;


                return model;



            }


        }
        public int count(DateTime StartTime, DateTime EndTime)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                ReportModel model = new ReportModel();
                var data = (from a in ent.PreRegistrations


                            select a);

                int count = data.Where(x => x.RegistrationDate >= StartTime && x.RegistrationDate <= EndTime).Count();
                return count;




            }


        }


    }

}
