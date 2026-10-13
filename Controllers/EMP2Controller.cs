using LanguageExt.ClassInstances;
using LanguageExt.Pipes;
using Microsoft.Ajax.Utilities;
using SunnyPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SunnyPractice.Views.EMP

{
    public class EMP2Controller : Controller
    {
        MyDbContext db = new MyDbContext();


        // =========================
        // PRODUCT LIST
        // =========================

        public ActionResult ProductList()
        {
            var result = from p in db.products
                         join l in db.logins
                         on p.Pid equals l.Pid
                         select new ProductLoginViewModel
                         {
                             Pid = p.Pid,
                             Pname = p.PName,
                             category = p.category,
                             Price = p.Price,
                             ImagePath = p.ImagePath,

                             username = l.Username,
                             password = l.Password,

                             Gender = p.Gender,
                             Address = p.Address,

                             IsNew = p.IsNew,
                             IsFeatured = p.IsFeatured,
                             InStock = p.InStock,
                             AvailableForDelivery = p.AvailableForDelivery
                         };

            return View(result.ToList());
        }


        // =========================
        // GET PRODUCT
        // =========================

        [HttpGet]
        public ActionResult products(int Pid = 0)
        {
            // New Product
            if (Pid == 0)
            {
                return View(new ProductLoginViewModel());
            }

            // Existing Product
            var result = (from p in db.products
                          join l in db.logins
                          on p.Pid equals l.Pid
                          where p.Pid == Pid
                          select new ProductLoginViewModel
                          {
                              Pid = p.Pid,
                              Pname = p.PName,
                              category = p.category,
                              Price = p.Price,
                              ImagePath = p.ImagePath,

                              username = l.Username,
                              password = l.Password,

                              Gender = p.Gender,
                              Address = p.Address,

                              IsNew = p.IsNew,
                              IsFeatured = p.IsFeatured,
                              InStock = p.InStock,
                              AvailableForDelivery = p.AvailableForDelivery
                          }).FirstOrDefault();

            if (result == null)
            {
                return View(new ProductLoginViewModel());
            }

            return View(result);
        }


        // =========================
        // INSERT / UPDATE AJAX
        // =========================
        [HttpPost]
        public JsonResult products(ProductLoginViewModel model)
        {
            try
            {
                
                // INSERT
                if (model.Pid == 0)
                {
                    // Image required for new product
                    if (model.Imagefile == null ||
                        model.Imagefile.ContentLength == 0)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Please select an image."
                        });
                    }

                    // Save image
                    string extension =
                        Path.GetExtension(model.Imagefile.FileName)
                        .ToLower();

                    string fileName =
                        Guid.NewGuid().ToString() + extension;

                    string folderPath =
                        Server.MapPath("~/Images/");

                    if (!Directory.Exists(folderPath))
                    { 
                        Directory.CreateDirectory(folderPath);
                    }

                    string physicalPath =
                        Path.Combine(folderPath, fileName);

                    model.Imagefile.SaveAs(physicalPath);

                    string imagePath =
                        "/Images/" + fileName;


                    // Insert Product
                    var product = new products2();

                    product.PName = model.Pname;
                    product.category = model.category;
                    product.Price = model.Price;
                    product.Gender = model.Gender;
                    product.Address = model.Address;
                    product.ImagePath = imagePath;

                    product.IsNew = model.IsNew;
                    product.IsFeatured = model.IsFeatured;
                    product.InStock = model.InStock;
                    product.AvailableForDelivery =
                        model.AvailableForDelivery;

                    db.products.Add(product);
                    db.SaveChanges();


                    // Insert Login
                    var login = new login2();

                    login.Username = model.username;
                    login.Password = model.password;
                    login.Pid = product.Pid;

                    db.logins.Add(login);
                    db.SaveChanges();


                    return Json(new
                    {
                        success = true,
                        message = "Record inserted successfully!"
                    });
                }


                // =========================
                // UPDATE
                // =========================

                var oldProduct =
                    db.products.Find(model.Pid);

                if (oldProduct == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Product not found."
                    });
                }


                // Update Product

                oldProduct.PName = model.Pname;
                oldProduct.category = model.category;
                oldProduct.Price = model.Price;
                oldProduct.Gender = model.Gender;
                oldProduct.Address = model.Address;

                oldProduct.IsNew = model.IsNew;
                oldProduct.IsFeatured = model.IsFeatured;
                oldProduct.InStock = model.InStock;
                oldProduct.AvailableForDelivery =
                    model.AvailableForDelivery;


                // Update image only if new image selected

                if (model.Imagefile != null &&
                    model.Imagefile.ContentLength > 0)
                {
                    string extension =
                        Path.GetExtension(
                            model.Imagefile.FileName)
                        .ToLower();

                    string fileName =
                        Guid.NewGuid().ToString() + extension;

                    string folderPath =
                        Server.MapPath("~/Images/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string physicalPath =
                        Path.Combine(folderPath, fileName);

                    model.Imagefile.SaveAs(physicalPath);

                    oldProduct.ImagePath =
                        "/Images/" + fileName;
                }


                // Update Login

                var loginData =
                    db.logins.FirstOrDefault(
                        x => x.Pid == model.Pid);

                if (loginData != null)
                {
                    loginData.Username = model.username;
                    loginData.Password = model.password;
                }


                db.SaveChanges();


                return Json(new
                {
                    success = true,
                    message = "Record updated successfully!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpPost]
        public JsonResult DeleteProduct(int Pid)
        {
            try
            {
                // --------------------------------------
                // FIND PRODUCT
                // --------------------------------------

                var product =
                    db.products.Find(Pid);

                if (product != null)
                {
                    db.products.Remove(product);
                }


                // ---------------------------------------------
                // FIND LOGIN
                // ---------------------------------------------

                var login =
                    db.logins.FirstOrDefault(
                        x => x.Pid == Pid);


                if (login != null)
                {
                    db.logins.Remove(login);
                }


                // ---------------------------------------------
                // DELETE
                // ---------------------------------------------

                db.SaveChanges();


                return Json(new
                {
                    success = true,
                    message = "Record deleted successfully!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpPost]
        public JsonResult CheckUsername(string username)
        {
            var login =
                    db.logins.FirstOrDefault(
                        x => x.Username == username);


            if (login != null)
            {
                return Json(new
                {
                    success = false,
                    message = "username already exist.please try different username"
                });
            }

            else
            {
                return Json(new
                {
                    success = true,
                    message = "username is available"
                });
            }

        }
    }

}












































//        [HttpPost]
//        public JsonResult products(ProductLoginViewModel model)
//        {
//            try
//            {
//                // -------------------------
//                // CHECKBOX VALIDATION
//                // At least one must be selected
//                // -------------------------

//                if (!model.IsNew &&
//                    !model.IsFeatured &&
//                    !model.InStock &&
//                    !model.AvailableForDelivery)
//                {
//                    return Json(new
//                    {
//                        success = false,
//                        message = "Please select at least one feature."
//                    });
//                }


//                // =========================
//                // INSERT
//                // =========================

//                if (model.Pid == 0)
//                {
//                    // Image is required for new product
//                    if (model.Imagefile == null ||
//                        model.Imagefile.ContentLength == 0)
//                    {
//                        return Json(new
//                        {
//                            success = false,
//                            message = "Please select an image."
//                        });
//                    }


//                    // Save Image
//                    string extension =
//                        Path.GetExtension(model.Imagefile.FileName)
//                            .ToLower();

//                    string fileName =
//                        Guid.NewGuid().ToString() + extension;

//                    string folderPath =
//                        Server.MapPath("~/Images/");

//                    string physicalPath =
//                        Path.Combine(folderPath, fileName);

//                    model.Imagefile.SaveAs(physicalPath);

//                    string imagePath =
//                        "/Images/" + fileName;


//                    // Insert Product
//                    var product = new products2();

//                    product.PName = model.Pname;
//                    product.category = model.category;
//                    product.Price = model.Price;
//                    product.Gender = model.Gender;
//                    product.Address = model.Address;
//                    product.ImagePath = imagePath;

//                    product.IsNew = model.IsNew;
//                    product.IsFeatured = model.IsFeatured;
//                    product.InStock = model.InStock;
//                    product.AvailableForDelivery =
//                        model.AvailableForDelivery;

//                    db.products.Add(product);
//                    db.SaveChanges();


//                    // Insert Login
//                    var login = new login2();

//                    login.Username = model.username;
//                    login.Password = model.password;
//                    login.Pid = product.Pid;

//                    db.logins.Add(login);
//                    db.SaveChanges();


//                    return Json(new
//                    {
//                        success = true,
//                        message = "Record inserted successfully!"
//                    });
//                }


//                // =========================
//                // UPDATE
//                // =========================

//                else
//                {
//                    var product =
//                        db.products.Find(model.Pid);

//                    if (product == null)
//                    {
//                        return Json(new
//                        {
//                            success = false,
//                            message = "Product not found."
//                        });
//                    }


//                    // Update Product
//                    product.PName = model.Pname;
//                    product.category = model.category;
//                    product.Price = model.Price;
//                    product.Gender = model.Gender;
//                    product.Address = model.Address;

//                    product.IsNew = model.IsNew;
//                    product.IsFeatured = model.IsFeatured;
//                    product.InStock = model.InStock;
//                    product.AvailableForDelivery =
//                        model.AvailableForDelivery;


//                    // Update Image only when
//                    // a new image is selected
//                    if (model.Imagefile != null &&
//                        model.Imagefile.ContentLength > 0)
//                    {
//                        string extension =
//                            Path.GetExtension(
//                                model.Imagefile.FileName)
//                            .ToLower();

//                        string fileName =
//                            Guid.NewGuid().ToString() +
//                            extension;

//                        string folderPath =
//                            Server.MapPath("~/Images/");

//                        string physicalPath =
//                            Path.Combine(
//                                folderPath,
//                                fileName);

//                        model.Imagefile.SaveAs(
//                            physicalPath);

//                        product.ImagePath =
//                            "/Images/" + fileName;
//                    }


//                    // Update Login
//                    var login =
//                        db.logins.FirstOrDefault(
//                            x => x.Pid == model.Pid);

//                    if (login != null)
//                    {
//                        login.Username = model.username;
//                        login.Password = model.password;
//                    }


//                    db.SaveChanges();
//                    db.SaveChanges();

//                    return Json(new
//                    {
//                        success = true,
//                        message = "Record updated successfully!"
//                    });
//                }
//                {
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new
//                {
//                    success = false,
//                    message = ex.Message
//                });


//            }
//        }


//        // =========================
//        // DELETE
//        // =========================

//        [HttpPost]
//        public JsonResult DeleteProduct(int Pid)
//        {
//            try
//            {
//                var product =
//                    db.products.Find(Pid);

//                if (product != null)
//                {
//                    db.products.Remove(product);
//                }

//                var login =
//                    db.logins.FirstOrDefault(
//                        x => x.Pid == Pid);

//                if (login != null)
//                {
//                    db.logins.Remove(login);
//                }

//                db.SaveChanges();

//                return Json(new
//                {
//                    success = true,
//                    message = "Record deleted successfully!"
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new
//                {
//                    success = false,
//                    message = ex.Message
//                });
//            }
//        }
//    }
//}













