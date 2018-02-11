using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockSupplierProvider
    {
        public List<StockSupplierModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockSupplierModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockSupplier>, IEnumerable<StockSupplierModel>>(ent.SetupStockSuppliers.Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }


        public int Insert(StockSupplierModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupStockSuppliers.Where(x => x.StockSupplierName == model.StockSupplierName).Any())
                {
                    return 0;
                }

                //first add in coa, Sundry Creditors add accgroupid =2 parenet id 1706 , hierarchycode 2.1253.1256 head level 5 isleave yes
                var Globj = new GL_AccSubGroups()
                {
                    AccGroupID = 2,
                    AccSubGroupName = model.StockSupplierName,
                    ParentID = 1706,
                    HeadLevel = 5,
                    HierarchyCode = "2.1253.1256.1706",
                    IsLeafLevel = true,
                    Status = true,
                    CreatedBy = Utility.GetCurrentLoginUserId(),
                    CreatedDate = DateTime.Today,
                    Remarks = "Automatic from stock",

                };
                ent.GL_AccSubGroups.Add(Globj);
                ent.SaveChanges();

                var objToSave = AutoMapper.Mapper.Map<StockSupplierModel, SetupStockSupplier>(model);
                objToSave.Status = true;
                objToSave.CreatedBy = 1;
                objToSave.AccountHeadId = Globj.AccSubGruupID;
                objToSave.CreatedDate = DateTime.Now;

                ent.SetupStockSuppliers.Add(objToSave);
                ent.SaveChanges();
            }
            return 1;

        }

    }
}