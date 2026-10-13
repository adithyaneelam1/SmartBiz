using AdharshaHighSchool.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SunnyPractice.Controllers
{
    public class SchoolController : Controller
    {
        private MyDbContext db = new MyDbContext();

        //HOME
        //HOME
        public ActionResult Index()
        {
            // Admin login check
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            var school = db.SchoolInfos.FirstOrDefault();

            return View(school);
        }

        //public ActionResult Index()
        //{
        //    var school = db.SchoolInfos.FirstOrDefault();

        //    return View(school);
        //}

        // ABOUT US
        public ActionResult About()
        {
            var school = db.SchoolInfos.FirstOrDefault();

            return View(school);
        }

        // ACADEMICS
        public ActionResult Academics()
        {
            var academics = db.Academics
                              .Where(x => x.IsActive)
                              .OrderBy(x => x.DisplayOrder)
                              .ToList();

            return View(academics);
        }

        // FACILITIES
        public ActionResult Facilities()
        {
            var facilities = db.Facilities
                               .Where(x => x.IsActive)
                               .OrderBy(x => x.DisplayOrder)
                               .ToList();

            return View(facilities);
        }


        // GALLERY
        public ActionResult Gallery()
        {
            var galleries = db.Galleries
                              .Where(x => x.IsActive)
                              .OrderByDescending(x => x.CreatedDate)
                              .ToList();

            return View(galleries);
        }



        // CONTACT
        // CONTACT - GET
        public ActionResult Contact()
        {
            var school = db.SchoolInfos.FirstOrDefault();

            ViewBag.School = school;

            return View(new ContactViewModel());
        }
        // CONTACT - POST


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            var school = db.SchoolInfos.FirstOrDefault();

            ViewBag.School = school;

            if (ModelState.IsValid)
            {
                var message = new ContactMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Message = model.Message,
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };

                db.ContactMessages.Add(message);
                db.SaveChanges();

                ViewBag.SuccessMessage =
                    "Thank you! Your message has been sent successfully.";

                ModelState.Clear();

                return View(new ContactViewModel());
            }

            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}