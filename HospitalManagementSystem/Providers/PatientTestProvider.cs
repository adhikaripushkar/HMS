using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class PatientTestProvider
    {
        public List<PatientInformationModel> GetPatientBasicInformation(int PatientID, int DepartmentID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<PatientInformationModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<PatientInformationModel>>(ent.EmergencyMasters).Where(x => x.EmergencyMasterId == PatientID));
            }
        }

        public bool CheckOpdIdExistOrNot(int opdId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.OpdMasters.Where(x => x.OpdID == opdId).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckEmergencyIdExistOrNot(int EmergencyId)
        {
            EHMSEntities ent = new EHMSEntities();
            var isExist = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == EmergencyId).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<PatientInformationModel> GetPatientBasicInformationFromOpd(int PatientID, int DepartmentID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var singleValue = ent.OpdMasters.Where(x => x.OpdID == PatientID).SingleOrDefault();

                if (singleValue != null)
                {

                    List<PatientInformationModel> myList = new List<PatientInformationModel>();
                    PatientInformationModel objPatientInf = new PatientInformationModel();
                    objPatientInf.EmergencyMasterId = singleValue.OpdID;
                    objPatientInf.FirstName = singleValue.FirstName;
                    objPatientInf.MiddleName = singleValue.MiddleName;
                    objPatientInf.LastName = singleValue.LastName;
                    objPatientInf.Age = singleValue.AgeYear.GetValueOrDefault();
                    objPatientInf.Gender = singleValue.Sex;
                    objPatientInf.DepartmentID = singleValue.DepartmentId.GetValueOrDefault();
                    objPatientInf.Address = singleValue.Address;
                    //objPatientInf.PhoneNo = singleValue.ContactName;
                    myList.Add(objPatientInf);
                    return myList;
                }
                else
                {
                    return null;
                }
            }
        }


        public List<PatientInformationModel> GetPatientBasicInformationFromEmergency(int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var singleValue = ent.OpdMasters.Where(x => x.OpdID == PatientID).SingleOrDefault();
                List<PatientInformationModel> myList = new List<PatientInformationModel>();
                PatientInformationModel objPatientInf = new PatientInformationModel();
                objPatientInf.EmergencyMasterId = singleValue.OpdID;
                objPatientInf.FirstName = singleValue.FirstName;
                objPatientInf.MiddleName = singleValue.MiddleName;
                objPatientInf.LastName = singleValue.LastName;
                objPatientInf.Age = singleValue.AgeYear.GetValueOrDefault();
                objPatientInf.Gender = singleValue.Sex;
                objPatientInf.DepartmentID = 10;
                myList.Add(objPatientInf);
                return myList;
            }
        }

        public List<PathoSectionModel> GetPathoSections()
        {
            List<PathoSectionModel> PathoSectionList = new List<PathoSectionModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupSections
                            select new { x.SectionId, x.Name }).ToList();
                //PathoSectionModel model = new PathoSectionModel()
                //{
                //    Name = data.Name,
                //    SectionId = data.SectionId

                //};
                foreach (var item in data)
                {
                    PathoSectionModel model = new PathoSectionModel();
                    model.Name = item.Name;
                    model.SectionId = item.SectionId;
                    PathoSectionList.Add(model);
                }


                //List<string> list = new List<string>();
                //list.Add("adad");
                //list.Add("22324");


                return PathoSectionList;
            }
        }
        public List<PathoTestModel> GetPathoTestSectionWise(int SectionID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<PathoTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupPathoTest>, IEnumerable<PathoTestModel>>(ent.SetupPathoTests).Where(x => x.SectionId == SectionID));
                // return new List<PathoTestModel>(AutoMapper.Mapper.Map<IEnumerable<SetupPathoTest>, IEnumerable<PathoTestModel>>(ent.SetupPathoTest));
            }
        }

        public List<TestCheckBoxListModel> GetAllTestWithCheckBoxes(int SectionID)
        {
            List<TestCheckBoxListModel> TestCheckBoxListModelList = new List<TestCheckBoxListModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.SetupPathoTests.ToList();
                foreach (var i in data)
                {
                    TestCheckBoxListModel model = new TestCheckBoxListModel();
                    model.SectionId = (Int32)i.SectionId;
                    model.TestId = i.TestId;
                    model.TestName = i.TestName;
                    model.Price = i.Price;
                    model.isSelected = false;
                    TestCheckBoxListModelList.Add(model);
                }
                return TestCheckBoxListModelList;
            }
        }

        public List<TestCheckBoxListModel> PopulatePatientTestForEdit(int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<TestCheckBoxListModel> tempList = new List<TestCheckBoxListModel>();
                List<TestCheckBoxListModel> tempListForPatientTest = new List<TestCheckBoxListModel>();

                var data = (from pt in ent.PatientTests
                            join ptd in ent.PatientTestDetails on pt.PatientTestID equals ptd.PatientTestID
                            join st in ent.SetupPathoTests on ptd.TestID equals st.TestId
                            where pt.PatientTestID == PatientTestID
                            select new TestCheckBoxListModel
                            {
                                TestDate = ptd.TestDate,
                                TestTime = ptd.TestTime,
                                SectionId = ptd.SectionID,
                                TestId = ptd.TestID,
                                TestName = st.TestName,
                                Price = st.Price,
                                DeliveryDate = ptd.DeliveryDate,
                                isSelected = true
                            }).ToList();
                foreach (var item in data)
                {
                    tempList.Add(item);
                }
                var testData = ent.SetupPathoTests.ToList();
                foreach (var item in testData)
                {
                    TestCheckBoxListModel model = new TestCheckBoxListModel();
                    var isExists = tempList.Where(x => x.TestId == item.TestId).FirstOrDefault();
                    model.SectionId = (int)item.SectionId;
                    model.Price = item.Price;
                    model.TestName = item.TestName;

                    if (isExists != null)
                    {
                        model.TestTime = isExists.TestTime;
                        model.DeliveryDate = isExists.DeliveryDate;
                        model.isSelected = true;
                        model.DeliveryDate = isExists.DeliveryDate;
                        model.TestDate = isExists.TestDate;
                    }
                    tempListForPatientTest.Add(model);
                }
                return tempListForPatientTest;
            }
        }
        public List<PatientTestModel> PopulatePatientTestMasterForEdit(int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<PatientTestModel> tempList = new List<PatientTestModel>();
                var data = (from pt in ent.PatientTests
                            where pt.PatientTestID == PatientTestID
                            select new PatientTestModel
                            {
                                PatientID = pt.PatientID,
                                TotalAmount = pt.TotalAmount,
                                Discount = pt.Discount,
                                PayableAmount = pt.PayableAmount,
                                TestRegistrationDate = pt.TestRegistrationDate,
                                ReferDoctorID = pt.ReferDoctorID
                            });
                foreach (var item in data)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }
        public void Insert(PatientTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int PatientDepartmentId = 0;
                var GetSourceIdFromOpdEmrID = 0;
                if (model.DepartmentID == 1000)//opd
                {
                    //get user original departmentId
                    var patientDeptId = ent.OpdMasters.Where(m => m.OpdID == model.PatientInformationModel.EmergencyMasterId).FirstOrDefault().DepartmentId;
                    PatientDepartmentId = Convert.ToInt32(patientDeptId);
                    GetSourceIdFromOpdEmrID = ent.GL_LedgerMaster.Where(m => m.SourceID == model.PatientInformationModel.EmergencyMasterId && m.DepartmentID == PatientDepartmentId).Select(m => m.LedgerMasterID).FirstOrDefault();

                }
                else if (model.DepartmentID == 1001)//emergency
                {

                    var patientDeptId = ent.OpdMasters.Where(m => m.OpdID == model.PatientInformationModel.EmergencyMasterId).FirstOrDefault().DepartmentId;
                    PatientDepartmentId = Convert.ToInt32(patientDeptId);
                    GetSourceIdFromOpdEmrID = ent.GL_LedgerMaster.Where(m => m.SourceID == model.PatientInformationModel.EmergencyMasterId && m.DepartmentID == PatientDepartmentId).Select(m => m.LedgerMasterID).FirstOrDefault();

                }
                else//ipd
                {
                    var patientDeptId = ent.OpdMasters.Where(m => m.OpdID == model.PatientInformationModel.EmergencyMasterId).FirstOrDefault().DepartmentId;
                    PatientDepartmentId = Convert.ToInt32(patientDeptId);
                    GetSourceIdFromOpdEmrID = ent.GL_LedgerMaster.Where(m => m.SourceID == model.PatientInformationModel.EmergencyMasterId && m.DepartmentID == PatientDepartmentId).Select(m => m.LedgerMasterID).FirstOrDefault();
                }


                var objTosavePatientTest = AutoMapper.Mapper.Map<PatientTestModel, PatientTest>(model);
                objTosavePatientTest.PatientID = model.PatientInformationModel.EmergencyMasterId;
                objTosavePatientTest.TestRegistrationDate = DateTime.Now;
                objTosavePatientTest.ReferDoctorID = model.ReferDoctorID;
                objTosavePatientTest.DepartmentID = PatientDepartmentId;
                objTosavePatientTest.Status = true;
                ent.PatientTests.Add(objTosavePatientTest);
                ent.SaveChanges();


                //ent.SaveChanges();

                foreach (var item in model.TestCheckBoxListModelList)
                {
                    //if (item.isSelected == true)
                    //{                 

                    model.PatientTestDetailModel = new PatientTestDetailModel();
                    var objToSavePatientTestDetails = AutoMapper.Mapper.Map<PatientTestDetailModel, PatientTestDetail>(model.PatientTestDetailModel);
                    objToSavePatientTestDetails.PatientID = model.PatientInformationModel.EmergencyMasterId;
                    objToSavePatientTestDetails.PatientTestID = objTosavePatientTest.PatientTestID;
                    //objToSavePatientTestDetails.DepartmentID = model.DepartmentID;
                    objToSavePatientTestDetails.DepartmentID = PatientDepartmentId;
                    objToSavePatientTestDetails.SectionID = item.SectionId;
                    objToSavePatientTestDetails.TestID = item.TestId;
                    objToSavePatientTestDetails.TestDate = DateTime.Now;
                    //objToSavePatientTestDetails.TestTime = item.TestTime;
                    //objToSavePatientTestDetails.DeliveryDate = item.DeliveryDate;
                    objToSavePatientTestDetails.Amount = (decimal)item.Price;
                    objToSavePatientTestDetails.Discount = item.DiscountPer;
                    objToSavePatientTestDetails.TotalAmount = (decimal)item.Price - item.DiscountPer;
                    objToSavePatientTestDetails.DeliveryStatus = false;
                    ent.PatientTestDetails.Add(objToSavePatientTestDetails);

                    //for times
                    if (item.Tim > 1)
                    {
                        for (int i = 0; i < item.Tim - 1; i++)
                        {
                            var objTosavePatientTestforTim = AutoMapper.Mapper.Map<PatientTestModel, PatientTest>(model);
                            objTosavePatientTestforTim.PatientID = model.PatientInformationModel.EmergencyMasterId;
                            objTosavePatientTestforTim.TestRegistrationDate = DateTime.Now;
                            objTosavePatientTestforTim.ReferDoctorID = model.ReferDoctorID;
                            objTosavePatientTestforTim.DepartmentID = PatientDepartmentId;
                            objTosavePatientTestforTim.Status = true;
                            ent.PatientTests.Add(objTosavePatientTestforTim);

                            model.PatientTestDetailModel = new PatientTestDetailModel();
                            var objToSavePatientTestDetailsForTim = AutoMapper.Mapper.Map<PatientTestDetailModel, PatientTestDetail>(model.PatientTestDetailModel);
                            objToSavePatientTestDetailsForTim.PatientID = model.PatientInformationModel.EmergencyMasterId;
                            objToSavePatientTestDetailsForTim.PatientTestID = objTosavePatientTestforTim.PatientTestID;
                            //objToSavePatientTestDetails.DepartmentID = model.DepartmentID;
                            objToSavePatientTestDetailsForTim.DepartmentID = PatientDepartmentId;
                            objToSavePatientTestDetailsForTim.SectionID = item.SectionId;
                            objToSavePatientTestDetailsForTim.TestID = item.TestId;
                            objToSavePatientTestDetailsForTim.TestDate = DateTime.Now;
                            //objToSavePatientTestDetails.TestTime = item.TestTime;
                            //objToSavePatientTestDetails.DeliveryDate = item.DeliveryDate;
                            objToSavePatientTestDetailsForTim.Amount = (decimal)item.Price;
                            objToSavePatientTestDetailsForTim.Discount = item.DiscountPer;
                            objToSavePatientTestDetailsForTim.TotalAmount = (decimal)item.Price - item.DiscountPer;
                            objToSavePatientTestDetailsForTim.DeliveryStatus = false;
                            ent.PatientTestDetails.Add(objToSavePatientTestDetailsForTim);
                            ent.SaveChanges();
                        }
                    }


                    // }
                }
                //added by Gopal on April 4 2014-Centralized Billing
                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();
                foreach (var item in model.TestCheckBoxListModelList)
                {
                    //Rate
                    var objCentralizedBilling = new CentralizedBilling()
                    {
                        AccountHeadId = 15,
                        Amount = (decimal)item.Rate,
                        AmountDate = DateTime.Now,
                        PaymentType = "Cash",
                        Narration1 = item.TestName,
                        DepartmentName = "Patho",
                        SubDepartmentId = PatientDepartmentId,
                        BillNumber = BillNumberStr,
                        LedgerMasterId = GetSourceIdFromOpdEmrID,
                        PatientId = model.PatientInformationModel.EmergencyMasterId,
                        PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.PatientInformationModel.EmergencyMasterId),
                        JVStatus = false,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        CreatedDate = DateTime.Now,
                        Remarks = "NA",
                        Status = true,
                        PaidOnPaid = true,
                        Narration2 = item.Tim.ToString(),
                        ItemID = item.TestId
                    };
                    ent.CentralizedBillings.Add(objCentralizedBilling);

                    //item wise tax
                    objCentralizedBilling = new CentralizedBilling()
                    {
                        AccountHeadId = 16,
                        Amount = (decimal)item.TaxAmount,
                        AmountDate = DateTime.Now,
                        PaymentType = "Cash",
                        Narration1 = item.TestName,
                        DepartmentName = "Patho",
                        SubDepartmentId = PatientDepartmentId,
                        BillNumber = BillNumberStr,
                        LedgerMasterId = GetSourceIdFromOpdEmrID,
                        PatientId = model.PatientInformationModel.EmergencyMasterId,
                        PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.PatientInformationModel.EmergencyMasterId),
                        JVStatus = false,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        CreatedDate = DateTime.Now,
                        Remarks = "NA",
                        Status = true,
                        PaidOnPaid = true,
                        Narration2 = item.Tim.ToString(),
                        ItemID = item.TestId
                    };
                    ent.CentralizedBillings.Add(objCentralizedBilling);
                    //item wise discount
                    objCentralizedBilling = new CentralizedBilling()
                    {
                        AccountHeadId = 22,
                        Amount = (decimal)item.DiscountPer,
                        AmountDate = DateTime.Now,
                        PaymentType = "Cash",
                        Narration1 = item.TestName,
                        DepartmentName = "Patho",
                        SubDepartmentId = PatientDepartmentId,
                        BillNumber = BillNumberStr,
                        LedgerMasterId = GetSourceIdFromOpdEmrID,
                        PatientId = model.PatientInformationModel.EmergencyMasterId,
                        PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.PatientInformationModel.EmergencyMasterId),
                        JVStatus = false,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                        CreatedDate = DateTime.Now,
                        Remarks = "NA",
                        Status = true,
                        PaidOnPaid = true,
                        Narration2 = item.Tim.ToString(),
                        ItemID = item.TestId
                    };
                    ent.CentralizedBillings.Add(objCentralizedBilling);
                }
                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;



                //update vouchernumber             
                //SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumber
                //                                    where x.DepartmentID == 1002 && x.FiscalYear == 1
                //                                    select x).First();
                //vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;



                ent.SaveChanges();
            }
        }

        public List<PatientTestModel> PopulatePatientTestList(int DepartmentID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                List<PatientTestModel> tempList = new List<PatientTestModel>();
                var data = (from pt in ent.PatientTests
                            join dm in ent.SetupDoctorMasters on pt.ReferDoctorID equals dm.DoctorID
                            join sd in ent.SetupDepartments on pt.DepartmentID equals sd.DeptID
                            join op in ent.OpdMasters on pt.PatientID equals op.OpdID

                            //where pt.DepartmentID == DepartmentID
                            select new PatientTestModel
                            {
                                PatientName = (op.FirstName ?? string.Empty) + " " + (op.MiddleName ?? " ") + " " + (op.LastName ?? string.Empty),
                                PatientTestID = pt.PatientTestID,
                                PatientID = pt.PatientID,
                                TestRegistrationDate = pt.TestRegistrationDate,
                                DoctorName = (dm.FirstName ?? string.Empty) + " " + (dm.MiddleName ?? " ") + " " + (dm.LastName ?? string.Empty),
                                DepartmentName = sd.DeptName,
                                TotalAmount = pt.TotalAmount,
                                Discount = pt.Discount,
                                PayableAmount = pt.PayableAmount
                            }).ToList();

                foreach (var item in data)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }

        public void Update(PatientTestModel model, int id, int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                DateTime dt = model.TestRegistrationDate.Date;
                // var objToSavePatientTest = ent.PatientTest.Where(x => x.PatientID == model.PatientInformationModel.EmergencyMasterId && x.TestRegistrationDate==model.TestRegistrationDate).FirstOrDefault();
                var objToSavePatientTest = ent.PatientTests.Where(x => x.PatientTestID == PatientTestID && x.TestRegistrationDate == model.TestRegistrationDate).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToSavePatientTest);
                ent.Entry(objToSavePatientTest).State = System.Data.EntityState.Modified;
                //needed TestDate in Where clause
                var dataTestToDelete = ent.PatientTestDetails.Where(x => x.PatientTestID == PatientTestID && x.TestDate == model.TestRegistrationDate).ToList();
                foreach (var item in dataTestToDelete)
                {
                    var objTestToDelete = ent.PatientTestDetails.Where(x => x.PatientTestID == PatientTestID && x.TestDate == model.TestRegistrationDate).FirstOrDefault();
                    ent.PatientTestDetails.Remove(objTestToDelete);
                    ent.SaveChanges();
                }
                //insert test..
                foreach (var item in model.TestCheckBoxListModelList)
                {
                    if (item.isSelected == true)
                    {
                        model.PatientTestDetailModel = new PatientTestDetailModel();
                        var objToSavePatientTestDetails = AutoMapper.Mapper.Map<PatientTestDetailModel, PatientTestDetail>(model.PatientTestDetailModel);
                        objToSavePatientTestDetails.PatientID = model.PatientInformationModel.EmergencyMasterId;
                        objToSavePatientTestDetails.PatientTestID = objToSavePatientTest.PatientTestID;
                        objToSavePatientTestDetails.DepartmentID = model.PatientInformationModel.DepartmentID;
                        objToSavePatientTestDetails.SectionID = item.SectionId;
                        objToSavePatientTestDetails.TestID = item.TestId;
                        objToSavePatientTestDetails.TestDate = (DateTime)item.DeliveryDate;
                        objToSavePatientTestDetails.TestTime = item.TestTime;
                        objToSavePatientTestDetails.DeliveryDate = item.DeliveryDate;
                        objToSavePatientTestDetails.Amount = (decimal)item.Price;
                        objToSavePatientTestDetails.Discount = 0;
                        objToSavePatientTestDetails.TotalAmount = (decimal)item.Price - 0;
                        objToSavePatientTestDetails.DeliveryStatus = false;
                        ent.PatientTestDetails.Add(objToSavePatientTestDetails);
                    }
                }

                ent.SaveChanges();
            }
        }
        public List<PathoOtherTestModelCB> GetOtherTestList()
        {
            EHMSEntities ent = new EHMSEntities();
            List<PathoOtherTestModelCB> PatientDetailList = new List<PathoOtherTestModelCB>();

            var data = ent.SetupOtherTests.ToList();
            foreach (var item in data)
            {
                PathoOtherTestModelCB obj = new PathoOtherTestModelCB();
                obj.OtherTestId = item.SetupOtherTestId;
                obj.OtherTestName = item.OtherTestName;
                obj.Narration1 = item.TestNameRemarks1;
                obj.Narration2 = item.TestNameRemarks2;
                obj.Amount = item.TotalAmount;
                PatientDetailList.Add(obj);
            }

            return PatientDetailList;

        }

        public List<TestCheckBoxListModel> getTestDetailTestIDWise(string TestID, string disIN, string PerAmount, string tim, string UserId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.Database.SqlQuery<TestCheckBoxListModel>("GetTestDetailTestIDWise '" + Convert.ToInt32(TestID) + "', 1006," + Convert.ToInt32(disIN) + "," + Convert.ToDecimal(PerAmount) + "," + Convert.ToInt32(tim) + "," + Convert.ToInt32(UserId) + "").ToList();
            }
        }

        public List<TestCheckBoxListModel> getPatientTestForPrint(int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<TestCheckBoxListModel>("GetTestDetailForPrint '" + Convert.ToInt32(PatientTestID) + "'").ToList();
            }
        }
        public List<PatientTestModel> getPatientTestPaymentForPrint(int PatientTestID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PatientTestModel>("GetTestPaymentForPrint '" + Convert.ToInt32(PatientTestID) + "'").ToList();
            }
        }
        public PatientTestModel getBillPaidTestModelList(string DeptName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PatientTestModel model = new PatientTestModel();
                model.BillPaidTestModelList = ent.Database.SqlQuery<BillPaidTestModel>("getBillCounterListOthers '" + DeptName + "'").ToList();
                return model;
            }

        }

        public PatientTestModel getBillPaidTestModelListById(string DeptName, int DeptId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PatientTestModel model = new PatientTestModel();
                model.BillPaidTestModelList = ent.Database.SqlQuery<BillPaidTestModel>("getBillCounterListOthers '" + DeptName + "', '" + DeptId + "'").ToList();
                return model;
            }

        }



        public PatientTestModel getDetailBillPaidTestModelList(string DeptName, string BillNo, int PatientID, int billNumberInt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                PatientTestModel model = new PatientTestModel();
                model.PatientInformationModel = GetPatientBasicInformationFromOpd(PatientID, 1000).FirstOrDefault();
                model.TestCheckBoxListModelList = ent.Database.SqlQuery<TestCheckBoxListModel>("getDetailBillTest '" + PatientID + "', '" + billNumberInt + "'").ToList();
                return model;
            }
        }

        public void InsertPatientFromDetail(PatientTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (model.ReferDoctorID == 1009)
                {
                    model.ReferDoctorID = 20;//Default doctor Id
                }
                var objInsertPatientTest = new PatientTest()
                {
                    //insert into patienttest
                    PatientID = model.PatientInformationModel.EmergencyMasterId,
                    DepartmentID = model.PatientInformationModel.DepartmentID,
                    ReferDoctorID = model.ReferDoctorID,
                    TestRegistrationDate = model.TestRegistrationDate,
                    Status = true
                };
                ent.PatientTests.Add(objInsertPatientTest);
                ent.SaveChanges();


                foreach (var item in model.TestCheckBoxListModelList)
                {
                    var objInsertPatientDetail = new PatientTestDetail()
                    {
                        //insert into patientdetail
                        PatientTestID = objInsertPatientTest.PatientTestID,
                        PatientID = model.PatientInformationModel.EmergencyMasterId,
                        DepartmentID = model.PatientInformationModel.DepartmentID,
                        SectionID = item.SectionId,
                        TestID = item.TestId,
                        TestDate = item.TestDate,
                        TestTime = item.TestTime,
                        DeliveryDate = item.DeliveryDate,
                        DeliveryStatus = false
                    };
                    ent.PatientTestDetails.Add(objInsertPatientDetail);
                    //ent.SaveChanges();

                    // update status in centralized billing
                    //var objtoUpdateCentralizedBilling = ent.CentralizedBilling.Where(x => x.BillNumber == item.BillNumber && x.ItemID == item.TestId && x.PatientId == model.PatientInformationModel.EmergencyMasterId);
                    //foreach (var item2 in objtoUpdateCentralizedBilling)
                    //{
                    //    item2.Status = false;
                    //}
                    int BillNumberInt = Convert.ToInt32(item.BillNo);
                    var objtoUpdateCentralizedBilling = ent.CentralizedBillingDetails.Where(x => x.BillNo == BillNumberInt && x.Status == true);
                    foreach (var item2 in objtoUpdateCentralizedBilling)
                    {
                        item2.Status = false;
                    }
                    ent.SaveChanges();

                }
            }
        }

        public List<PatientInformationModel> getPatientDetailByName(string PatientName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<PatientInformationModel>("GetPatientDetailsByName '" + PatientName + "%'").ToList();
            }
        }

        public List<SampleCollectedViewModel> GetSampleCollectedList(int LabNumber)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<SampleCollectedViewModel>("GetSampleCollectedList '" + LabNumber + "'").ToList();
            }
        }

        public List<GetCBCommissionViewModel> GetCommissionDetailsFromCB()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                DateTime Fromdate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                int Billno = 1;
                int DepartmentId = 1006;
                return ent.Database.SqlQuery<GetCBCommissionViewModel>("GetPathologyCommissionDetails '" + Fromdate + "','" + ToDate + "','" + Billno + "','" + DepartmentId + "'").ToList();
            }


        }

        public List<GetPathoCommDetailsViewModel> GetCommissionDetailsByBillNo(int BillNo)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                int Billno = BillNo;
                int DepartmentId = 1006;
                return ent.Database.SqlQuery<GetPathoCommDetailsViewModel>("GetPathoCommDetailsByBillNo '" + Billno + "','" + DepartmentId + "'").ToList();
            }



        }

        public bool InsertPathoCommissionDetails(PatientTestModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.AddMoreParticularsViewModelList)
                {
                    var Obj = new CommissionDetaislByType()
                    {
                        AccountHeadId = 1234,
                        Amount = item.CommissionAmout,
                        BillNo = model.ObjGetPathoCommDetailsViewModel.BillNo,
                        CommissionDate = DateTime.Now,
                        commissionDetailsId = 1006,
                        CommissionTypeId = 1,
                        CommissionTypeName = "Doctor Commission",
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        Status = true,
                        DoctorId = item.DoctorId,
                        CreatedDate = DateTime.Now,
                        Remarks = "fine",
                        JvStatus = false

                    };
                    ent.CommissionDetaislByTypes.Add(Obj);

                }
                ent.SaveChanges();

                //Update commissionDetails Table

                var ObjResult = ent.CommissionDetails.Where(x => x.DepartmentId != 1005 && x.BillNumber == model.ObjGetPathoCommDetailsViewModel.BillNo && x.Status == false);
                if (ObjResult.Count() > 0)
                {
                    foreach (var item in ObjResult)
                    {
                        item.Status = true;

                    }
                    ent.SaveChanges();
                }

            }



            return true;

        }
        public List<DoctorCommissionRptViewModel> GetDoctorCommissionReports()
        {
            PatientTestModel model = new PatientTestModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                //int DepartmentId = 1006;
                return ent.Database.SqlQuery<DoctorCommissionRptViewModel>("GetCommissionDoctorwiseReports ").ToList();

            }

        }


    }
}