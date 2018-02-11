using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;

namespace HospitalManagementSystem.Controllers
{
    public class BillCounterNewController : Controller
    {
        //
        // GET: /BillCounterNew/
        BillingCounterNewProvider pro = new BillingCounterNewProvider();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            BillingCounterNewModel model = new BillingCounterNewModel();
            return View(model);
        }

        // [HttpPost]
        public ActionResult AddMoreParticulars()
        {
            BillingCounterNewTestListModel model = new BillingCounterNewTestListModel();

            return PartialView("_AddMoreParticulars", model);

        }

        //public ActionResult ParticularsDetails(string id, string id2)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {

        //        try
        //        {
        //            string Valuenew = id.Trim();
        //            int BracIndex = id.IndexOf('{') + 1;
        //            int TotalLen = id.IndexOf('}') - 1;
        //            int Len = id.Length;
        //            int toval = Len - BracIndex;
        //            string value = id.Substring(BracIndex, toval - 1);
        //            int aa = value.IndexOf('^') + 1;
        //            int bb = value.Length;
        //            int lenth = bb - aa;
        //            string str = value.Substring(aa, lenth);


        //            var particular = pro.getTestDetailTestIDWiseNew(str, id2).FirstOrDefault();
        //            string conParticular = particular.Rate + "~" + particular.TaxAmount + "~" + particular.TestId;
        //            return Json(conParticular, JsonRequestBehavior.AllowGet);
        //        }
        //        catch
        //        {
        //            return Json("0" + "/" + "0", JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //}

    }
}
