using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class SetupFloorController : Controller
    {
        //
        // GET: /SetupFloor/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            SetupFloorProviders SFloorPro = new SetupFloorProviders();

            SetupFloorModel model = new SetupFloorModel();
            model.SetupFloorModelList = SFloorPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            SetupFloorModel model = new SetupFloorModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetupFloorModel model)
        {
            SetupFloorProviders SFloorPro = new SetupFloorProviders();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                var data = ent.SetupFloors.Where(m => m.FloorName == model.FloorName).Select(m => m.FloorId).ToList();
                if (data.Count == 0)
                {
                    int i = SFloorPro.Insert(model);
                    if (i != null)
                    {

                        TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    TempData["success"] = "Floor Name Already Exist !";
                }
                return RedirectToAction("Index");

            }


            return View();

        }
        public ActionResult Edit(int id)
        {
            SetupFloorProviders SFloorPro = new SetupFloorProviders();
            SetupFloorModel model = new SetupFloorModel();
            model = SFloorPro.GetList().Where(x => x.FloorId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupFloorModel model)
        {
            if (ModelState.IsValid)
            {
                SetupFloorProviders SFloorPro = new SetupFloorProviders();
                int i = SFloorPro.Update(model);
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
            SetupFloorProviders SFloorPro = new SetupFloorProviders();
            int i = SFloorPro.Delete(id);
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
        public ActionResult ChageStatusToInactive(int id)
        {
            SetupFloorProviders SFloorPro = new SetupFloorProviders();
            SFloorPro.ChangToInactive(id);
            return RedirectToAction("Index");
        }
        public ActionResult ChageStatusToActive(int id)
        {
            SetupFloorProviders SFloorPro = new SetupFloorProviders();
            SFloorPro.ChangToActive(id);
            return RedirectToAction("Index");
        }


    }
}
