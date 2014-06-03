// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.Expressions
{
    using System;
    using System.Linq.Expressions;

    internal abstract class ExpressionBuilderBase
    {
        private readonly Type itemType;
        private ParameterExpression parameterExpression;

        protected ExpressionBuilderBase(Type itemType)
        {
            this.itemType = itemType;
        }

        protected internal Type ItemType
        {
            get
            {
                return this.itemType;
            }
        }

        protected internal ParameterExpression ParameterExpression
        {
            get
            {
                if (this.parameterExpression == null)
                {
                    this.parameterExpression = Expression.Parameter(this.ItemType, "item");
                }

                return this.parameterExpression;
            }
            set
            {
                this.parameterExpression = value;
            }
        }
    }
}