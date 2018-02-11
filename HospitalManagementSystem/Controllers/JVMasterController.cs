using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Controllers
{
    public class JVMasterController : Controller
    {
        // GET: /JVMaster/
        JVMasterModel model = new JVMasterModel();
        JVMasterProvider pro = new JVMasterProvider();

        public ActionResult Index(string BillingDept, string JvDate, string JvDateEnd)
        {
            JVMasterModel model = new JVMasterModel();
            BillingDept = "1";
            if (!string.IsNullOrEmpty(BillingDept))
            {
                model.objBillingDetailViewModel = new BillingDetailViewModel();
                model.BillingDetailViewModelList = pro.GetTodayTransaction(JvDate, JvDateEnd);
                model.BillingDetailViewModelListNew = pro.GetBillDetailDisplayReport(JvDate, JvDateEnd);
                model.objBillingDetailViewModel.DepartmentName = BillingDept;
                model.objBillingDetailViewModel.BillingDate = JvDate;
                model.objBillingDetailViewModel.StartDate = JvDate;
                model.objBillingDetailViewModel.EndDate = JvDateEnd;
                model.Count = pro.CountifComisionPosted(JvDate, JvDateEnd);
            }
            return View(model);
        }

        public ActionResult ShowJournalVoucher(int id)
        {
            JVMasterModel model = new JVMasterModel();
            model.ObjJVDetailsViewModel = new JVDetailsViewModel();
            string Name = Utility.GetJVFormName(id);
            if (Name == "Payroll" || Name == "Stock")
            {
                model.JVDetailsModelList = pro.GetJvPayrollDisplayReport(id);
            }
            else if (Name == "Comission")
            {
                model.JVDetailsModelList = pro.GetJvCommisionDisplayReport(id);
            }
            else
            {
                model.JVDetailsModelList = pro.GetJvDisplayReport(id);
            }
            model.ObjJVDetailsViewModel.JVnumber = pro.GetJVNumberFromJVMasterId(id);
            model.ObjJVDetailsViewModel.JVDate = pro.GetJVTransactionDate(id);
            model.Narration1 = pro.GetJVNarration(id);
            model.JvType = pro.GetJVTypeFromJVMasterId(id);
            return View(model);
        }

        public ActionResult ShowPaymentAndReceiptVoucher(int id)
        {
            JVMasterModel model = new JVMasterModel();
            model.ObjJVDetailsViewModel = new JVDetailsViewModel();
            model.JVDetailsModelList = pro.GetListFromJVDetails(id);
            model.JVDetailsModelList = pro.GetJvDisplayReport(id);
            model.ObjJVDetailsViewModel.JVnumber = pro.GetJVNumberFromJVMasterId(id);
            model.ObjJVDetailsViewModel.JVDate = pro.GetJVTransactionDate(id);
            model.Narration1 = pro.GetJVNarration(id);
            return View(model);
        }

        public ActionResult MakeJournalVoucher(string DeptName, string jvDate, string JvDateEnd, string TotalAmount, string Narration)
        {
            model.objBillingDetailViewModel = new BillingDetailViewModel();
            model.BillingDetailViewModelList = pro.GetTodayTransaction(jvDate, JvDateEnd);
            model.objBillingDetailViewModel.AmountAfterDiscount = Convert.ToDecimal(TotalAmount);
            //model.objBillingDetailViewModel.TotalIncomeAmount = pro.GetTotalIncomeAmount(DeptName);
            model.objBillingDetailViewModel.DepartmentName = DeptName;
            model.JvType = "JV";
            int i = pro.SaveJournnalVoucher(model, jvDate, JvDateEnd, Narration);
            if (i > 0)
            {
                model.BillingDetailViewModelList = pro.GetComissionTransaction(jvDate, JvDateEnd);
                model.JvType = "JV";
                i = pro.SaveComissionJV(model, jvDate, JvDateEnd, Narration);
                if (i > 0)
                {
                    int id = pro.GetLastInsertedJVNumber() - 1;
                    return RedirectToAction("ShowJournalVoucher", "JVMaster", new { id = id });
                }
                else
                {
                    int id = pro.GetLastInsertedJVNumber();
                    return RedirectToAction("ShowJournalVoucher", "JVMaster", new { id = id });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PostJVForm(JVMasterModel model)
        {
            return View();
        }

        public ActionResult CreateJournalVoucher()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateJournalVoucher(JVMasterModel model)
        {
            return View();
        }


        public ActionResult BankReconcilation()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BankReconcilation(JVMasterModel model)
        {
            return PartialView("_BankReconcilation", model);
        }

        [HttpPost]
        public ActionResult _BankReconcilation(JVMasterModel model)
        {
            pro.CreateBankReconcilation(model);
            return RedirectToAction("BankReconcilation");

        }

        [HttpPost]
        public ActionResult AddMoreDrOrCr(JVMasterModel model)
        {
            return View();
        }

        public ActionResult ShowLedgerDetail()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ShowLedgerDetail(JVMasterModel model)
        {
            model.LedgerDetailsViewModelList = pro.GetLedgerDeatilList(model);
            return View(model);
        }

        public ActionResult LedgerAccountHeadWise()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LedgerAccountHeadWise(JVMasterModel model)
        {
            int DrOrCr = 1;
            model.LedgerViewModelAccountHeadWiseList = pro.GetLeadgerAccountHeadwise(model, DrOrCr);
            model.ObjLedgerViewModelAccountHeadWise.DrOrCrAmountType = DrOrCr;

            return PartialView("_LedgerDetailsAccWise", model);
        }

        public ActionResult CreateNewJV()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewJV(JVMasterModel model)
        {
            int i = 0;
            string ViewName = "";
            if (model.ObjAddMoreParticularsJVModel.DrAmountTotal == model.ObjAddMoreParticularsJVModel.CrAmountTotal)
            {
                i = pro.CreateNewJv(model);
                return RedirectToAction("ShowJournalVoucher", "JVMaster", new { id = i });
            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult AddMoreParticulars()
        {
            AddMoreParticularsJVModel model = new AddMoreParticularsJVModel();

            return PartialView("_AddMoreParticulars", model);

        }

        public ActionResult MainMenu()
        {
            return View();
        }

        public ActionResult Search()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(JVMasterModel model, string JvDate, string JvDateEnd)
        {
            model = pro.GetJVListBySearchParameter(model, JvDate, JvDateEnd);
            return PartialView("_DisplayVoucherLists", model);
        }

        public ActionResult GetTrailBalance()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetTrailBalance(JVMasterModel model)
        {
            model.objGetTrialBalanceModelViewList = pro.GetTrialBalance(model);
            return PartialView("_GetTrialBalance", model);
        }

        public ActionResult GetBillDetailByUser(int id)
        {
            JVMasterModel model = new JVMasterModel();
            if (id == 1)
            {
                model.Narration2 = "Master";
            }
            else
            {
                model.Narration2 = "Slave";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GetBillDetailByUser(JVMasterModel model)
        {
            model.objGetBilldetailByUserModelViewList = pro.GetBillDetailByUser(model);
            return PartialView("_GetBillDetailByUser", model);
        }

        public ActionResult GetBillDetailByDepartment(int id)
        {
            JVMasterModel model = new JVMasterModel();
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
        public ActionResult GetBillDetailByDepartment(JVMasterModel model)
        {
            model.objGetBilldetailByDepartmentModelViewList = pro.GetBillDetailByDepartment(model);
            return PartialView("_GetBillDetailByDepartment", model);
        }

        public ActionResult GetCashHandover()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetCashHandover(JVMasterModel model)
        {
            model.objGetCashHandoverDetailModelViewList = pro.GetCashDetailByUser(model);
            foreach (var item in model.objGetCashHandoverDetailModelViewList)
            {
                model.objGetCashHandoverDetailModelView.Amount += item.Amount;
            }
            return PartialView("_GetCashHandover", model);
        }

        public ActionResult _GetCashHandover()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult _GetCashHandover(JVMasterModel model)
        {

            int i = pro.InsertHandoverDetail(model);
            return RedirectToAction("GetCashHandover", "JVMaster");
        }

        public ActionResult HandOverReport()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult HandOverReport(JVMasterModel model)
        {
            model.objHandoverDetailReportModelViewList = pro.HandOverReport(model);
            return PartialView("_HandOverReport", model);
        }

        public ActionResult NotifyHandOver()
        {
            JVMasterModel model = new JVMasterModel();
            model.objNotificationHandoverModelViewList = pro.NotifyHandOver(Utility.GetCurrentLoginUserId());
            return View(model);
        }

        [HttpPost]
        public ActionResult NotifyHandOver(JVMasterModel model)
        {
            pro.UpdateHandoverStatus();
            return RedirectToAction("../Home/ShowDashBoard");
        }


        public ActionResult OpeningBalance()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult OpeningBalance(JVMasterModel model)
        {
            model.objOpeningBalanceModelViewList = pro.GetLeafLevelByID(model.objOpeningBalanceModelView.MainAccountHeadId);
            return PartialView("_OpeningBalance", model);
        }

        public ActionResult _OpeningBalance()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult _OpeningBalance(JVMasterModel model)
        {
            int i = pro.InsertOpeningBalance(model);
            return RedirectToAction("OpeningBalance", "JVMaster");
        }
        public ActionResult NewTrialBalance()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewTrialBalance(JVMasterModel model)
        {
            model.objGetTrialBalanceModelViewList = pro.NewTrialBalance(model);
            return PartialView("_NewTrialBalance", model);
        }

        public ActionResult GetAllTrialBalance()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }


        public ActionResult GetAllTrialBalance1(DateTime startdate, DateTime enddate, int upto)
        {
            JVMasterModel model = new JVMasterModel();
            model.objGetAllTrialBalanceModelView.StartDate = startdate;
            model.objGetAllTrialBalanceModelView.EndDate = enddate.Date;
            model.objGetAllTrialBalanceModelView.Level = 1;
            model.objGetAllTrialBalanceModelView.ParentID = 1;
            model.objGetAllTrialBalanceModelView.UpToLevel = upto;
            model.objGetAllTrialBalanceModelViewList = pro.GetAllTrialBalance(model);
            return PartialView("_GetAllTrialBalance", model);
        }

        public ActionResult TrialMainMenu()
        {
            return View();
        }

        public ActionResult PLSchedule()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PLSchedule(JVMasterModel model)
        {
            return PartialView("_PLSchedule", model);
        }

        public ActionResult BSSchedule()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BSSchedule(JVMasterModel model)
        {
            return PartialView("_BSSchedule", model);
        }

        public ActionResult BalanceSheet()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult BalanceSheet(JVMasterModel model)
        {
            return PartialView("_BalanceSheet", model);
        }

        public ActionResult IncomeAndExpenditure()
        {
            JVMasterModel model = new JVMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult IncomeAndExpenditure(JVMasterModel model)
        {
            return PartialView("_IncomeAndExpenditure", model);
        }

        public ActionResult CompareDateofBRS(string JvDate, string BRSDate)
        {
            DateTime JvEngDate = DateTime.Now;
            DateTime BRSEngDate = DateTime.Now;
            string Message = "";
            string ReconcileValue = "";
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    string year = JvDate.Substring(0, 4);
                    string month = JvDate.Substring(5, 2);
                    string Day = JvDate.Substring(8, 2);
                    JvEngDate = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetEnglishDate('" + year + "','" + month + "','" + Day + "')CreatedDate").FirstOrDefault().CreatedDate;
                    int totalcount = 0;
                    int newnum = 0;
                    bool isnum = int.TryParse(BRSDate, out newnum);
                    if (isnum)
                    {
                        foreach (var i in BRSDate)
                        {
                            totalcount++;
                        }
                    }
                    if (totalcount > 0 && totalcount <= 2)
                    {
                        if (Convert.ToInt32(BRSDate) > 0 && Convert.ToInt32(BRSDate) < 33)
                        {
                            BRSDate = year + '/' + month + '/' + BRSDate;
                        }
                    }
                    ReconcileValue = BRSDate;
                    year = BRSDate.Substring(0, 4);
                    month = BRSDate.Substring(5, 2);
                    Day = BRSDate.Substring(8, 2);
                    BRSEngDate = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select dbo.GetEnglishDate('" + year + "','" + month + "','" + Day + "')CreatedDate").FirstOrDefault().CreatedDate;
                    if (BRSEngDate < JvEngDate)
                    {
                        Message = "Wrong";
                    }
                }
                catch
                {
                    Message = "Wrong";
                }
            }
            return Json(new { Message, ReconcileValue });
        }

        public ActionResult GetBrsCount(int BankID, int BankSubID, DateTime BRSDate)
        {
            int count = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    if (BankSubID > 0)
                        count = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select COUNT(*)Number from dbo.BankReconcilation where ToDate >= '" + BRSDate + "' and BankID='" + BankID + "' and BankSubID='" + BankSubID + "'").FirstOrDefault().Number;
                    else
                        count = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select COUNT(*)Number from dbo.BankReconcilation where ToDate >= '" + BRSDate + "' and BankID='" + BankID + "' and BankSubID='" + BankSubID + "'").FirstOrDefault().Number;

                }
                catch
                {
                    count = 0;
                }
            }
            return Json(new { count });
        }

        public ActionResult GetAccountHead4LevelList(string DrCr, string Jvtype)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "Select AccountHead", Value = "0" });
                //var data = ent.GL_AccSubGroups.Where(x => x.HeadLevel == 4).ToList();
                var data = ent.GL_AccSubGroups.ToList();
                string Hierarchy = "1.320";
                if (Jvtype == "CV" && DrCr != "0")
                {
                    data = ent.GL_AccSubGroups.Where(x => x.HeadLevel == 4 && x.HierarchyCode.StartsWith(Hierarchy)).ToList();
                }
                else if (Jvtype == "PV" && DrCr == "2")
                {
                    data = ent.GL_AccSubGroups.Where(x => x.HeadLevel == 4 && x.HierarchyCode.StartsWith(Hierarchy)).ToList();
                }
                else if (Jvtype == "RV" && DrCr == "1")
                {
                    data = ent.GL_AccSubGroups.Where(x => x.HeadLevel == 4 && x.HierarchyCode.StartsWith(Hierarchy)).ToList();
                }
                string sql = "select AccSubGruupID, AccSubGroupName from GL_AccSubGroups where status =1";
                //string sql = "select AccSubGruupID, AccSubGroupName from GL_AccSubGroups where status =1 and HierarchyCode not like '11%'";
                //string sql = "select AccSubGruupID , AccSubGroupName from GL_AccSubGroups where HeadLevel=4 and HierarchyCode not like '1.320%' and Status=1";
                List<AccSubLoad> AccList = ent.Database.SqlQuery<AccSubLoad>(sql).ToList();
                if (Jvtype == "JV")
                {
                    foreach (var item in AccList)
                    {
                        ddlList.Add(new SelectListItem
                        {
                            Text = item.AccSubGroupName,
                            Value = Convert.ToString(item.AccSubGruupID)
                        });
                    }
                }
                else if (Jvtype == "PV" && DrCr == "1")
                {
                    foreach (var item in AccList)
                    {
                        ddlList.Add(new SelectListItem
                        {
                            Text = item.AccSubGroupName,
                            Value = Convert.ToString(item.AccSubGruupID)
                        });
                    }
                }
                else if (Jvtype == "RV" && DrCr == "2")
                {
                    foreach (var item in AccList)
                    {
                        ddlList.Add(new SelectListItem
                        {
                            Text = item.AccSubGroupName,
                            Value = Convert.ToString(item.AccSubGruupID)
                        });
                    }
                }
                else
                {
                    foreach (var item in data)
                    {
                        ddlList.Add(new SelectListItem { Text = item.AccSubGroupName, Value = item.AccSubGruupID.ToString() });
                    }
                }
                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
