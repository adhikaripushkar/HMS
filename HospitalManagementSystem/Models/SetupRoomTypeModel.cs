using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class SetupRoomTypeModel
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        public string RoomTypeName { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public bool Status { get; set; }

        public List<SetupRoomTypeModel> SetupRoomTypeList { get; set; }
    }
}