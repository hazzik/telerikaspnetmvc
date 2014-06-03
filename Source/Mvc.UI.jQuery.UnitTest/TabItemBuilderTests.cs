// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;

    using Xunit;

    public class TabItemBuilderTests
    {
        private readonly TabItem _item;
        private readonly TabItemBuilder _builder;

        public TabItemBuilderTests()
        {
            _item = new TabItem();
            _builder = new TabItemBuilder(_item);
        }

        [Fact]
        public void ToItem_should_return_internal_item()
        {
            Assert.Same(_item, _builder.ToItem());
        }

        [Fact]
        public void TabItem_operator_should_return_internal_item()
        {
            TabItem item = _builder;

            Assert.Same(_item, item);
        }

        [Fact]
        public void Should_be_able_to_set_html_attributes()
        {
            _builder.HtmlAttributes(new { @class = "foo" });

            Assert.Equal("foo", _item.HtmlAttributes["class"]);
        }

        [Fact]
        public void Should_be_able_to_set_text()
        {
            _builder.Text("Test Tab Item");

            Assert.Equal("Test Tab Item", _item.Text);
        }

        [Fact]
        public void Should_be_able_to_set_load_content_from()
        {
            _builder.LoadContentFrom("/Home/Items");

            Assert.Equal("/Home/Items", _item.LoadContentFromUrl);
        }

        [Fact]
        public void Should_be_able_to_set_content_html_attributes()
        {
            _builder.ContentHtmlAttributes(new { @class = "foo" });

            Assert.Equal("foo", _item.ContentHtmlAttributes["class"]);
        }

        [Fact]
        public void Should_be_able_to_set_content()
        {
            Action content = delegate { };

            _builder.Content(content);

            Assert.Same(content, _item.Content);
        }

        [Fact]
        public void Should_be_able_to_set_selected()
        {
            _builder.Selected(true);

            Assert.True(_item.Selected);
        }

        [Fact]
        public void Should_be_able_to_set_disabled()
        {
            _builder.Disabled(true);

            Assert.True(_item.Disabled);
        }
    }
}