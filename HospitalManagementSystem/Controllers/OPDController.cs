using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem;
using System.Web.UI;

namespace MasterSetup.Controllers
{
    [Authorize]
    public class OpdController : Controller
    {
        // GET: /Opd/
        PreRegistrationProvider prePro = new PreRegistrationProvider();
        OpdProvider pro = new OpdProvider();

        public ActionResult TodayPatient()
        {
            int i = pro.CountTodayPatient();
            ViewBag.count = i;
            return View();
        }

        //[CustomAuthorize(Roles = "OpdView, OpdAdmin, OpdCreate,superadmin, admin")]

        public ActionResult Index(int page = 1)
        {
            int EmployeeId = Utility.GetCurrentLoginUserId();
            int DepartmentId = Utility.GetCurrentUserDepartmentId();

            int pagesize = 25;
            OpdModel model = new OpdModel();

            if (EmployeeId == 999)
            {
                model.OpdModelList = pro.Getlist(page, pagesize);
                ViewBag.currentPage = page;
                ViewBag.TotalPages = Math.Ceiling((double)pro.GetTotalItemCount(0) / pagesize);
            }

            else
            {
                model.OpdModelList = pro.GetlistByDepartmentId(DepartmentId, page, pagesize);
                ViewBag.currentPage = page;
                double totalpage = Math.Ceiling((double)pro.GetTotalItemCount(DepartmentId) / pagesize);
                ViewBag.TotalPages = Math.Ceiling((double)pro.GetTotalItemCount(DepartmentId) / pagesize);
            }

            return View(model);
        }

        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult Create()
        {
            OpdModel model = new OpdModel();
            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdDoctorListModel = new OpdDoctorListModel();

            //model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
            //model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFee();
            //model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentRegistrationTaxFee();
            //model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnly();
            //model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentOldRegistrationTaxFee();
            //var ThisSession = this.Session["OpdTypeIdInt"];
            //if (ThisSession != null)
            //{
            //    model.opdtype = ThisSession.ToString();
            //}
            //else
            //{
            //    model.opdtype = "1";
            //}

            //if (model.opdtype == "2")
            //{
            //    model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentPayableRegFee();
            //    model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentPayableRegFee();
            //    model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentRegistrationTaxFeePay();
            //    model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentRegistrationTaxFee();
            //    model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnlyPay();
            //    model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnly();
            //    model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentOldRegistrationTaxFee();
            //}


            model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
            model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFee();
            model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentRegistrationTaxFee();
            model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnly();
            //model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentOldRegistrationTaxFee();

            var ThisSession = this.Session["OpdTypeIdInt"];
            if (ThisSession != null)
            {
                model.opdtype = ThisSession.ToString();
            }
            else
            {
                model.opdtype = "1";
            }

            if (model.opdtype == "2")
            {
                model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentPayableRegFee();
                model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentPayableRegFee();
                model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentRegistrationTaxFeePay();
                model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnlyPay();
                //model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentOldRegistrationTaxFee();
                //model.OpdFeeDetailsModel.RegistrationFeeOnly = Utility.GetCurrentRegistrationFeeOnly();
                //model.OpdFeeDetailsModel.FeeTaxAmount = Utility.GetCurrentOldRegistrationTaxFee();
            }







            for (int i = 1; i <= 1; i++)
            {

                var mod = new OpdDoctorListModel();

                model.OpdDoctorList.Add(mod);

            }

            model.CountryID = 153;
            return View(model);
        }



        [HttpPost]
        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult Create(OpdModel model)
        {
            var ThisSession = this.Session["OpdTypeIdInt"];
            if (ThisSession != null)
            {
                model.opdtype = ThisSession.ToString();
            }
            else
            {
                model.opdtype = "1";
            }
            int i = pro.Insert(model);
            if (i != 0)
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                Utility.SaveTrialAudit(controllerName, actionName);
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;

                int LastInsertdId = pro.GetLastInsertedId();
                return RedirectToAction("BillForm", new { id = i, FromSource = 1 });
            }
            else
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult AddDoctorList()
        {
            OpdDoctorListModel model = new OpdDoctorListModel();
            return PartialView("AddDoctorList", model);
        }
        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult Edit(int id)
        {
            OpdModel model = new OpdModel();

            model = pro.Getlist().Where(x => x.OpdID == id).FirstOrDefault();
            model.OpdDoctorList = pro.GetPatientDoctorList(id);
            model.OpdFeeDetailsModel = pro.GetPatientFeeDetailsList(id).LastOrDefault();
            return View(model);
        }
        [HttpPost]
        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult Edit(int id, OpdModel model)
        {
            if (ModelState.IsValid)
            {
                //update here
                int i = pro.Update(model);
                if (i != 0)
                {
                    string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    Utility.SaveTrialAudit(controllerName, actionName);
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");

                }

            }
            return View(model);
        }

        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate, OpdView, superadmin, admin")]
        public ActionResult Details(int id)
        {
            OpdModel model = new OpdModel();
            model = pro.Getlist().Where(x => x.OpdID == id).FirstOrDefault();
            model.OpdDoctorList = pro.GetPatientDoctorList(id);
            model.OpdFeeDetailsModel = pro.GetPatientFeeDetailsList(id).LastOrDefault();
            //return PartialView("_Details",model);
            return PartialView("_Details1", model);
        }

