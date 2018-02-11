using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class ScanFileUPloadPorvider
    {
        public int insert(ScanFileUploadModel model)
        {

            using (EHMSEntities ent = new EHMSEntities())

            {
                var SaveScanFilUpdate = AutoMapper.Mapper.Map<ScanFileUploadModel, ScanFileUpload>(model);
                SaveScanFilUpdate.CreateBy = 1;
                SaveScanFilUpdate.CreateDateBy = DateTime.Now;
                SaveScanFilUpdate.Status = true;
                ent.ScanFileUploads.Add(SaveScanFilUpdate);
                ent.SaveChanges();

            }
            return 1;
        }
    }
}