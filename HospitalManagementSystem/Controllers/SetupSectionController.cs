using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Provider;
using AutoMapper;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class SetUpSectionController : Controller
    {
        SetUpSectionProvider SectionProvider = new SetUpSectionProvider();

        public ActionResult Index()
        {

            SetUpSectionModel model = new SetUpSectionModel();
            model.SetUpSectionbModelList = SectionProvider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {

            SetUpSectionModel model = new SetUpSectionModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetUpSectionModel model)
        {
            if (ModelState.IsValid)
            {
                int i = SectionProvider.Insert(model);
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
            SetUpSectionModel model = new SetUpSectionModel();
            model = SectionProvider.GetList().Where(x => x.SectionId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpSectionModel model)
        {
            if (ModelState.IsValid)
            {
                int i = SectionProvider.Update(model);
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
            int i = SectionProvider.Delete(id);
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
        public ActionResult Details(int id, SetUpSectionModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupSection, SetUpSectionModel>();
                });

                //AutoMapper.Mapper.CreateMap<SetupSections, SetUpSectionModel>();
                var Details = ent.SetupSections.Where(x => x.SectionId == id).FirstOrDefault();
                SetUpSectionModel det = AutoMapper.Mapper.Map<SetupSection, SetUpSectionModel>(Details);
                return View(det);

            }



        }

    }
}
