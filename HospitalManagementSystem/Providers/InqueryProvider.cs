using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class InqueryProvider
    {
        public List<DoctorPartialDetails> Getlist(int id)
        {
            List<DoctorPartialDetails> EMIDmodel = new List<DoctorPartialDetails>();

            EHMSEntities ent = new EHMSEntities();
            var data = (from x in ent.SetupDoctorMasters
                        join y in ent.SetupDoctorDepartments on x.DoctorID equals y.DoctorID
                        join z in ent.SetupDepartments on y.DepartmentID equals z.DeptID
                        where y.DepartmentID == id
                        select new DoctorPartialDetails
                        {
                            Title = x.Title,
                            FirstName = x.FirstName,
                            MiddleName = x.MiddleName,
                            LastName = x.LastName,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            MobileNumber = x.MobileNumber,
                            Address = x.Address


                        }).ToList();

            foreach (var items in data)
            {
                EMIDmodel.Add(items);
            }
            return EMIDmodel;
        }
        public List<DoctorPartialDetails> GetlistByDName(string name)
        {
            List<DoctorPartialDetails> EMIDmodel = new List<DoctorPartialDetails>();

            EHMSEntities ent = new EHMSEntities();
            var data = (from x in ent.SetupDoctorMasters
                        join y in ent.SetupDoctorDepartments on x.DoctorID equals y.DoctorID
                        join z in ent.SetupDepartments on y.DepartmentID equals z.DeptID
                        where (x.FirstName + " " + (x.MiddleName + " " ?? string.Empty) + x.LastName) == name
                        select new DoctorPartialDetails
                        {
                            Title = x.Title,
                            FirstName = x.FirstName,
                            MiddleName = x.MiddleName,
                            LastName = x.LastName,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            MobileNumber = x.MobileNumber,
                            Address = x.Address


                        }).Distinct().ToList();

            foreach (var items in data)
            {
                EMIDmodel.Add(items);
            }
            return EMIDmodel;
        }

        public List<OpdModel> GetDepositForPatient(int searchVal, string value)
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdModel> returnOpdList = new List<OpdModel>();
            List<OpdModel> OpdModelList = new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters.Where(m => m.Status == true)));

            if (value != null)
            {

                if (searchVal == 1)
                {
                    int intValue = Convert.ToInt32(value);
                    returnOpdList = OpdModelList.Where(m => m.OpdID == intValue).ToList();

                }
                else if (searchVal == 2)
                {

                    //string[] words = value.Split(' ');
                    //string firstname = words[0];
                    //string lastname = words[2];

                    //returnOpdList = OpdModelList.Where(m => m.FirstName == firstname).ToList();
                    returnOpdList = OpdModelList.Where(x => (x.FirstName + " " + (x.MiddleName + " " ?? string.Empty) + x.LastName) == value).ToList();
                    //var dta = (from x in OpdModelList
                    //    where x.LastName + " " + (x.MiddleName + " " ?? string.Empty) + x.LastName == value
                    //    select x).ToList();
                    //foreach(var item in dta)
                    //{
                    //   returnOpdList.Add(item); 
                    //}

                }
                else if (searchVal == 3)
                {
                    returnOpdList = OpdModelList.Where(m => m.Address == value).ToList();
                }
                else if (searchVal == 4)
                {
                    //Int64 intValue = Convert.ToInt64(value);
                    returnOpdList = OpdModelList.Where(m => m.MobileNumber == value).ToList();
                }
                else if (searchVal == 5)
                {
                    Int32 intValue = Convert.ToInt32(value);
                    returnOpdList = OpdModelList.Where(m => m.AgeYear == intValue).ToList();


                }

                else if (searchVal == 6)
                {

                    returnOpdList = OpdModelList.Where(m => m.Sex == value).ToList();


                }
                else if (searchVal == 7)
                {

                    returnOpdList = OpdModelList.Where(m => m.BloodGroup == value).ToList();


                }
                else if (searchVal == 8)
                {
                    DateTime dateTime = Convert.ToDateTime(value);
                    returnOpdList = OpdModelList.Where(m => m.RegistrationDate.Date == dateTime).ToList();


                }

            }


            return returnOpdList;


        }

        public List<IpdInqueryReportModel> IpdSearchByColumnName(int searchColumnId, string txtVal)
        {
            List<IpdInqueryReportModel> list = new List<IpdInqueryReportModel>();
            List<IpdInqueryReportModel> ipdInqueryReportList = new List<IpdInqueryReportModel>();
            EHMSEntities ent = new EHMSEntities();

            list = (from i in ent.IpdRegistrationMasters
                    join b in ent.IpdPatientBedDetails on i.OpdNoEmergencyNo equals b.OpdEmergencyNumber
                    join r in ent.IpdPatientroomDetails on i.OpdNoEmergencyNo equals r.OpdNoEmergencyNo
                    select new IpdInqueryReportModel
                    {
                        IpdRegistrationId = i.IpdRegistrationID,
                        opdid = i.OpdNoEmergencyNo,
                        BedNumber = b.BedNumber,
                        IpdWordNumber = r.WardNo,
                        RoomType = r.RoomType,
                        RoomNumber = r.RoomNo,
                        RegistrationDate = i.RegistrationDate,

                    }).ToList();


            if (searchColumnId == 1)
            {

                ipdInqueryReportList = list.Where(m => m.opdid == Convert.ToInt32(txtVal)).ToList();

            }
            else if (searchColumnId == 2)
            {
                ipdInqueryReportList = list.Where(m => m.IpdRegistrationId == Convert.ToInt32(txtVal)).ToList();


            }
            else if (searchColumnId == 3)
            {

                ipdInqueryReportList = list.Where((m => m.RegistrationDate == Convert.ToDateTime(txtVal))).ToList();
            }
            else if (searchColumnId == 4)
            {
                ipdInqueryReportList = list.Where(m => m.BedNumber == Convert.ToInt32(txtVal)).ToList();
            }
            else if (searchColumnId == 5)
            {
                ipdInqueryReportList = list.Where(m => m.IpdWordNumber == Convert.ToInt32(txtVal)).ToList();
            }
            else if (searchColumnId == 6)
            {
                ipdInqueryReportList = list.Where(m => m.RoomNumber == Convert.ToInt32(txtVal)).ToList();
            }
            else if (searchColumnId == 7)
            {
                ipdInqueryReportList = list.Where(m => m.RoomType == Convert.ToInt32(txtVal)).ToList();
            }
            return ipdInqueryReportList;
        }

    }
}