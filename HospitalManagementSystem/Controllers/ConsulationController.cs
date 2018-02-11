using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Provider;

namespace HospitalManagementSystem.Controllers
{
    public class ConsulationController : Controller
    {
        //
        // GET: /Consulation/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Create(int id)
        {


            ConsulationModel obj = new ConsulationModel();
            obj.EmergencyMasterId = id;

            return PartialView("_Create", obj);
        }
        [HttpPost]
        public ActionResult Create(ConsulationModel model)
        {
            if (ModelState.IsValid)
            {
                ConsulationProvider cp = new ConsulationProvider();
                cp.Insertvitals(model);

                int id = (int)model.EmergencyMasterId;
                model.ListConsulationModels = cp.GetSelectedData(id);
            }
            return PartialView("_Index", model);


        }
        public ActionResult Index(int id)
        {
            ConsulationModel model = new ConsulationModel();
            ConsulationProvider cp = new ConsulationProvider();
            model.ListConsulationModels = cp.GetSelectedData(id);
            model.EmergencyMasterId = id;
            return PartialView("_Index", model);
        }
        public ActionResult Edit(int id)
        {

            ConsulationModel model = new ConsulationModel();
            ConsulationProvider cp = new ConsulationProvider();


            model = cp.GetListForConsulation().Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(ConsulationModel model)
        {
            ConsulationProvider cp = new ConsulationProvider();
            if (ModelState.IsValid)
            {
                cp.Update(model);
                model.ListConsulationModels = cp.GetSelectedData((int)model.EmergencyMasterId);
            }
            return PartialView("_Index", model);

        }

    }
}
