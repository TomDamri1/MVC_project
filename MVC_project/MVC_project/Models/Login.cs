using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;

namespace MVC_project.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Enter your Username!")]    
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Field is Requried")]
        public string Type { get; set; }

        [Required(ErrorMessage = "This Field is Requried")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This Field is Requried")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This Field is Requried")]
        public string ID { get; set; }
        [Required(ErrorMessage = "This Field is Requried")]
        public Object _id { get; set; }

        [Required(ErrorMessage = "This Field is Requried")]
        public List<string> course_list { get; set; }

        public static string getFullName_by_ID(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.login_collection =
                Models.MongoHelper.database.GetCollection<Models.Login>("Login");

            var filter = Builders<Models.Login>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.login_collection.Find(filter).FirstOrDefault();

            return result.FirstName.ToString() + " " + result.LastName.ToString();
        }
    }
}