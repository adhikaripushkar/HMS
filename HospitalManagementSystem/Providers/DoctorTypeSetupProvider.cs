using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class DoctorTypeSetupProvider
    {
        EHMSEntities ent = new EHMSEntities();

        public List<DoctorTypeSetupModel> GetListofDoctorTypeSetupModel()
        {

            // return new List<SetupManpowerModel>(AutoMapper.Mapper.Map<IEnumerable<SetupManpower>, IEnumerable<SetupManpowerModel>>(ent.SetupManpower));

            return new List<DoctorTypeSetupModel>(AutoMapper.Mapper.Map<IEnumerable<DoctorTypeSetup>, IEnumerable<DoctorTypeSetupModel>>(ent.DoctorTypeSetups));

        }

        public int insert(DoctorTypeSetupModel model)
        {
            int i = 0;
            var objToSave = AutoMapper.Mapper.Map<DoctorTypeSetupModel, DoctorTypeSetup>(model);
            ent.DoctorTypeSetups.Add(objToSave);
            i = ent.SaveChanges();


            return i;

        }


        public int Update(DoctorTypeSetupModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.DoctorTypeSetups.Where(x => x.DoctorTypeId == model.DoctorTypeId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);
            // ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();

            return i;


        }

        public DoctorTypeSetupModel GetDoctorSpecialistByDoctorId(int DoctorId)
        {
            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var result = ent.DoctorsDiseases.Where(x => x.Status == true && x.DoctorID == DoctorId).ToList();
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var ObjModel = new DoctorSpecialistViewModel()
                        {
                            DepartmentID = item.DepartmentID,
                            DiseaseID = item.DiseaseID,
                            DoctorDiseaseID = item.DoctorDiseaseID,
                            DiseaseName = item.DiseaseName,
                            DoctorID = item.DoctorID
                        };
                        model.DoctorSpecialistViewModelList.Add(ObjModel);
                    }


                }
                return model;
            }
        }



        public DoctorTypeSetupModel GetDoctorSpecialistList()
        {

            DoctorTypeSetupModel model = new DoctorTypeSetupModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.DoctorsDiseases.Where(x => x.Status == true).ToList();
                foreach (var item in result)
                {
                    var Obj = new DoctorSpecialistViewModel()
                    {
                        DepartmentID = item.DepartmentID,
                        DiseaseID = item.DiseaseID,
                        DoctorDiseaseID = item.DoctorDiseaseID,
                        DiseaseName = item.DiseaseName,
                        DoctorID = item.DoctorID
                    };
                    model.DoctorSpecialistViewModelList.Add(Obj);

                }
            }
            return model;
        }

        public bool UpdateDoctorDisease(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return true;
            }
        }


        public bool DeleteDoctorDisease(int DoctorDiseasesId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return true;
            }

        }

        public bool InsertIntoDoctorDisease(DoctorTypeSetupModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = new DoctorsDisease()
                {
                    DepartmentID = model.ObjDoctorSpecialistViewModel.DepartmentID,
                    DoctorID = model.ObjDoctorSpecialistViewModel.DoctorID,
                    DiseaseID = model.ObjDoctorSpecialistViewModel.DiseaseID,
                    DiseaseName = model.ObjDoctorSpecialistViewModel.DiseaseName,
                    Status = true,
                    BranchId = 1

                };
                ent.DoctorsDiseases.Add(result);
                ent.SaveChanges();
                return true;
            }
        }
    }
}