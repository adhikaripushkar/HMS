using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class SetupBlockController : Controller
    {
        //
        // GET: /SetupBlock/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            SetupBlockProviders SblockPro = new SetupBlockProviders();
            SetupBlockModel model = new SetupBlockModel();
            model.SetupBlockModelList = SblockPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            SetupBlockModel model = new SetupBlockModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetupBlockModel model)
        {
            SetupBlockProviders SblockPro = new SetupBlockProviders();

            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                var data = ent.SetupBlocks.Where(m => m.BlockName == model.BlockName).Select(m => m.BlockId).ToList();
                if (data.Count == 0)
                {
                    int i = SblockPro.Insert(model);
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
                    TempData["success"] = "Block Name Already Exist !";
                    return RedirectToAction("Index");

                }

            }


            return View();

        }
        public ActionResult Edit(int id)
        {
            SetupBlockProviders SblockPro = new SetupBlockProviders();
            SetupBlockModel model = new SetupBlockModel();
            model = SblockPro.GetList().Where(x => x.BlockId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupBlockModel model)
        {
            if (ModelState.IsValid)
            {
                SetupBlockProviders SblockPro = new SetupBlockProviders();
                int i = SblockPro.Update(model);
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
            SetupBlockProviders SblockPro = new SetupBlockProviders();
            int i = SblockPro.Delete(id);
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
            SetupBlockProviders SblockPro = new SetupBlockProviders();
            SblockPro.ChangToInactive(id);
            return RedirectToAction("Index");
        }
        public ActionResult ChageStatusToActive(int id)
        {
            SetupBlockProviders SblockPro = new SetupBlockProviders();
            SblockPro.ChangToActive(id);
            return RedirectToAction("Index");
        }





    }
}
