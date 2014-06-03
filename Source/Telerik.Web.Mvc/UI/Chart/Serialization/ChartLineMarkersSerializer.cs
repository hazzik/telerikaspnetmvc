﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal class ChartLineMarkersSerializer : IChartSerializer
    {
        private readonly ChartLineMarkers lineMarker;

        public ChartLineMarkersSerializer(ChartLineMarkers chartLineMarker)
        {
            this.lineMarker = chartLineMarker;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            
            FluentDictionary.For(result)
                .Add("visible", lineMarker.Visible, ChartDefaults.LineSeries.Markers.Visible)
                .Add("border", lineMarker.Border.CreateSerializer().Serialize(), ShouldSerializeBorder)
                .Add("type", lineMarker.Type.ToString().ToLowerInvariant(), ChartDefaults.LineSeries.Markers.Type.ToString().ToLowerInvariant())
                .Add("background", lineMarker.Background, ChartDefaults.LineSeries.Markers.Background)
                .Add("size", lineMarker.Size, ChartDefaults.LineSeries.Markers.Size);

            return result;
        }

        private bool ShouldSerializeBorder()
        {
            return lineMarker.Border.Color.CompareTo(ChartDefaults.LineSeries.Markers.Border.Color) != 0 ||
                   lineMarker.Border.Width != ChartDefaults.LineSeries.Markers.Border.Width;
        }
    }
}