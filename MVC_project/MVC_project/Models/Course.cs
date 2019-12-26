using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_project.Models
{
    public class Course
    {
        public Object _id { get; set; }

        public string Course_ID { set; get; }

        public string Lecturer_ID { set; get; }

        public string Name { set; get; }

        public string MoedA { set; get; }

        public string MoedB { set; get; }

        public string Day { set; get; }

        public string start { set; get; }

        public string end { set; get; }

        public string classroom { set; get; }

        public string MoedA_classroom { set; get; }

        public string MoedB_classroom { set; get; }

        public static int getDayAsInt(string day)
        {
            switch (day.ToLower())
            {
                case "sunday":
                    return 0;
                case "monday":
                    return 1;
                case "tuesday":
                    return 2;
                case "wednesday":
                    return 3;
                case "thursday":
                    return 4;
                case "friday":
                    return 5;
                case "saturday":
                    return 6;
                default:
                    return -1;
            }
        }

        public static int getHourAsInt(string hour)
        {
            int result =-1;
            if(!Int32.TryParse(hour.Substring(0, 2), out result))
            {
                return -1;
            }
            return result;
        }

    }

    
}