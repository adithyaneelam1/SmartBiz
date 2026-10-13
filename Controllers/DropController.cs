using SunnyPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunnyPractice.Controllers
{
    public class DropController : Controller
    {
 
        //private ApplicationDbContext db = new ApplicationDbContext();

        MyDbContext db = new MyDbContext();

        // GET: Drop
        public ActionResult Down()
        {
            // Load Country dropdown
            ViewBag.CountryList = new SelectList(
                db.country.ToList(),
                "cid",
                "cname"
            );

            return View();
        }


        // COUNTRY → STATE
        public JsonResult GetStates(int countryid)
        {
            var states = db.state
                .Where(x => x.countryid == countryid)
                .Select(x => new
                {
                    sid = x.sid,
                    sname = x.sname
                })
                .ToList();

            return Json(
                states,
                JsonRequestBehavior.AllowGet
            );
        }

        // STATE → CITY
        public JsonResult GetCities(int stateid)
        {
            var cities = db.city
                .Where(x => x.stateid == stateid)
                .Select(x => new
                {
                    cid = x.cid,
                    cname = x.cname
                })
                .ToList();

            return Json(
                cities,
                JsonRequestBehavior.AllowGet
            );
        }
    }
}


