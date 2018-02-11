using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using AutoMapper;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class SetUpEmergencyFeeController : Controller
    {
        //
        // GET: /SetUpEmergencyFee/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        SetUpEmergencyFeeModel model = new SetUpEmergencyFeeModel();
        SetupEmergencyFeeProviders SEFPro = new SetupEmergencyFeeProviders();

        public ActionResult Index()
        {


            model.SetUpEmergencyFeeModelList = SEFPro.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SetUpEmergencyFeeModel model)
        {
            if (ModelState.IsValid)
            {
                int i = SEFPro.Insert(model);
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

            model = SEFPro.GetList().Where(x => x.FeeId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpEmergencyFeeModel model)
        {
            if (ModelState.IsValid)
            {
                int i = SEFPro.Update(model);
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
            int i = SEFPro.Delete(id);
            if (i == 1)
            {
                TempData["success"] = "Records Delated Successfully !";

            }
            else
            {
                TempData["success"] = "Record Deletion Failed !";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id, SetUpEmergencyFeeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupEmergencyFee, SetUpEmergencyFeeModel>();
                });


                //AutoMapper.Mapper.CreateMap<SetupEmergencyFee, SetUpEmergencyFeeModel>();
                var Details = ent.SetupEmergencyFees.Where(x => x.FeeId == id).FirstOrDefault();
                SetUpEmergencyFeeModel det = AutoMapper.Mapper.Map<SetupEmergencyFee, SetUpEmergencyFeeModel>(Details);
                return View(det);

            }



        }
        public ActionResult ChageStatus(int id)
        {
            SEFPro.ChangToCurrent(id);
            return RedirectToAction("Index");
        }

    }
}
