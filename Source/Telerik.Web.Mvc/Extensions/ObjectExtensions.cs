// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Web.Mvc.UI;

namespace Telerik.Web.Mvc.Extensions
{
    internal static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object instance)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (instance != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(instance))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(instance));
                }
            }
            return dictionary;
        }
    }
}
