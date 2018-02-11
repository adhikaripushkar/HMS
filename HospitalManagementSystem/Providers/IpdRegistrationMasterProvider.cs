using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Providers;
using System.Data;


namespace HospitalManagementSystem.Providers
{
    public class IpdRegistrationMasterProvider
    {

        public int Insert(IpdRegistrationMasterModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var GetSourceIdFromOpdEmrID = 0;
                int PatientDepartmentId = 0;


                var PatientDepartmentIdVar = ent.OpdMasters.Where(x => x.OpdID == model.OpdNoEmergencyNo).FirstOrDefault().DepartmentId;
                PatientDepartmentId = Convert.ToInt32(PatientDepartmentIdVar);
                GetSourceIdFromOpdEmrID = ent.GL_LedgerMaster.Where(m => m.SourceID == model.OpdNoEmergencyNo && m.LedgerSourceType == "Patient").Select(m => m.LedgerMasterID).FirstOrDefault();

                var data = ent.IpdRegistrationMasters.Where(m => m.OpdNoEmergencyNo == model.OpdNoEmergencyNo && m.Status == true).ToList();



                if (data.Count > 0)
                {


                    //Status Update IPdBedDetails
                    var ObjIpdPatientBedDetail = ent.IpdPatientBedDetails.Where(m => m.OpdEmergencyNumber == model.OpdNoEmergencyNo && m.Status == true).FirstOrDefault();
                    ObjIpdPatientBedDetail.Status = false;
                    ObjIpdPatientBedDetail.LeaveDate = DateTime.Now;
                    ObjIpdPatientBedDetail.Remarks = "Bed Details";
                    ObjIpdPatientBedDetail.IsPaidBill = false;
                    ent.Entry(ObjIpdPatientBedDetail).State = EntityState.Modified;

                    //Staus Update IpdRoomDetails
                    var OsbjIpdPatientroomDetails = ent.IpdPatientroomDetails.Where(m => m.OpdNoEmergencyNo == model.OpdNoEmergencyNo && m.Status == true).FirstOrDefault();
                    OsbjIpdPatientroomDetails.Status = false;
                    ent.Entry(OsbjIpdPatientroomDetails).State = EntityState.Modified;
                    //sEnable Bed 
                    var GetIpdRoomID1 = ent.SetUpIpdRooms.Where(m => m.RoomType == OsbjIpdPatientroomDetails.RoomType && m.RoomNo == OsbjIpdPatientroomDetails.RoomNo).Select(m => m.IpdRoomId).FirstOrDefault();
                    var ObjStatus1 = ent.IpdRoomStatus.Where(m => m.IPDRoomId == (int)GetIpdRoomID1 && m.RoomNumber == OsbjIpdPatientroomDetails.RoomNo && m.BedNumber == OsbjIpdPatientroomDetails.BedNo).SingleOrDefault();
                    ObjStatus1.Status = true;
                    ent.Entry(ObjStatus1).State = EntityState.Modified;
                    //ent.SaveChanges();

                    //Insert New Row

                    var GetRegistrationID = ent.IpdRegistrationMasters.Where(m => m.OpdNoEmergencyNo == model.OpdNoEmergencyNo && m.Status == true).Select(m => m.IpdRegistrationID).FirstOrDefault();


                    //update ipdPatientDiagnosisDetails
                    foreach (var item in ent.IpdPatientDiagnosisDetails.Where(x => x.IpdRegistrationID == GetRegistrationID).ToList())
                    {
                        ent.IpdPatientDiagnosisDetails.Remove(item);
                    }
                    foreach (var item in model.ipdPatientDiagnosisDetailList)
                    {
                        IpdPatientDiagnosisDetail obj = new IpdPatientDiagnosisDetail();
                        obj.IpdRegistrationID = GetRegistrationID;
                        obj.DiagnosisID = item.DiagnosisID;
                        ent.IpdPatientDiagnosisDetails.Add(obj);

                    }
                    //update ipdPatientDiagnosisDetails


                    //Room Details
                    var objToSaveRoom = AutoMapper.Mapper.Map<IpdPatientroomDetailsModel, IpdPatientroomDetail>(model.IpdPatientroomDetailsModel);

                    objToSaveRoom.IpdRegistrationID = GetRegistrationID;
                    objToSaveRoom.OpdNoEmergencyNo = model.OpdNoEmergencyNo;
                    objToSaveRoom.Status = true;
                    ent.IpdPatientroomDetails.Add(objToSaveRoom);

                    //Bed Details
                    var objtosavebed = AutoMapper.Mapper.Map<IpdPatientBedDetailModel, IpdPatientBedDetail>(model.IpdPatientBedDetailsModel);
                    objtosavebed.IpdRegistrationId = GetRegistrationID;
                    objtosavebed.OpdEmergencyNumber = model.OpdNoEmergencyNo;

                    objtosavebed.BedNumber = objToSaveRoom.BedNo;
                    objtosavebed.AdmitedDate = (DateTime)model.RegistrationDate;
                    objtosavebed.LeaveDate = null;
                    objtosavebed.Status = true;
                    objtosavebed.Remarks = "BedDetails";
                    objtosavebed.IsPaidBill = false;

                    ent.IpdPatientBedDetails.Add(objtosavebed);
                    //Disabel Bed Status
                    var GetIpdRoomID = ent.SetUpIpdRooms.Where(m => m.RoomType == objToSaveRoom.RoomType && m.RoomNo == objToSaveRoom.RoomNo).Select(m => m.IpdRoomId).FirstOrDefault();
                    var ObjStatus = ent.IpdRoomStatus.Where(m => m.IPDRoomId == (int)GetIpdRoomID && m.RoomNumber == objToSaveRoom.RoomNo && m.BedNumber == objToSaveRoom.BedNo).SingleOrDefault();
                    ObjStatus.Status = false;
                    ent.Entry(ObjStatus).State = EntityState.Modified;


                    if (GetSourceIdFromOpdEmrID != 0)
                    {
                        int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Ipd", 1);
                        string BillNumberStr = "BL-" + BillNumberInt.ToString();

                        var objtoSaveCentralizedBilling = new CentralizedBilling()
                        {
                            AccountHeadId = 7,
                            Amount = model.IpdPatientBedDetailsModel.Amount,
                            AmountDate = DateTime.Now,
                            PaymentType = "Cash",
                            Narration1 = "Bed Fee",
                            DepartmentName = "Ipd",
                            SubDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            BillNumber = BillNumberStr,
                            LedgerMasterId = GetSourceIdFromOpdEmrID,
                            PatientLogId = model.OpdNoEmergencyNo,
                            PatientId = model.OpdNoEmergencyNo,
                            JVStatus = false,
                            CreatedBy = Utility.GetCurrentLoginUserId(),
                            CreatedDepartmentId = Utility.GetCurrentUserDepartmentId(),
                            CreatedDate = DateTime.Now,
                            Remarks = "Ipd",
                            PaidOnPaid = false,
                            Status = true,
                            ItemDiscountPercentage = 0
                        };

                        ent.CentralizedBillings.Add(objtoSaveCentralizedBilling);

                        //update Bill Number
                        SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                              where x.DepartmentName == "Ipd" && x.FiscalYearId == 1
                                                              select x).First();
                        billNumber.BillNumber = billNumber.BillNumber + 1;



                        //var maxvoucherno = ent.SetupVoucherNumber.Where(m => m.DepartmentID == 1003 && m.FiscalYear == 1).Max(m => m.VoucherNo);
                        //var updatevoucharnumber = ent.SetupVoucherNumber.Where(m => m.DepartmentID == 1003 && m.FiscalYear == 1).FirstOrDefault();
                        //updatevoucharnumber.VoucherNo = maxvoucherno + 1;
                        //ent.Entry(updatevoucharnumber).State = EntityState.Modified;



                    }

                    ent.SaveChanges();

                }

