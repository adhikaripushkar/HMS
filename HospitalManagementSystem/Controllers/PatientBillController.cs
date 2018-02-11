using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class PatientBillController : Controller
    {
        //
        // GET: /PatientBill/

        public ActionResult Index()
        {
            List<CentralizedBillingModel> modellist = new List<CentralizedBillingModel>();
            return View(modellist);
        }
        [HttpPost]
        public ActionResult Index(string name, int? id)
        {
            EHMSEntities ent = new EHMSEntities();

            List<CentralizedBillingModel> modellist = new List<CentralizedBillingModel>();
            if (id != null)
            {
                var list = (from cb in ent.CentralizedBillings
                            where cb.PatientId == id
                            where cb.PatientLogId == ent.CentralizedBillings.Where(x => x.PatientId == id).Max(x => x.PatientLogId)
                            select cb).ToList();
                foreach (var item in list)
                {
                    CentralizedBillingModel model = new CentralizedBillingModel();
                    model.AccountHeadId = item.AccountHeadId;
                    model.AmountDate = item.AmountDate;
                    model.Amount = item.Amount;
                    if (ent.SetupFeeTypes.Where(x => x.SetupFeeTypeId == item.AccountHeadId).SingleOrDefault().ParentAccountId == 3)
                    {
                        model.Amount = -item.Amount;
                    }
                    model.PatientId = item.PatientId;
                    modellist.Add(model);
                }
            }
            else if (name != "")
            {
                var list = (from cb in ent.CentralizedBillings
                            join r in ent.OpdMasters
                            on cb.PatientId equals r.OpdID
                            where r.FirstName.Trim() + " " + r.MiddleName.Trim() + " " + r.LastName.Trim() == name
                            where cb.PatientLogId == ent.CentralizedBillings.Where(x => x.PatientId == r.OpdID).Max(x => x.PatientLogId)
                            select cb).ToList();
                foreach (var item in list)
                {
                    CentralizedBillingModel model = new CentralizedBillingModel();
                    model.AccountHeadId = item.AccountHeadId;
                    model.AmountDate = item.AmountDate;
                    model.Amount = item.Amount;
                    if (ent.SetupFeeTypes.Where(x => x.SetupFeeTypeId == item.AccountHeadId).SingleOrDefault().ParentAccountId == 3)
                    {
                        model.Amount = -item.Amount;
                    }
                    model.PatientId = item.PatientId;
                    modellist.Add(model);
                }
            }
            return PartialView("BillDetail", modellist);
        }
        //
        // GET: /PatientBill/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PatientBill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PatientBill/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PatientBill/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PatientBill/Edit/5

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
        // GET: /PatientBill/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PatientBill/Delete/5

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

        public ActionResult AutocompletePatientName(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();
            var result = (from r in ent.OpdMasters
                          where (r.FirstName.Trim() + " " + r.MiddleName.Trim() + " " + r.LastName.Trim()).Contains(s)
                          select new { r.FirstName, r.MiddleName, r.LastName }).Distinct();
            foreach (var item in result)
            {
                names.Add(item.FirstName.Trim() + " " + item.MiddleName.Trim() + " " + item.LastName.Trim());
            }
            return Json(names, JsonRequestBehavior.AllowGet);
        }
    }
}
