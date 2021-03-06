namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.Extensions;
    using System;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/ElectricitySource.cs")]
        public ActionResult PieChart(bool? showLabels, string align, int? startAngle, int? padding, string position)
        {
            ViewBag.showLabels = showLabels ?? true;
            ViewBag.align = Enum.Parse(typeof(ChartPieLabelsAlign), align.HasValue() ? align : "Column");
            ViewBag.position = Enum.Parse(typeof(ChartPieLabelsPosition), position.HasValue() ? position : "OutsideEnd"); ;
            ViewBag.startAngle = startAngle ?? 90;
            ViewBag.padding = padding ?? 60;

            return View();
        }

        public ActionResult _SpainElectricity()
        {
            return Json(SpainElectricityStatsBuilder.GetCollection());
        }
    }
}