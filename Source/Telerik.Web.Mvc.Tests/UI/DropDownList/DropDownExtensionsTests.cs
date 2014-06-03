﻿namespace Telerik.Web.Mvc.UI.Tests.UI
{
    using Xunit;

    using System.Linq;

    public class DropDownExtensionsTests
    {
        private readonly DropDownList dropdownlist;

        public DropDownExtensionsTests()
        {
            dropdownlist = DropDownListTestHelper.CreateDropDownList();
            dropdownlist.Name = "dropdownlist";

            dropdownlist.Items.Add(new DropDownItem { Text = "item1", Value = "item1" });
            dropdownlist.Items.Add(new DropDownItem { Text = "item2", Value = "item2" });
            dropdownlist.Items.Add(new DropDownItem { Text = "item3", Value = "item3" });
            dropdownlist.Items.Add(new DropDownItem { Text = "item4", Value = "item4" });
        }

        [Fact]
        public void PrepareItemsAndDefineSelectedIndex_should_preserve_last_selected_item()
        {
            dropdownlist.Items[0].Selected = true;
            dropdownlist.Items[2].Selected = true;

            dropdownlist.PrepareItemsAndDefineSelectedIndex();

            Assert.Equal(true, dropdownlist.Items[2].Selected);
        }

        [Fact]
        public void PrepareItemsAndDefineSelectedIndex_should_select_item_if_selectedIndex_is_set()
        {
            dropdownlist.SelectedIndex = 3;

            dropdownlist.PrepareItemsAndDefineSelectedIndex();

            Assert.Equal(true, dropdownlist.Items[3].Selected);
        }

        [Fact]
        public void PrepareItemsAndDefineSelectedIndex_should_select_first_item_if_no_selected_items()
        {
            dropdownlist.PrepareItemsAndDefineSelectedIndex();

            Assert.Equal(true, dropdownlist.Items[0].Selected);
        }

        [Fact]
        public void PrepareItemsAndDefineSelectedIndex_should_not_select_any_item_if_no_selectedItems_and_component_is_comboBox()
        {
            var combobox = ComboBoxTestHelper.CreateComboBox();

            combobox.Items.Add(new DropDownItem { Text = "item1", Value = "item1" });
            combobox.Items.Add(new DropDownItem { Text = "item2", Value = "item2" });
            combobox.Items.Add(new DropDownItem { Text = "item3", Value = "item3" });
            combobox.Items.Add(new DropDownItem { Text = "item4", Value = "item4" });

            combobox.PrepareItemsAndDefineSelectedIndex();

            combobox.Items.Where(i => i.Selected == true);

            Assert.Equal(0, combobox.Items.Where(i => i.Selected == true).Count());
        }
    }
}
