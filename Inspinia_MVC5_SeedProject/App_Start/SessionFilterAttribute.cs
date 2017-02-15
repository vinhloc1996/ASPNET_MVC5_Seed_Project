using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inspinia_MVC5_SeedProject
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var route = filterContext.HttpContext.Request.RequestContext.RouteData;
            var username = filterContext.HttpContext.Session.Contents["username"];

            if ("Users Branches Students Cours".Contains(route.GetRequiredString("controller")) && username == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Home");

                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}