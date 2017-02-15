using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject;

namespace Inspinia_MVC5_SeedProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
//            filters.Add(new SessionFilterAttribute());
        }
    }
}
