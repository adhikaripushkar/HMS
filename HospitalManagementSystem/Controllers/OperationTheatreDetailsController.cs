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
    public class OperationTheatreDetailsController : Controller
    {
        //
        // GET: /OperationTheatreDetails/
        OperationTheatreDetailsProvider pro = new OperationTheatreDetailsProvider();
        public ActionResult Index()
        {
            OperationTheatreDetailsModel model = new OperationTheatreDetailsModel();
            model.OperationTheatreDetalisList = pro.GetList();
            return View(model);
        }

        //
        // GET: /OperationTheatreDetails/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /OperationTheatreDetails/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OperationTheatreDetails/Create

        [HttpPost]
        public ActionResult Create(OperationTheatreDetailsModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return View();

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /OperationTheatreDetails/Edit/5

        public ActionResult Edit(int id)
        {
            OperationTheatreDetailsModel model = new OperationTheatreDetailsModel();
            model = pro.GetObject(id);
            return View(model);
        }

        //
        // POST: /OperationTheatreDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, OperationTheatreDetailsModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    pro.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /OperationTheatreDetails/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /OperationTheatreDetails/Delete/5

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
    }
}
