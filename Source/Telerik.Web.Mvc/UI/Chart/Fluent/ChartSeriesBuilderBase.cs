// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring series.
    /// </summary>
    /// <typeparam name="TSeries"></typeparam>
    /// <typeparam name="TSeriesBuilder">The type of the series builder.</typeparam>
    public abstract class ChartSeriesBuilderBase<TSeries, TSeriesBuilder> : IHideObjectMembers
        where TSeriesBuilder : ChartSeriesBuilderBase<TSeries, TSeriesBuilder>
        where TSeries : IChartSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSeriesBuilderBase{TSeries, TSeriesBuilder}"/> class.
        /// </summary>
        /// <param name="series">The series.</param>
        protected ChartSeriesBuilderBase(TSeries series)
        {
            Guard.IsNotNull(series, "series");

            Series = series;
        }

        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        /// <value>The series.</value>
        public TSeries Series
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the series title displayed in the legend.
        /// </summary>
        /// <param name="text">The title.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.Bar(s => s.Sales).Name("Sales"))
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Name(string text)
        {
            Series.Name = text;

            return this as TSeriesBuilder;
        }

        /// <summary>
        /// Sets the series opacity.
        /// </summary>
        /// <param name="opacity">
        /// The series opacity in the range from 0 (transparent) to 1 (opaque).
        /// The default value is 1.
        /// </param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.Bar(s => s.Sales).Opacity(0.5))
        /// %&gt;
        /// </code>
        /// </example>
        public TSeriesBuilder Opacity(double opacity)
        {
            Series.Opacity = opacity;

            return this as TSeriesBuilder;
        }

        /// <summary>
        /// Sets the bar fill color
        /// </summary>
        /// <param name="color">The bar fill color (CSS syntax).</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///            .Name("Chart")
        ///            .Series(series => series.Bar(s => s.Sales).Color("Red"))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public TSeriesBuilder Color(string color)
        {
            Series.Color = color;

            return this as TSeriesBuilder;
        }
    }
}