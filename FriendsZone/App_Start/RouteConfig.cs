using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FriendsZone
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Account", "{userName}", new { Controller = "Account", action = "UserName" });

            routes.MapRoute("CreateAccount", "Account/CreateAccount", new { Controller = "Account", action = "CreateAccount" });
            routes.MapRoute("Default", "", new { Controller = "Account", action = "Index" });
            //routes.MapRoute(
            //name: "Default",
            //url: "{controller}/{action}/{id}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
