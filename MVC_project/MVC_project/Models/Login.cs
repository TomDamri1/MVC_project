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
    }
}