// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;
    using System.Collections.Generic;

    public class ComboBoxHtmlBuilderTests
    {

        private ComboBox combobox;
        private ComboBoxHtmlBuilder renderer;

        public ComboBoxHtmlBuilderTests()
        {
            combobox = ComboBoxTestHelper.CreateComboBox();
            renderer = new ComboBoxHtmlBuilder(combobox);
        }

        [Fact]
        public void Build_should_output_start_tag()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal("div", tag.TagName);
        }

        [Fact]
        public void Build_should_output_combobox_css_class()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal("t-widget t-combobox t-header", tag.Attribute("class"));
        }

        [Fact]
        public void Build_should_output_combobox_css_class_and_append_custom_css_classes()
        {
            combobox.HtmlAttributes.Add("class", "myLovelyClass");

            IHtmlNode tag = renderer.Build();

            Assert.Equal("t-widget t-combobox t-header myLovelyClass", tag.Attribute("class"));
        }

        [Fact]
        public void Build_should_output_id_attribute()
        {
            combobox.Name = "ComboBox";

            IHtmlNode tag = renderer.Build();

            Assert.Equal(combobox.Id, tag.Attribute("id"));
        }

        [Fact]
        public void Build_should_output_html_attributes()
        {
            combobox.HtmlAttributes.Add("height", "100px");

            IHtmlNode tag = renderer.Build();

            Assert.Equal("100px", tag.Attribute("height"));
        }

        [Fact]
        public void InnerContentTag_should_output_input_html_elemnt()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("input", tag.Children[0].TagName);
        }

        [Fact]
        public void InnerContentTag_should_output_input_css_class()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal(UIPrimitives.Input, tag.Children[0].Attribute("class"));
        }

        [Fact]
        public void InnerContentTag_should_output_input_type_text()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("text", tag.Children[0].Attribute("type"));
        }

        [Fact]
        public void InnerContentTag_should_output_input_title()
        {
            combobox.Name = "ComboBox";

            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal(combobox.Id, tag.Children[ 0 ].Attribute("title"));
        }

        [Fact]
        public void InnerContentTag_should_html_input_element_with_id_attribute()
        {
            combobox.Name = "ComboBox";

            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal(combobox.Id + "-input", tag.Children[0].Attribute("id"));
        }

        [Fact]
        public void InnerContentTag_should_html_input_element_output_html_attributes()
        {
            combobox.InputHtmlAttributes.Add("width", "100px");

            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("100px", tag.Children[0].Attribute("width"));
        }

        [Fact]
        public void InnerContentTag_should_not_render_value_if_there_is_no_appropriate() 
        {
            combobox.Items.Clear();

            IHtmlNode tag = renderer.InnerContentTag();
            Assert.Throws(typeof(System.Collections.Generic.KeyNotFoundException), () => tag.Children[0].Attribute("value"));
        }

        [Fact]
        public void InnerContentTag_should_not_render_value_if_selectedIndex_is_negative()
        {
            combobox.Items.Add(new DropDownItem { Text = "Item1", Value = "1" });
            combobox.Items.Add(new DropDownItem { Text = "Item2", Value = "2" });

            combobox.SelectedIndex = -1;

            IHtmlNode tag = renderer.InnerContentTag();
            Assert.Throws(typeof(System.Collections.Generic.KeyNotFoundException), () => tag.Children[0].Attribute("value"));
        }

        [Fact]
        public void InnerContentTag_should_add_attr_value_with_selected_item_text() 
        {
            combobox.Items.Add(new DropDownItem { Text = "Item1", Value = "1" });
            combobox.Items.Add(new DropDownItem { Text = "Item2", Value = "2" });

            combobox.SelectedIndex = 1;

            IHtmlNode tag = renderer.InnerContentTag();
            
            Assert.Equal("Item2", tag.Children[0].Attribute("value"));
        }

        [Fact]
        public void InnerContentTag_should_output_span_tag()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("span", tag.Children[1].TagName);
        }

        [Fact]
        public void InnerContentTag_should_span_with_select_css_class()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("t-select t-header", tag.Children[1].Attribute("class"));
        }

        [Fact]
        public void InnerContentTag_should_output_child_element_span()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("span", tag.Children[1].Children[0].TagName);
        }

        [Fact]
        public void InnerContentTag_should_output_child_element_span_with_icon_css_class()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("t-icon t-arrow-down", tag.Children[1].Children[0].Attribute("class"));
        }

        [Fact]
        public void InnerContentTag_should_output_child_element_span_with_innerHtml()
        {
            IHtmlNode tag = renderer.InnerContentTag();

            Assert.Equal("select", tag.Children[1].Children[0].InnerHtml);
        }

        [Fact]
        public void HiddenInputTag_should_render_input()
        {
            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.Equal("input", tag.TagName);
        }

        [Fact]
        public void HiddenInputTag_should_output_input_type_text_()
        {
            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.Equal("text", tag.Attribute("type"));
        }

        [Fact]
        public void HiddenInputTag_should_output_input_which_is_hidden_with_style()
        {
            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.Equal("display:none", tag.Attribute("style"));
        }

        [Fact]
        public void HiddenInputTag_should_output_input_with_name()
        {
            combobox.Name = "test";

            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.Equal(combobox.Id + "-value", tag.Attribute("id"));
            Assert.Equal(combobox.Name, tag.Attribute("name"));
        }

        [Fact]
        public void HiddenInputTag_should_not_render_value_if_there_is_no_appropriate()
        {
            combobox.Items.Clear();

            IHtmlNode tag = renderer.HiddenInputTag();
            Assert.Throws(typeof(System.Collections.Generic.KeyNotFoundException), () => tag.Attribute("value"));
        }

        [Fact]
        public void HiddenInputTag_should_not_render_value_if_selectedIndex_is_negative()
        {
            combobox.Items.Add(new DropDownItem { Text = "Item1", Value = "1" });
            combobox.Items.Add(new DropDownItem { Text = "Item2", Value = "2" });

            combobox.SelectedIndex = -1;

            IHtmlNode tag = renderer.HiddenInputTag();
            Assert.Throws(typeof(System.Collections.Generic.KeyNotFoundException), () => tag.Attribute("value"));
        }

        [Fact]
        public void HiddenInputTag_should_add_attr_value_with_selected_item_text()
        {
            combobox.Items.Add(new DropDownItem { Text = "Item1", Value = "1" });
            combobox.Items.Add(new DropDownItem { Text = "Item2", Value = "2" });

            combobox.SelectedIndex = 1;

            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.Equal("2", tag.Attribute("value"));
        }

        [Fact]
        public void HiddenInputTag_does_not_output_name_attribute_for_unnamed_components()
        {
            var renderer = new ComboBoxHtmlBuilder(new EditorComboBox("FontFace", new List<DropDownItem>() { new DropDownItem { Text = "Arial", Value = "Arial,Verdana,sans-serif" } } ));

            IHtmlNode tag = renderer.HiddenInputTag();

            Assert.False(tag.Attributes().ContainsKey("name"));
        }
    }
}
