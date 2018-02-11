using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {
        //
        // GET: /Security/

        public ActionResult Main()
        {
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);
            var employeeId = user.EmployeeId;
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRole()
        {
            var roles = Roles.GetAllRoles();
            return View(roles);
        }


        public ActionResult RoleCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(string RoleName)
        {

            Roles.CreateRole(Request.Form["RoleName"]);
            // ViewBag.ResultMessage = "Role created successfully !";

            return RedirectToAction("CreateRole", "Security");
        }

        public ActionResult RoleDelete(string RoleName)
        {

            Roles.DeleteRole(RoleName);
            // ViewBag.ResultMessage = "Role deleted succesfully !";


            return RedirectToAction("CreateRole", "Security");
        }
        public ActionResult RoleAddToUser()
        {
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string RoleName, string UserName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                ViewBag.ResultMessage = "This user already has the role specified !";
            }
            else
            {
                Roles.AddUserToRole(UserName, RoleName);
                ViewBag.ResultMessage = "Username added to the role succesfully !";
            }

            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
                SelectList list = new SelectList(Roles.GetAllRoles());
                ViewBag.Roles = list;
            }
            return View("RoleAddToUser");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                Roles.RemoveUserFromRole(UserName, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;


            return View("RoleAddToUser");
        }




    }
}
