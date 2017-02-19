using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;
using Microsoft.Ajax.Utilities;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class SchedulesController : Controller
    {
        private ABContext db = new ABContext();

        // GET: /Schedules/
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Cours);
            if (Session["isAccessAll"].Equals("False"))
            {
                int a = db.Users.Find(Session["username"]).branch_id;
                return View(schedules.Where(s => s.Cours.branch_id == a).DistinctBy(s => s.course_id));
            }
                return View(schedules.DistinctBy(s => s.course_id));
        }

        // GET: /Schedules/Details/5
        public ActionResult Details(int? cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schedule = db.Schedules.Where(s => s.course_id == cid);
//            if (schedule == null)
//            {
//                return HttpNotFound();
//            }
            return View(schedule);
        }

        // GET: /Schedules/Create
        public ActionResult Create()
        {
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name");
            return View();
        }

        // POST: /Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,course_id,date_learn")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", schedule.course_id);
            return View(schedule);
        }

        // GET: /Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", schedule.course_id);
            return View(schedule);
        }

        // POST: /Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,course_id,date_learn")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int a = db.Users.Find(Session["username"]).branch_id;
            ViewBag.course_id = new SelectList(db.Courses.Where(c => c.branch_id == a), "id", "name", schedule.course_id);
            return View(schedule);
        }

        // GET: /Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: /Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
