using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ItemBlockRecordController : Controller
    {
        //
        // GET: /ItemBlockRecord/

        public ActionResult Index()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                //return new List<SetUpMemberShipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberShip>, IEnumerable<SetUpMemberShipModel>>(ent.SetupMemberShip));

                return View(new List<ItemBlockRecordsModel>(AutoMapper.Mapper.Map<IEnumerable<ItemBlockRecord>, IEnumerable<ItemBlockRecordsModel>>(ent.ItemBlockRecords)));
            }
        }

        public ActionResult Create()
        {
            ItemBlockRecordsModel model = new ItemBlockRecordsModel();

            return View();

        }


        [HttpPost]
        public ActionResult Create(ItemBlockRecordsModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {


                int particularid = Convert.ToInt32(model.Particular);
                var itmBlockSetupData = ent.ItemBlockSetups.Where(x => x.BlockId == model.ItemBlockSetupID && x.ItemBlockSetupID == particularid).FirstOrDefault();

                var objtosave = AutoMapper.Mapper.Map<ItemBlockRecordsModel, ItemBlockRecord>(model);

                objtosave.CreatedDate = DateTime.Now;
                objtosave.CreatedBy = Utility.GetCurrentLoginUserId();
                objtosave.ItemBlockSetupID = itmBlockSetupData.ItemBlockSetupID;

                ent.ItemBlockRecords.Add(objtosave);
                int i = ent.SaveChanges();

                if (i != 0)
                {

                    TempData["Success"] = UtilityMessage.save;
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Success"] = UtilityMessage.savefailed;
                    return RedirectToAction("Index");


                }

            }

            // return View();

        }


        public ActionResult ListOfBlockWiseParticular(int id)
        {

            EHMSEntities ent = new EHMSEntities();
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "Select", Value = null });

            var collection = ent.ItemBlockSetups.Where(x => x.BlockId == id);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.Particular, Value = item.ItemBlockSetupID.ToString() });
            }


            return Json(ddlList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetSerialNumber(int BlockId, int ParticularId)
        {

            EHMSEntities ent = new EHMSEntities();
            int serialno = 0;

            var itmBlockSetupData = ent.ItemBlockSetups.Where(x => x.BlockId == BlockId && x.ItemBlockSetupID == ParticularId).FirstOrDefault();


            var itmBlockRecordData = ent.ItemBlockRecords.Where(x => x.ItemBlockSetupID == itmBlockSetupData.ItemBlockSetupID).ToList();

            if (itmBlockRecordData.Count < 1)
            {
                serialno = itmBlockSetupData.SerialNoFrom;

            }
            else
            {

                int maxSerialNo = itmBlockRecordData.Max(x => x.SerialNo);

                if (maxSerialNo < itmBlockSetupData.SerialNoTo)
                {
                    serialno = maxSerialNo + 1;

                }

            }


            //int SerialNoFrom = Convert.ToInt32(ent.ItemBlockSetup.Where(x => x.BlockId == id).Select(x => x.SerialNoFrom));
            //int SerialNoTo = Convert.ToInt32(ent.ItemBlockSetup.Where(x => x.BlockId == id).Select(x => x.SerialNoTo));




            return Json(serialno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Edit(int id)
        {
            ItemBlockRecordsModel model = new ItemBlockRecordsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.ItemBlockRecords.Where(x => x.ItemBlcokRecID == id).SingleOrDefault();
                AutoMapper.Mapper.Map(obj, model);
                model.ItemBlockSetupID = obj.ItemBlockSetup.BlockId;
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ItemBlockRecordsModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.ItemBlockRecords.Where(x => x.ItemBlcokRecID == model.ItemBlcokRecID).SingleOrDefault();
                model.CreatedBy = obj.CreatedBy;
                model.CreatedDate = obj.CreatedDate;
                model.Status = obj.Status;
                model.ItemBlockSetupID = obj.ItemBlockSetupID;
                AutoMapper.Mapper.Map(model, obj);
                ent.Entry(obj).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ItemBlockRecordsModel model = new ItemBlockRecordsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtodetails = ent.ItemBlockRecords.Where(x => x.ItemBlcokRecID == id).SingleOrDefault();
                AutoMapper.Mapper.Map(objtodetails, model);

            }

            return PartialView("_Details", model);

        }



        public ActionResult Delete(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtodel = ent.ItemBlockRecords.Where(x => x.ItemBlcokRecID == id).SingleOrDefault();

                ent.ItemBlockRecords.Remove(objtodel);
                int i = ent.SaveChanges();
                if (i != 0)
                {

                    TempData["Success"] = UtilityMessage.delete;

                }
                else
                {

                    TempData["Success"] = UtilityMessage.deletefailed;

                }

                return RedirectToAction("Index");
            }


        }

        public ActionResult SearchItemRecord(int itemsetupid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                //return new List<SetUpMemberShipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberShip>, IEnumerable<SetUpMemberShipModel>>(ent.SetupMemberShip));
                ViewBag.serialno = ent.ItemBlockSetups.Where(x => x.ItemBlockSetupID == itemsetupid).SingleOrDefault().SerialNoFrom + " to " + ent.ItemBlockSetups.Where(x => x.ItemBlockSetupID == itemsetupid).SingleOrDefault().SerialNoTo;
                return View(new List<ItemBlockRecordsModel>(AutoMapper.Mapper.Map<IEnumerable<ItemBlockRecord>, IEnumerable<ItemBlockRecordsModel>>(ent.ItemBlockRecords).Where(x => x.ItemBlockSetupID == itemsetupid)));
            }
        }


    }
}
