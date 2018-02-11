using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DailyJvDetailController : Controller
    {
        //
        // GET: /DailyJvDetail/
        DailyJvDetailProvider pro = new DailyJvDetailProvider();

        public ActionResult Index(int? id)
        {
            if (id == 1)
            {
                ViewBag.mid = 1;
            }
            else if (id == 2)
            {
                ViewBag.mid = 2;
            }
            else ViewBag.mid = 0;
            return View();
        }
        //
        // GET: /DailyJvDetail/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DailyJvDetail/Create

        public ActionResult Create()
        {
            EHMSEntities ent = new EHMSEntities();

            if (ent.GL_Transaction.Where(x => x.TransactionDate.Year == DateTime.Now.Year && x.TransactionDate.Month == DateTime.Now.Month && x.TransactionDate.Day == DateTime.Now.Day).Any() == false)
            {
                int msg = 1;
                return RedirectToAction("Index/" + msg);
            }
            else if (ent.DailyJvDetails.Where(x => x.Date == DateTime.Today).Any())
            {
                int msg = 2;
                return RedirectToAction("Index/" + msg);
            }
            DailyJvDetailModel model = new DailyJvDetailModel();
            model.JvDetailList = new List<DailyJvDetailModel>();
            model.Date = DateTime.Today;
            model.JvNo = ent.SetupVoucherNumbers.FirstOrDefault().VoucherNo.ToString();

            model.HospitalDetail = new SetUpHospitalDetailModel();
            var obj = ent.SetupHospitalDetails.FirstOrDefault();
            AutoMapper.Mapper.Map(obj, model.HospitalDetail);

            var gtlist = (from gt in ent.GL_Transaction
                          where (gt.TransactionDate.Year == model.Date.Year && gt.TransactionDate.Month == model.Date.Month && gt.TransactionDate.Day == model.Date.Day)
                          group gt by gt.Narration1 into grouplist

                          select new { headname = grouplist.Key, headtotal = grouplist.Sum(x => x.Amount) }).ToList();
            foreach (var item in gtlist)
            {
                DailyJvDetailModel md = new DailyJvDetailModel();
                md.AccountHead = item.headname;
                md.CrAmount = item.headtotal;
                model.JvDetailList.Add(md);
            }



            return View(model);
        }

        //
        // POST: /DailyJvDetail/Create

        [HttpPost]
        public ActionResult Create(DailyJvDetailModel model)
        {

            try
            {

                // TODO: Add insert logic here
                pro.Insert(model);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return View();
            }
        }

        //
        // GET: /DailyJvDetail/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DailyJvDetail/Edit/5

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
        // GET: /DailyJvDetail/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DailyJvDetail/Delete/5

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
