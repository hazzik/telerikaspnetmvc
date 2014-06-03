// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Web.Mvc;

    internal static class ViewDataDictionaryExtensions
    {
        public static object GetConvertedModelStateValue(this ViewDataDictionary instance, string key, Type destinationType)
        {
            ModelState modelState;

            return instance.ModelState.TryGetValue(key, out modelState) ?
                   modelState.Value.ConvertTo(destinationType, null) :
                   null;
        }

        public static bool HasModeStateError(this ViewDataDictionary instance, string key)
        {
            ModelState modelState;

            return instance.ModelState.TryGetValue(key, out modelState) ?
                   (modelState.Errors.Count > 0) :
                   false;
        }
    }
}