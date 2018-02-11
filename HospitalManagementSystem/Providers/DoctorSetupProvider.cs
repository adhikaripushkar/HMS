using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class DoctorSetupProvider
    {
        public List<DoctorSetupModel> Getlist()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<DoctorSetupModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDoctorMaster>, IEnumerable<DoctorSetupModel>>(ent.SetupDoctorMasters));
        }
        public List<DoctorSetupModel> GetlistBySearchWord(string sword)
        {
            string Searchword = sword.ToLower();
            EHMSEntities ent = new EHMSEntities();
            return new List<DoctorSetupModel>(AutoMapper.Mapper.Map<IEnumerable<SetupDoctorMaster>, IEnumerable<DoctorSetupModel>>(ent.SetupDoctorMasters.Where(x => x.FirstName.ToLower().Contains(Searchword))));
        }



        public int Insert(DoctorSetupModel model)
        {
            //Doctorsetup
            EHMSEntities ent = new EHMSEntities();
            var objToSave = AutoMapper.Mapper.Map<DoctorSetupModel, SetupDoctorMaster>(model);
            objToSave.Status = true;
            ent.SetupDoctorMasters.Add(objToSave);
            ent.SaveChanges();
            var data = model.checkBoxlistModel.ToList();

            foreach (var item in data)
            {
                model.DoctorDepartmentSetupModel = new DoctorDepartmentSetupModel();
                if (item.isSelected == true)
                {
                    // var objtosaveDepartment = AutoMapper.Mapper.Map<DoctorDepartmentSetupModel, SETUPDOCTORDEPARTMENT>(model.DoctorDepartmentSetupModel);

                    var objToSaveDetails = AutoMapper.Mapper.Map<DoctorDepartmentSetupModel, SetupDoctorDepartment>(model.DoctorDepartmentSetupModel);

                    objToSaveDetails.DoctorID = objToSave.DoctorID;
                    objToSaveDetails.DepartmentID = Convert.ToInt32(item.DeptID);

                    ent.SetupDoctorDepartments.Add(objToSaveDetails);
                }
            }
            int i = ent.SaveChanges();
            return i;

        }

        //public List<DoctorDepartmentSetupModel> getDetails(int DocId)
        //{
        //    EHMSEntities ent = new EHMSEntities();
        //    List<DoctorDepartmentSetupModel> model = new List<DoctorDepartmentSetupModel>();

        //    var collection = ent.SPDoctList(DocId);

        //    foreach (var item in collection)
        //    {
        //        DoctorDepartmentSetupModel data = new DoctorDepartmentSetupModel() 
        //        {
        //           Status=item.DeptName,
        //           Remarks=item.FirstName
        //        };
        //        model.Add(data);
        //    }
        //    return model;
        //}


        public int Update(DoctorSetupModel model)
        {
            EHMSEntities ent = new EHMSEntities();
            var objtoedit = ent.SetupDoctorMasters.Where(x => x.DoctorID == model.DoctorID).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objtoedit);

            var deptToDelete = ent.SetupDoctorDepartments.Where(y => y.DoctorID == model.DoctorID).ToList();

            foreach (var deptItem in deptToDelete)
            {
                var objToDelete = ent.SetupDoctorDepartments.Where(x => x.DoctorID == model.DoctorID).FirstOrDefault();
                ent.SetupDoctorDepartments.Remove(objToDelete);
                ent.SaveChanges();
            }


            var data = model.checkBoxlistModel.ToList();
            foreach (var item in data)
            {
                model.DoctorDepartmentSetupModel = new DoctorDepartmentSetupModel();
                if (item.isSelected == true)
                {
                    var objToSaveDetails = AutoMapper.Mapper.Map<DoctorDepartmentSetupModel, SetupDoctorDepartment>(model.DoctorDepartmentSetupModel);
                    objToSaveDetails.DoctorID = objtoedit.DoctorID;
                    objToSaveDetails.DepartmentID = Convert.ToInt32(item.DeptID);
                    ent.SetupDoctorDepartments.Add(objToSaveDetails);
                }
            }
            ent.Entry(objtoedit).State = System.Data.EntityState.Modified;
            int i = ent.SaveChanges();

            return i;
        }
        public int Delete(int doctorid)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.SetupDoctorMasters.Where(x => x.DoctorID == doctorid).FirstOrDefault();

            var deptToDelete = ent.SetupDoctorDepartments.Where(y => y.DoctorID == doctorid).ToList();

            foreach (var deptItem in deptToDelete)
            {
                var objToDeleteDept = ent.SetupDoctorDepartments.Where(x => x.DoctorID == doctorid).FirstOrDefault();
                ent.SetupDoctorDepartments.Remove(objToDeleteDept);
                ent.SaveChanges();
            }
            ent.SetupDoctorMasters.Remove(objToDelete);
            int i = ent.SaveChanges();
            return i;
        }
    }
}