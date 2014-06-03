// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.Expressions
{
    using System;
    using System.Linq.Expressions;

    internal abstract class FilterExpressionBuilder
    {
        private readonly ParameterExpression parameterExpression;

        protected FilterExpressionBuilder(ParameterExpression parameterExpression)
        {
            this.parameterExpression = parameterExpression;
        }

        public ParameterExpression ParameterExpression
        {
            get
            {
                return this.parameterExpression;
            }
        }

        public abstract Expression CreateBodyExpression();

        /// <exception cref="ArgumentException"><c>ArgumentException</c>.</exception>
        public LambdaExpression CreateFilterExpression()
        {
            Expression bodyExpression = this.CreateBodyExpression();
            return Expression.Lambda(bodyExpression, this.ParameterExpression);
        }
    }
}