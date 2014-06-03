namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class CoreController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "Core";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult DateFormatting()
        {
            return View();
        }

        public ActionResult NumberFormatting()
        {
            return View();
        }
    }
}
