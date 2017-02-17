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
    public class ResultsController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Results/
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Cours).Include(r => r.Student);
            return View(results.ToList());
        }

        // GET: /Results/Details/5
        public ActionResult Details(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.First(r => r.student_id == sid && r.course_id == cid);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: /Results/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "id", "name");
            ViewBag.student_id = new SelectList(db.Students, "id", "email");
            return View();
        }

        // POST: /Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,course_id,grade")] Result result)
        {
            if (ModelState.IsValid)
            {
                Enrollment enrollment = db.Enrollments.FirstOrDefault(e => e.student_id == result.student_id && e.course_id == result.course_id);
                
                if (enrollment != null)
                {
                    Result resultExisted = db.Results.FirstOrDefault(r => r.student_id == result.student_id && r.course_id == result.course_id);
                    if (resultExisted == null)
                    {
                        db.Results.Add(result);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Student was got the result already.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Student must be enroll this course before get result.");
                }
            }

            ViewBag.course_id = new SelectList(db.Courses, "id", "name", result.course_id);
            ViewBag.student_id = new SelectList(db.Students, "id", "email", result.student_id);
            return View(result);
        }

        // GET: /Results/Edit/5
        public ActionResult Edit(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.First(r => r.student_id == sid && r.course_id == cid);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "id", "name", result.course_id);
            ViewBag.student_id = new SelectList(db.Students, "id", "email", result.student_id);
            return View(result);
        }

        // POST: /Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,course_id,grade")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.Courses, "id", "name", result.course_id);
            ViewBag.student_id = new SelectList(db.Students, "id", "email", result.student_id);
            return View(result);
        }

        // GET: /Results/Delete/5
        public ActionResult Delete(int? sid, int? cid)
        {
            if (sid == null || cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.First(r => r.student_id == sid && r.course_id == cid);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: /Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? sid, int? cid)
        {
            Result result = db.Results.First(r => r.student_id == sid && r.course_id == cid);
            db.Results.Remove(result);
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