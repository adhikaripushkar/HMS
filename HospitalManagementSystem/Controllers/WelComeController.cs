using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class WelComeController : Controller
    {
        //
        // GET: /WelCome/

        public ActionResult Index()
        {
            string EmpName = HospitalManagementSystem.Utility.GetCurrentUserNameFromTable();
            TempData["welcome"] = "Welcome To The Hospital Management System ";
            TempData["UserName"] = EmpName;


            return View();
        }

    }
}
