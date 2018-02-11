using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Provider;
using System.IO;//for file
using AutoMapper;

namespace HospitalManagementSystem.Controllers
{
    public class SetUpHospitalDetailController : Controller
    {


        SetUpHospitalDetailProvider HospitalDetailProvider = new SetUpHospitalDetailProvider();

        public ActionResult Index()
        {

            SetUpHospitalDetailModel model = new SetUpHospitalDetailModel();
            model.SetUpHospitalDetailList = HospitalDetailProvider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {
            SetUpHospitalDetailModel model = new SetUpHospitalDetailModel();
            return View(model);
        }



        //string subPath ="ImagesPath"; // your code goes here
        //bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
        //if(!IsExists)
        //    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));



        [HttpPost]
        public ActionResult Create(SetUpHospitalDetailModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {

                    string mapPath = "~/Content/HospitalLogo/";
                    bool IsExists = System.IO.Directory.Exists(Server.MapPath(mapPath));
                    if (!IsExists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(mapPath));

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(mapPath), fileName);

                    model.ImageLogoName = fileName;
                    model.FilePath = path;
                    file.SaveAs(path);


                }

                int i = HospitalDetailProvider.Insert(model);

                if (i == 1)
                {
                    TempData["success"] = "Record Created Successfully !";

                }
                else
                {
                    TempData["success"] = "Record Insertion failed !";
                }
                return RedirectToAction("Index");
            }

            return View(model);


        }

        public ActionResult Edit(int id)
        {
            SetUpHospitalDetailModel model = new SetUpHospitalDetailModel();
            model = HospitalDetailProvider.GetList().Where(x => x.DetailsId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpHospitalDetailModel model, HttpPostedFileBase file)
        {
            // string str="MMTH_Logo .jpg";
            if (ModelState.IsValid)
            {

                if (file != null)
                {


                    string mapPath = "~/Content/HospitalLogo/";
                    bool IsExists = System.IO.Directory.Exists(Server.MapPath(mapPath));
                    if (!IsExists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(mapPath));

                    string[] files = System.IO.Directory.GetFiles(Server.MapPath(mapPath));
                    foreach (string pathFile in files)
                    {
                        System.IO.File.Delete(pathFile);
                    }




                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(mapPath), fileName);
                    model.ImageLogoName = fileName;
                    model.FilePath = path;
                    int i = HospitalDetailProvider.Update(model);
                    if (i == 1)
                    {
                        TempData["success"] = "Record Updated Successfully !";
                        file.SaveAs(path);

                    }
                    else
                    {
                        TempData["fail"] = "Update Failed";
                    }
                }
                else
                {

                    if (model.ImageLogoName == "RemovedClick")
                    {
                        model.FilePath = null;
                        model.ImageLogoName = null;

                        int i = HospitalDetailProvider.Update(model);

                        if (i != 0)
                        {

                            TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;

                            string mapPath = "~/Content/HospitalLogo/";
                            bool IsExists = System.IO.Directory.Exists(Server.MapPath(mapPath));
                            if (IsExists == true)
                            {

                                string[] files = System.IO.Directory.GetFiles(Server.MapPath(mapPath));
                                foreach (string pathFile in files)
                                {
                                    System.IO.File.Delete(pathFile);
                                }
                            }

                        }

                    }

                    else
                    {


                        int y = HospitalDetailProvider.UpdatewithoutImage(model);

                        if (y != 0)
                        {
                            TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;

                        }
                        else
                        {
                            TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                        }
                    }


                }


                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {

            int i = HospitalDetailProvider.Delete(id);
            if (i == 1)
            {

                string[] files = System.IO.Directory.GetFiles(Server.MapPath("~/Content/HospitalLogo/"));
                foreach (string pathFile in files)
                {
                    System.IO.File.Delete(pathFile);
                }



                TempData["success"] = "Records Deleted Successfully !";

            }
            else
            {
                TempData["fail"] = "Deletion Failed";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupHospitalDetail, SetUpHospitalDetailModel>();
                });


                //AutoMapper.Mapper.CreateMap<SetupHospitalDetails, SetUpHospitalDetailModel>();
                var Details = ent.SetupHospitalDetails.Where(x => x.DetailsId == id).FirstOrDefault();
                SetUpHospitalDetailModel det = AutoMapper.Mapper.Map<SetupHospitalDetail, SetUpHospitalDetailModel>(Details);
                return PartialView("_Details", det);

            }



        }



    }
}