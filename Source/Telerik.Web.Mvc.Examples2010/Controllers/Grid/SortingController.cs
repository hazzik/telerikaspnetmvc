namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;
	using Telerik.Web.Mvc.Examples.Models;

	public partial class GridController : Controller
	{
		public ActionResult Sorting(string sortMode)
		{
			ViewData["sortMode"] = sortMode ?? "SingleColumn";
			return View(GetOrders());
		}

		[GridAction]
		public ActionResult _Sorting()
		{
			return View(new GridModel<Order>
			{
				Data = GetOrders()
			});
		}
    }
}