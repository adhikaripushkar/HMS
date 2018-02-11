using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class JournalVFormatProvider
    {
        public int Insert(JournalVFormatModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objList = ent.JornalVFormats.ToList();
                foreach (var item in objList)
                {
                    item.IsCurrent = "N";
                    ent.Entry(item).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();
                }
                var objToSave = AutoMapper.Mapper.Map<JournalVFormatModel, JornalVFormat>(model);
                objToSave.IsCurrent = "Y";
                objToSave.JVFormat = objToSave.Prefix + "-" + objToSave.FiscalYear + "-" + objToSave.Postfix;
                ent.JornalVFormats.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;
        }
        public List<JournalVFormatModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<JournalVFormatModel> model = new List<JournalVFormatModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<JornalVFormat>, IEnumerable<JournalVFormatModel>>(ent.JornalVFormats).ToList();
                return model;
            }
        }
        public int Update(JournalVFormatModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.JornalVFormats.Where(x => x.JVId == model.JVId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);

                objToEdit.JVFormat = objToEdit.Prefix + "-" + objToEdit.FiscalYear + "-" + objToEdit.Postfix;

                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                i = ent.SaveChanges();
            }
            return i;
        }
        public int Delete(int id)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.JornalVFormats.Where(x => x.JVId == id).FirstOrDefault();
                ent.JornalVFormats.Remove(objToDelete);
                i = ent.SaveChanges();
            }
            return i;
        }
        public void Changestatus(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objt = ent.JornalVFormats.Where(x => x.IsCurrent.Equals("Y")).FirstOrDefault();
            objt.IsCurrent = "N";
            ent.Entry(objt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            var objtt = ent.JornalVFormats.Where(x => x.JVId == id).FirstOrDefault();
            objtt.IsCurrent = "Y";
            ent.Entry(objtt).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }
    }

}