                else
                {

                    var objToSave1 = AutoMapper.Mapper.Map<IpdRegistrationMasterModel, IpdRegistrationMaster>(model);
                    using (EHMSEntities ents = new EHMSEntities())
                    {
                        objToSave1.CreatedtBy = Utility.GetCurrentLoginUserId();
                        objToSave1.Status = true;
                        objToSave1.DepartmentID = PatientDepartmentId;
                        objToSave1.CreatedDate = DateTime.Now;
                    }

                    //Room Details
                    var objToSaveRoom1 = AutoMapper.Mapper.Map<IpdPatientroomDetailsModel, IpdPatientroomDetail>(model.IpdPatientroomDetailsModel);

                    objToSaveRoom1.IpdRegistrationID = objToSave1.IpdRegistrationID;
                    objToSaveRoom1.OpdNoEmergencyNo = objToSave1.OpdNoEmergencyNo;
                    objToSaveRoom1.Status = true;


                    //Bed Details

                    var objtosavebed1 = AutoMapper.Mapper.Map<IpdPatientBedDetailModel, IpdPatientBedDetail>(model.IpdPatientBedDetailsModel);
                    objtosavebed1.IpdRegistrationId = objToSave1.IpdRegistrationID;
                    objtosavebed1.OpdEmergencyNumber = objToSave1.OpdNoEmergencyNo;

                    objtosavebed1.BedNumber = objToSaveRoom1.BedNo;
                    objtosavebed1.AdmitedDate = (DateTime)objToSave1.CreatedDate;
                    objtosavebed1.LeaveDate = null;
                    objtosavebed1.Status = true;
                    objtosavebed1.IsPaidBill = false;

                    var TosavepatientDepositDetails = new PatientDepositDetail()
                    {
                        PatientID = model.OpdNoEmergencyNo,
                        DepartmentID = PatientDepartmentId,
                        DepositAmount = model.DepositeAmount,
                        TaxAmount = Convert.ToDecimal(0),
                        TaxPerset = Convert.ToDecimal(0),
                        Discount = 0,

                        TotalAmount = model.DepositeAmount,
                        CreateDate = DateTime.Now,
                        CreatedBy = Utility.GetCurrentLoginUserId(),
                        Status = true
                    };
                    ent.PatientDepositDetails.Add(TosavepatientDepositDetails);
                    
                    //Get Currrent Receipt Number
                    if (model.DepositeAmount > 0)
                    {
                        int UserAccountHeadIdInt = 0;
                        var userAccountHeadId = ent.OpdMasters.Where(x => x.OpdID == model.OpdNoEmergencyNo).FirstOrDefault();
                        if (userAccountHeadId.AccountHeadId != null)
                        {
                            UserAccountHeadIdInt = (int)userAccountHeadId.AccountHeadId;
                        }


                        var ObjGlAccSubGroup = new GL_AccSubGroups();

                        if (UserAccountHeadIdInt > 0)
                        {
                        }

                        else
                        {
                            ObjGlAccSubGroup = new GL_AccSubGroups()
                            {
                                AccGroupID = 2,
                                AccSubGroupName = HospitalManagementSystem.Utility.GetPatientNameWithIdFromOpd(model.OpdNoEmergencyNo),
                                ParentID = 1258,
                                HierarchyCode = "2.1253.1257.1258",
                                HeadLevel = 5,
                                AccountCode = null,
                                IsLeafLevel = true,
                                Status = true,
                                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                Remarks = "Deposit",
                                BranchId = 1

                            };

                            ent.GL_AccSubGroups.Add(ObjGlAccSubGroup);
                            ent.SaveChanges();
                            UserAccountHeadIdInt = ObjGlAccSubGroup.AccSubGruupID;

                        }
                        //Update in opdmaster table
                        OpdMaster AccountHeadId = (from x in ent.OpdMasters
                                                   where x.OpdID == model.OpdNoEmergencyNo
                                                   select x).First();
                        AccountHeadId.AccountHeadId = UserAccountHeadIdInt;
                        int UserReceiptNumber = Utility.GetMaxBillNumberFromDepartment("Deposit", 1);
                        int ReceiptNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);

                        var maxid = ent.IpdRegistrationMasters.Max(x => x.IpdRegistrationID);

                        var ObjPatientDepositMaster = new PatientDepositMaster()
                        {
                            PatientId = model.OpdNoEmergencyNo,
                            DepartmentId = 1003,
                            PatientDepartmentId = maxid+1,
                            DepostedBy = "",
                            RelationId = 0,
                            DepositedAmount = model.DepositeAmount,
                            DepositedDate = DateTime.Now,
                            SwipeCardId = "NN009",
                            SwipeCardDetail = "sdetails",
                            CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Now,
                            CreatedDepartmentId = 0,
                            DepositeType = 1,
                            Status = true,
                            BranchId = 1
                            
                        };
                        ent.PatientDepositMasters.Add(ObjPatientDepositMaster);

                        var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                        {
                            BillNo = ReceiptNumberInt,
                            BillDate = DateTime.Now,
                            TotalBillAmount = model.DepositeAmount,
                            Narration1 = "Deposit",
                            Narration2 = "",
                            DepartmentName = "CB",
                            SubDepartmentId = 1,
                            PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.OpdNoEmergencyNo),
                            PatientId = model.OpdNoEmergencyNo,
                            CreatedDepartmentId = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId(),
                            CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                            CreatedDate = DateTime.Now,
                            Remarks = "Deposit",
                            Status = true,
                            BranchId = 1,
                            JVStatus = false,
                            ReceiptId = Utility.GetMaxDepositeNumber(),
                            IsHandover = false,
                            ReturnedAmount = Convert.ToDecimal(0),
                            TenderAmount = Convert.ToDecimal(0),
                            TotalDiscountAmount = Convert.ToDecimal(0),
                            TotalDiscountID = 0,
                            PayableType=1//General
                        };

                        ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);

