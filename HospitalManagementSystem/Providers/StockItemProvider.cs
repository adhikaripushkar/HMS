using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Providers
{
    public class StockItemProvider
    {
        public List<StockItemModel> GetList()
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                //  this.ContextOptions.LazyLoadingEnabled = false;
                return new List<StockItemModel>(AutoMapper.Mapper.Map<IEnumerable<SetupStockItemType>, IEnumerable<StockItemModel>>(ent.SetupStockItemTypes.Where(x => x.Status == true)));
                // return new List<StudentRecordModel>(AutoMapper.Mapper.Map<IEnumerable<StudentRecords>, IEnumerable<StudentRecordModel>>(ent.StudentRecords));  

            }
        }

        public int Insert(StockItemModel model)
        {
            int i = 0;
            using (EHMSEntities ent = new EHMSEntities())
            {
                if (ent.SetupStockItemTypes.Where(x => x.StockItemTypeName == model.StockItemTypeName).Any())
                {
                    return 0;
                }
                var objToSave = AutoMapper.Mapper.Map<StockItemModel, SetupStockItemType>(model);
                objToSave.Status = true;
                objToSave.CreatedBy = 1;
                objToSave.CreatedDate = DateTime.Now;
                ent.SetupStockItemTypes.Add(objToSave);
                i = ent.SaveChanges();
            }
            return i;

        }

        public int Delete(int id)
        {
            int i = 0;
            EHMSEntities ent = new EHMSEntities();
            var obj = ent.SetupStockItemTypes.Where(x => x.StockItemTypeID == id).SingleOrDefault();
            ent.SetupStockItemTypes.Remove(obj);
            i = ent.SaveChanges();
            return i;
        }

        public bool DeleteStockGroup(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemGroupSetups.Where(x => x.StockGroupingId == id).SingleOrDefault();
                result.Status = false;
                ent.Entry(result).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
                return true;
            }

        }

        public bool DeleteStockLocation(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemLocationSetups.Where(x => x.StockItemLocationId == id).SingleOrDefault();
                result.Status = false;
                ent.Entry(result).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
                return true;
            }

        }
        public bool DeleteStockRack(int id)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemRackSetups.Where(x => x.StockItemRackSetupId == id).SingleOrDefault();
                result.Status = false;
                ent.Entry(result).State = System.Data.EntityState.Modified;
                ent.SaveChanges();
                return true;
            }

        }

        public StockItemModel GetGroupDetails()
        {
            StockItemModel model = new StockItemModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemGroupSetups.Where(x => x.Status == true);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var ViewModel = new StockItemGroupSetupViewModel()
                        {
                            GroupName = item.GroupName,
                            StockGroupingId = item.StockGroupingId,
                            FromId = (int)item.FromId,
                            ToId = (int)item.ToId

                        };
                        model.StockItemGroupSetupViewModelList.Add(ViewModel);

                    }
                }

                return model;
            }
        }

        public bool InsertGroupDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var groupDtls = new StockItemGroupSetup()
                {
                    GroupName = model.ObjStockItemGroupSetupViewModel.GroupName,
                    FromId = model.ObjStockItemGroupSetupViewModel.FromId,
                    ToId = model.ObjStockItemGroupSetupViewModel.ToId,
                    Status = true,
                    Narration = "stock Group"
                };
                ent.StockItemGroupSetups.Add(groupDtls);
                ent.SaveChanges();

                return true;
            }

        }

        public bool UpdateGroupDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemGroupSetups.Where(x => x.StockGroupingId == model.ObjStockItemGroupSetupViewModel.StockGroupingId).ToList();
                if (result.Count() > 0)
                {
                    var value = ent.StockItemGroupSetups.Where(x => x.StockGroupingId == model.ObjStockItemGroupSetupViewModel.StockGroupingId).FirstOrDefault();

                    value.GroupName = model.ObjStockItemGroupSetupViewModel.GroupName;
                    value.Status = true;
                    value.FromId = model.ObjStockItemGroupSetupViewModel.FromId;
                    value.ToId = model.ObjStockItemGroupSetupViewModel.ToId;
                    ent.Entry(value).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();

                }
                return true;
            }
        }

        public bool UpdateLocationDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemLocationSetups.Where(x => x.StockItemLocationId == model.ObjStockItemLocationViewModel.StockItemLocationId).ToList();
                if (result.Count() > 0)
                {
                    var value = ent.StockItemLocationSetups.Where(x => x.StockItemLocationId == model.ObjStockItemLocationViewModel.StockItemLocationId).FirstOrDefault();
                    value.LocatonName = model.ObjStockItemLocationViewModel.LocatonName;
                    value.Status = true;
                    value.DepartmentId = model.ObjStockItemLocationViewModel.DepartmentId;
                    value.WardID = model.ObjStockItemLocationViewModel.WardID;
                    value.BlockId = model.ObjStockItemLocationViewModel.BlockId;
                    ent.Entry(value).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();

                }
                return true;
            }
        }

        public bool UpdateRackDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemRackSetups.Where(x => x.StockItemRackSetupId == model.ObjStockItemRackViewModel.StockItemRackSetupId).ToList();
                if (result.Count() > 0)
                {
                    var value = ent.StockItemRackSetups.Where(x => x.StockItemRackSetupId == model.ObjStockItemRackViewModel.StockItemRackSetupId).FirstOrDefault();
                    value.RackName = model.ObjStockItemRackViewModel.RackName;
                    value.RackNumber = model.ObjStockItemRackViewModel.RackNumber;
                    value.LocationId = model.ObjStockItemRackViewModel.LocationId;
                    value.Status = true;
                    ent.Entry(value).State = System.Data.EntityState.Modified;
                    ent.SaveChanges();

                }
                return true;
            }
        }




        public bool InsertLocationDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var groupDtls = new StockItemLocationSetup()
                {
                    LocatonName = model.ObjStockItemLocationViewModel.LocatonName,
                    Status = true,
                    DepartmentId = model.ObjStockItemLocationViewModel.DepartmentId,
                    WardID = model.ObjStockItemLocationViewModel.WardID,
                    BlockId = model.ObjStockItemLocationViewModel.BlockId,
                    Narration = "Stock Location Setup"

                };
                ent.StockItemLocationSetups.Add(groupDtls);
                ent.SaveChanges();

                return true;
            }

        }

        public bool InsertRackDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                var groupDtls = new StockItemRackSetup()
                {
                    RackName = model.ObjStockItemRackViewModel.RackName,
                    RackNumber = model.ObjStockItemRackViewModel.RackNumber,
                    Status = true,
                    LocationId = model.ObjStockItemRackViewModel.LocationId,
                    Narration = "Stock Rack Setup"

                };
                ent.StockItemRackSetups.Add(groupDtls);
                ent.SaveChanges();

                return true;
            }

        }


        public StockItemModel LocationDetails()
        {
            StockItemModel model = new StockItemModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemLocationSetups.Where(x => x.Status == true);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var ViewModel = new StockItemLocationViewModel()
                        {
                            StockItemLocationId = item.StockItemLocationId,
                            LocatonName = item.LocatonName,
                            DepartmentId = (int)item.DepartmentId,
                            WardID = (int)item.WardID,
                            BlockId = (int)item.BlockId

                        };
                        model.StockItemLocationViewModelList.Add(ViewModel);

                    }
                }

                return model;
            }
        }

        public StockItemModel RackDetails()
        {
            StockItemModel model = new StockItemModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockItemRackSetups.Where(x => x.Status == true);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var ViewModel = new StockItemRackViewModel()
                        {
                            StockItemRackSetupId = item.StockItemRackSetupId,
                            LocationId = (int)item.LocationId,
                            RackName = item.RackName,
                            RackNumber = (int)item.RackNumber,




                        };
                        model.StockItemRackViewModelList.Add(ViewModel);

                    }
                }

                return model;
            }
        }

        public bool InsertItemGroupDetails(StockItemModel model)
        {
            using (EHMSEntities ent = new EHMSEntities())
            {
                int TotalNoOfItem = model.ObjStockItemGroupingMasterViewModel.TotalItem;
                int StartingNo = Convert.ToInt32(0);
                int EndNo = Convert.ToInt32(0);
                int MaxNoOfGrouping = Convert.ToInt32(0);
                if (TotalNoOfItem > 0)
                {
                    var MasterTabel = new StockGroupingItemMaster()
                    {
                        ItemId = model.ObjStockItemGroupingMasterViewModel.ItemId,
                        GroupingId = model.ObjStockItemGroupingMasterViewModel.GroupingId,
                        SubGroupingId = 1,
                        LocationId = model.ObjStockItemGroupingMasterViewModel.LocationId,
                        RackId = 1,
                        Status = true,
                        Remarks = model.ObjStockItemGroupingDetailsViewModel.Remarks,
                        TotalItem = model.ObjStockItemGroupingMasterViewModel.TotalItem,
                        CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                        Narration = "",


                    };
                    ent.StockGroupingItemMasters.Add(MasterTabel);
                    ent.SaveChanges();
                    int masterTableId = MasterTabel.StockGroupingItemMasterID;

                    //Get Group's starting and end nunber
                    var result = ent.StockItemGroupSetups.Where(x => x.Status == true && x.StockGroupingId == model.ObjStockItemGroupingMasterViewModel.GroupingId);
                    if (result.Count() > 0)
                    {
                        StartingNo = (int)result.FirstOrDefault().FromId;
                        EndNo = (int)result.FirstOrDefault().ToId;

                        //Get Group Max number from tabel
                        var maxNumber = ent.StockGroupingItemDetails.Where(x => x.Status == true && x.GroupID == model.ObjStockItemGroupingMasterViewModel.GroupingId);
                        if (maxNumber.Count() > 0)
                        {
                            MaxNoOfGrouping = (int)maxNumber.OrderByDescending(x => x.SerrialNo).FirstOrDefault().SerrialNo;
                            MaxNoOfGrouping = MaxNoOfGrouping + 1;
                        }
                        else
                        {
                            MaxNoOfGrouping = StartingNo;
                        }

                        for (int i = 1; i <= model.ObjStockItemGroupingMasterViewModel.TotalItem; i++)
                        {
                            var DetailsTable = new StockGroupingItemDetail()
                            {
                                GroupID = model.ObjStockItemGroupingMasterViewModel.GroupingId,
                                ItemId = model.ObjStockItemGroupingMasterViewModel.ItemId,
                                SerrialNo = MaxNoOfGrouping,
                                Description = model.ObjStockItemGroupingDetailsViewModel.Description,
                                LocationId = model.ObjStockItemGroupingDetailsViewModel.LocationId,
                                RackId = 1,
                                PartyName = model.ObjStockItemGroupingDetailsViewModel.PartyName,
                                Remarks = model.ObjStockItemGroupingDetailsViewModel.Remarks,
                                Status = true,
                                CreatedBy = HospitalManagementSystem.Utility.GetCurrentLoginUserId(),
                                CreatedDate = DateTime.Now,
                                BranchId = 1,
                                Condition = model.ObjStockItemGroupingDetailsViewModel.Condition

                            };
                            MaxNoOfGrouping++;
                            ent.StockGroupingItemDetails.Add(DetailsTable);

                        }
                        ent.SaveChanges();
                    }

                }

            }
            return true;
        }

        public StockItemModel GetGroupingItemDetailList()
        {
            StockItemModel model = new StockItemModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockGroupingItemMasters.Where(x => x.Status == true);
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var Obj = new StockItemGroupingMasterViewModel()
                        {
                            GroupingId = (int)item.GroupingId,
                            StockGroupingItemMasterID = item.StockGroupingItemMasterID,
                            ItemId = (int)item.ItemId,
                            TotalItem = (int)item.TotalItem,
                            Narration = item.Narration,
                            Remarks = item.Remarks

                        };
                        model.StockItemGroupingMasterViewModelList.Add(Obj);
                    }
                }
            }

            return model;

        }

        public StockItemModel GetGroupingItemDetails(int GroupId, int ItemId)
        {
            StockItemModel model = new StockItemModel();
            using (EHMSEntities ent = new EHMSEntities())
            {
                var result = ent.StockGroupingItemDetails.Where(x => x.Status == true && x.GroupID == GroupId && x.ItemId == ItemId).ToList();
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        var Obj = new StockItemGroupingDetailsViewModel()
                        {
                            StockGroupItemDetailsId = item.StockGroupItemDetailsId,
                            GroupID = (int)item.GroupID,
                            ItemId = (int)item.ItemId,
                            SerrialNo = (int)item.SerrialNo,
                            Description = item.Description,
                            LocationId = (int)item.LocationId,
                            RackId = (int)item.RackId,
                            PartyName = item.PartyName,
                            Condition = item.Condition
                        };
                        model.StockItemGroupingDetailsViewModelList.Add(Obj);
                    }
                }

            }

            return model;

        }
    }

}