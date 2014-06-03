// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents the options of the bar chart labels
    /// </summary>
    public class ChartBarLabels : ChartDataLabels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBarLabels" /> class.
        /// </summary>
        public ChartBarLabels()
        {
            Font = ChartDefaults.DataLabels.Font;
            Position = ChartDefaults.BarSeries.Labels.Position;
            Visible = ChartDefaults.DataLabels.Visible;
            Margin = new ChartSpacing(ChartDefaults.DataLabels.Margin);
            Padding = new ChartSpacing(ChartDefaults.DataLabels.Padding);
            Border = new ChartElementBorder(ChartDefaults.DataLabels.Border.Width, ChartDefaults.DataLabels.Border.Color);
            Color = ChartDefaults.DataLabels.Color;
        }

        /// <summary>
        /// Gets or sets the label position.
        /// </summary>
        /// <remarks>
        /// The default value is <see cref="ChartBarLabelsPosition.OutsideEnd"/> for clustered series and
        /// <see cref="ChartBarLabelsPosition.InsideEnd"/> for stacked series.
        /// </remarks>
        public ChartBarLabelsPosition Position
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        public override IChartSerializer CreateSerializer()
        {
            return new ChartBarLabelsSerializer(this);
        }
    }
}