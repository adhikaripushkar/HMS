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
    public class MemberTypeDiscountController : Controller
    {
        //
        // GET: /MemberTypeDiscount/
        MemberTypeDiscountProvider pro = new MemberTypeDiscountProvider();
        public ActionResult Index()
        {
            MemberTypeDiscountModel model = new MemberTypeDiscountModel();
            model.memberDiscountTypes = pro.GetList();
            return View(model);
        }

        //
        // GET: /MemberTypeDiscount/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MemberTypeDiscount/Create

        public ActionResult Create()
        {
            MemberTypeDiscountModel model = new MemberTypeDiscountModel();
            return View(model);
        }

        //
        // POST: /MemberTypeDiscount/Create

        [HttpPost]
        public ActionResult Create(MemberTypeDiscountModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    pro.Insert(model);
                    TempData["success"] = "Record Created Successfully !";
                    return RedirectToAction("Index");
                }
                else return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MemberTypeDiscount/Edit/5

        public ActionResult Edit(int id)
        {
            MemberTypeDiscountModel model = new MemberTypeDiscountModel();
            model = pro.GetObject(id);
            return View(model);
        }

        //
        // POST: /MemberTypeDiscount/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, MemberTypeDiscountModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    pro.Update(model);
                    TempData["success"] = "Record Updated Successfully !";
                    return RedirectToAction("Index");
                }
                else return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MemberTypeDiscount/Delete/5

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            TempData["success"] = "Record Deleted Successfully !";
            return RedirectToAction("Index");
        }

        //
        // POST: /MemberTypeDiscount/Delete/5

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
