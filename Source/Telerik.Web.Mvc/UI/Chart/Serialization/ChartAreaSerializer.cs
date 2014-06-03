// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartAreaSerializer : IChartSerializer
    {
        private readonly ChartArea chartArea;

        public ChartAreaSerializer(ChartArea charArea)
        {
            this.chartArea = charArea;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("background", chartArea.Background, "#fff")
                .Add("margin", chartArea.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("border", chartArea.Border.CreateSerializer().Serialize(), ShouldSerializeBorder);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return chartArea.Margin.Top != ChartDefaults.ChartArea.Margin ||
                   chartArea.Margin.Right != ChartDefaults.ChartArea.Margin ||
                   chartArea.Margin.Bottom != ChartDefaults.ChartArea.Margin ||
                   chartArea.Margin.Left != ChartDefaults.ChartArea.Margin;
        }

        private bool ShouldSerializeBorder()
        {
            return chartArea.Border.Color.CompareTo(ChartDefaults.ChartArea.Border.Color) != 0 ||
                   chartArea.Border.Width != ChartDefaults.ChartArea.Border.Width ||
                   chartArea.Border.DashType != ChartDefaults.ChartArea.Border.DashType;
        }
    }
}