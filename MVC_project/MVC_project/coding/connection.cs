using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_project.coding
{
    public class connection
    {
        public string is_course_ok(string user_id, string course_id)
        {
            //check if the course exist in course DB:
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter1 = Builders<Models.Course>.Filter.Eq("_id", course_id);
            var result = Models.MongoHelper.course_collection.Find(filter1).FirstOrDefault();
            if (result == null)
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
                return "this user is already assigned to the course";
            }

            //check schedule:


            return "Done!";
        }
    }
}