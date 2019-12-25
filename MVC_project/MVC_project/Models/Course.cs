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

        

    }
}