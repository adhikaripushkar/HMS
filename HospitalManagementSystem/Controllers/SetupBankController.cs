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
    public class SetupBankController : Controller
    {
        //
        // GET: /SetupBank/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        SetupBankProviders SBPro = new SetupBankProviders();
        SetupBankModel model = new SetupBankModel();

        public ActionResult Index()
        {
            model.SetupBankModelList = SBPro.GetList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetupBankModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    EHMSEntities ent = new EHMSEntities();
                    var data = ent.SetupBanks.Where(m => m.BankName == model.BankName).Select(m => m.BankSetupId).ToList();

                    if (data.Count == 0)
                    {
                        int i = SBPro.Insert(model);
                        if (i == 1)
                        {
                            TempData["success"] = "Record Created Successfully !";

                        }
                        else
                        {
                            TempData["success"] = "Record Creation Failed !";
                        }
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.AlreadyExist;
                    return RedirectToAction("Index");

                }
            }
            return View(model);


        }

        public ActionResult Edit(int id)
        {
            model = SBPro.GetList().Where(x => x.BankSetupId == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult GetNameFromID(int AccID)
        {
            string Name = "";
            using (EHMSEntities ent = new EHMSEntities())
            {
                Name = ent.Database.SqlQuery<string>("select AccSubGroupName from GL_AccSubGroups where AccSubGruupID='" + AccID + "'").FirstOrDefault();
            }
            return Json(new { Name });
        }

        [HttpPost]
        public ActionResult Edit(int id, SetupBankModel model)
        {
            if (ModelState.IsValid)
            {
                int i = SBPro.Update(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Updated Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Updation Failed !";
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            int i = SBPro.Delete(id);
            if (i == 1)
            {
                TempData["success"] = "Record Deleted Successfully !";

            }
            else
            {
                TempData["success"] = "Record Deletion Failed !";
            }
            return RedirectToAction("Index");
        }
    }
}
