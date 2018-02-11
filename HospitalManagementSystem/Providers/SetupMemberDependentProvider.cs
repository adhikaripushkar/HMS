using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class SetupMemberDependentProvider
    {





        public List<SetupMemberDependentModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new List<SetupMemberDependentModel>(AutoMapper.Mapper.Map<IEnumerable<SetupMemberDependent>, IEnumerable<SetupMemberDependentModel>>(ent.SetupMemberDependents));


            }
        }




        public int Insert(SetupMemberDependentModel model)
        {
            int i = 0;


            using (EHMSEntities ent = new EHMSEntities())
            {



                foreach (var item in model.lstOfDependentModelRel)
                {

                    if (item.FullName != null)
                    {


                        var objTosave = AutoMapper.Mapper.Map<SetupMemberDependentModel, SetupMemberDependent>(model);

                        objTosave.SetupMemberID = model.SetupMemberID;
                        objTosave.MemberTypeID = model.MemberTypeID;
                        objTosave.RelationID = item.RelationID;
                        objTosave.FullName = item.FullName;

                        objTosave.DateofBirth = item.DateofBirth;
                        objTosave.MoibleNumber = item.MoibleNumber;
                        objTosave.ContactNumber = item.ContactNumber;
                        objTosave.Email = item.Email;
                        objTosave.BloodGroupID = item.BloodGroupID;
                        objTosave.Gender = item.Gender;

                        ent.SetupMemberDependents.Add(objTosave);
                        i = ent.SaveChanges();


                    }


                }



            }

            return i;


        }

        public SetupMemberDependentModel GetRelationWithId(int id)
        {

            SetupMemberDependentModel obj = new SetupMemberDependentModel();

            using (EHMSEntities ent = new EHMSEntities())
            {


                obj = AutoMapper.Mapper.Map<SetupMemberDependent, SetupMemberDependentModel>(ent.SetupMemberDependents.Where(x => x.SetupMemberDetailID == id).FirstOrDefault());

            }


            return obj;
        }


        public int UpdateRelations(SetupMemberDependentModel model)
        {

            int i = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {

                var obj2Save = ent.SetupMemberDependents.Where(x => x.SetupMemberDetailID == model.SetupMemberDetailID).FirstOrDefault();

                AutoMapper.Mapper.Map(model, obj2Save);

                ent.Entry(obj2Save).State = System.Data.EntityState.Modified;

                i = ent.SaveChanges();

            }


            return i;
        }

        public int Delete1234(int id)
        {
            int i = 0;

            EHMSEntities ent = new EHMSEntities();

            var obj2delete = ent.SetupMemberDependents.Where(x => x.SetupMemberDetailID == id).FirstOrDefault();

            try
            {
                ent.SetupMemberDependents.Remove(obj2delete);
                i = ent.SaveChanges();
            }
            catch (Exception e)
            {
                return i;

            }



            return i;
        }


        public int Delete(int id)
        {
            int i = 0;

            EHMSEntities ent = new EHMSEntities();


            try
            {
                var obj = ent.SetupMemberDependents.Where(x => x.SetupMemberDetailID == id).FirstOrDefault();
                ent.SetupMemberDependents.Remove(obj);
                i = ent.SaveChanges();


            }
            catch (Exception e)
            {

                return i;
            }




            return i;

        }



    }
}