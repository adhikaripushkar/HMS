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
    public class SetupMedicalChargeController : Controller
    {
        //
        // GET: /SetupMedicalCharge/

        SetupMedicalChargeProvider pro = new SetupMedicalChargeProvider();

        public ActionResult Index()
        {

            SetupMedicalChargeModel model = new SetupMedicalChargeModel();

            model.lstofSetupMedicalChargeModel = pro.GetlistOfMedicalCharge();

            return View(model);

        }

        public ActionResult Create()
        {
            SetupMedicalChargeModel model = new SetupMedicalChargeModel();

            return View();
        }

        [HttpPost]
        public ActionResult Create(SetupMedicalChargeModel model)
        {



            SetupMedicalChargeModel obj = new SetupMedicalChargeModel();

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
            SetupMedicalChargeModel model = new SetupMedicalChargeModel();
            model = pro.GetlistOfMedicalCharge().Where(x => x.MedicalChargeId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(SetupMedicalChargeModel model)
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
            var objToDelete = ent.SetupMedicalcharges.Where(x => x.MedicalChargeId == id).FirstOrDefault();
            // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
            ent.SetupMedicalcharges.Remove(objToDelete);

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
            SetupMedicalChargeModel model = new SetupMedicalChargeModel();
            model = pro.GetlistOfMedicalCharge().Where(x => x.MedicalChargeId == id).FirstOrDefault();
            return View(model);
        }

    }
}
