namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;
	using Telerik.Web.Mvc.Examples.Models;

	public partial class GridController : Controller
	{
		public ActionResult RelatedGrids()
		{
			ViewData["Customers"] = GetCustomers();
			ViewData["Orders"] = GetOrdersForCustomer("ALFKI");

			return View();
		}

		[GridAction]
		public ActionResult _RelatedGrids_Orders(string customerID)
		{
			customerID = customerID ?? "ALFKI";

			return View(new GridModel<Order>
			{
				Data = GetOrdersForCustomer(customerID)
			});
		}

		[GridAction]
		public ActionResult _RelatedGrids_Customers()
		{
			return View(new GridModel<Customer>
			{
				Data = GetCustomers()
			});
		}
    }
}