// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Creates value axis for the <see cref="Chart{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the chart is bound to</typeparam>
    public class ChartValueAxisFactory<TModel> : IHideObjectMembers
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartValueAxisFactory{TModel}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ChartValueAxisFactory(Chart<TModel> container)
        {
            Guard.IsNotNull(container, "container");

            Container = container;
        }

        /// <summary>
        /// The parent Chart
        /// </summary>
        public Chart<TModel> Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines a numeric value axis.
        /// </summary>
        public virtual ChartNumericAxisBuilder Numeric()
        {
            var numericAxis = new ChartNumericAxis<TModel>(Container);

            Container.ValueAxis = numericAxis;

            return new ChartNumericAxisBuilder(numericAxis);
        }
    }
}
