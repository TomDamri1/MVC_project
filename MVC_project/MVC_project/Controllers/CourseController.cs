using MongoDB.Driver;
using MVC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_project.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return RedirectToAction("CourseData");
        }

        // GET: Course/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter = Builders<Models.Course>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.course_collection.Find(filter).FirstOrDefault();
            coding.connection chekcer = new coding.connection();
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string date = day + '/' + month + '/' + year;
            ViewBag.MoedA_Date_passed = chekcer.MoedAB_time_check(result.MoedA ,date);
            
            
            return View(result);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //assign lecturer automaticly ------------ TODO!

                coding.connection checker = new coding.connection();
                string check_result = checker.is_course_ok(collection);
                if (!check_result.Equals("true"))
                {
                    ViewBag.Error = check_result;
                    return View("Error");
                }
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");

                Models.MongoHelper.course_collection.InsertOneAsync(new Models.Course
                {
                    _id = collection["Course_ID"],
                    Course_ID = collection["Course_ID"],
                    Lecturer_ID = collection["Lecturer_ID"],
                    Name = collection["Name"],
                    MoedA = collection["MoedA"],
                    MoedA_classroom = collection["MoedA_classroom"],
                    MoedB = collection["MoedB"],
                    MoedB_classroom = collection["MoedB_classroom"],
                    Day = collection["Day"],
                    start = collection["start"],
                    end = collection["end"],
                    classroom = collection["classroom"],
                    student_list = new List<string>(),
                    grade_list = new List<string>(),
                    grade_listB = new List<string>(),
                });
                check_result = Course.addCourseToID(collection["Course_ID"], collection["Lecturer_ID"]);
                if (!check_result.Equals("true"))
                {
                    ViewBag.Error = check_result;
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch
            {

                return View("Error");
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter = Builders<Models.Course>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.course_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");
                coding.connection connection = new coding.connection();
                string check_result = connection.is_course_ok_after_edit(collection);
                if (check_result.Equals("true"))
                {
                    var filter = Builders<Models.Course>.Filter.Eq("_id", id);
                    var update = Builders<Models.Course>.Update
                        .Set("Lecturer_ID", collection["Lecturer_ID"])
                        .Set("Name", collection["Name"])
                        .Set("MoedA", collection["MoedA"])
                        .Set("MoedA_classroom", collection["MoedA_classroom"])
                        .Set("MoedB", collection["MoedB"])
                        .Set("MoedB_classroom", collection["MoedB_classroom"])
                        .Set("Day", collection["Day"])
                        .Set("start", collection["start"])
                        .Set("end", collection["end"])
                        .Set("classroom", collection["classroom"]);


                    var result = Models.MongoHelper.course_collection.UpdateOneAsync(filter, update);

                    return RedirectToAction("CourseData");
                }
                else
                {
                    ViewBag.Error = check_result;
                    return View("Error");
                }

                
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        // GET: Course/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter = Builders<Models.Course>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.course_collection.Find(filter).FirstOrDefault();
            return View(result);
        }
            

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");

                var filter = Builders<Models.Course>.Filter.Eq("_id", id);

                var result = Models.MongoHelper.course_collection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CourseData()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter = Builders<Models.Course>.Filter.Ne("_id", "");
            var result = Models.MongoHelper.course_collection.Find(filter).ToList();

            return View(result);
        }

        public ActionResult ChangeGrade(string student_id , string course_id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");

            var filter = Builders<Models.Course>.Filter.Eq("_id", course_id);
            var result = Models.MongoHelper.course_collection.Find(filter).FirstOrDefault();
            ViewBag.student_id = student_id;
            ViewBag.course_id = course_id;
            return View(result);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult ChangeGrade(string student_id, string course_id , FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.course_collection =
                    Models.MongoHelper.database.GetCollection<Models.Course>("Course");

                var filter = Builders<Models.Course>.Filter.Eq("_id", course_id);
                var course = Models.MongoHelper.course_collection.Find(filter).FirstOrDefault();
                int i = 0;
                List <string> student_list = course.student_list.ToList();
                List<string> grade_list=new List<string>();
                try
                {
                    while (!student_list[i].Equals(student_id))
                    {
                        i++;
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "There is no such a student in this course.";
                    return View("Error");
                }
                if (collection["moed"].ToString().ToLower().Equals("a"))
                {
                    grade_list = course.grade_list.ToList();
                    grade_list[i] = collection["grade"];
                    var update = Builders<Models.Course>.Update
                    .Set("grade_list", grade_list);
                    var result = Models.MongoHelper.course_collection.UpdateOneAsync(filter, update);
                }
                else if (collection["moed"].ToString().ToLower().Equals("b"))
                {
                    grade_list = course.grade_listB.ToList();
                    grade_list[i] = collection["grade"];
                    var update = Builders<Models.Course>.Update
                    .Set("grade_listB", grade_list);
                    var result = Models.MongoHelper.course_collection.UpdateOneAsync(filter, update);
                }
                else
                {
                    ViewBag.Error = "please fill the Moed. A or B only.";
                    return View("Error");
                }
                

                

                return RedirectToAction("CourseData");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        
        public ActionResult Exams(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.course_collection =
                Models.MongoHelper.database.GetCollection<Models.Course>("Course");
            string student_id = Session["_id"].ToString() ;
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var username_filter = Builders<Models.Login>.Filter.Eq("_id", student_id);
            var user = Models.MongoHelper.login_collection.Find(username_filter).FirstOrDefault();
            List<string> course_list = user.course_list.ToList();
            List<Course> courses = new List<Course>();
            foreach (string course_id in course_list)
            {
                var single_course_filter = Builders<Models.Course>.Filter.Eq("_id", course_id);
                var single_course_result = Models.MongoHelper.course_collection.Find(single_course_filter).FirstOrDefault();
                courses.Add(single_course_result);
            }


            return View(courses);
        }
    }
}
