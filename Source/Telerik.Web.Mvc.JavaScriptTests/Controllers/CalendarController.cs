namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "Calendar";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult Navigation()
        {
            return View();
        }

        public ActionResult ClientEvents()
        {
            return View();
        }

        public ActionResult Rendering()
        {
            return View();
        }
    }
}