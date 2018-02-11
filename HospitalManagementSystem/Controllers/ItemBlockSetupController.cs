using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class ItemBlockSetupController : Controller
    {
        //
        // GET: /ItemBlockSetup/

        public ActionResult Index()
        {
            ItemBlockSetupProvider IBSP = new ItemBlockSetupProvider();
            ItemBlockSetupModel model = new ItemBlockSetupModel();
            model.ItemBlockSetupList = IBSP.GetList();
            return View(model);
        }


        public ActionResult Create()
        {
            ItemBlockSetupModel model = new ItemBlockSetupModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ItemBlockSetupModel model)
        {
            ItemBlockSetupProvider IBSP = new ItemBlockSetupProvider();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();

                int i = IBSP.Insert(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ItemBlockSetupProvider IBSP = new ItemBlockSetupProvider();
            ItemBlockSetupModel model = new ItemBlockSetupModel();
            model = IBSP.GetList().Where(x => x.ItemBlcokSetupID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, ItemBlockSetupModel model)
        {
            if (ModelState.IsValid)
            {
                ItemBlockSetupProvider IBSP = new ItemBlockSetupProvider();
                int i = IBSP.Update(model);
                if (i != null)
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                    return RedirectToAction("Index");

                }
                //return View(model);
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            ItemBlockSetupProvider IBSP = new ItemBlockSetupProvider();
            int i = IBSP.Delete(id);
            if (i != null)
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
                return RedirectToAction("Index");

            }
        }
    }




}
