// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents the Plot area options
    /// </summary>
    public class PlotArea
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlotArea" /> class.
        /// </summary>
        public PlotArea()
        {
            Background = ChartDefaults.PlotArea.background;
            Margin = new ChartSpacing(ChartDefaults.PlotArea.Margin);
            Border = new ChartElementBorder(
                    ChartDefaults.PlotArea.Border.Width,
                    ChartDefaults.PlotArea.Border.Color,
                    ChartDefaults.PlotArea.Border.DashType
                );
        }

        /// <summary>
        /// Gets or sets the Plot area background.
        /// </summary>
        /// <value>
        /// The Plot area background.
        /// </value>
        public string Background
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Plot area border.
        /// </summary>
        /// <value>
        /// The Chart area border.
        /// </value>
        public ChartElementBorder Border
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Plot area margin.
        /// </summary>
        /// <value>
        /// The Chart area margin.
        /// </value>
        public ChartSpacing Margin
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        public IChartSerializer CreateSerializer()
        {
            return new PlotAreaSerializer(this);
        }
    }
}