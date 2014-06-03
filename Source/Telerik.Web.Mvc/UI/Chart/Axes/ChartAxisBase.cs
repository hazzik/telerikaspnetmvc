﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents a chart axis
    /// </summary>
    public abstract class ChartAxisBase<T> : IChartAxis where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartAxisBase{T}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public ChartAxisBase(Chart<T> chart)
        {
            Chart = chart;
            MinorTickSize = ChartDefaults.Axis.MinorTickSize;
            MajorTickSize = ChartDefaults.Axis.MajorTickSize;
            MajorTickType = ChartDefaults.Axis.MajorTickType;
            MinorTickType = ChartDefaults.Axis.MinorTickType;
            MajorGridLines = new ChartLine();
            MinorGridLines = new ChartLine();
            Line = new ChartLine(
                ChartDefaults.Axis.Line.Width,
                ChartDefaults.Axis.Line.Color,
                ChartDefaults.Axis.Line.Visible
            );
        }

        /// <summary>
        /// Gets or sets the chart.
        /// </summary>
        /// <value>The chart.</value>
        public Chart<T> Chart
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the minor tick size. The default is 3.
        /// </summary>
        public int MinorTickSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the major tick size. The default is 4.
        /// </summary>
        public int MajorTickSize
        {
            get;
            set;
        }

        /// <summary>
        /// The major tick type. The default is <see cref="ChartAxisTickType.Outside"/>.
        /// </summary>
        public ChartAxisTickType MajorTickType
        {
            get;
            set;
        }

        /// <summary>
        /// The minor tick type. The default is <see cref="ChartAxisTickType.None"/>.
        /// </summary>
        public ChartAxisTickType MinorTickType
        {
            get;
            set;
        }

        /// <summary>
        /// The major grid lines configuration.
        /// </summary>
        public ChartLine MajorGridLines
        {
            get;
            set;
        }

        /// <summary>
        /// The minor grid lines configuration.
        /// </summary>
        public ChartLine MinorGridLines
        {
            get;
            set;
        }

        /// <summary>
        /// The axis line configuration.
        /// </summary>
        public ChartLine Line
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the axis serializer.
        /// </summary>
        public abstract IChartSerializer CreateSerializer();
    }
}