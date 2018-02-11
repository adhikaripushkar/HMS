using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;


namespace HospitalManagementSystem.Providers
{
    public class IpdMedicalRecordProvider
    {

        public List<IpdMedicalRecord> IpdMedicalRecordData(int id, int ipdid)
        {
            List<IpdMedicalRecord> IpdMedicalRecordData = new List<IpdMedicalRecord>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = from a in ent.OpdMasters
                             join b in ent.IpdPatientroomDetails on a.OpdID equals b.OpdNoEmergencyNo
                             where a.OpdID == id && b.Status == true
                             select new IpdMedicalRecord
                             {
                                 FullName = a.FirstName + " " + (a.MiddleName + " " ?? string.Empty) + a.LastName,
                                 RegistrationDate = (DateTime)a.RegistrationDate,
                                 WardNo = b.WardNo,
                                 RoomNo = b.RoomNo,
                                 BedNo = b.BedNo,
                                 PatientID = id,
                                 IpdRegisterationID = ipdid
                             };
                foreach (var item in result)
                {
                    IpdMedicalRecordData.Add(item);
                }

            }

            return IpdMedicalRecordData;
        }

        public int Insert(IpdMedicalRecord model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtosave = AutoMapper.Mapper.Map<IpdMedicalRecord, IpdMrMedicineRecord>(model);
                objtosave.IpdRegisterationID = model.IpdRegisterationID;
                objtosave.PatientID = model.PatientID;

                foreach (var item in model.IpdMedicalRecordList)
                {
                    objtosave.Createdby = 1;
                    objtosave.CreatedDate = DateTime.Now;
                    objtosave.status = true;


                    objtosave.Doses = item.Doses;
                    objtosave.DosesTimes = item.DosesTimes;
                    objtosave.DosesTotalDays = item.DosesTotalDays;
                    objtosave.MedicineName = item.MedicineName;
                    ent.IpdMrMedicineRecords.Add(objtosave);
                    i = ent.SaveChanges();

                }


            }

