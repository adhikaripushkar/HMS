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
    public class StockController : Controller
    {
        //
        // GET: /Stock/
        DistributionProvider pro = new DistributionProvider();
        public ActionResult Main()
        {

            return View();
        }

        public ActionResult Index()
        {
            EHMSEntities ent = new EHMSEntities();
            StockDistributionMasterModel model = new StockDistributionMasterModel();
            model.DistributionMasterList = new List<StockDistributionMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockDistributionMaster>, IEnumerable<StockDistributionMasterModel>>(ent.StockDistributionMasters.Where(x => x.Status == true)));
            foreach (var item in model.DistributionMasterList)
            {
                item.TotalAmount = ent.StockDistributionDetails.Where(y => y.DistributionMasterId == item.DistributionMasterId).Sum(x => x.TotalAmount);
            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var list = ent.StockDistributionDetails.Where(x => x.DistributionMasterId == id).ToList();
            return View(list);
        }

        public ActionResult StockSetupMain()
        {

            return View();


        }

        public ActionResult Print(int id)
        {
            StockDistributionMasterModel model = new StockDistributionMasterModel();

            model.lstOfStockDistributionDetail = new List<StockDistributionDetail>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                model.DistributionMasterList = new List<StockDistributionMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockDistributionMaster>, IEnumerable<StockDistributionMasterModel>>(ent.StockDistributionMasters.Where(x => x.DistributionMasterId == id)));
                foreach (var item in model.DistributionMasterList)
                {
                    item.TotalAmount = ent.StockDistributionDetails.Where(y => y.DistributionMasterId == item.DistributionMasterId).Sum(x => x.TotalAmount);
                }

                model.lstOfStockDistributionDetail = ent.StockDistributionDetails.Where(x => x.DistributionMasterId == id).ToList();
            }

            return View(model);
        }


        public ActionResult UnitSetup()
        {

            return View();


        }

        public ActionResult Distribute()
        {
            EHMSEntities ent = new EHMSEntities();
            StockDistributionMasterModel model = new StockDistributionMasterModel();
            model.BillDate = DateTime.Today;
            try
            {
                var obj = ent.StockDistributionMasters.Where(x => x.DistributionMasterId == ent.StockDistributionMasters.Max(y => y.DistributionMasterId)).SingleOrDefault();
                string[] nums = obj.BillNo.Split('N');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.BillNo = "BN" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.BillNo = "BN001";
            }
            return View(model);


        }
        [HttpPost]
        public ActionResult Distribute(StockDistributionMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
            if (model.StockItemEntryList == null)
            {
                TempData["message"] = "Add items!!";
                return View(model);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                foreach (var item in model.StockItemEntryList)
                {
                    var obj = ent.DepartmentWiseStocks.Where(x => x.DepartmentID == did && x.ItemId == item.StockItemEntryId).SingleOrDefault();
                    if (item.Quantity > obj.Quantity)
                    {
                        TempData["message"] = "Quatity distributed is more than that available in stock!!";
                        return View(model);
                    }
                }
                int id = pro.Insert(model);
                return RedirectToAction("Print", new { id = id });
            }
            else
            {
                TempData["message"] = "Insufficient Data!!";
                return View(model);
            }

        }
        public ActionResult SearchPatient(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> opd = new List<string>();
            var obj = ent.OpdMasters.Where(x => x.OpdID == id).SingleOrDefault();


            if (obj == null)
            {
                opd.Add("");
                opd.Add("");
                opd.Add("");
                opd.Add("");
                opd.Add("");
                opd.Add("");
                opd.Add("");
            }
            else
            {
                opd.Add(obj.FirstName);
                if (obj.MiddleName != null)
                {
                    opd.Add(obj.MiddleName);
                }
                else { opd.Add(""); }
                opd.Add(obj.LastName);
                opd.Add(obj.Address);
                opd.Add(obj.Sex);
                opd.Add(Convert.ToString(obj.AgeYear));
                opd.Add(Convert.ToString(obj.DepartmentId));
                //DepartmentName
                //opd.Add(ent.SetupDepartments.Where(x => x.DeptID == obj.DepartmentId).SingleOrDefault().DeptName);
                opd.Add(HospitalManagementSystem.Utility.DepartmentName(Convert.ToInt32(obj.DepartmentId)));
            }
            return Json(opd, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockReport()
        {
            EHMSEntities ent = new EHMSEntities();
            StockItemMasterModel model = new StockItemMasterModel();
            model.StockItemMasterList = Session["report"] as List<StockItemMasterModel>;
            if (Session["report"] == null)
            {
                model.StockItemMasterList = new List<StockItemMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemMaster>, IEnumerable<StockItemMasterModel>>(ent.StockItemMasters).Where(x => x.Status == true));
            }
            Session["report"] = null;
            return View(model.StockItemMasterList);
        }
        [HttpPost]
        public ActionResult StockReport(int cat, int type, int stock)
        {
            EHMSEntities ent = new EHMSEntities();
            StockItemMasterModel model = new StockItemMasterModel();
            if (cat != 0 && type != 0)
            {
                model.StockItemMasterList = new List<StockItemMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemMaster>, IEnumerable<StockItemMasterModel>>(ent.StockItemMasters).Where(x => x.Status == true && ent.SetupStockItemEntries.Where(y => y.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().StockCategoryId == cat && ent.SetupStockItemEntries.Where(z => z.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().StockItemTypeId == type));
            }
            else if (cat != 0)
            {
                model.StockItemMasterList = new List<StockItemMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemMaster>, IEnumerable<StockItemMasterModel>>(ent.StockItemMasters).Where(x => x.Status == true && ent.SetupStockItemEntries.Where(y => y.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().StockCategoryId == cat));
            }
            else if (type != 0)
            {
                model.StockItemMasterList = new List<StockItemMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemMaster>, IEnumerable<StockItemMasterModel>>(ent.StockItemMasters).Where(x => x.Status == true && ent.SetupStockItemEntries.Where(z => z.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().StockItemTypeId == type));
            }
            else
            {
                model.StockItemMasterList = new List<StockItemMasterModel>(AutoMapper.Mapper.Map<IEnumerable<StockItemMaster>, IEnumerable<StockItemMasterModel>>(ent.StockItemMasters).Where(x => x.Status == true));
            }
            Session["report"] = model.StockItemMasterList;
            if (stock == 2)
            {
                Session["report"] = model.StockItemMasterList.Where(x => x.Quantity <= ent.SetupStockItemEntries.Where(y => y.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().MinStockQuantity).ToList();
            }
            if (stock == 3)
            {
                Session["report"] = model.StockItemMasterList.Where(x => x.Quantity >= ent.SetupStockItemEntries.Where(y => y.StockItemEntryId == x.StockItemEntryId).SingleOrDefault().MaxStockQuantity).ToList();
            }

            return null;

        }

        [HttpPost]
        public ActionResult ItemDistribution()
        {

            StockDistributionMasterModel model = new StockDistributionMasterModel();
            model.StockItemEntryList = new List<StockItemEntry>();
            return PartialView("ItemDistribution");
        }


        [HttpPost]
        public ActionResult GetSum(StockItemPurchaseModel model)
        {

            decimal amount = 0;
            try
            {
                foreach (var item in model.StockItemEntryList)
                {
                    amount = amount + item.Quantity * item.Rate;
                }

                return Json(amount, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReportMenu()
        {
            return View();
        }
        public ActionResult BreakageReturnReport()
        {
            EHMSEntities ent = new EHMSEntities();
            BreakageReturnReportModel model = new BreakageReturnReportModel();
            model.BrkRetReportList = ent.Database.SqlQuery<BreakageReturnReportModel>("BreakageReturnReport").ToList();
            return View(model);
        }

        public ActionResult DepartmentWiseStockReport()
        {
            EHMSEntities ent = new EHMSEntities();
            var list = ent.DepartmentWiseStocks.ToList();
            foreach (var item in list)
            {
                try
                {
                    item.CreatedBy = Convert.ToInt32(ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemId).SingleOrDefault().Quantity);
                }
                catch { }
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult DepartmentWiseStockReport(int? dept, int? item)
        {
            EHMSEntities ent = new EHMSEntities();
            var list = ent.DepartmentWiseStocks.ToList();
            foreach (var items in list)
            {
                try
                {
                    items.CreatedBy = Convert.ToInt32(ent.StockItemMasters.Where(x => x.StockItemEntryId == items.ItemId).SingleOrDefault().Quantity);
                }
                catch { }
            }
            if (dept != null && item != null)
            {
                list = list.Where(x => x.ItemId == item && x.DepartmentID == dept).ToList();
            }
            else if (dept != null)
            {
                list = list.Where(x => x.DepartmentID == dept).ToList();
            }
            else if (item != null)
            {
                list = list.Where(x => x.ItemId == item).ToList();
            }
            return View(list);
        }
        public ActionResult ItemGroupSetup()
        {


            return View();
        }

        public ActionResult GroupSetup()
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            model = pro.GetGroupDetails();
            return View(model);
        }

        public ActionResult SubGroupSetup()
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            model = pro.GetGroupDetails();
            return View(model);

        }


        public ActionResult ShowGroupingItemIndex()
        {
            StockItemModel model = new StockItemModel();
            StockItemProvider pro = new StockItemProvider();
            model = pro.GetGroupingItemDetailList();
            return View(model);

        }

        public ActionResult CreateGroupSetup()
        {
            StockItemModel model = new StockItemModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateGroupSetup(StockItemModel model)
        {
            //Frist check if number already inserted or not

            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemGroupSetups.Where(x => x.Status == true).ToList();
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {

                        bool isInRange = HospitalManagementSystem.Utility.CheckValueInRange((int)item.FromId, (int)item.ToId, (int)model.ObjStockItemGroupSetupViewModel.FromId);
                        bool isInRangeToId = HospitalManagementSystem.Utility.CheckValueInRange((int)item.FromId, (int)item.ToId, (int)model.ObjStockItemGroupSetupViewModel.ToId);
                        if (isInRange == true || isInRangeToId == true)
                        {
                            TempData["message"] = "This number is already assigned to another group";
                            return RedirectToAction("CreateGroupSetup", model);
                        }
                    }

                }

            }


            StockItemProvider pro = new StockItemProvider();
            pro.InsertGroupDetails(model);
            if (ModelState.IsValid)
            {

            }
            else
            {
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateVal.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        var exception = error.Exception;
                        // You may log the errors if you want
                    }
                }
            }
            return RedirectToAction("GroupSetup");
        }

        public ActionResult EditGroupDetails(int id)
        {
            StockItemModel model = new StockItemModel();
            StockItemProvider pro = new StockItemProvider();
            model = pro.GetGroupDetails();
            model.ObjStockItemGroupSetupViewModel = model.StockItemGroupSetupViewModelList.Where(x => x.StockGroupingId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditGroupDetails(StockItemModel model)
        {

            StockItemProvider pro = new StockItemProvider();
            pro.UpdateGroupDetails(model);
            return RedirectToAction("GroupSetup");
        }

        public ActionResult DeleteStockGroup(int id)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.DeleteStockGroup(id);
            return RedirectToAction("GroupSetup");
        }


        public ActionResult LocationSetup()
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            model = pro.LocationDetails();
            return View(model);
        }
        public ActionResult CreateLocationSetup()
        {
            StockItemModel model = new StockItemModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLocationSetup(StockItemModel model)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.InsertLocationDetails(model);
            return RedirectToAction("LocationSetup");
        }
        public ActionResult UdpateLocationDetails(int id)
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            model = pro.LocationDetails();
            model.ObjStockItemLocationViewModel = model.StockItemLocationViewModelList.Where(x => x.StockItemLocationId == id).FirstOrDefault();
            return View(model);

        }
        [HttpPost]
        public ActionResult UdpateLocationDetails(StockItemModel model)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.UpdateLocationDetails(model);
            return RedirectToAction("LocationSetup");

        }
        public ActionResult DeleteLocation(int id)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.DeleteStockLocation(id);
            return RedirectToAction("LocationSetup");
        }


        public ActionResult RackSetup()
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            model = pro.RackDetails();
            return View(model);
        }

        public ActionResult CreateRackSetup()
        {
            StockItemModel model = new StockItemModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateRackSetup(StockItemModel model)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.InsertRackDetails(model);
            return RedirectToAction("RackSetup");
        }
        public ActionResult EditRackSetup(int id)
        {
            StockItemModel model = new StockItemModel();
            StockItemProvider pro = new StockItemProvider();
            model = pro.RackDetails();
            model.ObjStockItemRackViewModel = model.StockItemRackViewModelList.Where(x => x.StockItemRackSetupId == id).FirstOrDefault();
            return View(model);

        }
        [HttpPost]
        public ActionResult EditRackSetup(StockItemModel model)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.UpdateRackDetails(model);
            return RedirectToAction("RackSetup");
        }

        public ActionResult DeleteRackSetup(int id)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.DeleteStockRack(id);
            return RedirectToAction("RackSetup");
        }

        public ActionResult ItemGroupDetailSetup()
        {
            StockItemProvider pro = new StockItemProvider();
            StockItemModel model = new StockItemModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult ItemGroupDetailSetup(StockItemModel model)
        {
            StockItemProvider pro = new StockItemProvider();
            pro.InsertItemGroupDetails(model);
            return View(model);

        }

        public ActionResult ShowGroupingItemDetails(int id, int itemId)
        {
            StockItemModel model = new StockItemModel();
            StockItemProvider pro = new StockItemProvider();
            model = pro.GetGroupingItemDetails(id, itemId);
            return View(model);
        }

        public ActionResult StockPurchaseReport()
        {
            return View();
        }

        public ActionResult StockItemPurchaseReport()
        {
            EHMSEntities ent = new EHMSEntities();
            ItemPurchaseReportModel model = new ItemPurchaseReportModel();
            StockItemPurchaseProvider SIEP = new StockItemPurchaseProvider();
            model.ItemPurchaseReportList = SIEP.GetItemPurchaseReport().ToList();

            return View(model);
        }


    }
}
