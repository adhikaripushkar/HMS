using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class FiscalYearProvider
    {
        public List<FiscalYearModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //this.ContextOptions.LazyLoadingEnabled = false;
                return new List<FiscalYearModel>(AutoMapper.Mapper.Map<IEnumerable<SetupFiscalYear>, IEnumerable<FiscalYearModel>>(ent.SetupFiscalYears).OrderByDescending(x => x.FiscalYearId));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(FiscalYearModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupFiscalYears.Any(s => s.FiscalYearBS == model.FiscalYearBS || s.FiscalYearAD == model.FiscalYearAD))
                {
                    return 0;
                }
                var objList = ent.SetupFiscalYears.ToList();
                foreach (var item in objList)
                {
                    item.IsCurrent = "N";
                    ent.Entry(item).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();
                }

                var objToSave = AutoMapper.Mapper.Map<FiscalYearModel, SetupFiscalYear>(model);

                objToSave.IsCurrent = "N";
                ent.SetupFiscalYears.Add(objToSave);
                ent.SaveChanges();
                return 1;
            }

        }
        public FiscalYearModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SetupFiscalYears.Where(x => x.FiscalYearId == id).FirstOrDefault();
                FiscalYearModel model = AutoMapper.Mapper.Map<SetupFiscalYear, FiscalYearModel>(obj);
                return model;
            }
        }

        public void Update(FiscalYearModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.SetupFiscalYears.Where(x => x.FiscalYearId == model.FiscalYearId).FirstOrDefault();
                char c = Convert.ToChar(objtoedit.IsCurrent);
                AutoMapper.Mapper.Map(model, objtoedit);
                objtoedit.IsCurrent = c.ToString();
                ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public void ChangToCurrent(int id)
        {
            EHMSEntities ent = new EHMSEntities();

            try
            {
                var objt = ent.SetupFiscalYears.Where(x => x.IsCurrent.Equals("Y")).FirstOrDefault();

                objt.IsCurrent = "N";
                ent.Entry(objt).State = System.Data.EntityState.Modified;
                ent.SaveChanges();


            }
            catch (Exception)
            {

                //throw;
            }



            var objN = ent.SetupFiscalYears.Where(x => x.FiscalYearId == id).FirstOrDefault();
            objN.IsCurrent = "Y";
            ent.Entry(objN).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }
}