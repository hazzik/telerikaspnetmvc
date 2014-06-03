namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class ChartController : Controller
    {
        public ActionResult ClientSideApi()
        {
            return View();
        }

        public ActionResult _SalesDataRandom()
        {
            var salesData = SalesDataBuilder.GetCollection();
            var rnd = new Random();

            // Randomize data
            foreach (var data in salesData)
            {
                var multiplier = (decimal) rnd.NextDouble();
                data.RepSales = Math.Round(data.RepSales * multiplier);
                data.TotalSales = Math.Round(data.TotalSales * multiplier);
            }

            return Json(salesData);
        }
    }
}