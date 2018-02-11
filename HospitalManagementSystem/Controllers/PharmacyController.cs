using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        //
        // GET: /Pharmacy/


        public ActionResult Main()
        {

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
