// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Infrastructure;

    public static class GridBindingContextExtensions
    {
        public static T ValueOf<T>(this IGridBindingContext context, string key)
        {
            return context.ValueProvider.ValueOf<T>(context.Prefix(key));
        }

        public static T ValueOf<T>(this IDictionary<string, ValueProviderResult> valueProvider, string key)
        {
            ValueProviderResult result;

            bool found = valueProvider.TryGetValue(key, out result);

            if (found)
            {
                return (T) result.ConvertTo(typeof(T), Culture.Current);
            }

            return default(T);
        }
    }
}