namespace AltNorthwind
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Spark.Web.Mvc;

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("List", "Customer/Index/{page}/{orderBy}/{filter}", new { controller = "Customer", action = "Index", page = 1, orderBy = string.Empty, filter = string.Empty });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = string.Empty });
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            SparkEngineStarter.RegisterViewEngine();
        }
    }
}