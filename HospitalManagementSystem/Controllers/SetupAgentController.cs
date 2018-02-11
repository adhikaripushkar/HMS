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
    [Authorize]
    public class SetupAgentController : Controller
    {
        //
        // GET: /SetupAgent/

        SetupAgentProvider pro = new SetupAgentProvider();

        public ActionResult Index()
        {
            SetupAgentModel model = new SetupAgentModel();

            model.lstOfSetupAgentModel = pro.GetlistOfSetupAgents();

            return View(model);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(SetupAgentModel model)
        {



            SetupAgentModel obj = new SetupAgentModel();

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
            SetupAgentModel model = new SetupAgentModel();
            model = pro.GetlistOfSetupAgents().Where(x => x.AgentId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(SetupAgentModel model)
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
            var objToDelete = ent.SetupAgents.Where(x => x.AgentId == id).FirstOrDefault();
            // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
            ent.SetupAgents.Remove(objToDelete);

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
            SetupAgentModel model = new SetupAgentModel();
            model = pro.GetlistOfSetupAgents().Where(x => x.AgentId == id).FirstOrDefault();
            return View(model);
        }


    }
}
