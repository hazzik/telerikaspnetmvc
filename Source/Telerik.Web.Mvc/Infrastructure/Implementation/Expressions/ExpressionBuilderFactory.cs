// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.Expressions
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Xml;

    using Extensions;

    internal static class ExpressionBuilderFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "memberType")]
        public static MemberAccessExpressionBuilderBase MemberAccess(Type elementType, Type memberType, string memberName)
        {
            memberType = memberType ?? typeof(object);

            if (elementType.IsCompatibleWith(typeof(DataRow)))
            {
                return new DataRowFieldAccessExpressionBuilder(memberType, memberName);
            }

            if (elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)))
            {
                return new CustomTypeDescriptorPropertyAccessExpressionBuilder(elementType, memberType, memberName);
            }

            if (elementType.IsCompatibleWith(typeof(XmlNode)))
            {
                return new XmlNodeChildElementAccessExpressionBuilder(memberName);
            }

            return new PropertyAccessExpressionBuilder(elementType, memberName);
        }
    }
}