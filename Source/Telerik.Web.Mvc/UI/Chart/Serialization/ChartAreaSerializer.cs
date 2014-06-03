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
        private readonly ChartArea charArea;

        public ChartAreaSerializer(ChartArea charArea)
        {
            this.charArea = charArea;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("background", charArea.Background, "#fff")
                .Add("margin", charArea.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("border", charArea.Border.CreateSerializer().Serialize(), ShouldSerializeBorder);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return charArea.Margin.Top != ChartDefaults.ChartArea.Margin ||
                   charArea.Margin.Right != ChartDefaults.ChartArea.Margin ||
                   charArea.Margin.Bottom != ChartDefaults.ChartArea.Margin ||
                   charArea.Margin.Left != ChartDefaults.ChartArea.Margin;
        }

        private bool ShouldSerializeBorder()
        {
            return charArea.Border.Color.CompareTo(ChartDefaults.ChartArea.Border.Color) != 0 ||
                   charArea.Border.Width != ChartDefaults.ChartArea.Border.Width;
        }
    }
}