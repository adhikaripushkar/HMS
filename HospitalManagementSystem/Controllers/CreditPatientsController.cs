//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using HospitalManagementSystem.Models;
//using HospitalManagementSystem.Providers;

//namespace HospitalManagementSystem.Controllers
//{
//    public class CreditPatientsController : Controller
//    {
//        EHMSEntities ent = new EHMSEntities();
//        CreditPatientProvider pro = new CreditPatientProvider();
//        CreditPatientDetailsModel model = new CreditPatientDetailsModel();

//        public ActionResult Index()
//        {
//            model.CreditPatientDetailsModelList = pro.PopulateCreditPatients();
//            return View(model);
//        }
//        public ActionResult Create()
//        {
//            return View(model);
//        }
//        public ActionResult SearchCreditPatients(string CreditPatientName)
//        {
//            CreditPatientDetailsModel model = new CreditPatientDetailsModel();
//            string creditPatientName = string.Empty;
//            if (!string.IsNullOrEmpty(CreditPatientName))
//            {
//                creditPatientName = CreditPatientName.ToLower();
//                model.CreditPatientDetailsModelList = pro.PopulateCreditPatients().Where(x => x.CreditPatientName.ToLower().StartsWith(CreditPatientName)).ToList();

//            }
//            return View("Index", model);
//        }

//        [HttpPost]
//        public ActionResult Create(CreditPatientDetailsModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                if (pro.InsertCreditPatients(model).Successmessage == "Success")
//                {
//                    TempData["MsgFail"] = "Data Has Not Been Saved Successfully";
//                }
//                else
//                {
//                    TempData["MsgError"] = model.ErrorMessage;
//                }
//            }
//            return RedirectToAction("Index");
//        }
//        public ActionResult Edit(int id)
//        {
//            model = pro.PopulateCreditPatients().Where(x => x.CreditPatientID == id).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public ActionResult Edit(int id, CreditPatientDetailsModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                if (pro.EditCreditPatients(id, model).Successmessage == "Success")
//                {
//                    TempData["Msg"] = "Data Has Been Updated successfully";
//                }
//                else if (model.Successmessage == "Fail")
//                {
//                    TempData["MsgFail"] = "Data Has Not Been Saved Successfully";
//                }
//                else
//                {
//                    TempData["MsgError"] = model.ErrorMessage;
//                }
//            }
//            return RedirectToAction("Index");
//        }
//        public ActionResult Delete(int id)
//        {
//            if (ModelState.IsValid)
//            {
//                if (pro.DeleteCreditPatients(id))
//                {
//                    TempData["Msg"] = "Data Has Been Deleted Successfully";
//                }
//                else
//                    TempData["Msg"] = "Data Has Not Been Deleted Succeessfully";
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}

