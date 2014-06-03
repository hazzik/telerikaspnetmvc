// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using System.Linq;
    using System.Collections.Generic;

    internal static class DropDownRenderableExtentions
    {
        internal static void UpdateSelectedIndexFromViewContext(this IDropDownRenderable instance)
        {
            IList<DropDownItem> items = instance.Items;
            string value = instance.ViewContext.Controller.ValueOf<string>(instance.Name);
            if (string.IsNullOrEmpty(value))
            {
                object viewDataValue = instance.ViewContext.ViewData.Eval(instance.Name);
                value = viewDataValue != null ? viewDataValue.ToString() : "";
            }

            if (value.HasValue() && items.Any())
            {
                int index = items.IndexOf(items.Where(item => (item.Value ?? item.Text).ToLower() == value.ToLower()).FirstOrDefault());
                if (index != -1)
                {
                    instance.SelectedIndex = index;
                }
            }
        }
    }
}
