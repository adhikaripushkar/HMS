using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;
using System.Web.Security;

//using RazorPDF;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
        //
        // GET: /Billing/

        //[CustomAuthorize(Roles = "superadmin, admin, AccountHead, AccountView")]noticed
        public ActionResult Main()
        {

            return View();
        }


        BillingProvider bpro = new BillingProvider();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BillingDetails(int id)
        {
            BillingModel model = new BillingModel();
            model.TransactionDate = DateTime.Now;
            model.EndTransactionDate = DateTime.Now;
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


        [HttpPost]
        public ActionResult BillingDetails(BillingModel model)
        {
            model.CentralizeBillingViewModelList = bpro.GetListForIndex(model.TransactionDate, model.EndTransactionDate);
            model.TransactionDate = model.TransactionDate;
            model.EndTransactionDate = model.EndTransactionDate;
            TempData["Value"] = "";
            return View(model);
        }

        public ActionResult SearchBy()
        {
            BillingModel model = new BillingModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult SearchBy(BillingModel model, string OpOrEmr)
        {
            int name;
            if (model.Value == "OPDID" && int.TryParse(model.Name, out name))
            {
                int id = Convert.ToInt32(name);
                model.CentralizeBillingViewModelList = bpro.SearchById(id);
            }
            else if (model.Value == "Name")
            {
                model.CentralizeBillingViewModelList = bpro.SearchByName(model.Name);
            }
            else
            {
                TempData["a"] = 1;
                return RedirectToAction("SearchBy");
            }
            return View(model);
        }

        public ActionResult SearchByDepartmentID()
        {
            BillingModel model = new BillingModel();
            model.TransactionDate = DateTime.Now;
            model.EndTransactionDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchByDepartmentID(BillingModel model)
        {
            model.CentralizeBillingViewModelList = bpro.SearchByDepartmentID(model.DepartmentID, model.TransactionDate, model.EndTransactionDate);
            return View(model);
        }

        public ActionResult SearchByCatagory()
        {
            BillingModel model = new BillingModel();
            model.TransactionDate = DateTime.Now;
            model.EndTransactionDate = DateTime.Now;
            return View(model);

        }

        [HttpPost]
        public ActionResult SearchByCatagory(BillingModel model)
        {
            model.BillingModelList = bpro.BillingModelList(model.FeeTypeId, model);
            return View(model);
        }

        public ActionResult SearchByRoomType()
        {
            BillingModel model = new BillingModel();

            return View(model);


        }
        [HttpPost]
        public ActionResult SearchByRoomType(BillingModel model)
        {
            model.BillingModelList = bpro.BillingListForRoom(model.RoomType);
            if (model.BillingModelList == null)
            {
                TempData["a"] = 1;
                return RedirectToAction("SearchByRoomType");

            }
            TempData["Value"] = model.BillingModelList.Sum(x => x.Amount);
            return View(model);

        }

        public ActionResult SearchByTodayTransaction()
        {
            BillingModel model = new BillingModel();
            model.TransactionDate = DateTime.Now;
            return View(model);

        }
        [HttpPost]
        public ActionResult SearchByTodayTransaction(BillingModel model)
        {

            model.BillingModelList = bpro.SearchTodayTransaction(model.TransactionDate);
            //  var list = new BillingModel();
            // list.BillingModelList = model.BillingModelList;
            if (model.BillingModelList == null)
            {
                TempData["a"] = 1;
                return RedirectToAction("SearchByRoomType");

            }
            //ViewBag.Value = model.BillingModelList;
            // var pdf = new PdfResult("",model);
            TempData["Value"] = model.BillingModelList.Sum(x => x.Amount);
            //return new PdfResult(model.BillingModelList, "SearchByTodayTransaction");
            return View(model);
        }
        public ActionResult SearchByUsers()
        {
            BillingModel model = new BillingModel();
            model.TransactionDate = DateTime.Today;
            model.EndTransactionDate = DateTime.Today;

            return View(model);
        }

        [HttpPost]
        public ActionResult SearchByUsers(BillingModel model)
        {

            //model.CentralizeBillingViewModelList = bpro.SearchByDepartmentID(model.DepartmentID, model.TransactionDate, model.EndTransactionDate);
            return View(model);
        }



        public JsonResult SearchPatient(string term)
        {
            //var result = (from r in ent.OpdMaster
            //              where r.FirstName.ToLower().Contains(term.ToLower())
            //              select new { r.FirstName }).Distinct();
            EHMSEntities ent = new EHMSEntities();


            var result = (from r in ent.OpdMasters
                          where r.FirstName.ToLower().Contains(term.ToLower())


                          select new { r.FirstName }).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BillingDischargeSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BillingDischargeSummary(BillingModel model)
        {

            model = bpro.GetAllDischargeBillSummary(model.PatientLogId);
            return View(model);
        }

        public ActionResult GetBillDetails(int id)
        {
            BillingModel model = new BillingModel();

            model.GetBillDetailByBillNoViewModelList = bpro.GetAllBillDetailsByBillNumber(id);
            return PartialView("_BillDetailsByBillNumber", model);
        }

        public ActionResult GetBalanceDeposit()
        {
            BillingModel model = new BillingModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetBalanceDeposit(BillingModel model)
        {
            model.DepositUsedRemainViewModelList = bpro.GetDepositBalanceUsed();
            return View(model);
        }
        public ActionResult TransactionDetails(int id)
        {
            BillingModel model = new BillingModel();
            int OpdId = HospitalManagementSystem.Utility.GetOpdIdFromAccountHeadIdFromOpdMaster(id);
            model = bpro.GetTransacationDetailsById(OpdId);
            return PartialView("_TransactionDetails", model);
            //return View();
        }

        public ActionResult PatientTranDetails()
        {
            BillingModel model = new BillingModel();
            return View();
        }
        [HttpPost]
        public ActionResult PatientTranDetails(string selectOptions, string value, string ReportOrRefund)
        {
            BillingModel model = new BillingModel();
            switch (selectOptions)
            {
                case "1":
                    //int OpdId = Convert.ToInt32(value);
                    model = bpro.GetPatientsDetailsByIdAndName(1, value);
                    break;

                case "2":
                    model = bpro.GetPatientsDetailsByIdAndName(2, value);
                    break;

                default:

                    break;
            }

            return PartialView("_PatientDetails", model);


        }

        public ActionResult ShowPatientTranRpts(int id)
        {
            BillingCounterModel model = new BillingCounterModel();
            BillingCounterProvider pro = new BillingCounterProvider();
            model = pro.GetAllDischargeBillSummary(id);

            int AccountHeadID = Convert.ToInt32(0);
            EHMSEntities ent = new EHMSEntities();
            var AccHeadId = ent.OpdMasters.Where(x => x.OpdID == id).FirstOrDefault().AccountHeadId;
            if (AccHeadId != null)
            {
                AccountHeadID = (int)AccHeadId;
                model.ObjDepositRefundViewModel.RemainingAmount = pro.getBalanceDeposit(AccountHeadID);
                //model.ObjDepositRefundViewModel.AccountSubHeadId = AccountHeadID;

            }
            else
            {
                model.ObjDepositRefundViewModel.RemainingAmount = Convert.ToDecimal(0);
            }

            return View(model);
        }




    }

}

