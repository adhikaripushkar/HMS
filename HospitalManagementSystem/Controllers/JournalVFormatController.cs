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
    public class JournalVFromatController : Controller
    {
        //
        // GET: /JournalVFromat/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        JournalVFormatProvider JVFPro = new JournalVFormatProvider();
        JournalVFormatModel model = new JournalVFormatModel();
        public ActionResult Index()
        {

            model.ListJournalVFormatModel = JVFPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {


            return View(model);
        }


        [HttpPost]
        public ActionResult Create(JournalVFormatModel model)
        {


            if (ModelState.IsValid)
            {
                EHMSEntities ent = new EHMSEntities();
                var data = ent.JornalVFormats.Where(m => m.JVFormat == model.JVFormat).Select(m => m.JVId).ToList();
                //var dataa = ent.SetupBlock.Where(m => m.BlockName == model.BlockName).Count();
                if (data.Count == 0)
                {
                    int i = JVFPro.Insert(model);
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
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.AlreadyExist;
                    return RedirectToAction("Index");

                }

            }


            return View();

        }
        public ActionResult Edit(int id)
        {

            model = JVFPro.GetList().Where(x => x.JVId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, JournalVFormatModel model)
        {
            if (ModelState.IsValid)
            {

                int i = JVFPro.Update(model);
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

            int i = JVFPro.Delete(id);
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
        //public ActionResult ChageStatusToInactive(int id)
        //{

        //    JVFPro.ChangToInactive(id);
        //    return RedirectToAction("Index");
        //}
        public ActionResult ChageStatus(int id)
        {

            JVFPro.Changestatus(id);
            return RedirectToAction("Index");
        }

    }
}
