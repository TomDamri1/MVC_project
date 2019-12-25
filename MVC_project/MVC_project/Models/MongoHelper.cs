using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace MVC_project.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://tomandmatan:tomandmatan123!@cluster0-jtfrr.gcp.mongodb.net/test?retryWrites=true&w=majority";
        public static string MongoDatabase = "MVC_project"; //the name in mongo

        public static IMongoCollection<Models.Login> login_collection { get; set; }
        public static IMongoCollection<Models.Course> course_collection { get; set; }

        internal static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {

            }
        }
    }
}