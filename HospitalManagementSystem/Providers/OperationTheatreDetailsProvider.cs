using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class OperationTheatreDetailsProvider
    {
        public List<OperationTheatreDetailsModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<OperationTheatreDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreDetail>, IEnumerable<OperationTheatreDetailsModel>>(ent.OperationTheatreDetails));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public void Insert(OperationTheatreDetailsModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objToSave = AutoMapper.Mapper.Map<OperationTheatreDetailsModel, OperationTheatreDetail>(model);

                objToSave.PersonAllocatedTypeID = 1;
                //objToSave.ValidUpto = Convert.ToDateTime(model.date);
                ent.OperationTheatreDetails.Add(objToSave);
                ent.SaveChanges();

            }

        }

        public OperationTheatreDetailsModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.OperationTheatreDetails.Where(x => x.OperationTheatreDetailsID == id).FirstOrDefault();
                OperationTheatreDetailsModel model = AutoMapper.Mapper.Map<OperationTheatreDetail, OperationTheatreDetailsModel>(obj);
                return model;
            }
        }

        public int Update(OperationTheatreDetailsModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.OperationTheatreDetails.Where(x => x.OperationTheatreDetailsID == model.OperationTheatreDetailsID).FirstOrDefault();
            objToEdit.PersonAllocatedID = model.PersonAllocatedID;
            objToEdit.PersonAllocatedTypeID = model.PersonAllocatedTypeID;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            return objToEdit.OperationTheatreMasterID;
        }

        public int Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objtoDelete = ent.OperationTheatreDetails.Where(x => x.OperationTheatreDetailsID == id).SingleOrDefault();
            ent.OperationTheatreDetails.Remove(objtoDelete);
            ent.SaveChanges();
            return objtoDelete.OperationTheatreMasterID;
        }

    }
}