using MongoDB.Driver;
using MVC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        // GET: Login/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var filter = Builders<Models.Login>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View("Create_user");
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.login_collection =
                    Models.MongoHelper.database.GetCollection<Models.Login>("Login");

                Models.MongoHelper.login_collection.InsertOneAsync(new Models.Login {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    _id = collection["ID"],
                    ID = collection["ID"],
                    UserName = collection["UserName"],
                    Type = collection["Type"],
                    Password = collection["Password"]
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var filter = Builders<Models.Login>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.login_collection =
                    Models.MongoHelper.database.GetCollection<Models.Login>("Login");

                var filter = Builders<Models.Login>.Filter.Eq("_id", id);
                var update = Builders<Models.Login>.Update
                    .Set("FirstName", collection["FirstName"])
                    .Set("LastName", collection["LastName"])
                    .Set("_id", collection["ID"])
                    .Set("ID", collection["ID"])
                    .Set("UserName", collection["UserName"])
                    .Set("Type", collection["Type"])
                    .Set("Password", collection["Password"]);
                var result = Models.MongoHelper.login_collection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var filter = Builders<Models.Login>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.login_collection =
                    Models.MongoHelper.database.GetCollection<Models.Login>("Login");

                var filter = Builders<Models.Login>.Filter.Eq("_id", id);

                var result = Models.MongoHelper.login_collection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LoginData()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var filter = Builders<Models.Login>.Filter.Ne("_id", "");
            var result = Models.MongoHelper.login_collection.Find(filter).ToList();

            return View(result);
        }

        public ActionResult LoginPressed()
        {
            Session["UserName"] = Request.Form["UserName"];
            Session["Password"] = Request.Form["Password"];

            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var username_filter = Builders<Models.Login>.Filter.Eq("UserName", Session["UserName"]);
            var password_filter = Builders<Models.Login>.Filter.Eq("Password", Session["Password"]);
            var filter = username_filter & password_filter;

            var result = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();

            try
            {
                Session["Type"] = result.Type;
                Session["FirstName"] = result.FirstName;
                Session["LastName"] = result.LastName;

                if(Session["Type"].Equals("student"))
                {
                    return View("StudentPage");
                }
                else if (Session["Type"].Equals("lecturer"))
                {
                    return View("LecturerPage");
                }
                else if (Session["Type"].Equals("admin"))
                {
                    return View("StudentPage");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View();
        }

        public ActionResult StudentPage()
        {
            return View();
        }

        public ActionResult LecturerPage()
        {
            return View();
        }

        public ActionResult admin()
        {
            return View();
        }


    }
}
