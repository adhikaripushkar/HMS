using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class PatientTestResultController : Controller
    {
        //
        // GET: /PatientTestResult/
        PatientTestResultProvider pro = new PatientTestResultProvider();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Create(string ipValue, string did, string tdate, string PatientTestID, string deptId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PatientTestResultModel model = new PatientTestResultModel();
                if (did != "" && ipValue != null)
                {
                    if (did != null || ipValue != "")
                    {
                        int ipValueInt = 0;
                        int PatientDepartmentId = 0;
                        int DropDownDepartmentId = Convert.ToInt32(deptId);
                        if (DropDownDepartmentId != 1001)
                        {
                            if (ipValue != "")
                            {
                                ipValueInt = Convert.ToInt32(ipValue);
                                //get patient department id from opd master
                                PatientDepartmentId = Utility.getPatientDepartmentIdFromOpdMaster(ipValueInt);
                            }
                            model.PopulatePatientTestDetailsModel = pro.GetPatientTestDetailList(ipValueInt, 4).FirstOrDefault();
                            //model.PatientTestResultModelList = pro.GetPatientTest(Convert.ToInt32(ipValue ?? "0"));
                            //model.PathoSectionModelResultList = pro.GetPathoSections();
                            model.PathoSectionModelResultList = pro.GetPathoSectionsbyPatientTest(Convert.ToInt32(PatientTestID ?? "0"));
                            model.PopulatePatientTestDetailModelList = pro.GetPopulatePatientTestDetailModelList(ipValueInt, PatientDepartmentId, did);
                        }
                        else//This is for emergency Case
                        {
                            if (ipValue != "")
                            {
                                ipValueInt = Convert.ToInt32(ipValue);
                                PatientDepartmentId = 10;

                            }
                            model.PopulatePatientTestDetailsModel = pro.GetPatientTestDetailListForEmerGency(ipValueInt).FirstOrDefault();
                            //model.PatientTestResultModelList = pro.GetPatientTest(Convert.ToInt32(ipValue ?? "0"));
                            //model.PathoSectionModelResultList = pro.GetPathoSections();
                            model.PathoSectionModelResultList = pro.GetPathoSectionsbyPatientTest(Convert.ToInt32(PatientTestID ?? "0"));
                            model.PopulatePatientTestDetailModelList = pro.GetPopulatePatientTestDetailModelList(ipValueInt, PatientDepartmentId, did);


                        }
                    }
                    if (tdate != "" && tdate != null)
                    {
                        int ipValueInt = Convert.ToInt32(ipValue);
                        //get patient department id from opd master
                        int PatientDepartmentId = Utility.getPatientDepartmentIdFromOpdMaster(ipValueInt);
                        model.PathoSectionModelResultList = pro.GetPathoSectionsbyPatientTest(Convert.ToInt32(PatientTestID ?? "0"));
                        model.PopulatePatientTestDetailModelList = pro.GetPopulatePatientTestDetailModelList(ipValueInt, PatientDepartmentId, did);
                        model.PatientTestResultModelList = pro.GetPatientTest(ipValueInt, Convert.ToDateTime(tdate), Convert.ToInt32(PatientTestID ?? "0"));
                    }
                }
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Create(PatientTestResultModel model)
        {
            if (ModelState.IsValid)
            {
                pro.Insert(model);
            }
            return View(model);
        }

        public ActionResult PatientReport(string fDate, string tDate, string pid, string deptId)
        {
            int patientId = 0;
            if (pid != "" && pid != string.Empty)
            {
                patientId = Convert.ToInt32(pid);
            }
            PatientTestResultModel model = new PatientTestResultModel();

            if (fDate != null && tDate != null)
            {
                int DepartMentId = Convert.ToInt32(deptId);
                if (DepartMentId == 1001)
                {
                    model.PopulatePatientTestModelList = pro.GetPatientTestListForEmer(fDate, tDate, patientId).ToList();
                }
                else
                {
                    model.PopulatePatientTestModelList = pro.GetPatientTestList(fDate, tDate, patientId).ToList();
                }

            }
            return View(model);

        }


        public ActionResult PatientReportResult(string PatientTestID)
        {
            int PTestId = 0;
            if (PatientTestID != "" && PatientTestID != string.Empty)
            {
                PTestId = Convert.ToInt32(PatientTestID);
            }
            PatientTestResultModel model = new PatientTestResultModel();

            model.PopulatePatientTestResultReportModelList = pro.GetPatientTestReportResultList(PTestId);
            model.TestID = PTestId;
            return View("PatientTestResultReport", model);

        }

        public ActionResult OtherTestMenu()
        {
            return View();
        }

        public ActionResult CytologyReportIndex()
        {
            CytologyReportsModel model = new CytologyReportsModel();
            model = pro.GetCytologyReportsList();
            return View(model);
        }
        public ActionResult BoneMarrowRptIndex()
        {
            BoneMarrowReportsModel model = new BoneMarrowReportsModel();
            model = pro.GetBoneMarrowReportsList();
            return View(model);
        }

        public ActionResult CreateBoneMarrowRpt(int id)
        {
            BoneMarrowReportsModel model = new BoneMarrowReportsModel();
            model.PatientId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateBoneMarrowRpt(BoneMarrowReportsModel model)
        {
            //model.PatientId = id;
            pro.CreateBoneMarrowRpt(model);
            return RedirectToAction("BoneMarrowRptIndex");

        }

        public ActionResult CreateCytologyRpt(int id)
        {
            CytologyReportsModel model = new CytologyReportsModel();
            model.PatientId = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCytologyRpt(CytologyReportsModel model)
        {
            if (ModelState.IsValid)
            {
                pro.CreateCytologyRpt(model);


            }

            //CytologyReportsModel model = new CytologyReportsModel();
            //return View(model);
            return RedirectToAction("CytologyReportIndex");
        }

        public ActionResult ShowCytologyReport(string CytopathNo, string RegisterId)
        {
            CytologyReportsModel model = new CytologyReportsModel();
            model = pro.GetCytologyReportByRegNumber(CytopathNo, Convert.ToInt32(RegisterId));
            return View(model);

        }

        public ActionResult ShowBoneMarrowRpt(int id)
        {
            BoneMarrowReportsModel model = new BoneMarrowReportsModel();
            model = pro.GetBoneMarrowRptFromId(id);
            return View(model);
        }

        public ActionResult SearchPatient()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SearchPatient(int srchVal, string value)
        {
            PatientTestResultModel model = new PatientTestResultModel();
            //OpdModel model = new OpdModel();
            OpdProvider pro = new OpdProvider();
            if (srchVal == 1)
            {
                try
                {
                    int patientId = Convert.ToInt16(value);
                    string str = "";
                    model.OpdModelList = pro.SearchOPD(patientId, str);
                    return PartialView("_OpdPatientRecordsWithPatientId", model);
                }

                catch (Exception e)
                {

                    TempData["msz"] = "Please Check Patient Id";
                    return PartialView("_OpdPatientRecordsWithPatientId", model);
                }

            }



            if (srchVal == 5)
            {

                int age = Convert.ToInt16(value);


            }

            if (srchVal == 2)
            {
                model.OpdModelList = pro.SearchOPD(0, value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }

            if (srchVal == 3)
            {
                model.OpdModelList = pro.SearchOPDWithArress(value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }

            if (srchVal == 4)
            {
                model.OpdModelList = pro.SearchOPDWithPhone(value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }
            return View();

        }

    }
}
