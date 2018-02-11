using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using System.Data.Entity.Core.Objects;

namespace HospitalManagementSystem.Providers
{
    public class BillingProvider
    {

        public List<CentralizeBillingViewModel> GetListForIndex(DateTime startdate, DateTime Enddate)
        {
            List<BillingModel> model = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.Database.SqlQuery<CentralizeBillingViewModel>("GetAllTransactionByDeptId '0','" + startdate + "','" + Enddate + "'").ToList();
                return ent.Database.SqlQuery<CentralizeBillingViewModel>("GetAllBillDetailsByDate '" + startdate + "','" + Enddate + "'").ToList();
            }

        }

        public List<CentralizeBillingViewModel> SearchById(int id)
        {
            string dateInString = "01.10.2000";
            string dateInStringEnd = "01.10.3000";
            DateTime startdate = DateTime.Parse(dateInString);
            DateTime EndDate = DateTime.Parse(dateInStringEnd);
            //DateTime startdate = DateTime.Now
            List<BillingModel> model = new List<BillingModel>();
            string name = "Default";
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<CentralizeBillingViewModel>("CentralizeBillingDetail '" + startdate + "','" + EndDate + "', '" + id + "', 'opd', '" + name + "'").ToList();
            }
        }
        //{
        //public List<BillingModel> SearchById(int id)
        //{
        //    List<BillingModel> models = new List<BillingModel>();
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var result = (from a in ent.OpdMaster
        //                      join b in ent.GL_Transaction on a.OpdID equals b.RefNo
        //                      join c in ent.GL_LedgerMaster on b.RefNo equals c.SourceID
        //                      where a.OpdID == id && c.SourceID == id
        //                      select new BillingModel
        //                          {
        //                              Narration1 = b.Narration1,
        //                              TransactionDate = b.TransactionDate,
        //                              LedgerName = c.LedgerName,
        //                              DepartmentID = b.DepartmentID,
        //                              Amount = b.Amount,
        //                              Name = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
        //                              OpdId = a.OpdID
        //                              //  string LedgerName = "A/C " + model.FirstName + " " + (model.MiddleName + " " ?? string.Empty) + model.LastName;
        //                          }).ToList();

        //        foreach (var item in result)
        //        {
        //            models.Add(item);

        //        }

        //    }
        //    return models;

        //}

        public List<CentralizeBillingViewModel> SearchByName(string name)
        {
            string dateInString = "01.10.2000";
            string dateInStringEnd = "01.10.3000";
            DateTime startdate = DateTime.Parse(dateInString);
            DateTime EndDate = DateTime.Parse(dateInStringEnd);
            //DateTime startdate = DateTime.Now
            List<BillingModel> model = new List<BillingModel>();
            //string name = "";
            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.Database.SqlQuery<CentralizeBillingViewModel>("CentralizeBillingDetail '" + startdate + "','" + EndDate + "', 0, 'Opd', '" + name + "'").ToList();

            }
        }

        public List<BillingModel> SearchByNameOld(string name)
        {
            List<BillingModel> models = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = (from a in ent.OpdMasters
                              join b in ent.GL_Transaction on a.OpdID equals b.RefNo
                              join c in ent.GL_LedgerMaster on b.RefNo equals c.SourceID
                              where a.FirstName == name || a.MiddleName == name || a.LastName == name
                              select new BillingModel
                              {
                                  OpdId = a.OpdID,
                                  Narration1 = b.Narration1,
                                  LedgerName = c.LedgerName,
                                  DepartmentID = b.DepartmentID,
                                  Amount = b.Amount,
                                  Name = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
                                  TransactionDate = b.TransactionDate


                              }).ToList();

                foreach (var item in result)
                {
                    models.Add(item);

                }

            }

            return models;
        }

        public List<CentralizeBillingViewModel> SearchByDepartmentID(int DepartmentID, DateTime startDate, DateTime endTime)
        {
            EHMSEntities ent = new EHMSEntities();
            return ent.Database.SqlQuery<CentralizeBillingViewModel>("GetAllTransactionByDeptId '" + DepartmentID + "', '" + startDate + "', '" + endTime + "'").ToList();


            //List<BillingModel> BillingModelList = new List<BillingModel>();
            //using (EHMSEntities ent = new EHMSEntities())
            //{

            //    var DetailsData = from i in
            //                          (from d in ent.OpdMaster
            //                           join e in ent.GL_LedgerMaster on d.OpdID equals e.SourceID
            //                           join f in ent.CentralizedBilling on d.OpdID equals f.PatientId
            //                           where d.DepartmentId == DepartmentID && e.DepartmentID == DepartmentID && f.SubDepartmentId == DepartmentID
            //                           select new { d.DepartmentId, d.OpdID, d.FirstName, d.MiddleName, d.LastName, f.Narration1, f.Amount, e.LedgerName, f.PatientLogId, e.LedgerMasterID, f.AmountDate })
            //                      group i by new { i.PatientLogId, i.LedgerMasterID } into g

            //                      select new BillingModel
            //                      {
            //                          BillingID = g.Key.PatientLogId,
            //                          PainName = g.FirstOrDefault().FirstName,
            //                          //Narration1 = g.FirstOrDefault().Narration1,
            //                          Narration1 = "Fee",
            //                          LedgerName = g.FirstOrDefault().LedgerName,
            //                          Amount = g.Sum(t => t.Amount),
            //                          TransectionDate = g.FirstOrDefault().AmountDate,
            //                          PatientLogId = g.Key.PatientLogId,
            //                          OpdId = g.FirstOrDefault().OpdID,
            //                          DepartmentID = (int)g.FirstOrDefault().DepartmentId,
            //                          Name = g.FirstOrDefault().FirstName + " " + (g.FirstOrDefault().MiddleName + " " ?? string.Empty) + g.FirstOrDefault().LastName,


            //                      };



            //    //var result = (from d in ent.OpdMaster
            //    //              join e in ent.GL_Transaction on d.OpdID equals e.RefNo
            //    //              join f in ent.GL_LedgerMaster on e.RefNo equals f.SourceID
            //    //              where d.DepartmentId == DepartmentID && e.DepartmentID == DepartmentID && f.DepartmentID == DepartmentID

            //    //              select new BillingModel
            //    //              {
            //    //                  OpdId = d.OpdID,
            //    //                  Narration1 = e.Narration1,
            //    //                  LedgerName = f.LedgerName,
            //    //                  DepartmentID = e.DepartmentID,
            //    //                  Amount = e.Amount,
            //    //                  TransactionDate = e.TransactionDate,
            //    //                  Name = d.FirstName + " " + (d.MiddleName + " " ?? string.Empty) + d.LastName,



            //    //              }).ToList();
            //    foreach (var item in DetailsData)
            //    {
            //        BillingModelList.Add(item);

            //    }


            //    return BillingModelList;

        }


        public List<BillingModel> BillingModelList(int FeeTypeId, BillingModel model)
        {
            List<BillingModel> BillingModelList = new List<BillingModel>();
            if (FeeTypeId == 19 || FeeTypeId == 20)
            {
                using (EHMSEntities ent = new EHMSEntities())
                {

                    var result = from a in ent.OpdMasters
                                 join b in ent.CentralizedBillings on a.OpdID equals b.PatientId
                                 where b.AccountHeadId == FeeTypeId && b.AmountDate >= model.TransactionDate && b.AmountDate <= model.EndTransactionDate

                                 select new BillingModel
                                 {

                                     OpdId = a.OpdID,
                                     Narration1 = b.Narration1,
                                     LedgerName = "",
                                     DepartmentID = b.SubDepartmentId,
                                     Amount = b.Amount,
                                     Name = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
                                     TransactionDate = b.AmountDate

                                 };
                    foreach (var item in result)
                    {
                        BillingModelList.Add(item);

                    }
                }


            }
            else
            {
                using (EHMSEntities ent = new EHMSEntities())
                {

                    var result = from a in ent.OpdMasters
                                 join b in ent.CentralizedBillings on a.OpdID equals b.PatientId
                                 where b.AccountHeadId == FeeTypeId && b.AmountDate >= model.TransactionDate && b.AmountDate <= model.EndTransactionDate

                                 select new BillingModel
                                 {

                                     OpdId = a.OpdID,
                                     Narration1 = b.Narration1,
                                     LedgerName = "",
                                     DepartmentID = b.SubDepartmentId,
                                     Amount = b.Amount,
                                     Name = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
                                     TransactionDate = b.AmountDate

                                 };

                    foreach (var item in result)
                    {
                        BillingModelList.Add(item);

                    }

                }


            }

            return BillingModelList;
        }
        public List<BillingModel> BillingListForRoom(int RoomType)
        {
            List<BillingModel> BillingListForRoom = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = (from a in ent.IpdPatientroomDetails
                              join b in ent.OpdMasters on a.OpdNoEmergencyNo equals b.OpdID

                              join c in ent.GL_Transaction on b.OpdID equals c.RefNo
                              join d in ent.GL_LedgerMaster on c.RefNo equals d.SourceID
                              join e in ent.OpdMasters on d.SourceID equals e.OpdID

                              where a.RoomType == RoomType

                              select new BillingModel
                              {

                                  OpdId = e.OpdID,
                                  Narration1 = c.Narration1,
                                  LedgerName = d.LedgerName,
                                  DepartmentID = (int)c.DepartmentID,
                                  Amount = c.Amount,
                                  Name = e.FirstName + " " + (e.MiddleName + " " ?? string.Empty) + e.LastName,
                                  TransactionDate = c.TransactionDate

                              }).Distinct().ToList();
                foreach (var item in result)
                {

                    BillingListForRoom.Add(item);

                }

                return BillingListForRoom;

            }

        }
        public List<BillingModel> SearchTodayTransaction(DateTime startdate)
        {
            List<BillingModel> model = new List<BillingModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {

                var result = (from b in ent.CentralizedBillings
                              join a in ent.GL_LedgerMaster on b.PatientId equals a.SourceID
                              join e in ent.OpdMasters on a.SourceID equals e.OpdID
                              where EntityFunctions.TruncateTime(b.AmountDate) == startdate
                              select new BillingModel
                              {
                                  OpdId = e.OpdID,
                                  Name = e.FirstName + " " + (e.MiddleName + " " ?? string.Empty) + e.LastName,
                                  LedgerName = a.LedgerName,
                                  Amount = b.Amount,
                                  DepartmentID = b.SubDepartmentId,
                                  Narration1 = b.Narration1,
                                  TransactionDate = b.AmountDate

                              }).ToList();


                foreach (var item in result)
                {
                    model.Add(item);
                }

                return model;

            }

        }
        public BillingModel GetAllDischargeBillSummary(int PatientLogId)
        { 
            BillingModel model = new BillingModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                //model.DischargeBillingDepositeList = ent.Database.SqlQuery<CentralizeBillingViewModel>("DischargeBillSummary '" + PatientLogId + "', '""++'").ToList();
            }
            return model;
        }

        public List<GetBillDetailByBillNoViewModel> GetAllBillDetailsByBillNumber(int billNumber)
        {
            
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.Database.SqlQuery<CentralizeBillingViewModel>("GetAllTransactionByDeptId '0','" + startdate + "','" + Enddate + "'").ToList();
                return ent.Database.SqlQuery<GetBillDetailByBillNoViewModel>("GetBillDetailsByBillNo "+billNumber).ToList();
            }

        }

        public List<DepositUsedRemainViewModel> GetDepositBalanceUsed()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<DepositUsedRemainViewModel>("GetBanlaceRemainingDepositDetails").ToList();
            }
        }

        public BillingModel GetTransacationDetailsById(int PatientId)
        {
            BillingModel model = new BillingModel();

            using (EHMSEntities ent = new EHMSEntities())
            {
                int PatienLogId = HospitalManagementSystem.Utility.getPatientLogID(PatientId);
                var TranByDepo = (from CBM in ent.CentralizedBillingMasters
                                  join CBD in ent.CentralizedBillingDetails
                                  on CBM.BillNo equals CBD.BillNo
                                  where CBM.PatientLogId == PatienLogId && CBM.Narration1 != "Deposit" && CBM.Narration2 == "ByDeposit"
                                  select new
                                  {
                                      CBM.PatientLogId,
                                      CBD.AccountHeadID,
                                      CBD.AccountSubHeadID,
                                      CBD.Amount,
                                      CBM.BillDate
                                  }).ToList();
                foreach (var item in TranByDepo)
                {
                    var ViewmodelByDepo = new DepositUsedViewModel()
                    {
                        DepoParticulars = HospitalManagementSystem.Utility.GetFeeTypeNameFromId(item.AccountHeadID),
                        DepoBillDate = item.BillDate,
                        DepoBillAmount = item.Amount


                    };
                    model.DepositUsedViewModelList.Add(ViewmodelByDepo);
                    
                }
                
             
            }
            return model;
        }


        public BillingModel GetPatientsDetailsByIdAndName(int TypeOfsearch, string SearchV)
        {
            BillingModel model = new BillingModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                switch (TypeOfsearch)
                {
                    case 1:
                        int OpdId = Convert.ToInt32(SearchV);
                        var result = ent.OpdMasters.Where(x => x.OpdID == OpdId);
                        foreach (var item in result)
                        {
                            var Viewmodel = new CentralizeBillingViewModel()
                            {
                                LedgerName = Utility.GetPatientNameWithIdFromOpd(item.OpdID),//PatientName
                                PatientId = item.OpdID,
                                Age = (int)item.AgeYear,
                                Narration1 = item.Address//address

                            };

                            model.CentralizeBillingViewModelList.Add(Viewmodel);

                        }


                        break;
                    case 2:

                        var resultName = ent.OpdMasters.Where(x => x.FirstName.Contains(SearchV));
                        foreach (var item in resultName)
                        {
                            var Viewmodel = new CentralizeBillingViewModel()
                            {
                                LedgerName = Utility.GetPatientNameWithIdFromOpd(item.OpdID),
                                PatientId = item.OpdID,
                                Age = (int)item.AgeYear,
                                Narration1 = item.Address

                            };

                            model.CentralizeBillingViewModelList.Add(Viewmodel);

                        }
                        break;
                    default:
                        break;
                }
                return model;
            }
        }







    }
}