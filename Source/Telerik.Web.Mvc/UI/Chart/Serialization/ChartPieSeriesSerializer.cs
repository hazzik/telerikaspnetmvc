﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
using System.Collections.Generic;
using Telerik.Web.Mvc.Infrastructure;

namespace Telerik.Web.Mvc.UI
{
    internal class ChartPieSeriesSerializer : ChartSeriesSerializer
    {
        private readonly IChartPieSeries series;

        public ChartPieSeriesSerializer(IChartPieSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", "pie")
                .Add("field", series.Member, () => { return series.Data == null && series.Member != null; })
                .Add("categoryField", series.CategoryMember, () => { return series.Data == null && series.CategoryMember != null; })
                .Add("explodeField", series.ExplodeMember, () => { return series.Data == null && series.ExplodeMember != null; })
                .Add("colorField", series.ColorMember, () => { return series.Data == null && series.ColorMember != null; })
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("padding", series.Padding, ChartDefaults.PieSeries.Padding)
                .Add("startAngle", series.StartAngle, ChartDefaults.PieSeries.StartAngle);

            if (series.Overlay != null)
            {
                result.Add("overlay", series.Overlay.Value);
            }

            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }

            var connectors = series.Connectors.CreateSerializer().Serialize();
            if (connectors.Count > 0)
            {
                result.Add("connectors", connectors);
            }

            return result;
        }
    }
}