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
    public class SetupRoomTypeController : Controller
    {
        SetupRoomTypeProvider pro = new SetupRoomTypeProvider();
        //
        // GET: /SetupRoomType/

        public ActionResult Index()
        {
            SetupRoomTypeModel model = new SetupRoomTypeModel();
            model.SetupRoomTypeList = pro.GetList();
            return View(model);
        }

        //Get: /SetupRoomType/Create/
        public ActionResult Create()
        {
            return View();
        }

        //Post: /SetupRoomType/Create/
        [HttpPost]
        public ActionResult Create(SetupRoomTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro.Insert(model);
                    if (i != 0)
                        TempData["success"] = UtilityMessage.save;
                    else
                        TempData["success"] = UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

        //Get: /SetupRoomType/Edit
        public ActionResult Edit(int id)
        {
            SetupRoomTypeModel model = pro.GetList().Where(x => x.RoomTypeId == id).FirstOrDefault();
            return View(model);
        }

        //Post: /SetupRoomType/Edit
        [HttpPost]
        public ActionResult Edit(SetupRoomTypeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro.Update(model);
                    if (i != 0)
                        TempData["success"] = UtilityMessage.edit;
                    else
                        TempData["success"] = UtilityMessage.editfailed;
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

        // ... /SetupRoomType/Delete/
        public ActionResult Delete(int id)
        {
            try
            {
                int i = pro.Delete(id);
                if (i != 0)
                    TempData["success"] = UtilityMessage.delete;
                else
                    TempData["success"] = UtilityMessage.deletefailed;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["success"] = "Error occur";
                return RedirectToAction("Index");
            }
        }

    }
}
