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
    public class SetUpHospitalFeeDiscountController : Controller
    {
        SetUpHospitalFeeDiscountProvider HospitalFeeDiscountProvider = new SetUpHospitalFeeDiscountProvider();

        public ActionResult Index()
        {

            SetUpHospitalFeeDiscountModel model = new SetUpHospitalFeeDiscountModel();
            model.SetUpHospitalFeeDiscountModelList = HospitalFeeDiscountProvider.GetList();
            return View(model);
        }

        public ActionResult Create()
        {

            SetUpHospitalFeeDiscountModel model = new SetUpHospitalFeeDiscountModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SetUpHospitalFeeDiscountModel model)
        {
            if (ModelState.IsValid)
            {
                int i = HospitalFeeDiscountProvider.Insert(model);
                if (i == 1)
                {
                    TempData["success"] = "Record Created Successfully !";

                }
                else
                {
                    TempData["fail"] = "Record Creation Failed !";
                }
                return RedirectToAction("Index");
            }
            return View(model);


        }

        public ActionResult Edit(int id)
        {
            SetUpHospitalFeeDiscountModel model = new SetUpHospitalFeeDiscountModel();
            model = HospitalFeeDiscountProvider.GetList().Where(x => x.FeeDiscountId == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, SetUpHospitalFeeDiscountModel model)
        {
            if (ModelState.IsValid)
            {
                int i = HospitalFeeDiscountProvider.Update(model);
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
            int i = HospitalFeeDiscountProvider.Delete(id);
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
        public ActionResult Details(int id, SetUpHospitalFeeDiscountModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetUpHospitalFeeModel, SetUpHospitalFeeDiscountModel>();
                });

                //AutoMapper.Mapper.CreateMap<SetUpHospitalFeeModel, SetUpHospitalFeeDiscountModel>();
                var Details = ent.SetupHospitalFeeDiscounts.Where(x => x.FeeDiscountId == id).FirstOrDefault();
                SetUpHospitalFeeDiscountModel det = AutoMapper.Mapper.Map<SetupHospitalFeeDiscount, SetUpHospitalFeeDiscountModel>(Details);
                return View(det);

            }



        }


    }
}
