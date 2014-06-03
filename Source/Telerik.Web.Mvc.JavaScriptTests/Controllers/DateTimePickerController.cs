namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class DateTimePickerController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "DateTimePicker";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ClientApi()
        {
            return View();
        }

        public ActionResult ClientEvents()
        {
            return View();
        }
        
        public ActionResult KeyboardSupport()
        {
            return View();
        }

        public ActionResult ComponentInitialization()
        {
            return View();
        }
    }
}
