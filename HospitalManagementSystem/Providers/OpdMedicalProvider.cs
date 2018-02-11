using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Controllers;

namespace HospitalManagementSystem.Providers
{
    public class OpdMedicalProvider
    {
        public int Insert(OpdModel model)
        {
            int i = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {
                var objTosaveOpdMaster = AutoMapper.Mapper.Map<OpdModel, OpdMaster>(model);
                objTosaveOpdMaster.CreatedBy = Utility.GetCurrentLoginUserId();
                objTosaveOpdMaster.CreatedDate = DateTime.Now;
                objTosaveOpdMaster.Status = true;
                objTosaveOpdMaster.DepartmentId = 4;
                objTosaveOpdMaster.DepartmentPatientId = null;
                objTosaveOpdMaster.RegistrationDate = DateTime.Now;
                objTosaveOpdMaster.RegistrationMode = "Visit";
                objTosaveOpdMaster.RegistrationSource = "Opd";
                ent.OpdMasters.Add(objTosaveOpdMaster);
                //OpdpatientDoctorDetails

                foreach (var item in model.OpdDoctorList)
                {
                    model.OpdPatientDoctorDetailsModel = new OpdPatientDoctorDetailsModel();
                    var objTosaveOpdDoctorList = AutoMapper.Mapper.Map<OpdPatientDoctorDetailsModel, OpdPatientDoctorDetail>(model.OpdPatientDoctorDetailsModel);
                    objTosaveOpdDoctorList.DoctorID = item.DoctorID;
                    objTosaveOpdDoctorList.DepartmentID = item.DepartmentID;
                    objTosaveOpdDoctorList.OpdID = objTosaveOpdMaster.OpdID;
                    objTosaveOpdDoctorList.PreferDate = item.PreferDate;
                    objTosaveOpdDoctorList.PreferTime = item.PreferTime;
                    objTosaveOpdDoctorList.RegistrationDate = objTosaveOpdMaster.RegistrationDate;
                    ent.OpdPatientDoctorDetails.Add(objTosaveOpdDoctorList);
                }
                //opdfeedetails
                //var objTosaveFeedetails = AutoMapper.Mapper.Map<OpdFeeDetailsModel, OpdFeeDetails>(model.OpdFeeDetailsModel);
                // decimal? aa = objTosaveFeedetails.DoctorFee;
                //objTosaveFeedetails.OpdID = objTosaveOpdMaster.OpdID;
                //objTosaveFeedetails.FeeDate = objTosaveOpdMaster.RegistrationDate;
                //objTosaveFeedetails.OtherDiscountPercentage = 0;
                //ent.OpdFeeDetails.Add(objTosaveFeedetails);

                //Legder&Transaction
                string LedgerName = "A/C " + model.FirstName + " " + (model.MiddleName + " " ?? string.Empty) + model.LastName;

                //Opd master max id
                int? intIdt = ent.OpdMasters.Max(u => (int?)u.OpdID);
                if (intIdt == null)
                {
                    intIdt = 1;
                }
                else
                {
                    intIdt = intIdt + 1;

                }

                var objToSavePatientLogMaster = new PatientLogMaster()
                {
                    //PatientId = ent.OpdMaster.Max(a => a.OpdID+1),
                    PatientId = intIdt.GetValueOrDefault() + 1,

                    RegistrationDate = Convert.ToDateTime(objTosaveOpdMaster.RegistrationDate),
                    DepartmentId = 13,//medical report
                    Status = true

                };

                ent.PatientLogMasters.Add(objToSavePatientLogMaster);



                //Patient logmaster max id
                int? intPatientLogID = ent.PatientLogMasters.Max(u => (int?)u.OpdMasterLogId);
                if (intPatientLogID == null)
                {
                    intPatientLogID = 1;
                }
                else
                {
                    intPatientLogID = intPatientLogID + 1;

                }


                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Opd", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();

                var objtoInsertLedger = new GL_LedgerMaster()
                {
                    AccountGroupID = 1,
                    AccountSubGroupID = 1,
                    AccountTypeID = 1,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    DepartmentID = 13,//medical report
                    LedgerName = LedgerName,
                    //SourceID = ent.OpdMaster.Max(a => a.OpdID+1),
                    SourceID = intIdt,
                    LedgerSourceType = "Patient",
                    Status = 1
                };

                ent.GL_LedgerMaster.Add(objtoInsertLedger);


                //LedgerMasterId Max Id
                int? LedgetMasterId = ent.GL_LedgerMaster.Max(u => (int?)u.LedgerMasterID);
                if (LedgetMasterId == null)
                {
                    LedgetMasterId = 1;
                }
                else
                {
                    LedgetMasterId = LedgetMasterId + 1;

                }

                var objToInsertTran = new GL_Transaction()
                {
                    Amount = model.OpdMedicalDetailModel.Amount,
                    DepartmentID = 13,//medical report
                    Dr_Cr = "Cr",
                    LedgerMasterID = objtoInsertLedger.LedgerMasterID,
                    Narration1 = "Medical Charge",
                    //RefNo = ent.OpdMaster.Max(a => a.OpdID+1),
                    RefNo = intIdt,
                    TransactionDate = DateTime.Now,
                    TransactionTypeID = 1,

                    FeeTypeId = 17,
                    PatientLogId = (int)intPatientLogID,
                    CreatedDate = DateTime.Now,
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    //VoucherNo = Utility.getMaxVoucherNumber(1, 1)
                };

                ent.GL_Transaction.Add(objToInsertTran);


                var OpdMedicalDetailInsert = new OpdMedicalDetail()
                {
                    OpdMasterId = objTosaveOpdMaster.OpdID,
                    ManPowerId = model.OpdMedicalDetailModel.ManPowerId,
                    AgentId = model.OpdMedicalDetailModel.AgentId,
                    PreHolo = model.OpdMedicalDetailModel.PreHolo,
                    Amount = model.OpdMedicalDetailModel.Amount,
                    Discount = model.OpdMedicalDetailModel.Discount,
                    Commission = model.OpdMedicalDetailModel.Commission,
                    CreatedDate = objTosaveOpdMaster.RegistrationDate,
                    CreatedBy = objTosaveOpdMaster.CreatedBy,
                    status = 1
                };
                ent.OpdMedicalDetails.Add(OpdMedicalDetailInsert);

                var objtoSaveCentralizedBilling = new CentralizedBilling()
                {
                    AccountHeadId = 17,
                    Amount = model.OpdMedicalDetailModel.Amount,
                    AmountDate = DateTime.Now,
                    PaymentType = "Cash",
                    Narration1 = "Fee Details",
                    DepartmentName = "Opd",
                    SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    BillNumber = BillNumberStr,
                    LedgerMasterId = (int)LedgetMasterId,
                    PatientLogId = (int)intPatientLogID,
                    PatientId = (int)intIdt,
                    JVStatus = false,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                    CreatedDate = DateTime.Now,
                    Remarks = "Opd Medical Records",
                    PaidOnPaid = false,
                    Status = true
                };
                ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);




                //update Bill Number
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Opd" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;



                //update vouchernumber             
                //SetupVoucherNumber vouchernumber = (from x in ent.SetupVoucherNumber
                //                                    where x.DepartmentID == 1 && x.FiscalYear == 1
                //                                    select x).First();
                //vouchernumber.VoucherNo = vouchernumber.VoucherNo + 1;
                i = ent.SaveChanges();
                return i;
            }

        }
        public List<OpdModel> Getlist()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                List<OpdModel> model = new List<OpdModel>();
                var getopdid = (from b in ent.OpdMedicalDetails
                                join l in ent.OpdMasters on b.OpdMasterId equals l.OpdID
                                select b).ToList();
                foreach (var item in getopdid)
                {
                    OpdModel obj = new OpdModel();
                    obj = AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters).Where(x => x.OpdID == item.OpdMasterId).SingleOrDefault();
                    model.Add(obj);
                }
                foreach (var item1 in model)
                {
                    var data = ent.OpdMedicalDetails.Where(x => x.OpdMasterId == item1.OpdID).FirstOrDefault();

                    if (data != null)
                    {

                        OpdMedicalDetailModel m = new OpdMedicalDetailModel()
                        {

                            OpdMasterId = data.OpdMasterId,
                            OpdMedicalDetailId = data.OpdMedicalDetailId,
                            ManPowerId = data.ManPowerId,
                            AgentId = data.AgentId,
                            PreHolo = data.PreHolo,
                            Amount = data.Amount,
                            Commission = data.Commission,
                            CreatedDate = data.CreatedDate,
                            CreatedBy = data.CreatedBy,
                            status = data.status

                        };
                        item1.OpdMedicalDetailModel = m;
                    }
                }



                return model;
            }






        }
        public List<OpdDoctorListModel> GetPatientDoctorList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OpdDoctorListModel>(AutoMapper.Mapper.Map<IEnumerable<OpdPatientDoctorDetail>, IEnumerable<OpdDoctorListModel>>(ent.OpdPatientDoctorDetails.Where(x => x.OpdID == id)));
            }
        }
        public int Update(OpdModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                //opdMaster
                var objtoeditOpdMaster = ent.OpdMasters.Where(x => x.OpdID == model.OpdID).FirstOrDefault();
                var objtoeditOpdMedicalDetail = ent.OpdMedicalDetails.Where(x => x.OpdMasterId == model.OpdID && x.CreatedDate == model.RegistrationDate).FirstOrDefault();
                //var objtoeditPatientFee = ent.OpdFeeDetails.Where(x => x.OpdID == model.OpdID).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoeditOpdMaster);
                ent.Entry(objtoeditOpdMaster).State = System.Data.EntityState.Modified;
                objtoeditOpdMedicalDetail.AgentId = model.OpdMedicalDetailModel.AgentId;
                objtoeditOpdMedicalDetail.ManPowerId = model.OpdMedicalDetailModel.ManPowerId;
                objtoeditOpdMedicalDetail.CreatedDate = objtoeditOpdMaster.RegistrationDate;
                objtoeditOpdMedicalDetail.CreatedBy = Utility.GetCurrentLoginUserId();
                objtoeditOpdMedicalDetail.status = 1;
                objtoeditOpdMedicalDetail.Amount = model.OpdMedicalDetailModel.Amount;
                objtoeditOpdMedicalDetail.Commission = model.OpdMedicalDetailModel.Commission;
                var dataDoctorToDelete = ent.OpdPatientDoctorDetails.Where(x => x.OpdID == model.OpdID && x.RegistrationDate == model.RegistrationDate).ToList();
                foreach (var item in dataDoctorToDelete)
                {
                    var objDoctorToDelete = ent.OpdPatientDoctorDetails.Where(y => y.OpdID == item.OpdID && y.RegistrationDate == item.RegistrationDate).FirstOrDefault();
                    ent.OpdPatientDoctorDetails.Remove(objDoctorToDelete);
                    ent.SaveChanges();
                }

                foreach (var item in model.OpdDoctorList)
                {
                    model.OpdPatientDoctorDetailsModel = new OpdPatientDoctorDetailsModel();
                    var objTosaveOpdDoctorList = AutoMapper.Mapper.Map<OpdPatientDoctorDetailsModel, OpdPatientDoctorDetail>(model.OpdPatientDoctorDetailsModel);
                    objTosaveOpdDoctorList.DoctorID = item.DoctorID;
                    objTosaveOpdDoctorList.DepartmentID = item.DepartmentID;
                    objTosaveOpdDoctorList.OpdID = objtoeditOpdMaster.OpdID;
                    objTosaveOpdDoctorList.PreferDate = item.PreferDate;
                    objTosaveOpdDoctorList.PreferTime = item.PreferTime;
                    objTosaveOpdDoctorList.RegistrationDate = objtoeditOpdMaster.RegistrationDate;
                    ent.OpdPatientDoctorDetails.Add(objTosaveOpdDoctorList);
                }
                i = ent.SaveChanges();
                return i;
            }
        }
        public OpdModel GetOpdDetails(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            OpdModel model = new OpdModel();
            model.PatientDoctorList = new List<OpdPatientDoctorDetailsModel>();
            model.OpdMedicalDetailList = new List<OpdMedicalDetailModel>();
            var objOpdMaster = ent.OpdMasters.Where(x => x.OpdID == id).SingleOrDefault();
            AutoMapper.Mapper.Map(objOpdMaster, model);
            var objOpdDoctorDetails = ent.OpdPatientDoctorDetails.OrderByDescending(x => x.PatientDoctorDetailID).Where(x => x.OpdID == id).ToList();
            AutoMapper.Mapper.Map(objOpdDoctorDetails, model.PatientDoctorList);
            var objOpdMedicalDetails = ent.OpdMedicalDetails.Where(x => x.OpdMasterId == id).ToList();
            AutoMapper.Mapper.Map(objOpdMedicalDetails, model.OpdMedicalDetailList);
            return model;
        }
        public List<OpdMedicalDetailModel> GetAgentAndtotalAmount()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                List<OpdMedicalDetailModel> model = new List<OpdMedicalDetailModel>();
                var getopdid = (from b in ent.OpdMedicalDetails

                                select b.AgentId).Distinct().ToList();
                foreach (var item in getopdid)
                {
                    OpdMedicalDetailModel obj = new OpdMedicalDetailModel();
                    obj.AgentId = AutoMapper.Mapper.Map<IEnumerable<OpdMedicalDetail>, IEnumerable<OpdMedicalDetailModel>>(ent.OpdMedicalDetails).Where(x => x.AgentId == item).Select(x => x.AgentId).FirstOrDefault();
                    obj.Amount = AutoMapper.Mapper.Map<IEnumerable<OpdMedicalDetail>, IEnumerable<OpdMedicalDetailModel>>(ent.OpdMedicalDetails).Where(x => x.AgentId == item).Sum(x => x.Commission);
                    model.Add(obj);


                }
                return model;

            }


        }
        public List<OpdMedicalDetailModel> GetAgentDetail(int id)
        {
            List<OpdMedicalDetailModel> model = new List<OpdMedicalDetailModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {

                model = (AutoMapper.Mapper.Map<IEnumerable<OpdMedicalDetail>, IEnumerable<OpdMedicalDetailModel>>(ent.OpdMedicalDetails.Where(x => x.AgentId == id))).ToList();


            }
            return model;

        }


    }
}

