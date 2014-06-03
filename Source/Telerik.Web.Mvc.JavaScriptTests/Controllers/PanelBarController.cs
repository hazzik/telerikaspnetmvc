namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class PanelBarController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "PanelBar";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ExpandCollapse()
        {
            return View();
        }

        public ActionResult SingleExpandCollapse() 
        {
            return View();
        }

        public ActionResult AjaxLoading()
        {
            return View();
        }

        public ActionResult ClientAPI() 
        {
            return View();
        }

        public ActionResult SingleExpandClientAPI() 
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
