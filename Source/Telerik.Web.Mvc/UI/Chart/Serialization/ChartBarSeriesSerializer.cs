// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartBarSeriesSerializer : ChartSeriesSerializer
    {
        private readonly IChartBarSeries series;

        public ChartBarSeriesSerializer(IChartBarSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", series.Orientation == ChartBarSeriesOrientation.Horizontal ? "bar" : "column")
                .Add("stack", series.Stacked, false)
                .Add("gap", series.Gap, ChartDefaults.BarSeries.Gap)
                .Add("spacing", series.Spacing, ChartDefaults.BarSeries.Spacing)
                .Add("field", series.Member, () => { return series.Data == null && series.Member != null; })
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("border", series.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("color", series.Color, string.Empty)
                .Add("overlay", series.Overlay.ToString().ToLowerInvariant(), ChartDefaults.BarSeries.Overlay.ToString().ToLowerInvariant());

            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }

            return result;
        }

        private bool ShouldSerializeBorder()
        {
            return series.Border.Color.CompareTo(ChartDefaults.BarSeries.Border.Color) != 0 ||
                   series.Border.Width != ChartDefaults.BarSeries.Border.Width;
        }
    }
}