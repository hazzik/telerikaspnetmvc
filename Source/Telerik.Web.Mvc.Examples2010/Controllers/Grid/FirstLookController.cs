namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;
	using Telerik.Web.Mvc.Examples.Models;

	public partial class GridController : Controller
    {
        public ActionResult FirstLook(bool? ajax, bool? scrolling, bool? paging, bool? filtering, bool? sorting)
        {
            ViewData["ajax"] = ajax ?? true;
            ViewData["scrolling"] = scrolling ?? true;
            ViewData["paging"] = paging ?? true;
            ViewData["filtering"] = filtering ?? true;
            ViewData["sorting"] = sorting ?? true;

            return View(GetOrders());
        }

		[GridAction]
		public ActionResult _FirstLook()
		{
			return View(new GridModel<Order>
			{
				Data = GetOrders()
			});
		}
    }
}