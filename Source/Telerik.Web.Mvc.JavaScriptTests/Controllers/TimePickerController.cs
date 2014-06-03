namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class TimePickerController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "TimePicker";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ClientAPI()
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

        public ActionResult KeyboardSupport() 
        {
            return View();
        }

        public ActionResult TimeView()
        {
            return View();
        }
    }
}