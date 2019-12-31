using MongoDB.Driver;
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

        public List<string> student_list { get; set; }

        public List<string> grade_list { get; set; }

        public List<string> grade_listB { get; set; }

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

        public static string getCourseIdByName(string name)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter1 = Builders<Models.Course>.Filter.Eq("Name", name);
            var course = Models.MongoHelper.course_collection.Find(filter1).FirstOrDefault();

            return course._id.ToString();
        }

        public static string addCourseToID(string course_id , string user_id)
        {
            var course = course_id;
            string id = user_id;
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");
            var filter = Builders<Models.Login>.Filter.Eq("_id", id);
            var user = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();
            var courses = user.course_list;
            coding.connection checker = new coding.connection();
            string check_result = checker.is_course_ok(id, course,null);
            if (check_result.Equals("true"))
            {
                courses.Add(course);
            }
            else
            {
                return check_result;
            }
            var update = Builders<Models.Login>.Update
                .Set("course_list", courses);
            var result = Models.MongoHelper.login_collection.UpdateOneAsync(filter, update);
            if (user.Type.ToLower().Equals("student")) {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");
                
                var course_filter = Builders<Models.Course>.Filter.Eq("_id", course_id);
                var this_course = Models.MongoHelper.course_collection.Find(course_filter).FirstOrDefault();
                var student_list = this_course.student_list.ToList();
                student_list.Add(user_id);
                var grade_list = this_course.grade_list.ToList();
                grade_list.Add("none");
                var grade_listB = this_course.grade_listB.ToList();
                grade_listB.Add("none");
                var update_course = Builders<Models.Course>.Update
                        .Set("student_list", student_list)
                        .Set("grade_list", grade_list)
                        .Set("grade_listB",grade_list);

                var result_course = Models.MongoHelper.course_collection.UpdateOneAsync(course_filter, update_course);
            }

            return check_result;
        }

    }

    
}