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
    [Authorize]
    public class SetUpHospitalFeeController : Controller
    {
        SetUpIpdHospitalFeeProvider HospitalFeeProvider = new SetUpIpdHospitalFeeProvider();

        public ActionResult Index()
        {

            SetUpHospitalFeeModel model = new SetUpHospitalFeeModel();
            model.SetUpHospitalFeeModelList = HospitalFeeProvider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {

            SetUpHospitalFeeModel model = new SetUpHospitalFeeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetUpHospitalFeeModel model)
        {
            if (ModelState.IsValid)
            {
                int i = HospitalFeeProvider.Insert(model);
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
            SetUpHospitalFeeModel model = new SetUpHospitalFeeModel();
            model = HospitalFeeProvider.GetList().Where(x => x.FeeId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpHospitalFeeModel model)
        {
            if (ModelState.IsValid)
            {
                int i = HospitalFeeProvider.Update(model);
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
            int i = HospitalFeeProvider.Delete(id);
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

        public ActionResult Details(int id, SetUpIpdRoomModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetupHospitalFee, SetUpHospitalFeeModel>();
                });


                //AutoMapper.Mapper.CreateMap<SetupHospitalFee, SetUpHospitalFeeModel>();
                var Details = ent.SetupHospitalFees.Where(x => x.FeeId == id).FirstOrDefault();
                SetUpHospitalFeeModel det = AutoMapper.Mapper.Map<SetupHospitalFee, SetUpHospitalFeeModel>(Details);
                return View(det);

            }



        }
        public ActionResult ChageStatus(int id, int type)
        {
            HospitalFeeProvider.ChangToCurrent(id, type);
            return RedirectToAction("Index");
        }

    }
}
