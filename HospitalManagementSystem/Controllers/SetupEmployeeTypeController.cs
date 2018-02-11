using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class SetupEmployeeTypeController : Controller
    {
        SetupEmployeeTypeProvider pro = new SetupEmployeeTypeProvider();
        //
        // GET: /SetupEmployeeType/

        public ActionResult Index()
        {
            SetupEmployeeTypeModel model = new SetupEmployeeTypeModel();
            model.SetupEmployeeTypeList = pro.GetList();
            return View(model);
        }

        // Get: /SetupEmployeeType/Create/
        public ActionResult Create()
        {
            return View();
        }

        //Post: /SetupEmployeeType/Create/
        [HttpPost]
        public ActionResult Create(SetupEmployeeTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro.Insert(model);
                    if (i != 0)
                    {
                        TempData["success"] = UtilityMessage.save;
                    }
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

        //Get: /SetupEmployeeType/Edit/
        public ActionResult Edit(int id)
        {
            SetupEmployeeTypeModel model = pro.GetList().Where(x => x.SetupEmployeeTypeID == id).FirstOrDefault();
            return View(model);
        }

        //Post: /SetupEmployeeType/Edit/
        [HttpPost]
        public ActionResult Edit(SetupEmployeeTypeModel model)
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

        // : SetupEmployeeType/Delete/
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
