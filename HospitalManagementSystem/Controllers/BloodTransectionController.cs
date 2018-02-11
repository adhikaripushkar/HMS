using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class BloodTransectionController : Controller
    {
        BloodTransectionProvider transectionPro = new BloodTransectionProvider();

        public ActionResult Main()
        {

            return View();
        }



        public ActionResult Index()
        {
            BloodTransectionModel model = new BloodTransectionModel();
            model.BloodTransectionLists = transectionPro.GetBloodTransectionLists();
            return View(model);
        }

        public ActionResult Create()
        {
            BloodTransectionModel model = new BloodTransectionModel();
            //model.ShowBloodStockDetailsModelList = transectionPro.GetTotalBloodStockLists();
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(BloodTransectionModel model)
        {
            if (HospitalManagementSystem.Utility.GetTotalBloodQuantity(model.BloodTypeId) > model.ObjBTDetails.QuantityUnit && HospitalManagementSystem.Utility.GetTotalBloodMl(model.BloodTypeId) > model.ObjBTDetails.QuantityML)
            {

                if (ModelState.IsValid)
                {
                    transectionPro.Insert(model);
                    return RedirectToAction("Index");
                }

            }
            ViewBag.errorsOccurred = "Quantity is greater than in stock value!";
            return View(model);


        }

    }
}
