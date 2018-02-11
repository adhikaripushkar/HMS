using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HospitalManagementSystem.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary 
                               {
                                   { "action", "NotAuthorized" },
                                   { "controller", "Home" }
                               });
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }
}