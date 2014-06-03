using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Telerik.Web.Mvc.Examples
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CustomRoute",
                "{controller}/CustomRoute/{page}/{orderBy}/{filter}",
                new { controller = "Grid", action = "CustomRoute", page = 1, orderBy = "", filter = "" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "FirstLook", id = "" });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            SiteMapManager.SiteMaps.Register<XmlSiteMap>("examples", sitmap => sitmap.Load());
        }
    }
}