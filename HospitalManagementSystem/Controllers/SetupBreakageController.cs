using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class StockBreakageController : Controller
    {
        //
        // GET: /StockBreakage/

        public ActionResult Index()
        {
            EHMSEntities ent = new EHMSEntities();
            StockItemEntryModel model = new StockItemEntryModel();
            model.StockItemEntryList = new List<StockItemEntryModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockItemEntry>, IEnumerable<StockItemEntryModel>>(ent.SetupStockItemEntries.Where(x => x.Status == true && ent.StockBreakages.Any(y => y.ItemId == x.StockItemEntryId))));

            return View(model);
        }

        //
        // GET: /StockBreakage/Details/5

        public ActionResult Details(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockBreakageModel model = new StockBreakageModel();
            model.BreakageModelList = new List<StockBreakageModel>(AutoMapper.Mapper.Map<IEnumerable<StockBreakage>, IEnumerable<StockBreakageModel>>(ent.StockBreakages.Where(x => x.Status == true && x.ItemId == id)));
            return View(model);
        }

        //
        // GET: /StockBreakage/Create

        public ActionResult Create()
        {

            StockBreakageModel model = new StockBreakageModel();
            model.BreakageEntryDate = DateTime.Today;

            return View(model);
        }

        //
        // POST: /StockBreakage/Create

        [HttpPost]
        public ActionResult Create(StockBreakageModel model, string search)
        {
            EHMSEntities ent = new EHMSEntities();
            if (search != null)
            {
                var objList = (from sie in ent.SetupStockItemEntries
                               join sim in ent.StockItemMasters
                                   on sie.StockItemEntryId equals sim.StockItemEntryId
                               where sie.StockCategoryId == model.CategoryId && sie.StockSubCategoryId == model.SubCategoryId
                               select new { sie, sim });
                model.BreakageModelList = new List<StockBreakageModel>();
                foreach (var item in objList)
                {
                    StockBreakageModel brkmodel = new StockBreakageModel();
                    brkmodel.ItemId = item.sie.StockItemEntryId;
                    brkmodel.TotalQuantity = Convert.ToInt32(item.sim.Quantity);
                    model.BreakageModelList.Add(brkmodel);

                }
                return View(model);
            }
            try
            {
                // TODO: Add insert logic here
                foreach (var item in model.BreakageModelList)
                {
                    if (item.BreakageQuantity != 0)
                    {
                        StockBreakage obj = new StockBreakage();
                        var objItem = ent.StockItemMasters.Where(x => x.StockItemEntryId == item.ItemId).SingleOrDefault();
                        obj.BreakageEntryDate = model.BreakageEntryDate;
                        obj.ItemId = item.ItemId;
                        obj.BreakageQuantity = item.BreakageQuantity;
                        obj.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                        obj.CreatedDate = DateTime.Today;
                        obj.Remarks = model.Remarks;
                        obj.Status = true;
                        ent.StockBreakages.Add(obj);
                        objItem.Quantity = objItem.Quantity - item.BreakageQuantity;
                        ent.Entry(objItem).State = System.Data.EntityState.Modified;

                    }

                }
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        //
        // GET: /StockBreakage/Edit/5

        public ActionResult Edit(int id, int newValue)
        {
            EHMSEntities ent = new EHMSEntities();

            var objbrk = ent.StockBreakages.Where(x => x.StockBreakgaeId == id).SingleOrDefault();
            var obj = ent.StockItemMasters.Where(x => x.StockItemEntryId == objbrk.ItemId).SingleOrDefault();
            obj.Quantity = obj.Quantity + objbrk.BreakageQuantity - newValue;
            objbrk.BreakageQuantity = newValue;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.Entry(objbrk).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return Json("Successfully Saved!!", JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /StockBreakage/Edit/5

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
        // GET: /StockBreakage/Delete/5

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.StockBreakages.Where(x => x.StockBreakgaeId == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return RedirectToAction("Details/" + obj.ItemId);
        }

        //
        // POST: /StockBreakage/Delete/5

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
    }
}
