// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;

    /// <summary>
    /// Represents a category axis in the <see cref="Chart{T}"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class ChartCategoryAxis<T> : ChartAxisBase<T>, IChartCategoryAxis where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartCategoryAxis{T}" /> class.
        /// </summary>
        /// <param name="chart">The chart.</param>
        public ChartCategoryAxis(Chart<T> chart)
            : base(chart)
        {
            MajorGridLines = new ChartLine(
                ChartDefaults.Axis.Category.MajorGridLines.Width,
                ChartDefaults.Axis.Category.MajorGridLines.Color,
                ChartDefaults.Axis.Category.MajorGridLines.Visible);

            MinorGridLines = new ChartLine(
                ChartDefaults.Axis.Category.MinorGridLines.Width,
                ChartDefaults.Axis.Category.MinorGridLines.Color,
                ChartDefaults.Axis.Category.MinorGridLines.Visible);
        }

        /// <summary>
        /// The categories displayed on the axis
        /// </summary>
        public IEnumerable Categories
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the member name to be used as category.
        /// </summary>
        /// <value>The member.</value>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the axis serializer.
        /// </summary>
        public override IChartSerializer CreateSerializer()
        {
            return new ChartCategoryAxisSerializer(this);
        }
    }
}