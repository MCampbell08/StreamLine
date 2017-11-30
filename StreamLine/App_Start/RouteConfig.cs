using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StreamLine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserProfile",
                url: "UserProfile/{friend}",
                defaults: new { controller = "Account", action = "UserProfile", friend = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "RemoveFriend",
                 url: "Remove/{friend}",
                 defaults: new { controller = "Account", action = "RemoveFriend", friend = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
