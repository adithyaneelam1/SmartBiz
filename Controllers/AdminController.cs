using AdharshaHighSchool.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdharshaHighSchool.Controllers
{
    public class AdminController : Controller
    {
        MyDbContext db = new MyDbContext();


        // =========================================
        // ADMIN LOGIN
        // =========================================

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }


        // POST: Admin/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var admin = db.Admins.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);

            if (admin != null)
            {
                Session["AdminId"] = admin.AdminId;
                Session["AdminName"] = admin.Name;
                return RedirectToAction("Index", "School");
            }

            ViewBag.Message = "Invalid username or password";

            return View();
        }


        // =========================================
        // DASHBOARD
        // =========================================

        public ActionResult Dashboard()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login", "Admin");
            }

            ViewBag.MessageCount =
                db.ContactMessages.Count();

            ViewBag.GalleryCount =
                db.Galleries.Count();

            ViewBag.AcademicCount =
                db.Academics.Count();

            ViewBag.FacilityCount =
                db.Facilities.Count();

            ViewBag.UnreadMessageCount =
                db.ContactMessages
                  .Count(x => !x.IsRead);

            return View();
        }


        // =========================================
        // ADMIN SETTINGS
        // =========================================

        // GET: Admin/Settings
        public ActionResult Settings()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var admin =
                db.Admins.FirstOrDefault();

            if (admin == null)
            {
                return HttpNotFound();
            }

            return View(
                "~/Views/Admin/Settings.cshtml",
                admin
            );
        }


        // POST: Admin/Settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Admin model)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var admin =
                db.Admins.Find(model.AdminId);

            if (admin == null)
            {
                ViewBag.Message =
                    "Admin record not found. AdminId = "
                    + model.AdminId;

                return View(
                    "~/Views/Admin/Settings.cshtml",
                    model
                );
            }


            // Update admin details
            admin.Name =
                model.Name;

            admin.Username =
                model.Username;

            admin.Email =
                model.Email;

            admin.Mobile =
                model.Mobile;


            // Change password only
            // when a new password is entered
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                admin.Password =
                    model.Password;
            }


            db.SaveChanges();


            ViewBag.Message =
                "Admin details saved successfully!";


            return View(
                "~/Views/Admin/Settings.cshtml",
                admin
            );
        }


        // =========================================
        // SCHOOL DETAILS
        // =========================================

        // IMPORTANT:
        // This method is intentionally unchanged.

        public ActionResult SchoolDetails()
        {
            var school =
                db.SchoolInfos.FirstOrDefault();

            return View(
                "~/Views/Admin/SchoolDetails.cshtml",
                school
            );
        }


        // =========================================
        // SCHOOL INFORMATION
        // =========================================

        // GET: Admin/SchoolInfo
        public ActionResult SchoolInfo()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var school =
                db.SchoolInfos.FirstOrDefault();

            return View(
                "~/Views/Admin/SchoolInfo.cshtml",
                school
            );
        }


        // POST: Admin/SchoolInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SchoolInfo(
            SchoolInfo model)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var school =
                db.SchoolInfos.FirstOrDefault();

            if (school == null)
            {
                return HttpNotFound();
            }


            school.SchoolName =
                model.SchoolName;

            school.Address =
                model.Address;

            school.Phone =
                model.Phone;

            school.Email =
                model.Email;

            school.AboutUs =
                model.AboutUs;

            school.Vision =
                model.Vision;

            school.Mission =
                model.Mission;

            school.PrincipalName =
                model.PrincipalName;

            school.PrincipalMessage =
                model.PrincipalMessage;


            db.SaveChanges();


            ViewBag.Message =
                "School information updated successfully!";


            return View(
                "~/Views/Admin/SchoolInfo.cshtml",
                school
            );
        }


        // =========================================
        // ACADEMICS
        // =========================================

        // GET: Admin/Academics
        public ActionResult Academics()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var academics =
                db.Academics
                  .OrderBy(x => x.DisplayOrder)
                  .ToList();

            return View(
                "~/Views/Admin/Academics.cshtml",
                academics
            );
        }


        // GET: Admin/AddAcademic
        public ActionResult AddAcademic()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var model =
                new Academic();

            return View(
                "~/Views/Admin/AddAcademic.cshtml",
                model
            );
        }


        // POST: Admin/AddAcademic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAcademic(
            Academic model)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                db.Academics.Add(model);

                db.SaveChanges();

                return RedirectToAction(
                    "Academics"
                );
            }

            return View(
                "~/Views/Admin/AddAcademic.cshtml",
                model
            );
        }


        // GET: Admin/EditAcademic
        public ActionResult EditAcademic(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var academic =
                db.Academics.Find(id);

            if (academic == null)
            {
                return HttpNotFound();
            }

            return View(
                "~/Views/Admin/EditAcademic.cshtml",
                academic
            );
        }


        // POST: Admin/EditAcademic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAcademic(
            Academic model)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                var academic =
                    db.Academics.Find(
                        model.AcademicId
                    );

                if (academic == null)
                {
                    return HttpNotFound();
                }


                academic.Title =
                    model.Title;

                academic.Description =
                    model.Description;

                academic.DisplayOrder =
                    model.DisplayOrder;

                academic.IsActive =
                    model.IsActive;


                db.SaveChanges();


                return RedirectToAction(
                    "Academics"
                );
            }

            return View(
                "~/Views/Admin/EditAcademic.cshtml",
                model
            );
        }


        // DELETE ACADEMIC
        public ActionResult DeleteAcademic(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var academic =
                db.Academics.Find(id);

            if (academic != null)
            {
                db.Academics.Remove(
                    academic
                );

                db.SaveChanges();
            }

            return RedirectToAction(
                "Academics"
            );
        }


        // =========================================
        // FACILITIES
        // =========================================

        // GET: Admin/Facilities
        public ActionResult Facilities()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var facilities =
                db.Facilities
                  .OrderBy(x => x.DisplayOrder)
                  .ToList();

            return View(
                "~/Views/Admin/Facilities.cshtml",
                facilities
            );
        }


        // GET: Admin/AddFacility
        public ActionResult AddFacility()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var model =
                new Facility();

            model.IsActive = true;
            model.DisplayOrder = 1;


            return View(
                "~/Views/Admin/AddFacility.cshtml",
                model
            );
        }


        // POST: Admin/AddFacility
        // POST: Add Facility
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFacility(
            Facility model,
            HttpPostedFileBase ImageFile)
        {
            string extension = null;


            // Check image
            if (ImageFile == null || ImageFile.ContentLength == 0)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Please select a facility image."
                );
            }
            else
            {
                // Get extension
                extension = System.IO.Path
                    .GetExtension(ImageFile.FileName)
                    .ToLower();


                // Allowed extensions
                string[] allowedExtensions =
                {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".webp"
        };


                // Check extension
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(
                        "ImageFile",
                        "Only JPG, JPEG, PNG, GIF and WEBP images are allowed."
                    );
                }
            }


            // Validate everything
            if (ModelState.IsValid)
            {
                // Create unique file name
                string fileName =
                    Guid.NewGuid().ToString("N") + extension;


                // Folder path
                string folderPath =
                    Server.MapPath("~/Folder1/Facilities/");


                // Create folder
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }


                // Full file path
                string filePath =
                    System.IO.Path.Combine(
                        folderPath,
                        fileName
                    );


                // Save uploaded image
                ImageFile.SaveAs(filePath);


                // Save image path
                model.Image =
                    "~/Folder1/Facilities/" + fileName;


                // Save facility to database
                db.Facilities.Add(model);

                db.SaveChanges();


                return RedirectToAction("Facilities");
            }


            // Return page if validation fails
            return View(
                "~/Views/Admin/AddFacility.cshtml",
                model
            );
        }


        // GET: Admin/EditFacility
        public ActionResult EditFacility(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var facility =
                db.Facilities.Find(id);

            if (facility == null)
            {
                return HttpNotFound();
            }

            return View(
                "~/Views/Admin/EditFacility.cshtml",
                facility
            );
        }


        // POST: Admin/EditFacility
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFacility(
            Facility model)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                var facility =
                    db.Facilities.Find(
                        model.FacilityId
                    );

                if (facility == null)
                {
                    return HttpNotFound();
                }


                facility.FacilityName =
                    model.FacilityName;

                facility.Description =
                    model.Description;

                facility.Image =
                    model.Image;

                facility.DisplayOrder =
                    model.DisplayOrder;

                facility.IsActive =
                    model.IsActive;


                db.SaveChanges();


                return RedirectToAction(
                    "Facilities"
                );
            }

            return View(
                "~/Views/Admin/EditFacility.cshtml",
                model
            );
        }


        // DELETE FACILITY
        public ActionResult DeleteFacility(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var facility =
                db.Facilities.Find(id);

            if (facility != null)
            {
                db.Facilities.Remove(
                    facility
                );

                db.SaveChanges();
            }

            return RedirectToAction(
                "Facilities"
            );
        }


        // =========================================
        // GALLERY
        // =========================================

        // GET: Admin/Gallery
        public ActionResult Gallery()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var galleries =
                db.Galleries
                  .OrderByDescending(
                      x => x.CreatedDate
                  )
                  .ToList();

            return View(
                "~/Views/Admin/Gallery.cshtml",
                galleries
            );
        }


        // GET: Admin/AddGallery
        public ActionResult AddGallery()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var model =
                new Gallery();

            model.CreatedDate =
                DateTime.Now;

            model.IsActive =
                true;


            return View(
                "~/Views/Admin/AddGallery.cshtml",
                model
            );
        }


        // POST: Admin/AddGallery
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGallery(
            Gallery model,
            HttpPostedFileBase ImageFile)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }


            if (ImageFile == null ||
                ImageFile.ContentLength == 0)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Please select an image."
                );

                return View(
                    "~/Views/Admin/AddGallery.cshtml",
                    model
                );
            }


            // Allowed extensions
            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".webp"
            };


            string extension =
                System.IO.Path
                    .GetExtension(
                        ImageFile.FileName
                    )
                    .ToLower();


            if (!allowedExtensions.Contains(
                    extension))
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Only JPG, JPEG, PNG, GIF and WEBP images are allowed."
                );

                return View(
                    "~/Views/Admin/AddGallery.cshtml",
                    model
                );
            }


            // Create unique file name
            string fileName =
                Guid.NewGuid()
                    .ToString("N")
                + extension;


            // Folder path
            string folderPath =
                Server.MapPath(
                    "~/Folder1/Gallery/"
                );


            // Create folder if it does not exist
            if (!System.IO.Directory.Exists(
                    folderPath))
            {
                System.IO.Directory.CreateDirectory(
                    folderPath
                );
            }


            // Full physical path
            string filePath =
                System.IO.Path.Combine(
                    folderPath,
                    fileName
                );


            // Save image
            ImageFile.SaveAs(
                filePath
            );


            // Save image path
            // to database
            model.Image =
                "~/Folder1/Gallery/"
                + fileName;


            model.CreatedDate =
                DateTime.Now;


            db.Galleries.Add(model);

            db.SaveChanges();


            return RedirectToAction(
                "Gallery"
            );
        }


        // GET: Admin/EditGallery
        public ActionResult EditGallery(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var gallery =
                db.Galleries.Find(id);

            if (gallery == null)
            {
                return HttpNotFound();
            }

            return View(
                "~/Views/Admin/EditGallery.cshtml",
                gallery
            );
        }


        // POST: Admin/EditGallery
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGallery(
            Gallery model,
            HttpPostedFileBase ImageFile)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }


            // Find existing gallery
            var gallery =
                db.Galleries.Find(
                    model.GalleryId
                );


            if (gallery == null)
            {
                return HttpNotFound();
            }


            // Update basic information
            gallery.Title =
                model.Title;

            gallery.Description =
                model.Description;

            gallery.IsActive =
                model.IsActive;


            // Check for new image
            if (ImageFile != null &&
                ImageFile.ContentLength > 0)
            {
                string[] allowedExtensions =
                {
                    ".jpg",
                    ".jpeg",
                    ".png",
                    ".gif",
                    ".webp"
                };


                string extension =
                    System.IO.Path
                        .GetExtension(
                            ImageFile.FileName
                        )
                        .ToLower();


                // Check extension
                if (!allowedExtensions.Contains(
                        extension))
                {
                    ModelState.AddModelError(
                        "ImageFile",
                        "Only JPG, JPEG, PNG, GIF and WEBP images are allowed."
                    );

                    return View(
                        "~/Views/Admin/EditGallery.cshtml",
                        model
                    );
                }


                // Delete old image
                if (!string.IsNullOrEmpty(
                        gallery.Image))
                {
                    string oldImagePath =
                        Server.MapPath(
                            gallery.Image
                        );


                    if (System.IO.File.Exists(
                            oldImagePath))
                    {
                        System.IO.File.Delete(
                            oldImagePath
                        );
                    }
                }


                // Create new file name
                string fileName =
                    Guid.NewGuid()
                        .ToString("N")
                    + extension;


                // Gallery folder
                string folderPath =
                    Server.MapPath(
                        "~/Folder1/Gallery/"
                    );


                // Create folder
                if (!System.IO.Directory.Exists(
                        folderPath))
                {
                    System.IO.Directory.CreateDirectory(
                        folderPath
                    );
                }


                // Full physical path
                string filePath =
                    System.IO.Path.Combine(
                        folderPath,
                        fileName
                    );


                // Save new image
                ImageFile.SaveAs(
                    filePath
                );


                // Update database path
                gallery.Image =
                    "~/Folder1/Gallery/"
                    + fileName;
            }


            // Save changes
            db.SaveChanges();


            return RedirectToAction(
                "Gallery"
            );
        }


        // DELETE GALLERY
        public ActionResult DeleteGallery(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var gallery =
                db.Galleries.Find(id);

            if (gallery != null)
            {
                db.Galleries.Remove(
                    gallery
                );

                db.SaveChanges();
            }

            return RedirectToAction(
                "Gallery"
            );
        }


        // =========================================
        // MESSAGES
        // =========================================

        // GET: Admin/Messages
        public ActionResult Messages()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var messages =
                db.ContactMessages
                  .OrderByDescending(
                      x => x.CreatedDate
                  )
                  .ToList();

            return View(
                "~/Views/Admin/Messages.cshtml",
                messages
            );
        }


        // GET: Admin/ViewMessage
        public ActionResult ViewMessage(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var message =
                db.ContactMessages.Find(id);


            if (message == null)
            {
                return HttpNotFound();
            }


            // Mark as read
            message.IsRead =
                true;

            db.SaveChanges();


            return View(
                "~/Views/Admin/ViewMessage.cshtml",
                message
            );
        }


        // GET: Admin/MarkMessageAsRead
        public ActionResult MarkMessageAsRead(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var message =
                db.ContactMessages.Find(id);


            if (message != null)
            {
                message.IsRead =
                    true;

                db.SaveChanges();
            }


            return RedirectToAction(
                "Messages"
            );
        }


        // GET: Admin/DeleteMessage
        public ActionResult DeleteMessage(
            int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var message =
                db.ContactMessages.Find(id);


            if (message != null)
            {
                db.ContactMessages.Remove(
                    message
                );

                db.SaveChanges();
            }


            return RedirectToAction(
                "Messages"
            );
        }


        // =========================================
        // LOGOUT
        // =========================================

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction(
                "Login"
            );
        }


        // =========================================
        // ADMIN LOGIN CHECK
        // =========================================

        private bool IsAdminLoggedIn()
        {
            return Session["AdminId"] != null;
        }


        // =========================================
        // DISPOSE
        // =========================================

        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}