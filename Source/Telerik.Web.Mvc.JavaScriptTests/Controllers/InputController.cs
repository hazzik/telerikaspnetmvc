namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class InputController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "Input";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult NumericTextBox()
        {
            return View();
        }

        public ActionResult ClientEvents()
        {
            return View();
        }

        public ActionResult ClientAPI()
        {
            return View();
        }
    }
}
