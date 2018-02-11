using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class EmergencyMasterProvider
    {

        public List<EmergecyMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return new List<EmergencyTriageModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTriage>, IEnumerable<emer>>(ent.PreRegistration));

                List<EmergecyMasterModel> model = new List<EmergecyMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<EmergecyMasterModel>>(ent.EmergencyMasters).Where(m => m.Status == 1).ToList();

                foreach (var item in model)
                {
                    var data = ent.EmergencyTriages.Where(x => x.EmergencyMasterId == item.EmergencyMasterId).FirstOrDefault();

                    if (data != null)
                    {

                        EmergencyTriageModel m = new EmergencyTriageModel()
                        {

                            Time = data.Time,
                            DoctorId = (int)data.DoctorId,
                            MedicalOfficer = (int)data.MedicalOfficer,
                            MediumId = (int)data.MediumId,
                            sourceTypeId = (int)data.sourceTypeId,
                            SourceId = (int)data.SourceId,
                            TriageLevel = (int)data.TriageLevel
                        };
                        item.EmergencyTriageModel = m;
                    }
                }
                return model;
            }
        }
        public List<EmergecyMasterModel> GetDischargeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return new List<EmergencyTriageModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyTriage>, IEnumerable<emer>>(ent.PreRegistration));

                List<EmergecyMasterModel> model = new List<EmergecyMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<EmergecyMasterModel>>(ent.EmergencyMasters).Where(m => m.Status == 0).ToList();

                foreach (var item in model)
                {
                    var data = ent.EmergencyTriages.Where(x => x.EmergencyMasterId == item.EmergencyMasterId).FirstOrDefault();

                    if (data != null)
                    {

                        EmergencyTriageModel m = new EmergencyTriageModel()
                        {

                            Time = data.Time,
                            DoctorId = (int)data.DoctorId,
                            MedicalOfficer = (int)data.MedicalOfficer,
                            MediumId = (int)data.MediumId,
                            sourceTypeId = (int)data.sourceTypeId,
                            SourceId = (int)data.SourceId,
                            TriageLevel = (int)data.TriageLevel
                        };
                        item.EmergencyTriageModel = m;
                    }
                }
                return model;
            }
        }

        public List<OpdEmergencyViewModel> GetEmergencyList()
        {
            EHMSEntities ent = new EHMSEntities();
            OpdEmergencyViewModel obj = new OpdEmergencyViewModel();
            obj.OpdEmergencyViewModelList = new List<OpdEmergencyViewModel>();
            var query = ent.OpdMasters.Where(x => x.RegistrationSource == "Emer" && x.Status == true);

            foreach (var item in query)
            {
                OpdEmergencyViewModel objViewmodel = new OpdEmergencyViewModel();
                objViewmodel.dischargeCheck = obj.dischargeCheck;
                objViewmodel.EmergencyId = item.DepartmentPatientId;
                objViewmodel.PatientTitle = item.PatientTitle;
                objViewmodel.Firstname = item.FirstName;
                objViewmodel.MiddleName = item.MiddleName;
                objViewmodel.LastName = item.LastName;
                //objViewmodel.AgeYear = (int)item.AgeYear;
                objViewmodel.AgeYear = item.AgeYear;
                objViewmodel.Gender = item.Sex;
                objViewmodel.Address = item.Address;
                objViewmodel.MaritalStatus = item.MaritalStatus;
                objViewmodel.EmergencyId = item.DepartmentPatientId;
                obj.OpdEmergencyViewModelList.Add(objViewmodel);

            }
            return obj.OpdEmergencyViewModelList;
        }
        public List<OpdEmergencyViewModel> GetEmergencyListForCencus(DateTime date)
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdEmergencyViewModel> EMIDmodell = new List<OpdEmergencyViewModel>();
            var dataD = (from x in ent.OpdMasters
                         join y in ent.EmergencyMasters on x.DepartmentPatientId equals y.EmergencyMasterId
                         where y.CreatedDate == date
                         select new OpdEmergencyViewModel
                         {

                             Firstname = x.FirstName,
                             MiddleName = x.MiddleName,
                             LastName = x.LastName,
                             AgeYear = x.AgeYear,
                             Gender = x.Sex,
                             Address = x.Address,
                             TimeIn = y.TimeIn,
                             TimeOutD = y.TimeOut

                         }).ToList();
            foreach (var itemss in dataD)
            {
                EMIDmodell.Add(itemss);
            }
            return EMIDmodell;
        }

        public List<OpdEmergencyViewModel> GetEmergencyDischargeList()
        {
            EHMSEntities ent = new EHMSEntities();
            OpdEmergencyViewModel obj = new OpdEmergencyViewModel();
            obj.OpdEmergencyViewModelList = new List<OpdEmergencyViewModel>();
            var query = ent.OpdMasters.Where(x => x.RegistrationSource == "Emer" && x.Status == false);

            foreach (var item in query)
            {
                OpdEmergencyViewModel objViewmodel = new OpdEmergencyViewModel();
                objViewmodel.dischargeCheck = obj.dischargeCheck;
                objViewmodel.EmergencyId = item.DepartmentPatientId;
                objViewmodel.PatientTitle = item.PatientTitle;
                objViewmodel.Firstname = item.FirstName;
                objViewmodel.MiddleName = item.MiddleName;
                objViewmodel.LastName = item.LastName;
                objViewmodel.AgeYear = (int)item.AgeYear;
                objViewmodel.Gender = item.Sex;
                objViewmodel.Address = item.Address;
                objViewmodel.MaritalStatus = item.MaritalStatus;
                objViewmodel.EmergencyId = item.DepartmentPatientId;
                obj.OpdEmergencyViewModelList.Add(objViewmodel);

            }
            return obj.OpdEmergencyViewModelList;
        }

        public List<OpdEmergencyViewModel> GetlistBySearchWord(string Swords)
        {
            EHMSEntities ent = new EHMSEntities();
            string searchWords = Swords.ToLower();
            OpdEmergencyViewModel obj = new OpdEmergencyViewModel();
            obj.OpdEmergencyViewModelList = new List<OpdEmergencyViewModel>();
            var query = ent.OpdMasters.Where(x => x.FirstName.Contains(Swords));

            foreach (var item in query)
            {
                OpdEmergencyViewModel objViewmodel = new OpdEmergencyViewModel();
                objViewmodel.Firstname = item.FirstName;
                objViewmodel.MiddleName = item.MiddleName;
                objViewmodel.LastName = item.LastName;
                objViewmodel.AgeYear = (int)item.AgeYear;
                objViewmodel.Gender = item.Sex;
                objViewmodel.EmergencyId = item.OpdID;
                objViewmodel.Address = item.Address;
                obj.OpdEmergencyViewModelList.Add(objViewmodel);
            }
            return obj.OpdEmergencyViewModelList;



            //return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMaster)).Where(x => (x.FirstName + " " + (x.MiddleName + " " ?? string.Empty) + " " + x.LastName).ToLower().StartsWith(searchWords)).Take(20).ToList();
        }

        public int Insert(EmergecyMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                int maxemerid;
                var query = ent.EmergencyMasters.Where(m => m.EmergencyMasterId == ent.EmergencyMasters.Max(n => n.EmergencyMasterId)).SingleOrDefault();
                if (query == null)
                {
                    maxemerid = 1;
                }
                else
                {
                    maxemerid = query.EmergencyMasterId + 1;
                }

                var objToSaveOpd = new OpdMaster()
                {
                    PatientTitle = model.ObjOpdMaster.PatientTitle,
                    FirstName = model.ObjOpdMaster.FirstName,
                    MiddleName = model.ObjOpdMaster.MiddleName,
                    LastName = model.ObjOpdMaster.LastName,
                    Sex = model.ObjOpdMaster.Sex,
                    AgeYear = model.ObjOpdMaster.AgeYear,
                    MaritalStatus = model.ObjOpdMaster.MaritalStatus,
                    BloodGroup = model.ObjOpdMaster.BloodGroup,
                    RegistrationDate = DateTime.Now,
                    RegistrationSource = "Emer",
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    DepartmentId = Utility.GetCurrentUserDepartmentId(),
                    DepartmentPatientId = maxemerid,
                    CreatedDate = DateTime.Now,
                    RegistrationMode = "Visit",
                    Address = model.ObjOpdMaster.Address,
                    Status = true,
                    PaidOnPaid = true,
                    CountryID = model.ObjOpdMaster.CountryID

                };

                ent.OpdMasters.Add(objToSaveOpd);
                ent.SaveChanges();

                var objToSave = AutoMapper.Mapper.Map<EmergecyMasterModel, EmergencyMaster>(model);
                try
                {
                    objToSave.OpdMasterId = maxemerid;
                    objToSave.EmergencyNumber = maxemerid;
                    objToSave.SerialNumber = maxemerid;
                    objToSave.CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId();
                    objToSave.Status = 1;
                    objToSave.CreatedDate = DateTime.Today;
                    objToSave.OutcomeTypeId = 1;
                    objToSave.OpdMasterId = objToSaveOpd.OpdID;
                    ent.EmergencyMasters.Add(objToSave);
                    ent.SaveChanges();


                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }



                var objtosavedetails = AutoMapper.Mapper.Map<EmergencyTriageModel, EmergencyTriage>(model.EmergencyTriageModel);
                objtosavedetails.EmergencyMasterId = objToSave.EmergencyMasterId;
                objtosavedetails.sourceTypeId = 1;
                objtosavedetails.SourceId = 1;
                objtosavedetails.TriageLevel = 1;
                ent.EmergencyTriages.Add(objtosavedetails);
                //feedetails
                var objTosaveFeedetails = AutoMapper.Mapper.Map<EmergencyFeeDetailsModel, EmergencyFeeDetail>(model.EmergencyFeeDetailsModel);
                // decimal? aa = objTosaveFeedetails.DoctorFee;
                objTosaveFeedetails.EmergencyMasterId = objToSave.EmergencyMasterId;
                objTosaveFeedetails.FeeDate = objToSave.CreatedDate;
                objTosaveFeedetails.OtherDiscountPercentage = 0;
                objTosaveFeedetails.DoctorFee = 0;
                objTosaveFeedetails.RegistrationFee = model.EmergencyFeeDetailsModel.RegistrationFee;
                objTosaveFeedetails.TotalAmount = model.EmergencyFeeDetailsModel.RegistrationFee;
                ent.EmergencyFeeDetails.Add(objTosaveFeedetails);

                //Legder&Transaction
                string LedgerName = "A/C " + model.ObjOpdMaster.FirstName + " " + (model.ObjOpdMaster.MiddleName + " " ?? string.Empty) + model.ObjOpdMaster.LastName;


                var objtoInsertLedger = new GL_LedgerMaster()
                {
                    AccountGroupID = 1,
                    AccountSubGroupID = 1,
                    AccountTypeID = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    DepartmentID = 1001,
                    LedgerName = LedgerName,
                    SourceID = objToSaveOpd.OpdID,
                    LedgerSourceType = "Patient",
                    Status = 1
                };

                ent.GL_LedgerMaster.Add(objtoInsertLedger);
                ent.SaveChanges();


                var objToSavePatientLogMaster = new PatientLogMaster()
                {
                    PatientId = objToSaveOpd.OpdID,
                    RegistrationDate = Convert.ToDateTime(objToSave.CreatedDate),
                    DepartmentId = 1001,
                    Status = true

                };

                ent.PatientLogMasters.Add(objToSavePatientLogMaster);
                ent.SaveChanges();

                //Patient logmaster max id

                int? intPatientLogID = objToSavePatientLogMaster.OpdMasterLogId;

                //if (intPatientLogID == null)
                //{
                //    intPatientLogID = 1;
                //}
                //else
                //{
                //    intPatientLogID = intPatientLogID;

                //}

                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();

                //Insert into new table

                //Emergency Ticket
                var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 357,//Emergency Ticket
                    Amount = Convert.ToDecimal(404),
                    Status = true,
                    DepartmentId = 1001,
                    Times = 1

                };
                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                var ObjCentralizedBillingDetailstax = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 1261,//Emergency Ticket
                    Amount = Convert.ToDecimal(20.2),
                    Status = true,
                    DepartmentId = 1001,
                    Times = 1

                };

                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetailstax);


                var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                {
                    BillNo = BillNumberInt,
                    PaymentTypeID = 372,//Cash or bank from coa
                    PaymentSubTypeID = 0,
                    Amount = model.EmergencyFeeDetailsModel.RegistrationFee,
                    Status = true
                };
                ent.CentralizedBillingPaymentTypes.Add(ObjCentralizedBillingPaymentType);

                var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                {
                    BillNo = BillNumberInt,
                    BillDate = DateTime.Today,
                    TotalBillAmount = model.EmergencyFeeDetailsModel.RegistrationFee,
                    TotalDiscountID = 0,
                    TotalDiscountAmount = 0,
                    Narration1 = "Narraion",
                    Narration2 = "",
                    DepartmentName = "Emergency",
                    SubDepartmentId = 1,
                    PatientLogId = (int)intPatientLogID,
                    PatientId = objToSaveOpd.OpdID,
                    //JVNumber=1
                    JVStatus = false,
                    CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Emergency",
                    Status = true,
                    BranchId = 1,
                    IsHandover = false,
                    ReceiptId = 0,
                    ReturnedAmount = Convert.ToDecimal(0),
                    TenderAmount = Convert.ToDecimal(0)

                };
                ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);


                var objtoSaveCentralizedBilling = new CentralizedBilling()
                {
                    AccountHeadId = 19,
                    Amount = model.EmergencyFeeDetailsModel.RegistrationFee,
                    AmountDate = DateTime.Now,
                    PaymentType = "Cash",
                    Narration1 = "Fee Details",
                    Narration2 = "Emergecy Fee",
                    DepartmentName = "Emergency",
                    SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    BillNumber = BillNumberStr,
                    LedgerMasterId = objtoInsertLedger.LedgerMasterID,
                    PatientLogId = (int)intPatientLogID,
                    PatientId = objToSaveOpd.OpdID,
                    JVStatus = false,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Emergency",
                    PaidOnPaid = false,
                    Status = true
                };
                ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                var objToInsertTran = new GL_Transaction()
                {
                    Amount = model.EmergencyFeeDetailsModel.RegistrationFee,
                    DepartmentID = 1001,
                    Dr_Cr = "Cr",
                    LedgerMasterID = objtoInsertLedger.LedgerMasterID,
                    Narration1 = "Emergency Fee",
                    RefNo = maxemerid,
                    TransactionDate = DateTime.Now,
                    TransactionTypeID = 1,
                    FeeTypeId = 19,//emergency fee
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,

                };

                ent.GL_Transaction.Add(objToInsertTran);


                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;


                //update vouchernumber             
                //SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumber
                //                                    where x.DepartmentID == 1 && x.FiscalYear == 1
                //                                    select x).First();
                //vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;

                i = ent.SaveChanges();

            }

            return i;

        }

        public int Update(EmergecyMasterModel model)
        {
            int i = 0;
            OpdMaster opdmo = new OpdMaster();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objToEdit = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == model.EmergencyMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objToEdit);
                objToEdit.CreatedBy = (int)HospitalManagementSystem.Utility.GetCurrentLoginUserId();

                objToEdit.Status = 1;
                objToEdit.CreatedDate = DateTime.Today;
                objToEdit.OutcomeTypeId = 1;
                ent.Entry(objToEdit).State = System.Data.EntityState.Modified;

                var objToEditObjOpd = ent.OpdMasters.Where(x => x.DepartmentPatientId == model.EmergencyMasterId).FirstOrDefault();
                AutoMapper.Mapper.Map(opdmo, objToEditObjOpd);

                objToEditObjOpd.PatientTitle = model.ObjOpdMaster.PatientTitle;
                objToEditObjOpd.FirstName = model.OpdEmergencyViewModel.Firstname;
                objToEditObjOpd.MiddleName = model.OpdEmergencyViewModel.MiddleName;
                objToEditObjOpd.LastName = model.OpdEmergencyViewModel.LastName;
                objToEditObjOpd.AgeYear = model.OpdEmergencyViewModel.AgeYear;
                objToEditObjOpd.Sex = model.OpdEmergencyViewModel.Gender;
                objToEditObjOpd.MaritalStatus = model.ObjOpdMaster.MaritalStatus;
                objToEditObjOpd.BloodGroup = model.ObjOpdMaster.BloodGroup;
                objToEditObjOpd.Address = model.OpdEmergencyViewModel.Address;
                ent.Entry(objToEditObjOpd).State = System.Data.EntityState.Modified;

                List<EmergencyTriageModel> modell = new List<EmergencyTriageModel>();

                modell = AutoMapper.Mapper.Map<IEnumerable<EmergencyTriage>, IEnumerable<EmergencyTriageModel>>(ent.EmergencyTriages).ToList();
                var trid = modell.Where(m => m.EmergencyMasterId == model.EmergencyMasterId).Select(m => m.EmergencyTriageId).SingleOrDefault();

                var a = objToEdit.EmergencyTriages.Select(m => m.EmergencyMasterId).SingleOrDefault();

                int PreResistratrionPre = Convert.ToInt32(trid);
                int b = Convert.ToInt32(a);
                model.EmergencyTriageModel.EmergencyTriageId = PreResistratrionPre;
                model.EmergencyTriageModel.EmergencyMasterId = b;
                var objToEditDetails = ent.EmergencyTriages.Where(x => x.EmergencyTriageId == model.EmergencyTriageModel.EmergencyTriageId).FirstOrDefault();

                objToEditDetails.sourceTypeId = 1;
                objToEditDetails.SourceId = 1;
                objToEditDetails.TriageLevel = 1;
                AutoMapper.Mapper.Map(model.EmergencyTriageModel, objToEditDetails);
                objToEditDetails.sourceTypeId = 1;
                objToEditDetails.SourceId = 1;
                objToEditDetails.TriageLevel = 1;
                objToEditDetails.DoctorId = model.EmergencyTriageModel.DoctorId;
                objToEditDetails.MedicalOfficer = model.EmergencyTriageModel.MedicalOfficer;
                ent.Entry(objToEditDetails).State = System.Data.EntityState.Modified;

                var objtoeditPatientFee = ent.EmergencyFeeDetails.Where(x => x.EmergencyMasterId == model.EmergencyMasterId).FirstOrDefault();

                objtoeditPatientFee.RegistrationFee = model.EmergencyFeeDetailsModel.RegistrationFee;
                objtoeditPatientFee.DoctorFee = 0;
                objtoeditPatientFee.MemberDiscountAmount = 0;
                objtoeditPatientFee.OtherDiscountPercentage = 0;
                objtoeditPatientFee.TotalAmount = model.EmergencyFeeDetailsModel.RegistrationFee;
                objtoeditPatientFee.Remarks = model.EmergencyFeeDetailsModel.Remarks;

                i = ent.SaveChanges();
            }
            return i;

        }
        public void Delete(int EmergencyId)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToDelete = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == EmergencyId).FirstOrDefault();
                ent.EmergencyMasters.Remove(objToDelete);
                var obj = ent.EmergencyTriages.Where(y => y.EmergencyMasterId == EmergencyId).FirstOrDefault();
                ent.EmergencyTriages.Remove(obj);
                ent.SaveChanges();
            }
        }
        public List<OpdModel> SearchEmergencyMaster(int EmergencyMasterid, string name)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                name = name.Trim();
                if (EmergencyMasterid == 0 && name == "")
                {
                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters)).Take(20).ToList();
                }
                else if (EmergencyMasterid == 0 && name != "")
                {
                    var query = from Emergency in ent.OpdMasters
                                let fullName = Emergency.FirstName.ToLower() + " " + (Emergency.MiddleName.ToLower() + " " ?? string.Empty) + Emergency.LastName.ToLower()
                                where fullName.Contains(name.ToLower())
                                select Emergency;
                    //FullName=o.FirstName+" "+(o.MiddleName+" "?? string.Empty)+o.LastName,

                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
                else if (EmergencyMasterid != 0 && name == "")
                {
                    var query = from Emergency in ent.OpdMasters
                                    //let fullName = opd.FirstName.ToLower() + " " + opd.MiddleName.ToLower() + " " + opd.LastName.ToLower()
                                where Emergency.OpdID == EmergencyMasterid
                                select Emergency;

                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
                else
                {
                    var query = from Emergency in ent.OpdMasters
                                let fullName = Emergency.FirstName.ToLower() + " " + (Emergency.MiddleName.ToLower() + " " ?? string.Empty) + Emergency.LastName.ToLower()
                                where Emergency.OpdID == EmergencyMasterid && fullName.Contains(name.ToLower())
                                select Emergency;

                    //var address=from opd in ent.OpdMaster 


                    return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
                }
            }

        }
        public List<EmergencyFeeDetailsModel> GetEmergencyPatientFeeDetailsList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<EmergencyFeeDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<EmergencyFeeDetail>, IEnumerable<EmergencyFeeDetailsModel>>(ent.EmergencyFeeDetails.Where(x => x.EmergencyMasterId == id)));
            }
        }

        public OpdEmergencyViewModel GetEmegrencyDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            OpdEmergencyViewModel model = new OpdEmergencyViewModel();

            var objEmergencyMaster = ent.OpdMasters.Where(x => x.DepartmentPatientId == id && x.RegistrationSource == "Emer").SingleOrDefault();

            OpdEmergencyViewModel objViewmodel = new OpdEmergencyViewModel();
            objViewmodel.PatientId = objEmergencyMaster.OpdID;
            objViewmodel.Firstname = objEmergencyMaster.FirstName;
            objViewmodel.MiddleName = objEmergencyMaster.MiddleName;
            objViewmodel.LastName = objEmergencyMaster.LastName;
            objViewmodel.AgeYear = (int)objEmergencyMaster.AgeYear;
            objViewmodel.Gender = objEmergencyMaster.Sex;
            objViewmodel.EmergencyId = (int)objEmergencyMaster.DepartmentPatientId;
            objViewmodel.Address = objEmergencyMaster.Address;
            objViewmodel.CreatedBy = (int)objEmergencyMaster.CreatedBy;
            objViewmodel.RegistrationDate = (DateTime)objEmergencyMaster.RegistrationDate;

            return objViewmodel;
        }


        public EmergecyMasterModel GetEmergencyDetailsForDischargeReport(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var obj = (from e in ent.EmergencyMasters
                       where e.EmergencyMasterId == id
                       select e).SingleOrDefault();
            EmergecyMasterModel model = AutoMapper.Mapper.Map<EmergencyMaster, EmergecyMasterModel>(obj);

            //var objTriage = (from t in ent.EmergencyTriage
            //                 where t.EmergencyMasterId == id
            //                 select t).SingleOrDefault();
            //model.EmergencyTriageModel = AutoMapper.Mapper.Map<EmergencyTriage, EmergencyTriageModel>(objTriage);

            //var objViatals = (from v in ent.Vitals where v.EmergencyMasterId == id select v).SingleOrDefault();
            //model.VitalsModel = AutoMapper.Mapper.Map<Vitals, VitalsModel>(objViatals);


            //IEnumerable<EmergencyTreatmentAllergies> objects = (from v in (IEnumerable<EmergencyTreatmentAllergies>)ent.EmergencyTreatmentAllergies
            //                                                    where v.EmergencyMasterId == id
            //                                                    select v);

            //List<EmergencyTreatmentAllergiesModel> ETmodel = new List<EmergencyTreatmentAllergiesModel>();
            //ETmodel = AutoMapper.Mapper.Map<IEnumerable<EmergencyTreatmentAllergies>, IEnumerable<EmergencyTreatmentAllergiesModel>>(ent.EmergencyTreatmentAllergies).Where(m => m.EmergencyMasterId == id).ToList();
            //model.ListEmergencyTreatmentAllergiesModel = ETmodel;







            //var objEmergencyTreatementAllergies = (from et in ent.EmergencyTreatmentAllergies where et.EmergencyMasterId == id select et).SingleOrDefault();
            //model.EmergencyTreatmentAllergiesModel = AutoMapper.Mapper.Map<EmergencyTreatmentAllergies, EmergencyTreatmentAllergiesModel>(objEmergencyTreatementAllergies);

            List<EmergencyTstCaridModel> ETCmodel = new List<EmergencyTstCaridModel>();
            ETCmodel = AutoMapper.Mapper.Map<IEnumerable<EmergencyTstCarid>, IEnumerable<EmergencyTstCaridModel>>(ent.EmergencyTstCarids).Where(m => m.EmergencyMasterId == id).ToList();
            model.ListEmergencyTstCaridModel = ETCmodel;





            List<EmergencyInvestigationDetailModel> EMIDmodel = new List<EmergencyInvestigationDetailModel>();
            EMIDmodel = AutoMapper.Mapper.Map<IEnumerable<EmergencyInvestigationDetail>, IEnumerable<EmergencyInvestigationDetailModel>>(ent.EmergencyInvestigationDetails).Where(m => m.EmergencyMasterId == id).ToList();
            model.ListEmergencyInvestigationDetailModel = EMIDmodel;

            return model;
        }
        public void UpdateStatus(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjEmergencyPatientDetail = ent.EmergencyMasters.Where(m => m.EmergencyMasterId == id).FirstOrDefault();
                var OpdED = ent.OpdMasters.Where(n => n.DepartmentPatientId == id && n.RegistrationSource == "Emer" && n.Status == true).FirstOrDefault();
                ObjEmergencyPatientDetail.Status = 0;
                OpdED.Status = false;

                ObjEmergencyPatientDetail.DischargeDate = DateTime.Today;
                ent.Entry(ObjEmergencyPatientDetail).State = EntityState.Modified;
                ent.SaveChanges();
            }
        }

        public List<OpdEmergencyViewModel> GetEmergencylistByRegistrationDate(DateTime? startdate, DateTime? enddate)
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdEmergencyViewModel> EMIDmodel = new List<OpdEmergencyViewModel>();
            var data = (from x in ent.OpdMasters
                        join y in ent.EmergencyMasters on x.DepartmentPatientId equals y.EmergencyMasterId
                        where y.OutcomeTypeId == 1 && x.RegistrationDate >= startdate && x.RegistrationDate <= enddate
                        select new OpdEmergencyViewModel
                        {
                            PatientTitle = x.PatientTitle,
                            Firstname = x.FirstName,
                            MiddleName = x.MiddleName,
                            LastName = x.LastName,
                            AgeYear = x.AgeYear,
                            Gender = x.Sex,
                            TimeIn = y.TimeIn,
                            EmergencyId = y.EmergencyNumber

                        }).ToList();
            foreach (var items in data)
            {
                EMIDmodel.Add(items);
            }

            return EMIDmodel;
        }
        public List<OpdEmergencyViewDischargeModel> GetEmergencylistByRegistrationDateDischarge(DateTime? startdate, DateTime? enddate)
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdEmergencyViewDischargeModel> EMIDmodell = new List<OpdEmergencyViewDischargeModel>();
            var dataD = (from x in ent.OpdMasters
                         join y in ent.EmergencyMasters on x.DepartmentPatientId equals y.EmergencyMasterId
                         //where y.OutcomeTypeId == 6 && x.DischargeDate >= startdate && y.DischargeDate <= enddate
                         where y.OutcomeTypeId == 6 && x.RegistrationDate >= startdate && x.RegistrationDate <= enddate
                         select new OpdEmergencyViewDischargeModel
                         {
                             PatientTitle = x.PatientTitle,
                             Firstname = x.FirstName,
                             MiddleName = x.MiddleName,
                             LastName = x.LastName,
                             AgeYear = x.AgeYear,
                             Gender = x.Sex,
                             EmergencyId = y.EmergencyNumber,

                             TimeOut = y.TimeOut

                         }).ToList();
            foreach (var itemss in dataD)
            {
                EMIDmodell.Add(itemss);
            }
            return EMIDmodell;
        }
    }
}

