// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;

    using System.Web;
    using System.Linq;
    using System.Collections.Generic;   

    internal static class DropDownExtentions
    {
        internal static void SyncSelectedIndex(this IDropDown instance)
        {
            int selectedItemIndex = -1;
            IList<DropDownItem> items = instance.Items;
            string value = instance.Value.HasValue() ? instance.Value : instance.GetValueFromViewDataByName();

            if (value.HasValue())
            {
                selectedItemIndex = items.IndexOf(items.FirstOrDefault(item => (item.Value ?? item.Text).ToLower() == value.ToLower()));
            }

            if (selectedItemIndex == -1)
            {
                selectedItemIndex = items.IndexOf(items.LastOrDefault(item => item.Selected == true));
            }

            if (selectedItemIndex != -1)
            {
                for (int i = 0, length = instance.Items.Count; i < length; i++)
                {
                    instance.Items[i].Selected = false;
                }

                instance.Items[selectedItemIndex].Selected = true;
                instance.SelectedIndex = selectedItemIndex;
            }
            else if (instance.SelectedIndex != -1 && instance.SelectedIndex < instance.Items.Count)
            {
                instance.Items[instance.SelectedIndex].Selected = true;
            }
            else if (instance is DropDownList)
            {
                instance.Items[0].Selected = true;
                instance.SelectedIndex = 0;
            }
        }

        internal static void EncodeTextPropertyofItems(this IDropDown instance) 
        {
            foreach (DropDownItem item in instance.Items)
            {
                item.Text = HttpUtility.HtmlEncode(item.Text);
            }
        }
    }
}
