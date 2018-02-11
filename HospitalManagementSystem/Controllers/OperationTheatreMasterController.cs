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
    public class OperationTheatreMasterController : Controller
    {
        // GET: /OperationTheatreMaster/
        OperationTheatreMasterProvider pro = new OperationTheatreMasterProvider();
        SurgeryChargeProvider pros = new SurgeryChargeProvider();

        [HttpPost]
        public ActionResult Main(int? PatientId)
        {

            OpdModel opdmodel = new OpdModel();
            OpdProvider pro = new OpdProvider();
            opdmodel.OpdModelList = pro.Getlist().Where(x => x.OpdID == PatientId).ToList();
            return View("Main", opdmodel);
        }


        public ActionResult Main()
        {
            OpdModel obj = new OpdModel();
            obj.OpdModelList = new List<OpdModel>();
            return View(obj);
        }


        public ActionResult Index()
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            model.OperationTheatreMasterList = pro.GetList();
            return View(model);
        }

        //
        // GET: /OperationTheatreMaster/Details/5

        public ActionResult Details(int id)
        {
            List<OperationTheatreDetailsModel> TheatreDetailsList = new List<OperationTheatreDetailsModel>();
            TheatreDetailsList = pro.GetTheatreDetailsList(id);
            return View(TheatreDetailsList);
        }

        // GET: /OperationTheatreMaster/Create

        public ActionResult Create(int id, int Billno)
        {
            OpdModel opdmodel = new OpdModel();
            OpdProvider pro = new OpdProvider();
            BillingCounterProvider Brpro = new BillingCounterProvider();
            opdmodel.OpdModelList = pro.Getlist().Where(x => x.OpdID == id).ToList();

            //model.BillingCounterPatientInformationModel = pro.GetPatientBasicInformationFromOpd(Convert.ToInt32(ipValue), 0).FirstOrDefault();
            //model.BalanceDeposit = pro.getBalanceDeposit(Convert.ToInt32(model.BillingCounterPatientInformationModel.AccountHeadId));

            OperationTheatreMasterModel obj = new OperationTheatreMasterModel();
            obj.refOfOpdModel = new OpdModel();
            foreach (var item in opdmodel.OpdModelList)
            {
                obj.refOfOpdModel.FirstName = item.FirstName;
                obj.refOfOpdModel.MiddleName = item.MiddleName;
                obj.refOfOpdModel.LastName = item.LastName;
                obj.refOfOpdModel.COA = item.COA;
            }
            obj.PatientACHeadId = (int)obj.refOfOpdModel.COA;
            obj.PatientDepoAmount = Brpro.getBalanceDeposit(obj.PatientACHeadId);
            obj.SourceID = id;
            ViewBag.value = 0;
            return View(obj);
        }

        //
        // POST: /OperationTheatreMaster/Create

        [HttpPost]
        public ActionResult Create(OperationTheatreMasterModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            try
            {
                // TODO: Add insert logic here
                if (model.PersonAllocatedList.Count == 0)
                {
                    TempData["message"] = "Select Persons.";
                    return View(model);
                }

                //Get Surgery Charge name
                EHMSEntities entity = new EHMSEntities();
                var result = entity.SurgeryCharges.Where(x => x.ChargeName.Contains(model.ChargeName)).ToList();
                if (result.Count() <= 0)
                {
                    TempData["message"] = "Please select surgery name.";
                    return View(model);
                }


                int i = pro.Insert(model);
                if (i != 0)
                {
                    //return RedirectToAction("BillRecords", new { id = i, opdid = model.SourceID });
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.save;
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["success"] = HospitalManagementSystem.UtilityMessage.savefailed;
                    return RedirectToAction("Index");
                }


            }
            catch (Exception e)
            {
                TempData["message"] = "Select Persons.";
                return View();
            }
        }



        //
        // GET: /OperationTheatreMaster/Edit/5

        public ActionResult Edit(int id, int PatientId)
        {
            EHMSEntities ent = new EHMSEntities();
            OperationTheatreMasterModel model = pro.GetObject(id);
            model.refOfOpdModel = new OpdModel();
            OpdModel opdmodel = new OpdModel();
            OpdProvider proopd = new OpdProvider();
            opdmodel.OpdModelList = proopd.Getlist().Where(x => x.OpdID == PatientId).ToList();

            foreach (var item in opdmodel.OpdModelList)
            {

                model.refOfOpdModel.FirstName = item.FirstName;
                model.refOfOpdModel.MiddleName = item.MiddleName;
                model.refOfOpdModel.LastName = item.LastName;

            }

            //model.SourceID = id;
            model.SourceID = PatientId;
            model.PersonAllocatedList = new List<PersonAllocated>();
            foreach (var item in pro.GetTheatreDetailsList(id))
            {
                PersonAllocated pa = new PersonAllocated();
                pa.PersonAllocateId = item.PersonAllocatedID;
                pa.PersonAllocatedTypeId = item.PersonAllocatedTypeID;
                model.PersonAllocatedList.Add(pa);
            }
            //model.ChargeName = ent.SurgeryCharge.Where(x=>x.ChargeAmount==model.Charge).FirstOrDefault().ChargeName;
            return View(model);
        }

        //
        // POST: /OperationTheatreMaster/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, OperationTheatreMasterModel model)
        {
            try
            {
                // TODO: Add update logic here
                //if (model.PersonAllocatedList.Count == 0) model.PersonAllocatedList=null;

                int i = pro.Update(model);
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
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /OperationTheatreMaster/Delete/5

        public ActionResult Delete(int id)
        {
            int i = pro.Delete(id);
            if (i != 0)
            {

                TempData["success"] = HospitalManagementSystem.UtilityMessage.delete;

                return RedirectToAction("Index");
            }
            else
            {
                TempData["success"] = HospitalManagementSystem.UtilityMessage.deletefailed;
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteTheatreDetails(int id)
        {
            OperationTheatreDetailsProvider pro1 = new OperationTheatreDetailsProvider();
            int i = pro1.Delete(id);
            TempData["message"] = "Successfully Deleted!";
            return RedirectToAction("Details/" + i);
        }



        //
        // POST: /OperationTheatreMaster/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        [HttpPost]
        public ActionResult PersonAllocated()
        {
            PersonAllocated model = new PersonAllocated();

            return PartialView("PersonAllocated");
        }



        public ActionResult EidtTheatreDetails(int id)
        {
            OperationTheatreDetailsProvider pro = new OperationTheatreDetailsProvider();
            OperationTheatreDetailsModel model = new OperationTheatreDetailsModel();
            model = pro.GetObject(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EidtTheatreDetails(int id, OperationTheatreDetailsModel model)
        {
            OperationTheatreDetailsProvider pro1 = new OperationTheatreDetailsProvider();

            try
            {
                if (ModelState.IsValid)
                {
                    int i = pro1.Update(model);
                    //List<OperationTheatreDetailsModel> TheatreDetailsList = new List<OperationTheatreDetailsModel>();
                    //TheatreDetailsList = pro.GetTheatreDetailsList(model.OperationTheatreMasterID);
                    TempData["message"] = "Successfully Updated!";
                    return RedirectToAction("Details/" + i);
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }




        public ActionResult OperationRecord(int id, int opd)
        {
            OperationRecordModel model = new OperationRecordModel();

            model = OperationRecordProvider.GetOperationRecords(opd, id);
            model.OperationTheatreMasterId = id;
            model.OpdId = opd;
            return View(model);
        }

        [HttpPost]
        public ActionResult OperationRecord(OperationRecordModel model)
        {
            OperationRecordProvider.Insert(model);
            model = OperationRecordProvider.GetOperationRecords(model.OpdId, model.OperationTheatreMasterId);
            TempData["message"] = "Successfully Saved!";
            return View(model);
        }


        public ActionResult SurgeryCharge(int? id)
        {

            SurgeryChargeModel model = new SurgeryChargeModel();
            if (id != null)
            {
                model = pros.GetObject(id);
            }
            model.SurgerChargeList = pros.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult SurgeryCharge(SurgeryChargeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.SurgeryChargeId == 0)
                    {
                        pros.Insert(model);
                    }
                    else pros.Update(model);
                    return RedirectToAction("SurgeryCharge");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }

        }

        public ActionResult DeleteSurgChg(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SurgeryCharges.Where(x => x.SurgeryChargeId == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return RedirectToAction("SurgeryCharge");
        }

        public ActionResult SearchChargeName(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            var names = ent.SurgeryCharges.Where(x => x.ChargeName.Contains(s) && x.Status == true).ToList();
            return Json(names, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetChargeAmount(string name)
        {

            //Get different amount ie payable and general case
            var CurrSession = System.Web.HttpContext.Current.Session["OpdTypeIdInt"];
            string CurrentSessionStr = string.Empty;
            if (CurrSession != null)
            {
                CurrentSessionStr = CurrSession.ToString();
            }
            else
            {
                CurrentSessionStr = "1";
            }
            EHMSEntities ent = new EHMSEntities();
            decimal amount = 0;

            try
            {
                if (CurrentSessionStr == "1")
                {
                    amount = ent.SurgeryCharges.Where(x => x.ChargeName == name).SingleOrDefault().TotalAmount;
                }
                else
                {
                    var Total = ent.SurgeryCharges.Where(x => x.ChargeName == name);
                    if (Total.Count() > 0)
                    {
                        var PercentageTotal = Total.SingleOrDefault().PayableGrandTotal;
                        if (PercentageTotal != null && PercentageTotal > 0)
                        {
                            amount = Convert.ToDecimal(PercentageTotal);

                        }
                        else
                        {
                            amount = Total.SingleOrDefault().TotalAmount;
                        }
                    }
                    else
                    {
                        amount = Convert.ToDecimal(0);
                    }

                }
            }
            catch { }

            return Json(amount, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BillRecords(int id, int opdid)
        {
            EHMSEntities ent = new EHMSEntities();

            var data = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == id && x.SourceID == opdid).FirstOrDefault();
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            AutoMapper.Mapper.Map(data, model);

            return View(model);

        }
        public ActionResult PreOT()
        {
            //OperationTheatreMasterModel model = new OperationTheatreMasterModel();

            return View();
        }
        public ActionResult _SearchPreOTDetails(DateTime Sdate, DateTime Edate)
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            return PartialView("_PREPOSTOT", model);
        }
        public ActionResult PostOT()
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            return View();
        }
        public ActionResult _SearchPostOTDetails(DateTime Sdate, DateTime Edate)
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            return PartialView("_PREPOSTOT", model);
        }
        public ActionResult SearchForOTDetail()
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            return View(model);

        }
        public ActionResult _SearchOperationTheatreDetails(DateTime Sdate, DateTime Edate, int SourceID, string name)
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();

            if (SourceID == 0 && name != "")
            {
                int id = HospitalManagementSystem.Utility.GetOpdIdByName(name);
                model.OperationTheatreMasterList = pro.GetListdateandidwise(Sdate, Edate, id);

            }
            else if (SourceID != 0 && name == "")
            {
                model.OperationTheatreMasterList = pro.GetListdateandidwise(Sdate, Edate, SourceID);

            }
            else
            {
                model.OperationTheatreMasterList = pro.GetListdatewise(Sdate, Edate);
            }


            return PartialView("_SOTDetails", model);
        }

        public ActionResult GetListFromCB(string StartDate, string EndDate, string PatientName)
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            model.FromCBViewDetailsList = pro.GetSurgeryListFromCBM();
            return View(model);

        }

        public ActionResult GetCommListFromCB()
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            model.GetCBCommissionViewModelOTList = pro.GetCommissionDetailsFromCB();
            return View(model);
        }

        public ActionResult ViewOtCommissionDetails(int id)
        {
            OperationTheatreMasterModel model = new OperationTheatreMasterModel();
            model.GetOTCommDetailsViewModelList = pro.GetCommissionDetailsByBillNo(id);
            model.ObjGetOTCommDetailsViewModel.BillNo = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewOtCommissionDetails(OperationTheatreMasterModel model)
        {
            pro.InsertOTCommissionDetails(model);
            //return View(model);
            return RedirectToAction("GetCommListFromCB");
        }


        [HttpPost]
        public ActionResult AddMoreParticulars()
        {
            AddMoreOTCommissionViewModel model = new AddMoreOTCommissionViewModel();
            return PartialView("_AddMoreOTCommission", model);

        }




    }
}
