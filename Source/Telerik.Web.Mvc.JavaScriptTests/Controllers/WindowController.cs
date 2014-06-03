namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class WindowController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "Window";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ClientCreation()
        {
            return View();
        }

        public ActionResult ClientSideApi()
        {
            return View();
        }
    }
}
