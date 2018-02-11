using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using AutoMapper;

namespace HospitalManagementSystem.Provider
{
    public class SetUpIpdRoomProvider
    {


        public List<SetUpIpdRoomModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetUpIpdRoomModel>(AutoMapper.Mapper.Map<IEnumerable<SetUpIpdRoom>, IEnumerable<SetUpIpdRoomModel>>(ent.SetUpIpdRooms));


            }
        }
        public int Insert(SetUpIpdRoomModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToSave = AutoMapper.Mapper.Map<SetUpIpdRoomModel, SetUpIpdRoom>(model);
                ent.SetUpIpdRooms.Add(objToSave);

                for (int i = model.StartBed; i <= model.EndBed; i++)
                {
                    model.ObjIpdRoomStatusModel = new IpdRoomStatusModel();
                    var ObjToSaveRoomStatus = AutoMapper.Mapper.Map<IpdRoomStatusModel, IpdRoomStatu>(model.ObjIpdRoomStatusModel);
                    ObjToSaveRoomStatus.IPDRoomId = objToSave.IpdRoomId;
                    ObjToSaveRoomStatus.RoomNumber = model.RoomNo;
                    ObjToSaveRoomStatus.BedNumber = i;
                    ObjToSaveRoomStatus.Status = true;
                    ent.IpdRoomStatus.Add(ObjToSaveRoomStatus);
                }



                ent.SaveChanges();
            }
            return 1;
        }
        public int Update(SetUpIpdRoomModel model)
        {
            RemoveAllFromRoomStatus(model.IpdRoomId);
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToEdit = ent.SetUpIpdRooms.Where(x => x.IpdRoomId == model.IpdRoomId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
                for (int i = model.StartBed; i <= model.EndBed; i++)
                {
                    model.ObjIpdRoomStatusModel = new IpdRoomStatusModel();
                    var ObjToSaveRoomStatus = AutoMapper.Mapper.Map<IpdRoomStatusModel, IpdRoomStatu>(model.ObjIpdRoomStatusModel);
                    ObjToSaveRoomStatus.IPDRoomId = model.IpdRoomId;
                    ObjToSaveRoomStatus.RoomNumber = model.RoomNo;
                    ObjToSaveRoomStatus.BedNumber = i;
                    ObjToSaveRoomStatus.Status = true;
                    ent.IpdRoomStatus.Add(ObjToSaveRoomStatus);
                }


                ent.SaveChanges();
            }

            return 1;
        }
        public int Delete(int IpdRoomId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetUpIpdRooms.Where(x => x.IpdRoomId == IpdRoomId).FirstOrDefault();

                // var objtodelete = ent.StudentRecords.Where(x => x.StudentRecordId == StudentRecordId).FirstOrDefault();
                ent.SetUpIpdRooms.Remove(objToDelete);
                RemoveAllFromRoomStatus(IpdRoomId);
                ent.SaveChanges();
            }
            return 1;
        }


        public void Details(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<SetUpIpdRoom, SetUpIpdRoomModel>();
                });
                //AutoMapper.Mapper.CreateMap<SetUpIpdRoom, SetUpIpdRoomModel>();
                var Details = ent.SetUpIpdRooms.Where(x => x.IpdRoomId == id).FirstOrDefault();
                SetUpIpdRoomModel det = AutoMapper.Mapper.Map<SetUpIpdRoom, SetUpIpdRoomModel>(Details);


            }


        }

        private void RemoveAllFromRoomStatus(int RoomId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in ent.IpdRoomStatus.Where(x => x.IPDRoomId == RoomId))
                    ent.IpdRoomStatus.Remove(item);
                ent.SaveChanges();


            }
        }
        public int GetRoomCountByRoomType(int RoomType, int RoomNumber)
        {
            EHMSEntities ent = new EHMSEntities();
            var total = ent.SetUpIpdRooms.Where(x => x.RoomType == RoomType && x.RoomNo == RoomNumber).Count();

            return Convert.ToInt32(total);
        }



    }
}