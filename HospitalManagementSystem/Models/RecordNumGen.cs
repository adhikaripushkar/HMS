using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class RecordNumGen
    {
        public string RecordNum { get; set; }

        public RecordNumGen(string s)
        {
            RecordNum = s;

        }
        public string RecNumGen()
        {
            if (Convert.ToInt32(this.RecordNum) + 1 < 10)
            {
                int i = Convert.ToInt32(this.RecordNum) + 1;
                this.RecordNum = "00" + i;
            }
            else if (Convert.ToInt32(this.RecordNum) + 1 >= 10 && Convert.ToInt32(this.RecordNum) + 1 <= 99)
            {
                int i = Convert.ToInt32(this.RecordNum) + 1;
                this.RecordNum = "0" + i;
            }
            else this.RecordNum = (Convert.ToInt32(this.RecordNum) + 1).ToString();
            return this.RecordNum;
        }
    }
}