using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class IpdMedicalRecordController : Controller
    {
        //
        // GET: /IpdMedicalRecord/
        IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
        IpdRegistrationMasterProvider IpdPro = new IpdRegistrationMasterProvider();
        public ActionResult Index()
        {
            return View();



        }
        public ActionResult AddIpdMRCommonTest(int id)
        {

            return View();
        }
        public ActionResult AddIpdMrMedical(int id, int ipdid)
        {
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            IpdMedicalRecord model = new IpdMedicalRecord();
            model.IpdMedicalRecordDataList = pro.IpdMedicalRecordData(id, ipdid);
            model.IpdMedicalRecordValue = pro.IpdMedicalRecordValue(id, ipdid);
            model.IpdRegisterationID = ipdid;
            model.PatientID = id;
            model.InsertedDate = DateTime.Today;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddIpdMrMedical(IpdMedicalRecord model)
        {
            foreach (var item in model.IpdMedicalRecordList)
            {
                if (item.Doses == null || item.DosesTimes == null || item.DosesTotalDays == null || item.MedicineName == null || model.PatientID == null || model.IpdRegisterationID == null)
                {
                    TempData["a"] = 10;
                    return RedirectToAction("AddIpdMrMedical", "IpdMedicalRecord", new { id = model.PatientID, ipdid = model.IpdRegisterationID });
                }
            }
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            int i = pro.Insert(model);


            return RedirectToAction("AddIpdMrMedical", "IpdMedicalRecord", new { id = model.PatientID, ipdid = model.IpdRegisterationID });
        }
        public ActionResult _MedicineEntry()
        {

            IpdMedicalRecord models = new IpdMedicalRecord();
            return PartialView("_MedicineEntry", models);

        }
        public ActionResult Edit(int id)
        {
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            IpdMedicalRecord model = new IpdMedicalRecord();

            model = pro.GetEdit(id).FirstOrDefault();

            return View("EditMedicalRecord", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, IpdMedicalRecord model)
        {
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();

            int i = pro.Update(model);

            return RedirectToAction("AddIpdMrMedical", new { id = model.PatientID, ipdid = model.IpdRegisterationID });


        }
        public ActionResult Delete(int id, int opd, int ipd)
        {


            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            int i = pro.Delete(id);

            return RedirectToAction("AddIpdMrMedical", new { id = opd, ipdid = ipd });



        }
        public ActionResult IpdMrMainTest(int id, int ipdid)
        {
            IpdMrMainTestModel model = new IpdMrMainTestModel();
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            model.InsertedDate = DateTime.Today;

            model.PatientID = id;
            model.IpdRegistrationID = ipdid;

            model.IpdMedicalRecordList = pro.IpdMedicalRecordData(id, ipdid);
            model.SectionTestCheckBoxList = pro.SectionCheckListModelList();
            model.IpdMrMainTestModelList = pro.ListSectionCheckBox(id);

            return View(model);

        }
        [HttpPost()]
        public ActionResult IpdMrMainTest(IpdMrMainTestModel model)
        {
            IpdMedicalRecordProvider Pro = new IpdMedicalRecordProvider();
            //if (ModelState.IsValid)
            //{
            int i = Pro.InsertCheckBox(model);
            //}
            return RedirectToAction("IpdMrMainTest", new { id = model.PatientID, ipdid = model.IpdRegistrationID });
        }

        public ActionResult IpdMrSectionTest()
        {
            IpdMrMainTestModel model = new IpdMrMainTestModel();

            return PartialView("IpdMrSectionTest", model);
        }
        public ActionResult DeleteMainTest(int id, int opd, int ipd)
        {


            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            int i = pro.DeleteMainTest(id);

            return RedirectToAction("IpdMrMainTest", new { id = opd, ipdid = ipd });



        }
        public ActionResult EditMainTest(int id, int opd, int ipd)
        {
            IpdMrMainTestModel model = new IpdMrMainTestModel();
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();
            model = pro.EditMainTest(id).FirstOrDefault();

            model.SectionTestCheckBoxList = pro.SectionCheckListModelList();
            SectionTestCheckBox models = new SectionTestCheckBox();
            models.isSelected = model.status;
            model.PatientID = opd;
            model.IpdRegistrationID = ipd;

            return View(model);


        }
        [HttpPost]
        public ActionResult EditMainTest(int id, IpdMrMainTestModel model)
        {
            model.IpdMrMainTestID = id;
            IpdMedicalRecordProvider pro = new IpdMedicalRecordProvider();

            int i = pro.UpdateMainModel(model);

            return RedirectToAction("IpdMrMainTest", new { id = model.PatientID, ipdid = model.IpdRegistrationID });




        }
        public ActionResult IpdMRCommonTestDetailsByID(int id, int opdid)
        {

            EHMSEntities ent = new EHMSEntities();
            // var FirstName = ent.OpdMaster.Where(m => m.OpdID == opdid).Select(m => m.FirstName.Trim()).SingleOrDefault();
            //string name = Convert.ToString(FirstName);
            IpdMRCommonTestModel model = new IpdMRCommonTestModel();
            // ViewBag.data = IpdPro.SearchByPatientName(name);

            ViewBag.data = IpdPro.SearchByPatientNamebyid(id, opdid);
            //ViewBag.data = IpdPro.SearchByPatientName(name).LastOrDefault();

            model.IpdMRCommonTestModeList = pro.GetByIpdRegistrationID(id);
            model.IpdRegistrationID = id;
            model.OpdID = opdid;
            return View(model);
        }



        [HttpPost]
        public ActionResult AddIpdMRCommonTest(IpdMRCommonTestModel model)
        {
            if (model.IpdMRCommonTestModeList.Count > 0)
            {
                foreach (var item in model.IpdMRCommonTestModeList)
                {
                    if (item.CommonTestName == null || item.Details == null || item.IpdMRcCommonTesDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
                    {
                        TempData["Error"] = "0";
                        return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });
                    }
                }
            }
            else if (model.IpdMRCommonTestModeList.Count < 0)
            {
                return null;
            }
            if (ModelState.IsValid)
            {

                pro.Insert(model);
                return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });
            }

            else
            {
                return RedirectToAction("AddIpdMRCommonTest");
            }
        }
        public ActionResult AddMoreIpdCommoTest()
        {
            IpdMRCommonTestModel model = new IpdMRCommonTestModel();

            return PartialView("AddMoreIpdCommoTest", model);
        }
        public ActionResult EditIpdMRCommonTest(int id)
        {
            IpdMRCommonTestModel model = new IpdMRCommonTestModel();
            model.IpdMRCommonTestModeList = pro.GetByIPdMRCommonTest(id);
            foreach (var item in model.IpdMRCommonTestModeList)
            {
                model.IpdMRCommonTestID = id;
                model.CommonTestName = item.CommonTestName;
                model.Details = item.Details;
                model.IpdMRcCommonTesDate = item.IpdMRcCommonTesDate;
                model.OpdID = item.OpdID;
                model.IpdRegistrationID = item.IpdRegistrationID;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult EditIpdMRCommonTest(IpdMRCommonTestModel model)
        {
            if (model.CommonTestName == null || model.Details == null || model.IpdMRcCommonTesDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
            {
                TempData["Error"] = "0";
                return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });
            }
            if (ModelState.IsValid)
            {
                pro.Update(model);
                return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });
            }
            else
            {

                return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });
            }
        }
        public ActionResult DeleteIpdMRCommonTest(int id)
        {

            IpdMRCommonTestModel model = new IpdMRCommonTestModel();
            model.IpdMRCommonTestModeList = pro.GetByIPdMRCommonTest(id);
            foreach (var item in model.IpdMRCommonTestModeList)
            {
                model.IpdRegistrationID = item.IpdRegistrationID;
                model.OpdID = item.OpdID;
            }
            pro.DeleteIpdMRCommonTest(id);
            return RedirectToAction("IpdMRCommonTestDetailsByID", new { id = model.IpdRegistrationID, opdid = model.OpdID });


        }

    }
}

