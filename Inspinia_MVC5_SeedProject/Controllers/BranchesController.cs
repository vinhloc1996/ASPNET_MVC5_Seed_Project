using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [HandleError]
    public class BranchesController : Controller
    {
        private ABContext db = new ABContext();

        public bool isAdmin(string username)
        {
            return db.Users.Find(username).isAccessAll;
        }

        // GET: /Branches
        public ActionResult Index()
        {
            if (!isAdmin(Session["username"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
//
//            return View(db.Branches.Where(x => x.id == db.Users.Find(Session["username"]).branch_id));

            return View(db.Branches.ToList());
        }

        // GET: /Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: /Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,address,create_date")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: /Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: /Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,address,create_date")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: /Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: /Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Branch branch = db.Branches.Find(id);
                db.Branches.Remove(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbe)
            {
                return RedirectToAction("Error", "Home",
                    new
                    {
                        msg =
                        "Your error occured in Branches controller when call action Delete. You cannot delete this branch because of using the relationship with other tables."
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

//        protected override void OnException(ExceptionContext filterContext)
//        {
//            if (!filterContext.ExceptionHandled)
//            {
//                string controller = filterContext.RouteData.Values["controller"].ToString();
//                string action = filterContext.RouteData.Values["action"].ToString();
//                Exception ex = filterContext.Exception;
////                ViewBag.ControllerErr = controller;
////                ViewBag.ActionErr = action;
////                ViewBag.ExceptionErr = ex;
//                RedirectToAction("Error", "Home", new {msg = "Your error occured in " + controller + " controller when call action " + action + " .\nError details is: " + ex.StackTrace});
//            }
//            base.OnException(filterContext);
//        }
    }
}