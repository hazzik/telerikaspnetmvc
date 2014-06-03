// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents default chart series settings
    /// </summary>
    public interface IChartSeriesDefaults : IChartSeries
    {
        /// <summary>
        /// The default settings for all bar series
        /// </summary>
        IChartBarSeries Bar
        {
            get;
        }

        /// <summary>
        /// The default settings for all column series
        /// </summary>
        IChartBarSeries Column
        {
            get;
        }

        /// <summary>
        /// The default settings for all line series
        /// </summary>
        IChartLineSeries Line
        {
            get;
        }
    }
}