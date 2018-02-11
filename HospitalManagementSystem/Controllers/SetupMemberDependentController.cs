using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Providers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class SetupMemberDependentController : Controller
    {
        //
        // GET: /SetupMemberDependent/

        public ActionResult Index()
        {

            SetUpMemberShipProvider pro = new SetUpMemberShipProvider();

            SetUpMemberShipModel obj = new SetUpMemberShipModel();

            obj.lstOfsetSetUpMemberShipModel = pro.Getlist();
            foreach (var item in obj.lstOfsetSetUpMemberShipModel)
            {

                item.FirstName = item.FirstName + " " + item.MiddleName + " " + item.LastName;

            }



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


            return View(obj);

        }


        public ActionResult GetLstWithName(string id)
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


            return PartialView("_GetLstWithName", obj);



        }



        public ActionResult Create(int Id, string FirstName, int MembertypeId, int MaxDepend, int DueDepend)
        {
            SetupMemberDependentModel model = new SetupMemberDependentModel();
            model.refSetUpMemberShipModel = new SetUpMemberShipModel();

            model.refSetUpMemberShipModel.FirstName = FirstName;

            model.SetupMemberID = Id;
            model.MemberTypeID = MembertypeId;

            if (DueDepend != 0)
            {


                for (int i = 1; i <= DueDepend; i++)
                {

                    var mod = new SetupMemberDependentModelRel();

                    model.lstOfDependentModelRel.Add(mod);

                }
            }


            return View(model);


        }


        [HttpPost]
        public ActionResult Create(SetupMemberDependentModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            SetupMemberDependentModel objmodl = new SetupMemberDependentModel();
            objmodl.refSetUpMemberShipModel = new SetUpMemberShipModel();

            if (model.lstOfDependentModelRel.Count == 0)
            {

                return View();
            }


            HospitalManagementSystem.Providers.SetupMemberDependentProvider obj = new Providers.SetupMemberDependentProvider();



            int i = obj.Insert(model);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.save;

                objmodl.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == model.SetupMemberID).ToList()));
                objmodl.refSetUpMemberShipModel.SetupMemberID = model.SetupMemberID;
                objmodl.refSetUpMemberShipModel.FirstName = model.refSetUpMemberShipModel.FirstName;
                return View("Relations", objmodl);
            }
            else
            {
                TempData["success"] = UtilityMessage.savefailed;

                objmodl.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == model.SetupMemberID).ToList()));
                objmodl.refSetUpMemberShipModel.SetupMemberID = model.SetupMemberID;
                objmodl.refSetUpMemberShipModel.FirstName = model.FullName;
                return View("Relations", objmodl);

            }



        }


        public ActionResult Edit(int id, string Name)
        {

            SetupMemberDependentProvider pro = new SetupMemberDependentProvider();

            SetupMemberDependentModel model = new SetupMemberDependentModel();



            //model.refSetUpMemberShipModel.FirstName = Name.ToString();


            model = pro.GetRelationWithId(id);
            model.refSetUpMemberShipModel = new SetUpMemberShipModel();

            model.refSetUpMemberShipModel.FirstName = Name.ToString();

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Edit(SetupMemberDependentModel model)
        {

            SetupMemberDependentProvider pro = new SetupMemberDependentProvider();
            SetupMemberDependentModel obj = new SetupMemberDependentModel();

            int i = pro.UpdateRelations(model);

            if (i != 0)
            {

                TempData["success"] = UtilityMessage.edit;
                EHMSEntities ent = new EHMSEntities();

                obj.refSetUpMemberShipModel = new SetUpMemberShipModel();
                obj.refSetUpMemberShipModel.FirstName = model.refSetUpMemberShipModel.FirstName;

                obj.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == model.SetupMemberID).ToList()));
                return View("Relations", obj);





            }
            else
            {

                TempData["success"] = UtilityMessage.editfailed;
                EHMSEntities ent = new EHMSEntities();

                obj.refSetUpMemberShipModel = new SetUpMemberShipModel();
                obj.refSetUpMemberShipModel.FirstName = model.refSetUpMemberShipModel.FirstName;

                model.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == model.SetupMemberID).ToList()));
                return View("Relations", model);



            }





        }


        public ActionResult Relations(int Id, string Name)
        {



            SetupMemberDependentModel model = new SetupMemberDependentModel();

            model.refSetUpMemberShipModel = new SetUpMemberShipModel();

            model.refSetUpMemberShipModel.FirstName = Name;
            model.refSetUpMemberShipModel.SetupMemberID = Id;


            using (EHMSEntities ent = new EHMSEntities())
            {

                model.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == Id).ToList()));
                return View(model);

            }




        }

        public ActionResult RelationsPartial(int id)
        {



            SetupMemberDependentModel model = new SetupMemberDependentModel();

            model.refSetUpMemberShipModel = new SetUpMemberShipModel();


            model.refSetUpMemberShipModel.SetupMemberID = id;


            using (EHMSEntities ent = new EHMSEntities())
            {

                model.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == id).ToList()));
                return PartialView(model);

            }




        }


        public ActionResult GetMemberTypeName(int id)
        {


            EHMSEntities ent = new EHMSEntities();

            var obj = ent.SetupMemberTypes.Where(x => x.MemberTypeID == ent.SetupMemberShips.Where(y => y.SetupMemberID == id).FirstOrDefault().MemberTypeID).SingleOrDefault();

            return Json(obj.MemberTypeID, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDependentNum(int id)
        {



            EHMSEntities ent = new EHMSEntities();

            var obj = ent.SetupMemberShips.Where(x => x.SetupMemberID == id).Select(x => x.MaximumDependent).SingleOrDefault();

            ViewBag.maxdepent = obj;
            return Json(obj, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AddDependentPersion()
        {



            SetupMemberDependentModelRel obj = new SetupMemberDependentModelRel();

            //obj.lstOfDependentModelRel = new List<SetupMemberDependentModelRel>();

            return PartialView("AddDependentPersion");

        }

        public ActionResult Delete(int id, int SetupMemId, string Name)
        {


            SetupMemberDependentModel model = new SetupMemberDependentModel();
            model.refSetUpMemberShipModel = new SetUpMemberShipModel();
            SetupMemberDependentProvider pro = new SetupMemberDependentProvider();
            EHMSEntities ent = new EHMSEntities();
            int i = pro.Delete(id);
            if (i != 0)
            {
                TempData["success"] = UtilityMessage.delete;

                model.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == SetupMemId).ToList()));
                model.refSetUpMemberShipModel.SetupMemberID = SetupMemId;
                model.refSetUpMemberShipModel.FirstName = Name;

            }
            else
            {
                TempData["success"] = UtilityMessage.deletefailed;

                model.lstOfSetupMemberDependentModel = new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents.Where(x => x.SetupMemberID == SetupMemId).ToList()));
                model.refSetUpMemberShipModel.SetupMemberID = SetupMemId;
                model.refSetUpMemberShipModel.FirstName = Name;

            }





            return View("Relations", model);



        }

    }
}