                        var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                        {
                            BillNo = ReceiptNumberInt,
                            AccountHeadID = 1258,//deposit
                            AccountSubHeadID = UserAccountHeadIdInt,//patientId
                            Amount = model.DepositeAmount,
                            Status = true,
                            DepartmentId = 1003//Ipd Department
                        };
                        ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                        var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                        {
                            BillNo = ReceiptNumberInt,
                            PaymentTypeID = 372,
                            Amount = model.DepositeAmount,
                            Status = true,
                            PaymentSubTypeID = Convert.ToInt32(0)
                        };
                        ent.CentralizedBillingPaymentTypes.Add(ObjCentralizedBillingPaymentType);
                        //update Bill Number
                        SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                              where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                              select x).First();
                        billNumber.BillNumber = billNumber.BillNumber + 1;
                        ent.SaveChanges();

                        //update Bill Number
                        SetupHospitalBillNumber billNumberDeposit = (from x in ent.SetupHospitalBillNumbers
                                                                     where x.DepartmentName == "Deposit" && x.FiscalYearId == 1
                                                                     select x).First();
                        billNumberDeposit.BillNumber = billNumberDeposit.BillNumber + 1;
                        ent.SaveChanges();

                    }
                    //var AdmissionDetails = ent.AdmissionDipositFee.Where(m => m.DepartmentID == 1003).ToList();

                    //if (AdmissionDetails.Count > 0)
                    //{
                    //    decimal TotalAmount = Convert.ToDecimal(ent.AdmissionDipositFee.Where(m => m.DepartmentID == 1003).Select(m => m.DepositAmount).FirstOrDefault());
                    //    decimal TaxAmount = Convert.ToDecimal(ent.AdmissionDipositFee.Where(m => m.DepartmentID == 1003).Select(m => m.TaxAmoutn).FirstOrDefault());
                    //    decimal TaxPesent = Convert.ToDecimal(ent.AdmissionDipositFee.Where(m => m.DepartmentID == 1003).Select(m => m.TaxPersentage).FirstOrDefault());
                    //    decimal GrandTotalAmount = Convert.ToDecimal(ent.AdmissionDipositFee.Where(m => m.DepartmentID == 1003).Select(m => m.TotalAmount).FirstOrDefault());

                    //    var TosavepatientDepositDetails = new PatientDepositDetails()
                    //    {
                    //        PatientID = model.OpdNoEmergencyNo,
                    //        DepartmentID = PatientDepartmentId,
                    //        DepositAmount = TotalAmount,
                    //        TaxAmount = TaxAmount,
                    //        TaxPerset = TaxPesent,
                    //        Discount = 0,
                    //        TotalAmount = GrandTotalAmount,
                    //        CreateDate = DateTime.Now,
                    //        CreatedBy = Utility.GetCurrentLoginUserId(),
                    //        Status = true


                    //    };

                    //    ent.PatientDepositDetails.Add(TosavepatientDepositDetails);
                    //    //sAmount For Tax


                    //}


                    if (GetSourceIdFromOpdEmrID != 0)
                    {
                        int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Ipd", 1);
                        string BillNumberStr = "BL-" + BillNumberInt.ToString();

                        //update Bill Number
                        SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                              where x.DepartmentName == "Ipd" && x.FiscalYearId == 1
                                                              select x).First();
                        billNumber.BillNumber = billNumber.BillNumber + 1;


                    }



                    ent.IpdRegistrationMasters.Add(objToSave1);
                    ent.IpdPatientroomDetails.Add(objToSaveRoom1);
                    ent.IpdPatientBedDetails.Add(objtosavebed1);
                    ent.SaveChanges();

                    //insert into ipdPatientDiagnosisDetails
                    foreach (var item in model.ipdPatientDiagnosisDetailList)
                    {
                        IpdPatientDiagnosisDetail obj = new IpdPatientDiagnosisDetail();
                        obj.IpdRegistrationID = objToSave1.IpdRegistrationID;
                        obj.DiagnosisID = item.DiagnosisID;
                        ent.IpdPatientDiagnosisDetails.Add(obj);

                    }
                    //insert into ipdPatientDiagnosisDetails


                    //Disabel Bed Status
                    var GetIpdRoomID = ent.SetUpIpdRooms.Where(m => m.RoomType == objToSaveRoom1.RoomType && m.RoomNo == objToSaveRoom1.RoomNo).Select(m => m.IpdRoomId).FirstOrDefault();
                    var ObjStatus = ent.IpdRoomStatus.Where(m => m.IPDRoomId == (int)GetIpdRoomID && m.RoomNumber == objToSaveRoom1.RoomNo && m.BedNumber == objToSaveRoom1.BedNo).SingleOrDefault();
                    ObjStatus.Status = false;
                    ent.Entry(ObjStatus).State = EntityState.Modified;
                    ent.SaveChanges();

                }

            }
            return 1;

        }
        public List<IpdRegistrationMasterModel> GetListForIndex()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<IpdRegistrationMasterModel> model = new List<IpdRegistrationMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<IpdRegistrationMaster>, IEnumerable<IpdRegistrationMasterModel>>(ent.IpdRegistrationMasters).Where(m => m.Status == true).ToList();
                return model;
            }
        }

        public List<CentralizedBillingModel> GetAllFromCentrBilling()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<CentralizedBillingModel> modelList = (from c in ent.CentralizedBillings
                                                           where c.Status == true && c.DepartmentName == "IPD" && c.Remarks == "CB"
                                                           group c by new { c.PatientId, c.DepartmentName, c.BillNumber, c.AmountDate } into g
                                                           select new CentralizedBillingModel
                                                           {
                                                               PatientId = g.Key.PatientId,
                                                               DepartmentName = g.Key.DepartmentName,
                                                               BillNumber = g.Key.BillNumber,
                                                               AmountDate = g.Key.AmountDate,

                                                           }).ToList();

                return modelList;
            }
        }

        public List<IpdPatientBedDetailModel> GetListForBed(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<IpdPatientBedDetailModel> models = new List<IpdPatientBedDetailModel>();
                models = AutoMapper.Mapper.Map<IEnumerable<IpdPatientBedDetail>, IEnumerable<IpdPatientBedDetailModel>>(ent.IpdPatientBedDetails).Where(x => x.IpdPatientBedDetailId == id).ToList();

                return models;
            }
        }
        public List<IpdPatientroomDetailsModel> GetListForRoom(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<IpdPatientroomDetailsModel> models = new List<IpdPatientroomDetailsModel>();
                models = AutoMapper.Mapper.Map<IEnumerable<IpdPatientroomDetail>, IEnumerable<IpdPatientroomDetailsModel>>(ent.IpdPatientroomDetails).Where(x => x.IpdRegistrationID == id).ToList();
                return models;

            }


        }

        public List<OpdModel> GetOpdModelList(int OpdEmrNo)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<OpdModel> model = new List<OpdModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMasters).Where(x => x.OpdID == OpdEmrNo).ToList();
                return model;
            }

        }

        //public List<OpdModel> GetAllDataOfOpdMaster(string OpdFirstName)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        if (OpdFirstName =="")
        //        {
        //            List<OpdModel> model = new List<OpdModel>();
        //            model = AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMaster).Where(m => m.Status == true).ToList();
        //            return model;
        //        }
        //        else
        //        {
        //            List<OpdModel> model = new List<OpdModel>();
        //            model = AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(ent.OpdMaster).Where(m=>m.FirstName.Contains(OpdFirstName) && m.Status==true).ToList();
        //            return model;
        //        }

        //    }

        //}

        public List<EmergecyMasterModel> GetEmergencyModelList(int IPdEmrNo)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<EmergecyMasterModel> model = new List<EmergecyMasterModel>();
                model = AutoMapper.Mapper.Map<IEnumerable<EmergencyMaster>, IEnumerable<EmergecyMasterModel>>(ent.EmergencyMasters).Where(m => m.EmergencyMasterId == IPdEmrNo).ToList();
                return model;
            }
        }
        public List<IpdSearchResults> Search(int wardno, int RoomType, int RoomNo)
        {
            List<IpdSearchResults> IpdSearchResultList = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                IpdSearchResults models = new IpdSearchResults();

                var data = (from x in ent.IpdRegistrationMasters
                            join y in ent.IpdPatientroomDetails on x.IpdRegistrationID equals y.IpdRegistrationID
                            join z in ent.OpdMasters on x.OpdNoEmergencyNo equals z.OpdID
                            join w in ent.IpdPatientBedDetails on x.IpdRegistrationID equals w.IpdRegistrationId
                            where y.RoomType == RoomType && y.RoomNo == RoomNo && y.WardNo == wardno && w.Status == false
                            select new IpdSearchResults
                            {
                                FullName = z.FirstName,
                                RegistrationDate = (DateTime)z.RegistrationDate,
                                BedNo = y.BedNo,
                                Address = z.Address,
                                Age = z.AgeYear



                            }).ToList();

                foreach (var item in data)
                {
                    models.FullName = item.FullName;
                    models.RegistrationDate = item.RegistrationDate;
                    models.BedNo = item.BedNo;
                    IpdSearchResultList.Add(item);
                }
            }
            return IpdSearchResultList;

        }
        //sMultiple Use This Query Show Detais patient And IpdMRCommonTest Entry//
        public List<IpdSearchResults> SearchByPatientName(string name)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DetailsData = (from o in ent.OpdMasters
                                   join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                   join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                   where o.Status == true && r.Status == true && room.Status == true && o.FirstName.Contains(name)
                                   select new IpdSearchResults
                                   {
                                       FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                       DepartmentId = r.DepartmentID,
                                       RoomType = room.RoomType,
                                       RoomNo = room.RoomNo,
                                       WardNo = room.WardNo,
                                       BedNo = room.BedNo,
                                       sex = o.Sex,
                                       Age = o.AgeYear,
                                       Address = o.Address,
                                       OpdEmrNumber = r.OpdNoEmergencyNo,
                                       IpdRegistrationNumber = r.IpdRegistrationID,
                                       RegistrationDate = o.RegistrationDate

                                   }).ToList();

                foreach (var items in DetailsData)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public List<IpdSearchResults> SearchByPatientNamebyid(int id, int opdid)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DetailsData = (from o in ent.OpdMasters
                                   join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                   join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                   where o.Status == true && r.Status == true && room.Status == true && o.OpdID == opdid && r.IpdRegistrationID == id
                                   select new IpdSearchResults
                                   {
                                       FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                       DepartmentId = r.DepartmentID,
                                       RoomType = room.RoomType,
                                       RoomNo = room.RoomNo,
                                       WardNo = room.WardNo,
                                       BedNo = room.BedNo,
                                       sex = o.Sex,
                                       Age = o.AgeYear,
                                       Address = o.Address,
                                       OpdEmrNumber = r.OpdNoEmergencyNo,
                                       IpdRegistrationNumber = r.IpdRegistrationID,
                                       RegistrationDate = o.RegistrationDate

                                   }).ToList();

                foreach (var items in DetailsData)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }

        public List<IpdSearchResults> PatientForIpd(int id)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DetailsData = (from o in ent.OpdMasters
                                   join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                   join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                   where o.Status == true && r.Status == true && room.Status == true && o.OpdID == id
                                   select new IpdSearchResults
                                   {
                                       FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                       DepartmentId = r.DepartmentID,
                                       RoomType = room.RoomType,
                                       RoomNo = room.RoomNo,
                                       WardNo = room.WardNo,
                                       BedNo = room.BedNo,
                                       sex = o.Sex,
                                       Age = o.AgeYear,
                                       Address = o.Address,
                                       OpdEmrNumber = r.OpdNoEmergencyNo,
                                       IpdRegistrationNumber = r.IpdRegistrationID,
                                       RegistrationDate = o.RegistrationDate

                                   }).ToList();

                foreach (var items in DetailsData)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }

        public List<IpdSearchResults> PatientForIpdAfterDischarge(int id, int ipdregid, int romid)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DetailsData = (from o in ent.OpdMasters
                                   join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                   join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                   where o.Status == true && r.Status == false && room.Status == false && o.OpdID == id
                                   && r.IpdRegistrationID == ipdregid && room.IpdRegistrationID == ipdregid && room.IpdPatientRoomDetailId == romid
                                   select new IpdSearchResults
                                   {
                                       FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                       DepartmentId = r.DepartmentID,
                                       RoomType = room.RoomType,
                                       RoomNo = room.RoomNo,
                                       WardNo = room.WardNo,
                                       BedNo = room.BedNo,
                                       sex = o.Sex,
                                       Age = o.AgeYear,
                                       Address = o.Address,
                                       OpdEmrNumber = r.OpdNoEmergencyNo,
                                       IpdRegistrationNumber = r.IpdRegistrationID,
                                       RegistrationDate = o.RegistrationDate

                                   }).ToList();

                foreach (var items in DetailsData)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }



        public int Discharge(int ipdresistrationid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                //status Of IpdResistrationMaste
                var ipdresistrationmasterdata = ent.IpdRegistrationMasters.Where(m => m.IpdRegistrationID == ipdresistrationid && m.Status == true).FirstOrDefault();
                if (ipdresistrationmasterdata != null)
                {
                    ipdresistrationmasterdata.Status = false;
                    ent.Entry(ipdresistrationmasterdata).State = EntityState.Modified;

                    //Status Update IPdBedDetails
                    var ObjIpdPatientBedDetail = ent.IpdPatientBedDetails.Where(m => m.OpdEmergencyNumber == ipdresistrationmasterdata.OpdNoEmergencyNo && m.Status == true).FirstOrDefault();
                    ObjIpdPatientBedDetail.Status = false;
                    ObjIpdPatientBedDetail.LeaveDate = DateTime.Now;
                    ObjIpdPatientBedDetail.Remarks = "Discharge";
                    ObjIpdPatientBedDetail.IsPaidBill = false;
                    ent.Entry(ObjIpdPatientBedDetail).State = EntityState.Modified;

                    //Staus Update IpdRoomDetails
                    var OsbjIpdPatientroomDetails = ent.IpdPatientroomDetails.Where(m => m.OpdNoEmergencyNo == ipdresistrationmasterdata.OpdNoEmergencyNo && m.Status == true).FirstOrDefault();
                    OsbjIpdPatientroomDetails.Status = false;
                    ent.Entry(OsbjIpdPatientroomDetails).State = EntityState.Modified;
                    //sEnable Bed 
                    var GetIpdRoomID1 = ent.SetUpIpdRooms.Where(m => m.RoomType == OsbjIpdPatientroomDetails.RoomType && m.RoomNo == OsbjIpdPatientroomDetails.RoomNo).Select(m => m.IpdRoomId).FirstOrDefault();
                    var ObjStatus1 = ent.IpdRoomStatus.Where(m => m.IPDRoomId == (int)GetIpdRoomID1 && m.RoomNumber == OsbjIpdPatientroomDetails.RoomNo && m.BedNumber == OsbjIpdPatientroomDetails.BedNo).SingleOrDefault();
                    ObjStatus1.Status = true;
                    ent.Entry(ObjStatus1).State = EntityState.Modified;
                    ent.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }


            }

        }

        //sMultiple Use This Query Show Detais patient And IpdMRCommonTest Entry//
        public List<IpdSearchResults> DischargReport(int id)
        {
            List<IpdSearchResults> ListIpdResult = new List<IpdSearchResults>();
            using (EHMSEntities ent = new EHMSEntities())
            {


                var DischargeDetails = (from o in ent.OpdMasters
                                        join r in ent.IpdRegistrationMasters on o.OpdID equals r.OpdNoEmergencyNo
                                        join room in ent.IpdPatientroomDetails on o.OpdID equals room.OpdNoEmergencyNo
                                        join bed in ent.IpdPatientBedDetails on o.OpdID equals bed.OpdEmergencyNumber
                                        where r.IpdRegistrationID == id
                                        select new IpdSearchResults
                                        {
                                            FullName = o.FirstName + " " + (o.MiddleName + " " ?? string.Empty) + o.LastName,
                                            // DepartmentId = r.DepartmentID,
                                            RoomType = room.RoomType,
                                            RoomNo = room.RoomNo,
                                            WardNo = room.WardNo,
                                            BedNo = room.BedNo,
                                            sex = o.Sex,
                                            Age = o.AgeYear,
                                            Address = o.Address,
                                            OpdEmrNumber = r.OpdNoEmergencyNo,
                                            IpdRegistrationNumber = r.IpdRegistrationID,
                                            RegistrationDate = r.RegistrationDate,
                                            DateOfDischarge = bed.LeaveDate,

                                        }).ToList();

                foreach (var items in DischargeDetails)
                {
                    ListIpdResult.Add(items);
                }
            }
            return ListIpdResult;

        }
        public int InsertInDischarge(IpdDischargeModel model)
        {
            int i = 0;

            foreach (var item in model.refofDiagnosisDetails.lstOfDiagnosisDetails)
            {

                var last = model.refofDiagnosisDetails.lstOfDiagnosisDetails.LastOrDefault();
                if (last == item)
                {

                    model.Diagnosis += item.DiagnosisName;
                }
                else
                {
                    model.Diagnosis += item.DiagnosisName + ",";
                }


            }


            using (EHMSEntities ent = new EHMSEntities())
            {
                var ToSaveDischargeDetils = AutoMapper.Mapper.Map<IpdDischargeModel, IpdDischarge>(model);
                ToSaveDischargeDetils.Dignosis = model.Diagnosis;
                ent.IpdDischarges.Add(ToSaveDischargeDetils);

                int patientlogid = Utility.getPatientLogID(model.refOfIpdRegistrationMasterModel.OpdNoEmergencyNo);

                var changeTovital = ent.VitalForOthers.Where(x => x.Status == true && x.OpdId == model.refOfIpdRegistrationMasterModel.OpdNoEmergencyNo && x.PatinetLogId == patientlogid).SingleOrDefault();
                if (changeTovital != null)
                {

                    changeTovital.Status = false;
                    ent.Entry(changeTovital).State = EntityState.Modified;
                }

                i = ent.SaveChanges();
            }
            return i;
        }


        public int PatientDischargeinIpd(int Ipdmasterid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var changetostatus = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == Ipdmasterid).SingleOrDefault();

                changetostatus.Status = false;

                var bedleave = ent.IpdPatientBedDetails.Where(x => x.IpdRegistrationId == Ipdmasterid && x.Status == true).FirstOrDefault();
                bedleave.LeaveDate = DateTime.Now;
                bedleave.Status = false;
                bedleave.Remarks = "Discharge";
                bedleave.IsPaidBill = false;

                var Roomleave = ent.IpdPatientroomDetails.Where(x => x.IpdRegistrationID == Ipdmasterid && x.Status == true).FirstOrDefault();
                int RoomId = Roomleave.RoomNo;
                int BedNo = Roomleave.BedNo;
                Roomleave.Status = false;

                var IPDroomStatus = ent.IpdRoomStatus.Where(x => x.RoomNumber == RoomId && x.BedNumber == BedNo && x.Status == false).FirstOrDefault();

                IPDroomStatus.Status = true;

                ent.Entry(bedleave).State = EntityState.Modified;
                ent.Entry(Roomleave).State = EntityState.Modified;
                ent.Entry(IPDroomStatus).State = EntityState.Modified;





                int i = ent.SaveChanges();
                return i;
            }

        }


        public int UpdateDischarge(IpdDischargeModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ToupdateData = ent.IpdDischarges.Where(m => m.ipdResistrationID == model.ipdResistrationID).FirstOrDefault();
                ToupdateData.BriefHistory = model.BriefHistory;
                ToupdateData.ClinicalFindings = model.ClinicalFindings;
                ToupdateData.DignosisID = model.DignosisID;
                ToupdateData.ipdResistrationID = model.ipdResistrationID;
                model.Medicinetreatment = model.Medicinetreatment;
                model.FurtherTreatment = model.FurtherTreatment;
                ent.Entry(ToupdateData).State = EntityState.Modified;
                ent.SaveChanges();
                return 1;

            }
        }

        //sInset Patient New In OpdMasterTable

        //public int NewPatientInsert(OpdModel model)
        //{
        //    var ToSave = AutoMapper.Mapper.Map<OpdModel, OpdMaster>(model);
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        ToSave.CreatedBy = Utility.GetCurrentLoginUserId();
        //        ToSave.Status = true;
        //        ToSave.PaidOnPaid = true;
        //        ToSave.CreatedDate = DateTime.Now;
        //        ToSave.RegistrationMode = "Visit";
        //        ToSave.RegistrationDate = DateTime.Now;
        //        ToSave.RegistrationSource = "Ipd";
        //        ent.OpdMaster.Add(ToSave);
        //        ent.SaveChanges();
        //        var maxID = ent.OpdMaster.Max(m => m.OpdID);
        //        return maxID;
        //    }

        //}

        public int NewPatientInsert(OpdModel model)
        {
            var ToSave = AutoMapper.Mapper.Map<OpdModel, OpdMaster>(model);
            using (EHMSEntities ent = new EHMSEntities())
            {
                ToSave.CreatedBy = Utility.GetCurrentLoginUserId();
                ToSave.Status = true;
                ToSave.PaidOnPaid = true;
                ToSave.CreatedDate = DateTime.Now;
                ToSave.RegistrationMode = "Visit";
                ToSave.RegistrationDate = DateTime.Now;
                ToSave.RegistrationSource = "Ipd";


                //Insert Patient Log into Log Table
                int? intIdt = ent.OpdMasters.Max(u => (int?)u.OpdID);
                if (intIdt == null)
                {
                    intIdt = 1;
                }
                else
                {
                    intIdt = intIdt + 1;
                }

                //Insert Patient Log into Log Table
                var objToSavePatientLogMaster = new PatientLogMaster()
                {
                    PatientId = intIdt.GetValueOrDefault(),
                    RegistrationDate = Convert.ToDateTime(ToSave.RegistrationDate),
                    DepartmentId = Utility.GetCurrentLoginUserId(),
                    OpdTypeID = Utility.GetOpdTypeFromSession(),//Payable or free
                    Status = true
                };
                ent.PatientLogMasters.Add(objToSavePatientLogMaster);
                ent.SaveChanges();
                ent.OpdMasters.Add(ToSave);
                ent.SaveChanges();
                var maxID = ent.OpdMasters.Max(m => m.OpdID);
                return maxID;
            }

        }


        public int InsertCeltralBillingPatientINIPDResistrationMaster(IpdRegistrationMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var GetSourceIdFromOpdEmrID = 0;
                int PatientDepartmentId = 0;


                var PatientDepartmentIdVar = ent.OpdMasters.Where(x => x.OpdID == model.OpdNoEmergencyNo).FirstOrDefault().DepartmentId;
                PatientDepartmentId = Convert.ToInt32(PatientDepartmentIdVar);
                GetSourceIdFromOpdEmrID = ent.GL_LedgerMaster.Where(m => m.SourceID == model.OpdNoEmergencyNo && m.LedgerSourceType == "Patient").Select(m => m.LedgerMasterID).FirstOrDefault();

                var data = ent.IpdRegistrationMasters.Where(m => m.OpdNoEmergencyNo == model.OpdNoEmergencyNo && m.Status == true).ToList();

                var objToSave1 = AutoMapper.Mapper.Map<IpdRegistrationMasterModel, IpdRegistrationMaster>(model);
                using (EHMSEntities ents = new EHMSEntities())
                {
                    objToSave1.CreatedtBy = Utility.GetCurrentLoginUserId();
                    objToSave1.Status = true;
                    objToSave1.DepartmentID = PatientDepartmentId;
                    objToSave1.CreatedDate = DateTime.Now;
                }

                //Room Details
                var objToSaveRoom1 = AutoMapper.Mapper.Map<IpdPatientroomDetailsModel, IpdPatientroomDetail>(model.IpdPatientroomDetailsModel);

                objToSaveRoom1.IpdRegistrationID = objToSave1.IpdRegistrationID;
                objToSaveRoom1.OpdNoEmergencyNo = objToSave1.OpdNoEmergencyNo;
                objToSaveRoom1.Status = true;


                //Bed Details

                var objtosavebed1 = AutoMapper.Mapper.Map<IpdPatientBedDetailModel, IpdPatientBedDetail>(model.IpdPatientBedDetailsModel);
                objtosavebed1.IpdRegistrationId = objToSave1.IpdRegistrationID;
                objtosavebed1.OpdEmergencyNumber = objToSave1.OpdNoEmergencyNo;

                objtosavebed1.BedNumber = objToSaveRoom1.BedNo;
                objtosavebed1.AdmitedDate = (DateTime)objToSave1.CreatedDate;
                objtosavebed1.LeaveDate = null;
                objtosavebed1.Status = true;

                if (model.DepartmentID != 1000)
                {

                }

                var TosavepatientDepositDetails = new PatientDepositDetail()
                {
                    PatientID = model.OpdNoEmergencyNo,
                    DepartmentID = PatientDepartmentId,
                    DepositAmount = model.DepositeAmount,
                    TaxAmount = Convert.ToDecimal(0),
                    TaxPerset = Convert.ToDecimal(0),
                    Discount = 0,
                    TotalAmount = model.DepositeAmount,
                    CreateDate = DateTime.Now,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    Status = true
                };

                ent.PatientDepositDetails.Add(TosavepatientDepositDetails);

                ent.IpdRegistrationMasters.Add(objToSave1);
                ent.IpdPatientroomDetails.Add(objToSaveRoom1);
                ent.IpdPatientBedDetails.Add(objtosavebed1);
                ent.SaveChanges();

                //Disabel Bed Status
                var GetIpdRoomID = ent.SetUpIpdRooms.Where(m => m.RoomType == objToSaveRoom1.RoomType && m.RoomNo == objToSaveRoom1.RoomNo).Select(m => m.IpdRoomId).FirstOrDefault();
                var ObjStatus = ent.IpdRoomStatus.Where(m => m.IPDRoomId == (int)GetIpdRoomID && m.RoomNumber == objToSaveRoom1.RoomNo && m.BedNumber == objToSaveRoom1.BedNo).SingleOrDefault();
                ObjStatus.Status = false;
                ent.Entry(ObjStatus).State = EntityState.Modified;


                //sDisable CentralizeBilling patient Status
                var StatasOffOFCentralizeBilling = ent.CentralizedBillings.Where(m => m.PatientId == model.OpdNoEmergencyNo && m.Status == true && m.DepartmentName == "IPD" && m.Remarks == "CB").ToList();
                foreach (var item in StatasOffOFCentralizeBilling)
                {
                    item.Status = false;
                    ent.Entry(item).State = EntityState.Modified;

                }
                ent.SaveChanges();

                return 1;

            }
        }

        public IpdSearchResults GetDailyIpdReport(DateTime ResistrationDate)
        {
            EHMSEntities ent = new EHMSEntities();
            IpdSearchResults model = new IpdSearchResults();
            // var IPDPatientID = ent.IpdRegistrationMaster.Where(m => m.RegistrationDate == ResistrationDate).ToList();

            //foreach (var item in IPDPatientID)
            //{
            var tolist = (from r in ent.IpdRegistrationMasters
                          join b in ent.IpdPatientBedDetails on r.IpdRegistrationID equals b.IpdRegistrationId
                          join o in ent.OpdMasters on r.OpdNoEmergencyNo equals o.OpdID
                          where (r.RegistrationDate >= ResistrationDate) && (r.RegistrationDate <= ResistrationDate) && r.Status == true// r.OpdNoEmergencyNo == item.OpdNoEmergencyNo
                          select new { r.OpdNoEmergencyNo, r.RegistrationDate, b.BedNumber, o.AgeYear, o.AgeMonth, o.AgeDay, o.Sex }).ToList();

            foreach (var items in tolist)
            {

                var ViewIPdResistrationMaster = new IpdRegistrationMasterModel();
                {
                    ViewIPdResistrationMaster.patientName = HospitalManagementSystem.Utility.OpdPatientName(items.OpdNoEmergencyNo);
                    ViewIPdResistrationMaster.BedNo = items.BedNumber;
                    ViewIPdResistrationMaster.RegistrationDate = items.RegistrationDate;
                    //ViewIPdResistrationMaster.DiagnosisID = items.DiagnosisID;
                    ViewIPdResistrationMaster.Gender = items.Sex;
                    ViewIPdResistrationMaster.Birth = items.AgeYear.ToString();
                    ViewIPdResistrationMaster.IpdRegistrationID = items.OpdNoEmergencyNo;




                }
                model.IpdRegistrationMasterModelList.Add(ViewIPdResistrationMaster);
            }

            var Dscharg = (from r in ent.IpdRegistrationMasters
                           join b in ent.IpdPatientBedDetails on r.IpdRegistrationID equals b.IpdRegistrationId
                           join o in ent.OpdMasters on r.OpdNoEmergencyNo equals o.OpdID
                           where (r.RegistrationDate >= ResistrationDate) && (r.RegistrationDate <= ResistrationDate) && r.Status == false// r.OpdNoEmergencyNo == item.OpdNoEmergencyNo
                           select new { r.OpdNoEmergencyNo, r.RegistrationDate, b.BedNumber, o.AgeYear, o.AgeMonth, o.AgeDay, o.Sex, b.LeaveDate }).ToList();


            foreach (var DschargItem in Dscharg)
            {

                var ViewDischarg = new IpdDischargeModel();
                {
                    ViewDischarg.patientName = HospitalManagementSystem.Utility.OpdPatientName(DschargItem.OpdNoEmergencyNo);
                    ViewDischarg.BedNo = DschargItem.BedNumber;
                    ViewDischarg.LeaveDate = DschargItem.LeaveDate;
                    //ViewDischarg.DignosisID = DschargItem.DiagnosisID;
                    ViewDischarg.Gender = DschargItem.Sex;
                    ViewDischarg.Birth = DschargItem.AgeYear + " " + DschargItem.AgeMonth + " " + DschargItem.AgeDay;
                    ViewDischarg.ipdResistrationID = DschargItem.OpdNoEmergencyNo;
                }
                model.IpdDischargeModelList.Add(ViewDischarg);



            }


            var Opetration = (from r in ent.IpdRegistrationMasters
                              join o in ent.OperationTheatreMasters on r.OpdNoEmergencyNo equals o.SourceID
                              where (r.RegistrationDate >= ResistrationDate) && (r.RegistrationDate <= ResistrationDate) && r.Status == true // && r.OpdNoEmergencyNo == item.OpdNoEmergencyNo
                              group r by new { r.OpdNoEmergencyNo, o.SourceID, o.ChargeName, o.OperationDate } into g
                              select new OperationTheatreMasterModel
                              {
                                  SourceID = g.Key.OpdNoEmergencyNo,
                                  //Diagnosis = g.Key.DiagnosisID,
                                  ChargeName = g.Key.ChargeName,
                                  OperationDate = g.Key.OperationDate,



                              }).ToList();




            foreach (var OptItem in Opetration)
            {

                var ViewOptThrt = new OperationTheatreMasterModel();
                {
                    ViewOptThrt.PatientName = Utility.OpdPatientName(OptItem.SourceID);
                    ViewOptThrt.Diagnosis = OptItem.Diagnosis;
                    ViewOptThrt.ChargeName = OptItem.ChargeName;
                    ViewOptThrt.OperationDate = OptItem.OperationDate;
                    ViewOptThrt.SourceID = OptItem.SourceID;
                }

                model.OperationTheatreMasterModelList.Add(ViewOptThrt);



            }

            return model;
        }
        public List<IpdPartialDetails> GetPatientlistByName(string name)
        {
            var id = Convert.ToInt32(name);
            List<IpdPartialDetails> EMIDmodel = new List<IpdPartialDetails>();

            EHMSEntities ent = new EHMSEntities();
            var data = (from a in ent.OpdMasters
                        join b in ent.IpdRegistrationMasters on a.OpdID equals b.OpdNoEmergencyNo
                        join c in ent.IpdPatientroomDetails on b.OpdNoEmergencyNo equals c.OpdNoEmergencyNo
                        join d in ent.SetupIpdWards on c.WardNo equals d.IpdWardId
                        join e in ent.SetupRoomTypes on c.RoomType equals e.RoomTypeId
                        where c.OpdNoEmergencyNo == id
                        select new IpdPartialDetails
                        {

                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            Address = a.Address,
                            Admitdate = a.RegistrationDate,
                            WardNo = c.WardNo,
                            RoomType = c.RoomType,
                            RoomNo = c.RoomNo,
                            BedNo = c.BedNo



                        }).Distinct().ToList();

            foreach (var items in data)
            {
                EMIDmodel.Add(items);
            }
            return EMIDmodel;
        }


        //by subin


        public int InsertIpdVital(VitalForOthersModel model)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtosaveinvital = AutoMapper.Mapper.Map<VitalForOthersModel, VitalForOther>(model);
                objtosaveinvital.Staff = Utility.GetCurrentLoginUserId().ToString();
                objtosaveinvital.Department = "Ipd";
                objtosaveinvital.Status = true;
                ent.VitalForOthers.Add(objtosaveinvital);
                i = ent.SaveChanges();

            }

            return i;

        }
        public int EditIpdVital(VitalForOthersModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var objtoeditVital = ent.VitalForOthers.Where(x => x.VitalForOtherId == model.VitalForOtherId).FirstOrDefault();
                AutoMapper.Mapper.Map(model, objtoeditVital);
                objtoeditVital.Staff = Utility.GetCurrentLoginUserId().ToString();
                i = ent.SaveChanges();

            }

            return i;
        }
        public List<VitalForOthersModel> GetListOfVitalWithPatientId(int patientid)
        {

            List<VitalForOthersModel> lstOfVital = new List<VitalForOthersModel>();

            VitalForOthersModel model = new VitalForOthersModel();

            int patientlogid = Utility.getPatientLogID(patientid);

            using (EHMSEntities ent = new EHMSEntities())
            {

                lstOfVital = new List<VitalForOthersModel>(AutoMapper.Mapper.Map<IEnumerable<VitalForOther>, IEnumerable<VitalForOthersModel>>(ent.VitalForOthers.Where(x => x.OpdId == patientid && x.PatinetLogId == patientlogid && x.Status == true).ToList()));


            }
            return lstOfVital;

        }



        public List<IPDWardDetailsViewModel> GetIpdWardDetails(IpdRegistrationMasterModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<IPDWardDetailsViewModel>("IPDWardDetails '" + model.StartDate + "','" + model.EndDate + "'").ToList();
            }
        }


        //Edited by bibek nov 13
        public IpdPatientDeischargedModel GetPatientBillDetails(int IPDID)
        {
            IpdPatientDeischargedModel model = new IpdPatientDeischargedModel();

            using (EHMSEntities ent = new EHMSEntities())
            {

                //Get First patient opdId From IpdId
                model.OpdID = HospitalManagementSystem.Utility.GetPatientOpdIdFromIpdId(IPDID);
                //var result = ent.OpdMaster.Where(x => x.OpdID == model.OpdID);
                //Get Patien BedDetails
                DateTime LeaveDate = DateTime.Today;
                var BedDetails = ent.IpdPatientBedDetails.Where(x => x.IpdRegistrationId == IPDID && x.IsPaidBill == false);
                foreach (var item in BedDetails)
                {
                    if (item.LeaveDate != null)
                    {
                        LeaveDate = (DateTime)item.LeaveDate;
                    }
                    var ObjToSave = new PatientBillDetailsViewModelNew()
                    {
                        TotalAmount = item.Amount,
                        BedNo = item.BedNumber,
                        IPDId = item.IpdRegistrationId,
                        RegistrationDate = item.AdmitedDate,
                        DischargedDate = (DateTime)item.LeaveDate

                    };

                    model.PatientBillDetailsViewModelNewList.Add(ObjToSave);
                }


                return model;
            }
        }

        public bool InsertPatientBedDetailDischarge(IpdPatientDeischargedModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int BillNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);
                string BillNumberStr = "BL-" + BillNumberInt.ToString();
                //
                int paymentTypeId = model.ObjPatientBillDetailsViewModelNew.PaymentMode;
                int PaymentSubHeadId = Convert.ToInt32(0);
                if (paymentTypeId == 372)
                {

                }
                else
                {
                    paymentTypeId = 1258;//By Depo
                    PaymentSubHeadId = model.ObjPatientBillDetailsViewModelNew.AccsubgroupHeadId;
                }
                var cbPayemntDetials = new CentralizedBillingPaymentType()
                {
                    BillNo = BillNumberInt,
                    PaymentTypeID = paymentTypeId,
                    PaymentSubTypeID = PaymentSubHeadId,
                    Amount = model.ObjPatientBillDetailsViewModelNew.TotalAmount,
                    Status = true

                };
                ent.CentralizedBillingPaymentTypes.Add(cbPayemntDetials);

                var CBmaster = new CentralizedBillingMaster()
                {
                    BillNo = BillNumberInt,
                    BillDate = DateTime.Now,
                    //TotalBillAmount = model.ObjPatientBillDetailsViewModelNew.TotalAmount,
                    TotalBillAmount = model.ObjPatientBillDetailsViewModelNew.TotalAmount,
                    TotalDiscountID = 0,
                    TotalDiscountAmount = Convert.ToDecimal(0),
                    Narration1 = "Narration",
                    Narration2 = "BedAmount",
                    DepartmentName = "Ipd",
                    SubDepartmentId = 1,
                    PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.OpdID),
                    PatientId = model.OpdID,
                    JVStatus = false,
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    CreatedDepartmentId = 0,
                    Remarks = "IPDBedDetails",
                    Status = true,
                    BranchId = 1,
                    ReceiptId = 0,
                    IsHandover = false,
                    TenderAmount = model.ObjPatientBillDetailsViewModelNew.TenderAmount,
                    ReturnedAmount = model.ObjPatientBillDetailsViewModelNew.ReturnedAmount

                };

                ent.CentralizedBillingMasters.Add(CBmaster);

                var cbdetails = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 326,//Bed Charge
                    //Amount = model.ObjPatientBillDetailsViewModelNew.TotalAmount,
                    Amount = model.ObjPatientBillDetailsViewModelNew.AmountWithOutTax,
                    Status = true,
                    DepartmentId = 1003,
                    Times = 1,
                    Narration2 = Convert.ToString(model.ObjPatientBillDetailsViewModelNew.TotalAmount)
                };
                ent.CentralizedBillingDetails.Add(cbdetails);
                var cbdetailstax = new CentralizedBillingDetail()
                {
                    BillNo = BillNumberInt,
                    AccountHeadID = 1261,//tax
                    Amount = model.ObjPatientBillDetailsViewModelNew.TaxAmountOnly,
                    Status = true,
                    DepartmentId = 1003,
                    Times = 1,
                    //Narration2 = Convert.ToString(model.ObjPatientBillDetailsViewModelNew.TotalAmount)
                };

                ent.CentralizedBillingDetails.Add(cbdetailstax);

                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == "Hospital" && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;
                ent.SaveChanges();

                //update IPDBedDetails
                var IPDBedDetails = (from x in ent.IpdPatientBedDetails
                                     where x.IpdRegistrationId == model.IpdRegistrationID && x.OpdEmergencyNumber == model.OpdID
                                     select x);
                foreach (IpdPatientBedDetail ho in IPDBedDetails)
                {
                    ho.IsPaidBill = true;
                }

                ent.SaveChanges();


            }
            return true;
        }

        public bool InsertIPDShiftDetails(IpdRegistrationMasterModel model)
        {

            //model.ObjShiftFromIPDViewModel = new ShiftFromIPDViewModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjShiftDetails = new ShiftFromIPDDepartment()
                {
                    OpdID = model.OpdNoEmergencyNo,
                    IPDRegistrationID = model.IpdRegistrationID,
                    ShiftedDepartmentId = model.ObjShiftFromIPDViewModel.ShiftedDepartmentId,
                    Status = true,
                    ShiftedDate = DateTime.Now,
                    CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Now,
                    Remarks = model.ObjShiftFromIPDViewModel.Remarks,
                    Narration = "",
                    DepartmentID = 1,

                };
                ent.ShiftFromIPDDepartments.Add(ObjShiftDetails);
                ent.SaveChanges();
            }

            return true;
        }

        public bool IsPatientAlreadyShiftedFromIPD(int OpdId, int IPDRegistrationID)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var Result = ent.ShiftFromIPDDepartments.Where(x => x.OpdID == OpdId && x.IPDRegistrationID == IPDRegistrationID && x.Status == true);
                if (Result.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public IpdRegistrationMasterModel GetIpdShiftDetailsOfPatient(int OpdId, int IPDRegistraionId)
        {
            IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();
            model.ObjShiftFromIPDViewModel = new ShiftFromIPDViewModel();

            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.ShiftFromIPDDepartments.OrderByDescending(x => x.ShiftFromIPDId).Where(x => x.OpdID == OpdId && x.IPDRegistrationID == IPDRegistraionId && x.Status == true);
                if (result.Count() > 0)
                {

                    var Output = result.FirstOrDefault();

                    model.ObjShiftFromIPDViewModel.ShiftedDepartmentId = Output.ShiftedDepartmentId;
                    model.ObjShiftFromIPDViewModel.Remarks = Output.Remarks;
                    model.ObjShiftFromIPDViewModel.ShiftedDate = Output.ShiftedDate;
                }
                return model;

            }

        }


        public int GetMaxIpdRegistrationNumber()
        {

            return 0;
        }





    }


}






