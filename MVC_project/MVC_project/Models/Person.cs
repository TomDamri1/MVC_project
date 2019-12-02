using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_project.Models
{
    public class Person
    {
        public String ID { get; set; }

        
        public String FirstName { get; set; }

        
        public String LastName { get; set; }

        protected String type { get; set; }
    }
}