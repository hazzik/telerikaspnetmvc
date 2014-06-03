namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;
    using System.Threading;

    public partial class TabStripController : Controller
	{
        public ActionResult AjaxLoading()
        {
            return View();
        }

        public ActionResult AjaxView_OpenSource()
        {
            Thread.Sleep(1000);
            return PartialView();
        }

        public ActionResult AjaxView_ExceptionalPerformance()
        {
            Thread.Sleep(1000);
            return PartialView();
        }

        public ActionResult AjaxView_BasedOnJQuery()
        {
            Thread.Sleep(1000);
            return PartialView();
        }

        public ActionResult AjaxView_CrossBrowser()
        {
            Thread.Sleep(1000);
            return PartialView();
        }
    }
}