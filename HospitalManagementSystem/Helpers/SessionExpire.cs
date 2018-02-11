using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagement.SecurityAttributes
{
    public class CheckSessionExpire : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext cc = HttpContext.Current;
            UserEnvironmentVariables us = new UserEnvironmentVariables();

            if (!us.isUserLoggedOn())
            {
                string val = cc.Request.Url.PathAndQuery;
                string urlHelper = new UrlHelper(filterContext.RequestContext).RouteUrl(new { controller = "Account", action = "LogIn", ReturnUrl = val, area = "" });
                if ((filterContext.HttpContext.Request.IsAjaxRequest()) || (cc.Response.StatusCode == 302) || (cc.Request.Headers["X-Requested-With"] == "XMLHttpRequest"))
                {
                    cc.Response.Clear();
                    cc.Response.StatusCode = Convert.ToInt32(401); //401:Unauthorized
                    cc.Response.End();
                }
                else
                {
                    cc.Response.Redirect(urlHelper);
                }

            }
            else
            {

                // Do nothing
            }
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}