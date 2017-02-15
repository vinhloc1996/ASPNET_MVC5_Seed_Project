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
            ViewData["SubTitle"] = "Welcome in ASP.NET MVC 5 INSPINIA SeedProject ";
            ViewData["Message"] =
                "It is an application skeleton for a typical MVC 5 project. You can use it to quickly bootstrap your webapp projects.";

            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User LoginUser, string returnUrl)
        {
            User user = GetUser(LoginUser.username, LoginUser.password);
            if (user != null)
            {
                Session["username"] = user.username;

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

        public ActionResult LogOut()
        {
//            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}