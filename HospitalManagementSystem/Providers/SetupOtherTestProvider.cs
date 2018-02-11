using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Providers
{
    public class SetupOtherTestProvider
    {

        public List<SetupOtherTestModel> GetlistByDepartmentName(int? DeptId)
        {
            EHMSEntities ent = new EHMSEntities();
            if (DeptId != 0)
            {
                return new List<SetupOtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupOtherTest>, IEnumerable<SetupOtherTestModel>>(ent.SetupOtherTests).Where(x => x.Status == true && x.TestTypeID == DeptId));
            }
            else
            {
                return new List<SetupOtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupOtherTest>, IEnumerable<SetupOtherTestModel>>(ent.SetupOtherTests).Where(x => x.Status == true));
            }

        }
        public List<SetupOtherTestModel> Getlist()
        {
            EHMSEntities ent = new EHMSEntities();


            return new List<SetupOtherTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupOtherTest>, IEnumerable<SetupOtherTestModel>>(ent.SetupOtherTests).Where(x => x.Status == true));


        }

        public int insert(SetupOtherTestModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            int i = 0;
            var objToSave = AutoMapper.Mapper.Map<SetupOtherTestModel, SetupOtherTest>(model);
            objToSave.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
            objToSave.Status = true;
            objToSave.CreatedDate = DateTime.Now;
            decimal TaxTotal = Convert.ToDecimal((model.PayableGrandTotal * 5) / 100);
            objToSave.PayableTaxTotal = TaxTotal;
            ent.SetupOtherTests.Add(objToSave);

            i = ent.SaveChanges();
            return i;

        }
        public int Update(SetupOtherTestModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.SetupOtherTests.Where(x => x.SetupOtherTestId == model.SetupOtherTestId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);
            decimal TaxTotal = Convert.ToDecimal((model.PayableGrandTotal * 5) / 100);
            objToEdit.PayableTaxTotal = TaxTotal;
            objToEdit.Status = true;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();
            return i;

        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupOtherTests.Where(x => x.SetupOtherTestId == id).FirstOrDefault();
                objToEdit.Status = false;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }
            return i;
        }






    }
}