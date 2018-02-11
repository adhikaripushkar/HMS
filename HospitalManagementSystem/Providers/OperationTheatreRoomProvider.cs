using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class OperationTheatreRoomProvider
    {
        public List<OperationTheatreRoomModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<OperationTheatreRoomModel>(AutoMapper.Mapper.Map<IEnumerable<SetupOperationTheatreRoom>, IEnumerable<OperationTheatreRoomModel>>(ent.SetupOperationTheatreRooms.Where(x => x.Stats == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(OperationTheatreRoomModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupOperationTheatreRooms.Any(s => s.RoomName == model.RoomName))
                {
                    return 0;
                }
                var objToSave = AutoMapper.Mapper.Map<OperationTheatreRoomModel, SetupOperationTheatreRoom>(model);
                objToSave.Stats = true;
                //objToSave.ValidUpto = Convert.ToDateTime(model.date);
                ent.SetupOperationTheatreRooms.Add(objToSave);
                ent.SaveChanges();
                return 1;
            }

        }

        public OperationTheatreRoomModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.SetupOperationTheatreRooms.Where(x => x.OperationTheatreRoomID == id).FirstOrDefault();
                OperationTheatreRoomModel model = AutoMapper.Mapper.Map<SetupOperationTheatreRoom, OperationTheatreRoomModel>(obj);
                return model;
            }
        }

        public void Update(OperationTheatreRoomModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.SetupOperationTheatreRooms.Where(x => x.OperationTheatreRoomID == model.OperationTheatreRoomID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoedit);
                objtoedit.Stats = true;
                //objtoedit.ValidUpto = Convert.ToDateTime(model.date);
                ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupOperationTheatreRooms.Where(x => x.OperationTheatreRoomID == id).FirstOrDefault();
            objToDelete.Stats = false;
            ent.Entry(objToDelete).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }

    }
}