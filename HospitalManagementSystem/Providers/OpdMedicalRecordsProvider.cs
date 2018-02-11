using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class OpdMedicalRecordsProviders
    {

        public List<TodayListViewModel> GetlistBySearchWord(int DeptId, int doctId)
        {

            EHMSEntities ent = new EHMSEntities();
            List<TodayListViewModel> todayList = new List<TodayListViewModel>();
            DateTime todayDate = DateTime.Today;

            //  if (DeptId == 0041 && doctId == 0051)
            //  {
            var details = (from p in ent.OpdPatientDoctorDetails
                           join q in ent.OpdMasters on p.OpdID equals q.OpdID
                           where p.PreferDate == DateTime.Today
                           orderby p.PatientDoctorDetailID
                           select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName + " " + (q.MiddleName + " " ?? string.Empty) + q.LastName, Address = q.Address, Age = q.AgeYear, DoctorId = p.DoctorID, DepartmentId = p.DepartmentID }).Take(4).ToList();

            foreach (var item in details)
            {
                todayList.Add(item);

            }
            // }

            //if (DeptId == 0041 && doctId != 0051)
            //{
            //    var details = (from p in ent.OpdPatientDoctorDetails
            //                   join q in ent.OpdMaster on p.OpdID equals q.OpdID
            //                   where p.PreferDate == DateTime.Today && p.DoctorID == doctId
            //                   orderby p.PatientDoctorDetailID
            //                   select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName, Address = q.Address, Age = q.AgeYear }).Take(4).ToList();

            //    foreach (var item in details)
            //    {
            //        todayList.Add(item);

            //    }


            //}

            //if (DeptId != 0041 && doctId != 0051)
            //{
            //    var details = (from p in ent.OpdPatientDoctorDetails
            //                   join q in ent.OpdMaster on p.OpdID equals q.OpdID
            //                   where p.PreferDate == DateTime.Today && p.DepartmentID == DeptId && p.DoctorID == doctId
            //                   orderby p.PatientDoctorDetailID
            //                   select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = ((q.FirstName ?? "") + " " + (q.LastName ?? "")).Trim(), Address = q.Address, Age = q.AgeYear }).Take(4).ToList();

            //    foreach (var item in details)
            //    {
            //        todayList.Add(item);

            //    }


            //}
            //if (DeptId != 0041 && doctId == 0051)
            //{
            //    var details = (from p in ent.OpdPatientDoctorDetails
            //                   join q in ent.OpdMaster on p.OpdID equals q.OpdID
            //                   where p.PreferDate == DateTime.Today && p.DepartmentID == DeptId
            //                   orderby p.PatientDoctorDetailID
            //                   select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName, Address = q.Address, Age = q.AgeYear }).Take(4).ToList();

            //    foreach (var item in details)
            //    {
            //        todayList.Add(item);

            //    }


            //}

            return todayList;

        }


        public List<TodayListViewModel> getOpdMRdetailwithDoctorIdandDepartmentId(int departmentId, int doctorId, DateTime date)
        {

            List<TodayListViewModel> lstOfTodayListViewModel = new List<TodayListViewModel>();
            EHMSEntities ent = new EHMSEntities();
            if (departmentId == 0 && doctorId == 0)
            {
                var todaylistviewModel = (from p in ent.OpdPatientDoctorDetails
                                          join q in ent.OpdMasters on p.OpdID equals q.OpdID
                                          where p.PreferDate == date
                                          select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName + " " + (q.MiddleName + " " ?? string.Empty) + q.LastName, Address = q.Address, Age = q.AgeYear }).ToList();
                foreach (var item in todaylistviewModel)
                {
                    lstOfTodayListViewModel.Add(item);
                }
            }
            else
            {

                //var todaylistviewModel = (from p in ent.OpdPatientDoctorDetails
                //                          join q in ent.OpdMaster on p.OpdID equals q.OpdID
                //                          where p.DepartmentID == departmentId && p.DoctorID == doctorId && p.PreferDate==DateTime.Today                         
                //                          select new TodayListViewModel { PatientDoctorId=p.PatientDoctorDetailID,PatinetId=p.OpdID,PatientName=q.FirstName+" "+   (q.MiddleName+  " " ?? string.Empty)+q.LastName,Address=q.Address,Age=q.AgeYear}).ToList();
                var todaylistviewModel = (from p in ent.OpdPatientDoctorDetails
                                          join q in ent.OpdMasters on p.OpdID equals q.OpdID
                                          where p.DepartmentID == departmentId && p.DoctorID == doctorId && p.PreferDate == date
                                          select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName + " " + (q.MiddleName + " " ?? string.Empty) + q.LastName, Address = q.Address, Age = q.AgeYear }).ToList();

                foreach (var item in todaylistviewModel)
                {
                    lstOfTodayListViewModel.Add(item);
                }
            }



            return lstOfTodayListViewModel;

        }

        public int EditCommonTestforMedicalRecords(int id, string shortDecription, string Details)
        {
            //int id = 0;

            EHMSEntities ent = new EHMSEntities();
            var editToSave = ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMRCommonTestId == id).FirstOrDefault();



            editToSave.Details = Details;
            editToSave.ShortDesc = shortDecription;
            editToSave.CreatedBy = Utility.GetCurrentLoginUserId();


            ent.Entry(editToSave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();


            return i;

        }


        //further test edit

        public int EditCommonTestforMedicalRecords(int OpdMrFurtherTestId, string fTestName)
        {
            EHMSEntities ent = new EHMSEntities();
            var editToSave = ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMRFurtherTestId == OpdMrFurtherTestId).FirstOrDefault();
            editToSave.TestName = fTestName;
            editToSave.CreatedBy = Utility.GetCurrentLoginUserId();
            ent.Entry(editToSave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            return i;


        }


        public int EditMedicineRefDoses(string Mdnamecls, int Dosescls, int DosesTimecls, int DosesDayclsspan, int OpdMrMedRefCls)
        {
            EHMSEntities ent = new EHMSEntities();
            var editToSave = ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMRMedicineReferId == OpdMrMedRefCls).FirstOrDefault();
            editToSave.MedicineName = Mdnamecls;
            editToSave.Doses = Dosescls.ToString();
            editToSave.DosesTime = DosesTimecls.ToString();
            editToSave.DosesDay = DosesTimecls.ToString();
            editToSave.CreatedBy = Utility.GetCurrentLoginUserId();
            ent.Entry(editToSave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            return i;
        }


        public List<TodayListViewModel> GetlistByPatientId(int patientId, DateTime date)
        {

            EHMSEntities ent = new EHMSEntities();
            List<TodayListViewModel> todayList = new List<TodayListViewModel>();

            DateTime todayDate = DateTime.Today;
            //(FullName.AgentMiddleName + " " ?? string.Empty)
            var details = (from p in ent.OpdPatientDoctorDetails
                           join q in ent.OpdMasters on p.OpdID equals q.OpdID
                           where p.OpdID == patientId && p.PreferDate == date
                           orderby p.PatientDoctorDetailID
                           select new TodayListViewModel { PatientDoctorId = p.PatientDoctorDetailID, PatinetId = p.OpdID, PatientName = q.FirstName + " " + (q.MiddleName + " " ?? string.Empty) + q.LastName, Address = q.Address, Age = q.AgeYear, DoctorId = p.DoctorID, DepartmentId = p.DepartmentID }).FirstOrDefault();

            todayList.Add(details);
            return todayList;

        }



        public int Insert(OpdMedicalRecordModel model)
        {
            int i = 0;
            //for furthertestName, get the name furthetest from FurtherTestModel
            string furtherTestNameFromModel = model.FurtherTestModel.TestName;

            //for medicineName,get the name of medicine name from MedicineReferModel

            string medicineName = model.MedicineReferModel.MedicineName;
            string does = model.MedicineReferModel.Doses;
            string doestimes = model.MedicineReferModel.DosesTime;
            string doesday = model.MedicineReferModel.DosesDay;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objTosaveMR = AutoMapper.Mapper.Map<OpdMedicalRecordModel, OpdMedicalRecordMaster>(model);
                objTosaveMR.CreatedBy = Utility.GetCurrentLoginUserId();
                objTosaveMR.CreatedDate = DateTime.Now;
                objTosaveMR.Status = true;
                objTosaveMR.DepartmentId = (int)model.TodayListViewModel.DepartmentId;
                objTosaveMR.DoctorId = (int)model.TodayListViewModel.DoctorId;
                objTosaveMR.PatientId = (int)model.TodayListViewModel.PatinetId;
                objTosaveMR.VisitedDate = DateTime.Today;
                objTosaveMR.Status = true;
                objTosaveMR.PatientLogId = Utility.patientlogid(model.refOfVitalForOthersModel.OpdId);

                ent.OpdMedicalRecordMasters.Add(objTosaveMR);

                //common test from CommonTestModel(this is a reference of OpdMedicalRecordCommonTestModel)

                var objtoCommonTest = AutoMapper.Mapper.Map<OpdMedicalRecordCommonTestModel, OpdMedicalRecordCommonTest>(model.CommonTestModel);


                objtoCommonTest.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                //objToCommonCase.PatientID = model.PatientId;
                objtoCommonTest.PatientID = model.TodayListViewModel.PatinetId;
                objtoCommonTest.CreatedBy = Utility.GetCurrentLoginUserId();
                objtoCommonTest.CreatedDate = DateTime.Now;
                objtoCommonTest.InsertedDate = DateTime.Now;
                objtoCommonTest.ShortDesc = model.CommonTestModel.ShortDesc;
                objtoCommonTest.Details = model.CommonTestModel.Details;
                objtoCommonTest.Status = true;
                ent.OpdMedicalRecordCommonTests.Add(objtoCommonTest);


                //common test from PatientCommontestList


                foreach (var item in model.PatientCommonTestList)
                {
                    model.CommonTestModel = new OpdMedicalRecordCommonTestModel();
                    var objToCommonCase = AutoMapper.Mapper.Map<OpdMedicalRecordCommonTestModel, OpdMedicalRecordCommonTest>(model.CommonTestModel);
                    objToCommonCase.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                    //objToCommonCase.PatientID = model.PatientId;
                    objToCommonCase.PatientID = model.TodayListViewModel.PatinetId;
                    objToCommonCase.CreatedBy = Utility.GetCurrentLoginUserId();
                    objToCommonCase.CreatedDate = DateTime.Now;
                    objToCommonCase.InsertedDate = DateTime.Now;
                    objToCommonCase.ShortDesc = item.ShortDesc;
                    objToCommonCase.Details = item.Details;
                    objToCommonCase.Status = true;
                    ent.OpdMedicalRecordCommonTests.Add(objToCommonCase);


                }



                //further test from FurtherTestModel 


                model.FurtherTestModel = new OpdMedicalRecordFurtherTestModel();
                var objToFurtherTestForFurtherTestModel = AutoMapper.Mapper.Map<OpdMedicalRecordFurtherTestModel, OpdMedicalRecordFurtherTest>(model.FurtherTestModel);
                objToFurtherTestForFurtherTestModel.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                objToFurtherTestForFurtherTestModel.PatientId = (int)model.TodayListViewModel.PatinetId;
                objToFurtherTestForFurtherTestModel.TestName = furtherTestNameFromModel;
                objToFurtherTestForFurtherTestModel.CreatedBy = Utility.GetCurrentLoginUserId();
                objToFurtherTestForFurtherTestModel.CreatedDate = DateTime.Now;
                objToFurtherTestForFurtherTestModel.Status = true;
                ent.OpdMedicalRecordFurtherTests.Add(objToFurtherTestForFurtherTestModel);



                //further test from Further test list
                foreach (var item in model.PatientFurtherTestList)
                {
                    model.FurtherTestModel = new OpdMedicalRecordFurtherTestModel();
                    var objToFurtherTest = AutoMapper.Mapper.Map<OpdMedicalRecordFurtherTestModel, OpdMedicalRecordFurtherTest>(model.FurtherTestModel);
                    objToFurtherTest.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                    objToFurtherTest.PatientId = (int)model.TodayListViewModel.PatinetId;
                    objToFurtherTest.TestName = item.TestName;
                    objToFurtherTest.CreatedBy = Utility.GetCurrentLoginUserId();
                    objToFurtherTest.CreatedDate = DateTime.Now;
                    objToFurtherTest.Status = true;
                    ent.OpdMedicalRecordFurtherTests.Add(objToFurtherTest);
                }

                //medicine doses from MedicineReferenceModel

                model.MedicineReferModel = new OpdMedicalRecordMedicineReferModel();
                var objToMDForMedicineReferenceModel = AutoMapper.Mapper.Map<OpdMedicalRecordMedicineReferModel, OpdMedicalRecordMedicineRefer>(model.MedicineReferModel);
                objToMDForMedicineReferenceModel.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                objToMDForMedicineReferenceModel.PatientId = (int)model.TodayListViewModel.PatinetId;
                objToMDForMedicineReferenceModel.MedicineName = medicineName;
                objToMDForMedicineReferenceModel.Doses = does;
                objToMDForMedicineReferenceModel.DosesDay = doesday;
                objToMDForMedicineReferenceModel.DosesTime = doestimes;
                objToMDForMedicineReferenceModel.CreatedBy = Utility.GetCurrentLoginUserId();
                objToMDForMedicineReferenceModel.CreatedDate = DateTime.Now;
                objToMDForMedicineReferenceModel.Status = true;
                ent.OpdMedicalRecordMedicineRefers.Add(objToMDForMedicineReferenceModel);


                //medicine doses from model.PatientMedicieDoessList

                foreach (var item in model.PatientMedicineDosesList)
                {
                    model.MedicineReferModel = new OpdMedicalRecordMedicineReferModel();
                    var objToMedicineDoses = AutoMapper.Mapper.Map<OpdMedicalRecordMedicineReferModel, OpdMedicalRecordMedicineRefer>(model.MedicineReferModel);
                    objToMedicineDoses.OpdMedicalRecordMastetId = objTosaveMR.OpdMedicalRecordMastetId;
                    objToMedicineDoses.PatientId = (int)model.TodayListViewModel.PatinetId;
                    objToMedicineDoses.MedicineName = item.MedicineName;
                    objToMedicineDoses.Doses = item.Doses;
                    objToMedicineDoses.DosesDay = item.DosesDay;
                    objToMedicineDoses.DosesTime = item.DosesTime;
                    objToMedicineDoses.CreatedBy = Utility.GetCurrentLoginUserId();
                    objToMedicineDoses.CreatedDate = DateTime.Now;
                    objToMedicineDoses.Status = true;
                    ent.OpdMedicalRecordMedicineRefers.Add(objToMedicineDoses);
                }


                i = ent.SaveChanges();





            }
            return i;

        }

        public List<OpdMedicalRecordListViewModel> GetInsertedMedicalRecordList()
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdMedicalRecordListViewModel> LvModel = new List<OpdMedicalRecordListViewModel>();

            DateTime todayDate = DateTime.Today;

            var details = (from p in ent.OpdMedicalRecordMasters
                           join q in ent.OpdMasters on p.PatientId equals q.OpdID
                           orderby p.CreatedDate
                           select new OpdMedicalRecordListViewModel { OpdMedicalRecordMastetId = p.OpdMedicalRecordMastetId, OpdId = p.PatientId, PatientFullName = ((q.FirstName ?? "") + (q.MiddleName ?? "") + (q.LastName ?? "")), PatientAddress = q.Address, PatientAge = q.AgeYear }).Take(5).ToList();

            foreach (var item in details)
            {
                LvModel.Add(item);
            }

            return LvModel;
        }


        public int AddMoreMedicicalDoes(OpdMedicalRecordModel model)
        {
            int i = 0;

            EHMSEntities ent = new EHMSEntities();
            model.MedicineReferModel = new OpdMedicalRecordMedicineReferModel();
            var objToMedicineDoses = AutoMapper.Mapper.Map<OpdMedicalRecordMedicineReferModel, OpdMedicalRecordMedicineRefer>(model.MedicineReferModel);
            foreach (var item in model.PatientMedicineDosesList)
            {
                objToMedicineDoses.OpdMedicalRecordMastetId = model.OpdMedicalRecordMastetId;
                objToMedicineDoses.PatientId = model.PatientId;
                objToMedicineDoses.MedicineName = item.MedicineName;
                objToMedicineDoses.Doses = item.Doses;
                objToMedicineDoses.DosesDay = item.DosesDay;
                objToMedicineDoses.DosesTime = item.DosesTime;
                objToMedicineDoses.CreatedDate = DateTime.Now;
                objToMedicineDoses.CreatedBy = Utility.GetCurrentLoginUserId();
                objToMedicineDoses.Status = true;
                ent.OpdMedicalRecordMedicineRefers.Add(objToMedicineDoses);
                i = ent.SaveChanges();

            }


            return i;


        }

        public int AddmoreFurtherTestFormView(OpdMedicalRecordModel model)
        {

            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            model.FurtherTestModel = new OpdMedicalRecordFurtherTestModel();
            var objToFurtherTest = AutoMapper.Mapper.Map<OpdMedicalRecordFurtherTestModel, OpdMedicalRecordFurtherTest>(model.FurtherTestModel);
            foreach (var item in model.PatientFurtherTestList)
            {
                objToFurtherTest.OpdMedicalRecordMastetId = model.OpdMedicalRecordMastetId;
                objToFurtherTest.PatientId = model.PatientId;
                objToFurtherTest.TestName = item.TestName;
                objToFurtherTest.CreatedDate = DateTime.Now;
                objToFurtherTest.CreatedBy = Utility.GetCurrentLoginUserId();
                objToFurtherTest.Status = true;
                ent.OpdMedicalRecordFurtherTests.Add(objToFurtherTest);
                i = ent.SaveChanges();



            }



            return i;


        }



        public int AddmoreCommonTest(OpdMedicalRecordModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            OpdMedicalRecordModel obj = new OpdMedicalRecordModel();
            obj.CommonTestModel = new OpdMedicalRecordCommonTestModel();
            var objToCommonCase = AutoMapper.Mapper.Map<OpdMedicalRecordCommonTestModel, OpdMedicalRecordCommonTest>(obj.CommonTestModel);
            foreach (var item in model.PatientCommonTestList)
            {

                objToCommonCase.OpdMedicalRecordMastetId = model.OpdMedicalRecordMastetId;

                objToCommonCase.PatientID = model.PatientId;
                objToCommonCase.ShortDesc = item.ShortDesc;
                objToCommonCase.Details = item.Details;
                objToCommonCase.CreatedBy = Utility.GetCurrentLoginUserId();
                objToCommonCase.CreatedDate = DateTime.Now;
                objToCommonCase.InsertedDate = DateTime.Now;


                objToCommonCase.Status = true;
                ent.OpdMedicalRecordCommonTests.Add(objToCommonCase);
                i = ent.SaveChanges();

            }


            return i;


        }


        public List<OpdMedicalRecordCommonTestModel> GetPatientCommonTestById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdMedicalRecordCommonTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordCommonTest>, IEnumerable<OpdMedicalRecordCommonTestModel>>(ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMedicalRecordMastetId == id)));
            }
        }

        public List<OpdMedicalRecordFurtherTestModel> GetPatientFurtherTestById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdMedicalRecordFurtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordFurtherTest>, IEnumerable<OpdMedicalRecordFurtherTestModel>>(ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMedicalRecordMastetId == id)));
            }
        }

        public List<OpdMedicalRecordMedicineReferModel> GetPatientMedicineReferById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdMedicalRecordMedicineReferModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordMedicineRefer>, IEnumerable<OpdMedicalRecordMedicineReferModel>>(ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMedicalRecordMastetId == id)));
            }
        }
        public List<OpdMedicalRecordModel> GetPatientMedicalRecordList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdMedicalRecordModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordMaster>, IEnumerable<OpdMedicalRecordModel>>(ent.OpdMedicalRecordMasters)).Take(20).ToList();
            }
        }




    }
}