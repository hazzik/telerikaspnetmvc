// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class PlotAreaSerializer : IChartSerializer
    {
        private readonly PlotArea plotArea;

        public PlotAreaSerializer(PlotArea plotArea)
        {
            this.plotArea = plotArea;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("background", plotArea.Background, "#fff")
                .Add("margin", plotArea.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("border", plotArea.Border.CreateSerializer().Serialize(), ShouldSerializeBorder);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return plotArea.Margin.Top != ChartDefaults.PlotArea.Margin ||
                   plotArea.Margin.Right != ChartDefaults.PlotArea.Margin ||
                   plotArea.Margin.Bottom != ChartDefaults.PlotArea.Margin ||
                   plotArea.Margin.Left != ChartDefaults.PlotArea.Margin;
        }

        private bool ShouldSerializeBorder()
        {
            return plotArea.Border.Color.CompareTo(ChartDefaults.PlotArea.Border.Color) != 0 ||
                   plotArea.Border.Width != ChartDefaults.PlotArea.Border.Width;
        }
    }
}