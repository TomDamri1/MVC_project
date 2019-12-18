using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_project.Models
{
    public class Course
    {
        string Course_ID { set; get; }

        string Lecturer_ID { set; get; }

        Schedule schedule { set; get; }

    }
}