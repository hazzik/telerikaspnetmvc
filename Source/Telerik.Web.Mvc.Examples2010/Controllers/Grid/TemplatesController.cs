namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;

	public partial class GridController : Controller
	{
		public ActionResult Templates()
		{
			return View(GetCustomers());
		}
    }
}