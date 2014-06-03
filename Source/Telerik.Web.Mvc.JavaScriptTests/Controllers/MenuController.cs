namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class MenuController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "Menu";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult OpenOnClick()
        {
            return View();
        }

        public ActionResult ClientAPI() 
        {
            return View();
        }
    }
}
