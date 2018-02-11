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
    public class SetupBillingTypeController : Controller
    {
        SetupBillingTypeProvider pro = new SetupBillingTypeProvider();
        //
        // GET: /SetupBillingType/

        public ActionResult Index()
        {
            SetupBilllingTypeModel model = new SetupBilllingTypeModel();
            model.SetupBillingTypeList = pro.GetList();
            return View(model);
        }

        //Get: /SetupBillingType/Create/
        public ActionResult Create()
        {
            return View();
        }

        //Post: /SetupBillingType/Create/
        [HttpPost]
        public ActionResult Create(SetupBilllingTypeModel model)
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

        //Get: /SetupBillingType/Edit
        public ActionResult Edit(int id)
        {
            SetupBilllingTypeModel model = pro.GetList().Where(x => x.SetupBillingTypeID == id).FirstOrDefault();
            return View(model);
        }

        //Post: /SetupBillingType/Edit
        [HttpPost]
        public ActionResult Edit(SetupBilllingTypeModel model)
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

        // ... /SetupBillingType/Delete/
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
