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
    public class StudentsController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Students/
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Branch);
            if (Session["isAccessAll"].Equals("False"))
            {
                int a = db.Users.Find(Session["username"]).branch_id;
                return View(students.Where(s => s.branch_id == a));
            }
            return View(students.ToList());
        }

        // GET: /Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Students/Create
        public ActionResult Create()
        {
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name");
            return View();
        }

        // POST: /Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,password,fullname,DOB,address,email,branch_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (Session["isAccessAll"].Equals("False"))
                {
                    int a = db.Users.Find(Session["username"]).branch_id;
                    student.branch_id = a;
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", student.branch_id);
            return View(student);
        }

        // GET: /Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", student.branch_id);
            return View(student);
        }

        // POST: /Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,password,fullname,DOB,address,email,branch_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.branch_id = new SelectList(db.Branches, "id", "name", student.branch_id);
            return View(student);
        }

        // GET: /Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbe)
            {
                return RedirectToAction("Error", "Home",
                    new
                    {
                        msg =
                        "Your error occured in Students controller when call action Delete. You cannot delete this branch because of using the relationship with other tables."
                    });
            }
        }

        public ActionResult ResetPassword(int id)
        {
            Student student = db.Students.Find(id);
            student.password = student.email;
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