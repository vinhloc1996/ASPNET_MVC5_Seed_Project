using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class UsersController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Users/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Branch);
            if (Session["isAccessAll"].Equals("False"))
            {
                int a = db.Users.Find(Session["username"]).branch_id;
                return View(users.Where(c => c.branch_id == a));
            }
            return View(users.ToList());
        }

        // GET: /Users/Details/5
        public ActionResult Details(string id)
        {
            if (Session["isAccessAll"].Equals("False"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            if (Session["isAccessAll"].Equals("False"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,password,email,address,branch_id,isAccessAll")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", user.branch_id);
            return View(user);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["isAccessAll"].Equals("False"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", user.branch_id);
            return View(user);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,password,email,address,branch_id,isAccessAll")] User user)
        {
            if (ModelState.IsValid)
            {
                if (Session["username"].Equals(user.username))
                {
                    user.isAccessAll = true;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", user.branch_id);
            return View(user);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["isAccessAll"].Equals("False"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword(string id)
        {
            User user = db.Users.Find(id);
            user.password = user.username;
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