// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring axes.
    /// </summary>
    /// <typeparam name="TAxis"></typeparam>
    /// <typeparam name="TAxisBuilder">The type of the series builder.</typeparam>
    public abstract class ChartAxisBuilderBase<TAxis, TAxisBuilder> : IHideObjectMembers
        where TAxisBuilder : ChartAxisBuilderBase<TAxis, TAxisBuilder>
        where TAxis : IChartAxis
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartAxisBuilderBase{TAxis, TAxisBuilder}"/> class.
        /// </summary>
        /// <param name="axis">The axis.</param>
        protected ChartAxisBuilderBase(TAxis axis)
        {
            Guard.IsNotNull(axis, "series");

            Axis = axis;
        }

        /// <summary>
        /// Gets or sets the axis.
        /// </summary>
        /// <value>The axis.</value>
        public TAxis Axis
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the axis minor tick size.
        /// </summary>
        /// <param name="tickSize">The minor tick size.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().MinorTickSize(10))
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MinorTickSize(int minorTickSize)
        {
            Axis.MinorTickSize = minorTickSize;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets the axis major tick size.
        /// </summary>
        /// <param name="tickSize">The major tick size.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().MajorTickSize(10))
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MajorTickSize(int majorTickSize)
        {
            Axis.MajorTickSize = majorTickSize;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets the major tick type.
        /// </summary>
        /// <param name="majorTickType">The major tick type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().MajorTickType(ChartAxisTickType.Inside))
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MajorTickType(ChartAxisTickType majorTickType)
        {
            Axis.MajorTickType = majorTickType;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets the minor tick type.
        /// </summary>
        /// <param name="minorTickType">The minor tick type.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().MinorTickType(ChartAxisTickType.Inside))
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MinorTickType(ChartAxisTickType minorTickType)
        {
            Axis.MinorTickType = minorTickType;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Configures the major grid lines.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .MajorGridLines(lines => lines.Visible(true))
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MajorGridLines(Action<ChartLineBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartLineBuilder(Axis.MajorGridLines));

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets color and width of the major grid lines and enables them.
        /// </summary>
        /// <param name="color">The major gridlines width</param>
        /// <param name="width">The major gridlines color (CSS syntax)</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .MajorGridLines(2, "red")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MajorGridLines(int width, string color)
        {
            Axis.MajorGridLines.Width = width;
            Axis.MajorGridLines.Color = color;
            Axis.MajorGridLines.Visible = true;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Configures the minor grid lines.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .MinorGridLines(lines => lines.Visible(true))
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MinorGridLines(Action<ChartLineBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartLineBuilder(Axis.MinorGridLines));

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets color and width of the minor grid lines and enables them.
        /// </summary>
        /// <param name="color">The minor gridlines width</param>
        /// <param name="width">The minor gridlines color (CSS syntax)</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .MinorGridLines(2, "red")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder MinorGridLines(int width, string color)
        {
            Axis.MinorGridLines.Width = width;
            Axis.MinorGridLines.Color = color;
            Axis.MinorGridLines.Visible = true;

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Configures the axis line.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .Line(line => line.Color("#f00"))
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder Line(Action<ChartLineBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartLineBuilder(Axis.Line));

            return this as TAxisBuilder;
        }

        /// <summary>
        /// Sets color and width of the lines and enables them.
        /// </summary>
        /// <param name="color">The axis line width</param>
        /// <param name="width">The axis line color (CSS syntax)</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .CategoryAxis(axis => axis
        ///                 .Categories(s => s.DateString)
        ///                 .Line(2, "#f00")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TAxisBuilder Line(int width, string color)
        {
            Axis.Line.Width = width;
            Axis.Line.Color = color;
            Axis.Line.Visible = true;

            return this as TAxisBuilder;
        }
    }
}
