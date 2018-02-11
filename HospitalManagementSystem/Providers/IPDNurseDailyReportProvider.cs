using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;
using HospitalManagementSystem;

namespace HospitalManagementSystem.Providers
{
    public class IpdNurseDailyReportProvider
    {

        public IpdSearchResults GetNurseDailyReport(int IpdResistration)
        {
            IpdSearchResults model = new IpdSearchResults();

            using (EHMSEntities ent = new EHMSEntities())
            {

                var IpdMRCommonTest = ent.IpdMRCommonTests.Where(m => m.IpdRegistrationID == IpdResistration).ToList();

                foreach (var item in IpdMRCommonTest)
                {
                    var ViewIpdMrCommonTest = new IpdMRCommonTestModel();
                    {
                        ViewIpdMrCommonTest.CommonTestName = item.CommonTestName;
                        ViewIpdMrCommonTest.Details = item.Details;
                        ViewIpdMrCommonTest.IpdMRcCommonTesDate = (DateTime)item.IpdMRcCommonTesDate;
                    }
                    model.IpdMRCommontestList.Add(ViewIpdMrCommonTest);
                }

                var IpdMRMainTest = ent.IpdMrMainTests.Where(m => m.IpdRegistrationMasterID == IpdResistration).ToList();

                foreach (var items in IpdMRMainTest)
                {
                    var ViewMainTest = new IpdMrMainTestModel();
                    {
                        ViewMainTest.SectionId = items.SectionID;
                        ViewMainTest.TestID = items.TestID;
                        ViewMainTest.ShortDescription = items.ShortDescription;
                        ViewMainTest.LongDescription = items.LongDescription;
                    }
                    model.IpdMrMainTestList.Add(ViewMainTest);
                }

                var IpdMRMedecinTestRepor = ent.IpdMrMedicineRecords.Where(m => m.IpdRegisterationID == IpdResistration).ToList();


                foreach (var itemss in IpdMRMedecinTestRepor)
                {
                    var ViewMedicineRecord = new IpdMedicalRecord();
                    {
                        ViewMedicineRecord.MedicineName = itemss.MedicineName;
                        ViewMedicineRecord.Doses = itemss.Doses;
                        ViewMedicineRecord.DosesTotalDays = itemss.DosesTotalDays;
                        ViewMedicineRecord.DosesTimes = itemss.DosesTimes;
                    }
                    model.IpdMedecalRecordList.Add(ViewMedicineRecord);
                }


                return model;

            }
        }

    }
}