using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Helpers
{
    public class CommonHelper
    {
        public static int GetDateDayDifference()
        {
            DateTime oldDate = new DateTime(2002, 7, 15);
            DateTime newDate = DateTime.Now;

            // Difference in days, hours, and minutes.
            TimeSpan ts = newDate - oldDate;
            // Difference in days.
            int differenceInDays = ts.Days;
            return differenceInDays;
        }
        public static string GetAgeByBirthday(int AgeType, DateTime BirthdayDate)
        {
            string retrunValue = string.Empty;
            DateTime Birth = BirthdayDate;
            DateTime Today = DateTime.Now;

            TimeSpan Span = Today - Birth;

            DateTime Age = DateTime.MinValue + Span;

            // note: MinValue is 1/1/1 so we have to subtract...
            int Years = Age.Year - 1;
            int Months = Age.Month - 1;
            int Days = Age.Day - 1;
            if (AgeType == 1)
            {
                retrunValue = Years.ToString();
            }
            else if (AgeType == 2)
            {
                retrunValue = Months.ToString();
            }
            else
            {
                retrunValue = Days.ToString();
            }

            return retrunValue;
        }

        public static Int32 GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        public static int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }




    }
}