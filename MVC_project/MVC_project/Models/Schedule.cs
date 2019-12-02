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
        public int SUNDAY   = 0;
        public int MONDAY   = 1;
        public int TUESDAY  = 2;
        public int WEDNESDAY= 3;
        public int THURSDAY = 4;
        public int FRIDAY   = 5;
        public int SATURDAY = 6;

        private string[,] Week = new string[7, 12] { 
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" },
            { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", "Empty" }
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
    }

}