using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupRoomTypeProvider
    {
        public List<SetupRoomTypeModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetupRoomTypeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupRoomType>, IEnumerable<SetupRoomTypeModel>>(ent.SetupRoomTypes));
            }
        }

        public int Insert(SetupRoomTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupRoomTypes.Where(x => x.RoomTypeName.ToLower() == model.RoomTypeName.ToLower());

                if (item.Count() == 0)
                {
                    var objToSave = AutoMapper.Mapper.Map<SetupRoomTypeModel, SetupRoomType>(model);
                    objToSave.DepartmentId = 555;
                    ent.SetupRoomTypes.Add(objToSave);
                    int i = ent.SaveChanges();
                    return i;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Update(SetupRoomTypeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var item = ent.SetupRoomTypes.Where(x => x.RoomTypeName == model.RoomTypeName && x.RoomTypeId != model.RoomTypeId);
                if (item.Count() == 0)
                {
                    var objToUpdate = ent.SetupRoomTypes.Where(x => x.RoomTypeId == model.RoomTypeId).FirstOrDefault();
                    objToUpdate.DepartmentId = 555;
                    AutoMapper.Mapper.Map(model, objToUpdate);
                    ent.Entry(objToUpdate).State = System.Data.EntityState.Modified;
                    int i = ent.SaveChanges();
                    return i;
                }
                else
                    return 0;
            }
        }

        public int Delete(int deleteid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.SetupRoomTypes.Where(x => x.RoomTypeId == deleteid).FirstOrDefault();
                ent.SetupRoomTypes.Remove(objToDelete);
                int i = ent.SaveChanges();
                return i;
            }
        }
    }
}