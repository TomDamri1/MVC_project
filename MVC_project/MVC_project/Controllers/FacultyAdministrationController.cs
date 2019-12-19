using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_project.Controllers
{
    public class FacultyAdministrationController : Controller
    {
        // GET: FacultyAdministration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LandingPage()
        {
            return View("FacultyAdministration");
        }
    }
}