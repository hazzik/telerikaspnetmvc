namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Extensions;
    using Telerik.Web.Mvc.UI;

    public class TreeViewController : Controller
    {
        public ActionResult Index()
        {
            ViewData["controllerName"] = "TreeView";
            ViewData["actionNames"] = this.GetActions();

            return View("Suite");
        }

        public ActionResult ExpandCollapse()
        {
            return View();
        }

        public ActionResult ClientEvents()
        {
            return View();
        }

        public ActionResult ClientSideRendering()
        {
            return View();
        }

        public ActionResult LoadOnDemand()
        {
            return View();
        }

        public ActionResult DragAndDrop()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadOnDemand(TreeViewItem item)
        {
            return new JsonResult()
            {
                Data = new List<TreeViewItem>()
                {
                    new TreeViewItem() { Text = "Loaded", Enabled = true, LoadOnDemand = false, Value = "4" }
                }
            };
        }
    }
}