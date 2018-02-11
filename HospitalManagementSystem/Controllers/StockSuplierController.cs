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
    public class StockSupplierController : Controller
    {
        //
        // GET: /StockSupplier/
        StockSupplierProvider pro = new StockSupplierProvider();
        public ActionResult Index()
        {
            StockSupplierModel model = new StockSupplierModel();
            model.StockSupplierList = pro.GetList();
            return View(model);
        }

        //
        // GET: /StockSupplier/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /StockSupplier/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StockSupplier/Create

        [HttpPost]
        public ActionResult Create(StockSupplierModel model)
        {
            int i;
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    i = pro.Insert(model);

                    if (i != 0)
                    {

                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        return RedirectToAction("Index");
                    }


                    TempData["message"] = "Record with this name already exists in database!";
                    return View(model);
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /StockSupplier/Edit/5

        public ActionResult Edit(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            StockSupplierModel model = new StockSupplierModel();
            var obj = ent.SetupStockSuppliers.Where(x => x.StockSupplierID == id).SingleOrDefault();
            AutoMapper.Mapper.Map(obj, model);
            return View(model);
        }

        //
        // POST: /StockSupplier/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, StockSupplierModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    EHMSEntities ent = new EHMSEntities();
                    var obj = ent.SetupStockSuppliers.Where(x => x.StockSupplierID == model.StockSupplierID).SingleOrDefault();
                    model.CreatedBy = obj.CreatedBy;
                    model.Status = obj.Status;
                    model.CreatedDate = obj.CreatedDate;
                    AutoMapper.Mapper.Map(model, obj);
                    ent.Entry(obj).State = System.Data.EntityState.Modified;
                    int i = ent.SaveChanges();
                    if (i != 0)
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;

                    }
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /StockSupplier/Delete/5

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SetupStockSuppliers.Where(x => x.StockSupplierID == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;

            }
            return RedirectToAction("Index");
        }

        //
        // POST: /StockSupplier/Delete/5

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

        public ActionResult AutocompleteSupplier(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> Suppliers = new List<string>();
            var list = ent.SetupStockSuppliers.Where(x => x.StockSupplierName.Contains(s) && x.Status == true).ToList();
            foreach (var item in list)
            {
                Suppliers.Add(item.StockSupplierName);
            }
            return Json(Suppliers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {
            StockSupplierModel model = new StockSupplierModel();
            model.StockSupplierList = pro.GetList();
            return View(model);
        }

    }
}
