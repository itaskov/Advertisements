using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Advertisements.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Advertisements",
                url: "Advertisements/Pagination/{status}",
                defaults: new { controller = "Advertisements", action = "Pagination", status = UrlParameter.Optional },
                namespaces: new[] { "Advertisements.Web.Controllers" }
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Advertisements.Web.Controllers" }
            );
        }
    }
}
