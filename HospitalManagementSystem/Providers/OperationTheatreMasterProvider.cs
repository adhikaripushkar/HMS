using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
namespace HospitalManagementSystem.Providers
{
    public class OperationTheatreMasterProvider
    {
        public List<OperationTheatreMasterModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OperationTheatreMasterModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreMaster>, IEnumerable<OperationTheatreMasterModel>>(ent.OperationTheatreMasters.Where(x => x.Status == true)));

            }
        }
        public List<OperationTheatreMasterModel> GetListdatewise(DateTime Sdate, DateTime Edate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OperationTheatreMasterModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreMaster>, IEnumerable<OperationTheatreMasterModel>>(ent.OperationTheatreMasters.Where(x => x.Status == true && x.CreatedDate >= Sdate && x.CreatedDate <= Edate)));

            }
        }
        public List<OperationTheatreMasterModel> GetListdateandidwise(DateTime Sdate, DateTime Edate, int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OperationTheatreMasterModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreMaster>, IEnumerable<OperationTheatreMasterModel>>(ent.OperationTheatreMasters.Where(x => x.Status == true && x.CreatedDate >= Sdate && x.CreatedDate <= Edate && x.SourceID == id)));

            }
        }

        public int Insert(OperationTheatreMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                if (ent.OperationTheatreMasters.Where(x => x.Status == true && x.OperationDate == model.OperationDate && (
                    (x.OperationStartTime <= model.OperationStartTime && x.OperationEndTime >= model.OperationStartTime) ||
                     (x.OperationStartTime <= model.OperationEndTime && x.OperationEndTime >= model.OperationEndTime)
                    ) && x.OperationRoomID == model.OperationRoomID).Any())
                {
                    return i;
                }


                var objToSave = AutoMapper.Mapper.Map<OperationTheatreMasterModel, OperationTheatreMaster>(model);
                objToSave.Status = true;
                //objToSave.SourceID = "1";
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                //objToSave.ValidUpto = Convert.ToDateTime(model.date);
                if (model.Discount != null) objToSave.TotalCharge = model.Charge - model.Discount;
                else objToSave.TotalCharge = model.Charge;
                ent.OperationTheatreMasters.Add(objToSave);
                ent.SaveChanges();
                int k = objToSave.OperationTheatreMasterID;

                foreach (var item in model.PersonAllocatedList)
                {
                    var objTheatreDetails = new OperationTheatreDetail();
                    objTheatreDetails.OperationTheatreMasterID = objToSave.OperationTheatreMasterID;
                    objTheatreDetails.PersonAllocatedID = item.PersonAllocateId;
                    objTheatreDetails.PersonAllocatedTypeID = item.PersonAllocatedTypeId;
                    ent.OperationTheatreDetails.Add(objTheatreDetails);
                    i = ent.SaveChanges();
                }

                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();


                //calcuate charge amount and tax
                decimal ChargeFeeAndTaxTotal = (decimal)model.Charge;
                decimal ChargeOnly = (ChargeFeeAndTaxTotal * 100) / 105;
                decimal ChargeFeeTaxOnly = ChargeFeeAndTaxTotal - ChargeOnly;
                decimal ChargeAmountAfterDiscount = Convert.ToDecimal(0);


                //Calculate Discount

                //Check If discount or not
                decimal TotalDiscountAmount = Convert.ToDecimal(0);
                int TotalDiscountHeadId = 0;
                //Discount Calculation
                if (model.Discount > 0)
                {
                    TotalDiscountAmount = (decimal)model.Discount;
                    TotalDiscountHeadId = 369;
                    ChargeAmountAfterDiscount = ChargeFeeAndTaxTotal - TotalDiscountAmount;

                }
                //fee amount
                var ObjFeeDetails = new CentralizedBillingDetail()
                {

                };
                //tax amount
                var ObjTaxDetails = new CentralizedBillingDetail()
                {

                };

                //discount amount
                var ObjDiscountDtls = new CentralizedBillingDetail()
                {


                };



                //Payment Type only Deposit
                var ObjPayment = new CentralizedBillingPaymentType()
                {

                };

                //master table
                var ObjMaster = new CentralizedBillingMaster()
                {

                };

                //Insert into centralizedbillingdetails
                //var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                //{
                //    BillNo = BillNumberInt,
                //    AccountHeadID = 111,//Ot Charge id from coa
                //    Amount = ChargeOnly,
                //    Status = true,
                //    Times = 1

                //};
                //ent.CentralizedBillingDetail.Add(ObjCentralizedBillingDetails);

                //ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                //{
                //    BillNo = BillNumberInt,
                //    AccountHeadID = 112,//Ot Charge tax from coa
                //    Amount = ChargeFeeTaxOnly,
                //    Status = true,
                //    DepartmentId = 1005,
                //    Times = 1

                //};
                //ent.CentralizedBillingDetail.Add(ObjCentralizedBillingDetails);


                //var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                //{
                //    BillNo = BillNumberInt,
                //    PaymentTypeID = 1,//Cash or bank from coa
                //    PaymentSubTypeID = 1,
                //    Amount = ChargeAmountAfterDiscount,
                //    Status = true
                //};
                //ent.CentralizedBillingPaymentType.Add(ObjCentralizedBillingPaymentType);

                //Add Details in centralized billing master
                //var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                //{
                //    BillNo = BillNumberInt,
                //    TotalBillAmount = ChargeFeeAndTaxTotal,
                //    Narration1 = "Narration",
                //    Narration2 = "",
                //    DepartmentName = "Ot",
                //    SubDepartmentId = 1,
                //    PatientLogId = (int)objToSave.SourceID,
                //    PatientId = (int)objToSave.SourceID,
                //    TotalDiscountID = TotalDiscountHeadId,
                //    TotalDiscountAmount = TotalDiscountAmount,
                //    //JVNumber=1
                //    JVStatus = false,
                //    CreatedDepartmentId = Hospital.Utility.GetCurrentUserDepartmentId(),
                //    CreatedBy = Hospital.Utility.GetCurrentLoginUserId(),
                //    CreatedDate = DateTime.Now,
                //    Remarks = "OT",
                //    Status = true,
                //    BranchId = 1,
                //    IsHandover = false,
                //    ReceiptId = 0,
                //    ReturnedAmount = Convert.ToDecimal(0),
                //    TenderAmount = Convert.ToDecimal(0)

                //};
                //ent.CentralizedBillingMaster.Add(ObjCentralizedBillingMaster);



                //objtoSaveCentralizedBilling = new CentralizedBilling()
                //{
                //    AccountHeadId = 22,
                //    Amount = (decimal)model.Discount,
                //    AmountDate = DateTime.Now,
                //    PaymentType = "Cash",
                //    Narration1 = "OT Fee Discount",
                //    Narration2 = "Discount",
                //    DepartmentName = "OT",
                //    SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                //    BillNumber = BillNumberStr,
                //    LedgerMasterId = ent.GL_LedgerMaster.Where(x => x.SourceID == objToSave.SourceID).SingleOrDefault().LedgerMasterID,
                //    PatientLogId = (int)objToSave.SourceID,
                //    PatientId = (int)objToSave.SourceID,
                //    JVStatus = false,
                //    CreatedBy = Utility.GetCurrentLoginUserId(),
                //    CreatedDate = DateTime.Now,
                //    Remarks = "OT",
                //    Status = true,
                //    ItemDiscountPercentage = 0
                //};
                //ent.CentralizedBilling.Add(objtoSaveCentralizedBilling);



                //update Bill Number
                //SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumber
                //                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                //                                      select x).First();
                //billNumber.BillNumber = billNumber.BillNumber + 1;

                ent.SaveChanges();

                return k;
            }

        }

        public OperationTheatreMasterModel GetObject(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == id).FirstOrDefault();
                OperationTheatreMasterModel model = AutoMapper.Mapper.Map<OperationTheatreMaster, OperationTheatreMasterModel>(obj);
                return model;
            }
        }

        public int Update(OperationTheatreMasterModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoedit = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == model.OperationTheatreMasterID).FirstOrDefault();
                //model.SourceID = objtoedit.SourceID;
                model.CreatedBy = objtoedit.CreatedBy;
                model.CreatedDate = objtoedit.CreatedDate;
                model.TotalCharge = objtoedit.TotalCharge;
                var objtocom = AutoMapper.Mapper.Map(model, objtoedit);
                objtocom.Status = true;


                //objtoedit.ValidUpto = Convert.ToDateTime(model.date);
                ent.Entry(objtocom).State = System.Data.EntityState.Modified;
                try
                {
                    i = ent.SaveChanges();
                }
                catch (Exception e)
                {
                    string s = e.InnerException.ToString();
                }
                var objlist = ent.OperationTheatreDetails.Where(x => x.OperationTheatreMasterID == model.OperationTheatreMasterID).ToList();
                foreach (var item in objlist)
                {
                    ent.OperationTheatreDetails.Remove(item);
                    ent.SaveChanges();
                }

                foreach (var item in model.PersonAllocatedList)
                {
                    var objTheatreDetails = new OperationTheatreDetail();
                    objTheatreDetails.OperationTheatreMasterID = model.OperationTheatreMasterID;
                    objTheatreDetails.PersonAllocatedID = item.PersonAllocateId;
                    objTheatreDetails.PersonAllocatedTypeID = item.PersonAllocatedTypeId;
                    ent.OperationTheatreDetails.Add(objTheatreDetails);
                    i = ent.SaveChanges();
                }
            }
            return i;

        }

        public int Delete(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var objToDelete = ent.OperationTheatreMasters.Where(x => x.OperationTheatreMasterID == id).FirstOrDefault();
            objToDelete.Status = false;
            ent.Entry(objToDelete).State = System.Data.EntityState.Modified;
            ent.SaveChanges();

            foreach (var item in ent.OperationTheatreDetails.Where(x => x.OperationTheatreMasterID == id))
            {
                ent.OperationTheatreDetails.Remove(item);
            }
            int i = ent.SaveChanges();
            return i;
        }

        public List<OperationTheatreDetailsModel> GetTheatreDetailsList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new List<OperationTheatreDetailsModel>(AutoMapper.Mapper.Map<IEnumerable<OperationTheatreDetail>, IEnumerable<OperationTheatreDetailsModel>>(ent.OperationTheatreDetails.Where(x => x.OperationTheatreMasterID == id)));
            }
        }

        //GetSurgeryListFromCBM
        public List<FromCBViewDetails> GetSurgeryListFromCBM()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                int PatientId = 123;
                string PatientName = "Default";
                return ent.Database.SqlQuery<FromCBViewDetails>("GetSurgeryDetailsFromCB '" + FromDate + "','" + ToDate + "','" + PatientId + "','" + PatientName + "'").ToList();
            }
        }

        public List<GetCBCommissionViewModelOT> GetCommissionDetailsFromCB()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                DateTime Fromdate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                int Billno = 1;
                int DepartmentId = 1005;
                return ent.Database.SqlQuery<GetCBCommissionViewModelOT>("GetPathologyCommissionDetails '" + Fromdate + "','" + ToDate + "','" + Billno + "','" + DepartmentId + "'").ToList();
            }


        }

        public List<GetOTCommDetailsViewModel> GetCommissionDetailsByBillNo(int BillNo)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                int Billno = BillNo;
                int DepartmentId = 1005;
                return ent.Database.SqlQuery<GetOTCommDetailsViewModel>("GetPathoCommDetailsByBillNo '" + Billno + "','" + DepartmentId + "'").ToList();
            }
        }


        public bool InsertOTCommissionDetails(OperationTheatreMasterModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                foreach (var item in model.AddMoreOTCommissionViewModelList)
                {
                    var Obj = new CommissionDetaislByType()
                    {
                        AccountHeadId = 1234,
                        Amount = item.CommissionAmout,
                        BillNo = model.ObjGetOTCommDetailsViewModel.BillNo,
                        CommissionDate = DateTime.Now,
                        commissionDetailsId = 1,
                        CommissionTypeId = 1005,
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

                var ObjResult = ent.CommissionDetails.Where(x => x.DepartmentId == 1005 && x.BillNumber == model.ObjGetOTCommDetailsViewModel.BillNo && x.Status == false);
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

    }
}