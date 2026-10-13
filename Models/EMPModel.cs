using LanguageExt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SunnyPractice.Models
{
    public class productsModel
    {

        [Key]
        public int Pid { get; set; }
        public string PName { get; set; }

        public string category { get; set; }

        public long? price { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }


        public string imagepath { get; set; }

        public HttpPostedFileBase Imagefile { get; set; }

    }

    [Table("Country")]
    public class Country1
    {
        [Key]
        public int cid { get; set; }

        public string cname { get; set; }
    }


    [Table("state")]
    public class State2 
    {
        [Key]
        public int sid { get; set; }

        public string sname { get; set; }

        public int countryid { get; set; }
    }

    [Table("city")]
    public class city3
    {
        [Key]
        public int cid { get; set; }

        public string cname { get; set; }

        public int stateid { get; set; }
    }


    [Table("Emp")]
    public class EMP3 
    {

        [Key]
        public int Eid { get; set; }

        [Required(ErrorMessage = "Please enter product Ename")]
        public string Ename { get; set; }
        [Required(ErrorMessage = "Please enter product salary")]
        public long ? salary { get; set; }

    }

    [Table("products")]
    public class products2
    {

        [Key]
        public int Pid { get; set; }
        public string PName { get; set; }

        public string category { get; set; }

        public long? Price { get; set; }
       // [Column("imagepath")]
        public string ImagePath { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public bool InStock { get; set; }

        public bool AvailableForDelivery { get; set; }

    }


    [Table("logins")]
    public class login2
    {

        [Key]
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public int? Pid { get; set; }

    }

    public class ProductLoginViewModel
    {
        public int Pid { get; set; }

        [Required(ErrorMessage = "Please enter product name")] 
        public string Pname { get; set; }

        [Required]
        public string category { get; set; }


        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        public long? Price { get; set; }

       
        public string ImagePath { get; set; }

        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public bool InStock { get; set; }

        public bool AvailableForDelivery { get; set; }


        [Required(ErrorMessage = "Please select an image")]
        public HttpPostedFileBase Imagefile { get; set; }
    }

}


    


   