namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class DatePickerController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "DatePicker";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ClientEvents()
        {
            return View();
        }

        public ActionResult DateParsing()
        {
            return View();
        }

        public ActionResult DateView()
        {
            return View();
        }

        public ActionResult DatePicker() 
        {
            return View();
        }

        public ActionResult ParseByToken() 
        {
            return View();
        }

        public ActionResult ClientAPI()
        {
            return View();
        }

        public ActionResult Popup()
        {
            return View();
        }
    }
}
