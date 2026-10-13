using Microsoft.Ajax.Utilities;
//using SunnyPractice.model;

using SunnyPractice.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI;
using System.Xml.Linq;

namespace SunnyPractice.Controllers
{
    public class HomeController : Controller
    {
        string conString = @"Data Source=NAVEEN\SQLEXPRESS;Initial Catalog=SunnyDB;Integrated Security=True";


        //string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        public ActionResult index()
        {
            ViewBag.msg = "your contact page";

            return View();
        }        
        
        public  ActionResult AjaxTest()
        {
            return View();
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //LIST PAGE
        public ActionResult Registration()
        {
            List<Hp> list = new List<Hp>()
            ;

            SqlConnection con = new SqlConnection(conString);

            string query = "select * from hp";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Hp
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    name = dr["Name"].ToString(),
                    age = Convert.ToInt32(dr["age"]),
                    Gender = dr["Gender"].ToString(),
                    disease = dr["disease"].ToString()
                });
            }
            ViewBag.data = list;

            con.Close();

            return View();
        }             

        
        //GET BY ID 
        [HttpGet]
        public ActionResult Contact(int id = 0)
        {

            if (id > 0)
            {
                SqlConnection con = new SqlConnection(conString);

                string query = "select *  from hp where id = @id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ViewBag.Id = dr["id"];
                    ViewBag.Name = dr["name"];
                    ViewBag.age = dr["age"];
                    ViewBag.Gender = dr["gender"];
                    ViewBag.disease = dr["disease"];

                }

                con.Close();
            }

            return View();
        }      


        // INSERT OR UPDATE
        [HttpPost]
        public ActionResult Contact(Hp model)
        {

            SqlConnection con = new SqlConnection(conString);

            string query = "";
            if (model.Id > 0)
            {
                query = "update hp set name=@name,age=@age,gender=@gender,disease=@disease where id=@id";
                TempData["Message"] = "Record update Successfully";
            }
            else

            {
                query = "insert into hp(name,age,gender,Disease) values (@name,@age,@gender,@Disease)";
                TempData["Message"] = "Record inserted Successfully";

            }
            
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", model.Id);
            cmd.Parameters.AddWithValue("@name", model.name);
            cmd.Parameters.AddWithValue("@age", model.age);
            cmd.Parameters.AddWithValue("@Disease", model.disease);
            cmd.Parameters.AddWithValue("@gender", model.Gender);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Registration");
        }

        // DELETE
        [HttpPost]
        public ActionResult delete(int id)
        {
            SqlConnection con = new SqlConnection(conString);

            string query = "delete from hp where id = @id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            TempData["Message"] = "Record delete Successfully";

            return RedirectToAction("Registration");

        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



        //[HttpPost]
        //public ActionResult insert(string name, string age, string disease)
        //{
        //    SqlConnection con = new SqlConnection(conString);

        //    string query = "INSERT INTO hp(name,age,disease) VALUES(@name,@age,@disease)";

        //    SqlCommand cmd = new SqlCommand(query, con);

        //    //cmd.Parameters.AddWithValue("@id", id);
        //    cmd.Parameters.AddWithValue("@name", name);
        //    cmd.Parameters.AddWithValue("@age", age);
        //    cmd.Parameters.AddWithValue("@Disease", disease);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    return RedirectToAction("Registration");

        //}

        //public ActionResult get(int id)
        //{
        //    SqlConnection con = new SqlConnection(conString);

        //    string query = "select *  from hp where id = @id";

        //    SqlCommand cmd = new SqlCommand(query, con);

        //    cmd.Parameters.AddWithValue("@id", id);

        //    con.Open();

        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {

        //        ViewBag.Id = dr["id"];
        //        ViewBag.Name = dr["name"];
        //        ViewBag.age = dr["age"];
        //        ViewBag.Gender = dr["gender"];
        //        ViewBag.disease = dr["disease"];

        //    }

        //    con.Close();

        //    return View("Contact");

        //}

        //[HttpPost]
        //public ActionResult update(int id, string name, string age, string disease)
        //{
        //    SqlConnection con = new SqlConnection(conString);

        //    string query = "update hp set id=@id, name=@name,age=@age,disease=@disease where id=@id";

        //    SqlCommand cmd = new SqlCommand(query, con);

        //    cmd.Parameters.AddWithValue("@id", id);
        //    cmd.Parameters.AddWithValue("@name", name);
        //    cmd.Parameters.AddWithValue("@age", age);
        //    cmd.Parameters.AddWithValue("@Disease", disease);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    return RedirectToAction("Contact");

        //}


        ////[HttpPost]
        ////public ActionResult Contact(int id, string name, int age, string gender, string disease)
        ////{
        ////    SqlConnection con = new SqlConnection(conString);

        ////    string query = @"INSERT INTO hp
        ////             (Id, Name, Age, Gender, Disease)
        ////             VALUES
        ////             (@Id, @Name, @Age, @Gender, @Disease)";

        ////    SqlCommand cmd = new SqlCommand(query, con);

        ////    cmd.Parameters.AddWithValue("@Id", id);
        ////    cmd.Parameters.AddWithValue("@Name", name);
        ////    cmd.Parameters.AddWithValue("@Age", age);
        ////    cmd.Parameters.AddWithValue("@Gender", gender);
        ////    cmd.Parameters.AddWithValue("@Disease", disease);

        ////    con.Open();
        ////    cmd.ExecuteNonQuery();
        ////    con.Close();

        ////    return RedirectToAction("Contact");
        ////}
        ///

        //string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //using (SqlConnection con = new SqlConnection(cs))
        //{
        //    string query = @"SELECT * FROM Logins WHERE UserName = @UserName AND Password = @Password";

        //    SqlCommand cmd = new SqlCommand(query, con);

        //    cmd.Parameters.AddWithValue("@UserName", uname);
        //    cmd.Parameters.AddWithValue("@Password", password);

        //    con.Open();

        //    SqlDataReader dr = cmd.ExecuteReader();

        //    if (dr.Read())
        //    {
        //        //Session["UserId"] = dr["Id"];
        //        //Session["UserName"] = dr["UserName"];
        //        ViewBag.username = dr["UserName"];

        //        // ViewBag.msg = "You are successfully logged in.";

        //        return RedirectToAction("UserDashboard");
        //    }
        //    else
        //    {
        //        ViewBag.msg = "Invalid username or password.";
        //    }

        ////[HttpPost]
        ////public ActionResult Index(string uname, string password, string fname, string city, string gender)
        ////{




        ////    if (uname == "naveen" && password == "123")
        ////    {
        ////        ViewBag.msg = "your are successfully logged in";
        ////    }
        ////    else if (uname == "sunny" && password == "456")
        ////    {
        ////        ViewBag.msg = "the username and passward is incorrect";
        ////    }
        ////    else if (uname == "adithya" && password == "123")
        ////    {
        ////        ViewBag.msg = "invalid username and passward";
        ////    }

        ////    else if (fname == "sambaiah")
        ////    {

        ////        ViewBag.msg = "if father is sambaiah valid";
        ////    }
        ////    else if (city == "gudur")
        ////    {

        ////        ViewBag.msg = "your city is gudur";

        ////    }

        ////    else if (gender == "male,")
        ////    {
        ////        ViewBag.msg = "given gender will be satisfied";
        ////    }

        ////    else
        ////    {
        ////        ViewBag.msg = "given information is not satisfied";

        ////    }



        ////        return View();

        ////}
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        ////public ActionResult UserDashboard()
        ////{
        ////    ViewBag.Message = "Dashboard";

        ////    return View();
        ////}


        ////[HttpGet]
        ////public ActionResult Registration()
        ////{
        ////    ViewBag.Message = "Registration";

        ////    return View();
        ////}

        //[HttpGet]

    }
}