        public ActionResult BillForm(int id, int FromSource)
        {
            OpdModel model = new OpdModel();
            //Multiple visit or old and new registration bill.......
            int TotalCount = pro.GetNumberOfVisitOfPatientInHospital(id);
            if (TotalCount > 1)
            {
                model = pro.GetAllBillDate(id);
                return View("MultipleBillCase", model);
                //return RedirectToAction("MultipleBillCase", model);
            }


            model = pro.GetOpdDetails(id);
            return View(model);
        }


        public ActionResult PrintMultipleBill(int id, int PatientId)
        {
            OpdModel model = new OpdModel();
            model = pro.GetOpdDetailsForMultipleBillCase(id, PatientId);
            return View("BillForm", model);
        }



        public ActionResult MultipleBillCase(OpdModel model)
        {
            return View(model);
        }


        //[CustomAuthorize(Roles = "OpdSearch, OpdMain, superadmin, admin")]
        public ActionResult SearchIndex(OpdModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            DateTime fromdate = model.FromDate;
            DateTime todate = model.toDate;
            int pagesize = 25;
            string PatientName = string.Empty;
            if (!string.IsNullOrEmpty(model.NameofPatent))
            {
                PatientName = model.NameofPatent.ToLower();
            }


            int DepartmentId = Utility.GetCurrentUserDepartmentId();
            var list = new List<OpdModel>();
            list = pro.GetlistByDepartmentId(DepartmentId, 1, 50).Where(x => x.RegistrationDate <= todate && x.RegistrationDate >= fromdate).ToList();
            model.OpdModelList = list.Where(x => x.FirstName.ToLower().StartsWith(PatientName)).ToList();

            ViewBag.currentPage = 1;
            // double totalpage = Math.Ceiling((double)ent.OpdMaster.Where(x=>x.RegistrationSource == "Opd").Count() / pagesize);
            ViewBag.TotalPages = Math.Ceiling((double)ent.OpdMasters.Where(x => x.RegistrationSource == "Opd").Count() / pagesize);
            ViewBag.currentPage = 1;
            ViewBag.TotalPages = 1;
            return View("Index", model);

        }
        //public ActionResult SearchZone(OpdModel model)
        //{
        //    EHMSEntities ent = new EHMSEntities();
        //    DateTime fromdate = model.FromDate;
        //    DateTime todate = model.toDate;
        //    string pagesize = 25;
        //    string ZoneName = string.Empty;
        //    if(!string.IsNullOrEmpty())
        //}
        //public ActionResult DoctorSearchIndex(OpdModel model)
        //{
        //    EHMSEntities ent = new EHMSEntities();
        //    DateTime fromdate = model.FromDate;
        //    DateTime todate = model.toDate;
        //    string DoctorName = string.Empty;
        //    if (!string.IsNullOrEmpty(model.NameofDoctor))
        //    {
        //        DoctorName = model.NameofDoctor.ToLower();
        //    }
        //    int DepartmentId = Utility.GetCurrentUserDepartmentId();
        //    var list = new List<OpdModel>();
        //}

