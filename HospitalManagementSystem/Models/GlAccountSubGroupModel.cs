using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class GlAccountSubGroupModel
    {
        public int EmployeeMasterId { get; set; }
        public int AccountHeadId { get; set; }
        public int AccSubGruupID { get; set; }
        [DisplayName("Main Head")]
        public int AccGroupID { get; set; }
        public string AccGroupName { get; set; }
        public string AccSubGroupName { get; set; }
        [DisplayName("Sub Account Head")]
        public string AccSubGroupName1 { get; set; }
        [DisplayName("Account Head")]
        public int ParentID { get; set; }
        public string HierarchyCode { get; set; }
        public int HeadLevel { get; set; }
        public string AccountCode { get; set; }
        public bool IsLeafLevel { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remarks { get; set; }
        public List<GlAccountSubGroupModel> GlAccountSubGroupModelList { get; set; }
    }

    public class GlAccountMainGroupVM
    {

    }

    public class GlAccountFirstGroupVM
    {

    }

    public class GlAccountSecondGroupVM
    {

    }
    public class GlAccountThirdGroupVM
    {

    }

    public class GlAccountFourthGroupVM
    {

    }

    public class GlAccountFifthGroupVM
    {

    }

    public class GlAccountSixthGroupVM
    {

    }

    public class GlAccountSevenGroupVM
    {

    }


}