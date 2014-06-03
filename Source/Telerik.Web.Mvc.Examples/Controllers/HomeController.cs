namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;

	[PopulateSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
	public class HomeController : Controller
	{
        public ActionResult FirstLook()
		{
			return View();
		}
	}
}