using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;


namespace HospitalManagementSystem.Providers
{
    public class SetUpMemberShipProvider
    {

        public int GetTotalItemCount()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.SetupMemberShips.Count();
            }
        }


        public List<SetUpMemberShipModel> GetlistforIndex(int page, int pagesize)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetUpMemberShipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberShip>, IEnumerable<SetUpMemberShipModel>>(ent.SetupMemberShips.OrderBy(x => x.SetupMemberID).Skip((page - 1) * pagesize).Take(pagesize)));

                // ent.Departments.OrderBy(x=>x.DepartmentId).Skip((page - 1) * pagesize).Take(pagesize);


            }

        }


        public List<SetUpMemberShipModel> Getlist()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<SetUpMemberShipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberShip>, IEnumerable<SetUpMemberShipModel>>(ent.SetupMemberShips));

                // ent.Departments.OrderBy(x=>x.DepartmentId).Skip((page - 1) * pagesize).Take(pagesize);


            }

        }


        public List<SetUpMemberShipModel> GetlistWithName(string value)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return new List<SetupEmployeeTypeModel>(AutoMapper.Mapper.Map<IEnumerable<SetupEmployeeType>, IEnumerable<SetupEmployeeTypeModel>>(ent.SetupEmployeeType));

                return new List<SetUpMemberShipModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberShip>, IEnumerable<SetUpMemberShipModel>>(ent.SetupMemberShips.Where(x => x.FirstName.ToLower().Contains(value.ToLower())).ToList()));

            }

        }


        public int Insert(SetUpMemberShipModel model)
        {
            int i = 0;


            using (EHMSEntities ent = new EHMSEntities())
            {



                var objToSave = AutoMapper.Mapper.Map<SetUpMemberShipModel, SetupMemberShip>(model);
                objToSave.CreatedBy = 0;
                objToSave.CreatedDate = DateTime.Now;

                ent.SetupMemberShips.Add(objToSave);
                i = ent.SaveChanges();


            }

            return i;


        }





        public SetUpMemberShipModel GetMemberShipWithId(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var obj = ent.SetupMemberShips.Where(x => x.SetupMemberID == id).FirstOrDefault();
                SetUpMemberShipModel model = AutoMapper.Mapper.Map<SetupMemberShip, SetUpMemberShipModel>(obj);

                return model;

            }



        }



        //public void Update(OperationTheatreRoomModel model)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var objtoedit = ent.SetupOperationTheatreRoom.Where(x => x.OperationTheatreRoomID == model.OperationTheatreRoomID).FirstOrDefault();
        //        AutoMapper.Mapper.Map(model, objtoedit);
        //        objtoedit.Stats = true;
        //        //objtoedit.ValidUpto = Convert.ToDateTime(model.date);
        //        ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
        //        ent.SaveChanges();
        //    }
        //}



        public int Update(SetUpMemberShipModel model)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtosave = ent.SetupMemberShips.Where(x => x.SetupMemberID == model.SetupMemberID).SingleOrDefault();

                model.CreatedBy = objtosave.CreatedBy;
                model.CreatedDate = objtosave.CreatedDate;
                model.Status = objtosave.Status;

                AutoMapper.Mapper.Map(model, objtosave);

                ent.Entry(objtosave).State = System.Data.EntityState.Modified;

                i = ent.SaveChanges();


            }

            return i;


        }




        public SetUpMemberShipModel GetDetailsOfMembershipWithId(int id)
        {


            SetUpMemberShipModel model = new SetUpMemberShipModel();

            using (EHMSEntities ent = new EHMSEntities())
            {

                var obj = ent.SetupMemberShips.Where(x => x.SetupMemberID == id).SingleOrDefault();

                model = AutoMapper.Mapper.Map<SetupMemberShip, SetUpMemberShipModel>(obj);



            }



            return model;

        }

        public int Delete(int Id)
        {
            int i = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {





                var objToDelete = ent.SetupMemberShips.Where(x => x.SetupMemberID == Id).FirstOrDefault();
                var objToDeleteDependent = ent.SetupMemberDependents.Where(x => x.SetupMemberID == Id).ToList();







                //if (objToDeleteDependent!=null)
                //{
                //    using (EHMSEntities ent1 = new EHMSEntities())
                //    {
                //        ent1.SetupMemberDependent.Remove(objToDeleteDependent);

                //        i = ent1.SaveChanges();
                //    }
                //}

                try
                {
                    foreach (var item in objToDeleteDependent)
                    {
                        ent.SetupMemberDependents.Remove(item);

                    }



                    ent.SetupMemberShips.Remove(objToDelete);


                }
                catch (Exception e)
                {
                    ent.SetupMemberShips.Remove(objToDelete);

                }

                i = ent.SaveChanges();








            }
            return i;


        }









    }
}