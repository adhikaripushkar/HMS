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
    public class SetUpIpdWardController : Controller
    {
        //
        // GET: /SetUpIpdWard/

        SetUpIpdWardProvider Wardprovider = new SetUpIpdWardProvider();

        public ActionResult Index()
        {

            SetUpIpdWardModel model = new SetUpIpdWardModel();
            model.SetUpIpdWardModelList = Wardprovider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {
            SetUpIpdWardModel model = new SetUpIpdWardModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetUpIpdWardModel model)
        {
            if (ModelState.IsValid)
            {
                int i = Wardprovider.Insert(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Created Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Creation Failed !";
                }
                return RedirectToAction("Index");
            }

            return View(model);


        }










        public ActionResult Edit(int id)
        {
            SetUpIpdWardModel model = new SetUpIpdWardModel();
            model = Wardprovider.GetList().Where(x => x.IpdWardId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpIpdWardModel model)
        {
            if (ModelState.IsValid)
            {
                int i = Wardprovider.Update(model);
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

            int i = Wardprovider.Delete(id);
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



        public ActionResult Details(int id, SetUpIpdWardModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupIpdWard, SetUpIpdWardModel>();
                });

                //AutoMapper.Mapper.CreateMap<SetupIpdWard, SetUpIpdWardModel>();
                var Details = ent.SetupIpdWards.Where(x => x.IpdWardId == id).FirstOrDefault();
                SetUpIpdWardModel det = AutoMapper.Mapper.Map<SetupIpdWard, SetUpIpdWardModel>(Details);
                return View(det);

            }



        }




    }
}
