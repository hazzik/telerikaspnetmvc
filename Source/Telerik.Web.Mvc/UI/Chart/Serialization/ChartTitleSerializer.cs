// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartTitleSerializer : IChartSerializer
    {
        private readonly ChartTitle title;

        public ChartTitleSerializer(ChartTitle title)
        {
            this.title = title;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("text", title.Text, string.Empty)
                .Add("font", title.Font, ChartDefaults.Title.Font)
                .Add("position", title.Position.ToString().ToLower(), ChartDefaults.Title.Position.ToString().ToLower())
                .Add("align", title.Align.ToString().ToLower(), ChartDefaults.Title.Align.ToString().ToLower())
                .Add("margin", title.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("padding", title.Padding.CreateSerializer().Serialize(), ShouldSerializePadding)
                .Add("border", title.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("visible", title.Visible, ChartDefaults.Title.Visible);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return  title.Margin.Top != ChartDefaults.Title.Margin ||
                    title.Margin.Right != ChartDefaults.Title.Margin ||
                    title.Margin.Bottom != ChartDefaults.Title.Margin ||
                    title.Margin.Left != ChartDefaults.Title.Margin;
        }

        private bool ShouldSerializePadding()
        {
            return title.Padding.Top != ChartDefaults.Title.Padding ||
                   title.Padding.Right != ChartDefaults.Title.Padding ||
                   title.Padding.Bottom != ChartDefaults.Title.Padding ||
                   title.Padding.Left != ChartDefaults.Title.Padding;
        }

        private bool ShouldSerializeBorder()
        {
            return title.Border.Color.CompareTo(ChartDefaults.Legend.Border.Color) != 0 ||
                   title.Border.Width != ChartDefaults.Legend.Border.Width;
        }
    }
}