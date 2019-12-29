using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_project.Models
{
    /*
     * hours are between 8:00 and 20:00
     */
    public class Schedule
    {
        public static int SUNDAY   = 0;
        public static int MONDAY   = 1;
        public static int TUESDAY  = 2;
        public static int WEDNESDAY = 3;
        public static int THURSDAY = 4;
        public static int FRIDAY   = 5;
        public static int SATURDAY = 6;

        public string[,] Week2 = new string[7, 12] { 
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" }
        };

        public string[,] Week = new string[7, 13] {
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"},
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" , "Empty"}
        };

        public void AddItemToSchedule(int day, int hour, string item)
        {
            this.Week[day, hour - 8] = item;
        }
        public void RemoveItemFromSchedule(int day, int hour)
        {
            this.Week[day, hour - 8] = "Empty";
        }
        public string getHour(int day, int hour)
        {
            return this.Week[day, hour - 8];
        }

        public static string intToHour(int hour)
        {
            string result = ":00";
            if (hour < 10)
            {
                result = "0" + hour.ToString() + result;
            }
            else
            {
                result = hour.ToString() + result;
            }
            return result;
        }
    }

}