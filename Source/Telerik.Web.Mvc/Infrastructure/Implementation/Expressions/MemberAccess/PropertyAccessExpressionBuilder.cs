// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.Expressions
{
    using System;
    using System.Linq.Expressions;

    internal class PropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        public PropertyAccessExpressionBuilder(Type itemType, string memberName) : base(itemType, memberName)
        {
        }

        public override Expression CreateMemberAccessExpression()
        {
            //if no property specified then return the item itself
            if (string.IsNullOrEmpty(this.MemberName))
            {
                return this.ParameterExpression;
            }

            return ExpressionFactory.MakeMemberAccess(this.ParameterExpression, this.MemberName, true);
        }
    }
} 