using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class AboutUsController : Controller
    {
        //
        // GET: /AboutUs/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowChart()
        {
            return View();
        }
        public ActionResult ShowChartDetails()
        {
            return View();
        }

    }
}
