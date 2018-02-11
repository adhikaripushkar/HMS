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
    public class PackageController : Controller
    {
        //
        // GET: /Package/
        EHMSEntities ent = new EHMSEntities();
        //PackageProvider pro = new PackageProvider();
        public ActionResult Index()
        {
            PackageModel model = new PackageModel();
            model.PackageModelList = new List<PackageModel>(AutoMapper.Mapper.Map<IEnumerable<PackageMaster>, IEnumerable<PackageModel>>(ent.PackageMasters.Where(x => x.Status == true)));
            return View(model);
        }

        //
        // GET: /Package/Details/5


        //
        // GET: /Package/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Package/Create

        [HttpPost]
        public ActionResult Create(PackageModel model)
        {
            if (model.PackageDetailList == null)
            {
                TempData["message"] = "Add items to package !!";
                return View();
            }
            if (ent.PackageMasters.Any(x => x.PackageName == model.PackageName))
            {
                TempData["message"] = "Package with this name already exits !!";
                return View(model);
            }
            try
            {


                var obj = AutoMapper.Mapper.Map<PackageModel, PackageMaster>(model);
                obj.Status = true;
                obj.Vat = Convert.ToDecimal(0);
                obj.VatAmount = Convert.ToDecimal(0);
                obj.CreatedBy = Utility.GetCurrentLoginUserId();
                obj.CreatedDate = DateTime.Now;
                ent.PackageMasters.Add(obj);
                ent.SaveChanges();
                foreach (var item in model.PackageDetailList)
                {
                    var detail = new PackageDetail();
                    detail.PackageId = obj.PackageId;
                    detail.DeptId = item.DeptId;
                    detail.TestId = item.TestId;
                    if (item.DeptId == 1006)
                    {
                        detail.SectionId = (int)ent.SetupPathoTests.Where(x => x.TestId == item.TestId).SingleOrDefault().SectionId;
                    }
                    else
                    {
                        detail.SectionId = 0;
                    }
                    detail.Status = true;
                    ent.PackageDetails.Add(detail);
                }
                ent.SaveChanges();
                TempData["message"] = "Package created successfully !!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Package/Edit/5

        public ActionResult Edit(int id)
        {
            var obj = ent.PackageMasters.Where(x => x.PackageId == id).SingleOrDefault();
            PackageModel model = new PackageModel();
            AutoMapper.Mapper.Map(obj, model);
            model.PackageDetailList = new List<PackageDetailModel>(AutoMapper.Mapper.Map<IEnumerable<PackageDetail>, IEnumerable<PackageDetailModel>>(ent.PackageDetails.Where(x => x.PackageId == id && x.Status == true)));

            return View(model);
        }

        //
        // POST: /Package/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PackageModel model)
        {
            try
            {
                // TODO: Add update logic here
                var obj = ent.PackageMasters.Where(x => x.PackageId == id).SingleOrDefault();
                AutoMapper.Mapper.Map(model, obj);
                obj.Status = true;
                obj.Vat = Convert.ToDecimal(0);
                obj.VatAmount = Convert.ToDecimal(0);
                ent.Entry(obj).State = System.Data.EntityState.Modified;
                foreach (var item in ent.PackageDetails.Where(x => x.PackageId == id).ToList())
                {
                    ent.PackageDetails.Remove(item);
                }
                foreach (var item in model.PackageDetailList)
                {
                    var detail = new PackageDetail();
                    detail.PackageId = id;
                    detail.DeptId = item.DeptId;
                    detail.TestId = item.TestId;
                    if (item.DeptId == 1006)
                    {
                        detail.SectionId = (int)ent.SetupPathoTests.Where(x => x.TestId == item.TestId).SingleOrDefault().SectionId;
                    }
                    else
                    {
                        detail.SectionId = 0;
                    }
                    detail.Status = true;
                    ent.PackageDetails.Add(detail);
                }
                ent.SaveChanges();
                TempData["message"] = "Package updated successfully !!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        //
        // GET: /Package/Delete/5

        public ActionResult Delete(int id)
        {
            var obj = ent.PackageMasters.Where(x => x.PackageId == id).SingleOrDefault();
            obj.Status = false;
            ent.Entry(obj).State = System.Data.EntityState.Modified;
            foreach (var item in ent.PackageDetails.Where(x => x.PackageId == id).ToList())
            {
                ent.PackageDetails.Remove(item);
            }
            ent.SaveChanges();
            TempData["message"] = "Package deleted successfully !!";
            return RedirectToAction("Index");
        }

        //
        // POST: /Package/Delete/5

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
        public ActionResult PackageDetail()
        {
            PackageDetailModel model = new PackageDetailModel();

            return PartialView("PackageDetail");
        }

        public ActionResult AssignPatient(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AssignPatient(int id, PackagePatientAssignment obj)
        {
            EHMSEntities ent = new EHMSEntities();
            obj.PackageId = id;
            obj.Status = true;
            obj.CreatedBy = Utility.GetCurrentLoginUserId();
            obj.CreatedDate = DateTime.Now;
            obj.AssignDate = DateTime.Now;
            ent.PackagePatientAssignments.Add(obj);
            ent.SaveChanges();
            TempData["message"] = "Patient assigned to the package !!";
            return RedirectToAction("Index");
        }

        public ActionResult AutocompletePackageName(string s)
        {
            EHMSEntities ent = new EHMSEntities();
            List<string> names = new List<string>();
            var pck = ent.PackageMasters.Where(x => x.PackageName.Contains(s) && x.Status == true).ToList();
            foreach (var item in pck)
            {
                names.Add(item.PackageName);
            }
            return Json(names, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PackagePatientList()
        {
            EHMSEntities ent = new EHMSEntities();
            List<PackagePatientAssignment> list = ent.PackagePatientAssignments.Where(x => x.Status == true).ToList();
            return View(list);
        }

    }
}
