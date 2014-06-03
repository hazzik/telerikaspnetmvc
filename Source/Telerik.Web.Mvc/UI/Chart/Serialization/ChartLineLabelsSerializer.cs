// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartLineLabelsSerializer : IChartSerializer
    {
        private readonly ChartLineLabels lineLabels;

        public ChartLineLabelsSerializer(ChartLineLabels lineLabels)
        {
            this.lineLabels = lineLabels;
        }

        public IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("font", lineLabels.Font, ChartDefaults.DataLabels.Font)
                .Add("position", lineLabels.Position.ToString().ToCamelCase(), ChartDefaults.LineSeries.Labels.Position.ToString().ToCamelCase())
                .Add("margin", lineLabels.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("padding", lineLabels.Padding.CreateSerializer().Serialize(), ShouldSerializePadding)
                .Add("border", lineLabels.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("color", lineLabels.Color, ChartDefaults.DataLabels.Color)
                .Add("background", lineLabels.Background, string.Empty)
                .Add("format", lineLabels.Format, string.Empty)
                .Add("visible", lineLabels.Visible, ChartDefaults.DataLabels.Visible);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return lineLabels.Margin.Top != ChartDefaults.DataLabels.Margin ||
                   lineLabels.Margin.Right != ChartDefaults.DataLabels.Margin ||
                   lineLabels.Margin.Bottom != ChartDefaults.DataLabels.Margin ||
                   lineLabels.Margin.Left != ChartDefaults.DataLabels.Margin;
        }

        private bool ShouldSerializePadding()
        {
            return lineLabels.Padding.Top != ChartDefaults.DataLabels.Padding ||
                   lineLabels.Padding.Right != ChartDefaults.DataLabels.Padding ||
                   lineLabels.Padding.Bottom != ChartDefaults.DataLabels.Padding ||
                   lineLabels.Padding.Left != ChartDefaults.DataLabels.Padding;
        }

        private bool ShouldSerializeBorder()
        {
            return lineLabels.Border.Color.CompareTo(ChartDefaults.DataLabels.Border.Color) != 0 ||
                   lineLabels.Border.Width != ChartDefaults.DataLabels.Border.Width;
        }
    }
}