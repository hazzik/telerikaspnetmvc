// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents the options of the line chart labels
    /// </summary>
    public class ChartLineLabels : ChartDataLabels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartLineLabels" /> class.
        /// </summary>
        public ChartLineLabels()
        {
            Font = ChartDefaults.DataLabels.Font;
            Position = ChartDefaults.LineSeries.Labels.Position;
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
        /// The default value is <see cref="ChartLineLabelsPosition.Above"/> for clustered series and
        /// <see cref="ChartLineLabelsPosition.Above"/> for stacked series.
        /// </remarks>
        public ChartLineLabelsPosition Position
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a serializer
        /// </summary>
        public override IChartSerializer CreateSerializer()
        {
            return new ChartLineLabelsSerializer(this);
        }
    }
}