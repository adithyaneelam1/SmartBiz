using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Pipes;
using SunnyPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace SunnyPractice.Controllers
{
    public class EMPController : Controller
    {

        string conString = @"Data Source=NAVEEN\SQLEXPRESS;Initial Catalog=SunnyDB;Integrated Security=True";
        //private object pid;
        //private object username;
        private string query;
        //private readonly object password;
        //private readonly int Count;
        //private readonly object matched;

        //public object PASSWORD { get; private set; }
        //public object USERNAME { get; private set; }
        //public object cmd { get; private set; }

        //public ActionResult productlist()
        //{
        //    List<productsModel> list = new List<productsModel>();


        //    SqlConnection con = new SqlConnection(conString);
        //    query = "EXEC list_products";

        //    //string query = "select * from products";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();

        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        list.Add(new productsModel
        //        {
        //            Pid = Convert.ToInt32(dr["PId"]),
        //            PName = dr["PName"].ToString(),
        //            category = dr["Category"].ToString(),
        //            price = Convert.ToInt64(dr["price"]),
        //            Username = dr["username"].ToString(),
        //            Password = dr["password"].ToString(),
        //            imagepath = dr["imagepath"].ToString()

        //        });
        //    }
        //    //ViewBag.data = list;

        //    con.Close();

        //    return View(list);
        //}

        [HttpGet]
        public ActionResult products(int Pid = 0)
        {


            if (Pid > 0)
            {
                SqlConnection con = new SqlConnection(conString);

                /* string query = "select *  from products where Pid = @Pid"*/
                ;

                string query = "exec GetbyId_products @Pid";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Pid", Pid);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ViewBag.Pid = dr["Pid"];
                    ViewBag.Pname = dr["Pname"];
                    ViewBag.category = dr["category"];
                    ViewBag.price = dr["price"];
                    ViewBag.username = dr["username"];
                    ViewBag.password = dr["password"];
                    ViewBag.imagepath = dr["imagepath"];
                }

                con.Close();
            }


            //  ViewBag.data = "products list";


            return View();
        }


        // [HttpPost]

        //public ActionResult products(productsModel model)

        //{

        //    //string imagePath = null;

        //    // Check image
        //    if (model.Imagefile != null &&
        //        model.Imagefile.ContentLength > 0)
        //    {
        //        // Get extension
        //        string extension = Path.GetExtension(model.Imagefile.FileName)
        //            .ToLower();


        //        // Generate unique file name
        //        string fileName = Guid.NewGuid().ToString() + extension;


        //        // Physical folder
        //        string folderPath = Server.MapPath("~/Images/");


        //        // Physical file path
        //        string physicalPath = Path.Combine(folderPath, fileName);


        //        // Save image
        //        model.Imagefile.SaveAs(physicalPath);


        //        // Path to save in database
        //        model.imagepath = "/Images/" + fileName;
        //    }



        //    SqlConnection con = new SqlConnection(conString);


        //    string query = "";

        //    if (model.Pid > 0)


        //    {
        //        query = "EXEC PRODUCTS_UPDATE @PID, @pname, @category, @price , @username ,@password,@imagepath";
        //        TempData["Message"] = "Record updated Successfully";

        //    }

        //    else
        //    {


        //        query = "EXEC PRODUCT_INSERT @pname, @category, @price, @username, @password,@imagepath";

        //        TempData["Message"] = "Record inserted Successfully";

        //    }



        //    SqlCommand cmd = new SqlCommand(query, con);

        //    con.Open();

        //    cmd.Parameters.AddWithValue("@pid", model.Pid);
        //    cmd.Parameters.AddWithValue("@pname", model.PName);
        //    cmd.Parameters.AddWithValue("@category", model.category);
        //    cmd.Parameters.AddWithValue("@price", model.price);
        //    cmd.Parameters.AddWithValue("@username", model.Username);
        //    cmd.Parameters.AddWithValue("@password", model.Password);
        //    cmd.Parameters.AddWithValue("@imagepath", model.imagepath);
        //    cmd.ExecuteNonQuery();

        //    con.Close();

        //    return RedirectToAction("productlist");
        //}




        //privavoid logins(object uSERNAME, object pASSWORD, Select pid)
        //{
        //    throw new NotImplementedException();
        //}

        //private void values(object username, object password, Select pid)
        //{
        //    throw new NotImplementedException();
        //}

        //private Select MAX(object pid)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public ActionResult productdelete(int Pid)
        //{
        //    SqlConnection con = new SqlConnection(conString);

        //    //string query = "delete from products where Pid = @Pid";

        //    query = "EXEC PRODUCTS_delete @pid";


        //    SqlCommand cmd = new SqlCommand(query, con);



        //    con.Open();

        //    cmd.Parameters.AddWithValue("@Pid", Pid);


        //    cmd.ExecuteNonQuery();


        //    TempData["Message"] = "Record deleted Successfully";

        //    con.Close();

        //    return RedirectToAction("productlist");
        //}

        //}


    }


}



