using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class OperationTheatreDetailsModel
    {
        public int OperationTheatreDetailsID { get; set; }

        //[Required(ErrorMessage = "Required")]
        //[DisplayName("Operation Theatre")]
        public int OperationTheatreMasterID { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Person Allocated")]
        public int PersonAllocatedID { get; set; }

        //[Required(ErrorMessage = "Required")]
        [DisplayName("Person Allocated Type")]
        public int PersonAllocatedTypeID { get; set; }

        public List<OperationTheatreDetailsModel> OperationTheatreDetalisList { get; set; }


    }


}