        public ActionResult Search()
        {
            OpdModel model = new OpdModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(string selectOptions, string value, int? bloodid)
        {
            OpdModel model = new OpdModel();
            if (selectOptions == "3")//Blood Group
            {

                model.OpdModelList = pro.SearchByBloodGroup(bloodid.ToString());
                return PartialView("_OpdSearch", model);

            }
            if (selectOptions == "1")//Patient Id
            {
                int n;
                bool isNumeric = int.TryParse(value, out n);
                if (isNumeric)
                {
                    model.OpdModelList = pro.SearchOPD(Convert.ToInt32(value), "");
                }

                return PartialView("_OpdSearch", model);

            }

            if (selectOptions == "2")//Patient Name
            {
                model.OpdModelList = pro.SearchOPD(0, value);
                return PartialView("_OpdSearch", model);
            }

            if (selectOptions == "4")//Address
            {
                model.OpdModelList = pro.SearchOPDWithArress(value);
                return PartialView("_OpdSearch", model);
            }
            if (selectOptions == "5")//Mobile
            {
                model.OpdModelList = pro.SearchOPDWithArress(value);
                return PartialView("_OpdSearch", model);
            }
            if (selectOptions == "6")//Mobile
            {
                model.OpdModelList = pro.SearchOPDWithArress(value);
                return PartialView("_OpdSearch", model);
            }



            return PartialView("_OpdSearch", model);
        }

        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult OldPatient()
        {

            OpdModel model = new OpdModel();
            return View(model);

        }


        [HttpPost]
        //[CustomAuthorize(Roles = "OpdAdmin, OpdCreate,superadmin, admin")]
        public ActionResult OldPatient(int srchVal, string value)
        {
            if (srchVal == 1)
            {
                try
                {
                    int patientId = Convert.ToInt16(value);
                    OpdModel model = new OpdModel();
                    string str = "";
                    model.OpdModelList = pro.SearchOPD(patientId, str);
                    return PartialView("_OpdPatientRecordsWithPatientId", model);
                }

                catch (Exception e)
                {

                    TempData["msz"] = "Please Check Patient Id";
                    OpdModel model = new OpdModel();
                    return PartialView("_OpdPatientRecordsWithPatientId", model);
                }

            }



            if (srchVal == 5)
            {

                int age = Convert.ToInt16(value);


            }

            if (srchVal == 2)
            {
                OpdModel model = new OpdModel();
                model.OpdModelList = pro.SearchOPD(0, value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }

            if (srchVal == 3)
            {
                OpdModel model = new OpdModel();
                model.OpdModelList = pro.SearchOPDWithArress(value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }

            if (srchVal == 4)
            {
                OpdModel model = new OpdModel();
                model.OpdModelList = pro.SearchOPDWithPhone(value);
                return PartialView("_OpdPatientRecordsWithPatientId", model);
            }

            return View();

        }

        public ActionResult _OpdSearchBybloodGroup(string bloodgroupid)
        {

            OpdModel model = new OpdModel();
            model.OpdModelList = pro.SearchByBloodGroup(bloodgroupid);
            return PartialView("_OpdPatientByBloodGroup", model);



        }

        public ActionResult SearchByBloodGroup()
        {

            return View();
        }


        public ActionResult OpdBilling(int id)
        {
            OpdModel model = new OpdModel();

            if (id == 1)
            {
                ViewBag.LayoutId = "Master";
            }
            else
            {
                ViewBag.LayoutId = "Slave";
            }
            return View(model);
        }

        public ActionResult _SearchBillDetails(DateTime Sdate, DateTime Edate, string patientName, string id)
        {
            OpdModel model = new OpdModel();
            EHMSEntities ent = new EHMSEntities();
            int opdid;
            string PatinetName = "Default";
            if (!string.IsNullOrEmpty(patientName))
            {
                PatinetName = patientName;

            }
            if (id == "")
            {
                opdid = 0;
            }
            else
            {
                opdid = Convert.ToInt32(id);
            }

            if (Sdate != null && Edate != null)
            {
                //model.CentralizeBillingModelList = pro.GetOpdBill(Sdate, Edate, PatinetName, opdid);
                ViewBag.TotalAmousnt = "67.00";
                return PartialView("_BillingDetails", model);
            }
            else
            {
                return null;
            }
        }


        //[CustomAuthorize(Roles = "OpdCreate, OpdMain, superadmin, admin")]
        public ActionResult ViewOldPatient(int id)
        {
            OpdModel model = new OpdModel();
            model = pro.Getlist().Where(x => x.OpdID == id).FirstOrDefault();
            DateTime TodayDate = DateTime.Now;
            DateTime PreviousRegDate = model.RegistrationDate;
            decimal totalDays = Math.Round(Convert.ToDecimal((TodayDate - PreviousRegDate).TotalDays));

            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            model.OpdDoctorListModel = new OpdDoctorListModel();

            model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFeeForOldPatient();
            model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFeeForOldPatient();

            if (totalDays > 7)//New registration fee
            {

                model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
                model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFee();
            }



            var mod = new OpdDoctorListModel();

            model.OpdDoctorList.Add(mod);
            return View(model);

        }

        [HttpPost]
        public ActionResult ViewOldPatient(int id, OpdModel model)
        {

            EHMSEntities ent = new EHMSEntities();

            DateTime date = model.OpdDoctorList.Select(x => x.PreferDate.Value).FirstOrDefault();

            int count = ent.OpdPatientDoctorDetails.Where(x => x.OpdID == id && x.PreferDate == date).Count();
            if (count >= 1)
            {
                TempData["PreferDate"] = "Patient Already Prefered On This Date !";
                return View(model);
            }


            if (ModelState.IsValid)
            {
                //update here
                int i = pro.OldPatientInsert(model);
                if (i != 0)
                {
                    string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    Utility.SaveTrialAudit(controllerName, actionName);
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                    //return RedirectToAction("Index");
                    int LastInsertdId = pro.GetLastInsertedId();
                    //return RedirectToAction("Index");
                    return RedirectToAction("BillForm", new { id = id, FromSource = 1 });
                    //int TotalCount = pro.GetNumberOfVisitOfPatientInHospital(id);
                    //if (TotalCount > 1)
                    //{
                    //    //model = pro.GetAllBillDate(id);
                    //    //model.OpdID = id;
                    //    ////return View("MultipleBillCase", model);
                    //    ////return RedirectToAction("PrintMultipleBill", new { id = model.OpdFeeDetailsModel.OpdFeeDetailsID, PatientId = model.OpdID });
                    //    //return RedirectToAction("MultipleBillCase", model);
                    //}

                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");

                }

            }
            return View(model);


        }
        public ActionResult ListTodayValue()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationModel model = new PreRegistrationModel();

                DateTime a = DateTime.Now;

                model.PreRegistrationModelList = prePro.GetResult();
                return View(model);
            }
        }

        public ActionResult PreRegistrationData(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PreRegistrationModel model = new PreRegistrationModel();
                List<PreRegistrationModel> models = new List<PreRegistrationModel>();

                model = prePro.GetResult().Where(x => x.PreRegistrationID == id).FirstOrDefault();
                model.OpdDoctorList = prePro.Value(id);


                model.OpdFeeDetailsModel = new OpdFeeDetailsModel();
                model.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
                model.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFee();

                return View(model);

            }

        }
        [HttpPost]
        public ActionResult PreRegistrationData(int id, PreRegistrationModel model)
        {
            //var errors = (ModelState.Values.SelectMany(v => v.Errors).ToList());
            if (ModelState.IsValid)
            {

                int i = pro.Insert(model);
                if (i != 0)
                {
                    string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    Utility.SaveTrialAudit(controllerName, actionName);
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                    prePro.UpdateUserStatus(id);
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");



        }


        public ActionResult _SearchOption()
        {
            return PartialView();
        }

        public ActionResult MemberSelf(int Id, string FirstName, int MembertypeId, int MaxDepend, int DueDepend)
        {

            OpdModel opdobj = new OpdModel();

            // SetUpMemberShipModel setupmemobj = new SetUpMemberShipModel();
            EHMSEntities ent = new EHMSEntities();


            var obj = ent.SetupMemberShips.Where(x => x.SetupMemberID == Id).FirstOrDefault();

            SetUpMemberShipModel setupmemobj = AutoMapper.Mapper.Map<SetupMemberShip, SetUpMemberShipModel>(obj);

            // opdobj = AutoMapper.Mapper.Map<SetUpMemberShipModel, OpdModel>(setupmemobj);


            // OpdModel opdobj = AutoMapper.Mapper.Map<SetUpMemberShipModel, OpdModel>(setupmemobj);
            opdobj.IsMember = 1;
            opdobj.CountryID = setupmemobj.CountryID;
            opdobj.FirstName = setupmemobj.FirstName;
            opdobj.MiddleName = setupmemobj.MiddleName;
            opdobj.LastName = setupmemobj.LastName;
            opdobj.Address = setupmemobj.Address;
            obj.Gender = setupmemobj.Gender;
            opdobj.BloodGroup = setupmemobj.BloodGroupID.ToString();
            opdobj.ContactName = setupmemobj.ContactNumber;
            opdobj.MobileNumber = setupmemobj.MobileNumber;
            opdobj.Email = setupmemobj.Email;
            opdobj.MemberTypeID = setupmemobj.MemberTypeID;

            opdobj.AgeYear = DateTime.Now.Year - setupmemobj.DateofBirth.Year;

            opdobj.OpdFeeDetailsModel = new OpdFeeDetailsModel();
            opdobj.OpdFeeDetailsModel.RegistrationFee = Utility.GetCurrentRegistrationFee();
            opdobj.OpdFeeDetailsModel.TotalAmount = Utility.GetCurrentRegistrationFee();


            for (int i = 1; i <= 1; i++)
            {

                var mod = new OpdDoctorListModel();

                opdobj.OpdDoctorList.Add(mod);

            }

            return View("Create", opdobj);

        }

        public ActionResult MembershipRealtion(int id)
        {


            return View();

        }

        public ActionResult GetDoctorFeeWithId(int id)
        {
            EHMSEntities ent = new EHMSEntities();

            try
            {
                if (id == 1009)
                {
                    var fee = 0;
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var fee = ent.SetupDoctorFees.Where(x => x.DoctorID == id).Select(x => x.DoctorFee).FirstOrDefault();
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {

                var feenot = 0;
                return Json(feenot, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult TransactionDetails(int id)
        {
            OpdModel model = new OpdModel();
            //model.BillingModelList = pro.GetTransacationDetailsById(id);
            return PartialView("_TransactionDetail", model);
        }
        //[CustomAuthorize(Roles = "OpdAdmin, superadmin, admin")]
        public ActionResult OpdType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeOpdType(string OpdTypeId)
        {
            if (OpdTypeId != null && OpdTypeId != "")
            {
                int OpdTypeInt = Convert.ToInt32(OpdTypeId);
                System.Web.HttpContext.Current.Session["OpdTypeIdInt"] = OpdTypeInt;
            }
            else
            {
                System.Web.HttpContext.Current.Session["OpdTypeIdInt"] = 1;//general
            }


            return RedirectToAction("Index");
        }


        public ActionResult OpdVital(int id)
        {
            OpdProvider pro = new OpdProvider();

            int i = Utility.patientlogid(id);

            VitalForOthersModel model = new VitalForOthersModel();

            model.lstOfVitalForOthersModel = pro.GetListOfVitalWithPatientId(id);

            model.PatinetLogId = i;
            model.OpdId = id;

            return View(model);

        }

        public ActionResult OpdVitalIndex()
        {

            OpdProvider pro = new OpdProvider();

            return View();

        }



        [HttpPost]
        public ActionResult OpdVital(VitalForOthersModel model)
        {



            OpdProvider pro = new OpdProvider();

            int i = pro.InsertOPdVital(model);
            if (i != 0)
            {

                TempData["Success"] = UtilityMessage.save;
                return RedirectToAction("OpdVital", new { id = model.OpdId });

            }
            else
            {

                TempData["Success"] = UtilityMessage.editfailed;
                return RedirectToAction("OpdVital", new { id = model.OpdId });

            }




        }

        public ActionResult EditVital(int id)
        {
            VitalForOthersModel model = new VitalForOthersModel();

            using (EHMSEntities ent = new EHMSEntities())
            {

                //var dataOfVital = ent.VitalForOthers.Where(x => x.OpdId == id && x.PatinetLogId == patientlogid).FirstOrDefault();

                model = AutoMapper.Mapper.Map<VitalForOther, VitalForOthersModel>(ent.VitalForOthers.Where(x => x.VitalForOtherId == id).FirstOrDefault());


            }



            return PartialView("_EditVital", model);



        }


        [HttpPost]
        public ActionResult EditVital(VitalForOthersModel model)
        {

            string str = "";
            OpdProvider pro = new OpdProvider();

            int i = pro.EditOpdVital(model);
            if (i != 0)
            {


                TempData["Success"] = UtilityMessage.edit;

            }
            else
            {

                TempData["Success"] = UtilityMessage.editfailed;

            }
            return RedirectToAction("OpdVital", new { id = model.OpdId });
        }

        public ActionResult OpdMain()
        {
            return View();
        }

        public ActionResult DoctorCommisionReport(int id)
        {
            OpdModel model = new OpdModel();

            if (id == 1)
            {
                ViewBag.LayoutId = "Master";
            }
            else
            {
                ViewBag.LayoutId = "Slave";
            }
            return View(model);
        }

        public ActionResult _SearchDocCommDetails(DateTime Sdate, DateTime Edate, string DocId)
        {
            DoctorCommisionModel model = new DoctorCommisionModel();
            model.DoctorCommisionModelList = new List<DoctorCommisionModel>();
            if (Sdate != null && Edate != null)
            {
                int DocIdInt = Convert.ToInt32(DocId);
                model.DoctorCommisionModelList = pro.GetDoctorCommision(Sdate, Edate);
                if (DocIdInt != 1009)
                {
                    model.DoctorCommisionModelList = model.DoctorCommisionModelList.Where(x => x.DoctorID == DocIdInt).ToList();
                }

                return PartialView("_doctorComm", model);
            }
            else
            {
                return null;
            }
        }

        public ActionResult HospitalCommision(int id)
        {

            return View();

        }






    }
}
