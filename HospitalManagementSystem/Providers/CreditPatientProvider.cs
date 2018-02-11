//using HospitalManagementSystem;
//using HospitalManagementSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Hospital.Providers
//{
//    public class CreditPatientsProvider
//    {
//        CreditPatientDetailsModel model = new CreditPatientDetailsModel();
//        public List<CreditPatientDetailsModel> PopulateCreditPatients()
//        {
//            using (EHMSEntities ent = new EHMSEntities())
//            {
//                List<CreditPatientDetailsModel> ResultList = ent..Select(x => new CreditPatientDetailsModel()
//                {
//                    CreditPatientID = x.CreditPatientID,
//                    CreditPatientName = x.CreditPatientName,
//                    CreditPatientCase = x.CreditPatientCase,
//                    CreditPatientRefer = x.CreditPatientRefer,
//                    CreditpatientTest = x.CreditpatientTest,
//                    CreditPatientRepresentative = x.CreditPatientRepresentative,
//                    CreditPatientBroughtBy = x.CreditPatientBroughtBy,

//                }).ToList();
//                return ResultList;
//            }
//        }
//        public CreditPatientDetailsModel InsertCreditPatients(CreditPatientDetailsModel model)
//        {
//            using (EHMSEntities ent = new EHMSEntities())
//            {
//                var objInsertCreditPatients = new CreditPatientDetails()
//                {
//                    CreditPatientName = model.CreditPatientName,
//                    CreditPatientCase = model.CreditPatientCase,
//                    CreditPatientRefer = model.CreditPatientRefer,
//                    CreditpatientTest = model.CreditpatientTest,
//                    CreditPatientRepresentative = model.CreditPatientRepresentative,
//                    CreditPatientBroughtBy = model.CreditPatientRepresentative,
//                };
//                int i = 0;
//                try
//                {
//                    ent.CreditPatientDetails.Add(objInsertCreditPatients);
//                    i = ent.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    model.ErrorMessage = ex.ToString();
//                    model.Successmessage = "Error";
//                    return model;
//                }
//                if (i > 0)
//                {
//                    model.Successmessage = "Fail";
//                    return model;
//                }
//                else
//                {
//                    model.Successmessage = "Fail";
//                    return model;
//                }
//            }
//        }
//        public CreditPatientDetailsModel EditCreditPatients(int id, CreditPatientDetailsModel model)
//        {
//            using (EHMSEntities ent = new EHMSEntities())
//            {
//                var objCreditpatientsedit = ent.CreditPatientDetails.ToList().Where(x => x.CreditPatientID == id).SingleOrDefault();
//                objCreditpatientsedit.CreditPatientName = model.CreditPatientName;
//                objCreditpatientsedit.CreditPatientCase = model.CreditPatientCase;
//                objCreditpatientsedit.CreditPatientRefer = model.CreditPatientRefer;
//                objCreditpatientsedit.CreditpatientTest = model.CreditpatientTest;
//                objCreditpatientsedit.CreditPatientRepresentative = model.CreditPatientRepresentative;
//                objCreditpatientsedit.CreditPatientBroughtBy = model.CreditPatientBroughtBy;
//                int i = 0;
//                try
//                {
//                    i = ent.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    model.ErrorMessage = ex.ToString();
//                    model.Successmessage = "Error";
//                    return model;
//                }
//                if (i > 0)
//                {
//                    model.Successmessage = "Success";
//                    return model;
//                }
//                else
//                {
//                    model.Successmessage = "Fail";
//                    return model;
//                }
//            }
//        }
//        public bool DeleteCreditPatients(int id)
//        {
//            using (EHMSEntities ent = new EHMSEntities())
//            {
//                var objDeleteCreditPatients = ent.CreditPatientDetails.ToList().Where(x => x.CreditPatientID == id).SingleOrDefault();
//                ent.CreditPatientDetails.Remove(objDeleteCreditPatients);
//                int i = ent.SaveChanges();
//                if (i > 0)
//                    return true;
//                else
//                    return false;
//            }
//        }
//    }
//}

