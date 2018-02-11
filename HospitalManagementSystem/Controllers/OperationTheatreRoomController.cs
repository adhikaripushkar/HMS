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
    public class OperationTheatreRoomController : Controller
    {
        //
        // GET: /OperationTheatreRoom/
        OperationTheatreRoomProvider pro = new OperationTheatreRoomProvider();
        public ActionResult Index()
        {
            OperationTheatreRoomModel model = new OperationTheatreRoomModel();
            model.OperationTheatreRoomList = pro.GetList();
            return View(model);
        }

        //
        // GET: /OperationTheatreRoom/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /OperationTheatreRoom/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OperationTheatreRoom/Create

        [HttpPost]
        public ActionResult Create(OperationTheatreRoomModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    int i = pro.Insert(model);
                    if (i == 0)
                    {
                        TempData["message"] = "Value with this name already exists in database.";
                        return View(model);
                    }
                    else
                    {
                        TempData["success"] = UtilityMessage.save;
                        return RedirectToAction("Index");
                    }
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
        // GET: /OperationTheatreRoom/Edit/5

        public ActionResult Edit(int id)
        {
            OperationTheatreRoomModel model = new OperationTheatreRoomModel();
            model = pro.GetObject(id);
            return View(model);
        }

        //
        // POST: /OperationTheatreRoom/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, OperationTheatreRoomModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    pro.Update(model);
                    TempData["success"] = UtilityMessage.edit;
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
        // GET: /OperationTheatreRoom/Delete/5

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            TempData["success"] = UtilityMessage.delete;
            return RedirectToAction("Index");
        }

        //
        // POST: /OperationTheatreRoom/Delete/5

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
