using MVC_project.Dal;
using MVC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVC_project.ModelView
{
    public class LoginViewModel
    {
        public Login login { get; set; }
        public LoginDal dal { get; set; }
        public List<Login> logins { get; set; }
    }
}