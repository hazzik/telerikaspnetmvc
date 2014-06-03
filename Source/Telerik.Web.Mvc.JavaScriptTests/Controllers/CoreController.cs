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

        public ActionResult FormatString()
        {
            return View();
        }
        
        public ActionResult DateFormatting()
        {
            return View();
        }

        public ActionResult NumberFormatting()
        {
            return View();
        }

        public ActionResult TimeParsing() 
        {
            return View();
        }
    }
}
