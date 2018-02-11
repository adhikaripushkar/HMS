using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class TrialAuditController : Controller
    {
        //
        // GET: /TrialAudit/
        TrialAuditProviders pro = new TrialAuditProviders();
        public ActionResult Index()
        {
            TrialAuditModel model = new TrialAuditModel();
            model.TrialAuditLists = pro.Getlist();
            return View(model);
        }

    }
}
