// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents chart line chart series
    /// </summary>
    public interface IChartLineSeries : IChartBoundSeries
    {
        /// <summary>
        /// A value indicating if the lines should be stacked.
        /// </summary>
        bool Stacked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the line chart data labels configuration
        /// </summary>
        ChartLineLabels Labels
        {
            get;
        }

        /// <summary>
        /// The line chart markers configuration.
        /// </summary>
        ChartLineMarkers Markers
        {
            get;
            set;
        }

        /// <summary>
        /// The line chart line width.
        /// </summary>
        double Width
        {
            get;
            set;
        }
    }
}