using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using System.Data.Entity.Validation;
using System.Text;

namespace HospitalManagementSystem.Providers
{
    public class SetupPathoTestProviders
    {
        public List<SetupPathoTestModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetupPathoTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupPathoTest>, IEnumerable<SetupPathoTestModel>>(ent.SetupPathoTests));


            }
        }
        public int Insert(SetupPathoTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetupPathoTestModel, SetupPathoTest>(model);
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                objToSave.Status = 1;
                decimal TaxTotal = Convert.ToDecimal((model.PayableGrandTotal * 5) / 100);
                objToSave.PayableTaxTotal = TaxTotal;

                decimal PathoTaxTotal = Convert.ToDecimal((model.PathologyCommAmt * 5) / 100);
                objToSave.PathologyCommPer = PathoTaxTotal;

                ent.SetupPathoTests.Add(objToSave);
                ent.SaveChanges();
                int LastInsertedId = objToSave.TestId;

                //insert into Gl_accsub group coa
                if (model.SectionId == 24)
                {
                    var GlAccSubgroup = new GL_AccSubGroups()
                    {
                        AccGroupID = 3,
                        AccSubGroupName = model.TestName,
                        ParentID = 423,
                        HierarchyCode = "3.1.2.3.423",
                        HeadLevel = 6,
                        AccountCode = null,
                        IsLeafLevel = true,
                        Status = true,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDate = DateTime.Today,
                        Remarks = "SpecialTest",
                        BranchId = 1

                    };
                    ent.GL_AccSubGroups.Add(GlAccSubgroup);
                    ent.SaveChanges();
                    int LastInsertedCoaId = GlAccSubgroup.AccSubGruupID;
                    //Update setuppatho test
                    SetupPathoTest PathoTest = (from x in ent.SetupPathoTests
                                                where x.TestId == LastInsertedId
                                                select x).First();
                    PathoTest.AccountHeadId = LastInsertedCoaId;
                    try
                    {
                        ent.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (var failure in ex.EntityValidationErrors)
                        {
                            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                            foreach (var error in failure.ValidationErrors)
                            {
                                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                sb.AppendLine();
                            }
                        }

                        throw new DbEntityValidationException(
                            "Entity Validation Failed - errors follow:\n" +
                            sb.ToString(), ex
                        ); // Add the original exception as the innerException
                    }



                }





            }
            return 1;


        }

        public int Update(SetupPathoTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetupPathoTests.Where(x => x.TestId == model.TestId).FirstOrDefault();
                decimal TaxTotal = Convert.ToDecimal((model.PayableGrandTotal * 5) / 100);
                decimal PathoTaxTotal = Convert.ToDecimal((model.PathologyCommAmt * 5) / 100);
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.PayableTaxTotal = TaxTotal;
                objToEdit.PathologyCommPer = PathoTaxTotal;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }

        public int Delete(int TestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupPathoTests.Where(x => x.TestId == TestID).FirstOrDefault();
                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetupPathoTests.Remove(objToDelete);

                ent.SaveChanges();
            }
            return 1;
        }


    }
}