namespace Telerik.Web.Mvc.Examples
{
	using System.Web.Mvc;

	public partial class GridController : Controller
	{
		public ActionResult AutoGenerateColumns()
		{
			return View(GetEditableCustomers());
		}
    }
}