
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class StockReturnInController : Controller
    {
        //
        // GET: /StockReturnIn/
        public ActionResult Index()
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnInModel model = new StockReturnInModel();
            model.ReturnInList = new List<StockReturnInModel>(AutoMapper.Mapper.Map<IEnumerable<StockReturnIn>, IEnumerable<StockReturnInModel>>(ent.StockReturnIns.Where(x => x.Status == true)));
            return View(model);
        }
        public ActionResult Create(string id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnInModel model = new StockReturnInModel();
            model.ReturnInList = new List<StockReturnInModel>();
            model.ReturnInDate = DateTime.Today;
            var objlist = (from ro in ent.StockReturnOuts
                           join pm in ent.StockItemPurchases on ro.PurchaseId equals pm.ItemPurchaseId
                           where ro.ReturnOutNo == id
                           select new { ro, pm }).ToList();

            foreach (var item in objlist)
            {
                StockReturnInModel obj = new StockReturnInModel();
                obj.PurchaseId = item.ro.PurchaseId;
                obj.ReturnOutId = item.ro.ReturnOutId;
                obj.ItemId = item.ro.ItemId;
                obj.Remarks = item.ro.ReturnOutNo;
                obj.ReturnInId = item.pm.ItemBillNo;
                obj.ReturnInQty = item.ro.ReturnOutQty;
                model.ReturnInList.Add(obj);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(int? returnoutno, DateTime? fromdate, DateTime? todate, int? SupplierId)
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnInModel model = new StockReturnInModel();
            model.ReturnInList = new List<StockReturnInModel>();
            string df = fromdate.ToString();
            string dt = todate.ToString();
            string retoutno = returnoutno.ToString();
            if (df != "" && dt != "")
            {

                DateTime dafr = Convert.ToDateTime(df);
                DateTime dto = Convert.ToDateTime(dt);
                var objlist = (from ro in ent.StockReturnOuts
                               join pm in ent.StockItemPurchases on ro.PurchaseId equals pm.ItemPurchaseId
                               where ro.ReturnOutDate >= dafr && ro.ReturnOutDate <= dto
                               select new { ro, pm }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnInModel obj = new StockReturnInModel();
                    obj.PurchaseId = item.ro.PurchaseId;
                    obj.ReturnOutId = item.ro.ReturnOutId;
                    obj.ItemId = item.ro.ItemId;
                    obj.Remarks = item.pm.StockEntryNo;
                    obj.ReturnInId = item.pm.ItemBillNo;
                    obj.ReturnInQty = item.ro.ReturnOutQty;
                    model.ReturnInList.Add(obj);
                }
            }
            else if (retoutno != "")
            {

                var objlist = (from ro in ent.StockReturnOuts
                               join pm in ent.StockItemPurchases on ro.PurchaseId equals pm.ItemPurchaseId
                               where ro.ReturnOutNo == retoutno
                               select new { ro, pm }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnInModel obj = new StockReturnInModel();
                    obj.PurchaseId = item.ro.PurchaseId;
                    obj.ReturnOutId = item.ro.ReturnOutId;
                    obj.ItemId = item.ro.ItemId;
                    obj.Remarks = item.pm.StockEntryNo;
                    obj.ReturnInId = item.pm.ItemBillNo;
                    obj.ReturnInQty = item.ro.ReturnOutQty;
                    model.ReturnInList.Add(obj);
                }
            }
            else
            {

                var objlist = (from ro in ent.StockReturnOuts
                               join pm in ent.StockItemPurchases on ro.PurchaseId equals pm.ItemPurchaseId
                               where ro.SupplierId == SupplierId
                               select new { ro, pm }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnInModel obj = new StockReturnInModel();
                    obj.PurchaseId = item.ro.PurchaseId;
                    obj.ReturnOutId = item.ro.ReturnOutId;
                    obj.ItemId = item.ro.ItemId;
                    obj.Remarks = item.pm.StockEntryNo;
                    obj.ReturnInId = item.pm.ItemBillNo;
                    obj.ReturnInQty = item.ro.ReturnOutQty;
                    model.ReturnInList.Add(obj);
                }
            }
            model.ReturnInDate = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveReturnIn(StockReturnInModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            foreach (var item in model.ReturnInList)
            {
                if (item.ReturnInQty != 0)
                {
                    StockReturnIn obj = new StockReturnIn();
                    var itemmaster = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemId).SingleOrDefault();
                    obj.ReturnOutId = item.ReturnOutId;
                    obj.PurchaseId = item.PurchaseId;
                    obj.ItemId = item.ItemId;
                    obj.ReturnInQty = item.ReturnInQty;
                    obj.ReturnInDate = model.ReturnInDate;
                    obj.Remarks = model.Remarks;
                    obj.Status = true;
                    obj.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                    obj.CreatedDate = DateTime.Today;
                    ent.StockReturnIns.Add(obj);
                    itemmaster.Quantity = itemmaster.Quantity + item.ReturnInQty;
                    ent.Entry(itemmaster).State = System.Data.EntityState.Modified;
                }
            }
            ent.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AutocompleteReturnOut(string s)
        {
            EHMSEntities ent = new EHMSEntities();

            List<string> items = new List<string>();

            var list = ent.StockReturnOuts.Where(x => x.ReturnOutNo.Contains(s) && x.Status == true).ToList();

            foreach (var item in list)
            {
                items.Add(item.ReturnOutNo);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockReturnIns.Where(x => x.ReturnInId == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id, int newValue)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockReturnIns.Where(x => x.ReturnInId == id).SingleOrDefault();
            var objitem = ent.StockItemMasters.Where(x => x.StockItemEntryId == obj.ItemId).SingleOrDefault();
            objitem.Quantity = objitem.Quantity - obj.ReturnInQty + newValue;
            obj.ReturnInQty = newValue;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.Entry(objitem).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return Json("Successfully Saved!!", JsonRequestBehavior.AllowGet);
        }

    }
}
