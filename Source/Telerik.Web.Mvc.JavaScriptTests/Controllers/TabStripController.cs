namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class TabStripController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "TabStrip";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult AjaxLoading()
        {
            return View();
        }

        public ActionResult ClientAPI() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxView1()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxView2()
        {
            return PartialView();
        }
    }
}
