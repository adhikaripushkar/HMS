using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagement.SecurityAttributes
{
    public class UserEnvironmentVariables
    {
        public void SetLogOn(int userId,string UserName)
        {
            HttpContext.Current.Session["UserName"] = UserName;
            HttpContext.Current.Session["UserId"] = userId;
            //HttpContext.Current.Session["DepartmentId"] = Utility.GetDepartmentId(userId);

            //string usernm = HttpContext.current.Session["UserId"].ToString();
        }

        public bool isUserLoggedOn()
        {
            if (HttpContext.Current.Session["UserName"] != null || HttpContext.Current.Session["UserId"] != null)
                return true;
            else
                return false;
        }
    }
}