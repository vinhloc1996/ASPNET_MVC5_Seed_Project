using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [HandleError]
    public class CoursController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Cours/
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Branch);
            if (Session["isAccessAll"].Equals("False"))
            {
                int a = db.Users.Find(Session["username"]).branch_id;
                return View(courses.Where(c => c.branch_id == a));
            }
            return View(courses.ToList());
        }

        // GET: /Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: /Cours/Create
        public ActionResult Create()
        {
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name");
            return View();
        }

        // POST: /Cours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,branch_id,key")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                if (Session["isAccessAll"].Equals("False"))
                {
                    int a = db.Users.Find(Session["username"]).branch_id;
                    cours.branch_id = a;
                }
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", cours.branch_id);
            return View(cours);
        }

        // GET: /Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", cours.branch_id);
            return View(cours);
        }

        // POST: /Cours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,branch_id,key")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", cours.branch_id);
            return View(cours);
        }

        // GET: /Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: /Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cours cours = db.Courses.Find(id);
                db.Courses.Remove(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbe)
            {
                return RedirectToAction("Error", "Home",
                    new
                    {
                        msg =
                        "Your error occured in Courses controller when call action Delete. You cannot delete this branch because of using the relationship with other tables."
                    });
            }
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