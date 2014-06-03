// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the chart data labels.
    /// </summary>
    public class ChartLineLabelsBuilder : ChartDataLabelsBuilderBase<ChartLineLabelsBuilder>
    {
        private readonly ChartLineLabels lineLabels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartLineLabelsBuilder" /> class.
        /// </summary>
        /// <param name="chartBarLabels">The data labels configuration.</param>
        public ChartLineLabelsBuilder(ChartLineLabels chartLineLabels)
            : base(chartLineLabels)
        {
            lineLabels = chartLineLabels;
        }

        /// <summary>
        /// Sets the labels position
        /// </summary>
        /// <param name="position">The data labels position.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Labels(labels => labels
        ///                   .Position(ChartLineLabelsPosition.Above)
        ///                   .Visible(true)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartLineLabelsBuilder Position(ChartLineLabelsPosition position)
        {
            lineLabels.Position = position;
            return this;
        }
    }
}