using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HospitalManagementSystem.Providers
{
    public class DietIPDProviders
    {
        public List<DietTypeSetupViewModel> GetDietTypeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<DietTypeSetupViewModel> List = ent.DietTypeSetups.Select(x => new DietTypeSetupViewModel()
                {
                    DietTypeSetupId = x.DietTypeSetupId,
                    TypeName = x.TypeName
                }).ToList();

                return List;
            }

        }

        public bool InsertDietType(DietIPDModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjToInsert = new DietTypeSetup()
                {
                    TypeName = model.ObjDietTypeSetupViewModel.TypeName,
                    Status = 1

                };
                ent.DietTypeSetups.Add(ObjToInsert);
                int i = ent.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public bool EditDietType(DietIPDModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjToEdit = ent.DietTypeSetups.Where(x => x.DietTypeSetupId == model.ObjDietTypeSetupViewModel.DietTypeSetupId).SingleOrDefault();
                ObjToEdit.TypeName = model.ObjDietTypeSetupViewModel.TypeName;
                int i = ent.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }

        }


        public List<PatientInformationDetailsViewModel> PatientInformation(int id, int ipdid)
        {
            List<PatientInformationDetailsViewModel> PatientList = new List<PatientInformationDetailsViewModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from a in ent.OpdMasters
                             join b in ent.IpdPatientroomDetails on a.OpdID equals b.OpdNoEmergencyNo
                             where a.OpdID == id && b.Status == true
                             select new PatientInformationDetailsViewModel
                             {
                                 FullName = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
                                 RegistrationDate = (DateTime)a.RegistrationDate,
                                 WardNo = b.WardNo,
                                 RoomNo = b.RoomNo,
                                 BedNo = b.BedNo,
                                 PatientID = id,
                                 IpdRegisterationID = ipdid
                             };
                foreach (var item in result)
                {
                    PatientList.Add(item);
                }

            }

            return PatientList;
        }

        public bool insertPatientDietDetails(DietIPDModel model)
        {


            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjToInsert = new PatientDietDetail()
                {
                    IPDRegistrationId = model.ObjPatientDietDetailsViewModel.IPDRegistrationId,
                    OPDId = model.ObjPatientDietDetailsViewModel.OPDId,
                    DietTypeId = model.ObjPatientDietDetailsViewModel.DietTypeId,
                    Diet = model.ObjPatientDietDetailsViewModel.Diet,
                    DietDate = model.ObjPatientDietDetailsViewModel.DietDate,
                    DietTime = DateTime.Now.TimeOfDay,
                    NurseInChargeId = model.ObjPatientDietDetailsViewModel.NurseInChargeId,
                    NurseName = model.ObjPatientDietDetailsViewModel.NurseName,
                    IsDietTaken = 0,//0 is not taken
                    Status = 0,

                };
                ent.PatientDietDetails.Add(ObjToInsert);
                int i = ent.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;

            }
        }

        public List<PatientDietDetailsViewModel> GetPatientDietList(int OpdId, int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PatientDietDetailsViewModel> ReturnList = ent.PatientDietDetails.Where(x => x.OPDId == OpdId && x.IPDRegistrationId == PatientId).Select(x => new PatientDietDetailsViewModel()
                {
                    PatientDietMasterId = x.PatientDietMasterId,
                    OPDId = x.OPDId,
                    IPDRegistrationId = x.IPDRegistrationId,
                    NurseInChargeId = x.NurseInChargeId,
                    NurseName = x.NurseName,
                    DietTypeId = x.DietTypeId,
                    Diet = x.Diet,
                    DietDate = x.DietDate

                }).ToList();

                return ReturnList;
            }
        }


    }
}