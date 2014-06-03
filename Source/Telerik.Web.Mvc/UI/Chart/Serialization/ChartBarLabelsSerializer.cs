// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartBarLabelsSerializer : IChartSerializer
    {
        private readonly ChartBarLabels barLabels;

        public ChartBarLabelsSerializer(ChartBarLabels barLabels)
        {
            this.barLabels = barLabels;
        }

        public IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("font", barLabels.Font, ChartDefaults.DataLabels.Font)
                .Add("position", barLabels.Position.ToString().ToCamelCase(), ChartDefaults.BarSeries.Labels.Position.ToString().ToCamelCase())
                .Add("margin", barLabels.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("padding", barLabels.Padding.CreateSerializer().Serialize(), ShouldSerializePadding)
                .Add("border", barLabels.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("color", barLabels.Color, ChartDefaults.DataLabels.Color)
                .Add("background", barLabels.Background, string.Empty)
                .Add("format", barLabels.Format, string.Empty)
                .Add("visible", barLabels.Visible, ChartDefaults.DataLabels.Visible);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return barLabels.Margin.Top != ChartDefaults.DataLabels.Margin ||
                   barLabels.Margin.Right != ChartDefaults.DataLabels.Margin ||
                   barLabels.Margin.Bottom != ChartDefaults.DataLabels.Margin ||
                   barLabels.Margin.Left != ChartDefaults.DataLabels.Margin;
        }

        private bool ShouldSerializePadding()
        {
            return barLabels.Padding.Top != ChartDefaults.DataLabels.Padding ||
                   barLabels.Padding.Right != ChartDefaults.DataLabels.Padding ||
                   barLabels.Padding.Bottom != ChartDefaults.DataLabels.Padding ||
                   barLabels.Padding.Left != ChartDefaults.DataLabels.Padding;
        }

        private bool ShouldSerializeBorder()
        {
            return barLabels.Border.Color.CompareTo(ChartDefaults.DataLabels.Border.Color) != 0 ||
                   barLabels.Border.Width != ChartDefaults.DataLabels.Border.Width;
        }
    }
}