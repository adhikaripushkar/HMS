using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem;

namespace Hospital.Controllers
{
    [Authorize]
    public class DoctorTypeSetupController : Controller
    {
        //
        // GET: /DoctorTypeSetup/

        DoctorTypeSetupProvider pro = new DoctorTypeSetupProvider();

        public ActionResult Index()
        {

            DoctorTypeSetupModel obj = new DoctorTypeSetupModel();
            obj.lstOfDoctorTypeSetupModel = pro.GetListofDoctorTypeSetupModel();
            return View(obj);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(DoctorTypeSetupModel model)
        {
            int i = pro.insert(model);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
            }
            else
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
            }

            return RedirectToAction("Index");

        }


        public ActionResult Edit(int id)
        {

            EHMSEntities ent = new EHMSEntities();
            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            model = pro.GetListofDoctorTypeSetupModel().Where(x => x.DoctorTypeId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(DoctorTypeSetupModel model)
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
            var objToDelete = ent.DoctorTypeSetups.Where(x => x.DoctorTypeId == id).FirstOrDefault();
            // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
            ent.DoctorTypeSetups.Remove(objToDelete);

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
            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            model = pro.GetListofDoctorTypeSetupModel().Where(x => x.DoctorTypeId == id).FirstOrDefault();
            return View(model);
        }



    }
}
