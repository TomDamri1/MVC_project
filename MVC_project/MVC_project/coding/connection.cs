using MongoDB.Driver;
using MVC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_project.coding
{
    public class connection
    {
        //CHEKS IF ITS OK TO THE USER TO SIGN TO THIS COURSE
        public string is_course_ok(string user_id, string course_id)
        {
            //check if the course exist in course DB:
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter1 = Builders<Models.Course>.Filter.Eq("_id", course_id);
            var course = Models.MongoHelper.course_collection.Find(filter1).FirstOrDefault();
            if (course == null)
            {
                return "there is no such a course.";
            }

            //check if the course exist for this user:
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");
            var filter2 = Builders<Models.Login>.Filter.Eq("_id", user_id);
            var courses = Models.MongoHelper.login_collection.Find(filter2).FirstOrDefault().course_list;
            if (courses.Contains(course_id))
            {
                return "this user is already assigned to the course.";
            }

            //check schedule:
            Schedule schedule = GetSchedule(user_id);
            int start = Course.getHourAsInt(course.start);
            int end = Course.getHourAsInt(course.end);
            int day = Course.getDayAsInt(course.Day);
            for (int i = start; i < end; i++)
            {
                if (!schedule.getHour(day, i).Equals("Empty"))
                {
                    return "there is another course in this time.";
                }
            }

           

            return "true";
        }

        public string is_course_ok(FormCollection collection)
        {
            string _id = collection["Course_ID"];
            string name = collection["Name"];
            string Lecturer_ID = collection["Lecturer_ID"];
            string MoedA = collection["MoedA"];
            string MoedA_classroom = collection["MoedA_classroom"];
            string MoedB = collection["MoedB"];
            string MoedB_classroom = collection["MoedB_classroom"];
            string Day = collection["Day"];
            int start = Course.getHourAsInt( collection["start"]);
            int end = Course.getHourAsInt(collection["end"]);
            string classroom = collection["classroom"];

            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            //check if the name is ok
            var name_filter = Builders<Models.Course>.Filter.Eq("Name", name);
            var course = Models.MongoHelper.course_collection.Find(name_filter).FirstOrDefault();
            if (course != null)
            {
                return "there is a course with the same name.";
            }

            //check if the lecturer got time for this course
            Schedule schedule = GetSchedule(Lecturer_ID);
            int lstart = start;
            int lend = end;
            int day = Course.getDayAsInt(Day);
            for (int i = lstart; i < lend; i++)
            {
                if (!schedule.getHour(day, i).Equals("Empty"))
                {
                    return "this lecturer is taken in those hours.";
                }
            }

            //check if moedA is befor moedB
            if (!MoedAB_time_check(MoedA, MoedB))
            {
                return "MoedA is before MoedB";
            }

            //check if the class is taken during the exam period
            //if there is a course that moed a or b is in the same class in the same day
            if (!is_class_ok_for_exam(MoedA, MoedA_classroom))
            {
                return "moedA classroom is taken";
            }
            if(!is_class_ok_for_exam(MoedB , MoedB_classroom))
            {
                return "moedB classroom is taken";
            }

            //check if there is a lesson in the same class in those hours


            return "true";
        }

        public Schedule GetSchedule(string user_id)
        {
            Schedule schedule = new Schedule();
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");
            var filter2 = Builders<Models.Login>.Filter.Eq("_id", user_id);
            var courses = Models.MongoHelper.login_collection.Find(filter2).FirstOrDefault().course_list;

            foreach (Object course_id in courses)
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");
                try
                {
                    var filter1 = Builders<Models.Course>.Filter.Eq("_id", course_id);
                    var course = Models.MongoHelper.course_collection.Find(filter1).FirstOrDefault();
                    int day = Course.getDayAsInt(course.Day);
                    int start = Course.getHourAsInt(course.start);
                    int end = Course.getHourAsInt(course.end);
                    string course_name = course.Name;

                    for (int i = start; i < end; i++)
                    {
                        try
                        {
                            schedule.AddItemToSchedule(day, i, course_name);
                        }
                        catch { }
                    }
                }
                catch { }
            }

            return schedule;
        }

        public bool MoedAB_time_check(string A , string B)
        {
            String[] a = A.Split('/');
            String[] b = B.Split('/');

            int Bday = Int32.Parse(b[0]);
            int Aday = Int32.Parse(a[0]);

            int Bmonth = Int32.Parse(b[1]);
            int Amonth = Int32.Parse(a[1]);

            int Byear = Int32.Parse(b[2]);
            int Ayear = Int32.Parse(a[2]);
            if ( Byear > Ayear ) return true;
            if ( Ayear > Byear ) return false;

            if ( Bmonth > Amonth ) return true;
            if ( Amonth > Bmonth ) return false;

            if ( Bday >  Aday )return true;
            return false;
        }

        public bool is_class_ok_for_exam(string exam_date , string classroom)// input : moedA / moedB
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var date_filter = Builders<Models.Course>.Filter.Eq("MoedA", exam_date);
            var courses = Models.MongoHelper.course_collection.Find(date_filter).ToList();
            foreach (Course course in courses)
            {
                if (course.MoedA_classroom.Equals(classroom))
                {
                    return false;
                }
            }

            //and same to moed b
            date_filter = Builders<Models.Course>.Filter.Eq("MoedB", exam_date);
            courses = Models.MongoHelper.course_collection.Find(date_filter).ToList();
            foreach (Course course in courses)
            {
                if (course.MoedB_classroom.Equals(classroom))
                {
                    return false;
                }
            }

            return true;
        }

        public bool is_class_ok(string day , string start , string end)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var date_filter = Builders<Models.Course>.Filter.Eq("Day", day);
            var courses = Models.MongoHelper.course_collection.Find(date_filter).ToList();
            foreach (Object course in courses)
            {
                
            }

            return true;
        }
    }
}