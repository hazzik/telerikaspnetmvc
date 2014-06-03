// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq.Expressions;

    using Infrastructure;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GridColumnFactory<T> : IHideObjectMembers where T : class
    {
        private readonly IGridColumnContainer<T> container;

        public GridColumnFactory(IGridColumnContainer<T> container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual GridBoundColumnBuilder<T> Add(Expression<Func<T, object>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            GridColumn<T> column = new GridColumn<T>(expression);

            container.Columns.Add(column);

            return new GridBoundColumnBuilder<T>(column);
        }

        public virtual GridTemplateColumnBuilder<T> Add(Action<T> templateAction)
        {
            Guard.IsNotNull(templateAction, "templateAction");

            GridColumn<T> column = new GridColumn<T>(templateAction)
            {
                Filterable = false
            };

            container.Columns.Add(column);

            return new GridTemplateColumnBuilder<T>(column);
        }
    }
}