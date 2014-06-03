namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;

    using System.Web.Mvc;
    
    public class DropDownController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "DropDown";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult DropDown()
        {
            return View();
        }

        public ActionResult Loader()
        {
            return View();
        }
    }
}
