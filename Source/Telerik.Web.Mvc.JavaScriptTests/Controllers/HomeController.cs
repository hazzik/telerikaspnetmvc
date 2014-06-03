namespace Telerik.Web.Mvc.JavaScriptTests.Controllers
{
    using System.Web.Mvc;
using System.Text.RegularExpressions;

    [HandleError]
    public class HomeController : Controller
    {
        Regex indexPageRegex = new Regex(@"JavascriptTests[/]?$", RegexOptions.Compiled);
        public ActionResult Index()
        {
            var requestedTestPage = Request.QueryString.Get("testpage");

            if (indexPageRegex.IsMatch(requestedTestPage))
            {
                requestedTestPage += (indexPageRegex.Match(requestedTestPage).Length == 15 ? "/" : "") + "Home/Suite";
                return RedirectToAction("Index", new { autorun = "true", testpage = requestedTestPage });
            }

            return View();
        }

        public ActionResult TestPage()
        {
            return View();
        }
    
        public ActionResult Suite()
        {
            return View();
        }

        public ActionResult GlobalizationSuite()
        {
            return View();
        }
    }
}