            return i;
        }
        public List<SectionTestCheckBox> SectionCheckListModelList()
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                IpdMrMainTestModel models = new IpdMrMainTestModel();
                List<IpdMrMainTestModel> model = new List<IpdMrMainTestModel>();
                List<SectionTestCheckBox> SectionTestCheckBoxList = new List<SectionTestCheckBox>();

                var result = (from a in ent.SetupPathoTests

                              select new SectionTestCheckBox
                              {
                                  SectionId = (int)a.SectionId,

                                  TestID = a.TestId,
                                  TestName = a.TestName

                              }).ToList();



                foreach (var item in result)
                {

                    SectionTestCheckBoxList.Add(item);

                }

                return SectionTestCheckBoxList;
            }
        }
        public List<IpdMedicalRecord> IpdMedicalRecordValue(int id, int ipdid)
        {
            EHMSEntities ent = new EHMSEntities();
            List<IpdMedicalRecord> model = new List<IpdMedicalRecord>();
            return new List<IpdMedicalRecord>(AutoMapper.Mapper.Map<IEnumerable<IpdMrMedicineRecord>, IEnumerable<IpdMedicalRecord>>(ent.IpdMrMedicineRecords).Where(x => x.PatientID == id && x.IpdRegisterationID == ipdid));

        }
        public List<IpdMedicalRecord> GetEdit(int id)
        {
            EHMSEntities ent = new EHMSEntities();
            List<IpdMedicalRecord> model = new List<IpdMedicalRecord>();
            return new List<IpdMedicalRecord>(AutoMapper.Mapper.Map<IEnumerable<IpdMrMedicineRecord>, IEnumerable<IpdMedicalRecord>>(ent.IpdMrMedicineRecords).Where(x => x.IpdMrMedicalRecordID == id));

        }


        public int Update(IpdMedicalRecord model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtoEdit = ent.IpdMrMedicineRecords.Where(x => x.IpdMrMedicalRecordID == model.IpdMrMedicalRecordID).FirstOrDefault();
                objtoEdit.MedicineName = model.MedicineName;
                objtoEdit.Doses = model.Doses;
                objtoEdit.DosesTimes = model.DosesTimes;
                objtoEdit.DosesTotalDays = model.DosesTotalDays;

                ent.Entry(objtoEdit).State = System.Data.EntityState.Modified;
                ent.SaveChanges();


            }
            return 1;



        }
        public int Delete(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtodel = ent.IpdMrMedicineRecords.Where(x => x.IpdMrMedicalRecordID == id).FirstOrDefault();

                ent.IpdMrMedicineRecords.Remove(objtodel);
                ent.SaveChanges();

            }
            return 1;

        }
        public List<IpdMrMainTestModel> IpdMrMainTestModelList(int section)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<IpdMrMainTestModel> model = new List<IpdMrMainTestModel>();

                var result = (from a in ent.SetupPathoTests
                              where a.SectionId == section
                              select new IpdMrMainTestModel
                              {

                                  TestID = a.TestId,
                                  TestName = a.TestName

                              }).Distinct();
                foreach (var item in result)
                {

                    model.Add(item);

                }

                return model;

            }

        }
        public int InsertCheckBox(IpdMrMainTestModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {


                foreach (var item in model.SectionTestCheckBoxList)
                {
                    if (item.isSelected == true)
                    {
                        var objtosave = AutoMapper.Mapper.Map<IpdMrMainTestModel, IpdMrMainTest>(model);
                        objtosave.PatientID = model.PatientID;
                        objtosave.IpdRegistrationMasterID = model.IpdRegistrationID;
                        objtosave.CreatedBy = 1;
                        objtosave.CreatedDate = DateTime.Today;
                        objtosave.SectionID = item.SectionId;
                        objtosave.TestID = item.TestID;
                        objtosave.ShortDescription = model.ShortDescription;
                        objtosave.LongDescription = model.LongDescription;
                        objtosave.Status = true;
                        objtosave.InsertedDate = DateTime.Today;
                        ent.IpdMrMainTests.Add(objtosave);

                    }


                }
                i = ent.SaveChanges();


            }

            return i;
        }
        public List<IpdMrMainTestModel> ListSectionCheckBox(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                List<IpdMrMainTestModel> ListValue = new List<IpdMrMainTestModel>();

                var result = (from a in ent.IpdMrMainTests
                              join b in ent.SetupPathoTests on a.TestID equals b.TestId
                              join c in ent.SetupSections on a.SectionID equals c.SectionId
                              where a.PatientID == id
                              select new IpdMrMainTestModel
                              {
                                  TestName = b.TestName,
                                  SectionName = c.Name,
                                  PatientID = a.PatientID,
                                  IpdRegistrationID = a.IpdRegistrationMasterID,
                                  IpdMrMainTestID = a.IpdMrMainTestID,
                                  SectionId = a.SectionID
                              });

                foreach (var item in result)
                {
                    ListValue.Add(item);

                }

                return ListValue;
            }


        }
        public int DeleteMainTest(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtodel = ent.IpdMrMainTests.Where(x => x.IpdMrMainTestID == id).FirstOrDefault();

                ent.IpdMrMainTests.Remove(objtodel);
                ent.SaveChanges();

            }
            return 1;

        }
        public List<IpdMrMainTestModel> EditMainTest(int id)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                IpdMrMainTestModel model = new IpdMrMainTestModel();
                return new List<IpdMrMainTestModel>(AutoMapper.Mapper.Map<IEnumerable<IpdMrMainTest>, IEnumerable<IpdMrMainTestModel>>(ent.IpdMrMainTests).Where(x => x.IpdMrMainTestID == id));



            }


        }
        public int UpdateMainModel(IpdMrMainTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var objtodel = ent.IpdMrMainTests.Where(x => x.IpdMrMainTestID == model.IpdMrMainTestID).FirstOrDefault();

                ent.IpdMrMainTests.Remove(objtodel);
                ent.SaveChanges();

                foreach (var item in model.SectionTestCheckBoxList)
                {
                    if (item.isSelected == true)
                    {
                        var objtoEdit = AutoMapper.Mapper.Map<IpdMrMainTestModel, IpdMrMainTest>(model);
                        //  var objtoEdit = ent.IpdMrMainTest.Where(x => x.IpdMrMainTestID == model.IpdMrMainTestID).FirstOrDefault();
                        //var objtosave = AutoMapper.Mapper.Map<IpdMrMainTestModel, IpdMrMainTest>(model);
                        objtoEdit.PatientID = model.PatientID;
                        objtoEdit.IpdRegistrationMasterID = model.IpdRegistrationID;
                        objtoEdit.CreatedBy = 1;
                        objtoEdit.CreatedDate = DateTime.Now;
                        objtoEdit.SectionID = item.SectionId;
                        objtoEdit.TestID = item.TestID;
                        objtoEdit.ShortDescription = model.ShortDescription;
                        objtoEdit.LongDescription = model.LongDescription;
                        objtoEdit.Status = true;
                        objtoEdit.InsertedDate = DateTime.Now;
                        ent.IpdMrMainTests.Add(objtoEdit);

                    }


                }
                ent.SaveChanges();





            }
            return 1;



        }

        //sCommonTest Details
        public virtual List<IpdMRCommonTestModel> GetByIpdRegistrationID(int id)
        {
            List<IpdMRCommonTestModel> model = new List<IpdMRCommonTestModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                model = AutoMapper.Mapper.Map<IEnumerable<IpdMRCommonTest>, IEnumerable<IpdMRCommonTestModel>>(ent.IpdMRCommonTests).Where(m => m.IpdRegistrationID == id).ToList();

            }

            return model;
        }
        public int Insert(IpdMRCommonTestModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())
            {
                var data = AutoMapper.Mapper.Map<IpdMRCommonTestModel, IpdMRCommonTest>(model);

                foreach (var item in model.IpdMRCommonTestModeList)
                {
                    data.OpdID = model.OpdID;
                    data.IpdRegistrationID = model.IpdRegistrationID;
                    data.CommonTestName = item.CommonTestName;
                    data.Details = item.Details;
                    data.IpdMRcCommonTesDate = item.IpdMRcCommonTesDate;
                    data.CreateBy = 1;
                    data.CreateByDate = DateTime.Today;
                    ent.IpdMRCommonTests.Add(data);
                    ent.SaveChanges();

                }

            }

            return 1;
        }
        //sEdit in IpdMRCommonTest
        public virtual List<IpdMRCommonTestModel> GetByIPdMRCommonTest(int id)
        {
            List<IpdMRCommonTestModel> model = new List<IpdMRCommonTestModel>();
            using (EHMSEntities ent = new EHMSEntities())
            {
                model = AutoMapper.Mapper.Map<IEnumerable<IpdMRCommonTest>, IEnumerable<IpdMRCommonTestModel>>(ent.IpdMRCommonTests).Where(m => m.IpdMRCommonTestID == id).ToList();

            }

            return model;
        }
        //sUpdate IPdMRCommonTes
        public int Update(IpdMRCommonTestModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ToUpdate = ent.IpdMRCommonTests.Where(m => m.IpdMRCommonTestID == model.IpdMRCommonTestID).SingleOrDefault();
                model.CreateBy = ToUpdate.CreateBy;
                model.CreateByDate = ToUpdate.CreateByDate;
                model.OpdID = ToUpdate.OpdID;
                model.IpdRegistrationID = ToUpdate.IpdRegistrationID;
                AutoMapper.Mapper.Map(model, ToUpdate);
                ent.Entry(ToUpdate).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
            }
            return 1;
        }
        public int DeleteIpdMRCommonTest(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var ObjDelete = ent.IpdMRCommonTests.Where(m => m.IpdMRCommonTestID == id).FirstOrDefault();
                ent.IpdMRCommonTests.Remove(ObjDelete);
                ent.SaveChanges();
            }
            return 1;
        }
    }


}