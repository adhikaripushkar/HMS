using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class OpdMedicalRecordController : Controller
    {
        //
        // GET: /OpdMedicalRecord/

        OpdMedicalRecordsProviders mrpro = new OpdMedicalRecordsProviders();

        public ActionResult Index(string Default, string DefaultDoc)
        {

            EHMSEntities ent = new EHMSEntities();
            int DepartmentId = 0041;//static value
            int DoctorId = 0051;//static value

            if (!String.IsNullOrEmpty(Default))
            {
                DepartmentId = Convert.ToInt32(Default);
            }

            if (!String.IsNullOrEmpty(DefaultDoc))
            {
                DoctorId = Convert.ToInt32(DefaultDoc);
            }

            TodayListViewModel model = new TodayListViewModel();



            model.todayList = mrpro.GetlistBySearchWord(DepartmentId, DoctorId);
            model.PrefDate = DateTime.Now;
            foreach (var item in model.todayList)
            {

                int i = ent.OpdMedicalRecordMasters.Where(x => x.PatientId == item.PatinetId && x.PatientDoctorDetailID == item.PatientDoctorId).Count();
                item.count = i;
                if (i >= 1)
                {
                    var data = ent.OpdMedicalRecordMasters.Where(x => x.PatientId == item.PatinetId && x.PatientDoctorDetailID == item.PatientDoctorId).FirstOrDefault();
                    item.PatientlogId = Convert.ToInt32(data.PatientLogId);
                    item.OpdMedicalRecordMasterId = data.OpdMedicalRecordMastetId;
                }
            }
            return View(model);
        }

        public ActionResult OpdMedRepWithDeptIdNDocId(int deptid, int docid, DateTime date)
        {
            EHMSEntities ent = new EHMSEntities();
            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();
            TodayListViewModel model = new TodayListViewModel();
            model.DoctorId = docid;
            model.DepartmentId = deptid;
            model.PrefDate = date;
            model.todayList = pro.getOpdMRdetailwithDoctorIdandDepartmentId(deptid, docid, date);

            foreach (var item in model.todayList)
            {

                int i = ent.OpdMedicalRecordMasters.Where(x => x.PatientId == item.PatinetId && x.PatientDoctorDetailID == item.PatientDoctorId).Count();
                item.count = i;
                if (i >= 1)
                {
                    var data = ent.OpdMedicalRecordMasters.Where(x => x.PatientId == item.PatinetId && x.PatientDoctorDetailID == item.PatientDoctorId).FirstOrDefault();
                    item.PatientlogId = Convert.ToInt32(data.PatientLogId);
                    item.OpdMedicalRecordMasterId = data.OpdMedicalRecordMastetId;
                }

            }

            return PartialView(model);


        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult WriteRecord(int id, DateTime date)
        {
            OpdMedicalRecordModel model = new OpdMedicalRecordModel();
            model.refOfVitalForOthersModel = new VitalForOthersModel();



            model.refOfVitalForOthersModel.OpdId = id;
            using (EHMSEntities ent = new EHMSEntities())
            {

                int patientlogid = Utility.GetoldPatientLogid(id);

                var VitalData = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientlogid).FirstOrDefault();

                var datainmodel = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(VitalData);

                model.refOfVitalForOthersModel = datainmodel;



            }

            model.todayList = mrpro.GetlistByPatientId(id, date);



            model.TodayListViewModel = mrpro.GetlistByPatientId(id, date).FirstOrDefault();

            model.PatientDoctorDetailID = (int)model.TodayListViewModel.PatientDoctorId;

            int? DepatId = model.TodayListViewModel.DepartmentId;


            return View(model);

        }


        [HttpPost]
        public ActionResult WriteRecord(OpdMedicalRecordModel model)
        {

            //foreach (var item in ModelState)
            //{

            //}

            // if (ModelState.IsValid)
            // {
            mrpro.Insert(model);
            // }

            return RedirectToAction("Index");
        }


        public ActionResult ViewOpdMedicalRecord(int id, int docid, int deptid, int patientLogId, int OpdmedRecmasid)
        {
            OpdMedicalRecordModel obj = new OpdMedicalRecordModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var VitalData = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientLogId).FirstOrDefault();

                var datainmodel = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(VitalData);

                obj.refOfVitalForOthersModel = datainmodel;

                obj.PatientLogId = patientLogId;
                obj.PatientId = id;
                obj.DoctorId = docid;
                obj.DepartmentId = deptid;
                obj.OpdMedicalRecordMastetId = OpdmedRecmasid;
                obj.CurrentCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid).Select(x => x.CurrentCase).FirstOrDefault();
                obj.PreviousCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid).Select(x => x.PreviousCase).FirstOrDefault();
                obj.ProvisionalDiagnosis = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid).Select(x => x.ProvisionalDiagnosis).FirstOrDefault();

                // var obj =ent.OpdMedicalRecordFurtherTest.Where(x => x.OpdMedicalRecordMastetId == MedicalReportMasterId && x.PatientId == id).ToList();

                obj.PatientFurtherTestList = new List<OpdMedicalRecordFurtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordFurtherTest>, IEnumerable<OpdMedicalRecordFurtherTestModel>>(ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid && x.PatientId == id).ToList()));
                obj.PatientCommonTestList = new List<OpdMedicalRecordCommonTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordCommonTest>, IEnumerable<OpdMedicalRecordCommonTestModel>>(ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid && x.PatientID == id).ToList()));
                obj.PatientMedicineDosesList = new List<OpdMedicalRecordMedicineReferModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordMedicineRefer>, IEnumerable<OpdMedicalRecordMedicineReferModel>>(ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMedicalRecordMastetId == OpdmedRecmasid && x.PatientId == id).ToList()));

            }

            return View(obj);

        }
        public ActionResult OpdMedicalReportPrint(int id, int docid, int deptid, int patientlogid, int opdMedMasid)
        {
            OpdMedicalRecordModel obj = new OpdMedicalRecordModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                // int patientlogid = Utility.GetoldPatientLogid(id);

                var VitalData = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientlogid).FirstOrDefault();

                var datainmodel = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(VitalData);

                obj.refOfVitalForOthersModel = datainmodel;

                //  int MedicalReportMasterId = Utility.GetOpdMedicalReportMasterId(id, docid, deptid);

                obj.PatientLogId = patientlogid;
                obj.PatientId = id;
                obj.DoctorId = docid;
                obj.DepartmentId = deptid;
                obj.OpdMedicalRecordMastetId = opdMedMasid;
                obj.CurrentCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid).Select(x => x.CurrentCase).FirstOrDefault();
                obj.PreviousCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid).Select(x => x.PreviousCase).FirstOrDefault();
                obj.ProvisionalDiagnosis = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid).Select(x => x.ProvisionalDiagnosis).FirstOrDefault();

                // var obj =ent.OpdMedicalRecordFurtherTest.Where(x => x.OpdMedicalRecordMastetId == MedicalReportMasterId && x.PatientId == id).ToList();

                obj.PatientFurtherTestList = new List<OpdMedicalRecordFurtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordFurtherTest>, IEnumerable<OpdMedicalRecordFurtherTestModel>>(ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid && x.PatientId == id).ToList()));
                obj.PatientCommonTestList = new List<OpdMedicalRecordCommonTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordCommonTest>, IEnumerable<OpdMedicalRecordCommonTestModel>>(ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid && x.PatientID == id).ToList()));
                obj.PatientMedicineDosesList = new List<OpdMedicalRecordMedicineReferModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordMedicineRefer>, IEnumerable<OpdMedicalRecordMedicineReferModel>>(ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMedicalRecordMastetId == opdMedMasid && x.PatientId == id).ToList()));
            }


            return View(obj);


        }


        public ActionResult OpdMedRecrdDataforCommonTest(int commtestId, string shortDes, string Detail)
        {
            string str = "";
            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();

            int i = pro.EditCommonTestforMedicalRecords(commtestId, shortDes, Detail);
            if (i != 0)
            {

                str = "Edit Common Test Success";
            }

            return Json(str, JsonRequestBehavior.AllowGet);

        }




        public ActionResult OpdMedRecrdDataforFurtherTest(int OpdMrFurtherTestId, string fTestName)
        {

            string str = "";

            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();

            int i = pro.EditCommonTestforMedicalRecords(OpdMrFurtherTestId, fTestName);

            if (i != 0)
            {

                str = "Edit Further Test Success";
            }

            return Json(str, JsonRequestBehavior.AllowGet);


        }

        public ActionResult OpdMedRecrdDataMedicineDoses(string Mdnamecls, int Dosescls, int DosesTimecls, int DosesDayclsspan, int OpdMrMedRefCls)
        {
            string str = "";

            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();
            int i = pro.EditMedicineRefDoses(Mdnamecls, Dosescls, DosesTimecls, DosesDayclsspan, OpdMrMedRefCls);
            if (i != 0)
            {
                str = "Medicine Records Updated Success";
            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }


        public ActionResult deleteMedicalDoes(int id)
        {
            string str = "";
            EHMSEntities ent = new EHMSEntities();
            var delobj = ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMRMedicineReferId == id).FirstOrDefault();
            ent.OpdMedicalRecordMedicineRefers.Remove(delobj);
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Delete Success Medicine Doeses Record";
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }


        public ActionResult deleteFurtherTest(int id)
        {

            string str = "";
            EHMSEntities ent = new EHMSEntities();
            var delobj = ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMRFurtherTestId == id).FirstOrDefault();
            ent.OpdMedicalRecordFurtherTests.Remove(delobj);
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Delete Success Further Test Record";
            }
            return Json(str, JsonRequestBehavior.AllowGet);

        }

        public ActionResult deleteCommonTest(int id)
        {

            string str = "";
            EHMSEntities ent = new EHMSEntities();
            var delobj = ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMRCommonTestId == id).FirstOrDefault();
            ent.OpdMedicalRecordCommonTests.Remove(delobj);
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Delete Success Common Test Record";
            }
            return Json(str, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FormForFurtherTest(int Deptid, int patientid, int docid, int masterid)
        {
            var omrm = new OpdMedicalRecordModel();
            omrm.PatientId = patientid;
            omrm.DepartmentId = Deptid;
            omrm.DoctorId = docid;
            omrm.OpdMedicalRecordMastetId = masterid;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
                omrm.PatientLogId = Convert.ToInt32(data.PatientLogId);
            }
            omrm.PatientFurtherTestList.Insert(0, new OpdMedicalRecordFurtherTestModel());
            return PartialView(omrm);
        }

        public ActionResult CommontestRecord(int Deptid, int patientid, int docid, int masterid)
        {

            var omrm = new OpdMedicalRecordModel();
            omrm.PatientId = patientid;
            omrm.DepartmentId = Deptid;
            omrm.DoctorId = docid;
            omrm.OpdMedicalRecordMastetId = masterid;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
                omrm.PatientLogId = Convert.ToInt32(data.PatientLogId);
            }

            omrm.PatientCommonTestList.Insert(0, new OpdMedicalRecordCommonTestModel());
            return PartialView(omrm);

        }


        public ActionResult FormForMedicineRecords(int Deptid, int patientid, int docid, int masterid)
        {


            var omrm = new OpdMedicalRecordModel();
            omrm.PatientId = patientid;
            omrm.DepartmentId = Deptid;
            omrm.DoctorId = docid;
            omrm.OpdMedicalRecordMastetId = masterid;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
                omrm.PatientLogId = Convert.ToInt32(data.PatientLogId);
            }
            omrm.PatientMedicineDosesList.Insert(0, new OpdMedicalRecordMedicineReferModel());
            return PartialView(omrm);

        }




        public ActionResult GetDataFromMedicine(OpdMedicalRecordModel model)
        {
            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();

            int i = pro.AddMoreMedicicalDoes(model);
            if (i != 0)
            {

                return RedirectToAction("ViewOpdMedicalRecord", new { id = model.PatientId, docid = model.DoctorId, deptid = model.DepartmentId, patientLogId = model.PatientLogId, OpdmedRecmasid = model.OpdMedicalRecordMastetId });

            }

            return View();

        }
        public ActionResult GetDataFromFurtherTest(OpdMedicalRecordModel model)
        {
            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();
            int i = pro.AddmoreFurtherTestFormView(model);
            if (i != 0)
            {
                return RedirectToAction("ViewOpdMedicalRecord", new { id = model.PatientId, docid = model.DoctorId, deptid = model.DepartmentId, patientLogId = model.PatientLogId, OpdmedRecmasid = model.OpdMedicalRecordMastetId });
            }

            return View();


        }

        public ActionResult GetDataFormForCommonTest(OpdMedicalRecordModel model)
        {
            OpdMedicalRecordsProviders pro = new OpdMedicalRecordsProviders();
            int i = pro.AddmoreCommonTest(model);
            if (i != 0)
            {

                return RedirectToAction("ViewOpdMedicalRecord", new { id = model.PatientId, docid = model.DoctorId, deptid = model.DepartmentId, patientLogId = model.PatientLogId, OpdmedRecmasid = model.OpdMedicalRecordMastetId });

            }

            return View();

        }


        [HttpPost]
        public ActionResult AddPatientCommonTest()
        {
            OpdMedicalRecordCommonTestModel model = new OpdMedicalRecordCommonTestModel();
            return PartialView("_AddCommonTest", model);
        }
        [HttpPost]
        public ActionResult AddPatientFurtherTest()
        {
            OpdMedicalRecordFurtherTestModel model = new OpdMedicalRecordFurtherTestModel();
            return PartialView("_AddFurtherTest", model);
        }
        [HttpPost]
        public ActionResult AddPatientMedicineDose()
        {
            OpdMedicalRecordMedicineReferModel model = new OpdMedicalRecordMedicineReferModel();
            return PartialView("_AddMedicineDoseTest", model);
        }


        public ActionResult SearchPatient()
        {
            OpdMedicalRecordModel model = new OpdMedicalRecordModel();
            model.OpdMedicalRecordListViewModelList = mrpro.GetInsertedMedicalRecordList();
            return View(model);
        }


        public ActionResult ViewDetails(int id)
        {
            OpdMedicalRecordModel model = new OpdMedicalRecordModel();
            model = mrpro.GetPatientMedicalRecordList().Where(x => x.OpdMedicalRecordMastetId == id).FirstOrDefault();
            model.PatientCommonTestList = mrpro.GetPatientCommonTestById(id);
            model.PatientFurtherTestList = mrpro.GetPatientFurtherTestById(id);
            model.PatientMedicineDosesList = mrpro.GetPatientMedicineReferById(id);

            return View(model);

        }


        public ActionResult EditPreviousCase(string PrevCaseVal, int masterid)
        {
            string str = "";
            PrevCaseVal = PrevCaseVal.Replace("\n", " ");
            EHMSEntities ent = new EHMSEntities();

            var objTosave = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
            objTosave.PreviousCase = PrevCaseVal;
            ent.Entry(objTosave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Edit Success Of Previous Case Records";
            }
            return Json(str, JsonRequestBehavior.AllowGet);


        }

        public ActionResult EditCurrentCase(string CrntCaseVal, int masterid)
        {

            string str = "";
            CrntCaseVal = CrntCaseVal.Replace("\n", " ");
            EHMSEntities ent = new EHMSEntities();

            var objTosave = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
            objTosave.CurrentCase = CrntCaseVal;
            ent.Entry(objTosave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Edit Success Of Current Case Records";
            }
            return Json(str, JsonRequestBehavior.AllowGet);


        }

        public ActionResult EditProvDigCase(string ProvDigVal, int masterid)
        {

            string str = "";
            ProvDigVal = ProvDigVal.Replace("\n", " ");
            EHMSEntities ent = new EHMSEntities();

            var objTosave = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == masterid).FirstOrDefault();
            objTosave.ProvisionalDiagnosis = ProvDigVal;
            ent.Entry(objTosave).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            if (i != 0)
            {
                str = "Edit Success Of Provisional Diagnosis Records";
            }
            return Json(str, JsonRequestBehavior.AllowGet);


        }

        public ActionResult OldPatientMedicalRecords()
        {

            opdOldPatientMedicalRecords model = new opdOldPatientMedicalRecords();
            model.lstOfOpdOldPatientMedicalRecords = new List<opdOldPatientMedicalRecords>();
            return View(model);

        }

        [HttpPost]
        public ActionResult OldPatientMedicalRecords(opdOldPatientMedicalRecords model)
        {
            opdOldPatientMedicalRecords obj = new opdOldPatientMedicalRecords();
            obj.lstOfOpdOldPatientMedicalRecords = model.GetOldPatientMedicalRecordsWithPatientId(model.OpdId);
            foreach (var item in obj.lstOfOpdOldPatientMedicalRecords)
            {

                obj.OpdId = item.OpdId;

            }
            return PartialView("_OldPatientMedicalRecords", obj);

        }


        public ActionResult OpdMedicalReportPrintForOld(int id, int docid, int deptid, int patientlogid, int opdmedmastid)
        {

            OpdMedicalRecordModel obj = new OpdMedicalRecordModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                //int patientlogid = Utility.GetoldPatientLogid(id);

                var VitalData = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientlogid).FirstOrDefault();

                var datainmodel = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(VitalData);

                obj.refOfVitalForOthersModel = datainmodel;







                // get OpdMedicalReportMasterId



                // int MedicalReportMasterId = Utility.GetOpdMedicalReportMasterId(id, docid, deptid);


                obj.PatientId = id;
                obj.DoctorId = docid;
                obj.DepartmentId = deptid;
                obj.OpdMedicalRecordMastetId = opdmedmastid;
                obj.CurrentCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid).Select(x => x.CurrentCase).FirstOrDefault();
                obj.PreviousCase = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid).Select(x => x.PreviousCase).FirstOrDefault();
                obj.ProvisionalDiagnosis = ent.OpdMedicalRecordMasters.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid).Select(x => x.ProvisionalDiagnosis).FirstOrDefault();

                // var obj =ent.OpdMedicalRecordFurtherTest.Where(x => x.OpdMedicalRecordMastetId == MedicalReportMasterId && x.PatientId == id).ToList();

                obj.PatientFurtherTestList = new List<OpdMedicalRecordFurtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordFurtherTest>, IEnumerable<OpdMedicalRecordFurtherTestModel>>(ent.OpdMedicalRecordFurtherTests.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid && x.PatientId == id).ToList()));
                obj.PatientCommonTestList = new List<OpdMedicalRecordCommonTestModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordCommonTest>, IEnumerable<OpdMedicalRecordCommonTestModel>>(ent.OpdMedicalRecordCommonTests.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid && x.PatientID == id).ToList()));
                obj.PatientMedicineDosesList = new List<OpdMedicalRecordMedicineReferModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMedicalRecordMedicineRefer>, IEnumerable<OpdMedicalRecordMedicineReferModel>>(ent.OpdMedicalRecordMedicineRefers.Where(x => x.OpdMedicalRecordMastetId == opdmedmastid && x.PatientId == id).ToList()));

            }

            return PartialView(obj);


        }



    }
}
