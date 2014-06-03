﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents the default settings for all series in the <see cref="Telerik.Web.Mvc.UI.Chart{T}"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class ChartSeriesDefaults<T> : ChartSeriesBase<T>, IChartSeriesDefaults where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSeriesDefaults{T}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public ChartSeriesDefaults(Chart<T> chart)
            : base(chart)
        {
            Bar = new ChartBarSeries<T, object>(chart);
            Column = new ChartBarSeries<T, object>(chart);
            Line = new ChartLineSeries<T, object>(chart);
        }

        /// <summary>
        /// The default settings for all bar series.
        /// </summary>
        public IChartBarSeries Bar
        {
            get;
            private set;
        }

        /// <summary>
        /// The default settings for all column series.
        /// </summary>
        public IChartBarSeries Column
        {
            get;
            private set;
        }

        /// <summary>
        /// The default settings for all line series.
        /// </summary>
        public IChartLineSeries Line
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a serializer for the series defaults
        /// </summary>
        public override IChartSerializer CreateSerializer()
        {
            return new ChartSeriesDefaultsSerializer(this);
        }
    }
}