using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SunnyPractice.Models;
using EntityState = System.Data.Entity.EntityState;

namespace SunnyPractice.Controllers
{
    public class login2Controller : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: login2
        public ActionResult Index()
        {
            return View(db.logins.ToList());
        }

        // GET: login2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            login2 login2 = db.logins.Find(id);
            if (login2 == null)
            {
                return HttpNotFound();
            }
            return View(login2);
        }

        // GET: login2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: login2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Pid")] login2 login2)
        {
            if (ModelState.IsValid)
            {
                db.logins.Add(login2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login2);
        }

        // GET: login2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            login2 login2 = db.logins.Find(id);
            if (login2 == null)
            {
                return HttpNotFound();
            }
            return View(login2);
        }

        // POST: login2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Pid")] login2 login2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login2);
        }

        // GET: login2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            login2 login2 = db.logins.Find(id);
            if (login2 == null)
            {
                return HttpNotFound();
            }
            return View(login2);
        }

        // POST: login2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            login2 login2 = db.logins.Find(id);
            db.logins.Remove(login2);
            db.SaveChanges();
            return RedirectToAction("Index");
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
