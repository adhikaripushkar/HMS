using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Providers
{
    public class DepositMasterProvider
    {

        public List<DepositMasterModel> GetAll()
        {
            EHMSEntities ent = new EHMSEntities();
            return new List<DepositMasterModel>(AutoMapper.Mapper.Map<IEnumerable<PatientDepositMaster>, IEnumerable<DepositMasterModel>>(ent.PatientDepositMasters).Take(100));
        }

        public int Insert(DepositMasterModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                //Insert into CA Table
                //If User already exist, dont insert
                //check if user has alreary accountheadcode in opdmaster
                //Get Currnet receipt number
                int UserReceiptNumber = Utility.GetMaxBillNumberFromDepartment("Deposit", 1);
                int UserAccountHeadIdInt = 0;
                var userAccountHeadId = ent.OpdMasters.Where(x => x.OpdID == model.PatientId).FirstOrDefault();
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
                        AccSubGroupName = HospitalManagementSystem.Utility.GetPatientNameWithIdFromOpd(model.PatientId),
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
                                           where x.OpdID == model.PatientId
                                           select x).First();
                AccountHeadId.AccountHeadId = UserAccountHeadIdInt;

                var SaveDepositMaster = AutoMapper.Mapper.Map<DepositMasterModel, PatientDepositMaster>(model);
                SaveDepositMaster.CreatedBy = Utility.GetCurrentLoginUserId();
                SaveDepositMaster.CreatedDate = DateTime.Now;
                SaveDepositMaster.CreatedDepartmentId = Utility.GetCurrentUserDepartmentId();
                SaveDepositMaster.Status = true;
                SaveDepositMaster.SwipeCardId = "NN009";
                SaveDepositMaster.ReceiptID = UserReceiptNumber;
                SaveDepositMaster.SwipeCardDetail = "ScardDetails";

                ent.PatientDepositMasters.Add(SaveDepositMaster);

                //Get Currrent Receipt Number
                int ReceiptNumberInt = Utility.GetMaxBillNumberFromDepartment("Hospital", 1);


                var ObjCentralizedBillingMaster = new CentralizedBillingMaster()
                {
                    BillNo = ReceiptNumberInt,
                    BillDate = DateTime.Now,
                    TotalBillAmount = model.DepositedAmount,
                    Narration1 = "Deposit",
                    Narration2 = "",
                    DepartmentName = "CB",
                    SubDepartmentId = 1,
                    PatientLogId = HospitalManagementSystem.Utility.getPatientLogID(model.PatientId),
                    PatientId = model.PatientId,
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
                    PayableType = 1//general

                };

                ent.CentralizedBillingMasters.Add(ObjCentralizedBillingMaster);

                var ObjCentralizedBillingDetails = new CentralizedBillingDetail()
                {
                    BillNo = ReceiptNumberInt,
                    AccountHeadID = 1258,//deposit
                    AccountSubHeadID = UserAccountHeadIdInt,//patientId
                    Amount = model.DepositedAmount,
                    Status = true,
                    DepartmentId = 1003//Ipd Department
                };
                ent.CentralizedBillingDetails.Add(ObjCentralizedBillingDetails);

                var ObjCentralizedBillingPaymentType = new CentralizedBillingPaymentType()
                {
                    BillNo = ReceiptNumberInt,
                    PaymentTypeID = 372,
                    Amount = model.DepositedAmount,
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

                return SaveDepositMaster.PatientDepositMasterId;
            }
        }

        public int Update(DepositMasterModel model)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var objToEdit = ent.PatientDepositMasters.Where(x => x.PatientDepositMasterId == model.PatientDepositMasterId).FirstOrDefault();

            AutoMapper.Mapper.Map(model, objToEdit);
            objToEdit.Status = true;
            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            i = ent.SaveChanges();

            return i;

        }
        //dont delete data just update status
        public int Delete(int id)
        {
            int i = 1;
            using (EHMSEntities ent = new EHMSEntities())
            {
                //var objToDelete = ent.PatientDepositMaster.Where(x => x.PatientDepositMasterId == id).FirstOrDefault();
                //ent.PatientDepositMaster.Remove(objToDelete);
                //i = ent.SaveChanges();

                var cust = (from c in ent.PatientDepositMasters
                            where c.PatientDepositMasterId == id
                            select c).First();
                cust.Status = false;
                ent.SaveChanges();

            }


            return i;
        }



        public List<PatientPartialDetails> GetDepositForPatientEmergency(int EmergencyId, string Name)
        {
            EHMSEntities ent = new EHMSEntities();
            string name = Name.Trim();
            List<PatientPartialDetails> PatientDetailList = new List<PatientPartialDetails>();
            List<PatientPartialDetails> PatientDetailList1 = new List<PatientPartialDetails>();
            if (EmergencyId == 0 && name == "")
            {

                var data = ent.OpdMasters.Take(20).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientDepartmentId = 1001;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1001;
                    obj.Sex = item.Sex;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;

            }

            else if (EmergencyId == 0 && name != "")
            {
                OpdMaster a = new OpdMaster();
                var dataName = ent.OpdMasters.Take(20).ToList();
                foreach (var item in dataName)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientDepartmentId = 1001;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;

                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1001;
                    obj.Sex = item.Sex;
                    PatientDetailList1.Add(obj);
                }



                var data = PatientDetailList1.Where(x => x.PatientFullName.Contains(name)).ToList();

                foreach (var item in data)
                {

                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = obj.PatientId;
                    obj.PatientDepartmentId = 1001;
                    obj.PatientFullName = item.PatientFullName;
                    obj.Age = item.Age;
                    obj.DepartmentID = 1001;
                    obj.Sex = item.Sex;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;


            }
            else if (EmergencyId != 0 && name == "")
            {
                var data = ent.OpdMasters.Where(x => x.OpdID == EmergencyId).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientDepartmentId = 1001;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1001;
                    obj.Sex = item.Sex;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;
            }
            else
            {
                var data = ent.OpdMasters.Where(x => x.OpdID == EmergencyId).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientDepartmentId = 1001;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1001;
                    obj.Sex = item.Sex;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;

                //return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
            }
        }

        public List<PatientPartialDetails> SearchOPDFromAddressDeposit(string address)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.OpdMasters.Where(x => x.Address.Contains(address)).ToList();
            List<PatientPartialDetails> PatientDetailList = new List<PatientPartialDetails>();
            foreach (var item in data)
            {
                PatientPartialDetails obj = new PatientPartialDetails();
                obj.PatientId = item.OpdID;
                obj.PatientDepartmentId = item.DepartmentId;
                obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                obj.Age = item.AgeYear;
                obj.DepartmentID = 1000;
                obj.Sex = item.Sex;
                PatientDetailList.Add(obj);
            }

            return PatientDetailList;
        }

        public List<PatientPartialDetails> SearchEmergencyFromAddressDeposit(string address)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.OpdMasters.Where(x => x.Address.Contains(address)).ToList();
            List<PatientPartialDetails> PatientDetailList = new List<PatientPartialDetails>();
            foreach (var item in data)
            {
                PatientPartialDetails obj = new PatientPartialDetails();
                obj.PatientId = item.OpdID;
                obj.PatientDepartmentId = 1001;
                obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                obj.Age = item.AgeYear;
                obj.DepartmentID = 1001;
                obj.Sex = item.Sex;
                PatientDetailList.Add(obj);
            }

            return PatientDetailList;
        }

        public List<PatientPartialDetails> SearchOPDFromPhoneDeposit(string address)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.OpdMasters.Where(x => x.ContactName.Contains(address)).ToList();
            List<PatientPartialDetails> PatientDetailList = new List<PatientPartialDetails>();
            foreach (var item in data)
            {
                PatientPartialDetails obj = new PatientPartialDetails();
                obj.PatientId = item.OpdID;
                obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                obj.Age = item.AgeYear;
                obj.PatientDepartmentId = obj.DepartmentID;
                obj.DepartmentID = 1000;
                obj.Sex = item.Sex;
                PatientDetailList.Add(obj);
            }

            return PatientDetailList;
        }




        public List<PatientPartialDetails> GetDepositForPatient(int opdId, string Name)
        {
            EHMSEntities ent = new EHMSEntities();
            string name = Name.Trim();
            List<PatientPartialDetails> PatientDetailList = new List<PatientPartialDetails>();

            if (opdId == 0 && name == "")
            {

                var data = ent.OpdMasters.Take(20).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1000;
                    obj.PatientDepartmentId = item.DepartmentId;
                    obj.Sex = item.Sex;
                    obj.Address = item.Address;
                    obj.ContactNumber = item.ContactName;
                    obj.RegistrationDate = (DateTime)item.RegistrationDate;

                    PatientDetailList.Add(obj);

                }

                return PatientDetailList;

            }

            else if (opdId == 0 && name != "")
            {
                var data = ent.OpdMasters.Where(x => x.FirstName.StartsWith(name)).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1000;
                    obj.PatientDepartmentId = item.DepartmentId;
                    obj.Sex = item.Sex;
                    obj.Address = item.Address;
                    obj.ContactNumber = item.ContactName;
                    obj.RegistrationDate = (DateTime)item.RegistrationDate;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;


            }
            else if (opdId != 0 && name == "")
            {
                var data = ent.OpdMasters.Where(x => x.OpdID == opdId).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1000;
                    obj.PatientDepartmentId = item.DepartmentId;
                    obj.Sex = item.Sex;
                    obj.Address = item.Address;
                    obj.ContactNumber = item.ContactName;
                    obj.RegistrationDate = (DateTime)item.RegistrationDate;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;
            }
            else
            {
                var data = ent.OpdMasters.Where(x => x.OpdID == opdId).ToList();
                foreach (var item in data)
                {
                    PatientPartialDetails obj = new PatientPartialDetails();
                    obj.PatientId = item.OpdID;
                    obj.PatientFullName = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
                    obj.Age = item.AgeYear;
                    obj.DepartmentID = 1000;
                    obj.PatientDepartmentId = item.DepartmentId;
                    obj.Sex = item.Sex;
                    obj.Address = item.Address;
                    obj.ContactNumber = item.ContactName;
                    obj.RegistrationDate = (DateTime)item.RegistrationDate;
                    PatientDetailList.Add(obj);
                }

                return PatientDetailList;

                //return new List<OpdModel>(AutoMapper.Mapper.Map<IEnumerable<OpdMaster>, IEnumerable<OpdModel>>(query)).ToList();
            }


        }

        public string GetPatientFullName(int departmentId, int patientID)
        {
            string PatientFullName = string.Empty;
            EHMSEntities ent = new EHMSEntities();
            if (departmentId == 1000)
            {
                var fullname = (from a in ent.OpdMasters
                                where a.OpdID == patientID
                                select new
                                {
                                    a.FirstName,
                                    a.MiddleName,
                                    a.LastName
                                }).SingleOrDefault();
                if (fullname != null)
                {
                    return fullname.FirstName + " " + fullname.MiddleName + " " + fullname.LastName;
                }
                else
                {

                    return "";
                }

            }
            else
            {
                var fullname = (from a in ent.OpdMasters
                                where a.OpdID == patientID
                                select new
                                {
                                    a.FirstName,
                                    a.MiddleName,
                                    a.LastName
                                }).SingleOrDefault();
                if (fullname != null)
                {
                    return fullname.FirstName + " " + fullname.MiddleName + " " + fullname.LastName;
                }
                else
                {

                    return "";
                }
            }

        }
        public int GetPatientDepartmentId(int PatientId)
        {
            //int PatientDepartmentId = 0;
            EHMSEntities ent = new EHMSEntities();
            var result = ent.OpdMasters.Where(x => x.OpdID == PatientId).Select(x => x.DepartmentId).FirstOrDefault();
            return Convert.ToInt32(result);
        }
    }
}