
using LanguageExt.ClassInstances;
using LanguageExt.Pipes;
using SunnyPractice.Controllers;
using SunnyPractice.Models;
using SunnyPractice.Views.EMP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SunnyPractice.Models
{
    public class MyDbContext : DbContext
    {
        internal object Products;
        internal object student;

        public MyDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<products2> products { get; set; }

        public DbSet<login2> logins { get; set; }

        public DbSet<EMP3> Emp{ get; set; }

        public DbSet<Country1> country { get; set; }

        public DbSet<State2> state { get; set; }

        public DbSet<city3> city { get; set; }

        //public DbSet<Student> students { get; set; }

    }

}

       

