using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        public ActionResult LoginPage()
        {


            return PartialView("_LoginCustom");

        }


        public ActionResult LoginCustom()
        {

            LoginModel obj = new LoginModel();
            obj = (LoginModel)TempData["Model"];

            return PartialView("_LoginCustom", obj);

        }

        [HttpPost]
        public ActionResult LoginCustom(LoginModel obj)
        {
            if (obj.UserName == "admin" && obj.Password == "admin")
            {
                return RedirectToAction("ShowDashBoard");
            }

            TempData["success"] = "Please Enter Valied User Name and Password";
            TempData["Model"] = obj;
            return RedirectToAction("LoginCustom");

        }
        public ActionResult ShowDashBoard()
        {
            return View();
        }

    }
}