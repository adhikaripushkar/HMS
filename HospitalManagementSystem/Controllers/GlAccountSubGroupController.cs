using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class GlAccountSubGroupController : Controller
    {
        //
        // GET: /GlAccountSubGroup/

        GlAccountSubGroupProvider pro = new GlAccountSubGroupProvider();
        public ActionResult Index()
        {
            GlAccountSubGroupModel model = new GlAccountSubGroupModel();
            model.GlAccountSubGroupModelList = pro.GetAccountGroup();
            return View(model);
        }

        public ActionResult SearchIndex(string AccSubGroupName)
        {
            GlAccountSubGroupModel model = new GlAccountSubGroupModel();
            string accountAccSubGroupName = string.Empty;
            if (!string.IsNullOrEmpty(AccSubGroupName))
            {
                AccSubGroupName = AccSubGroupName.ToLower();
                model.GlAccountSubGroupModelList = pro.GetAccountGroup().Where(x => x.AccSubGroupName.ToLower().StartsWith(AccSubGroupName)).ToList();

            }
            return View("Index", model);
        }

        public ActionResult Create()
        {
            GlAccountSubGroupModel model = new GlAccountSubGroupModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(GlAccountSubGroupModel model)
        {
            if (ModelState.IsValid)
            {
                pro.Insert(model);
            }
            return RedirectToAction("Create");
        }

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            GlAccountSubGroupModel model = new GlAccountSubGroupModel();
            model = pro.GetAllAccountGroup().Where(x => x.AccSubGruupID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, GlAccountSubGroupModel model)
        {
            pro.Edit(id, model);
            return RedirectToAction("Index");
        }

        public ActionResult GetEmployeeFromID(int EmpID)
        {
            string EmployeeName = "";

            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select EmployeeFullName as AccSubGroupName from SetupEmployeeMaster where Status=1 and EmployeeMasterId ='" + EmpID + "'";
                var data = ent.Database.SqlQuery<GlAccountSubGroupModel>(sql).ToList();
                foreach (var item in data)
                {
                    EmployeeName = item.AccSubGroupName;
                }
            }

            return Json(new { EmployeeName });
        }

        public ActionResult GetAccHeadID(string Name)
        {
            int AccountHeadId = 0;
            int SalaryPayableAcHeadID = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccHeadID as AccountHeadId from TBLConfigDetail where Description ='Staff Advance' and sStatus='A'";
                var data = ent.Database.SqlQuery<GlAccountSubGroupModel>(sql).ToList();
                foreach (var item in data)
                {
                    AccountHeadId = item.AccountHeadId;
                }

                string sql1 = @"select AccHeadID as AccountHeadId from TBLConfigDetail where Description ='Salary Payable' and sStatus='A'";
                var data1 = ent.Database.SqlQuery<GlAccountSubGroupModel>(sql1).ToList();
                foreach (var item in data1)
                {
                    SalaryPayableAcHeadID = item.AccountHeadId;
                }
            }
            return Json(new { AccountHeadId, SalaryPayableAcHeadID });
        }

        public ActionResult DetailOfCOA()
        {
            GlAccountSubGroupModel model = new GlAccountSubGroupModel();
            return View(model);

        }

    }
}
