using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class StockItemEntryModel
    {
        [DisplayName("Item Name")]
        public int StockItemEntryId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Category")]
        public int StockCategoryId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Sub Category")]
        public int StockSubCategoryId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Unit")]
        public int StockUnitId { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Supplier")]
        public int? StockSupplierId { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Item Type")]
        public int? StockItemTypeId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Item Name")]
        public string StockItemName { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Page Number")]
        public int? PageNumber { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Min Stock Quantity")]
        public decimal? MinStockQuantity { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Max Stock Quantity")]
        public decimal? MaxStockQuantity { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Opening Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public int OpeningStock { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public int GroupId { get; set; }

        public List<StockItemEntryModel> StockItemEntryList { get; set; }


    }

    public class StockItemMasterModel
    {
        public int StockItemMasterId { get; set; }

        public int StockItemEntryId { get; set; }

        public decimal Quantity { get; set; }

        public int UnitId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public List<StockItemMasterModel> StockItemMasterList { get; set; }

    }



}