using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class RelationshipController : Controller
    {
        //
        // GET: /Relationship/
        RelationshipProvider pro = new RelationshipProvider();
        public ActionResult Index()

        {
            RelationshipModel model = new RelationshipModel();
            model.RelationshipList = pro.GetList();
            return View(model);
        }

        //
        // GET: /Relationship/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Relationship/Create

        public ActionResult Create()
        {
            RelationshipModel model = new RelationshipModel();
            return View(model);
        }

        //
        // POST: /Relationship/Create

        [HttpPost]
        public ActionResult Create(RelationshipModel model)
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
                        TempData["message"] = "Value with this name already exists in Records.";
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
        // GET: /Relationship/Edit/5

        public ActionResult Edit(int id)
        {
            RelationshipModel model = new RelationshipModel();
            model = pro.GetObject(id);
            return View(model);
        }

        //
        // POST: /Relationship/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, RelationshipModel model)
        {
            try
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
            catch
            {
                return View();
            }
        }

        //
        // GET: /Relationship/Delete/5

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            TempData["success"] = "Record Deleted Successfully !";
            return RedirectToAction("Index");
        }

        //
        // POST: /Relationship/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AutocompleteRelationship(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> relations = new List<string>();
            var units = ent.SetupRelationships.Where(x => x.RelationName.Contains(s) && x.Status == "A").ToList();
            foreach (var item in units)
            {
                relations.Add(item.RelationName);
            }
            return Json(relations, JsonRequestBehavior.AllowGet);
        }

    }
}
