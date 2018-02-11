using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetUpIpdRoomModel
    {

        public int IpdRoomId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Room Type")]

        public int RoomType { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Room No")]
        public int RoomNo { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Starting Bed No")]
        public int StartBed { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Ending Bed No")]
        public int EndBed { get; set; }

        [Display(Name = "Bed Amount")]
        public decimal BedAmount { get; set; }

        [Display(Name = "Room Prefixes")]
        public string RoomPrefix { get; set; }
        [Display(Name = "Room Suffixes")]
        public string RoomSufix { get; set; }
        [Display(Name = "Bed Prefixes")]
        public string BedPrefix { get; set; }
        [Display(Name = "Bed Suffixes")]
        public string BedSufix { get; set; }
        public bool Status { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public IpdRoomStatusModel ObjIpdRoomStatusModel { get; set; }

        public List<SetUpIpdRoomModel> SetUpIpdRoomModelList { get; set; }
    }
    public class IpdRoomStatusModel
    {
        public int IPDRoomStatusId { get; set; }
        public int IPDRoomId { get; set; }
        public int BedNumber { get; set; }
        public int RoomNumber { get; set; }

    }
}