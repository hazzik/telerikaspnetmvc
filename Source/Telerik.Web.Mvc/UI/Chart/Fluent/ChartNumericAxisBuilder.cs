// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring numeric axis.
    /// </summary>
    public class ChartNumericAxisBuilder : ChartAxisBuilderBase<IChartNumericAxis, ChartNumericAxisBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartNumericAxisBuilder"/> class.
        /// </summary>
        /// <param name="axis">The axis.</param>
        public ChartNumericAxisBuilder(IChartNumericAxis axis)
            : base(axis)
        {
        }

        /// <summary>
        /// Sets the axis minimum value.
        /// </summary>
        /// <param name="min">The axis minimum value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().Min(4))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartNumericAxisBuilder Min(double min)
        {
            Axis.Min = min;

            return this;
        }

        /// <summary>
        /// Sets the axis maximum value.
        /// </summary>
        /// <param name="max">The axis maximum value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().Max(4))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartNumericAxisBuilder Max(double max)
        {
            Axis.Max = max;

            return this;
        }

        /// <summary>
        /// Sets the interval between major divisions.
        /// </summary>
        /// <param name="majorUnit">The interval between major divisions.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().MajorUnit(4))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartNumericAxisBuilder MajorUnit(double majorUnit)
        {
            Axis.MajorUnit = majorUnit;

            return this;
        }

        /// <summary>
        /// Sets value at which the first perpendicular axis crosses this axis.
        /// </summary>
        /// <param name="axisCrossingValue">The value at which the first perpendicular axis crosses this axis.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().AxisCrossingValue(4))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartNumericAxisBuilder AxisCrossingValue(double axisCrossingValue)
        {
            Axis.AxisCrossingValue = axisCrossingValue;

            return this;
        }

        /// <summary>
        /// Sets the axis labels format
        /// </summary>
        /// <param name="format">The axis labels format.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .ValueAxis(a => a.Numeric().Format("{0:C}"))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartNumericAxisBuilder Format(string format)
        {
            Axis.Format = format;

            return this;
        }
    }
}