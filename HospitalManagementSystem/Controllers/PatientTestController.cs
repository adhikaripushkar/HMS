using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System.Data;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class PatientTestController : Controller
    {
        //
        // GET: /PatientTest/
        PatientTestProvider pro = new PatientTestProvider();
        //[CustomAuthorize(Roles = "PathologyAdmin, PathologyCreate,PathologyView,  superadmin, admin")]
        public ActionResult Main()
        {
            return View();
        }
        //[CustomAuthorize(Roles = "PathologyAdmin, PathologyCreate,PathologyView,  superadmin, admin")]
        public ActionResult Index()
        {
            PatientTestModel model = new PatientTestModel();
            model.PatientTestModelList = pro.PopulatePatientTestList(4);
            return View(model);
        }


        public ActionResult GetPatientInformation(string DepartmentID, string ipValue)
        {
            PatientTestModel model = new PatientTestModel();
            model.PatientInformationModel = pro.GetPatientBasicInformation(Convert.ToInt32(ipValue), 4).FirstOrDefault();
            return RedirectToAction("Create", model);
        }
        [HttpPost]
        public ActionResult PatientTest()
        {
            PathoTestModel model = new PathoTestModel();
            return PartialView("_PatientTest", model);
        }
        //[CustomAuthorize(Roles = "PathologyAdmin, PathologyCreate,PathologyView,  superadmin, admin")]
        public ActionResult Create(string ipValue)
        {
            PatientTestModel model = new PatientTestModel();
            EHMSEntities ent = new EHMSEntities();
            //int departmentId = 0;
            //int DropDownListDepartmentId = Convert.ToInt32(deptId);

            //if (DropDownListDepartmentId == 1001)//emergency
            //{
            //    model.DepartmentID = 1001;
            //    if (ipValue != null && ipValue != "" && (pro.CheckEmergencyIdExistOrNot(Convert.ToInt32(ipValue)) != false))
            //    {
            //        model.PatientInformationModel = pro.GetPatientBasicInformationFromEmergency(Convert.ToInt32(ipValue)).FirstOrDefault();
            //    }

            //}
            //else
            //{
            if (ipValue != null && ipValue != "" && (pro.CheckOpdIdExistOrNot(Convert.ToInt32(ipValue)) != false))
            {
                model.PatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipValue), 4).FirstOrDefault();

            }

            // }

            model.PathoSectionModelList = pro.GetPathoSections();
            //added by bibek
            tempList.Clear();
            model.PathoOtherTestModelCBList = pro.GetOtherTestList();
            //model.PathoTestModelList = pro.GetPathoTestSectionWise(1);
            model.PatientID = Convert.ToInt32(ipValue);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PatientTestModel model)
        {
            if (model.PatientInformationModel != null)
            {
                if (ModelState.IsValid)
                {
                    pro.Insert(model);
                }
                model.PathoSectionModelList = pro.GetPathoSections();

                // return RedirectToAction("PrintPatientTest",model);
                return View("PrintPatientTest", model);
            }
            else
            {
                return RedirectToAction("Create");
            }

        }

        public ActionResult PatientTestResult(int id, int PatientTestID, string DeptID)
        {
            PatientTestModel model = new PatientTestModel();
            //string DropDownListDepartmentId = DeptID;

            //if (DropDownListDepartmentId == "Emergency")//emergency
            //{
            //    model.DepartmentID = 1001;
            //    if (id != 0 && (pro.CheckEmergencyIdExistOrNot(Convert.ToInt32(id)) != false))
            //    {
            //        model.PatientInformationModel = pro.GetPatientBasicInformationFromEmergency(Convert.ToInt32(id)).FirstOrDefault();
            //    }

            //}
            //else
            //{
            if (id != 0 && (pro.CheckOpdIdExistOrNot(Convert.ToInt32(id)) != false))
            {
                model.PatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(id), 4).FirstOrDefault();

            }

            //}

            model.TestCheckBoxListModelList = pro.getPatientTestForPrint(PatientTestID);
            model.TotalAmount = pro.getPatientTestPaymentForPrint(PatientTestID).FirstOrDefault().TotalAmount;
            model.Discount = pro.getPatientTestPaymentForPrint(PatientTestID).FirstOrDefault().Discount;
            model.PayableAmount = pro.getPatientTestPaymentForPrint(PatientTestID).FirstOrDefault().PayableAmount;
            return View("PrintPatientTest", model);
        }
        //[CustomAuthorize(Roles = "PathologyAdmin, PathologyCreate, superadmin, admin")]
        public ActionResult Edit(int id, int PatientTestID)
        {
            PatientTestModel model = new PatientTestModel();
            model = pro.PopulatePatientTestMasterForEdit(PatientTestID).FirstOrDefault();
            //department Static 
            model.PatientInformationModel = pro.GetPatientBasicInformationFromOpd(id, 4).FirstOrDefault();
            model.TestCheckBoxListModelList = pro.PopulatePatientTestForEdit(PatientTestID);
            model.PathoSectionModelList = pro.GetPathoSections();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(int id, int PatientTestID, PatientTestModel model)
        {
            if (ModelState.IsValid)
            {
                //edit
                pro.Update(model, id, PatientTestID);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchTestName(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();
            var result = (from r in ent.SetupPathoTests
                          join sc in ent.SetupSections on r.SectionId equals sc.SectionId
                          where (r.TestName).Contains(s)
                          select new { r.TestId, r.TestName, sc.SectionId, sc.Name }).Distinct();
            foreach (var item in result)
            {
                names.Add(item.TestName + "{" + item.Name + "^" + item.TestId + "}");
            }
            return Json(names, JsonRequestBehavior.AllowGet);


        }
        static List<TestCheckBoxListModel> tempList = new List<TestCheckBoxListModel>();
        public ActionResult PopulateTestDetail(string ipValue, string ipTestID, string ipid, string ipDept, string tim, string disin, string amt, string userid)
        {
            PatientTestModel model = new PatientTestModel();
            if (string.IsNullOrWhiteSpace(ipid) == false && string.IsNullOrWhiteSpace(ipDept) == false)
            {
                //int DropDownListDepartmentId = Convert.ToInt32(ipDept);

                //if (DropDownListDepartmentId == 1001)//emergency
                //{
                //    model.DepartmentID = 1001;
                //    if (ipid != null && ipid != "" && (pro.CheckEmergencyIdExistOrNot(Convert.ToInt32(ipid)) != false))
                //    {
                //        model.PatientInformationModel = pro.GetPatientBasicInformationFromEmergency(Convert.ToInt32(ipid)).FirstOrDefault();
                //    }

                //}
                //else
                //{
                if (ipid != null && ipid != "" && (pro.CheckOpdIdExistOrNot(Convert.ToInt32(ipid)) != false))
                {
                    model.PatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();

                }

                //}
            }
            if (string.IsNullOrWhiteSpace(ipValue) == false)
            {
                string Valuenew = ipValue.Trim();
                int BracIndex = ipValue.IndexOf('{') + 1;
                int TotalLen = ipValue.IndexOf('}') - 1;
                int Len = ipValue.Length;
                int toval = Len - BracIndex;
                string value = ipValue.Substring(BracIndex, toval - 1);
                int aa = value.IndexOf('^') + 1;
                int bb = value.Length;
                int lenth = bb - aa;
                string str = value.Substring(aa, lenth);
                foreach (var itemL in tempList)
                {
                    if (ipValue == itemL.TestName)
                    {
                        TempData["failed"] = "Test Name already exists";
                        model.TestCheckBoxListModelList = tempList;
                        return View("Create", model);
                    }
                }
                model.TestCheckBoxListModelList = pro.getTestDetailTestIDWise(str, disin, amt, tim, userid);

                foreach (var item in model.TestCheckBoxListModelList)
                {

                    tempList.Add(item);
                }

            }
            if (string.IsNullOrWhiteSpace(ipTestID) == false && string.IsNullOrWhiteSpace(ipValue) == true)
            {
                //for patientinformation
                if (string.IsNullOrWhiteSpace(ipid) == false && string.IsNullOrWhiteSpace(ipDept) == false)
                {
                    //int DropDownListDepartmentId = Convert.ToInt32(ipDept);

                    //if (DropDownListDepartmentId == 1001)//emergency
                    //{
                    //    model.DepartmentID = 1001;
                    //    if (ipid != null && ipid != "" && (pro.CheckEmergencyIdExistOrNot(Convert.ToInt32(ipid)) != false))
                    //    {
                    //        model.PatientInformationModel = pro.GetPatientBasicInformationFromEmergency(Convert.ToInt32(ipid)).FirstOrDefault();
                    //    }

                    //}
                    //else
                    //{
                    if (ipid != null && ipid != "" && (pro.CheckOpdIdExistOrNot(Convert.ToInt32(ipid)) != false))
                    {
                        model.PatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipid), 4).FirstOrDefault();

                    }

                    // }
                }
                //
                var itemToRemove = tempList.Single(x => x.TestId == Convert.ToInt32(ipTestID));
                tempList.Remove(itemToRemove);
            }
            model.PatientID = (Convert.ToInt32(ipid));
            model.TestCheckBoxListModelList = tempList;
            return View("Create", model);
        }
        public ActionResult BillPaidTest()
        {
            PatientTestModel model = new PatientTestModel();
            //model = pro.getBillPaidTestModelList("Patho");
            model = pro.getBillPaidTestModelListById("Patho", 1006);
            return View(model);

        }
        public ActionResult DetailOfBillPaidTest(int id, string billno, string dept, int BillnumberInt)
        {
            PatientTestModel model = new PatientTestModel();
            model = pro.getDetailBillPaidTestModelList("Patho", billno, id, BillnumberInt);

            return View(model);
        }

        [HttpPost]
        public ActionResult DetailOfBillPaidTest(PatientTestModel model)
        {
            if (ModelState.IsValid)
            {
                //insert into patienttest
                pro.InsertPatientFromDetail(model);
            }
            return RedirectToAction("BillPaidTest");
        }

        public ActionResult SearchPatient()
        {
            return PartialView();
        }

        public ActionResult GetPatientDetailsByName(string id)
        {
            PatientTestModel model = new PatientTestModel();
            model.PatientInformationModelList = pro.getPatientDetailByName(id);
            return PartialView("PatientListView", model);
        }


        public ActionResult SampleCollectedIndex(string LabNubmer)
        {
            PatientTestModel model = new PatientTestModel();
            int LabNumberInt = Convert.ToInt32(0);
            if (!string.IsNullOrEmpty(LabNubmer))
            {
                bool result = int.TryParse(LabNubmer, out LabNumberInt);
                if (result)
                {
                    LabNumberInt = Convert.ToInt32(LabNubmer);
                }

            }

            model.SampleCollectedViewModelList = pro.GetSampleCollectedList(LabNumberInt);
            return View(model);
        }

        public ActionResult DisplayCommission()
        {
            PatientTestModel model = new PatientTestModel();
            model.GetCBCommissionViewModelList = pro.GetCommissionDetailsFromCB();
            return View(model);
        }

        public ActionResult ViewCommissionDetails(int id)
        {
            PatientTestModel model = new PatientTestModel();
            model.GetPathoCommDetailsViewModelList = pro.GetCommissionDetailsByBillNo(id);
            model.ObjGetPathoCommDetailsViewModel.BillNo = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewCommissionDetails(PatientTestModel model)
        {
            pro.InsertPathoCommissionDetails(model);
            //return View(model);
            return RedirectToAction("DisplayCommission");
        }


        [HttpPost]
        public ActionResult AddMoreParticulars()
        {
            AddMoreParticularsViewModel model = new AddMoreParticularsViewModel();

            return PartialView("_AddMoreCommission", model);

        }

        public ActionResult DoctroCommissionIndex()
        {
            PatientTestModel model = new PatientTestModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ShowDoctorCommissionDetails()
        {
            PatientTestModel model = new PatientTestModel();
            model.DoctorCommissionRptViewModelList = pro.GetDoctorCommissionReports();
            return View(model);
        }



    }
}
