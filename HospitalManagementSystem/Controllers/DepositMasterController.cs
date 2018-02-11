using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System.Drawing;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DepositMasterController : Controller
    {
        //
        // GET: /DepositMaster/
        DepositMasterModel model = new DepositMasterModel();
        DepositMasterProvider pro = new DepositMasterProvider();
        OpdModel modelOpd = new OpdModel();
        OpdProvider proOpd = new OpdProvider();

        //[CustomAuthorize(Roles = "DepositView, DepositAdmin, DepositCreate,IPDView, IPDCreate, IPDAdmin, superadmin, admin")]
        public ActionResult Index(string FromDateString, string TodateString)
        {
            model.DepositMasterModelList = pro.GetAll().Where(x => x.Status == true && x.DepositedDate == DateTime.Today).ToList();
            if (!String.IsNullOrEmpty(FromDateString) && !String.IsNullOrEmpty(TodateString))
            {
                DateTime Dt1 = DateTime.Parse(FromDateString);
                DateTime Dt2 = DateTime.Parse(TodateString);
                model.DepositMasterModelList = pro.GetAll().Where(x => x.Status == true && (x.DepositedDate >= Dt1.Date && x.DepositedDate <= Dt2.Date)).ToList();
            }


            return View(model);
        }

        public ActionResult SearchPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchPatient(int srchVal, string value)
        {
            EHMSEntities ent = new EHMSEntities();
            PatientPartialDetails obj = new PatientPartialDetails();

            if (srchVal == 1)
            {
                try
                {
                    int patientId = Convert.ToInt16(value);
                    PatientPartialDetails modelPartial = new PatientPartialDetails();
                    string str = "";
                    modelPartial.PatientPartialDetailsList = pro.GetDepositForPatient(patientId, str);
                    return PartialView("_DepositPatientSearch", modelPartial);
                }
                catch (Exception e)
                {

                    TempData["msz"] = "Please Check Patient Id";
                    OpdModel model = new OpdModel();
                    return PartialView("_DepositPatientSearch", model);
                }

            }


            if (srchVal == 2)
            {
                PatientPartialDetails modelPartial = new PatientPartialDetails();
                modelPartial.PatientPartialDetailsList = pro.GetDepositForPatient(0, value);


                return PartialView("_DepositPatientSearch", modelPartial);
            }

            return PartialView("_DepositPatientSearch", obj);

        }


        //[CustomAuthorize(Roles = "DepositAdmin, DepositCreate, IPDCreate, IPDAdmin, superadmin, admin")]
        public ActionResult Create(int id, int DeptId)
        {
            DepositMasterModel model = new DepositMasterModel();
            model.objPatientPartialDetail = new PatientPartialDetails();
            PatientPartialDetails objPartialDetails = new PatientPartialDetails();

            if (DeptId == 1000)
            {
                //get opd patient details
                model.objPatientPartialDetail.PatientFullName = pro.GetPatientFullName(DeptId, id);
                objPartialDetails.PatientDepartmentId = pro.GetPatientDepartmentId(id);
                model.PatientDepartmentId = pro.GetPatientDepartmentId(id);



            }
            else
            {
                //get emergency detail

                model.objPatientPartialDetail.PatientFullName = pro.GetPatientFullName(DeptId, id); ;
                model.PatientDepartmentId = 1001;
            }

            //DepositMasterModel model = new DepositMasterModel();
            model.DepartmentId = DeptId;
            model.PatientId = id;

            return View(model);
        }

        public ActionResult DepositeReceipt(int id)
        {
            DepositMasterModel obj = new DepositMasterModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var data = ent.PatientDepositMasters.Where(x => x.PatientDepositMasterId == id).FirstOrDefault();
                obj = AutoMapper.Mapper.Map<PatientDepositMaster, DepositMasterModel>(data);


            }
            return View(obj);

        }


        [HttpPost]
        public ActionResult Create(DepositMasterModel model)
        {
            int lstInsertPatietDepMasId = pro.Insert(model);
            if (lstInsertPatietDepMasId != 0)
            {
                DepositMasterModel obj = new DepositMasterModel();
                using (EHMSEntities ent = new EHMSEntities())
                {

                    var data = ent.PatientDepositMasters.Where(x => x.PatientDepositMasterId == lstInsertPatietDepMasId).FirstOrDefault();
                    obj = AutoMapper.Mapper.Map<PatientDepositMaster, DepositMasterModel>(data);


                }
                TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                return View("DepositeReceipt", obj);


            }
            else
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
            }

            return RedirectToAction("Index");

        }


        //[CustomAuthorize(Roles = "DepositAdmin, DepositCreate, IPDCreate, IPDAdmin, superadmin, admin")]
        public ActionResult Edit(int id)
        {
            DepositMasterModel model = new DepositMasterModel();

            model = pro.GetAll().Where(x => x.PatientDepositMasterId == id).FirstOrDefault();

            if (model.DepartmentId == 1000)
            {
                model.objPatientPartialDetail = new PatientPartialDetails();
                model.objPatientPartialDetail.PatientFullName = pro.GetPatientFullName(1000, model.PatientId);
            }
            else
            {
                model.objPatientPartialDetail = new PatientPartialDetails();
                model.objPatientPartialDetail.PatientFullName = pro.GetPatientFullName(1001, model.PatientId);
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(DepositMasterModel model)
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
        //[CustomAuthorize(Roles = "DepositAdmin, DepositCreate, IPDCreate, IPDAdmin, superadmin, admin")]
        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);
            if (i != 0)
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
