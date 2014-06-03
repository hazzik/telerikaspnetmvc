namespace Mvc.UI.jQuery.Examples.SparkView
{
    using System.Web.Mvc;

    [AutoPopulateSourceCodes, HandleError]
    public class ProgressBarController : Controller
    {
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult AutoRetrieve()
        {
            ViewData["myProgressBar"] = 40;

            return View();
        }

        public ActionResult UpdateElements()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }
    }
}