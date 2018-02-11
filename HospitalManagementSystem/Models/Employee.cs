using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string mobileNo { get; set; }
        public string Education { get; set; }

        public List<Employee> lstOfEmp { get; set; }


        public Employee()
        {

        }

        public Employee(int id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;

        }




        public List<Employee> getEmpList()
        {

            List<Employee> lstOfEmp = new List<Employee>();

            lstOfEmp.Add(new Employee(1, "mahesh", "Kanchapur-2(Saptati)"));
            lstOfEmp.Add(new Employee(2, "surendra", "Kathmandu-Tripureswar"));
            lstOfEmp.Add(new Employee(3, "Bibek", "Kathmandu-Putalisadak"));
            lstOfEmp.Add(new Employee(4, "Sundar", "Kathmandu-Maitighar"));



            return lstOfEmp;




        }
    }
}

  