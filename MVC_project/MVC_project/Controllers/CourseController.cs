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
                    MoedB = collection["MoedB"],
                    Day = collection["Day"],
                    start = collection["start"],
                    end = collection["end"]

                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

                var filter = Builders<Models.Course>.Filter.Eq("_id", id);
                var update = Builders<Models.Course>.Update
                    .Set("Lecturer_ID", collection["Lecturer_ID"])
                    .Set("Name", collection["Name"])
                    .Set("MoedA", collection["MoedA"])
                    .Set("MoedB", collection["MoedB"])
                    .Set("Day", collection["Day"])
                    .Set("start", collection["start"])
                    .Set("end", collection["end"]);
                var result = Models.MongoHelper.course_collection.UpdateOneAsync(filter, update);

                return RedirectToAction("CourseData");
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
    }
}
