using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class EmpController : Controller
    {
        //
        // GET: /Emp/

        public ActionResult Index()
        {
            Employee empObj = new Employee();


            empObj.lstOfEmp = empObj.getEmpList();


            return View(empObj);
        }

        public ActionResult More(int id)
        {

            Employee obj = new Employee();
            obj.name = obj.getEmpList().Where(x => x.id == id).Select(x => x.name).SingleOrDefault();
            obj.id = obj.getEmpList().Where(x => x.id == id).Select(x => x.id).SingleOrDefault();

            //var idOfEmp = obj.lstOfEmp.Select(x => x.id == id).SingleOrDefault();



            return View("showMore", obj);
        }

        public ActionResult Address(int id)
        {

            Employee obj = new Employee();

            obj.address = obj.getEmpList().Where(x => x.id == id).Select(x => x.address).SingleOrDefault();

            return PartialView("_Address", obj);

        }

        public ActionResult Test()
        {


            return View();

        }




        public ActionResult TestMultiple()
        {

            EHMSEntities ent = new EHMSEntities();


            return View(new List<OperationTheatreMasterModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreMaster>, IEnumerable<OperationTheatreMasterModel>>(ent.OperationTheatreMasters.Where(x => x.Status == true))));


        }










    }
}
