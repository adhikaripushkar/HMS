using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Providers
{
    public class PatientTestResultProvider
    {
        public List<PopulatePatientTestDetailsModel> GetPatientTestDetailList(int PatientID, int DepartmentID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PopulatePatientTestDetailsModel> PatientDetailList = new List<PopulatePatientTestDetailsModel>();
                var data = (from x in ent.OpdMasters
                            where x.OpdID == PatientID
                            where x.OpdID == PatientID && x.DepartmentId == DepartmentID
                            select new PopulatePatientTestDetailsModel
                            {
                                OpdID = x.OpdID,
                                DepartmentID = (int)(x.DepartmentId),
                                FirstName = x.FirstName,
                                MiddleName = x.MiddleName,
                                LastName = x.LastName,
                                Sex = x.Sex,
                                AgeYear = (int)x.AgeYear
                            }).ToList();
                foreach (var item in data)
                {
                    PatientDetailList.Add(item);
                }
                return PatientDetailList;
            }
        }


        public List<PopulatePatientTestDetailsModel> GetPatientTestDetailListForEmerGency(int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {


                List<PopulatePatientTestDetailsModel> PatientDetailList = new List<PopulatePatientTestDetailsModel>();
                var data = (from x in ent.OpdMasters
                            where x.OpdID == PatientID
                            //where x.OpdID == PatientID && x.DepartmentId == DepartmentID
                            select new PopulatePatientTestDetailsModel
                            {
                                OpdID = x.OpdID,
                                DepartmentID = 1001,
                                FirstName = x.FirstName,
                                MiddleName = x.MiddleName,
                                LastName = x.LastName,
                                Sex = x.Sex,
                                AgeYear = (int)x.AgeYear
                            }).ToList();
                foreach (var item in data)
                {
                    PatientDetailList.Add(item);
                }
                return PatientDetailList;
            }
        }
        //public List<PatientTestResultModel> GetPatienTestInTree(int PatientID, DateTime TestDate, int PatientTestID)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        List<PatientTestResultModel> PatientTestList = new List<PatientTestResultModel>();//final list
        //        List<PatientTestResultModel> tempListForLoop = new List<PatientTestResultModel>();//templist

        //    }
        //}

        public List<PatientTestResultModel> GetPatientTest(int PatientID, DateTime TestDate, int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PatientTestResultModel> PatientTestList = new List<PatientTestResultModel>();



                var test = TestDate.Date;
                var data = (from pt in ent.PatientTests
                            join ptd in ent.PatientTestDetails on pt.PatientTestID equals ptd.PatientTestID
                            join spt in ent.SetupPathoTests on ptd.TestID equals spt.TestId
                            join un in ent.SetupUnits on spt.UnitId equals un.UnitId
                            where pt.PatientTestID == PatientTestID && pt.TestRegistrationDate == TestDate && !ent.PatientTestResults.Select(x => x.PatientTestDetailID).Contains(ptd.PatientTestDetailID)
                            select new { pt.PatientID, ptd.TestID, spt.TestName, ptd.SectionID, ptd.PatientTestID, ptd.PatientTestDetailID, spt.RefRange, spt.ParentId, spt.IsParent, spt.ConvFactor, un.UnitName }).ToList();
                foreach (var item in data)
                {
                    var dataF = ent.Database.SqlQuery<PatientTestResultModel>("getPatientTestListParentWise '" + item.ParentId + "', '" + item.TestID + "'").ToList();

                    //var dataF = (from spt in ent.SetupPathoTest where spt.ParentId==item.TestID
                    //             //pt.PatientTestID == PatientTestID && pt.TestRegistrationDate == TestDate && !ent.PatientTestResult.Select(x => x.PatientTestDetailID).Contains(ptd.PatientTestDetailID)
                    //             select new {spt.TestId, spt.TestName, spt.SectionId, spt.RefRange, spt.ParentId, spt.IsParent }).ToList();

                    //if (item.IsParent == true)
                    //{
                    foreach (var itemF in dataF)
                    {
                        PatientTestResultModel model = new PatientTestResultModel();
                        model.TestID = itemF.TestID;
                        model.TestName = itemF.TestName;
                        model.SectionID = (int)itemF.SectionID;
                        model.PatientTestID = item.PatientTestID;
                        model.PatientTestDetailID = item.PatientTestDetailID;
                        model.RefRange = itemF.RefRange;
                        PatientTestList.Add(model);
                    }
                    //}
                    //else
                    //{
                    //    PatientTestResultModel model = new PatientTestResultModel();
                    //    model.PatientTestID = item.PatientTestID;
                    //    model.TestID = item.TestID;
                    //    model.TestName = item.TestName;
                    //    model.SectionID = item.SectionID;
                    //    model.PatientTestID = item.PatientTestID;
                    //    model.PatientTestDetailID = item.PatientTestDetailID;
                    //    model.RefRange = item.RefRange;
                    //    PatientTestList.Add(model);                        
                    //}


                }
                return PatientTestList;
            }
        }

        public List<PathoSectionModelResult> GetPathoSections()
        {
            List<PathoSectionModelResult> PathoSectionList = new List<PathoSectionModelResult>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupSections
                            select new { x.SectionId, x.Name }).ToList();
                foreach (var item in data)
                {
                    PathoSectionModelResult model = new PathoSectionModelResult();
                    model.Name = item.Name;
                    model.SectionId = item.SectionId;
                    PathoSectionList.Add(model);
                }
                return PathoSectionList;
            }
        }

        public List<PathoSectionModelResult> GetPathoSectionsbyPatientTest(int PatientTestID)
        {
            List<PathoSectionModelResult> PathoSectionList = new List<PathoSectionModelResult>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupSections
                            join y in ent.PatientTestDetails on x.SectionId equals y.SectionID
                            where y.PatientTestID == PatientTestID
                            select new { x.SectionId, x.Name }).ToList().Distinct();
                foreach (var item in data)
                {
                    PathoSectionModelResult model = new PathoSectionModelResult();
                    model.Name = item.Name;
                    model.SectionId = item.SectionId;
                    PathoSectionList.Add(model);
                }
                return PathoSectionList;
            }
        }


        public void Insert(PatientTestResultModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.PatientTestResultModelList)
                {
                    if (item.Value1 != null && item.Value1 != "")
                    {
                        var objTosavePatientTestResult = AutoMapper.Mapper.Map<PatientTestResultModel, PatientTestResult>(model);
                        objTosavePatientTestResult.PatientTestID = item.PatientTestID;
                        objTosavePatientTestResult.PatientTestDetailID = item.PatientTestDetailID;
                        objTosavePatientTestResult.TestID = item.TestID;
                        objTosavePatientTestResult.Value1 = item.Value1;
                        objTosavePatientTestResult.Value2 = item.Value2;
                        objTosavePatientTestResult.Status = true;
                        ent.PatientTestResults.Add(objTosavePatientTestResult);
                    }
                }
                ent.SaveChanges();
            }
        }

        public List<PopulatePatientTestDetailModel> GetPopulatePatientTestDetailModelList(int PatientID, int DepartmentID, string TestDate)
        {//date for TestRegistrationDate
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PopulatePatientTestDetailModel> TempList = new List<PopulatePatientTestDetailModel>();
                var data = (from pt in ent.PatientTests
                            join doc in ent.SetupDoctorMasters on pt.ReferDoctorID equals doc.DoctorID
                            //where pt.PatientID == PatientID && pt.DepartmentID==DepartmentID
                            select new PopulatePatientTestDetailModel
                            {
                                PatientID = pt.PatientID,
                                TestRegistrationDate = pt.TestRegistrationDate,
                                PatientTestID = pt.PatientTestID,
                                PayableAmount = pt.PayableAmount,
                                ReferDoctorID = pt.ReferDoctorID,
                                DoctorName = (doc.FirstName ?? "") + " " + (doc.MiddleName ?? "") + " " + (doc.LastName ?? " "),
                                DepartmentID = pt.DepartmentID

                            });
                DateTime tempTestDate = new DateTime();
                if (TestDate != "" && TestDate != null)
                {
                    tempTestDate = Convert.ToDateTime(TestDate);
                }
                if (PatientID != 0)
                {
                    //with patientID
                    //data = data.Where(con => con.PatientID == PatientID && con.DepartmentID == DepartmentID);
                    data = data.Where(con => con.PatientID == PatientID);
                }
                if (PatientID != 0 && TestDate != "" && TestDate != null)
                {
                    //with testdate
                    data = data.Where(con => con.PatientID == PatientID && con.TestRegistrationDate == tempTestDate);
                    //data = data.Where(con => con.PatientID == PatientID && con.DepartmentID == DepartmentID && con.TestRegistrationDate == tempTestDate);
                }
                if (PatientID == 0 && TestDate != "")
                {
                    //with only testdate
                    //data = data.Where(con => con.DepartmentID == DepartmentID && con.TestRegistrationDate == tempTestDate);
                    data = data.Where(con => con.TestRegistrationDate == tempTestDate);
                    //data = data.ToList();
                }
                foreach (var item in data.ToList())
                {
                    TempList.Add(item);
                }
                return TempList;
            }
        }

        public List<PopulatePatientTestResultReportModel> GetPatientTestReportResultList(int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PopulatePatientTestResultReportModel>("GetPatientTestResult '" + PatientTestID + "'").ToList();
            }
        }
        public List<PopulatePatientTestModel> GetPatientTestList(string fdate, string tdate, int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PopulatePatientTestModel>("GetPatientTestList '" + fdate + "','" + tdate + "','" + PatientID + "'").ToList();
            }
        }
        public List<PopulatePatientTestModel> GetPatientTestListForEmer(string fdate, string tdate, int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PopulatePatientTestModel>("GetPatientTestListForEmer '" + fdate + "','" + tdate + "','" + PatientID + "'").ToList();
            }
        }

        public bool CreateBoneMarrowRpt(BoneMarrowReportsModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var BoneRpt = new BoneMarrowReport()
                {
                    PatientId = model.PatientId,
                    ReferDocId = model.ReferDocId,
                    IPNumber = model.IPNumber,
                    Medicine = model.Medicine,
                    DateOfDispatch = model.DateOfDispatch,
                    SiteOfAspiration = model.SiteOfAspiration,
                    BoneMarrowNumber = model.BoneMarrowNumber,
                    ClinicalFeature = model.ClinicalFeature,
                    PBSFinding = model.PBSFinding,
                    BMAFinding = model.BMAFinding,
                    Cellularity = model.Cellularity,
                    MERation = model.MERation,
                    Myelopoiesis = model.Myelopoiesis,
                    Erythropoiesis = model.Erythropoiesis,
                    Megakaryopoiesis = model.Megakaryopoiesis,
                    Myelogram = model.Myelogram,
                    PlasmaCells = model.PlasmaCells,
                    Hemoparasites = model.Hemoparasites,
                    IMPRESSION = model.IMPRESSION,
                    Remarks1 = model.Remarks1,
                    Remarks2 = model.Remarks2,
                    Status = true,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    PatientLogId = Utility.GetoldPatientLogid(model.PatientId),
                    BranchId = 1,
                    Registerdate = model.Registerdate,
                    WardOrOpd = model.WardOrOpd

                };
                ent.BoneMarrowReports.Add(BoneRpt);
                ent.SaveChanges();
                return true;
            }
        }

        public bool CreateCytologyRpt(CytologyReportsModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var cytoReprot = new CytologyReport()
                {
                    CytopathNo = model.CytopathNo,
                    PatientId = model.PatientId,
                    RefDocId = model.RefDocId,
                    RegId = model.RegId,
                    DateOfDispatch = model.DateOfDispatch,
                    DateOfReceipt = model.DateOfReceipt,
                    WardOrOpd = model.WardOrOpd,
                    ClinicalFeatures = model.ClinicalFeatures,
                    Speciman = model.Speciman,
                    Site = model.Site,
                    ProcedureNote = model.ProcedureNote,
                    MiicroDescription = model.MiicroDescription,
                    Diagnosis = model.Diagnosis,
                    Comments = model.Comments,
                    Status = true,
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today,
                    Remarks = "Patho"


                };
                ent.CytologyReports.Add(cytoReprot);
                ent.SaveChanges();


                return true;
            }

        }

        public BoneMarrowReportsModel GetBoneMarrowReportsList()
        {
            BoneMarrowReportsModel model = new BoneMarrowReportsModel();
            model.BoneMarrowReportsModelList = new List<BoneMarrowReportsModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.BoneMarrowReports.Where(x => x.Status == true);
                foreach (var item in result)
                {
                    var ViewModel = new BoneMarrowReportsModel()
                    {
                        BonmarrowRptId = item.BonmarrowRptId,
                        PatientId = (int)item.PatientId,
                        ReferDocId = (int)item.ReferDocId,
                        ClinicalFeature = item.ClinicalFeature,
                        IPNumber = item.IPNumber,
                        BoneMarrowNumber = item.BoneMarrowNumber


                    };
                    model.BoneMarrowReportsModelList.Add(ViewModel);
                }


            }
            return model;
        }

        public CytologyReportsModel GetCytologyReportsList()
        {
            CytologyReportsModel model = new CytologyReportsModel();
            model.CytologyReportsModelList = new List<CytologyReportsModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.CytologyReports.Where(x => x.Status == true);
                foreach (var item in result)
                {
                    var Viewmodel = new CytologyReportsModel()
                    {
                        CytologyId = item.CytologyId,
                        CytopathNo = item.CytopathNo,
                        ProcedureNote = item.ProcedureNote,
                        Comments = item.Comments,
                        Site = item.Site,
                        Diagnosis = item.Diagnosis,
                        DateOfDispatch = (DateTime)item.DateOfDispatch,
                        DateOfReceipt = (DateTime)item.DateOfReceipt,
                        PatientId = (int)item.PatientId,
                        RefDocId = (int)item.RefDocId,
                        RegId = (int)item.RegId,
                        WardOrOpd = item.WardOrOpd,
                        ClinicalFeatures = item.ClinicalFeatures,
                        Speciman = item.Speciman,
                        MiicroDescription = item.MiicroDescription
                    };

                    model.CytologyReportsModelList.Add(Viewmodel);
                }

                return model;
            }


        }

        public BoneMarrowReportsModel GetBoneMarrowRptFromId(int BoneMarrowId)
        {
            BoneMarrowReportsModel model = new BoneMarrowReportsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var SingleResult = ent.BoneMarrowReports.Where(x => x.BonmarrowRptId == BoneMarrowId).FirstOrDefault();
                model.BonmarrowRptId = SingleResult.BonmarrowRptId;
                model.BoneMarrowNumber = SingleResult.BoneMarrowNumber;
                model.PatientId = (int)SingleResult.PatientId;
                model.ReferDocId = (int)SingleResult.ReferDocId;
                model.Registerdate = (DateTime)SingleResult.Registerdate;
                model.DateOfDispatch = (DateTime)SingleResult.DateOfDispatch;
                model.Cellularity = SingleResult.Cellularity;
                model.ClinicalFeature = SingleResult.ClinicalFeature;
                model.CreatedBy = (int)SingleResult.CreatedBy;
                model.Erythropoiesis = SingleResult.Erythropoiesis;
                model.Myelogram = SingleResult.Myelogram;
                model.MERation = SingleResult.MERation;
                model.Megakaryopoiesis = SingleResult.Megakaryopoiesis;
                model.IMPRESSION = SingleResult.IMPRESSION;
                model.Hemoparasites = SingleResult.Hemoparasites;
                model.PlasmaCells = SingleResult.PlasmaCells;
                model.BMAFinding = SingleResult.BMAFinding;
                model.WardOrOpd = SingleResult.WardOrOpd;
                model.Myelopoiesis = SingleResult.Myelopoiesis;
                model.PBSFinding = SingleResult.PBSFinding;
                model.Remarks1 = SingleResult.Remarks1;
                model.Remarks2 = SingleResult.Remarks2;
                model.IPNumber = SingleResult.IPNumber;

            }
            return model;
        }


        public CytologyReportsModel GetCytologyReportByRegNumber(string CytopathNo, int RegisterId)
        {
            CytologyReportsModel model = new CytologyReportsModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ResultSingle = ent.CytologyReports.Where(x => x.RegId == RegisterId).FirstOrDefault();

                model.CytologyId = ResultSingle.CytologyId;
                model.CytopathNo = ResultSingle.CytopathNo;
                model.ProcedureNote = ResultSingle.ProcedureNote;
                model.Comments = ResultSingle.Comments;
                model.Site = ResultSingle.Site;
                model.Diagnosis = ResultSingle.Diagnosis;
                model.DateOfDispatch = (DateTime)ResultSingle.DateOfDispatch;
                model.DateOfReceipt = (DateTime)ResultSingle.DateOfReceipt;
                model.PatientId = (int)ResultSingle.PatientId;
                model.RefDocId = (int)ResultSingle.RefDocId;
                model.RegId = (int)ResultSingle.RegId;
                model.WardOrOpd = ResultSingle.WardOrOpd;
                model.ClinicalFeatures = ResultSingle.ClinicalFeatures;
                model.Speciman = ResultSingle.Speciman;
                model.MiicroDescription = ResultSingle.MiicroDescription;



                return model;
            }

        }



    }
}