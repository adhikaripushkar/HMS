using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BloodTransectionModel
    {
        public int BloodTransectionMasterId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Department Name")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Patient Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Blood Group")]
        public int BloodTypeId { get; set; }
        [DisplayName("Refer By")]
        public string ReferBy { get; set; }
        [DisplayName("Required Date")]
        public DateTime RequiredDate { get; set; }
        [DisplayName("Purpose")]
        public string Purpose { get; set; }


        public BloodTransectionDetailModel ObjBTDetails { get; set; }
        public ShowBloodStockDetailsModel ObjBloodStockDetails { get; set; }

        public List<BloodTransectionModel> BloodTransectionLists { get; set; }
        public List<BloodTransectionDetailModel> BloodTransectionDetailsList { get; set; }
        public List<ShowBloodStockDetailsModel> ShowBloodStockDetailsModelList { get; set; }




    }
    public class BloodTransectionDetailModel
    {
        public int BloodTransectionDetailId { get; set; }
        public int BloodTransectionMasterId { get; set; }
        [DisplayName("Pouch Number")]
        public string PouchNumber { get; set; }
        [DisplayName("Quantity in ML")]
        public decimal QuantityML { get; set; }
        [DisplayName("Quantity in Unit")]
        public int QuantityUnit { get; set; }
    }

    public class ShowBloodStockDetailsModel
    {
        public int BloodStockMasterId { get; set; }
        [DisplayName("Blood Group")]
        public int BloodType { get; set; }
        [DisplayName("Quantity Unit")]
        public int QuantityUnit { get; set; }
        [DisplayName("Quantity ML")]
        public decimal QuantityML { get; set; }
    }
}