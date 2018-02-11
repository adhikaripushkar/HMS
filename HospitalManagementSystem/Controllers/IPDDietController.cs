using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class IPDDietController : Controller
    {
        //
        // GET: /IPDDiet/


        DietIPDProviders pro = new DietIPDProviders();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DietTypeIndex()
        {
            DietIPDModel model = new DietIPDModel();
            model.DietTypeSetupViewModelList = pro.GetDietTypeList();
            return View(model);
        }

        public ActionResult DietTypeCreate()
        {
            DietIPDModel model = new DietIPDModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult DietTypeCreate(DietIPDModel model)
        {

            pro.InsertDietType(model);
            return RedirectToAction("DietTypeIndex");
        }

        public ActionResult DietTypeEdit(int id)
        {
            DietIPDModel model = new DietIPDModel();
            model.ObjDietTypeSetupViewModel = pro.GetDietTypeList().Where(x => x.DietTypeSetupId == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult DietTypeEdit(DietIPDModel model)
        {
            pro.EditDietType(model);
            return RedirectToAction("DietTypeIndex");
        }

        public ActionResult AddIpdDiet(int id, int ipdid)
        {
            DietIPDModel model = new DietIPDModel();
            model.PatientInformationDetailsViewModeList = pro.PatientInformation(id, ipdid);
            model.PatientDietDetailsViewModelList = pro.GetPatientDietList(id, ipdid);
            model.ObjPatientDietDetailsViewModel.OPDId = id;
            model.ObjPatientDietDetailsViewModel.IPDRegistrationId = ipdid;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddIpdDiet(DietIPDModel model)
        {

            pro.insertPatientDietDetails(model);
            return View(model);
        }





    }
}
