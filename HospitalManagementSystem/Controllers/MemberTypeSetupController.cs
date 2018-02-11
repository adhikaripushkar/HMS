using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagmentSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class MemberTypeSetupController : Controller
    {
        //
        // GET: /MemberTypeSetup/
        MemberTypeSetupProvider pro = new MemberTypeSetupProvider();
        public ActionResult Index()
        {
            MemberTypeModel model = new MemberTypeModel();
            model.memberTypes = pro.GetList();
            return View(model);
        }



        //
        // GET: /MemberTypeSetup/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MemberTypeSetup/Create

        public ActionResult Create()
        {
            MemberTypeModel model = new MemberTypeModel();
            return View(model);
        }

        //
        // POST: /MemberTypeSetup/Create

        [HttpPost]
        public ActionResult Create(MemberTypeModel model)
        {
            int flag;
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    flag = pro.Insert(model);
                    if (flag == 1)
                    {
                        TempData["success"] = "Record Created Successfully !";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Value with this name already exists in Records !";
                        return View(model);
                    }
                }
                else return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MemberTypeSetup/Edit/5

        public ActionResult Edit(int id)
        {
            MemberTypeModel model = new MemberTypeModel();
            model = pro.GetObject(id);
            model.date = model.ValidUpto.ToString();
            return View(model);
        }

        //
        // POST: /MemberTypeSetup/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, MemberTypeModel model)
        {

            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                pro.Update(model);
                TempData["success"] = "Record Updated Successfully !";
                return RedirectToAction("Index");
            }
            else return View(model);

        }

        //
        // GET: /MemberTypeSetup/Delete/5

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            TempData["success"] = "Record Deleted Successfully !";
            return RedirectToAction("Index");
        }

        //
        // POST: /MemberTypeSetup/Delete/5

        public ActionResult AutocompleteMemberType(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> MemberType = new List<string>();
            var units = ent.SetupMemberTypes.Where(x => x.MemberTypeName.Contains(s) && x.Status == "A").ToList();
            foreach (var item in units)
            {
                MemberType.Add(item.MemberTypeName);
            }
            return Json(MemberType, JsonRequestBehavior.AllowGet);
        }
    }
}
