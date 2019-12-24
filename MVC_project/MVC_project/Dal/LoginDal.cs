using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC_project.Models;
namespace MVC_project.Dal
{
    public class LoginDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Login>().ToTable("Login_tbl");
        }
        public DbSet<Login> Users { get; set; }
    }
}