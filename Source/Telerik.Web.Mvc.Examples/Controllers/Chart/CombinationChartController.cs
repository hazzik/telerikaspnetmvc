namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class ChartController : Controller
    {
        public ActionResult CombinationChart()
        {
            return View(SalesDataBuilder.GetCollection());
        }
    }
}