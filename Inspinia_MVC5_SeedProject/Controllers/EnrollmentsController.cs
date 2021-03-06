﻿using System;
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
    public class EnrollmentsController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Enrollments/
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Cours).Include(e => e.Student);
            if (Session["isAccessAll"].Equals("False"))
            {
                int a = db.Users.Find(Session["username"]).branch_id;
                return View(enrollments.Where(e => e.Cours.branch_id == a && e.Student.branch_id == a));
            }
            return View(enrollments.ToList());
        }

        // GET: /Enrollments/Details/5
        public ActionResult Details(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.First(e => e.student_id == sid && e.course_id == cid);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: /Enrollments/Create
        public ActionResult Create()
        {
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name");
            ViewBag.student_id = new SelectList(db.Students.Where(s => s.branch_id == a), "id", "email");
            return View();
        }

        // POST: /Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,course_id,create_date,isPresent")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                Enrollment enrollmentExisted =
                    db.Enrollments.FirstOrDefault(
                        e => e.student_id == enrollment.student_id && e.course_id == enrollment.course_id);
                if (enrollmentExisted == null)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Student was enrolled this course already.");
                }
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", enrollment.course_id);
            ViewBag.student_id = new SelectList(db.Students.Where(s => s.branch_id == a), "id", "email", enrollment.student_id);
            return View(enrollment);
        }

        // GET: /Enrollments/Edit/5
        public ActionResult Edit(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.First(e => e.student_id == sid && e.course_id == cid);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", enrollment.course_id);
            ViewBag.student_id = new SelectList(db.Students.Where(s => s.branch_id == a), "id", "email", enrollment.student_id);
            return View(enrollment);
        }

        // POST: /Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,course_id,create_date,isPresent")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", enrollment.course_id);
            ViewBag.student_id = new SelectList(db.Students.Where(s => s.branch_id == a), "id", "email", enrollment.student_id);
            return View(enrollment);
        }

        // GET: /Enrollments/Delete/5
        public ActionResult Delete(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.First(e => e.student_id == sid && e.course_id == cid);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: /Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int sid, int cid)
        {
            Enrollment enrollment = db.Enrollments.First(e => e.student_id == sid && e.course_id == cid);
            if (ModelState.IsValid)
            {
                Result resultExisted = db.Results.FirstOrDefault(r => r.student_id == sid && r.course_id == cid);
                if (resultExisted == null)
                {
                    db.Enrollments.Remove(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Student was got the result already, cannot delete this record.");
                }
            }
            return View(enrollment);
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