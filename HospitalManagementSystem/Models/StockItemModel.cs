using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockItemModel
    {
        public int StockItemTypeID { get; set; }

        [DisplayName("Item Type Name")]
        [Required(ErrorMessage = "*")]
        public string StockItemTypeName { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<StockItemModel> StockItemList { get; set; }
        public List<StockItemGroupSetupViewModel> StockItemGroupSetupViewModelList { get; set; }
        public List<StockItemLocationViewModel> StockItemLocationViewModelList { get; set; }
        public List<StockItemRackViewModel> StockItemRackViewModelList { get; set; }

        public StockItemGroupSetupViewModel ObjStockItemGroupSetupViewModel { get; set; }
        public StockItemLocationViewModel ObjStockItemLocationViewModel { get; set; }
        public StockItemRackViewModel ObjStockItemRackViewModel { get; set; }


        public StockItemGroupingMasterViewModel ObjStockItemGroupingMasterViewModel { get; set; }
        public List<StockItemGroupingMasterViewModel> StockItemGroupingMasterViewModelList { get; set; }

        public StockItemGroupingDetailsViewModel ObjStockItemGroupingDetailsViewModel { get; set; }
        public List<StockItemGroupingDetailsViewModel> StockItemGroupingDetailsViewModelList { get; set; }

        public StockItemModel()
        {
            ObjStockItemGroupSetupViewModel = new StockItemGroupSetupViewModel();
            ObjStockItemLocationViewModel = new StockItemLocationViewModel();
            ObjStockItemRackViewModel = new StockItemRackViewModel();
            StockItemRackViewModelList = new List<StockItemRackViewModel>();
            StockItemLocationViewModelList = new List<StockItemLocationViewModel>();
            StockItemGroupSetupViewModelList = new List<StockItemGroupSetupViewModel>();

            ObjStockItemGroupingMasterViewModel = new StockItemGroupingMasterViewModel();
            ObjStockItemGroupingDetailsViewModel = new StockItemGroupingDetailsViewModel();
            StockItemGroupingMasterViewModelList = new List<StockItemGroupingMasterViewModel>();
            StockItemGroupingDetailsViewModelList = new List<StockItemGroupingDetailsViewModel>();



        }

    }

    public class StockItemGroupSetupViewModel
    {

        public int StockGroupingId { get; set; }
        public string GroupName { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public Boolean Status { get; set; }
        public int BranchId { get; set; }
        public string Narration { get; set; }
    }

    public class StockItemGroupSubGroupViewModel
    {
        public int stockItemGroupSubId { get; set; }
        public int GroupId { get; set; }
        public string SubGroupName { get; set; }
        public Boolean Status { get; set; }
        public string Narration { get; set; }


    }
    public class StockItemLocationViewModel
    {


        public int StockItemLocationId { get; set; }
        public string LocatonName { get; set; }
        public Boolean Status { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }
        public int WardID { get; set; }
        public string Narration { get; set; }
        public int BlockId { get; set; }

    }

    public class StockItemRackViewModel
    {

        public int StockItemRackSetupId { get; set; }
        public string RackName { get; set; }
        public int RackNumber { get; set; }
        public int LocationId { get; set; }
        public string Narration { get; set; }
        public Boolean Status { get; set; }
        public int BranchId { get; set; }
        public string Remarks { get; set; }



    }
    public class StockItemGroupingMasterViewModel
    {
        public int StockGroupingItemMasterID { get; set; }
        public int ItemId { get; set; }
        public int GroupingId { get; set; }
        public int SubGroupingId { get; set; }
        public int LocationId { get; set; }
        public int RackId { get; set; }
        public Boolean Status { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public int TotalItem { get; set; }
        public int CreatedBy { get; set; }

    }
    public class StockItemGroupingDetailsViewModel
    {
        public int StockGroupItemDetailsId { get; set; }
        public int GroupID { get; set; }
        public int ItemId { get; set; }
        public int SerrialNo { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int RackId { get; set; }
        public string PartyName { get; set; }
        public string Remarks { get; set; }
        public Boolean Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }
        public string Condition { get; set; }


    }

}