// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the chart data labels.
    /// </summary>
    public class ChartLineMarkersBuilder
    {
        private readonly ChartLineMarkers lineMarkers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartLineMarkersBuilder" /> class.
        /// </summary>
        /// <param name="chartBarLabels">The line chart markers configuration.</param>
        public ChartLineMarkersBuilder(ChartLineMarkers chartLineMarkers)
        {
            lineMarkers = chartLineMarkers;
        }

        /// <summary>
        /// Sets the markers shape type.
        /// </summary>
        /// <param name="type">The markers shape type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Type(ChartMarkerShape.Triangle)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartLineMarkersBuilder Type(ChartMarkerShape type)
        {
            lineMarkers.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the markers size.
        /// </summary>
        /// <param name="visible">The markers size.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Size(10)
        ///               );
        ///            )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartLineMarkersBuilder Size(int size)
        {
            lineMarkers.Size = size;
            return this;
        }

        /// <summary>
        /// Sets the markers visibility
        /// </summary>
        /// <param name="visible">The markers visibility.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Line(s => s.Sales)
        ///               .Markers(markers => markers
        ///                   .Visible(true)
        ///               );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartLineMarkersBuilder Visible(bool visible)
        {
            lineMarkers.Visible = visible;
            return this;
        }

        /// <summary>
        /// Sets the markers border
        /// </summary>
        /// <param name="width">The markers border width.</param>
        /// <param name="color">The markers border color (CSS syntax).</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///                .Line(s => s.Sales)
        ///                .Markers(markers => markers
        ///                    .Border(1, "Red")
        ///                );
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartLineMarkersBuilder Border(int width, string color)
        {
            lineMarkers.Border = new ChartElementBorder(width, color);
            return this;
        }
    }
}