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
    public class SetUpMemberShipController : Controller
    {
        //
        // GET: /SetUpMemberShip/


        public ActionResult GetMemberShipListWithValue(string id)
        {


            SetUpMemberShipProvider pro = new SetUpMemberShipProvider();

            SetUpMemberShipModel obj = new SetUpMemberShipModel();

            obj.lstOfsetSetUpMemberShipModel = pro.GetlistWithName(id);

            foreach (var item in obj.lstOfsetSetUpMemberShipModel)
            {

                using (EHMSEntities ent = new EHMSEntities())
                {

                    item.cnt = (from x in ent.SetupMemberDependents
                                where x.SetupMemberID == item.SetupMemberID
                                select x).ToList().Count();

                    item.cnt = item.MaximumDependent - item.cnt;

                }

            }


            return PartialView("GetMemberShipList", obj);


        }


        public ActionResult GetMemberShipList()
        {

            SetUpMemberShipModel obj = new SetUpMemberShipModel();

            SetUpMemberShipProvider pro = new SetUpMemberShipProvider();

            obj.lstOfsetSetUpMemberShipModel = pro.Getlist();
            foreach (var item in obj.lstOfsetSetUpMemberShipModel)
            {

                using (EHMSEntities ent = new EHMSEntities())
                {

                    item.cnt = (from x in ent.SetupMemberDependents
                                where x.SetupMemberID == item.SetupMemberID
                                select x).ToList().Count();

                    item.cnt = item.MaximumDependent - item.cnt;

                }

            }

            return PartialView(obj);


        }

        //public ActionResult Index(int? page)
        //{
        //    SetUpMemberShipModel obj = new SetUpMemberShipModel();

        //    SetUpMemberShipProvider pro = new SetUpMemberShipProvider();
        //    obj.lstOfsetSetUpMemberShipModel = pro.Getlist();
        //    var pageNumber = page ?? 1;
        //    var onePageOfProducts = obj.lstOfsetSetUpMemberShipModel.ToPagedList(pageNumber, 25);
        //    ViewBag.OnePageOfProducts = onePageOfProducts;
        //    return View(obj);
        //}






        public ActionResult Index(int? page)
        {

            SetUpMemberShipModel obj = new SetUpMemberShipModel();

            SetUpMemberShipProvider pro = new SetUpMemberShipProvider();

            obj.lstOfsetSetUpMemberShipModel = pro.Getlist();

            var pageNumber = page ?? 1;
            //var onePageOfProducts = obj.lstOfsetSetUpMemberShipModel.ToPagedList(pageNumber, 25);

            //ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(obj);

        }


        public ActionResult Create()
        {

            SetUpMemberShipModel model = new SetUpMemberShipModel();
            model.CountryID = 153;

            return View(model);
        }


        [HttpPost]
        public ActionResult Create(SetUpMemberShipModel model)
        {


            if (ModelState.IsValid)
            {
                HospitalManagementSystem.Providers.SetUpMemberShipProvider obj = new SetUpMemberShipProvider();

                int i = obj.Insert(model);

                if (i != 0)
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }



            }
            return View("Index");

        }



        public ActionResult Edit(int id)
        {

            SetUpMemberShipModel model = new SetUpMemberShipModel();

            SetUpMemberShipProvider obj = new SetUpMemberShipProvider();

            model = obj.GetMemberShipWithId(id);


            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(SetUpMemberShipModel model)
        {

            if (ModelState.IsValid)
            {
                SetUpMemberShipProvider obj = new SetUpMemberShipProvider();
                int i = obj.Update(model);

                if (i != 0)
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.edit;
                    return RedirectToAction("Index");
                }
                else
                {

                    TempData["success"] = HospitalManagementSystem.UtilityMessage.editfailed;
                    return RedirectToAction("Index");
                }


            }


            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {

            SetUpMemberShipModel model = new SetUpMemberShipModel();

            SetUpMemberShipProvider obj = new SetUpMemberShipProvider();
            model = obj.GetDetailsOfMembershipWithId(id);

            return View(model);

        }


        public ActionResult Delete(int Id)
        {
            SetUpMemberShipProvider pro = new SetUpMemberShipProvider();
            int i = pro.Delete(Id);
            if (i != 0)
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;

            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
            }
            return RedirectToAction("Index");

        }

    }
}
