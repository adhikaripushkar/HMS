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
    public class SetupOtherDiscountController : Controller
    {
        SetupOtherDiscountProvider pro = new SetupOtherDiscountProvider();
        //
        // GET: /SetupOtherDiscount/

        public ActionResult Index()
        {
            SetupOtherDiscountModel model = new SetupOtherDiscountModel();
            model.SetupOtherDiscountList = pro.GetList();
            return View(model);
        }

        //Get: /SetupOtherDiscount/Create/
        public ActionResult Create()
        {
            return View();
        }

        //Post: /SetupOtherDiscount/Create/
        [HttpPost]
        public ActionResult Create(SetupOtherDiscountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro.Insert(model);
                    if (i != 0)
                        TempData["success"] = UtilityMessage.save;
                    else
                        TempData["success"] = UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

        //Get: /SetupOtherDiscount/Edit
        public ActionResult Edit(int id)
        {
            SetupOtherDiscountModel model = pro.GetList().Where(x => x.DiscountID == id).FirstOrDefault();
            return View(model);
        }

        //Post: /SetupOtherDiscount/Edit
        [HttpPost]
        public ActionResult Edit(SetupOtherDiscountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro.Update(model);
                    if (i != 0)
                        TempData["success"] = UtilityMessage.edit;
                    else
                        TempData["success"] = UtilityMessage.editfailed;
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

        // ... /SetupOtherDiscount/Delete/
        public ActionResult Delete(int id)
        {
            try
            {
                int i = pro.Delete(id);
                if (i != 0)
                    TempData["success"] = UtilityMessage.delete;
                else
                    TempData["success"] = UtilityMessage.deletefailed;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

    }
}
