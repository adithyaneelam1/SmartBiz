using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace SunnyPractice.Models
{
    public class Hp
    {
        [Key]
        public int Id { get; set; }

        public string name { get; set; }
        public int age { get; set; }

        public string disease { get; set; }

        public string Gender { get; set; }

    };

}