using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class MisPorvider
    {
        public static IEnumerable<SelectListItem> GetDepartment()
        {
            return new SelectList(new[]{
                new{Id ="0",Value="Select"},
                new{Id="1",Value="Opd"},
                new{Id="2",Value="Operation Theatre"},
                new{Id="3",Value="Emergency"},
                new{Id="4",Value="Admission/Discharge"}
            }, "Id", "Value");
        }

        public List<GetBilldetailByDepartmentModelView> GetBillDetailByDepartment(JVMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<GetBilldetailByDepartmentModelView>("BillDetailByDepartment '" + model.objGetBilldetailByDepartmentModelView.BillDate + "','" + model.objGetBilldetailByDepartmentModelView.BillDateTo + "'").ToList();
            }
        }
        public static IEnumerable<MisModel> GetAgerangeListProvider(string agerange)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<MisModel>("GetAgerange'" + agerange + "'").ToList();
            }
        }
        public static IEnumerable<MisModel> GetGenderTypeProvider(string fromDate, string toDate, string genderrange)
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            //    //string genderrangeint = Convert.ToString(genderrange);
            //    return ent.Database.SqlQuery<MisModel>("GetGenderReport'" + genderrange + "'").ToList();
            //}
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            string genderrangeint = genderrange;

            List<MisModel> MisList = new List<MisModel>();
            var genderrangelist = (from m in ent.OpdMasters
                                   join d in ent.OpdFeeDetails
                                    on m.OpdID equals d.OpdID
                                   where m.Sex == genderrangeint && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                                   select new { m, d }).ToList();
            foreach (var item in genderrangelist)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                model.Amount = item.d.TotalAmount;
                MisList.Add(model);
            }
            return MisList;
        }
        public static IEnumerable<MisModel> GetVdcMunProvider(string fromDate, string toDate, string vdcmunno)
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            //    return ent.Database.SqlQuery<MisModel>("getVdcMunicipality'" + vdcmunno + "'").ToList();
            //}
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int vdcmunnoid = Convert.ToInt32(vdcmunno);

            List<MisModel> MisList = new List<MisModel>();
            var vdcmunlist = (from m in ent.OpdMasters
                              join d in ent.OpdFeeDetails
                               on m.OpdID equals d.OpdID
                              where m.vdcID == vdcmunnoid && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                              select new { m, d }).ToList();
            foreach (var item in vdcmunlist)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                model.Amount = item.d.TotalAmount;
                MisList.Add(model);
            }
            return MisList;
        }

        public static IEnumerable<MisModel> GetDistrictProvider(string fromDate, string toDate, string districtno)
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            //    return ent.Database.SqlQuery<MisModel>("GetDistrict'" + districtno + "'").ToList();
            //}
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int districtnoid = Convert.ToInt32(districtno);

            List<MisModel> MisList = new List<MisModel>();
            var districtList = (from m in ent.OpdMasters
                                join d in ent.OpdFeeDetails
                                on m.OpdID equals d.OpdID
                                where m.DistrictId == districtnoid && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                                select new { m, d }).ToList();
            foreach (var item in districtList)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                model.Amount = item.d.TotalAmount;
                MisList.Add(model);
            }
            return MisList;
        }
        public static IEnumerable<MisModel> GetZoneProvider(string fromDate, string toDate, string zoneno)
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            //    return ent.Database.SqlQuery<MisModel>("GetZone'" + zoneno + "'").ToList();
            //}
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int zonenoid = Convert.ToInt32(zoneno);

            List<MisModel> MisList = new List<MisModel>();
            var zoneList = (from m in ent.OpdMasters
                            join d in ent.OpdFeeDetails
                            on m.OpdID equals d.OpdID
                            where m.ZoneID == zonenoid && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                            select new { m, d }).ToList();
            foreach (var item in zoneList)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + "" + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                model.Amount = item.d.TotalAmount;
                MisList.Add(model);
            }
            return MisList;
        }
        public static IEnumerable<MisModel> GetOutReferprovider(string fromDate, string toDate, string referno)
        {
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int refernoid = Convert.ToInt32(referno);

            List<MisModel> MisList = new List<MisModel>();
            var referList = (from m in ent.OpdMasters
                             join d in ent.OpdFeeDetails
                            on m.OpdID equals d.OpdID
                             where m.ReferId == refernoid && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                             select new { m, d }).ToList();
            foreach (var item in referList)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                model.Amount = item.d.TotalAmount;
                MisList.Add(model);
            }
            return MisList;
        }
        public static IEnumerable<MisModel> GetDoctorListProvider(string fromDate, string toDate, string doctor)
        {
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);
            int doctorid = Convert.ToInt32(doctor);

            List<MisModel> MisList = new List<MisModel>();
            var doctorList = (from m in ent.OpdMasters
                              join d in ent.OpdPatientDoctorDetails
                              on m.OpdID equals d.OpdID
                              where d.DoctorID == doctorid && m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                              select new { m, d }).ToList();

            foreach (var item in doctorList)
            {
                MisModel model = new MisModel();
                model.OpdId = item.m.OpdID.ToString();
                model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                model.Sex = item.m.Sex;
                model.RegistrationDate = item.m.RegistrationDate;

                MisList.Add(model);
            }
            return MisList;
        }
        public static IEnumerable<MisModel> GetOpdList(string fromDate, string toDate, string department)
        {
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(fromDate);
            DateTime todate = Convert.ToDateTime(toDate);

            List<MisModel> MisList = new List<MisModel>();
            if (department == "1")
            {
                var opdList = (from m in ent.OpdMasters
                               join d in ent.OpdFeeDetails
                               on m.OpdID equals d.OpdID
                               where m.RegistrationDate >= fromdate && m.RegistrationDate <= todate
                               select new { m, d }).ToList();

                foreach (var item in opdList)
                {
                    MisModel model = new MisModel();
                    model.OpdId = item.m.OpdID.ToString();
                    model.Name = item.m.FirstName + " " + item.m.MiddleName + " " + item.m.LastName;
                    model.Sex = item.m.Sex;
                    model.RegistrationDate = item.m.RegistrationDate;

                    model.Amount = item.d.TotalAmount;
                    MisList.Add(model);
                }
            }
            else if (department == "2")
            {
                var OpThList = (from m in ent.OperationTheatreMasters
                                join
                                    p in ent.OpdMasters
                                    on m.SourceID equals p.OpdID
                                join r in ent.SetupOperationTheatreRooms
                                on m.OperationRoomID equals r.OperationTheatreRoomID
                                join d in ent.OpdFeeDetails
                                on p.OpdID equals d.OpdID
                                where m.OperationDate >= fromdate && m.OperationDate <= todate && m.Status == true
                                select new { m, p, r, d }).ToList();

                foreach (var item in OpThList)
                {
                    MisModel model = new MisModel();

                    model.OperationTheatreId = item.m.OperationTheatreMasterID;
                    model.OperationDate = item.m.OperationDate;
                    model.OperationTime = item.m.OperationStartTime;
                    model.OperationEndTime = item.m.OperationEndTime;
                    model.OperationRoom = item.r.RoomName;
                    model.PatientName = item.p.FirstName + " " + item.p.MiddleName + " " + item.p.LastName;
                    model.TotalCharge = item.m.TotalCharge;
                    model.FeeDate = item.d.FeeDate;

                    MisList.Add(model);
                }
            }



            return MisList;

        }

        public static List<OpdModel> GetPatientNames(string FromDate, string ToDate, string PatientId, string PatientName)
        {
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = Convert.ToDateTime(FromDate);
            DateTime todate = Convert.ToDateTime(ToDate);
            int pid = 0;

            var emg = new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters.Where(x => x.RegistrationDate >= fromdate && x.RegistrationDate <= todate)));

            if (PatientId != "" && PatientName != "")
            {
                pid = Convert.ToInt32(PatientId);
                PatientName = PatientName.Trim();
                emg = emg.Where(x => x.OpdID == pid && x.FirstName.Trim() + " " + x.MiddleName.Trim() + " " + x.LastName.Trim() == PatientName).ToList();
            }
            else if (PatientId != "")
            {
                pid = Convert.ToInt32(PatientId);
                emg = emg.Where(x => x.OpdID == pid).ToList();
            }
            else if (PatientName != "")
            {
                PatientName = PatientName.Trim();
                emg = emg.Where(x => x.FirstName.Trim() + " " + x.MiddleName.Trim() + " " + x.LastName.Trim() == PatientName).ToList();
            }
            return emg;
        }

        public static EmergecyMasterModel GetEmergencyDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = (from e in ent.EmergencyMasters
                       where e.EmergencyMasterId == id
                       select e).SingleOrDefault();
            EmergecyMasterModel model = AutoMapper.Mapper.Map<EmergencyMaster, EmergecyMasterModel>(obj);
            var objTriage = (from t in ent.EmergencyTriages
                             where t.EmergencyMasterId == id
                             select t).SingleOrDefault();
            model.EmergencyTriageModel = AutoMapper.Mapper.Map<EmergencyTriage, EmergencyTriageModel>(objTriage);
            return model;
        }

        public List<PathoTestDetailViewModel> GetMisPathoTestDetails(string StartDate, string EndDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PathoTestDetailViewModel>("MISPathoTestDetails '" + StartDate + "', '" + EndDate + "'").ToList();
            }

        }

        public MisModel GetIPDPatientDetailsFromIPNumber(int IpNumber)
        {
            MisModel model = new MisModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == IpNumber && x.Status == true);
                if (result.Count() > 0)
                {
                    var FirstResult = result.FirstOrDefault();
                    model.ObjIPDetailViewModel.IPId = FirstResult.IpdRegistrationID;
                    model.ObjIPDetailViewModel.OpdId = FirstResult.OpdNoEmergencyNo;
                    model.ObjIPDetailViewModel.RegisterDate = FirstResult.RegistrationDate;
                    //model.ObjIPDetailViewModel.ReferDocId = FirstResult.ReferDoctorId;

                }
            }


            return model;
        }

        public bool InsertMRRecordDischarge(MisModel model)
        {
            //model.ObjMRRecordViewModel = new MRRecordViewModel();

            using (EHMSEntities ent = new EHMSEntities())
            {
                var MRObject = new MedicalRecordDischarge()
                {
                    IPId = model.ObjIPDetailViewModel.IPId,
                    OpdId = model.ObjIPDetailViewModel.IPId,
                    ICDCode = model.ObjMRRecordViewModel.ICDCode,
                    DiagnosisCode = model.ObjMRRecordViewModel.DiagnosisCode,
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Status = true,
                    DischargeDocId = model.ObjMRRecordViewModel.DischargeDocId,
                    RegisteredDate = model.ObjIPDetailViewModel.RegisterDate,
                    DischargeDate = model.ObjMRRecordViewModel.DischargeDate,
                    ConditionAtDischargeId = model.ObjMRRecordViewModel.ConditionAtDischargeId,
                    Remarks = model.ObjMRRecordViewModel.Remarks


                };
                ent.MedicalRecordDischarges.Add(MRObject);
                ent.SaveChanges();
            }
            return true;
        }

        public MisModel GetMrDischargeReports()
        {
            MisModel model = new MisModel();
            model.ObjMRRecordViewModel = new MRRecordViewModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.MedicalRecordDischarges.OrderByDescending(x => x.MedicalRecordId).Where(x => x.Status == true).Take(50);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var ObjMr = new MRRecordViewModel()
                        {
                            OpdId = item.OpdId,
                            IPId = item.IPId,
                            RegisteredDate = item.RegisteredDate,
                            DischargeDate = (DateTime)item.DischargeDate,
                        };

                        model.MRRecordViewModelList.Add(ObjMr);
                    }



                }
            }
            return model;
        }

        public List<PieChartDepartmentswiseViewModel> GetDeptWiseDetailForPie()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PieChartDepartmentswiseViewModel>("PieChartDepartmentWise").ToList();
            }

        }

        public List<DoctorCommissionViewModel> GetDoctorCommissionListByDocId(int DoctorID, string StartdDate, string EndDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //[CommissionDetailByCondition]
                //return ent.Database.SqlQuery<DoctorCommissionViewModel>("GetDoctorCommissionDetails '" + StartdDate + "', '" + EndDate + "'").ToList();
                return ent.Database.SqlQuery<DoctorCommissionViewModel>("CommissionDetailByCondition '" + DoctorID + "', '" + StartdDate + "', '" + EndDate + "'").ToList();
            }

        }

        public List<DoctorCommissionViewModel> GetDoctorCommissionListForMis(string StartdDate, string EndDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<DoctorCommissionViewModel>("GetCommissionDetails '" + StartdDate + "', '" + EndDate + "'").ToList();
            }

        }

        public MisModel GetEHSDoctorDetailsByDocId(int DoctorId)
        {
            MisModel model = new MisModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.CommissionDetails.Where(x => x.DocId == DoctorId);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        EHSDoctorDetailViewModel obj = new EHSDoctorDetailViewModel()
                        {
                            Billnumber = item.BillNumber,
                            DoctorId = (int)item.DocId,
                            FeeAmount = item.CommissionAmount,
                            CommissionName = item.CommissionName,
                            TestId = (int)item.TestId

                        };
                        model.EHSDoctorDetailViewModelList.Add(obj);
                    }

                }
            }

            return model;
        }

        public List<GeneralPayableReportModel> GetBillingReportGeneralAndPayable(string StartdDate, string EndDate, string Type)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int itype = Convert.ToInt32(Type);
                return ent.Database.SqlQuery<GeneralPayableReportModel>("BillGeneralPayableReport '" + itype + "','" + StartdDate + "', '" + EndDate + "'").ToList();
            }
        }

        public List<BillAmountDifference> GetBillingAmountDifference(string StartdDate, string EndDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<BillAmountDifference>("CheckBill '" + StartdDate + "', '" + EndDate + "'").ToList();
            }
        }
        public List<OpdDepartmentWiseReportVM> GetOpdDepartmentWiseReport(string StartdDate, string EndDate, int DeptId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.Database.SqlQuery<OpdDepartmentWiseReportVM>("OpdDepartmentWiseReports '" + StartdDate + "', '" + EndDate + "','" + DeptId + "'").ToList();
            }
        }



    }
}