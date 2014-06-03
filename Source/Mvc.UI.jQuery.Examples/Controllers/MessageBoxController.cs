namespace Mvc.UI.jQuery.Examples
{
    using System.Web.Mvc;

    [AutoPopulateSourceCodes, HandleError]
    public class MessageBoxController : Controller
    {
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}