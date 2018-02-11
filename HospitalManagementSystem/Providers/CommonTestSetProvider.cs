using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class CommonTestSetProvider
    {



        public List<CommonTestSetupModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<CommonTestSetupModel>(AutoMapper.Mapper.Map<IEnumerable<HospitalExtraTest>, IEnumerable<CommonTestSetupModel>>(ent.HospitalExtraTests));


            }
        }



        public int Insert(CommonTestSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<CommonTestSetupModel, HospitalExtraTest>(model);
                try
                {
                    objToSave.CreatedBy = Utility.GetCurrentLoginUserId();
                    objToSave.CreatedDate = DateTime.Now;
                    objToSave.Vat = 0;
                    objToSave.VatAmount = 0;
                    objToSave.CommonTestTypeId = 1;
                    objToSave.Status = true;
                    ent.HospitalExtraTests.Add(objToSave);
                    i = ent.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
            return i;

        }


        public int Update(CommonTestSetupModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.HospitalExtraTests.Where(x => x.HospitalExtraTestId == model.HospitalExtraTestId).FirstOrDefault();
                model.CommonTestTypeId = 1;
                AutoMapper.Mapper.Map(model, objToEdit);


                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }

            return i;
        }

        public int delete(int id)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.HospitalExtraTests.Where(x => x.HospitalExtraTestId == id).FirstOrDefault();
                objToEdit.Status = false;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }

            return i;

        }


    }
}