// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    
    using Telerik.Web.Mvc.Extensions;

    internal static class ViewComponentExtensions
    {
        public static string GetValueFromViewDataByName(this IViewComponent instance)
        {
            string result = instance.ViewContext.Controller.ValueOf<string>(instance.Name);
            if (result.HasValue())
            {
                return result;
            }

            object viewDateValue = instance.ViewContext.ViewData.Eval(instance.Name);
            return Convert.ToString(viewDateValue) ?? string.Empty;
        }

        public static IDictionary<string, object> GetUnobtrusiveValidationAttributes(this IViewComponent instance)
        {    
#if MVC3
            var viewContext = instance.ViewContext;
            
            if (viewContext.UnobtrusiveJavaScriptEnabled)
            {
                var viewData = viewContext.ViewData;
                var htmlHelper = new HtmlHelper(viewContext, new ViewComponentViewDataContainer{ ViewData = viewData });

                var metadata = viewData.ModelMetadata;
                
                if (metadata != null && metadata.ContainerType == null)
                {
                    metadata = null;
                }
                
                return htmlHelper.GetUnobtrusiveValidationAttributes(instance.Name, metadata);
            }
#endif
            return null;
        }
    }

    internal class ViewComponentViewDataContainer : IViewDataContainer 
    {
        public ViewDataDictionary ViewData
        {
            get;
            set;
        }
    }
}
