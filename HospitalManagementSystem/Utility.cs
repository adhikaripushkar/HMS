using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HospitalManagementSystem;
using HospitalManagementSystem.Models;
using System.Net.Mail;
using System.Net;
using WebMatrix.WebData;

namespace HospitalManagementSystem
{
    public class Utility
    {

        public string success = "Record Created Successfully !";
        public static string GetBlockNameWithID(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                int i = ent.ItemBlockSetups.Where(x => x.ItemBlockSetupID == id).SingleOrDefault().BlockId;
                string str = GetBlockName(i);
                return str;

            }

        }

        public static int GetCurrentFiscalYearID()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string value = "Y";
                var result = from b in ent.SetupFiscalYears
                             where b.IsCurrent == value
                             select b.FiscalYearId;
                var Fyid = result.FirstOrDefault();
                return Fyid;
            }
        }


        public static string GetCurrentFiscalYearNameInBS()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string value = "Y";
                var result = from b in ent.SetupFiscalYears
                             where b.IsCurrent == value
                             select b.FiscalYearBS;
                var Fyid = result.FirstOrDefault();
                return Fyid;
            }
        }

        public static IEnumerable<SelectListItem> GetCOAHeadList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.GL_AccSubGroups.Where(x => x.IsLeafLevel == true).ToList(), "AccSubGruupID", "AccSubGroupName");
            }
        }


        public static IEnumerable<SelectListItem> GetEmployeeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupEmployeeMasters.ToList(), "EmployeeMasterId", "EmployeeFullName");
            }
        }


        public static IEnumerable<SelectListItem> GetStockCategoryList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockCategories.Where(x => x.Status == true).ToList(), "StockCategoryID", "StockCategoryName");
            }

        }

        public static IEnumerable<SelectListItem> GetDiagosisList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupDiagnosis.Where(m => m.Status == true).ToList(), "DiagnosisId", "DiagnosisName");
            }
        }


        public static string DepartmentNameForOpd(int opdid)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = (from d in ent.SetupDepartments
                           join o in ent.OpdPatientDoctorDetails on d.DeptID equals o.DepartmentID
                           where o.OpdID == opdid
                           select new { d.DeptName }).FirstOrDefault();

                if (obj != null)
                {
                    return obj.DeptName;
                }
                return "";
            }

        }


        public static string GetFullName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var FullName = (from a in ent.PreRegistrations
                                where a.PreRegistrationID == id
                                select new
                                {
                                    a.FirstName,
                                    a.MiddleName,
                                    a.LastName
                                }).SingleOrDefault();
                return FullName.FirstName + " " + FullName.MiddleName + " " + FullName.LastName;
            }
        }

        public static string FullName(string FirstName, string MiddleName, string LastName)
        {

            return (FirstName + " " + (MiddleName + " " ?? string.Empty) + LastName);

        }
        public static IEnumerable<SelectListItem> GetZoneListSearch()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.TblZones.ToList(), "ZoneID", "ZoneInEng");
            }
        }

        public static IEnumerable<SelectListItem> GetDistrictSearch()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.TblDistricts.ToList(), "DistrictID", "DistrictEng");
            }
        }
        public static IEnumerable<SelectListItem> GetVDCSearch()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.VDCMUNs.ToList(), "VdcMunID", "VdcMunNameEng");
            }
        }

        public static IEnumerable<SelectListItem> GetDepartmentList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupDepartments.ToList(), "DeptID", "DeptName");
            }

        }

        public static IEnumerable<SelectListItem> GetPrimaryDepartmentList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupDepartments.Where(x => x.AssociateDepartmentID == 1001).ToList(), "DeptID", "DeptName");
            }

        }

        //Edited by bibek march 09
        public static IEnumerable<SelectListItem> GetDepartmentListBilling()
        {
            return new SelectList(new[]
            {
                new {Id="all",Value="---ALL-"},
                new {Id="Opd",Value="Opd"},
                new {Id="Patho",Value="Pathology"},
                new {Id="Ipd",Value="Ipd"},
                new {Id="Emergency",Value="Emergency"},
                new {Id="Ot",Value="O.Theatre"},
                new {Id="Radio",Value="Radiology"}

            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> GetPaymentMode()
        {
            return new SelectList(new[]
            {

                new {Id="372",Value="Cash"},
                //new {Id="689",Value="Bank"},
                new{Id="315",Value="Deposit"}


            }, "Id", "Value");


        }

        public static IEnumerable<SelectListItem> GetPaymentModeForStock()
        {
            return new SelectList(new[]
            {
                new {Id="372",Value="Credit"},
                new {Id="373",Value="Cash"},
                new {Id="374",Value="Bank"}
                //new{Id="315",Value="Deposit"}
            
            
            }, "Id", "Value");


        }

        //IPD SHIFT Details

        public static IEnumerable<SelectListItem> IpdDischargedCondition()
        {
            return new SelectList(new[]
            {
                new {Id="900",Value="Recovered"},
                new {Id="901",Value="Improved"},
                new {Id="902",Value="Worse"},
                new {Id="903",Value="LAMA"},
                new {Id="904",Value="Abscended"},
                new {Id="905",Value="Death"},
                new {Id="906",Value="Coma"},

            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetShiftListFromIPD()
        {
            return new SelectList(new[]
            {
                new {Id="900",Value="LAMA"},
                new {Id="901",Value="DAMA"},
                new {Id="902",Value="ICU"},
                new {Id="903",Value="CCU"},
                new {Id="904",Value="SURGERY"},
                new {Id="905",Value="DEATH"},
                new {Id="906",Value="COMA"},

            }, "Id", "Value");

        }





        //Edited by bibek march 09
        public static IEnumerable<SelectListItem> GetDepartmentListForPathology()
        {
            return new SelectList(new[]
            {
                new {Id="1006",Value="PATHOLOGY"},
                new {Id="1000",Value="OPD"},
                //new {Id="1001",Value="EMERGENCY"},
                //new {Id="1003",Value="IPD"}, 
                new {Id="1005",Value="SURGERY"},
                new {Id="1004",Value="OTHERS"},
                new {Id="1007",Value="DENTAL"},
                new {Id="1008",Value="EYE"},
                new {Id="1009",Value="PHYSIOTHERAPY"},
                new {Id="1010",Value="ENT"},


            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetMemberType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupMemberTypes.Where(x => x.Status.Equals("A") && ent.SetupMemberDiscounts.Any(y => y.MemberShipType == x.MemberTypeID && y.Status.Equals("A")) == false).ToList(), "MemberTypeID", "MemberTypeName");
            }

        }

        public static IEnumerable<SelectListItem> GetAllMemberType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupMemberTypes.ToList(), "MemberTypeID", "MemberTypeName");
            }

        }

        public static string MemberTypeName(int id)
        {

            string str = "";

            using (EHMSEntities ent = new EHMSEntities())
            {

                if (id == 0)
                {
                    return str;
                }
                try
                {

                    return ent.SetupMemberTypes.Where(x => x.MemberTypeID == id).SingleOrDefault().MemberTypeName;
                }
                catch
                {

                    return "";

                }

            }
        }


        public static int GetMaxDependent(int id)
        {


            using (EHMSEntities ent = new EHMSEntities())
            {

                var maxval = ent.SetupMemberShips.Where(x => x.MaximumDependent == id).Select(x => x.MaximumDependent).SingleOrDefault();

                if (maxval > 0)
                {
                    return maxval;
                }
                else
                {

                    return 0;
                }
            }


        }

        public static string GetDateOnly(string date)
        {
            return date.Substring(0, date.IndexOf(' '));
        }



        public static bool isExists(decimal DeptID, int DoctorID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.SetupDoctorDepartments.Where(x => x.DepartmentID == DeptID && x.DoctorID == DoctorID).Any();


            }

        }


        public static IEnumerable<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>
                       {
                         new SelectListItem {Text = "Male", Value = "Male"},
                         new SelectListItem {Text = "Female", Value = "Female"},
                         new SelectListItem {Text = "Other", Value = "Other"}
                       };

        }

        //from Gopal sir ,date on 11/20/2013

        public static IEnumerable<SelectListItem> RegistrationModeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Visit", Value="Visit"},
                new SelectListItem{Text="Phone",Value="Phone"},
                new SelectListItem{Text="Web", Value="Web"}
            };
        }
        public static IEnumerable<SelectListItem> MatitalStatusList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text ="Married",Value="Married"},
                new SelectListItem{Text="Unmarried",Value="Unmarried"}
            };
        }

        public static IEnumerable<SelectListItem> MemberTypeList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text ="General",Value="1"}
            };
        }

        public static IEnumerable<SelectListItem> BloodGroupList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupBloodGroups.ToList(), "BloodGroupID", "BloodGroup");
            }

        }
        //Edited By Pushkar
        public static IEnumerable<SelectListItem> GetAgeRange()
        {
            return new SelectList(new[]{
            new{Id="0", Value="Select Age Range"},
            new{Id="1",Value="0-9"},
            new{Id="2",Value="10-19"},
            new{Id="3",Value="20-29"},
            new{Id="4",Value="30-39"},
            new{Id="5",Value="40-49"},
            new{Id="6",Value="50-59"},
            new{Id="7",Value="60-69"},
            new{Id="8",Value="70-79"},
            new{Id="9",Value="80-89"}
            }, "Id", "Value");
        }

        public static IEnumerable<SelectListItem> GetGenderType()
        {
            return new SelectList(new[]{
            new {Id="0", Value="Select Gender Type"},
            new{Id="1", Value="Male"},
            new{Id="2", Value="Female"}
            }, "Id", "Value");
        }

        public static int GetCurrentLoginUserId()
        {
            var UserContext = new UsersContext();
            var userId = WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name);//user profile user id


            var user = UserContext.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
            var employeeId = user.EmployeeId;
            if (employeeId != null)
            {
                return Convert.ToInt32(employeeId);
            }
            else
            {
                return 999;//SuperAdmin
            }

        }

        public static string GetCurrentLoginUserName()
        {
            var UserContext = new UsersContext();
            var userId = WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name);
            var user = UserContext.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);

            //var employeeId = user.EmployeeId;
            if (user != null)
            {
                string username = user.UserName.ToLower();
                return username;
            }
            else
            {
                return "";
            }

        }

        //saru mam

        public static IEnumerable<SelectListItem> GetCOA4thHeadList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //                string sql = @"select 0 as AccSubgruupID, '--Select--' as AccSubGroupName
                //                            Union
                //                          select AccSubGruupID, AccSubGroupName from GL_AccSubgroups";
                string sql = @"select 0 as AccSubGruupID, '--Select--' as AccSubGroupName
                                Union
                                select AccSubGruupID , AccSubGroupName from GL_AccSubGroups where HeadLevel=4";
                return new SelectList(ent.Database.SqlQuery<AccSubLoad>(sql).ToList(), "AccSubGruupID", "AccSubGroupName");
            }
        }


        public static IEnumerable<SelectListItem> GetCOA5thHeadList(int Accid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select 0 as AccSubGruupID, '--Select--' as AccSubGroupName
                                Union
                                select AccSubGruupID , AccSubGroupName from GL_AccSubGroups where ParentID ='" + Accid + "' and IsLeafLevel=1";
                return new SelectList(ent.Database.SqlQuery<AccSubLoad>(sql).ToList(), "AccSubGruupID", "AccSubGroupName");
            }
        }




        public static int GetCurrentUserDepartmentId()
        {
            var UserContext = new UsersContext();
            var userId = WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name);
            var user = UserContext.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
            var employeeId = user.EmployeeId;
            if (employeeId != null)
            {
                EHMSEntities ent = new EHMSEntities();
                int empId = Convert.ToInt32(employeeId);
                int count = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == empId).Count();
                if (count > 0)
                {
                    return ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == empId).FirstOrDefault().LoginDepartmentIdnt;
                }
                else
                {
                    return 0;
                }


            }
            else
            {
                return 909;//Default superadmin departmentID
            }


        }

        public static string GetCurrentUserNameFromTable()
        {
            var UserContext = new UsersContext();
            var UserId = WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name);
            var user = UserContext.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
            var employeeId = user.EmployeeId;
            if (employeeId != null)
            {
                EHMSEntities ent = new EHMSEntities();
                int empId = Convert.ToInt32(employeeId);
                var EmpName = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == empId).FirstOrDefault();
                if (EmpName != null)
                {
                    return EmpName.EmployeeFullName;
                }
                else
                    return "";

            }
            else
            {
                return "Super Admin";//Default superadmin departmentID
            }
        }

        public static int GetCurrentUserAssociatedDepartmentId()//opd, ipd, emergency or .......
        {
            var UserContext = new UsersContext();
            var userId = WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name);
            var user = UserContext.UserProfiles.SingleOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
            var employeeId = user.EmployeeId;
            if (employeeId != null)
            {
                using (EHMSEntities ent = new EHMSEntities())
                {
                    int empId = Convert.ToInt32(employeeId);
                    int LoginDeptId = ent.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == empId).FirstOrDefault().LoginDepartmentIdnt;
                    var AssociateId = ent.SetupDepartments.Where(x => x.DeptID == LoginDeptId).FirstOrDefault().AssociateDepartmentID;
                    return Convert.ToInt32(AssociateId);
                }

            }
            else
            {
                return 909;//Default superadmin departmentID
            }


        }

        public static string GetEmployeeNameFromEmpID(int EmpId)
        {
            if (EmpId == 999)
            {
                return "Superadmin";
            }
            using (EHMSEntities enta = new EHMSEntities())
            {
                var data = enta.SetupEmployeeMasters.Where(x => x.EmployeeMasterId == EmpId).FirstOrDefault();

                if (data != null)
                {
                    return data.EmployeeFullName;
                }
                else
                    return "";
            }

        }



        public static string GetDepartmentName(int DepartmentId)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                if (DepartmentId == 1000)
                {
                    return "OPD";
                }
                else if (DepartmentId == 1001)
                {
                    return "Emergency";
                }
                else if (DepartmentId == 909)
                {
                    return "OPD";

                }

                else
                {

                    int DeptNameCount = ent.SetupDepartments.Where(x => x.DeptID == DepartmentId).Count();
                    if (DeptNameCount > 0)
                    {
                        string deptName = ent.SetupDepartments.Where(x => x.DeptID == DepartmentId).FirstOrDefault().DeptName;
                        return deptName;
                    }
                    else
                    {
                        return "--";
                    }


                }

            }
        }


        public static string GetPatientNameFromCOA(int coa)
        {


            using (EHMSEntities ent = new EHMSEntities())
            {

                var PatId = ent.OpdMasters.Where(x => x.AccountHeadId == coa).ToList();
                if (PatId.Count() > 0)
                {
                    int PatientId = PatId.FirstOrDefault().OpdID;
                    var fullname = (from a in ent.OpdMasters
                                    where a.OpdID == PatientId
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
                    return "";
                }



            }
        }
        public static string GetDoctorNameFromDoctorId(int DocId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var fullname = (from a in ent.SetupDoctorMasters
                                where a.DoctorID == DocId
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

        public static string GetPatientNameWithIdFromOpd(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var fullname = (from a in ent.OpdMasters
                                where a.OpdID == id
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

        public static string GetPatientNameWithIdFromEmergency(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var fullname = (from a in ent.OpdMasters
                                where a.OpdID == id
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




        public static string GetRoomTypeName(int RoomTypeId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  var result= ent.SetupRoomType.Where(x => x.RoomTypeId == RoomTypeId).FirstOrDefault().RoomTypeName;
                var result = from c in ent.SetupRoomTypes
                             where c.RoomTypeId == RoomTypeId
                             select c.RoomTypeName;
                var roomtypname = result.FirstOrDefault();
                if (roomtypname != null)
                {
                    return roomtypname;
                }
                else
                {

                    return "";
                }

            }
        }

        public static IEnumerable<SelectListItem> GetRoomTypeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupRoomTypes.ToList(), "RoomTypeId", "RoomTypeName");
            }

        }

        public static IEnumerable<SelectListItem> GetRelationTypeDD()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupRelationships.ToList(), "RelationId", "RelationName");
            }

        }
        public static int GetTotalBloodQuantity(int BloodGroupType)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                if (ent.BloodStockManagementMasters.Where(x => x.BloodType == BloodGroupType).Any())
                {
                    return Convert.ToInt32(ent.BloodStockManagementMasters.Where(x => x.BloodType == BloodGroupType).FirstOrDefault().QuantityUnit);
                }
                else
                {
                    return 0;
                }
            }

        }

        public static decimal GetTotalBloodMl(int BloodGroupType)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                if (ent.BloodStockManagementMasters.Where(x => x.BloodType == BloodGroupType).Any())
                {
                    return Convert.ToDecimal(ent.BloodStockManagementMasters.Where(x => x.BloodType == BloodGroupType).FirstOrDefault().QuantityML);
                }
                else
                {
                    return 0;
                }
            }

        }

        public static string GetBloodGroupTitle(int BloodGroupId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.SetupBloodGroups.Where(x => x.BloodGroupID == BloodGroupId).FirstOrDefault().BloodGroup;
            }
        }



        public static string GetBloodGroupTitleByMasterId(int BloodTransectionMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.SetupBloodGroup.Where(x => x.BloodGroupID == BloodTransectionMasterId).FirstOrDefault().BloodGroup;
                var result = from c in ent.SetupBloodGroups
                             where c.BloodGroupID == BloodTransectionMasterId
                             select c.BloodGroup;
                var bloodtitle = result.FirstOrDefault();
                if (bloodtitle != null)
                {
                    return bloodtitle;

                }
                else
                {
                    return "";
                }
            }
        }



        public static IEnumerable<SelectListItem> GetBloodGroupList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupBloodGroups.ToList(), "BloodGroupID", "BloodGroup");
            }
        }

        public static string GetGenderTitle(int GenderID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.SetupGender.Where(x => x.GenderId == GenderID).FirstOrDefault().Name;
                var result = from b in ent.SetupGenders
                             where b.GenderId == GenderID
                             select b.Name;
                var genderid = result.FirstOrDefault();
                if (genderid != null)
                {
                    return genderid;
                }
                else
                {

                    return "";
                }

            }
        }


        public static IEnumerable<SelectListItem> GetGenderListFromDB()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupGenders.ToList(), "GenderId", "Name");
            }

        }
        public static IEnumerable<SelectListItem> GetZoneList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.TblZones.ToList(), "ZoneID", "ZoneInEng");
            }
        }

        public static string GetDistricttitleById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var title = ent.TblDistricts.Where(x => x.DistrictID == id).FirstOrDefault();

                if (title != null)
                {
                    return title.DistrictEng;
                }
                else
                {
                    return "N/A";
                }

            }
        }

        public static IEnumerable<SelectListItem> GetDistrictList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.TblDistricts.ToList(), "DistrictID", "DistrictEng");
            }
        }
        //public static IEnumerable<SelectListItem> GetCategory()
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        return new SelectList(ent.SetupStockCategory.Where(x => x.Status == true).ToList(), "StockCategoryID", "StockCategoryName");
        //    }
        //}
        public static IEnumerable<SelectListItem> GetVdc1()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.VDCMUNs.ToList(), "VdcMunID", "VdcMunNameEng");
            }
        }

        public static IEnumerable<SelectListItem> GetOutRefer()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.ReferTables.ToList(), "Id", "Refername");
            }
        }
        public static IEnumerable<SelectListItem> GetVdc(int? id)
        {
            if (id == 0)
            {
                IEnumerable<SelectListItem> list;
                list = GetVdc1();
                return list;
            }
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.VDCMUNs.Where(x => x.DistrictID == id).ToList(), "VdcMunID", "VdcMunNameEng");
            }
        }

        public static IEnumerable<SelectListItem> GetDistrictList(int? ZoneId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.TblDistricts.Where(x => x.ZoneID == ZoneId).ToList(), "DistrictID", "DistrictEng");
            }
        }



        public static IEnumerable<SelectListItem> GetOperationTheatreMaster()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.OperationTheatreMasters.Where(x => x.Status == true).ToList(), "OperationTheatreMasterID", "OperationTheatreMasterID");
            }

        }

        public static IEnumerable<SelectListItem> GetDoctorsForEHSReportList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMaster> doctorList = (from s in ent.SetupEmployeeMasters where s.Status == true && s.DesignationID == 2 select s).ToList();
                List<object> obj = new List<object>();
                obj.Add(new SelectListItem { Text = "--Unit--", Value = "1009" });

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Value = item.SPAccountHeadID,
                        Text = item.EmployeeFullName
                    });
                }


                return new SelectList(obj, "Value", "Text");
            }

        }


        public static IEnumerable<SelectListItem> GetDoctorsForEHS()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupEmployeeMaster> doctorList = (from s in ent.SetupEmployeeMasters where s.Status == true && s.DesignationID == 2 && s.EmployeeTypeId == 56 select s).ToList();
                List<object> obj = new List<object>();
                obj.Add(new SelectListItem { Text = "--Unit--", Value = "1009" });

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Value = item.SPAccountHeadID,
                        Text = item.EmployeeFullName
                    });
                }


                return new SelectList(obj, "Value", "Text");
            }

        }




        public static IEnumerable<SelectListItem> GetDoctors()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupDoctorMaster> doctorList = (from s in ent.SetupDoctorMasters where s.Status == true select s).ToList();
                List<object> obj = new List<object>();
                obj.Add(new SelectListItem { Text = "--Unit--", Value = "1009" });

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Value = item.DoctorID,
                        Text = item.FirstName + " " + item.MiddleName + " " + item.LastName
                    });
                }


                return new SelectList(obj, "Value", "Text");
            }

        }



        public static IEnumerable<SelectListItem> GetType()
        {
            return new SelectList(new[]{
                new{Id="1",Value="Doctor"},
                new{Id="2",Value="Nurse"}
            }, "Id", "Value");
        }
        public static IEnumerable<SelectListItem> GetIpdColumnNameList()
        {

            return new SelectList(new[]{
                new {Id="1",Value="OPD ID"},
                new {Id="2",Value="IPD ID"},
                new {Id="3",Value="Registration Date"},
                new {Id="4",Value="Bed Number"},
                new {Id="5",Value="Word Number"},
                new {Id="6",Value="Room Number"},
                new{Id="7",Value="Room Type"}

               }, "Id", "Value");
        }

        public static IEnumerable<SelectListItem> GetOpdSearchDDL()
        {

            return new SelectList(new[]{
                new {Id="1",Value="Patient Id"},
                new {Id="2",Value="First Name"},
                //new {Id="3",Value="Address"},
                new {Id="4",Value="Phone"},
                //new {Id="5",Value="Age"},
                //new {Id="6",Value="Sex"},
                //new{Id="7",Value="Blood Group"},
                //new {Id="8",Value="Registration Date"}
               }, "Id", "Value");
        }
        public static IEnumerable<SelectListItem> GetFeeTypeOPD()
        {
            return new SelectList(new[]{
            new {Id = "1", Value="Registration"},
            //new {Id = "2", Value="Old Registration"},
            //new {Id = "3", Value="Emergency Registration"},
            //new {Id = "4", Value = "Consulatant OPD"},
            //new {Id = "5", Value = "General Registration"},
            }, "Id", "Value");
        }
        public static IEnumerable<SelectListItem> GetPreFeeTypeOPD()
        {
            return new SelectList(new[]{
            new {Id = "3", Value = "New Registration Pay"},
            }, "Id", "Value");
        }
        public static IEnumerable<SelectListItem> GetFeeType()
        {
            return new SelectList(new[]{
                new {Id="1",Value="New Registration"},
                new {Id="3",Value="New Registration Pay"},
                //new {Id="19",Value="Emergency"},
                //new {Id="11",Value="New Registration Pay"},
                //new {Id="34", Value="General OPD"},
                //new {Id="35", Value="Consultant OPD"},
                //new {Id="36", Value="Emergency OPD"}
                
               }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetPatientAgeType()
        {
            return new SelectList(new[]{
                new {Id="1",Value="Year"},
                new {Id="2",Value="Month"},
                new {Id="3",Value="Days"}

               }, "Id", "Value");

        }

        public static string GetAgeTypeByItsId(int AgeTypeId)
        {
            string retunAgeType = string.Empty;
            switch (AgeTypeId)
            {
                case 1:
                    retunAgeType = "Year";
                    break;

                case 2:
                    retunAgeType = "Month";
                    break;

                case 3:
                    retunAgeType = "Days";
                    break;

            }
            return retunAgeType;

        }




        public static IEnumerable<SelectListItem> GetOtherTestOrRadiologyDD()
        {
            return new SelectList(new[]{
                new {Id="1",Value="Radiology"},
                new {Id="2",Value="Other Test"},
                new {Id="3",Value="Dental"},//Restoration & RCT, Extraction & Surgery, Perior & Scaling, X-ray
                new {Id="4",Value="Eye"},
                new {Id="5",Value="Physiotherapy"},
                new {Id="6",Value="Ent"},



            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetOpdSearchDDLCrt()
        {

            return new SelectList(new[]{
                new {Id="0",Value="No"},
                new {Id="1",Value="Member Ship"},
                       // new {Id="2",Value="Old Patient"}
                //new {Id="3",Value="Address"},
                //new {Id="4",Value="Phone"}
               }, "Id", "Value");
        }



        public static IEnumerable<SelectListItem> lstOfMaxDepnd()
        {

            var Num = new List<SelectListItem>();

            for (var i = 0; i <= 7; i++)
            {
                Num.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }


            return Num;
        }
        public static string GetVDCNameEng(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var vdcname = (from v in ent.VDCMUNs
                               where v.VdcMunID == id
                               select new { v.VdcMunNameEng }).SingleOrDefault();
                if (vdcname != null)
                {
                    return vdcname.VdcMunNameEng;
                }
                else return "-----";
            }
        }
        public static string GetDoctorName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                if (id == 1009)
                {

                    return "Unit";


                }

                var name = (from s in ent.SetupDoctorMasters
                            where s.DoctorID == id
                            select new { s.FirstName, s.MiddleName, s.LastName }).SingleOrDefault();

                if (name != null)
                {
                    return name.FirstName + " " + name.MiddleName + " " + name.LastName;
                }
                else
                {
                    return "-----";
                }
            }
        }

        public static IEnumerable<SelectListItem> GetOperationRoom()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupOperationTheatreRooms.Where(x => x.Stats == true).ToList(), "OperationTheatreRoomID", "RoomName");
            }

        }

        public static string GetOperationRoomName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from n in ent.SetupOperationTheatreRooms
                             where n.OperationTheatreRoomID == id
                             select n.RoomName;
                var romname = result.FirstOrDefault();
                if (romname != null)
                {
                    return romname;
                }
                else
                {
                    return "";
                }
            }


        }

        public static string DepartmentName(int a)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from n in ent.SetupDepartments
                             where n.DeptID == a
                             select n.DeptName;
                var deptname = result.FirstOrDefault();
                if (deptname != null)
                {
                    return deptname;
                }
                else
                {
                    return "";
                }
            }


        }
        public static string DoctorName(int a)
        {

            try
            {
                using (EHMSEntities ent = new EHMSEntities())
                {


                    if (a == 1009)
                    {


                        return "Unit";

                    }

                    var FullName = (from p in ent.SetupDoctorMasters
                                    where p.DoctorID == a
                                    select new
                                    {
                                        p.FirstName,
                                        p.MiddleName,
                                        p.LastName
                                    }).SingleOrDefault();
                    return FullName.FirstName + " " + FullName.MiddleName + " " + FullName.LastName;
                }
            }
            catch (Exception e)
            {
                return "";

            }
        }


        public static TimeSpan GetTime(int a)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return (TimeSpan)(ent.PreRegistrationPreferDetails.Where(x => x.PreferDetailsID == a).FirstOrDefault().PreferTime);
            }
        }

        public static DateTime GetDate(int a)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return (DateTime)(ent.PreRegistrationPreferDetails.Where(x => x.PreferDetailsID == a).FirstOrDefault().PreferDate);
            }
        }


        public static int getMaxVoucherNumber(string JvType, int fiscalYearID)
        {
            try
            {
                using (EHMSEntities ent = new EHMSEntities())
                {
                    var data = (from x in ent.SetupVoucherNumbers
                                where (x.FiscalYear == fiscalYearID && x.JvType == JvType)
                                select new { x.VoucherNo }).ToList();
                    int maxVoucherNumber = Convert.ToInt32(data.Max(x => x.VoucherNo));

                    return maxVoucherNumber;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }



        public static int GetMaxBillNumberFromDepartment(string DepartmentName, int FiscalYearId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = (from x in ent.SetupHospitalBillNumbers
                            where (x.DepartmentName == DepartmentName && x.FiscalYearId == FiscalYearId)
                            select new { x.BillNumber }).ToList();
                int maxBillNumber = Convert.ToInt32(data.Max(x => x.BillNumber));
                return maxBillNumber;
            }
        }

        public static int GetMaxDepositeNumber()
        {
            int DepoReceiptNumber = Convert.ToInt32(0);
            using (EHMSEntities ent = new EHMSEntities())
            {

                //Int32 maxnum = Convert.ToInt32(ent.CentralizedBillingMaster.Where(x => x.Status == true).ToList().LastOrDefault().ReceiptId);

                //if (maxnum == null || maxnum == 0)
                //{

                //    return 1;

                //}
                //else
                //{

                //    return Convert.ToInt32(maxnum + 1);

                //}
                var depoNumber = ent.PatientDepositMasters.ToList();
                if (depoNumber.Count() > 0)
                {
                    int depoReceiptNumber = depoNumber.LastOrDefault().PatientDepositMasterId;
                    DepoReceiptNumber = depoReceiptNumber;
                    return DepoReceiptNumber + 1;
                }
                else
                {
                    return 1;
                }


            }


        }


        public static string GetSectionNameById(int sectionId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var count = ent.SetupSections.Where(x => x.SectionId == sectionId).Count();
                if (count > 0)
                {
                    var sectionName = ent.SetupSections.Where(x => x.SectionId == sectionId).FirstOrDefault().Name;
                    return sectionName;
                }
                else
                {
                    return "No SectionName";
                }
            }

        }

        public static IEnumerable<SelectListItem> GetSectionList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupSections.ToList(), "SectionId", "Name");
            }
        }

        public static IEnumerable<SelectListItem> GetUnitList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupUnits.ToList(), "UnitId", "UnitName");
            }
        }

        public static IEnumerable<SelectListItem> GetTestParentHead()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.SetupPathoTests.Where(x => x.IsParent == true);
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.TestName, Value = item.TestId.ToString() });

                }
                return ddlList;

            }
        }




        public static string GetUnitNameById(int unitId)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupUnits
                              where c.UnitId == unitId
                              select c.UnitName;
                var unitName = results.FirstOrDefault();

                if (unitName != null)
                {
                    return unitName;
                }
                else
                {
                    return "--";
                }
            }

        }


        public static string GetTestNameById(int testId)
        {
            if (testId == 0)
            {
                return "--";
            }
            else
            {
                using (EHMSEntities ent = new EHMSEntities())
                {
                    var result = from n in ent.SetupPathoTests
                                 where n.TestId == testId
                                 select n.TestName;
                    var tstname = result.FirstOrDefault();
                    if (tstname != null)
                    {
                        return tstname;

                    }
                    else
                    {

                        return "";
                    }


                }

            }

        }
        public static List<BloodStockManagementMaster> GetBloodStockLists()
        {
            using (var ent = new EHMSEntities())
            {
                return ent.BloodStockManagementMasters.ToList();
            }
        }

        public static decimal GetDoctorFee(int id)
        {
            using (var ent = new EHMSEntities())
            {
                try
                {
                    var drfee = ent.OpdFeeDetails.Where(x => x.OpdID == id).FirstOrDefault().DoctorFee;
                    return Convert.ToDecimal(drfee);
                }
                catch
                {

                    return 0;

                }
            }

        }


        public static decimal MemberDiscountAmt(int id)
        {
            using (var ent = new EHMSEntities())
            {

                try
                {
                    var memdisfee = ent.OpdFeeDetails.Where(x => x.OpdID == id).FirstOrDefault().MemberDiscountAmount;
                    return Convert.ToDecimal(memdisfee);
                }
                catch
                {

                    return 0;

                }
            }

        }




        public static decimal otherDiscountAmt(int id)
        {
            using (var ent = new EHMSEntities())
            {
                try
                {
                    var otherdisfee = ent.OpdFeeDetails.Where(x => x.OpdID == id).FirstOrDefault().OtherDiscountAmount;
                    return Convert.ToDecimal(otherdisfee);
                }
                catch
                {

                    return 0;

                }
            }

        }

        //Get fee for opd bill form
        public static string GetOpdFee(int id, DateTime RegDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var fee = ent.OpdFeeDetails.OrderByDescending(x => x.OpdFeeDetailsID).Where(x => x.OpdID == id && x.FeeDate == RegDate).FirstOrDefault().TotalAmount;
                    return fee.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }

        //Get address for opd bill form
        public static string GetOpdAddress(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.OpdMasters.Where(x => x.OpdID == id).SingleOrDefault().Address;
                    return address;


                }
                catch
                {
                    return "";
                }
            }

        }


        //sundar

        public static IEnumerable<SelectListItem> GetIpdWardName()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupIpdWards.Where(m => m.Status == true).ToList(), "IpdWardId", "WardName");
            }

        }

        public static IEnumerable<SelectListItem> GetRoomType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SelectListItem> RoomTypelList = new List<SelectListItem>();
                RoomTypelList.Add(new SelectListItem { Text = "Select Room Type", Value = null });
                var data = (from x in ent.SetupRoomTypes
                            where x.Status == true
                            select new { x.RoomTypeId, x.RoomTypeName }).Distinct();

                foreach (var item in data)
                {
                    RoomTypelList.Add(new SelectListItem { Text = item.RoomTypeName, Value = item.RoomTypeId.ToString() });
                }
                return RoomTypelList;
            }
        }




        public static IEnumerable<SelectListItem> GetRoomNumber(int Roomtype)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = new SelectList(ent.SetUpIpdRooms.Where(m => m.RoomType == Roomtype).Select(m => m.RoomNo).ToList());
                return data;
            }

        }
        public static IEnumerable<SelectListItem> GetBedNumber(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = new SelectList(ent.IpdRoomStatus.Where(m => m.RoomNumber == id && m.Status == true).Select(m => m.BedNumber).ToList());
                return data;
            }
        }


        //gopal sir
        public static IEnumerable<SelectListItem> GetFiscalYearsinEnglish()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFiscalYears.ToList(), "FiscalYearId", "FiscalYearAD");
            }
        }


        //gopal sir
        public static string GetFiscalYearInEnglishByID(int FiscalYearid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var totalCount = ent.SetupFiscalYears.Where(x => x.FiscalYearId == FiscalYearid).Any();
                if (totalCount)
                {
                    return ent.SetupFiscalYears.Where(x => x.FiscalYearId == FiscalYearid).FirstOrDefault().FiscalYearAD;
                }
                else
                {
                    return "FYSETUP";
                }

            }
        }

        //edited by bibek decem 19th
        public static IEnumerable<SelectListItem> GetFiscalYearForDropDown()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFiscalYears.ToList(), "FiscalYearId", "FiscalYearBS");
            }

        }


        //subin

        public static string outcomename(int a)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                return ent.EmergencyOutcomeTypes.Where(x => x.OutcomeTypeId == a).FirstOrDefault().TypeName;

            }


        }

        public static IEnumerable<SelectListItem> GetOutComeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.EmergencyOutcomeTypes.ToList(), "OutcomeTypeId", "TypeName");
            }

        }

        public static string getEmergencyPatientName(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                var FullName = (from p in ent.OpdMasters
                                where p.DepartmentPatientId == id
                                select new
                                {
                                    p.FirstName,
                                    p.MiddleName,
                                    p.LastName
                                }).SingleOrDefault();
                return FullName.FirstName + " " + FullName.MiddleName + " " + FullName.LastName;
            }

        }

        public static IEnumerable<SelectListItem> GetTriageMedium()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupTriageMediums.ToList(), "TriageMediumId", "MediumName");
            }
        }


        public static IEnumerable<SelectListItem> GetDoctorNameForEmergency()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupDoctorMasters.ToList(), "DoctorID", "FirstName");
            }


        }


        public static IEnumerable<SelectListItem> GetMemberShipLst()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupMemberShips.ToList(), "SetupMemberId", "FullName");
            }

        }


        public static IEnumerable<SelectListItem> GetDayOfWeek()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="Sunday"},
                new {Id="2",Value="Monday"},
                new {Id="3",Value="Tuesday"},
                new {Id="4",Value="Wednesday"},
                new {Id="5",Value="Thursday"},
                new {Id="6",Value="Friday"},
                new {Id="7",Value="Saturday"},
                new {Id="8", Value="On Request"}

            }, "Value", "Value");
        }


        public static IEnumerable<SelectListItem> GetDayOfWeekText()
        {
            return new SelectList(new[]
            {
                new {Value="1",Text="Sunday"},
                new {Value="2",Text="Monday"},
                new {Value="3",Text="Tuesday"},
                new {Value="4",Text="Wenesday"},
                new {Value="5",Text="Thursday",},
                new {Value="6",Text="Friday"},
                new {Value="7",Text="Saturday"}

            });
        }

        public static string getDayName(int id)
        {

            string str = "";

            if (id == 1)
            {

                str = "Sunday";

            }

            if (id == 2)
            {

                str = "Monday";

            }
            if (id == 3)
            {

                str = "Tuesday";

            }
            if (id == 4)
            {

                str = "Wenesday";

            }
            if (id == 5)
            {

                str = "Thursday";

            }
            if (id == 6)
            {

                str = "Friday";

            }
            if (id == 7)
            {

                str = "Saturday";

            }
            //Added By Pushkar
            if (id == 8)
            {
                str = "On Request";
            }

            return str;

        }


        public static IEnumerable<SelectListItem> GetShift()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="1st"},
                new {Id="2",Value="2nd"},
                new {Id="3",Value="3rd"},
                new {Id="4",Value="4th"},
                new {Id="5",Value="5th"},
                new {Id="6",Value="6th"},
                new {Id="7",Value="7th"},
                new {Id="7",Value="8th"}

            }, "Value", "Value");
        }


        public static IEnumerable<SelectListItem> GetDoctorList(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var info = "";

                return new SelectList(info);

            }

        }


        public static IEnumerable<SelectListItem> ChooseSearchMethod()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text ="ID", Value ="OPDID"},
                new SelectListItem{Text = "Name", Value ="Name"}
            };

        }


        public static IEnumerable<SelectListItem> OpdOrEmergency()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text ="OPD", Value ="OPD"},
                new SelectListItem{Text = "EMERGENCY", Value ="EMER"}
            };

        }
        //inserted on april 10th
        public static IEnumerable<SelectListItem> GetFeeTypeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFeeTypes.ToList(), "SetupFeeTypeId", "FeeTypeName");
            }
        }



        public static IEnumerable<SelectListItem> CatagoryName()
        {
            return new List<SelectListItem>
            {
            new SelectListItem{Text="DOC Fee", Value ="DOC Fee"},
            new SelectListItem{Text ="REG Fee", Value ="REG Fee"},
            new SelectListItem{Text ="Bed Fee", Value ="Bed Fee"}

            };

        }

        public static string GetMemberShipName(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                var str = ent.SetupMemberShips.Where(x => x.SetupMemberID == id).Select(x => x.FirstName).SingleOrDefault();
                if (str != null)
                {
                    return str;
                }
                else
                {
                    return "";
                }
            }


        }


        public static string GetRelationName(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var str = ent.SetupRelationships.Where(x => x.RelationId == id).Select(x => x.RelationName).SingleOrDefault();

                if (str != null)
                {
                    return str;
                }
                else
                {
                    return "";
                }

            }
        }

        public static string GetBloodGroupNameWithId(int BloodGroupID)
        {
            string str = "";
            using (EHMSEntities ent = new EHMSEntities())
            {
                str = ent.SetupBloodGroups.Where(x => x.BloodGroupID == BloodGroupID).Select(x => x.BloodGroup).SingleOrDefault();
                if (str != null)
                {
                    return str;
                }
                else
                {
                    return "";
                }
            }

        }


        //Get Stock Category Name
        public static string GetStockCategoryName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                // return ent.SetupStockCategory.Where(x => x.StockCategoryID == id).FirstOrDefault().StockCategoryName;

                var result = from u in ent.SetupStockCategories
                             where u.StockCategoryID == id
                             select u.StockCategoryName;

                var crtname = result.FirstOrDefault();

                if (crtname != null)
                {
                    return crtname;
                }
                else
                {
                    return "";
                }

            }
        }


        //Stock Item Entry Begin

        public static IEnumerable<SelectListItem> GetCategory()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockCategories.Where(x => x.Status == true).ToList(), "StockCategoryID", "StockCategoryName");
            }
        }

        public static IEnumerable<SelectListItem> GetSubCategory()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockSubCategories.Where(x => x.Status == true && x.StockCategoryID == ent.SetupStockCategories.Where(y => y.Status == true).FirstOrDefault().StockCategoryID).ToList(), "StockSubCategoryID", "StockSubCategoryName");
            }
        }



        public static IEnumerable<SelectListItem> GetUnit()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockUnits.Where(x => x.Status == true).ToList(), "StockUnitID", "StockUnitName");
            }
        }

        public static IEnumerable<SelectListItem> GetSupplier()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockSuppliers.Where(x => x.Status == true).ToList(), "StockSupplierID", "StockSupplierName");
            }
        }
        public static IEnumerable<SelectListItem> GetSupplierType()
        {
            return new SelectList(new[]{
                new{Text="Manufacturer",Value="Manufacturer"},
                new{Text="Supplier",Value="Supplier"},
                new{Text="Vendor",Value="Vendor"},

            }, "Text", "Value");
        }
        public static IEnumerable<SelectListItem> GetItemType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockItemTypes.Where(x => x.Status == true).ToList(), "StockItemTypeID", "StockItemTypeName");
            }
        }
        public static string GetSubCategoryName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockSubCategories.Where(x => x.StockSubCategoryID == id).SingleOrDefault().StockSubCategoryName;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string GetUnitName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockUnits.Where(x => x.StockUnitID == id).SingleOrDefault().StockUnitName;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string GetSupplierName(int? id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockSuppliers.Where(x => x.StockSupplierID == id).SingleOrDefault().StockSupplierName;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string GetItemTypeName(int? id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockItemTypes.Where(x => x.StockItemTypeID == id).SingleOrDefault().StockItemTypeName;
                }
                catch
                {
                    return "";
                }
            }
        }


        public static IEnumerable<SelectListItem> GetItemName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockItemEntries.Where(
                    x => x.Status == true &&
                    x.StockCategoryId == ent.SetupStockCategories.Where(y => y.Status == true).FirstOrDefault().StockCategoryID &&
                    x.StockSubCategoryId == ent.SetupStockSubCategories.Where(z => z.Status == true && z.StockCategoryID == x.StockCategoryId).FirstOrDefault().StockSubCategoryID).ToList()
                    , "StockItemEntryId", "StockItemName");
            }
        }
        public static IEnumerable<SelectListItem> GetItemNameEdit(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockItemEntries.Where(x => x.StockSubCategoryId == id && x.Status == true).ToList(), "StockItemEntryId", "StockItemName");
            }
        }
        //Stock Item Entry End
        //Stock Item Purchase Begin

        public static string GetItemName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.StockItemPurchaseDetails.Where(x => x.StockItemPurchaseDetailId == id).SingleOrDefault().SetupStockItemEntry.StockItemName;
                }
                catch
                {
                    return "";
                }
            }
        }


        public static string GetPurchaseDetailUnit(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.StockItemPurchaseDetails.Where(x => x.StockItemPurchaseDetailId == id).SingleOrDefault().SetupStockUnit.StockUnitName;
                }
                catch
                {
                    return "";
                }
            }
        }

        //Stock Item Purchase End
        //Demand Begin

        public static string GetDemandItemName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).SingleOrDefault().StockItemName;
                }
                catch
                {

                    return "";

                }
            }
        }

        //Demand End
        //Allotment Begin
        public static IEnumerable<SelectListItem> GetDepartmentListWithSelect()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "Select", Value = "0" });
            using (EHMSEntities ent = new EHMSEntities())
            {

                foreach (var item in ent.SetupDepartments.Where(x => x.IsAvailable == true).ToList())
                {
                    ddlList.Add(new SelectListItem { Text = item.DeptName, Value = item.DeptID.ToString() });
                }

            }
            return ddlList;
        }

        public static IEnumerable<SelectListItem> GetSource()
        {
            return new SelectList(new[]{
                new{Id="1",Value="Stock"},
                new{Id="2",Value="Market"}
            }, "Id", "Value");
        }

        public static decimal GetAvailableStock(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.StockItemMasters.Where(x => x.StockItemEntryId == id).SingleOrDefault().Quantity;
                }
                catch
                {
                    return 0;

                }
            }
        }

        public static string GetDepartmentFromAllotment(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var name = (from s in ent.SetupDepartments
                                join i in ent.ItemDemandMasters on s.DeptID equals i.DepartmentID
                                join a in ent.ItemAllotmentMasters on i.ItemDemandID equals a.ItemDemandID
                                where a.ItemAllotmentMasterID == id
                                select s.DeptName).SingleOrDefault();
                    return name;
                }
                catch
                {

                    return "";
                }
            }
        }

        public static string GetEmergencyAddress(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.OpdMasters.Where(x => x.OpdID == id).SingleOrDefault().Address;
                    return address;
                }
                catch
                {
                    return "";
                }
            }

        }
        //Get fee for Emergency bill form
        public static string GetEmergencyFeeRegistration(int? id, DateTime RegDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var regfee = ent.SetupEmergencyFees.OrderByDescending(x => x.FeeId).FirstOrDefault().EmergencyFee;
                    return regfee.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        public static string GetEmergencyFee(int? id, DateTime RegDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var fee = ent.EmergencyFeeDetails.OrderByDescending(x => x.EmergencyFeeDetailsId).Where(x => x.EmergencyMasterId == id).FirstOrDefault().TotalAmount;
                    return fee.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }

        //public static string GetEmergencyTax(int? id, DateTime Regdate)
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var emergencytax = ent.EmergencyFeeDetails.OrderByDescending(x => x.EmergencyFeeDetailsId).Where(x => x.EmergencyMasterId == id).FirstOrDefault();
        //    }
        //}
        // for relationship for discharge report
        public static string GetRelationship(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.SetupRelationships.Where(x => x.RelationId == id).SingleOrDefault().RelationName;
                    return address;
                }
                catch
                {
                    return "";
                }
            }

        }
        //Subin
        public static string GetTriagemediumForReport(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.SetupTriageMediums.Where(x => x.TriageMediumId == id).SingleOrDefault().MediumName;
                    return address;
                }
                catch
                {
                    return "";
                }
            }

        }
        //Subin
        public static string GetOutComeTypeForReport(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.EmergencyOutcomeTypes.Where(x => x.OutcomeTypeId == id).SingleOrDefault().TypeName;
                    return address;
                }
                catch
                {
                    return "";
                }
            }

        }
        //subin
        public static List<SetupDepartment> GetDeptList()
        {
            using (var ent = new EHMSEntities())
            {
                return ent.SetupDepartments.ToList();
            }
        }
        //subin
        public static List<SetupEmployeeShiftMaster> GetShiftListforemployee()
        {
            using (var ent = new EHMSEntities())
            {
                return ent.SetupEmployeeShiftMasters.ToList();
            }
        }

        //public static string shiftName(int id)
        //{
        //    string main = "";
        //    string starttime = "";
        //    string endtime = "";
        //    EHMSEntities ent = new EHMSEntities();

        //    try
        //    {
        //        var srttime = (from n in ent.SetupEmployeeShiftMaster
        //                       where n.EmployeeShiftMasterId == id
        //                       select n.StartTime);
        //        starttime = srttime.FirstOrDefault().ToString();



        //        var entime = (from n in ent.SetupEmployeeShiftMaster
        //                      where n.EmployeeShiftMasterId == id
        //                      select n.EndTime);
        //        endtime = entime.FirstOrDefault().ToString();

        //        main = "(" + starttime + " " + "To" + " " + endtime + ")";

        //        return main;
        //    }
        //    catch 
        //    {

        //        return "0:0:0";

        //    }
        //}


        //subin
        public static bool isExitShift(int shiftId, int employeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.SetupEmployeeShifts.Where(x => x.EmployeeShiftMasterId == shiftId && x.EmployeeMasterId == employeeMasterId).Any();
            }
        }
        //subin
        public static bool isExitDepartment(int DepartmentId, int employeeMasterId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.SetupEmployeeDepartments.Where(x => x.DepartmentId == DepartmentId && x.EmployeeMasterId == employeeMasterId).Any();
            }
        }
        //subin
        public static IEnumerable<SelectListItem> GetEmployeeTypeList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupEmployeeTypes.ToList(), "SetupEmployeeTypeID", "SetupEmployeeTypeName");
            }

        }
        //subin
        public static IEnumerable<SelectListItem> GetWardTypeList()
        {
            return new List<SelectListItem>
                       {
                         new SelectListItem {Text = "Male", Value = "Male"},
                         new SelectListItem {Text = "Female", Value = "Female"},
                         new SelectListItem {Text = "Others", Value = "Others"}
                       };

        }
        //subin
        public static IEnumerable<SelectListItem> GetBlockList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupBlocks.Where(x => x.Status.Equals("Y")).ToList(), "BlockId", "BlockName");
            }

        }

        public static string GetStockGroupingNameFromId(int groupId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemGroupSetups.Where(x => x.Status == true && x.StockGroupingId == groupId);
                if (result.Count() > 0)
                {
                    return result.FirstOrDefault().GroupName;
                }
                else
                {
                    return "";
                }
            }

        }



        public static IEnumerable<SelectListItem> GetStockItemGroupLists()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var resut = ent.StockItemGroupSetups.Where(x => x.Status == true).ToList();
                if (resut.Count() > 0)
                {
                    return new SelectList(ent.StockItemGroupSetups.Where(x => x.Status == true).ToList(), "StockGroupingId", "GroupName");
                }
                else
                {
                    SelectList sl = new SelectList(new[]{
                        new SelectListItem{ Text="No-Group", Value="-1"},}, "Value", "Text");
                    return sl;
                }
            }

        }

        public static IEnumerable<SelectListItem> GetStockItemGroupLocationLists()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var resut = ent.StockItemLocationSetups.Where(x => x.Status == true).ToList();
                if (resut.Count() > 0)
                {
                    return new SelectList(ent.StockItemLocationSetups.Where(x => x.Status == true).ToList(), "StockItemLocationId", "LocatonName");
                }
                else
                {
                    SelectList sl = new SelectList(new[]{
                        new SelectListItem{ Text="No-Location", Value="-2"},}, "Value", "Text");
                    return sl;
                }
            }

        }


        //subin
        public static IEnumerable<SelectListItem> GetFloorList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFloors.Where(x => x.Status.Equals("Y")).ToList(), "FloorId", "FloorName");
            }

        }
        //subin
        public static string GetBlockName(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupBlocks.Where(x => x.BlockId == Id).FirstOrDefault().BlockName;
                }
                catch
                {

                    return "";
                }
            }
        }
        //subin
        public static string GetFloorName(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupFloors.Where(x => x.FloorId == Id).FirstOrDefault().FloorName;
                }
                catch
                {

                    return "";

                }
            }
        }
        //subin
        public static string GetEmployeeType(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return ent.SetupEmployeeType.Where(x => x.SetupEmployeeTypeID == Id).FirstOrDefault().SetupEmployeeTypeName;

                var results = from c in ent.SetupEmployeeTypes
                              where c.SetupEmployeeTypeID == Id
                              select c.SetupEmployeeTypeName;

                var empName = results.FirstOrDefault();

                if (empName != null)
                {
                    return empName;
                }
                else
                {
                    return "EMPSETUP";
                }

            }
        }


        //Get Current Fee from registration fee
        public static decimal GetCurrentRegistrationFee()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == 1
                              select c.HospitalFee;
                //Commented By Pushkar
                //select c.TotalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }
        //Get Current Fee from registration fee
        public static decimal GetCurrentRegistrationFeeWithID(int feeTypeid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == feeTypeid
                              select c.HospitalFee;
                //Commented By Pushkar
                //select c.TotalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }
        //Edited By Pushkar
        //public static decimal GetCurrentRegistrationFee()
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupHospitalFee
        //                      where c.Status == true
        //                      select c.HospitalFee;
        //        var totalFeeStr = results.FirstOrDefault();

        //        if (totalFeeStr != null)
        //        {
        //            return Convert.ToDecimal(totalFeeStr);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        public static decimal GetCurrentPayableRegFee()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == 11
                              //Edited By Pushkar
                              //select c.TotalFee;
                              select c.HospitalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }
        //Edited By Pushkar
        //public static decimal GetCurrentPayableRegFee()
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupHospitalFee
        //                      where c.Status == true
        //                      select c.TotalFee;
        //        var totalFeeStr = results.FirstOrDefault();

        //        if (totalFeeStr != null)
        //        {
        //            return Convert.ToDecimal(totalFeeStr);
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //}

        public static decimal GetCurrentRegistrationFeeTaxAmountOnly()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true
                              select c.Tax;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }



        //Editted by bibek on feb 25th
        //Get Current Fee from registration fee
        public static decimal GetCurrentRegistrationFeeOnly()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == 1
                              select c.HospitalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }

        //Edited By Pushkar
        //public static decimal GetCurrentRegistrationFeeOnly()
        //{

        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupHospitalFee
        //                      where c.Status == true
        //                      select c.HospitalFee;
        //        var totalFeeStr = results.FirstOrDefault();

        //        if (totalFeeStr != 0)
        //        {
        //            return Convert.ToDecimal(totalFeeStr);
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //}

        public static decimal GetCurrentRegistrationFeeOnlyPay()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == 11
                              select c.HospitalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }

            }
        }
        //Edited By Pushkar
        //public static decimal GetCurrentRegistrationFeeOnlyPay()
        //{

        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupHospitalFee
        //                      where c.Status == true
        //                      select c.HospitalFee;
        //        var totalFeeStr = results.FirstOrDefault();

        //        if (totalFeeStr != 0)
        //        {
        //            return Convert.ToDecimal(totalFeeStr);
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //}

        //To Test Tommorow
        //get tax amount
        public static decimal GetCurrentRegistrationTaxFee()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true
                              select c.HospitalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != 0)
                {
                    var taxAmt = from c in ent.SetupHospitalFees
                                 where c.Status == true
                                 select c.Tax;
                    var totalTax = taxAmt.FirstOrDefault();
                    if (totalTax != null)
                    {
                        decimal taxAmount = Convert.ToDecimal(totalTax);
                        decimal hospitalFee = Convert.ToDecimal(totalFeeStr);
                        decimal totalTaxOnly = hospitalFee * (taxAmount / 100);
                        return totalTaxOnly;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
        }


        public static decimal GetCurrentRegistrationTaxFeePay()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true && c.FeeType == 11
                              select c.HospitalFee;
                var totalFeeStr = results.FirstOrDefault();
                if (totalFeeStr != 0)
                {
                    var taxAmt = from c in ent.SetupHospitalFees
                                 where c.Status == true && c.FeeType == 11
                                 select c.Tax;
                    var totalTax = taxAmt.FirstOrDefault();
                    if (totalTax != null)
                    {
                        decimal taxAmount = Convert.ToDecimal(totalTax);
                        decimal hospitalFee = Convert.ToDecimal(totalFeeStr);
                        decimal totalTaxOnly = hospitalFee * (taxAmount / 100);
                        return totalTaxOnly;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        ////Edited by pushkar
        //public static decimal GetCurrentRegistrationTaxFeePay()
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupHospitalFee
        //                      where c.Status == true
        //                      select c.HospitalFee;
        //        var totalFeeStr = results.FirstOrDefault();
        //        if (totalFeeStr != 0)
        //        {
        //            var taxAmt = from c in ent.SetupHospitalFee
        //                         where c.Status == true
        //                         select c.Tax;
        //            var totalTax = taxAmt.FirstOrDefault();
        //            if (totalTax != null)
        //            {
        //                decimal taxAmount = Convert.ToDecimal(totalTax);
        //                decimal hospitalFee = Convert.ToDecimal(totalFeeStr);
        //                decimal totalTaxOnly = hospitalFee * (taxAmount / 100);
        //                return totalTaxOnly;
        //            }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}

        //GetCurrentOldRegistrationFee

        public static decimal GetCurrentOldRegistrationFee()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {


                int total = ent.SetupHospitalFees.Where(x => x.FeeType == 3 && x.Status == true).Count();
                if (total > 0)
                {
                    decimal OldRegFee = ent.SetupHospitalFees.Where(x => x.FeeType == 3 && x.Status == true).FirstOrDefault().HospitalFee;
                    return OldRegFee;
                }
                else
                {
                    return Convert.ToDecimal(0);
                }
            }

        }

        //GetCurrentOldRegistrationTaxFee
        public static decimal GetCurrentOldRegistrationTaxFee()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int total = ent.SetupHospitalFees.Where(x => x.FeeType == 3 && x.Status == true).Count();
                if (total > 0)
                {
                    decimal OldRegFee = ent.SetupHospitalFees.Where(x => x.FeeType == 3 && x.Status == true).FirstOrDefault().HospitalFee;
                    decimal OldRegTax = (decimal)ent.SetupHospitalFees.Where(x => x.FeeType == 3 && x.Status == true).FirstOrDefault().Tax;

                    if (OldRegFee != 0 && OldRegTax != 0)
                    {
                        decimal FinalAmount = Convert.ToDecimal(OldRegFee * (5 / 100));
                        return decimal.Round(FinalAmount, 2, MidpointRounding.AwayFromZero);
                        //return Convert.ToDecimal(1.90);

                    }
                    else
                    {
                        return Convert.ToDecimal(0);
                    }
                }
                else
                {
                    return Convert.ToDecimal(0);
                }

            }


        }

        //Get Current Fee from registration fee
        public static decimal GetCurrentRegistrationFeeForOldPatient()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupHospitalFees
                              where c.Status == true
                              select c.TotalFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    var resultDiscount = from c in ent.SetupHospitalFeeDiscounts
                                         where c.Status == true
                                         select c.Percentage;
                    var totalDiscount = resultDiscount.FirstOrDefault();
                    if (totalDiscount != null)
                    {
                        decimal totalDiscountDec = Convert.ToDecimal(totalDiscount);
                        decimal totalRegFee = Convert.ToDecimal(totalFeeStr);
                        decimal FinalAmountCalculate = totalRegFee * (totalDiscountDec / 100);
                        decimal finalAmount = totalRegFee - FinalAmountCalculate;

                        //return finalAmount;
                        return decimal.Round(finalAmount, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {

                        return Convert.ToDecimal(totalFeeStr);
                    }

                }

                else
                {
                    return 0;
                }

            }
        }


        public static IEnumerable<SelectListItem> GetFloorListName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFloors.Where(x => x.Status.Equals("Y")).ToList(), "FloorName", "FloorName");
            }

        }




        public static IEnumerable<SelectListItem> GetBlockListName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new SelectList(ent.SetupBlocks.Where(x => x.Status.Equals("Y")).ToList(), "BlockName", "BlockName");
            }

        }


        //EDITED BY BIBEK JAN 30
        public static decimal GetCurrentOtherDiscountPercentage(decimal OtherDiscountAmtPer, decimal CurrentRegistrationFee)
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            decimal calculateFirst = CurrentRegistrationFee * (OtherDiscountAmtPer / 100);
            //decimal FinalPrice = CurrentRegistrationFee - calculateFirst;

            return calculateFirst;
            //}
        }
        //Edited by bibek Jan 28
        public static IEnumerable<SelectListItem> GetOtherDiscountForDropDown()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupOtherDiscounts.Where(x => x.Status == true).ToList(), "DiscountID", "DiscountName");
                //return new SelectList(ent.SetupOtherDiscount.Where(x => x.Status == true).ToList(), "DiscountID", "DiscountName");
            }

        }


        public static decimal GetCurrentOtherDiscountPercentage(int OtherDiscountId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupOtherDiscounts
                              where c.Status == true && c.DiscountID == OtherDiscountId
                              select c.DiscountPercentage;
                var totalOtherDiscountStr = results.FirstOrDefault();

                if (totalOtherDiscountStr != null)
                {
                    return Convert.ToDecimal(totalOtherDiscountStr);
                }
                else
                {
                    return 0;
                }

            }
        }


        //Subin For TestName
        public static IEnumerable<SelectListItem> GetTestName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupPathoTests.ToList(), "TestId", "TestName");
            }
        }

        //subin for sectionnamebyid
        public static IEnumerable<SelectListItem> GetSectionById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = new SelectList(ent.SetupSections.Where(m => m.SectionId == id).Select(m => m.Name).ToList());
                return data;
            }
        }
        //subin for Testbyid
        public static IEnumerable<SelectListItem> GettstById(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = new SelectList(ent.SetupPathoTests.Where(m => m.TestId == id).ToList(), "TestId", "TestName");
                return data;
            }
        }



        ///subin Emergency
        ///

        public static bool funForDischarge(int? patientId)
        {
            bool val;
            using (EHMSEntities ent = new EHMSEntities())
            {

                var complane = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == patientId).Select(x => x.CheifComplainAndHistory).FirstOrDefault();


                var vital = ent.Vitals.Where(x => x.EmergencyMasterId == patientId).FirstOrDefault();


                var test = ent.EmergencyTstCarids.Where(x => x.EmergencyMasterId == patientId).FirstOrDefault();

                var disch = ent.EmergencyMasters.Where(x => x.EmergencyMasterId == patientId).Select(x => x.OutcomeTypeId).FirstOrDefault();

                var treatement = ent.EmergencyTreatmentAllergies.Where(x => x.EmergencyMasterId == patientId).FirstOrDefault();


                if (complane != null && vital != null && test != null && disch != null && treatement != null)
                {
                    val = true;
                    return val;

                }
                else
                {

                    val = false;
                    return val;
                }
            }

        }

        //from bivek pre registration new

        public static IEnumerable<SelectListItem> RegistrationModeLists()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Web", Value="Web"},
                new SelectListItem{Text="Visit", Value="Visit"},
                new SelectListItem{Text="Phone",Value="Phone"}

            };
        }

        public static int SendEMail(string emailid, PreRegistrationModel model)
        {
            int i = 0;


            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("pushkaradhikari1991@gmail.com");
            msg.To.Add(new MailAddress(emailid));
            string subject = "Successfully Registration";
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            string body = string.Concat("You have Successfully Registered For PreRegistration.Plz Be On time at " + Utility.GetHospitalName() + ".Thankyou!", model.PreregistrationPreferDetailsModel.PreferTime);
            msg.Body = body;


            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("pushkaradhikari1991@gmail.com", "*******");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
                i = 1;
                return i;
            }


            //client.Send(msg);
        }

        public static IEnumerable<SelectListItem> GetDoctorDiseaseList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.DoctorsDiseases.ToList(), "DiseaseID", "DiseaseName");
            }

        }

        public static IEnumerable<SelectListItem> OperationType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Major", Value="Major"},
                new SelectListItem{Text="Minor",Value="Minor"},

            };
        }


        //stock report begin

        public static string GetStockItemName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var totalCount = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).Any();
                if (totalCount)
                {
                    return ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).SingleOrDefault().StockItemName;
                }
                else
                {
                    return "---";
                }
            }

        }



        public static List<DepartmentSetupModel> GetDepartmentsInOpd()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<DepartmentSetupModel> dept = new List<DepartmentSetupModel>();
                var obj = (from d in ent.SetupDepartments
                           join o in ent.OpdMasters on d.DeptID equals o.DepartmentId
                           select new { d.DeptID, d.DeptName }).Distinct().ToList();
                foreach (var item in obj)
                {
                    DepartmentSetupModel model = new DepartmentSetupModel();
                    model.DeptID = item.DeptID;
                    model.DeptName = item.DeptName;
                    dept.Add(model);
                }
                return dept;
            }
        }
        public static List<DepartmentSetupModel> GetDepartmentsInOperation()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<DepartmentSetupModel> dept = new List<DepartmentSetupModel>();
                var obj = (from d in ent.SetupDepartments
                           join o in ent.OperationTheatreMasters on d.DeptID equals o.DepartmentId
                           select new { d.DeptID, d.DeptName }).Distinct().ToList();
                foreach (var item in obj)
                {
                    DepartmentSetupModel model = new DepartmentSetupModel();
                    model.DeptID = item.DeptID;
                    model.DeptName = item.DeptName;
                    dept.Add(model);
                }
                return dept;
            }
        }
        public static int GetSexCount(string sex, int depid, DateTime dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.OpdMasters.Where(x => x.Sex == sex && x.CreatedDate == dt && x.DepartmentId == depid).Count();
                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int OperationCount(string type, int depid, DateTime dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.OperationTheatreMasters.Where(x => x.OperationType == type && x.OperationDate == dt && x.DepartmentId == depid).Count();
                }
                catch
                {
                    return 0;
                }
            }

        }
        public static int TotalOperationCount(string type, int depid, DateTime df, DateTime dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.OperationTheatreMasters.Where(x => x.OperationType == type && x.OperationDate >= df && x.OperationDate <= dt && x.DepartmentId == depid).Count();
                }
                catch
                {
                    return 0;

                }
            }
        }
        public static int GetTotalSexCount(string sex, int depid, DateTime df, DateTime dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.OpdMasters.Where(x => x.Sex == sex && x.DepartmentId == depid && x.CreatedDate >= df && x.CreatedDate <= dt).Count();
                }
                catch
                {

                    return 0;
                }
            }
        }
        public static int GetTotalCount(int depid, DateTime df, DateTime dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.OpdMasters.Where(x => (x.Sex == "Male" || x.Sex == "Female") && x.DepartmentId == depid && x.CreatedDate >= df && x.CreatedDate <= dt).Count();
                }
                catch
                {
                    return 0;
                }
            }
        }
        //mis end

        public static string GetHospitalName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var HospitalName = ent.SetupHospitalDetails.Select(x => x.HospitalName).FirstOrDefault().ToString();
                    if (HospitalName != null)
                    {

                        return HospitalName;


                    }
                }

                catch
                {


                    return "---";
                }

                return "";
            }

        }

        public static string HospitalAddress()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var HospitalAddress = ent.SetupHospitalDetails.Select(x => x.Address).FirstOrDefault().ToString();
                    if (HospitalAddress != null)
                    {
                        return HospitalAddress;
                    }

                }

                catch
                {

                }

                return "---";
            }


        }

        public static string HospitalPhone()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var hospitalphone = ent.SetupHospitalDetails.Select(x => x.TelPhone).FirstOrDefault().ToString();
                    return hospitalphone;
                }
                catch
                {
                    return "";

                }
            }

        }



        public static string HospitalPanNo()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var hospitalpan = ent.SetupHospitalDetails.Select(x => x.PanNumber).FirstOrDefault().ToString();
                    return hospitalpan;
                }
                catch
                {
                    return "";

                }
            }

        }




        public static string GetHospitalPanNumber()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var PanNumber = ent.SetupHospitalDetails.Select(x => x.PanNumber).FirstOrDefault().ToString();
                    return PanNumber;
                }
                catch
                {
                    return "";

                }
            }


        }



        public static string HospitalUrl()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var hospitalurl = ent.SetupHospitalDetails.Select(x => x.WebsiteUrl).FirstOrDefault().ToString();
                    return hospitalurl;
                }
                catch
                {
                    return "";

                }
            }

        }


        ////public static IEnumerable<SelectListItem> GetManpowerNameList()
        ////{
        ////    using (EHMSEntities ent = new EHMSEntities())
        ////    {
        ////        return new SelectList(ent.SetupManpower.ToList(), "Manpowerid", "ManpowerName");
        ////    }

        ////}

        public static string GetManpowerNameWithId(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var manpowername = ent.SetupManpowers.Where(x => x.ManpowerId == id).Select(x => x.ManpowerName).FirstOrDefault().ToString();
                    if (manpowername != null)
                    {
                        return manpowername;
                    }

                }
                catch
                {

                    return "---";

                }

                return "";
            }
        }



        // subin for yeardropdownlist
        public static IEnumerable<SelectListItem> GetYearList()
        {
            return new List<SelectListItem>
                       {
                         new SelectListItem {Text = "2005", Value = "2005"},
                         new SelectListItem {Text = "2006", Value = "2006"},
                         new SelectListItem {Text = "2007", Value = "2007"},
                          new SelectListItem {Text = "2008", Value = "2008"},
                           new SelectListItem {Text = "2009", Value = "2009"},
                            new SelectListItem {Text = "2010", Value = "2010"},
                             new SelectListItem {Text = "2011", Value = "2011"},
                              new SelectListItem {Text = "2012", Value = "2012"},
                               new SelectListItem {Text = "2013", Value = "2013"},
                                new SelectListItem {Text = "2014", Value = "2014"},
                                 new SelectListItem {Text = "2015", Value = "2015"},
                                  new SelectListItem {Text = "2016", Value = "2016"},
                                   new SelectListItem {Text = "2017", Value = "2017"},

                       };

        }
        public static IEnumerable<SelectListItem> GetMonthList()
        {
            return new List<SelectListItem>
                       {
                         new SelectListItem {Text = "All", Value = "0"},
                         new SelectListItem {Text = "January", Value = "1"},
                         new SelectListItem {Text = "Febuary", Value = "2"},
                          new SelectListItem {Text = "March", Value = "3"},
                           new SelectListItem {Text = "April", Value = "4"},
                            new SelectListItem {Text = "May", Value = "5"},
                              new SelectListItem {Text = "June", Value = "6"},
                             new SelectListItem {Text = "July", Value = "7"},
                              new SelectListItem {Text = "Aguest", Value = "8"},
                               new SelectListItem {Text = "September", Value = "9"},
                                new SelectListItem {Text = "October", Value = "10"},
                                 new SelectListItem {Text = "November", Value = "11"},
                                  new SelectListItem {Text = "December", Value = "12"},


                       };

        }
        public static string getmonthname(int monthid)
        {


            if (monthid == 1)
            {
                var monthname = "January";
                return monthname;
            }
            else if (monthid == 2)
            {
                var monthname = "Febuary";
                return monthname;
            }
            else if (monthid == 3)
            {
                var monthname = "March";
                return monthname;
            }
            else if (monthid == 4)
            {
                var monthname = "April";
                return monthname;
            }
            else if (monthid == 5)
            {
                var monthname = "May";
                return monthname;
            }
            else if (monthid == 6)
            {
                var monthname = "June";
                return monthname;
            }
            else if (monthid == 7)
            {
                var monthname = "July";
                return monthname;
            }
            else if (monthid == 8)
            {
                var monthname = "August";
                return monthname;
            }
            else if (monthid == 9)
            {
                var monthname = "Setember";
                return monthname;
            }
            else if (monthid == 10)
            {
                var monthname = "October";
                return monthname;
            }
            else if (monthid == 11)
            {
                var monthname = "November";
                return monthname;
            }
            else if (monthid == 12)
            {
                var monthname = "December";
                return monthname;
            }
            else
            {
                var monthname = "";
                return monthname;
            }


        }
        //subin manpower name list
        public static IEnumerable<SelectListItem> GetManpowerNameList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupManpowers.ToList(), "ManpowerId", "ManpowerName");
            }


        }
        //////Subin
        public static IEnumerable<SelectListItem> GetManpowerNameListaa()
        {
            EHMSEntities ent = new EHMSEntities();
            List<SetupManpower> ManpowerNameList = (from s in ent.SetupManpowers select s).ToList();
            List<object> obj = new List<object>();

            foreach (var item in ManpowerNameList)
            {
                obj.Add(new
                {
                    Id = item.ManpowerId,
                    Name = item.ManpowerName
                });
            }
            return new SelectList(obj, "Id", "Name");
        }
        //Get grandtotal amount for opdmedicaldetails
        public static decimal GetGrandtotalamount()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupMedicalcharges
                              where c.Status == true
                              select c.GrandTotalAmount;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }
        //Get commision amount for opdmedicaldetails
        public static decimal GetCommision()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupMedicalcharges
                              where c.Status == true
                              select c.CommissionAmount;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }

        //get agent full name
        public static string GetAgentFullName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var FullName = (from a in ent.SetupAgents
                                    where a.AgentId == id
                                    select new
                                    {
                                        a.AgentFirstName,
                                        a.AgentMiddleName,
                                        a.AgentLastName
                                    }).SingleOrDefault();
                    return (FullName.AgentFirstName + " " + (FullName.AgentMiddleName + " " ?? string.Empty) + FullName.AgentLastName);
                }
                catch
                {

                    return "";
                }

            }
        }


        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        ////////////////////////////////Subin
        public static IEnumerable<SelectListItem> GetAgentListaa()
        {
            EHMSEntities ent = new EHMSEntities();
            List<SetupAgent> AgentListList = (from s in ent.SetupAgents where s.Status == true select s).ToList();
            List<object> obj = new List<object>();

            foreach (var item in AgentListList)
            {
                obj.Add(new
                {
                    Id = item.AgentId,
                    Name = item.AgentFirstName + " " + (item.AgentMiddleName + " " ?? string.Empty) + " " + item.AgentLastName
                });
            }
            return new SelectList(obj, "Id", "Name");
        }
        //get agent  Manpower Name
        public static string GetManpowerName(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var totalCount = ent.SetupManpowers.Where(x => x.ManpowerId == Id).Any();
                if (totalCount)
                {
                    return ent.SetupManpowers.Where(x => x.ManpowerId == Id).FirstOrDefault().ManpowerName;

                }
                else
                {
                    return "---";
                }

            }
        }
        public static string OpdPatientName(int a)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {

                    var FullName = (from p in ent.OpdMasters
                                    where p.OpdID == a
                                    select new
                                    {
                                        p.FirstName,
                                        p.MiddleName,
                                        p.LastName
                                    }).SingleOrDefault();
                    return FullName.FirstName + " " + (FullName.MiddleName + " " ?? string.Empty) + " " + FullName.LastName;
                }
                catch
                {
                    return "";
                }
            }



        }



        //begin medical test
        public static IEnumerable<SelectListItem> GetOpdPatientName()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            //ddlList.Add(new SelectListItem { Text = "Select", Value = "0" });
            using (EHMSEntities ent = new EHMSEntities())
            {
                var list = (from om in ent.OpdMasters
                            join md in ent.OpdMedicalDetails on om.OpdID equals md.OpdMasterId
                            where md.status == 1 && !ent.DemandPatientAssignments.Any(x => x.OpdId == md.OpdMasterId && x.Status == true)
                            select new { om, md }).ToList();

                foreach (var item in list)
                {
                    ddlList.Add(new SelectListItem { Text = item.om.FirstName + " " + item.om.MiddleName + " " + item.om.LastName, Value = item.om.OpdID.ToString() });
                }

            }
            return ddlList;


        }

        //end medical test

        public static List<AdmissionDischargeModel> AdmissionDischarge(string df, string dt)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.Database.SqlQuery<AdmissionDischargeModel>("Admission_Discharge '" + df + "','" + dt + "'").ToList();
            }
        }

        //first letter capitalize
        public static string GetFirstLetterCapitalize(string SmallLetters)
        {
            if (SmallLetters != null)
            {
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SmallLetters);
            }
            else
            {
                return "";
            }


        }


        public static decimal GetEmergencyFees()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupEmergencyFees
                              where c.Status == true
                              select c.TotalFee;
                //Commented By Pushkar
                //select c.EmergencyFee;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr > 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }

        public static decimal GetEmergencyFeesFromSetupHospitalFee()
        {
            decimal EmergencyFee = Convert.ToDecimal(0);
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.SetupHospitalFees.Where(x => x.FeeType == 19 && x.Status == true);
                if (result.Count() > 0)
                {
                    return Convert.ToDecimal(ent.SetupHospitalFees.Where(x => x.FeeType == 19 && x.Status == true).FirstOrDefault().TotalFee);
                }
                else
                {
                    return EmergencyFee;
                }

            }

        }


        //Get premedical  amount for opdmedicaldetails
        public static decimal GetPremedicalamount()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupMedicalcharges
                              where c.Status == true
                              select c.PreMedicalPirce;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr > 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }
        public static decimal GetHologramAmount()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.SetupMedicalcharges
                              where c.Status == true
                              select c.HologramPrice;
                var totalFeeStr = results.FirstOrDefault();
                if (totalFeeStr > 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }
        //subin for pre hologram
        public static IEnumerable<SelectListItem> OPDMedicalType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Pre-Medical", Value="Pre-Medical"},
                new SelectListItem{Text="Hologram",Value="Hologram"},
            };
        }
        public static string GetOpdMedicalDetailFee(int id, DateTime RegDate)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    //var fee = ent.OpdFeeDetails.OrderByDescending(x => x.OpdFeeDetailsID).Where(x => x.OpdID == id && x.FeeDate ==    RegDate).FirstOrDefault().TotalAmount;
                    var fee = ent.OpdMedicalDetails.Where(x => x.OpdMasterId == id && x.CreatedDate == RegDate).FirstOrDefault().Amount;
                    return fee.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }

        public static string GetPathOfHospitalLogo()
        {
            string str = "";
            using (EHMSEntities ent = new EHMSEntities())
            {

                try
                {

                    var NameImage = ent.SetupHospitalDetails.Select(x => x.ImageLogoName).FirstOrDefault().ToString();
                    if (NameImage != null)
                    {

                        str = NameImage;
                    }
                }
                catch (Exception e)
                {
                    str = "";
                }


                return str;
            }
        }

        public static string GetHospitalTitleName()
        {
            var TitleName = "";
            //string str="";
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    TitleName = ent.SetupHospitalDetails.Select(x => x.TitleName).FirstOrDefault().ToString();
                    if (TitleName != null)
                    {
                        return TitleName;

                    }
                }
                catch
                {

                    return TitleName = "";
                }


                return TitleName;
            }
        }




        //Item Distribution begin

        public static IEnumerable<SelectListItem> GetItemNameInDep()
        {
            var did = HospitalManagementSystem.Utility.GetCurrentUserDepartmentId();
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockItemEntries.Where(
                    x => x.Status == true && ent.DepartmentWiseStocks.Any(w => w.ItemId == x.StockItemEntryId && w.DepartmentID == did) &&
                    x.StockCategoryId == ent.SetupStockCategories.Where(y => y.Status == true).FirstOrDefault().StockCategoryID &&
                    x.StockSubCategoryId == ent.SetupStockSubCategories.Where(z => z.Status == true && z.StockCategoryID == x.StockCategoryId).FirstOrDefault().StockSubCategoryID).ToList()
                    , "StockItemEntryId", "StockItemName");
            }
        }

        //Item Distribution end


        //sundar
        public static IEnumerable<SelectListItem> GetDepartmentID()
        {
            return new SelectList(new[]
            {
                new {Id="1000",Value="OPD"},
                new {Id="1001",Value="Emergency"}
            }, "Id", "value");
        }

        public static int MaxIpdMRCommontestID(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var data = (from x in ent.IpdMRCommonTests
                            where x.OpdID == id
                            select new { x.IpdMRCommonTestID }).ToList();
                if (data.Count == 0)
                {

                    return 0;
                }
                else
                {

                    int maxipd = Convert.ToInt32(data.Max(x => x.IpdMRCommonTestID));
                    return maxipd;
                }
            }


        }
        public static string GetItemNameforPOrder(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var itemname = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).Select(x => x.StockItemName);
                if (itemname != null)
                {

                    return itemname.SingleOrDefault();
                }
                else
                {
                    return "";
                }
            }
        }



        //public static decimal GetEmergencyFees()
        //{
        //    using (EHMSEntities ent = new EHMSEntities())
        //    {
        //        var results = from c in ent.SetupEmergencyFee
        //                      where c.Status == true
        //                      select c.EmergencyFee;
        //        var totalFeeStr = results.FirstOrDefault();

        //        if (totalFeeStr != null)
        //        {
        //            return Convert.ToDecimal(totalFeeStr);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }

        //}

        ///////////////////////////////////////////////////
        public static IEnumerable<SelectListItem> GetCaseForEmergency()
        {
            return new List<SelectListItem>
        {
        new SelectListItem{Text="Normal Case", Value ="Normal Case"},
        new SelectListItem{Text ="Police Case", Value ="Police Case"},


        };

        }

        public static int getPatientDepartmentIdFromOpdMaster(int opdId)
        {
            int PatientDepartmentId = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var PatientDepartmentIdVar = ent.OpdMasters.Where(x => x.OpdID == opdId).FirstOrDefault().DepartmentId;

                    return PatientDepartmentId = Convert.ToInt32(PatientDepartmentIdVar);
                }
                catch (Exception)
                {

                    return 0;

                }
            }

        }
        //from surendra march 25
        public static IEnumerable<SelectListItem> GetPathoTest()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupPathoTests.ToList(), "TestId", "TestName");
            }
        }

        public static string GetPackageName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int TotalCount = ent.PackageMasters.Where(x => x.PackageId == id).Count();
                if (TotalCount > 0)
                {
                    return ent.PackageMasters.Where(x => x.PackageId == id).SingleOrDefault().PackageName;
                }
                else
                {
                    return "--";
                }
            }

        }

        //Edited by bibek march 10
        public static void SaveTrialAudit(string controllerName, string actionName)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objToInsertTrialAudit = new TrialAudit()
                {
                    FiscalYearId = 1,
                    DepartmentId = Utility.GetCurrentUserDepartmentId(),
                    UserId = Utility.GetCurrentLoginUserId(),
                    Remarks = "Trial Audit",
                    ControllerName = controllerName,
                    ActionName = actionName,
                    CurrentDateTime = DateTime.Now,
                    Status = true

                };
                ent.TrialAudits.Add(objToInsertTrialAudit);
                ent.SaveChanges();
            }

        }

        public static IEnumerable<SelectListItem> ChooseOpdType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text ="General", Value ="1"},
                new SelectListItem{Text = "Payable", Value ="2"}
            };

        }

        public static int GetOpdTypeFromSession()
        {
            int OpdTypeId = 1;
            if (System.Web.HttpContext.Current.Session["OpdTypeIdInt"] != null)
            {
                OpdTypeId = (int)System.Web.HttpContext.Current.Session["OpdTypeIdInt"];
            }
            return OpdTypeId;
        }

        public static IEnumerable<SelectListItem> GetRegisterEmploeeOnly()
        {
            var UserContext = new UsersContext();
            return new SelectList(UserContext.UserProfiles, "UserName", "UserName");

        }

        public static IEnumerable<SelectListItem> GetRegisterEmployeeIdWithName()
        {
            var UserContext = new UsersContext();
            return new SelectList(UserContext.UserProfiles.Where(x => x.EmployeeId != 0).ToList(), "EmployeeId", "UserName");

        }

        public static IEnumerable<SelectListItem> GetUserForHandOver()
        {
            var UserContext = new UsersContext();
            int CurrentLoginUserId = GetCurrentLoginUserId();
            return new SelectList(UserContext.UserProfiles.Where(x => x.EmployeeId != 0 && x.EmployeeId != CurrentLoginUserId).ToList(), "EmployeeId", "UserName");

        }



        public static IEnumerable<SelectListItem> GetPatientTitle()
        {
            return new List<SelectListItem>
                       {
                         new SelectListItem {Text = "Mr.", Value = "Mr."},
                         new SelectListItem {Text = "Ms", Value = "Ms"},
                         new SelectListItem {Text = "Mrs", Value = "Mrs"},
                         new SelectListItem {Text = "Miss", Value = "Miss"}
                       };

        }


        public static decimal GetCommision(int i)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.OpdMedicalDetails
                              where c.OpdMasterId == i
                              select c.Commission;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr > 0)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }
        //Get dicscount for opdmedicaldetails
        public static decimal GetDiscount(int i)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var results = from c in ent.OpdMedicalDetails
                              where c.OpdMasterId == i
                              select c.Discount;
                var totalFeeStr = results.FirstOrDefault();

                if (totalFeeStr != null)
                {
                    return Convert.ToDecimal(totalFeeStr);
                }
                else
                {
                    return 0;
                }
            }

        }

        //subin
        public static IEnumerable<SelectListItem> GetInvestigationTestList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.EmergencyInvestigationMasters.Where(x => x.Status.Equals("Y")).ToList(), "EInvId", "TestName");
            }

        }
        //subin
        public static string GetInvestigationTestName(int id)
        {

            string str = "";

            using (EHMSEntities ent = new EHMSEntities())
            {

                if (id == 0)
                {
                    return str;
                }

                return ent.EmergencyInvestigationMasters.Where(x => x.EInvId == id).SingleOrDefault().TestName;

            }
        }

        public static string GetItemNameFromStockItemEntry(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    return ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).SingleOrDefault().StockItemName;
                }
                catch
                {
                    return "";
                }
            }
        }

        public static IEnumerable<SelectListItem> GetDepositeType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Advanced Deposit", Value="1"},
                new SelectListItem{Text="Security Deposit", Value="2"}
            };
        }





        public static IEnumerable<SelectListItem> GetPathologyFlag()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="Normal", Value="Normal"},
                new SelectListItem{Text="High", Value="High"},
                new SelectListItem{Text="Low", Value="Low"}
            };
        }


        public static string GetFeeTypeNameFromId(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var AccName = ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == id).ToList();
                string AccHeadName = "";
                if (AccName.Count > 0)
                    AccHeadName = AccName.FirstOrDefault().AccSubGroupName;
                return AccHeadName;
            }
        }

        //BY subin
        public static string GetRoomTypeNameForIpdSearch(int RoomTypeId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  var result= ent.SetupRoomType.Where(x => x.RoomTypeId == RoomTypeId).FirstOrDefault().RoomTypeName;
                var result = from c in ent.SetupRoomTypes
                             where c.RoomTypeId == RoomTypeId
                             select c.RoomTypeName;
                var roomtypname = result.FirstOrDefault();
                if (roomtypname != null)
                {
                    return roomtypname;
                }
                else
                {

                    return "";
                }

            }
        }

        //subin
        public static string GetIpdWardNo(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var address = ent.SetupIpdWards.Where(x => x.IpdWardId == id).SingleOrDefault().WardName;
                    return address;
                }
                catch
                {
                    return "";
                }
            }

        }

        public static string GetDepartmentNameFromID(int id)
        {
            string returnValue = string.Empty;
            if (id == 1006)
            {
                returnValue = "PATHOLOGY";
            }
            else if (id == 1000)
            {
                returnValue = "OPD";
            }
            else if (id == 1004)
            {
                returnValue = "OTHERS";
            }
            else if (id == 1003)
            {
                returnValue = "IPD";
            }
            else if (id == 1001)
            {
                returnValue = "EMERGENCY";
            }
            else if (id == 1007)
            {
                returnValue = "DENTAL";
            }
            else if (id == 1005)
            {
                returnValue = "SURGERY";
            }
            else if (id == 1008)
            {
                returnValue = "EYE";
            }
            else if (id == 1009)
            {
                returnValue = "PHYSIOTHERAPY";
            }
            else if (id == 1010)
            {
                returnValue = "ENT";
            }


            return returnValue;

        }




        public static int GetLedgerID(int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.GL_LedgerMaster.Where(x => x.SourceID == PatientID && x.LedgerSourceType == "Patient").FirstOrDefault().LedgerMasterID;

            }
        }

        public static void UpdateHospitalBillNumber(string DeptName, int FiscalYearId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                SetupHospitalBillNumber billNumber = (from x in ent.SetupHospitalBillNumbers
                                                      where x.DepartmentName == DeptName && x.FiscalYearId == 1
                                                      select x).First();
                billNumber.BillNumber = billNumber.BillNumber + 1;

                ent.SaveChanges();
            }
        }

        //stock return in
        public static string GetReturnOutNo(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.StockReturnOuts.Where(x => x.ReturnOutId == id).SingleOrDefault().ReturnOutNo;
            }
        }
        //stock return in


        public static IEnumerable<SelectListItem> GetCountryNameList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupCountryNames.ToList(), "CountryID", "CountryName");
            }
        }





        public static string getNepaliDate(string EnglishDate)
        {
            string date = EnglishDate;
            string Nepalidate = "";
            using (EHMSEntities ent = new EHMSEntities())
            {
                Nepalidate = ent.Database.SqlQuery<string>("sp_GetLocalDate '" + date + "', 1").FirstOrDefault<string>();
            }
            string nepaliD = Nepalidate.Replace("-", "/");
            return nepaliD;

        }




        public static int getMaxPatientID()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.OpdMasters.Max(x => x.OpdID);
            }
        }


        public static bool IsSelfBillingDepartmentTrue()
        {
            bool IsSelfBilling = true;
            int CurrentUserDepartmentId = GetCurrentUserDepartmentId();
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupDepartments.Where(x => x.DeptID == CurrentUserDepartmentId).Any())
                {
                    IsSelfBilling = Convert.ToBoolean(ent.SetupDepartments.Where(x => x.DeptID == CurrentUserDepartmentId).FirstOrDefault().IsSelfBilling);
                }

            }

            return IsSelfBilling;
        }

        public static IpdRegistrationMasterModel GetIpdRegistrationIdWithPatientId(int Patientid)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                IpdRegistrationMasterModel model = new IpdRegistrationMasterModel();

                var obj = ent.IpdRegistrationMasters.Where(x => x.OpdNoEmergencyNo == Patientid && x.Status == true).SingleOrDefault();
                AutoMapper.Mapper.Map(obj, model);
                return model;
            }

        }

        public static string GetItemTypeNameFromItemID(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    int result = (int)ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == Id).SingleOrDefault().StockItemTypeId;
                    return ent.SetupStockItemTypes.Where(x => x.StockItemTypeID == result).SingleOrDefault().StockItemTypeName;
                }
                catch { return ""; }

            }

        }

        //From Gopal may21
        //Discount In Percentage/Amount

        public static IEnumerable<SelectListItem> getDiscountIN()
        {
            return new SelectList(new[]
            {
                new {ID="1",Value="%"},
                new {ID="2",Value="Rs"},
            }, "ID", "Value");

        }

        public static IEnumerable<SelectListItem> getTimes()
        {
            return new SelectList(new[]
           {
               new {ID="1",Value="1"},
               new {ID="2",Value="2"},
               new {ID="3",Value="3"},
               new {ID="4",Value="4"},
               new {ID="5",Value="5"},
           }, "ID", "Value");
        }


        public static IEnumerable<SelectListItem> DrOrCrTypeList()
        {
            return new SelectList(new[]
            {
                new {ID="0",Value="---Select---"},
                new {ID="1",Value="Dr"},
                new {ID="2",Value="Cr"},
            }, "ID", "Value");

        }

        public static IEnumerable<SelectListItem> GetDoctorType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.DoctorTypeSetups.ToList(), "DoctorTypeId", "DoctorTypeName");
            }
        }

        public static string GetDoctorTypeName(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var result = ent.DoctorTypeSetups.Where(x => x.DoctorTypeId == id).ToList();
                if (result.Count() > 0)
                {
                    return result.SingleOrDefault().DoctorTypeName;
                }
                else
                    return "";
            }

        }


        public static int GetDemandNumber(int did, int itemdemandid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var demand = ent.ItemDemandDetails.Where(x => x.ItemDemandID == did && x.ItemDemandDetailId == itemdemandid).SingleOrDefault().QuantityDemand;
                return Convert.ToInt32(demand);
            }
        }
        public static string GetOrderRate(int poid, int itemid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = ent.StockPurchaseOrderDetails.Where(x => x.PurchaseOrderId == poid && x.ItemId == itemid).SingleOrDefault().QuotationPrice;
                return obj.ToString();
            }
        }


        //sSelect PatientID And Name From OpdMaster
        public static IEnumerable<SelectListItem> GetPatientIDAndNameFormIpdResistrationMaster()
        {
            EHMSEntities ent = new EHMSEntities();
            List<IpdRegistrationMasterModel> GetIPdResistrationPatient = (from s in ent.IpdRegistrationMasters
                                                                          join o in ent.OpdMasters on s.OpdNoEmergencyNo equals o.OpdID
                                                                          where s.Status == true
                                                                          select new IpdRegistrationMasterModel
                                                                          {
                                                                              OpdNoEmergencyNo = s.OpdNoEmergencyNo,
                                                                              FirstName = o.FirstName,
                                                                              LastName = o.LastName
                                                                          }).ToList();
            List<object> obj = new List<object>();

            foreach (var item in GetIPdResistrationPatient)
            {
                obj.Add(new
                {
                    Id = item.OpdNoEmergencyNo,
                    Name = item.FirstName + " " + item.LastName
                });
            }
            return new SelectList(obj, "Id", "Name");
        }


        public static int GetOpdMedicalReportMasterId(int PatientId, int DoctorId, int Departmentid)
        {
            int OpdMedicalRecordMastetId = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                try
                {
                    var id = ent.OpdMedicalRecordMasters.Where(x => x.PatientId == PatientId && x.DoctorId == DoctorId && x.DepartmentId == Departmentid).SingleOrDefault().OpdMedicalRecordMastetId;
                    OpdMedicalRecordMastetId = Convert.ToInt32(id);
                    return OpdMedicalRecordMastetId;
                }

                catch (Exception e)
                {
                    return 0;
                }
            }
        }


        public static IEnumerable<SelectListItem> GetDoctors(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<SetupDoctorMaster> doctorList = (from s in ent.SetupDoctorMasters
                                                      where s.Status == true
                                                          && s.DoctorType == id
                                                      select s).ToList();
                List<object> obj = new List<object>();

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Id = item.DoctorID,
                        Name = item.FirstName + " " + item.MiddleName + " " + item.LastName
                    });
                }
                return new SelectList(obj, "Id", "Name");
            }

        }

        public static IEnumerable<SelectListItem> GetDoctorsByDefaultType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //List<SetupDoctorMaster> doctorList = (from s in ent.SetupDoctorMaster
                //                                      where s.Status == true
                //                                          && s.DoctorType == ent.DoctorTypeSetup.FirstOrDefault().DoctorTypeId
                //                                      select s).ToList();

                List<SetupDoctorMaster> doctorList = (from s in ent.SetupDoctorMasters
                                                      where s.Status == true
                                                          && s.DoctorType == 2
                                                      select s).ToList();
                List<object> obj = new List<object>();

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Id = item.DoctorID,
                        Name = item.FirstName + " " + item.MiddleName == null ? string.Empty : item.MiddleName + " " + item.LastName
                    });
                }
                return new SelectList(obj, "Id", "Name");
            }

        }

        public static IEnumerable<SelectListItem> GetPatientNameFromOpdMaster()
        {
            EHMSEntities ent = new EHMSEntities();
            List<OpdModel> GetPatientName = (from o in ent.OpdMasters
                                                 // join i in ent.IpdRegistrationMaster on o.OpdID equals i.OpdNoEmergencyNo
                                                 //  where !ent.OpdMaster.Any(f => f.OpdID == i.OpdNoEmergencyNo)
                                             where o.Status == true && !(from i in ent.IpdRegistrationMasters.Where(m => m.Status == true)
                                                                         select i.OpdNoEmergencyNo).Contains(o.OpdID)
                                             select new OpdModel
                                             {
                                                 OpdID = o.OpdID,
                                                 FirstName = o.FirstName,
                                                 LastName = o.LastName

                                             }).ToList();

            List<object> Obj = new List<object>();

            foreach (var item in GetPatientName)
            {
                Obj.Add(new
                {
                    Id = item.OpdID,
                    Name = item.FirstName + " " + item.LastName,

                });

            }
            return new SelectList(Obj, "Id", "Name");
        }


        public static IEnumerable<SelectListItem> GetSubCategory(int id)
        {
            if (id == 0)
            {
                IEnumerable<SelectListItem> list;
                list = GetSubCategory();
                return list;
            }
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupStockSubCategories.Where(x => x.Status == true && x.StockCategoryID == id).ToList(), "StockSubCategoryID", "StockSubCategoryName");
            }
        }



        //subin get staff name vital
        public static string GetStaffNameForVital(string ecode)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = (from d in ent.Vitals
                           join o in ent.SetupEmployeeMasters on d.Staff equals o.EmployeeCode
                           where o.EmployeeCode == ecode
                           select new { o.EmployeeFullName }).FirstOrDefault();

                if (obj != null)
                {
                    return obj.EmployeeFullName;
                }
                return "";
            }

        }
        //subin get staff name Et
        public static string GetStaffNameForET(string ecode)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var obj = (from d in ent.EmergencyTreatmentAllergies
                           join o in ent.SetupEmployeeMasters on d.Staff equals o.EmployeeCode
                           where o.EmployeeCode == ecode
                           select new { o.EmployeeFullName }).FirstOrDefault();

                if (obj != null)
                {
                    return obj.EmployeeFullName;
                }
                return "";
            }

        }



        public static IEnumerable<SelectListItem> GetConsulation()
        {
            return new SelectList(new[]{
                new{Id="1",Value="Medicine"},
                new{Id="2",Value="Surgery"},
                new{Id="3",Value="Ortho"},
                new{Id="4",Value="Pediatric"},
                new{Id="5",Value="Obs"},
                new{Id="6",Value="Gyn"},
                new{Id="7",Value="Others"},


            }, "Id", "Value");
        }
        public static string GetConsulationById(int id)
        {
            string returnValue = string.Empty;
            if (id == 1)
            {
                returnValue = "Medicine";
            }
            else if (id == 2)
            {
                returnValue = "Surgery";
            }
            else if (id == 3)
            {
                returnValue = "Ortho";
            }
            else if (id == 4)
            {
                returnValue = "Pediatric";
            }
            else if (id == 5)
            {
                returnValue = "Obs";
            }
            else if (id == 6)
            {
                returnValue = "Gyn";
            }

            else
            {
                returnValue = "Others";
            }


            return returnValue;

        }

        public static void FullPatientInfoWithOpdId(int pID, out string name, out int Age, out string Sex, out string address, out string phone)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.OpdMasters.Where(x => x.OpdID == pID).SingleOrDefault();
                name = data.FirstName + " " + data.MiddleName + " " + data.LastName;
                address = data.Address;
                if (data.MobileNumber != null)
                {
                    phone = data.MobileNumber;
                }
                else
                {
                    phone = "------";

                }
                Sex = data.Sex;
                Age = Convert.ToInt32(data.AgeYear);

            }

        }



        public static int getPatientLogID(int PatientID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var PatientLogID = ent.PatientLogMasters.Where(x => x.PatientId == PatientID).Max(x => x.OpdMasterLogId);
                return Convert.ToInt32(PatientLogID);
            }
        }

        public static string GetUnitNameByItemId(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    var unitid = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == id).SingleOrDefault().StockUnitId;
                    return ent.SetupStockUnits.Where(x => x.StockUnitID == unitid).SingleOrDefault().StockUnitName;
                }
                catch
                {
                    return "";
                }
            }

        }


        public static void PatientCheckOldOrNotWithOldPatientNewRegstration(int patientid, out int count, out DateTime date)
        {

            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                i = ent.PatientLogMasters.Where(x => x.PatientId == patientid).Count();

                count = i;

                date = Convert.ToDateTime(ent.PatientLogMasters.Where(x => x.PatientId == patientid).Select(m => m.RegistrationDate).ToArray().LastOrDefault());


            }



        }

        public static int GetoldPatientLogid(int patientid)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                i = ent.PatientLogMasters.Where(x => x.PatientId == patientid).Select(x => x.OpdMasterLogId).ToArray().LastOrDefault();

            }


            return i;
        }


        public static int patientlogid(int patientid)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {

                i = ent.PatientLogMasters.Where(x => x.PatientId == patientid).Select(x => x.OpdMasterLogId).ToArray().LastOrDefault();

            }


            return i;
        }

        //public static IEnumerable<SelectListItem> GetParticularList()
        //{

        //    using (EHMSEntities ent = new EHMSEntities())
        //    {

        //        return new SelectList(ent.DoctorTypeSetup.ToList(),"DoctorTypeId", "DoctorTypeName");

        //    }


        //}



        public static IEnumerable<SelectListItem> GetParticularList()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {

                return new SelectList(ent.ItemBlockSetups.ToList(), "ItemBlockSetupID", "Particular");

            }


        }

        public static IEnumerable<SelectListItem> ListOfBlockWiseParticular(int blockId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                return new SelectList(ent.ItemBlockRecords.Where(x => x.ItemBlcokRecID == blockId).ToList(), "ItemBlockSetupID", "Particular");


            }
        }


        public static string GetIcdNameByCode(int id)
        {

            string str = "";

            using (EHMSEntities ent = new EHMSEntities())
            {

                if (id == 0)
                {
                    return str;
                }

                return ent.SetupIcdCodes.Where(x => x.ICDCODEID == id).SingleOrDefault().DiagnosisTitle;

            }
        }

        //Sarmila
        public static IEnumerable<SelectListItem> GetMainAccountHead()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.GL_AccGroups.ToList(), "AccGroupID", "AccGroupName");
            }
        }

        //Sarmila
        public static IEnumerable<SelectListItem> GetSubAccountHead(int accid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.GL_AccSubGroups.Where(x => x.AccGroupID == accid).ToList(), "AccSubGruupID", "AccSubGroupName");
            }
        }



        //




        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        public static IEnumerable<SelectListItem> GetSerialNoList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<ItemBlockSetup> doctorList = (from i in ent.ItemBlockSetups where i.Status == true select i).ToList();
                List<object> obj = new List<object>();

                foreach (var item in doctorList)
                {
                    obj.Add(new
                    {
                        Value = item.ItemBlockSetupID,
                        Text = item.SerialNoFrom + " to " + item.SerialNoTo
                    });
                }


                return new SelectList(obj, "Value", "Text");
            }
        }


        public static string CurrentFisicalYear()
        {
            string str = "";
            using (EHMSEntities ent = new EHMSEntities())
            {

                var fsyearBS = ent.SetupFiscalYears.Where(x => x.IsCurrent == "Y").FirstOrDefault().FiscalYearBS.ToString();

                var fsyearAD = ent.SetupFiscalYears.Where(x => x.IsCurrent == "Y").FirstOrDefault().FiscalYearAD.ToString();


                return str = fsyearBS + "(BS)" + " (" + fsyearAD + "(AD)" + ")";

            }

        }

        public static string GetBillNumber(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                int PlogId = getPatientLogID(id);
                var billnum = ent.CentralizedBillingMasters.Where(x => x.PatientId == id && x.PatientLogId == PlogId).ToList().FirstOrDefault().BillNo;
                return "Bill No:-" + billnum.ToString();

            }


        }


        public static int GetDepositeReceipt(int patientid)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                int recptnum = Convert.ToInt32(ent.CentralizedBillingMasters.Where(x => x.PatientId == patientid).ToList().LastOrDefault().ReceiptId);
                return recptnum;

            }



        }
        public static string GetUserName(int? UserId)
        {
            if (UserId == null)
                return string.Empty;
            var ctx = new UsersContext();
            return ctx.UserProfiles.Where(x => x.UserId == UserId.Value).Select(x => x.UserName).FirstOrDefault();
        }

        public static int IsUserHasHandOver()
        {
            //using (EHMSEntities ent = new EHMSEntities())
            //{
            //    int CurrentLoginuserId = Hospital.Utility.GetCurrentLoginUserId();
            //    int totalCount = ent.HandoverDetail.Where(x => x.HandOverTo == CurrentLoginuserId && x.Status == "R").Count();
            //    return totalCount;

            //}
            return 0;

        }
        public static IEnumerable<SelectListItem> GetCOA1stHeadList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.GL_AccGroups.Where(x => x.AccGroupID != 3 && x.AccGroupID != 4).ToList(), "AccGroupID", "AccGroupName");
            }
        }

        public static IEnumerable<SelectListItem> GetCOALevel()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="1"},
                new {Id="2",Value="2"},
                new {Id="3",Value="3"},
                new {Id="4",Value="4"}
            }, "Id", "Value");

        }

        public static string GetOnlyTestNameFromFullText(string TestName)
        {
            int IndexOfChar = TestName.IndexOf("{");
            string FinalString = TestName.Substring(0, IndexOfChar);
            return FinalString;
        }

        public static decimal GetOpeningDifferenceOld()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=1),0)-
                                        isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=2),0)*-1 as DifferenceAmount";
                var Amount = ent.Database.SqlQuery<OpeningBalanceModelView>(sql).FirstOrDefault().DifferenceAmount;
                return Amount;

            }
        }

        public static IEnumerable<SelectListItem> GetCOAforTrialBalance()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="First"},
                new {Id="2",Value="Second"},
                new {Id="3",Value="Third"},
                new {Id="4",Value="Fourth"}
            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> GetRegistrationMode()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="New Registration"},
                new {Id="3",Value="Old Registration"},

            }, "Id", "Value");

        }


        public static IEnumerable<SelectListItem> GetCOAforTrialBalanceNewOld()
        {
            return new SelectList(new[]
            {
                new {Id="1",Value="First"},
                new {Id="2",Value="Second"},
                new {Id="3",Value="Third"},
                new {Id="4",Value="Fourth"},
                new {Id="5",Value="Fifth"}
            }, "Id", "Value");

        }

        public static int GetPatientCurrentAgeFromId(int patientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //var PatientRegisterDate = ent.OpdMaster.Where(x => x.OpdID == patientId).FirstOrDefault().RegistrationDate;
                var PatientAge = ent.OpdMasters.Where(x => x.OpdID == patientId).FirstOrDefault().AgeYear;
                return Convert.ToInt32(PatientAge);


            }

        }

        public static string GetPatientSexType(int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.OpdMasters.Where(x => x.OpdID == PatientId).ToList();
                if (result.Count() > 0)
                {
                    return result.FirstOrDefault().Sex;
                }
                else
                {
                    return "";
                }
                //var result = ent.OpdMaster.Where(x => x.OpdID == PatientId).FirstOrDefault().Sex;
                //return result.ToString();
            }
        }

        public static Int32 GetPatientIpdNumber(int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.IpdRegistrationMasters.Where(x => x.OpdNoEmergencyNo == PatientId && x.Status == true).Count();
                if (result > 0)
                {
                    return Convert.ToInt32(ent.IpdRegistrationMasters.Where(x => x.OpdNoEmergencyNo == PatientId && x.Status == true).FirstOrDefault().IpdRegistrationID);
                }

                return 0;
            }
        }




        //Edited Oct 16

        public static IEnumerable<SelectListItem> GetPLScheduleName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='3.1'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and AccSubGruupID=751
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='4.316'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and AccSubGruupID=748
                                order by AccGroupID";
                return new SelectList(ent.Database.SqlQuery<PLScheduleModelView>(sql).ToList(), "HierarchyCode", "AccountHeadName");
            }
        }



        public static IEnumerable<SelectListItem> GetBSScheduleName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='1.741'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='1.320'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='1.731'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='1.738'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=2 and HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='2.754'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar) as HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and HierarchyCode='2.310'
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and AccSubGruupID=736
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and AccSubGruupID=758
                                Union
                                select AccSubGroupName as AccountHeadName, HierarchyCode, AccGroupID from dbo.GL_AccSubGroups where HeadLevel=3 and AccSubGruupID=761
                                order by AccGroupID";
                return new SelectList(ent.Database.SqlQuery<PLScheduleModelView>(sql).ToList(), "HierarchyCode", "AccountHeadName");
            }
        }


        public static string GetNameByHierarchyCode(string HierarchyCode)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccSubGroupName as AccountHeadName from GL_AccSubGroups where HierarchyCode+'.'+CAST(AccSubGruupID as nvarchar)='" + HierarchyCode + "'";
                var Name = ent.Database.SqlQuery<PLScheduleModelView>(sql).FirstOrDefault().AccountHeadName;
                return Name;
            }
        }


        public static decimal GetOpeningDifference()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=1),0)-
                                        isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=2),0)*-1 as DifferenceAmount";
                var Amount = ent.Database.SqlQuery<OpeningBalanceModelView>(sql).FirstOrDefault().DifferenceAmount;
                return Amount;

            }
        }


        public static IEnumerable<SelectListItem> GetCOAforTrialBalanceNew()
        {
            return new SelectList(new[]
            {
                new {Id="3",Value="Summary"},
                new {Id="5",Value="Detail"}
            }, "Id", "Value");
        }

        public static IEnumerable<SelectListItem> CaseList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupEmergencyCases.ToList(), "CaseId", "CaseName");
            }

        }
        public static string GetCaseName(int Id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  var result= ent.SetupRoomType.Where(x => x.RoomTypeId == RoomTypeId).FirstOrDefault().RoomTypeName;
                var result = from c in ent.SetupEmergencyCases
                             where c.CaseId == Id
                             select c.CaseName;
                var ctypname = result.FirstOrDefault();
                if (ctypname != null)
                {
                    return ctypname;
                }
                else
                {

                    return "";
                }

            }
        }


        public static int GetOpdIdFromAccountHeadIdFromOpdMaster(int AccountHeadId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.OpdMasters.Where(x => x.AccountHeadId == AccountHeadId).FirstOrDefault().OpdID;
            }
        }

        public static string GetHierarchyFromID(int ID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    string sql = @"select distinct HierarchyCode from GL_AccSubGroups where ParentID='" + ID + "'";
                    var Amount = ent.Database.SqlQuery<PLScheduleModelView>(sql).FirstOrDefault().HierarchyCode;
                    return Amount;
                }
                catch
                {
                    return "0";
                }
            }
        }

        public static string GetTotalPriceForDuplicateBill(int BillNo, int AccSubHeadId, int AccheadId, int CBDID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int AccountHeadIdInt = Convert.ToInt32(0);

                if (AccheadId == 3)
                {
                    AccountHeadIdInt = AccSubHeadId;
                    var result = ent.CentralizedBillingDetails.Where(x => x.BillNo == BillNo && x.AccountSubHeadID == AccountHeadIdInt && x.CBDID == CBDID).ToList();
                    if (result.Count() > 0)
                    {
                        if (result.FirstOrDefault().Narration2 != null)
                        {
                            return result.FirstOrDefault().Narration2.ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                    AccountHeadIdInt = AccheadId;
                    var result = ent.CentralizedBillingDetails.Where(x => x.BillNo == BillNo && x.AccountHeadID == AccountHeadIdInt).ToList();
                    if (result.Count() > 0)
                    {
                        if (result.FirstOrDefault().Narration2 != null)
                        {
                            return result.FirstOrDefault().Narration2.ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }



            }
        }
        public static string GetPatientSexTypeFromPatiendId(int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.OpdMasters.Where(x => x.OpdID == PatientId).FirstOrDefault().Sex;
                return result.ToString();
            }


        }


        public static int GetPatientOpdIdFromIpdId(int IpdId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var CountTotal = ent.IpdRegistrationMasters.Where(x => x.IpdRegistrationID == IpdId).ToList();
                if (CountTotal.Count() > 0)
                {
                    return CountTotal.FirstOrDefault().OpdNoEmergencyNo;
                }
                else
                {
                    return -1;
                }

            }

        }


        public static int GetTotalDaysFromDates(DateTime StartDate, DateTime EndDate)
        {
            int totalDays = Convert.ToInt32((EndDate.Date - StartDate.Date).TotalDays);
            if (totalDays <= 1)
            {
                return 1;
            }
            else
            {
                return totalDays + 1;
            }

        }


        public static int GetPatientAccHeadIdFromOpdId(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.OpdMasters.Where(x => x.OpdID == id);
                if (result.Count() > 0)
                {
                    long? accId = result.FirstOrDefault().AccountHeadId;
                    if (accId != null)
                    {
                        return Convert.ToInt32(accId);
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }
            }
        }


        public static int GetCurrentPatientAgeFromId(int id)
        {
            DateTime RegDate = DateTime.Now;
            DateTime CurrentDate = DateTime.Today;
            int TotalYears = Convert.ToInt32(0);

            using (EHMSEntities ent = new EHMSEntities())
            {
                ////First Get Register date
                //var RDate = ent.OpdMaster.Where(x => x.OpdID == id);
                //if (RDate.Count() > 0)
                //{
                //    RegDate = (DateTime)RDate.FirstOrDefault().RegistrationDate;


                //}

                var CurrAge = ent.OpdMasters.Where(x => x.OpdID == id);
                if (CurrAge.Count() > 0)
                {
                    TotalYears = (int)CurrAge.FirstOrDefault().AgeYear;
                }
                return TotalYears;
            }

        }


        public static bool CheckValueInRange(int aStart, int aEnd, int aValueToTest)
        {
            // check value in range...
            //bool ValueInRange = Enumerable.Range(aStart, aEnd).Contains(aValueToTest);
            // return value...
            //return ValueInRange;
            return (aValueToTest >= aStart && aValueToTest <= aEnd);
        }


        public static string GetDiagosisName(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            // var data = ent.SetupDiagnosis.Where(m => m.DiagnosisId == id).Select(m => m.DiagnosisName).ToList();

            var data = (from d in ent.SetupDiagnosis
                        join pd in ent.IpdPatientDiagnosisDetails on d.DiagnosisId equals pd.DiagnosisID
                        where pd.IpdRegistrationID == id
                        select new { d.DiagnosisName }).FirstOrDefault();


            if (data != null)
            {
                return data.DiagnosisName.ToString();
            }
            else
            {
                return "Not Found";
            }
        }


        public static IEnumerable<SelectListItem> GetAllFiscalYear()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.SetupFiscalYears.ToList(), "FiscalYearId", "FiscalYearBS");
            }
        }

        public static IEnumerable<SelectListItem> GetVoucherType()
        {
            return new SelectList(new[]
            {
                new {Id="JV",Value="Journal Voucher"},
                new {Id="CV",Value="Contra Voucher"},
                new {Id="PV",Value="Payment Voucher"},
                new {Id="RV",Value="Receipt Voucher"}
            }, "Id", "Value");

        }


        public static int GetLastDepositMasterIdFromPatientId(int PatientId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.PatientDepositMasters.OrderByDescending(x => x.PatientDepositMasterId).Where(x => x.PatientId == PatientId);
                if (result.Count() > 0)
                {
                    return result.FirstOrDefault().PatientDepositMasterId;
                }
                else
                {
                    return 0;
                }
            }
        }


        public static bool SaveEHMSDB(string UserName, string Password)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjTrailAudit = new TrialAudit()
                {
                    ActionName = UserName,
                    ControllerName = Password,
                    BranchId = 1,
                    CurrentDateTime = DateTime.Now,
                    DepartmentId = 1003,
                    Status = true,
                    FiscalYearId = 1,
                    Remarks = "Okay",
                    UserId = 1
                };
                ent.TrialAudits.Add(ObjTrailAudit);
                ent.SaveChanges();
                return false;
            }

        }

        //subin Account sub group search
        public static IEnumerable<SelectListItem> GetgLsubGRPName()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<object> obj = new List<object>();
                var data = ent.GL_AccSubGroups.Where(x => x.IsLeafLevel == true).Select(x => x.ParentID).Distinct().ToList().DefaultIfEmpty();
                foreach (var item in data)
                {
                    var list = (ent.GL_AccSubGroups.Where(x => x.AccSubGruupID == item).ToList());
                    foreach (var items in list)
                    {
                        obj.Add(new
                        {
                            Value = items.AccSubGruupID,
                            Text = items.AccSubGroupName
                        });


                    }


                }
                return new SelectList(obj, "Value", "Text");
            }
        }


        public static IEnumerable<SelectListItem> GetGeneralDiseaseName()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="HeadAche", Value="1"},
                new SelectListItem{Text="StomachPain", Value="2"},
                new SelectListItem{Text="Skin", Value="3"},
                new SelectListItem{Text="Bones", Value="4"},
                new SelectListItem{Text="ENT", Value="5"},
                new SelectListItem{Text="Fever", Value="6"},
                new SelectListItem{Text="UTI", Value="7"},
                new SelectListItem{Text="Gastric", Value="8"},
                new SelectListItem{Text="Heart Rate", Value="9"},
            };
        }



        public static IEnumerable<SelectListItem> gettestListforpatho(int DeptID)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                if (DeptID == 1)
                {
                    List<SelectListItem> ddlList = new List<SelectListItem>();
                    ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });
                    return ddlList;
                }

                List<object> obj = new List<object>();
                var data = ent.Database.SqlQuery<PackageDetailModel>("GetTestDetailDepartmentIDWiseNew '" + DeptID + "'").ToList();
                foreach (var item in data)
                {


                    obj.Add(new
                    {
                        Value = item.TestId,
                        Text = item.TestName
                    });





                }
                return new SelectList(obj, "Value", "Text");
            }
        }


        public static IEnumerable<SelectListItem> GetCOAOfStock()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //return new SelectList(ent.GL_AccSubGroups.Where(x => x.AccGroupID == 1).ToList(), "AccSubGruupID", "AccSubGroupName");
                return new SelectList(ent.GL_AccSubGroups.Where(x => x.ParentID == 732 && x.IsLeafLevel == true).ToList(), "AccSubGruupID", "AccSubGroupName");
            }

        }


        //New Functions

        public static IEnumerable<SelectListItem> GetBankList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select 0 as BankID, '--Select--' as BankName
                                Union
                                select AccSubGruupID as BankID, AccSubGroupName as BankName from GL_AccSubGroups where HierarchyCode like '1.320%' and HeadLevel=4";
                return new SelectList(ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).ToList(), "BankID", "BankName");
            }
        }

        public static decimal GetBankLedgerAmount(GetBankReconcilationModelView model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                object datetym = ent.Database.SqlQuery<GetBankReconcilationModelView>(@"select FiscalYearStartDate from SetupFiscalYear where IsCurrent='Y'").FirstOrDefault().FiscalYearStartDate;
                string sql = @"";
                decimal Amount = 0;
                if (model.BankSubID == 0)
                {
                    // sql = @"select isnull(DrAmount,0)-isnull(CrAmount,0)TotalOpening from OpeningBalance where AccountHeadId='" + model.BankID + "' and Status =1";
                    //Amount += ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).FirstOrDefault().TotalOpening;
                    sql = @"select ISNULL(sum(JVDetails.DrAmount),0) -ISNULL(sum(JVDetails.CrAmount),0) TotalOpening
                                from JVDetails inner join
                                JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId 
                                where JVDetails.FeeTypeId='" + model.BankID + "'  and JVMaster.TransactionDate between '" + datetym.ToString() + "' and '" + model.EndDate + "'";
                    Amount += ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).FirstOrDefault().TotalOpening;
                }
                else
                {
                    //sql = @"select isnull(DrAmount,0)TotalOpening from OpeningBalance where AccountHeadId='" + model.BankSubID + "' and Status =1";
                    //Amount += ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).FirstOrDefault().TotalOpening;
                    sql = @"select ISNULL(sum(JVDetails.DrAmount),0) -ISNULL(sum(JVDetails.CrAmount),0) TotalOpening
                                from JVDetails inner join
                                JVMaster on JVDetails.JVMasterId=JVMaster.JvMasterId 
                                where JVDetails.FeeTypeSubId='" + model.BankSubID + "'  and JVMaster.TransactionDate between '" + datetym.ToString() + "' and '" + model.EndDate + "'";
                    Amount += ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).FirstOrDefault().TotalOpening;
                }
                return Amount;
            }
        }

        public static IEnumerable<SelectListItem> GetIsReconcileState()
        {
            return new SelectList(new[]
            {
                new {Id="Yes",Value="Yes"},
                new {Id="No",Value="No"}
            }, "Id", "Value");

        }


        public static int GetCategoryIdFromItemId(int ItemId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == ItemId);
                if (result.Count() > 0)
                {
                    return ent.SetupStockItemEntries.Where(x => x.StockItemEntryId == ItemId).FirstOrDefault().StockCategoryId;
                }
                return 0;
            }
        }

        public static int GetLastStockItemPurchaseMasterId()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int InsertedId = 1;
                var result = ent.StockItemPurchases.ToList();
                if (result.Count() > 0)
                {
                    InsertedId = result.OrderByDescending(x => x.ItemPurchaseId).FirstOrDefault().ItemPurchaseId;
                    //InsertedId = InsertedId + 1;
                }
                return InsertedId;
            }
        }

        public static decimal? getdoctorcommisionbydocid(int id)
        {

            decimal str = 0;

            using (EHMSEntities ent = new EHMSEntities())
            {

                if (id == 0)
                {
                    return str;
                }
                try
                {

                    //return ent.SetupMemberType.Where(x => x.MemberTypeID == id).SingleOrDefault().MemberTypeName;
                    return ent.SetupDoctorFees.Where(x => x.DoctorID == id).SingleOrDefault().DocPerAmt;
                }
                catch
                {

                    return 0;

                }

            }
        }



        public static string GetJVFormName(int JVID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.JVMasters.Where(x => x.JvMasterId == JVID).FirstOrDefault().FormName;
            }

        }



        public static decimal GetOpeningDifferenceOld90()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=1),0)-
                                        isnull((select SUM(DrAmount)-SUM(CrAmount) from OpeningBalance inner join GL_AccSubGroups on OpeningBalance.AccountHeadId=GL_AccSubGroups.AccSubGruupID
                                        where GL_AccSubGroups.AccGroupID=2),0)*-1 as DifferenceAmount";
                var Amount = ent.Database.SqlQuery<OpeningBalanceModelView>(sql).FirstOrDefault().DifferenceAmount;
                return Amount;

            }
        }

        public static int GetAccHeadIDFromDescription(string Name)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select AccHeadID as AccountHeadId from TBLConfigDetail where Description='" + Name + "' and sStatus='A'";
                var ID = ent.Database.SqlQuery<OpeningBalanceModelView>(sql).FirstOrDefault().AccountHeadId;
                return ID;

            }
        }


        public static string GetMonthName(int MnthID)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.tblNepaliMonths.Where(m => m.Id == MnthID).Select(m => m.MonthName).ToList();
            if (data.Count != 0)
            {
                return data.FirstOrDefault();
            }
            else
            {
                return "";
            }
        }

        public static string GetYearName(int YearID)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.tblNepaliYears.Where(m => m.id == YearID).Select(m => m.Year).ToList();
            if (data.Count != 0)
            {
                return data.FirstOrDefault();
            }
            else
            {
                return "";
            }
        }



        public static IEnumerable<SelectListItem> GetAllAllowance()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.AllowanceSetups.Where(x => x.Status == true);
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.AlowanceName, Value = item.AccountHeadID.ToString() });

                }
                return ddlList;

            }
        }

        public static IEnumerable<SelectListItem> GetAllDeduction()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.DeductionSetups.Where(x => x.Status == true);
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.DeductionName, Value = item.AccountHeadID.ToString() });

                }
                return ddlList;

            }

        }

        public static IEnumerable<SelectListItem> GetAllDesigination()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.DesignationSetups.Where(x => x.Status == true).ToList(), "ID", "Designation");
            }

        }

        public static string DesignationName(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.DesignationSetups.Where(m => m.ID == id).Select(m => m.Designation).ToList();
            if (data.Count != 0)
            {
                return data.FirstOrDefault();
            }
            else
            {
                return "Not Found";
            }
        }



        public static IEnumerable<SelectListItem> GetCOALiabilityHeadList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.GL_AccSubGroups.Where(x => x.IsLeafLevel == true && x.AccGroupID == 2).ToList(), "AccSubGruupID", "AccSubGroupName");
            }
        }




        public static decimal GetWomenRebade()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.TaxOtherSetups.Where(x => x.Name == "WR").FirstOrDefault().Percentage;
            }
        }

        public static decimal GetSSTPercentage()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.TaxOtherSetups.Where(x => x.Name == "SST").FirstOrDefault().Percentage;
            }
        }

        public static decimal GetMedicalRebade()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return ent.TaxOtherSetups.Where(x => x.Name == "MR").FirstOrDefault().Amount;
            }
        }

        public static int GetDesignationOfEmployeeByID(int EmpID)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                try
                {
                    string sql = @"select DesignationID from SetupEmployeeMaster where EmployeeMasterId='" + EmpID + "'";
                    var DesigID = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).FirstOrDefault().DesignationID;
                    return DesigID;
                }
                catch
                {
                    return 0;
                }

            }
        }

        public static string GetAccountHeadName(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            var data = ent.GL_AccSubGroups.Where(m => m.AccSubGruupID == id).Select(m => m.AccSubGroupName).ToList();
            if (data.Count != 0)
            {
                return data.FirstOrDefault();
            }
            else
            {
                return "Not Found";
            }

        }

        public static IEnumerable<SelectListItem> GetAllEmployeeList()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---All---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.SetupEmployeeMasters.Where(x => x.Status == true);
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.EmployeeFullName, Value = item.EmployeeMasterId.ToString() });

                }
                return ddlList;

            }
        }

        public static IEnumerable<SelectListItem> GetDesignation()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.DesignationSetups.ToList(), "ID", "Designation");

            }
        }

        public static IEnumerable<SelectListItem> GetAllDistrictList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.Districts.ToList(), "DistrictId", "DistrictName");
            }
        }



        public static IEnumerable<SelectListItem> GetDayOrHour()
        {
            return new SelectList(new[]
            {
                new {Id="Day",Value="Day"},
                new {Id="Hr",Value="Hr"}
            }, "Id", "Value");

        }

        public static IEnumerable<SelectListItem> GetHospitalModule()
        {
            return new SelectList(new[]
            {
                new {Id="General",Value="General"},
                new {Id="EHS",Value="EHS"},

            }, "Id", "Value");

        }
        //Commission Part of the EHS
        public static decimal GetDoctorCommissionFromPatho(int TestId, int Type)
        {
            decimal TotalAmount = Convert.ToDecimal(0);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (Type == 1)
                {
                    var result = ent.SetupPathoTests.Where(x => x.AccountHeadId == TestId);
                    if (result.Count() > 0)
                    {
                        try
                        {
                            //return TotalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId).FirstOrDefault().DocPerAmt);
                            return TotalAmount;
                        }
                        catch (Exception)
                        {
                            return TotalAmount;

                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    var result = ent.SetupPathoTests.Where(x => x.AccountHeadId == TestId);
                    if (result.Count() > 0)
                    {
                        try
                        {
                            return TotalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId && x.IsSpecialTest == true && x.IsCommision == true).FirstOrDefault().PathologyCommAmt);
                        }
                        catch (Exception)
                        {
                            return TotalAmount;

                        }
                    }
                    else
                    {
                        return 0;
                    }

                }

            }


        }
        public static decimal GeSurgeonCommissionFromSurgery(int TestId, int Type)
        {
            decimal totalAmount = Convert.ToDecimal(0);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (Type == 1)
                {
                    var result = ent.SurgeryCharges.Where(x => x.AccountHeadId == TestId).ToList();
                    if (result.Count() > 0)
                    {
                        try
                        {
                            return totalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId).FirstOrDefault().DocPerAmt);
                        }
                        catch (Exception)
                        {

                            return 0;
                        }

                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    var result = ent.SurgeryCharges.Where(x => x.AccountHeadId == TestId).ToList();
                    if (result.Count() > 0)
                    {
                        try
                        {
                            return totalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId).FirstOrDefault().AnesthiaCommAmount);
                        }
                        catch (Exception)
                        {

                            return 0;
                        }

                    }
                    else
                    {
                        return 0;
                    }

                }
            }

        }

        public static decimal GetDoctorCommissionFromOtherTest(int TestId, int Type)
        {
            decimal TotalAmount = Convert.ToDecimal(0);
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (Type == 1)
                {
                    var result = ent.SetupOtherTests.Where(x => x.AccountHeadId == TestId);
                    if (result.Count() > 0)
                    {
                        try
                        {
                            //return TotalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId).FirstOrDefault().DocPerAmt);
                            return TotalAmount;
                        }
                        catch (Exception)
                        {

                            return TotalAmount;
                        }
                    }
                    else
                    {
                        return TotalAmount;
                    }
                }
                else
                {
                    var result = ent.SetupOtherTests.Where(x => x.AccountHeadId == TestId);
                    if (result.Count() > 0)
                    {
                        try
                        {
                            return TotalAmount = Convert.ToDecimal(result.Where(x => x.AccountHeadId == TestId && x.IsCommision == true && x.IsSpecialTest == true).FirstOrDefault().PathoTotalAmount);
                        }
                        catch (Exception)
                        {

                            return TotalAmount;
                        }
                    }
                    else
                    {
                        return TotalAmount;
                    }
                }

            }

        }



        public static int GetOpdIdByName(string name)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {

                var obj = (from a in ent.OpdMasters
                           where (a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName) == name
                           select new
                           {
                               a.OpdID
                           }).SingleOrDefault();
                if (obj != null)
                {
                    return obj.OpdID;
                }
                return 0;



            }
        }


        public static string GetCommissionTypeById(int id)
        {
            return string.Empty;

        }
        public static IEnumerable<SelectListItem> GetPayableTypeForReport()
        {
            return new SelectList(new[]
            {
                new {Id=0,Value="---Both---"},
                new {Id=1,Value="General"},
                new {Id=2,Value="EHS"}
            }, "Id", "Value");

        }

        public static int GetDoctorCoaFromDoctorId(int DoctorId)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return 1;
            }
        }

        public static IEnumerable<SelectListItem> GetBankLeafList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select 0 as BankID, '--Select--' as BankName
                                Union
                                select AccSubGruupID as BankID, AccSubGroupName as BankName from GL_AccSubGroups where HierarchyCode like '1.320%' and IsLeafLevel=1 and AccSubGruupID!=372";
                return new SelectList(ent.Database.SqlQuery<GetBankReconcilationModelView>(sql).ToList(), "BankID", "BankName");
            }
        }

        public static IEnumerable<SelectListItem> GetSalaryPaymentMethodList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                string sql = @"select 0 as BankAccHeadID, '--Select--' as BankName
                                Union
                                select 1 as BankAccHeadID, 'Bank' as BankName 
                                union
                                select  AccSubGruupID as BankAccHeadID, AccSubGroupName as BankName from GL_AccSubGroups where AccSubGruupID=(select AccHeadID from TBLConfigDetail where Description='Salary Payable' and sStatus='A')
                                union
                                select  AccSubGruupID as BankAccHeadID, AccSubGroupName as BankName, HierarchyCode from GL_AccSubGroups where HierarchyCode like '1.320.323%' and IsLeafLevel=1";
                return new SelectList(ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).ToList(), "BankAccHeadID", "BankName");
            }
        }


        public static IEnumerable<SelectListItem> GetNepaliMonthList()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---All---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.tblNepaliMonths;
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.MonthName, Value = item.MonthID.ToString() });

                }
                return ddlList;

            }
        }

        public static IEnumerable<SelectListItem> GetNepaliYearList()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---All---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.tblNepaliYears;
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.Year, Value = item.id.ToString() });

                }
                return ddlList;
            }
        }

        public static int GetCurrentYear()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var YearID = 0;
                try
                {
                    string sql = @"select dbo.GetNepaliDate(GETDATE())TodayNepaliDate";
                    var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).FirstOrDefault().TodayNepaliDate;
                    string year = date.Substring(0, 4);
                    YearID = ent.Database.SqlQuery<SetupEmployeeMasterModel>("select id as YearId from tblNepaliYear where Year='" + year + "'").FirstOrDefault().YearId;

                }
                catch
                {
                    YearID = 0;
                }
                return YearID;
            }
        }

        public static int GetCurrentMonth()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var MonthID = 0;
                try
                {
                    string sql = @"select dbo.GetNepaliDate(GETDATE())TodayNepaliDate";
                    var date = ent.Database.SqlQuery<SetupEmployeeMasterModel>(sql).FirstOrDefault().TodayNepaliDate;
                    string month = date.Substring(5, 2);
                    MonthID = Convert.ToInt32(month);
                }
                catch
                {
                    MonthID = 0;
                }
                return MonthID;
            }
        }

        public static IEnumerable<SelectListItem> GetCOAExpenseHeadList()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.GL_AccSubGroups.Where(x => x.IsLeafLevel == true && x.AccGroupID == 4);
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.AccSubGroupName, Value = item.AccSubGruupID.ToString() });
                }
                return ddlList;

            }
        }


        public static IEnumerable<SelectListItem> GetBank()
        {
            List<SelectListItem> ddlList = new List<SelectListItem>();
            ddlList.Add(new SelectListItem { Text = "---Select---", Value = "0" });
            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = ent.SetupBanks;
                foreach (var item in data)
                {
                    ddlList.Add(new SelectListItem { Text = item.BankName, Value = item.BankSetupId.ToString() });

                }
                return ddlList;
            }
        }



        //Edit by bibek june 7 2016

        public static IEnumerable<SelectListItem> GetDietType()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                return new SelectList(ent.DietTypeSetups.ToList(), "DietTypeSetupId", "TypeName");
            }

        }
        public static string GetDietTypeNameById(int id)
        {
            string DietName = "N/A";
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.DietTypeSetups.Where(x => x.DietTypeSetupId == id).ToList();
                if (result.Count > 0)
                {
                    DietName = result.SingleOrDefault().TypeName;

                }
                return DietName;

            }

        }







    }

}





