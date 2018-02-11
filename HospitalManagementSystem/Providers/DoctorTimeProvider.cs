using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DoctorTimeProvider
    {

        public List<DoctorTimeModel> Getlist()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<DoctorTimeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDoctorAvailableTime>, IEnumerable<DoctorTimeModel>>(ent.SetupDoctorAvailableTimes)).ToList();

        }
        public int Insert(DoctorTimeModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var doctorTime = AutoMapper.Mapper.Map<DoctorTimeModel, SetupDoctorAvailableTime>(model);

                doctorTime.DoctorID = model.DoctorID;
                doctorTime.DoctorDays = model.DoctorDays;


                foreach (var item in model.DoctorTimeList)
                {

                    if (model.DoctorDays == null)
                    {

                        string day = "";

                        for (int d = 1; d <= 7; d++)
                        {
                            // @Hospital.Utility.GetIpdWardName().Where(x => x.Value == Model.IpdPatientroomDetailsModel.WardNo.ToString()).Select(x => x.Text).SingleOrDefault()


                            //day=Hospital.Utility.GetDayOfWeek()Select(x=>x.Text).Single();
                            //if (i == 1)
                            //{
                            //    day = "Sunday";
                            //}
                            //if(i==)

                            day = HospitalManagementSystem.Utility.getDayName(d);

                            var ObjDocotrTime = AutoMapper.Mapper.Map<DoctorTimeModel, SetupDoctorAvailableTime>(model);

                            ObjDocotrTime.DoctorID = model.DoctorID;
                            ObjDocotrTime.DoctorDays = day;
                            ObjDocotrTime.Shift = item.Shift;
                            ObjDocotrTime.StartTime = item.StartTime;
                            ObjDocotrTime.EndTime = item.EndTime;
                            ObjDocotrTime.CheckPatientPerDay = item.CheckPatientPerDay;
                            ObjDocotrTime.WebPatientPerDay = item.WebPatientPerDay;
                            ObjDocotrTime.DoctorFee = 0;
                            ObjDocotrTime.MaxTokenNumber = 0;
                            ObjDocotrTime.Remarks = item.Remarks;
                            ObjDocotrTime.CreatedBy = 1;
                            ObjDocotrTime.CreatedDate = DateTime.Now;
                            ObjDocotrTime.Status = true;

                            ent.SetupDoctorAvailableTimes.Add(ObjDocotrTime);
                            i = ent.SaveChanges();
                        }
                    }
                    else
                    {

                        var ObjDocotrTime = AutoMapper.Mapper.Map<DoctorTimeModel, SetupDoctorAvailableTime>(model);
                        ObjDocotrTime.DoctorID = model.DoctorID;
                        ObjDocotrTime.DoctorDays = model.DoctorDays;
                        ObjDocotrTime.Shift = item.Shift;
                        ObjDocotrTime.StartTime = item.StartTime;
                        ObjDocotrTime.EndTime = item.EndTime;
                        ObjDocotrTime.CheckPatientPerDay = item.CheckPatientPerDay;
                        ObjDocotrTime.WebPatientPerDay = item.WebPatientPerDay;
                        ObjDocotrTime.DoctorFee = 0;
                        ObjDocotrTime.MaxTokenNumber = 0;
                        ObjDocotrTime.Remarks = item.Remarks;
                        ObjDocotrTime.CreatedBy = 1;
                        ObjDocotrTime.CreatedDate = DateTime.Now;
                        ObjDocotrTime.Status = true;

                        ent.SetupDoctorAvailableTimes.Add(ObjDocotrTime);
                        i = ent.SaveChanges();
                    }
                }
                return i;
            }

        }
        public List<DoctorTimeModel> GetDoctorTimeDetails(int id, string DayOfWeek)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                if (DayOfWeek == "")
                {
                    List<DoctorTimeModel> models = new List<DoctorTimeModel>();
                    models = AutoMapper.Mapper.Map<IEnumerable<SetupDoctorAvailableTime>, IEnumerable<DoctorTimeModel>>(ent.SetupDoctorAvailableTimes).Where(m => m.DoctorID == id).ToList();
                    return models;
                }
                else
                {
                    List<DoctorTimeModel> models = new List<DoctorTimeModel>();
                    models = AutoMapper.Mapper.Map<IEnumerable<SetupDoctorAvailableTime>, IEnumerable<DoctorTimeModel>>(ent.SetupDoctorAvailableTimes).Where(m => m.DoctorID == id && m.DoctorDays == DayOfWeek).ToList();
                    return models;
                }
            }
        }
        public List<DoctorTimeModel> GetDoctosrNameList()
        {
            List<DoctorTimeModel> model = new List<DoctorTimeModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupDoctorAvailableTimes
                            where x.Status == true
                            select new DoctorTimeModel
                            {
                                DoctorID = x.DoctorID
                            }).Distinct().ToList();

                foreach (var item in data)
                {
                    model.Add(item);

                }
                return model;

            }

        }
        //sShow All GetData
        public List<DoctorTimeModel> GetById(int id)
        {
            List<DoctorTimeModel> EditListData = new List<DoctorTimeModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupDoctorAvailableTimes
                            join y in ent.SetupDoctorMasters on x.DoctorID equals y.DoctorID
                            where x.DoctorID == id && x.Status == true
                            select new DoctorTimeModel
                            {
                                WebPatientPerDay = x.WebPatientPerDay,
                                CheckPatientPerDay = x.CheckPatientPerDay,
                                TimeId = x.DoctorAvailableTimeId,
                                DoctorDays = x.DoctorDays,
                                Shift = x.Shift,
                                StartTime = x.StartTime,
                                EndTime = x.EndTime,
                                DoctorName = y.FirstName + " " + y.LastName

                            }).ToList();

                foreach (var item in data)
                {
                    EditListData.Add(item);
                }
                return EditListData;

            }
        }
        public List<DoctorTimeModel> EditData(int id)
        {
            List<DoctorTimeModel> EditListData = new List<DoctorTimeModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupDoctorAvailableTimes
                            join y in ent.SetupDoctorMasters on x.DoctorID equals y.DoctorID
                            where x.DoctorAvailableTimeId == id
                            select new DoctorTimeModel
                            {

                                DoctorID = x.DoctorID,
                                DoctorAvailableTimeId = x.DoctorAvailableTimeId,
                                WebPatientPerDay = x.WebPatientPerDay,
                                CheckPatientPerDay = x.CheckPatientPerDay,
                                StartTime = x.StartTime,
                                EndTime = x.EndTime,
                                DoctorName = y.FirstName + " " + y.LastName

                            }).ToList();

                foreach (var item in data)
                {
                    EditListData.Add(item);
                }
                return EditListData;

            }
        }
        //Update
        public int Update(DoctorTimeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var UpdateData = ent.SetupDoctorAvailableTimes.Where(m => m.DoctorAvailableTimeId == model.DoctorAvailableTimeId).SingleOrDefault();
                model.CreatedBy = UpdateData.CreatedBy;
                model.DoctorID = UpdateData.DoctorID;
                model.Shift = UpdateData.Shift;
                model.DoctorFee = UpdateData.DoctorFee;
                model.CreatedDate = UpdateData.CreatedDate;
                model.MaxTokenNumber = UpdateData.MaxTokenNumber;
                model.DoctorDays = UpdateData.DoctorDays;
                model.Status = UpdateData.Status;
                model.Remarks = UpdateData.Remarks;
                AutoMapper.Mapper.Map(model, UpdateData);
                ent.Entry(UpdateData).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
                return 1;
            }
        }

    }
}