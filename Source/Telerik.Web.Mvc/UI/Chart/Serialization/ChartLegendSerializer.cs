﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartLegendSerializer : IChartSerializer
    {
        private readonly ChartLegend legend;

        public ChartLegendSerializer(ChartLegend legend)
        {
            this.legend = legend;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();

            FluentDictionary.For(result)
                .Add("font", legend.Font, ChartDefaults.Legend.Font)
                .Add("position", legend.Position.ToString().ToLower(), ChartDefaults.Legend.Position.ToString().ToLower())
                .Add("offsetX", legend.OffsetX, 0)
                .Add("offsetY", legend.OffsetY, 0)
                .Add("margin", legend.Margin.CreateSerializer().Serialize(), ShouldSerializeMargin)
                .Add("padding", legend.Padding.CreateSerializer().Serialize(), ShouldSerializePadding)
                .Add("border", legend.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("visible", legend.Visible, ChartDefaults.Legend.Visible);

            return result;
        }

        private bool ShouldSerializeMargin()
        {
            return  legend.Margin.Top != ChartDefaults.Legend.Margin ||
                    legend.Margin.Right != ChartDefaults.Legend.Margin ||
                    legend.Margin.Bottom != ChartDefaults.Legend.Margin ||
                    legend.Margin.Left != ChartDefaults.Legend.Margin;
        }

        private bool ShouldSerializePadding()
        {
            return legend.Padding.Top != ChartDefaults.Legend.Padding ||
                   legend.Padding.Right != ChartDefaults.Legend.Padding ||
                   legend.Padding.Bottom != ChartDefaults.Legend.Padding ||
                   legend.Padding.Left != ChartDefaults.Legend.Padding;
        }

        private bool ShouldSerializeBorder()
        {
            return legend.Border.Color.CompareTo(ChartDefaults.Legend.Border.Color) != 0 ||
                   legend.Border.Width != ChartDefaults.Legend.Border.Width;
        }
    }
}