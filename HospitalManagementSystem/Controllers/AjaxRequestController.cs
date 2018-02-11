using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
namespace HospitalManagementSystem.Controllers
{
    public class AjaxRequestController : Controller
    {
        EHMSEntities ent = new EHMSEntities();
        public ActionResult GetDoctorNameList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "Select Doctor", Value = null });
                ddlList.Add(new SelectListItem { Text = "Unit", Value = "1009" });
                var data = (from x in ent.SetupDoctorMasters
                            join y in ent.SetupDoctorDepartments on x.DoctorID equals y.DoctorID
                            join z in ent.SetupDepartments on y.DepartmentID equals z.DeptID
                            where y.DepartmentID == id
                            select new { x.DoctorID, x.FirstName, x.MiddleName, x.LastName }).Distinct();

                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
                }
                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDoctorNameListFromEmployee(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "Select Doctor", Value = null });
                ddlList.Add(new SelectListItem { Text = "Unit", Value = "1009" });
                var data = (from x in ent.SetupEmployeeMasters
                            join y in ent.SetupEmployeeDepartments on x.EmployeeMasterId equals y.EmployeeMasterId
                            join z in ent.SetupDepartments on y.DepartmentId equals z.DeptID
                            where y.DepartmentId == id && x.DesignationID == 2
                            select new { x.SPAccountHeadID, x.EmployeeFullName }).Distinct();

                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.EmployeeFullName, Value = item.SPAccountHeadID.ToString() });
                }
                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult test(int id)
        {

            return Redirect("Views/DoctorSetup/_DoctorDetailPartial");

        }

        //saru mam

        public ActionResult GetSubAccountHeadLeafList(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                string Hierarchy = Utility.GetHierarchyFromID(id);
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "Select AccountHead", Value = "0" });
                //var data = ent.GL_AccSubGroups.Where(x => x.HierarchyCode.StartsWith())
                var data = ent.GL_AccSubGroups.Where(x => x.HierarchyCode.StartsWith(Hierarchy) && x.IsLeafLevel == true).ToList();
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.AccSubGroupName, Value = item.AccSubGruupID.ToString() });
                }
                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult getText()
        {
            string str = string.Empty;
            str = "hello";
            JsonResult jResult = new JsonResult();
            jResult.Data = str;
            return jResult;
        }

        public ActionResult GetEnglishDate(string NepaliDate)
        {
            //string date = NepaliDate;
            //DateTime engdate;
            DateTime engdate;
            DateTime temp;
            string date = string.Empty;

            if (DateTime.TryParse(NepaliDate, out temp))
            {
                date = NepaliDate;
                using (EHMSEntities ent = new EHMSEntities())
                {
                    string[] arry = date.Split('/');
                    string yr = arry[0];
                    string mm = arry[1];
                    string dd = arry[2];
                    engdate = ent.Database.SqlQuery<DateTime>("sp_GetRomanDate " + yr + "," + mm + "," + dd + "").FirstOrDefault<DateTime>();


                }
                //JsonResult jResult = new JsonResult();
                //jResult.Data = engdate.ToString();
                //return jResult;
                return Json(engdate.ToShortDateString(), JsonRequestBehavior.AllowGet);


            }
            else
            {
                return Json(DateTime.Now.ToShortDateString(), JsonRequestBehavior.AllowGet);

            }



        }

        public ActionResult getNepaliDate(string EnglishDate)
        {
            string date = EnglishDate;
            string engdate;
            using (EHMSEntities ent = new EHMSEntities())
            {
                string[] arry = date.Split('/');
                string mm = arry[0];
                string dd = arry[1];
                string yr = arry[2];
                //engdate = ent.Database.SqlQuery<DateTime>("sp_GetLocalDate " + yr + "," + mm + "," + dd + "").FirstOrDefault<DateTime>();
                engdate = ent.Database.SqlQuery<string>("sp_GetLocalDate '" + date + "', 1").FirstOrDefault<string>();
            }

            return Json(engdate.Replace('-', '/'), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetDistrictList(int id)
        {

            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.TblDistricts.Where(x => x.ZoneID == id);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.DistrictEng, Value = item.DistrictID.ToString() });
            }


            var ddlSelectOptionList = ddlList;
            JsonResult jResult = new JsonResult();
            jResult.Data = ddlSelectOptionList;
            return jResult;
        }


        [HttpPost]
        public ActionResult GetRoomNumber(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "Select room number", Value = null });
            var collection = ent.SetUpIpdRooms.Where(m => m.RoomType == id);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.RoomNo.ToString(), Value = item.RoomNo.ToString() });
            }

            var ddlSelectOptionList = ddlList;
            JsonResult jResult = new JsonResult();
            jResult.Data = ddlSelectOptionList;
            return jResult;

        }
        //[HttpPost]
        //public ActionResult GetVdcList(int id)
        //{
        //    List<SelectListItem> ddlList = new List<SelectListItem>();
        //    ddlList.Add(new SelectListItem { Text = "Select VDC List", Value = null });
        //    var collection = ent.VDCMUN.Where(m => m.DistrictID == id);
        //    foreach (var item in collection)
        //    {
        //        ddlList.Add(new SelectListItem { Text = item.VdcMunNameEng.ToString(), Value = item.VdcMunNameEng.ToString() });
        //    }
        //    var ddlSelectOptionList = ddlList;
        //    JsonResult jresult = new JsonResult();
        //    jresult.Data = ddlSelectOptionList;
        //    return jresult;
        //}

        public JsonResult GetBedNumber(int RoomTypeID, int RoomNumber)
        {
            var GetId = ent.SetUpIpdRooms.Where(m => m.RoomType == RoomTypeID && m.RoomNo == RoomNumber).Select(m => m.IpdRoomId).FirstOrDefault();
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "Select Bed Number", Value = null });
            var collection = ent.IpdRoomStatus.Where(m => m.RoomNumber == RoomNumber && m.Status == true && m.IPDRoomId == (int)GetId);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.BedNumber.ToString(), Value = item.BedNumber.ToString() });

            }

            var ddlSelectOptionList = ddlList;
            JsonResult jResult = new JsonResult();
            jResult.Data = ddlSelectOptionList;
            //return jResult;
            return Json(ddlList, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetRoomAmount(int roomTypeId, int RoomNumber)
        {
            var str = ent.SetUpIpdRooms.Where(m => m.RoomNo == RoomNumber && m.RoomType == roomTypeId).Select(m => m.BedAmount).FirstOrDefault();
            JsonResult jResult = new JsonResult();
            jResult.Data = str;

            return Json(str, JsonRequestBehavior.AllowGet);
        }


        //This is from surendra (Stock Module)
        public ActionResult GetSubCategory(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupStockSubCategories.Where(x => x.StockCategoryID == id);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.StockSubCategoryName, Value = item.StockSubCategoryID.ToString() });
            }
            //var ddlSelectOptionList = ddlList;
            //JsonResult jResult = new JsonResult();
            //jResult.Data = ddlSelectOptionList;
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetlistofVDC(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "Select VDC", Value = null });
            var collection = ent.VDCMUNs.Where(x => x.DistrictID == id);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.VdcMunNameEng, Value = item.VdcMunID.ToString() });
            }
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemName(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupStockItemEntries.Where(x => x.StockSubCategoryId == id && x.Status == true);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.StockItemName, Value = item.StockItemEntryId.ToString() });
            }
            //var ddlSelectOptionList = ddlList;
            //JsonResult jResult = new JsonResult();
            //jResult.Data = ddlSelectOptionList;
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemNameByCategory(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupStockItemEntries.Where(x => x.Status == true &&
                x.StockCategoryId == id && x.StockSubCategoryId == ent.SetupStockSubCategories.Where(y => y.Status == true && y.StockCategoryID == id).FirstOrDefault().StockSubCategoryID);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.StockItemName, Value = item.StockItemEntryId.ToString() });
            }
            //var ddlSelectOptionList = ddlList;
            //JsonResult jResult = new JsonResult();
            //jResult.Data = ddlSelectOptionList;
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }




        //edite by bibek on jan 30
        [HttpPost]
        public ActionResult GetOtherDiscountAmtFinal(int id, int id2)
        {
            decimal getPercentage = HospitalManagementSystem.Utility.GetCurrentOtherDiscountPercentage(id);
            decimal getCurrentRegPrice = HospitalManagementSystem.Utility.GetCurrentRegistrationFeeWithID(id2);
            decimal FinalPrice = HospitalManagementSystem.Utility.GetCurrentOtherDiscountPercentage(getPercentage, getCurrentRegPrice);

            JsonResult jResult = new JsonResult();
            jResult.Data = FinalPrice;
            return jResult;
        }


        public ActionResult GetItemNameInDep(int id)
        {
            var did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupStockItemEntries.Where(x => x.StockSubCategoryId == id && x.Status == true && ent.DepartmentWiseStocks.Any(y => y.DepartmentID == did && y.ItemId == x.StockItemEntryId));
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.StockItemName, Value = item.StockItemEntryId.ToString() });
            }
            //var ddlSelectOptionList = ddlList;
            //JsonResult jResult = new JsonResult();
            //jResult.Data = ddlSelectOptionList;
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemNameByCategoryInDep(int id)
        {
            var did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupStockItemEntries.Where(x => x.Status == true && ent.DepartmentWiseStocks.Any(z => z.ItemId == x.StockItemEntryId && z.DepartmentID == did) &&
                x.StockCategoryId == id && x.StockSubCategoryId == ent.SetupStockSubCategories.Where(y => y.Status == true && y.StockCategoryID == id).FirstOrDefault().StockSubCategoryID);
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.StockItemName, Value = item.StockItemEntryId.ToString() });
            }
            //var ddlSelectOptionList = ddlList;
            //JsonResult jResult = new JsonResult();
            //jResult.Data = ddlSelectOptionList;
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDoctorByType(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            var collection = ent.SetupDoctorMasters.Where(x => x.DoctorType == id).ToList();
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.FirstName + " " + item.MiddleName + " " + item.LastName, Value = item.DoctorID.ToString() });
            }
            return Json(ddlList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIcdCode(string Name)
        {
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();



            List<IpdRegistrationMasterModel> templist = new List<IpdRegistrationMasterModel>();
            templist = (from icod in ent.SetupIcdCodes
                        where icod.DiagnosisTitle.StartsWith(Name)
                        select new IpdRegistrationMasterModel
                        {
                            id = icod.ICDCODEID,
                            FirstName = icod.DiagnosisTitle,
                            LastName = icod.CodeNumber,
                        }).Take(10).ToList();



            foreach (var item in templist)
            {
                names.Add(item.FirstName + "" + "~" + item.LastName);
            }
            return Json(names, JsonRequestBehavior.AllowGet);


        }


        //Sarmila
        public ActionResult GetSubAccountHeadList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> ddlList = new List<SelectListItem>();
                ddlList.Add(new SelectListItem { Text = "Select AccountHead", Value = "0" });
                var data = ent.GL_AccSubGroups.Where(x => x.AccGroupID == id && x.IsLeafLevel == false).ToList();
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.AccSubGroupName, Value = item.AccSubGruupID.ToString() });
                }
                return Json(ddlList, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SearchPatienName(string s)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();
                List<string> names = new List<string>();
                List<OpdModel> templist = new List<OpdModel>();
                templist = (from opd in ent.OpdMasters
                            where opd.FirstName.StartsWith(s)
                            select new OpdModel
                            {
                                FirstName = opd.FirstName,
                                MiddleName = opd.MiddleName,
                                LastName = opd.LastName
                            }).Take(10).ToList();



                foreach (var item in templist)
                {
                    names.Add(item.FirstName + " " + item.LastName);
                }
                return Json(names, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public ActionResult SearchIPPatienName(string s)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();
                List<string> names = new List<string>();
                List<OpdModel> templist = new List<OpdModel>();
                templist = (from opd in ent.OpdMasters
                            join IPD in ent.IpdRegistrationMasters on opd.OpdID equals IPD.OpdNoEmergencyNo
                            where opd.FirstName.StartsWith(s)
                            select new OpdModel
                            {
                                FirstName = opd.FirstName,
                                MiddleName = opd.MiddleName,
                                LastName = opd.LastName
                            }).Take(10).ToList();



                foreach (var item in templist)
                {
                    names.Add(item.FirstName + " " + item.LastName);
                }
                return Json(names, JsonRequestBehavior.AllowGet);

            }

        }



        public ActionResult GetTestnamebydepartmentforpatho(int id)
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            //ddlList.Add(new SelectListItem { Text = "---Select Test---", Value = "0" });
            var collection = ent.Database.SqlQuery<PackageDetailModel>("GetTestDetailDepartmentIDWiseNew '" + id + "'").ToList();
            foreach (var item in collection)
            {
                ddlList.Add(new SelectListItem { Text = item.TestName, Value = item.TestId.ToString() });
            }

            var ddlSelectOptionList = ddlList;
            JsonResult jResult = new JsonResult();
            jResult.Data = ddlSelectOptionList;
            //return jResult;
            return Json(ddlSelectOptionList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PieChartDepartmentsWise()
        {
            MisPorvider pro = new MisPorvider();
            MisModel model = new MisModel();
            model.PieChartDepartmentswiseViewModelList = pro.GetDeptWiseDetailForPie();
            return Json(model.PieChartDepartmentswiseViewModelList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchPatienNameOT(string s)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                OpdModel model = new OpdModel();
                List<string> names = new List<string>();
                List<OpdModel> templist = new List<OpdModel>();
                templist = (from opd in ent.OpdMasters
                            join y in ent.OperationTheatreMasters
                            on opd.OpdID equals y.SourceID
                            where opd.FirstName.StartsWith(s)
                            select new OpdModel
                            {
                                FirstName = opd.FirstName,
                                MiddleName = opd.MiddleName,
                                LastName = opd.LastName
                            }).Take(10).ToList();



                foreach (var item in templist)
                {
                    names.Add(item.FirstName + " " + item.MiddleName + " " + item.LastName);
                }
                return Json(names, JsonRequestBehavior.AllowGet);

            }





        }

        public ActionResult NewoldRegistration(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var registrationFee = ent.SetupHospitalFees.Where(x => x.FeeType == Id).Select(x => x.HospitalFee).FirstOrDefault();
                return Json(registrationFee, JsonRequestBehavior.AllowGet);
            }
        }

        //Edited By Pushkar
        public ActionResult GetVDCNPTitleByDistrict(int id)
        {

            var collection = ent.VDCMUNs.Where(x => x.DistrictID == id).Select(x => x.VdcMunNameEng).FirstOrDefault();
            JsonResult jResult = new JsonResult();
            jResult.Data = collection;
            return Json(collection, JsonRequestBehavior.AllowGet);

        }

    }
}
