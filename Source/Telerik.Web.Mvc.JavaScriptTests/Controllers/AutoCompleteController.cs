namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;

    using System.Web.Mvc;

    public class AutoCompleteController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "AutoComplete";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult Multiple()
        {
            return View();
        }

        public ActionResult KeyboardSupport()
        {
            return View();
        }
    }
}
