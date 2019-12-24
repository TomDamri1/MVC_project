using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_project.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Enter your Username!")]    
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password!")]
        public string Password { get; set; }

        public string Type { get; set; }

        public string ID { get; set; }
        public Object _id { get; set; }
    }
}