using System;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception, bool isAjaxRequet)
        {
            Response.StatusCode = statusCode;

            if (!isAjaxRequet)
            {
                ErrorModel model = new ErrorModel { HttpStatusCode = statusCode, Exception = exception };

                return View(model);
            }
            else
            {

                var errorObjet = new { message = exception.Message };
                return Json(errorObjet, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
