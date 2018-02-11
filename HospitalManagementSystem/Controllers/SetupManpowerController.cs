using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Controllers
{
    public class SetupManpowerController : Controller
    {

        // GET: /SetupManpower/

        SetupManpowerProvider pro = new SetupManpowerProvider();

        public ActionResult Index()
        {
            SetupManpowerModel model = new SetupManpowerModel();

            model.lstofSetupManpowerModel = pro.GetlistOfMainpower();

            return View(model);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(SetupManpowerModel model)
        {



            SetupManpowerModel obj = new SetupManpowerModel();

            int i = pro.Insert(model);



            if (i != 0)
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


        public ActionResult Edit(int id)
        {

            EHMSEntities ent = new EHMSEntities();
            SetupManpowerModel model = new SetupManpowerModel();
            model = pro.GetlistOfMainpower().Where(x => x.ManpowerId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(SetupManpowerModel model)
        {

            int i = pro.Update(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                return RedirectToAction("Index");

            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                return RedirectToAction("Index");
            }


        }

        public ActionResult Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupManpowers.Where(x => x.ManpowerId == id).FirstOrDefault();
            // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
            ent.SetupManpowers.Remove(objToDelete);

            int i = ent.SaveChanges();
            if (i != 0)
            {

                TempData["success"] = UtilityMessage.delete;

            }
            else
            {

                TempData["success"] = UtilityMessage.deletefailed;
            }

            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            SetupManpowerModel model = new SetupManpowerModel();
            model = pro.GetlistOfMainpower().Where(x => x.ManpowerId == id).FirstOrDefault();
            return View(model);
        }




    }
}
