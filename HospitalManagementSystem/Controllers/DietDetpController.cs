using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class DietDetpController : Controller
    {
        //
        // GET: /DietDetp/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            //DietIPDModel model = new DietIPDModel();
            return View();
        }

    }
}
