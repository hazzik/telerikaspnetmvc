// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class CustomAttributeProviderExtensions
    {
        public static string GetDisplayName(this ICustomAttributeProvider member)
        {
            return member.GetCustomAttributes(typeof(DisplayAttribute), false)
                         .OfType<DisplayAttribute>()
                         .Select(attribute => attribute.Name)
                         .LastOrDefault();
        }

        public static string GetFormat(this ICustomAttributeProvider member)
        {
            return member.GetCustomAttributes(typeof(DisplayFormatAttribute), false)
                         .OfType<DisplayFormatAttribute>()
                         .Select(attribute => attribute.DataFormatString)
                         .LastOrDefault();
        }
    }
}