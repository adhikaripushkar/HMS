using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BillingCounterNewModel
    {

        public BillingCounterNewTestListModel BillingCounterNewTestListModel { get; set; }
        public List<BillingCounterNewTestListModel> BillingCounterNewTestListModelList { get; set; }


        public BillingCounterNewModel()
        {
            BillingCounterNewTestListModel = new BillingCounterNewTestListModel();
            BillingCounterNewTestListModelList = new List<BillingCounterNewTestListModel>();


        }

    }


    //public class BillingCounterNewTestListModel
    //{
    //    public int TestId { get; set; }
    //    public string TestCode { get; set; }
    //    public string TestName { get; set; }
    //    public int SectionId { get; set; }
    //    public decimal? Price { get; set; }
    //    public bool isSelected { get; set; }
    //    public decimal HospitalFee { get; set; }
    //    public decimal TaxAmount { get; set; }
    //    public decimal Rate { get; set; }
    //    public int Tim { get; set; }
    //    public int UserId { get; set; }

    //    //testdetails

    //    [DisplayName("Test Date")]
    //    public DateTime TestDate { get; set; }
    //    [DisplayName("Test Time")]
    //    [DataType(DataType.Time)]
    //    public DateTime? TestTime { get; set; }
    //    [DisplayName("Delivery Date")]
    //    [DataType(DataType.Time)]
    //    public DateTime? DeliveryDate { get; set; }

    //    [DisplayName("Amount")]
    //    public decimal Amount { get; set; }
    //    [DisplayName("Discount")]
    //    public decimal? Discount { get; set; }
    //    [DisplayName("Total Amount")]
    //    public decimal TotalAmount { get; set; }
    //    [DisplayName("Delivery Status")]
    //    public bool DeliveryStatus { get; set; }
    //    public int DepartmentID { get; set; }
    //    //
    //    public decimal? DiscountPer { get; set; }


    //}



}