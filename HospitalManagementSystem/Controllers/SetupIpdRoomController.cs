using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Provider;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Controllers
{
    public class SetUpIpdRoomController : Controller
    {
        //
        // GET: /SetUpIpdRoom/

        SetUpIpdRoomProvider IpdRoomProvider = new SetUpIpdRoomProvider();

        public ActionResult Index()
        {

            SetUpIpdRoomModel model = new SetUpIpdRoomModel();
            model.SetUpIpdRoomModelList = IpdRoomProvider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {
            SetUpIpdRoomModel model = new SetUpIpdRoomModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetUpIpdRoomModel model)
        {
            SetUpIpdRoomModel modelRoom = new SetUpIpdRoomModel();
            if (ModelState.IsValid)
            {
                int roomType = model.RoomType;
                int roomNubmer = model.RoomNo;

                //count room number according to room type
                int TotalCount = IpdRoomProvider.GetRoomCountByRoomType(roomType, roomNubmer);
                if (TotalCount > 0)
                {
                    TempData["success"] = "This room number is already assign !";
                }
                else
                {
                    int i = IpdRoomProvider.Insert(model);
                    if (i == 1)
                    {
                        TempData["success"] = "Record Created Successfully !";

                    }
                    else
                    {
                        TempData["success"] = "Record Creation Failed !";
                    }

                }
                return RedirectToAction("Index");



            }
            return View(model);


        }






        public ActionResult Edit(int id)
        {
            SetUpIpdRoomModel model = new SetUpIpdRoomModel();
            model = IpdRoomProvider.GetList().Where(x => x.IpdRoomId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpIpdRoomModel model)
        {
            if (ModelState.IsValid)
            {
                int i = IpdRoomProvider.Update(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Updated Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Updation Failed !";
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            int i = IpdRoomProvider.Delete(id);
            if (i == 1)
            {
                TempData["success"] = "Record Deleted Successfully !";

            }
            else
            {
                TempData["success"] = "Record Deletion Failed !";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id, SetUpIpdRoomModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetUpIpdRoom, SetUpIpdRoomModel>();
                });

                //AutoMapper.Mapper.CreateMap<SetUpIpdRoom, SetUpIpdRoomModel>();
                var Details = ent.SetUpIpdRooms.Where(x => x.IpdRoomId == id).FirstOrDefault();
                SetUpIpdRoomModel det = AutoMapper.Mapper.Map<SetUpIpdRoom, SetUpIpdRoomModel>(Details);
                return View(det);

            }



        }

    }
}
