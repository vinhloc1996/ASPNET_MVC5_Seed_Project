using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Inspinia_MVC5_SeedProject.Models;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Welcome to ABEducation Project ";
            ViewData["Message"] =
                "This project is made by Loc Nguyen. The project is open source at https://github.com/vinhloc1996/ASPNET_MVC5_Seed_Project";
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User loginUser, string returnUrl)
        {
                User user = GetUser(loginUser.username, loginUser.password);
                if (user != null)
                {
                    Session["username"] = user.username;
                    Session["isAccessAll"] = user.isAccessAll.ToString();
                    Session["branch_id"] = user.branch_id;

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login details are wrong.");
                }
                return View(user);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private User GetUser(string username, string password)
        {

            using (var db = new ABContext())
            {
                var user = db.Users.FirstOrDefault(u => u.username == username);
                if (user != null)
                {
                    if (user.password == password)
                    {
                        return user;
                    }
                }
            }
            return null;
        }

        private Student GetStudent(string username, string password)
        {
            using (var db = new ABContext())
            {
                var student = db.Students.FirstOrDefault(u => u.email == username);
                if (student != null)
                {
                    if (student.password == password)
                    {
                        return student;
                    }
                }
            }
            return null;
        }

        public ActionResult LogOut()
        {
//            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Error(string msg)
        {
            ViewBag.err = msg;
            return View();
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}