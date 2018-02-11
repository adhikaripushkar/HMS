using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class ReturnOutController : Controller
    {
        //
        // GET: /ReturnInOut/

        public ActionResult Index()
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnOutModel model = new StockReturnOutModel();
            StockReturnOutModel model2 = new StockReturnOutModel();
            model2.ReturnOutList = new List<StockReturnOutModel>();
            model.ReturnOutList = new List<StockReturnOutModel>(AutoMapper.Mapper.Map<IEnumerable<StockReturnOut>, IEnumerable<StockReturnOutModel>>(ent.StockReturnOuts.Where(x => x.Status == true)));
            foreach (var item in model.ReturnOutList)
            {
                if (model2.ReturnOutList.Any(s => s.ReturnOutNo == item.ReturnOutNo) == false)
                {
                    model2.ReturnOutList.Add(item);
                }
            }
            model.ReturnOutList = model2.ReturnOutList;
            return View(model);
        }

        //
        // GET: /ReturnInOut/Details/5

        public ActionResult Details(string id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnOutModel model = new StockReturnOutModel();


            model.ReturnOutList = new List<StockReturnOutModel>(AutoMapper.Mapper.Map<IEnumerable<StockReturnOut>, IEnumerable<StockReturnOutModel>>(ent.StockReturnOuts.Where(x => x.Status == true && x.ReturnOutNo == id)));
            return View(model);
        }

        //
        // GET: /ReturnInOut/Create

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnOutModel model = new StockReturnOutModel();
            model.ReturnOutDate = DateTime.Today;
            try
            {
                var obj = ent.StockReturnOuts.Where(x => x.ReturnOutId == ent.StockReturnOuts.Max(y => y.ReturnOutId)).SingleOrDefault();
                string[] nums = obj.ReturnOutNo.Split('O');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.ReturnOutNo = "RO" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.ReturnOutNo = "RO001";
            }
            return View(model);
        }

        //
        // POST: /ReturnInOut/Create

        [HttpPost]
        public ActionResult Create(StockReturnOutModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            try
            {
                // TODO: Add insert logic here
                foreach (var item in model.ReturnOutList)
                {
                    if (item.ReturnOutQty != 0)
                    {
                        StockReturnOut obj = new StockReturnOut();
                        var objitem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemId).SingleOrDefault();
                        obj.PurchaseId = item.PurchaseId;
                        obj.PurchaseOrderNo = item.PurchaseOrderNo;
                        obj.ItemId = item.ItemId;
                        obj.ReturnOutQty = item.ReturnOutQty;
                        obj.ReturnOutDate = model.ReturnOutDate;
                        obj.SupplierId = item.SupplierId;
                        obj.Remarks = model.Remarks;
                        obj.ReturnOutNo = model.ReturnOutNo;
                        obj.Status = true;
                        obj.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                        obj.CreatedDate = DateTime.Today;
                        ent.StockReturnOuts.Add(obj);
                        objitem.Quantity = objitem.Quantity - item.ReturnOutQty;
                        ent.Entry(objitem).State = System.Data.EntityState.Modified;
                    }
                }
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ReturnInOut/Edit/5

        public ActionResult Edit(int id, int newValue)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockReturnOuts.Where(x => x.ReturnOutId == id).SingleOrDefault();
            var objitem = ent.StockItemMasters.Where(x => x.StockItemEntryId == obj.ItemId).SingleOrDefault();
            objitem.Quantity = objitem.Quantity + obj.ReturnOutQty - newValue;
            obj.ReturnOutQty = newValue;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.Entry(objitem).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return Json("Successfully Saved!!", JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ReturnInOut/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ReturnInOut/Delete/5

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockReturnOuts.Where(x => x.ReturnOutId == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /ReturnInOut/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SearchPurchase(string SupplierId, string PurchaseOrderNo, string stockentryno, string billno, string dateto, string fromdate)
        {
            EHMSEntities ent = new EHMSEntities();
            StockReturnOutModel model = new StockReturnOutModel();
            model.ReturnOutList = new List<StockReturnOutModel>();

            if (dateto != "" && fromdate != "")
            {
                DateTime df = Convert.ToDateTime(fromdate);
                DateTime dt = Convert.ToDateTime(dateto);
                df = df.AddDays(-1);
                dt = dt.AddDays(1);
                var objlist = (from pm in ent.StockItemPurchases
                               join pd in ent.StockItemPurchaseDetails
                                   on pm.ItemPurchaseId equals pd.ItemPurchaseId
                               where pm.CreatedDate >= df && pm.CreatedDate <= dt
                               select new { pm, pd }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnOutModel obj = new StockReturnOutModel();
                    obj.ItemId = item.pd.StockItemEntryId;
                    obj.PurchaseId = item.pd.ItemPurchaseId;
                    obj.SupplierId = item.pm.StockSupplierId;
                    obj.PurchaseOrderNo = item.pm.ItemOrderId;
                    obj.ReturnOutQty = item.pd.Quantity;
                    model.ReturnOutList.Add(obj);
                }
            }
            else if (PurchaseOrderNo != "")
            {
                var objlist = (from pm in ent.StockItemPurchases
                               join pd in ent.StockItemPurchaseDetails
                                   on pm.ItemPurchaseId equals pd.ItemPurchaseId
                               where pm.ItemOrderId == PurchaseOrderNo
                               select new { pm, pd }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnOutModel obj = new StockReturnOutModel();
                    obj.ItemId = item.pd.StockItemEntryId;
                    obj.PurchaseId = item.pd.ItemPurchaseId;
                    obj.SupplierId = item.pm.StockSupplierId;
                    obj.PurchaseOrderNo = item.pm.ItemOrderId;
                    obj.ReturnOutQty = item.pd.Quantity;
                    model.ReturnOutList.Add(obj);
                }
            }
            else if (stockentryno != "")
            {
                var objlist = (from pm in ent.StockItemPurchases
                               join pd in ent.StockItemPurchaseDetails
                                   on pm.ItemPurchaseId equals pd.ItemPurchaseId
                               where pm.StockEntryNo == stockentryno
                               select new { pm, pd }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnOutModel obj = new StockReturnOutModel();
                    obj.ItemId = item.pd.StockItemEntryId;
                    obj.PurchaseId = item.pd.ItemPurchaseId;
                    obj.SupplierId = item.pm.StockSupplierId;
                    obj.PurchaseOrderNo = item.pm.ItemOrderId;
                    obj.ReturnOutQty = item.pd.Quantity;
                    model.ReturnOutList.Add(obj);
                }
            }
            else if (billno != "")
            {
                int i = Convert.ToInt32(billno);
                var objlist = (from pm in ent.StockItemPurchases
                               join pd in ent.StockItemPurchaseDetails
                                   on pm.ItemPurchaseId equals pd.ItemPurchaseId
                               where pm.ItemBillNo == i
                               select new { pm, pd }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnOutModel obj = new StockReturnOutModel();
                    obj.ItemId = item.pd.StockItemEntryId;
                    obj.PurchaseId = item.pd.ItemPurchaseId;
                    obj.SupplierId = item.pm.StockSupplierId;
                    obj.PurchaseOrderNo = item.pm.ItemOrderId;
                    obj.ReturnOutQty = item.pd.Quantity;
                    model.ReturnOutList.Add(obj);
                }
            }
            else
            {
                int i = Convert.ToInt32(SupplierId);
                var objlist = (from pm in ent.StockItemPurchases
                               join pd in ent.StockItemPurchaseDetails
                                   on pm.ItemPurchaseId equals pd.ItemPurchaseId
                               where pm.StockSupplierId == i
                               select new { pm, pd }).ToList();

                foreach (var item in objlist)
                {
                    StockReturnOutModel obj = new StockReturnOutModel();
                    obj.ItemId = item.pd.StockItemEntryId;
                    obj.PurchaseId = item.pd.ItemPurchaseId;
                    obj.SupplierId = item.pm.StockSupplierId;
                    obj.PurchaseOrderNo = item.pm.ItemOrderId;
                    obj.ReturnOutQty = item.pd.Quantity;
                    model.ReturnOutList.Add(obj);
                }
            }
            model.ReturnOutDate = DateTime.Today;
            try
            {
                string num = ent.StockReturnOuts.Max(x => x.ReturnOutNo);
                string[] nums = num.Split('O');
                RecordNumGen rcnum = new RecordNumGen(nums[1]);

                model.ReturnOutNo = "RO" + rcnum.RecNumGen();
            }
            catch (Exception e)
            {
                model.ReturnOutNo = "RO001";
            }
            return View("Create", model);
        }

        public ActionResult Autocompletes(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            string[] par = s.Split('_');
            s = par[0];
            List<string> items = new List<string>();
            if (par[1] == "1")
            {
                var list = ent.StockItemPurchases.Where(x => x.ItemOrderId.Contains(s) && x.Status == true).ToList();

                foreach (var item in list)
                {
                    items.Add(item.ItemOrderId);
                }
            }
            if (par[1] == "2")
            {
                var list = ent.StockItemPurchases.Where(x => x.StockEntryNo.Contains(s) && x.Status == true).ToList();

                foreach (var item in list)
                {
                    items.Add(item.StockEntryNo);
                }
            }
            if (par[1] == "3")
            {
                var list = ent.StockItemPurchases.Where(x => x.Status == true).ToList();

                foreach (var item in list)
                {
                    if (item.ItemBillNo.ToString().Contains(s))
                    {
                        items.Add(item.ItemBillNo.ToString());
                    }
                }
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

    }
}
