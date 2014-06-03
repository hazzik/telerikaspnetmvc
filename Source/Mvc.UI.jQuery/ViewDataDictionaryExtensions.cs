// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
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