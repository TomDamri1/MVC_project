using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_project.Models
{
    public class Student : Person
    {
        HashSet<String> courseList = new HashSet<String> { };
        public void init(String _FirstName, String _LastName , String _ID)
        {
            FirstName = _FirstName;
            LastName = _LastName;
            ID = _ID;
            type = "Student";
        }
        
        public void addCourse(String course_id)
        {
            courseList.Add(course_id);
        }

        public void removeCourse(String course_id)
        {
            courseList.Remove(course_id);
        }


    }
}