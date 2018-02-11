using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class OperationTheatreRoomModel
    {
        public int OperationTheatreRoomID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Room Name")]
        public string RoomName { get; set; }

        public bool Stats { get; set; }

        public List<OperationTheatreRoomModel> OperationTheatreRoomList { get; set; }
    }
}