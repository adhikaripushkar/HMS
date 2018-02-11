using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class PatientDischargedController : Controller
    {
        //
        // GET: /PatientDischarged/



        public ActionResult Index()
        {

            IpdPatientDeischargedModel obj = new IpdPatientDeischargedModel();
            obj.lstIpdPatientDeischarged = new List<IpdPatientDeischargedModel>();
            obj.lstIpdPatientDeischarged = obj.GetList();

            return View(obj.lstIpdPatientDeischarged);
        }

        public ActionResult EditDischarged(int id, int opdid, int ipdregid, int bedid)
        {
            EHMSEntities ent = new EHMSEntities();
            IpdDischargeModel model = new IpdDischargeModel();
            var valuesDis = ent.IpdDischarges.Where(x => x.IpdDischargeID == id).FirstOrDefault();

            AutoMapper.Mapper.Map(valuesDis, model);
            model.refOfIpdRegistrationMasterModel = new IpdRegistrationMasterModel();

            model.refOfIpdRegistrationMasterModel = Utility.GetIpdRegistrationIdWithPatientId(opdid);
            model.refOfIpdRegistrationMasterModel.OpdNoEmergencyNo = opdid;

            // model.DignosisID = digid;
            //model.Diagnosis = ent.IpdDischarge.Where(x => x.IpdDischargeID == dischargeid).SingleOrDefault().Dignosis;
            model.Diagnosis = ent.IpdDischarges.Where(x => x.IpdDischargeID == id).SingleOrDefault().Dignosis;
            model.ipdResistrationID = ipdregid;
            model.IpdPatientBedDetailId = bedid;
            //model.Opdid = opdid;


            return View(model);

        }



        [HttpPost]
        public ActionResult EditDischarged(IpdDischargeModel model)
        {


            EHMSEntities ent = new EHMSEntities();
            IpdPatientDeischargedModel obj = new IpdPatientDeischargedModel();

            int i = obj.UpdateIpdDischargeDetails(model);

            if (i != 0)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult PatientBillDetails(int id)
        {

            IpdPatientDeischargedModel model = new IpdPatientDeischargedModel();
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            BillingCounterProvider Bpro = new BillingCounterProvider();
            model.OpdID = HospitalManagementSystem.Utility.GetPatientOpdIdFromIpdId(id);

            model = pro.GetPatientBillDetails(id);
            model.IpdRegistrationID = id;

            //model.ObjPatientBillDetailsViewModelNew.AccsubgroupHeadId=
            int PatientAccountHeadId = HospitalManagementSystem.Utility.GetPatientAccHeadIdFromOpdId(model.OpdID);
            if (PatientAccountHeadId > 0)
            {
                model.ObjPatientBillDetailsViewModelNew.BalanceDeposit = Bpro.getBalanceDeposit(Convert.ToInt32(PatientAccountHeadId));
                model.ObjPatientBillDetailsViewModelNew.AccsubgroupHeadId = PatientAccountHeadId;
            }
            else
            {
                model.ObjPatientBillDetailsViewModelNew.BalanceDeposit = Convert.ToDecimal(0);

            }


            return View(model);

        }

        [HttpPost]
        public ActionResult PatientBillDetails(IpdPatientDeischargedModel model)
        {
            IpdRegistrationMasterProvider pro = new IpdRegistrationMasterProvider();
            pro.InsertPatientBedDetailDischarge(model);
            return RedirectToAction("Index");
            //return RedirectToAction("PrintBillCb", "BillingCounter"); 



        }

    }
}
