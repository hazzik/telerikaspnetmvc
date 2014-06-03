// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static string LastMemberName<T>(this Expression<Func<T, object>> expression) where T : class
        {
            MemberExpression memberExpression = expression.ToMemberExpression();
            return (memberExpression != null) ? memberExpression.Member.Name : null;
        }

        public static string MemberWithoutInstance<T>(this Expression<Func<T, object>> expression) where T : class
        {
            MemberExpression memberExpression = expression.ToMemberExpression();

            if (memberExpression == null)
            {
                return null;
            }

            if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression innerMemberExpression = (MemberExpression)memberExpression.Expression;

                return memberExpression.ToString().Substring(innerMemberExpression.Expression.ToString().Length + 1);
            }

            return memberExpression.Member.Name;
        }

        public static MemberExpression ToMemberExpression<T>(this Expression<Func<T, object>> expression) where T : class
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            return memberExpression;
        }
    }
}
