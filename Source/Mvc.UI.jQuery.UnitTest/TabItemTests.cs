// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using Xunit;

    public class TabItemTests
    {
        private readonly Mvc.UI.jQuery.TabItem _tabItem;

        public TabItemTests()
        {
            _tabItem = new Mvc.UI.jQuery.TabItem();
        }

        [Fact]
        public void HtmlAttributes_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_tabItem.HtmlAttributes);
        }

        [Fact]
        public void ContentHtmlAttributes_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_tabItem.ContentHtmlAttributes);
        }

        [Fact]
        public void Should_be_able_to_set_text()
        {
            _tabItem.Text = "My Header";

            Assert.Equal("My Header", _tabItem.Text);
        }

        [Fact]
        public void Should_be_able_to_set_load_content_from_url()
        {
            _tabItem.LoadContentFromUrl = "/Home/Content/1";

            Assert.Equal("/Home/Content/1", _tabItem.LoadContentFromUrl);
        }

        [Fact]
        public void LoadContentFromUrl_should_clear_content_html_Attributes()
        {
            _tabItem.ContentHtmlAttributes.Add("class", "myClass");

            _tabItem.LoadContentFromUrl = "/Home/Content/1";

            Assert.Empty(_tabItem.ContentHtmlAttributes);
        }

        [Fact]
        public void LoadContentFromUrl_should_reset_content()
        {
            _tabItem.Content = () => { };

            _tabItem.LoadContentFromUrl = "/Home/Content/1";

            Assert.Null(_tabItem.Content);
        }

        [Fact]
        public void Content_should_reset_load_content_from_url()
        {
            _tabItem.Content = () => { };

            Assert.Null(_tabItem.LoadContentFromUrl);
        }

        [Fact]
        public void Should_be_able_to_set_selected()
        {
            _tabItem.Selected = true;

            Assert.True(_tabItem.Selected);
        }

        [Fact]
        public void Selected_should_reset_disabled_to_false()
        {
            _tabItem.Disabled = true;

            _tabItem.Selected = true;

            Assert.False(_tabItem.Disabled);
        }

        [Fact]
        public void Should_be_able_to_set_disabled()
        {
            _tabItem.Disabled = true;

            Assert.True(_tabItem.Disabled);
        }

        [Fact]
        public void Disabled_should_reset_selected_to_false()
        {
            _tabItem.Selected = true;

            _tabItem.Disabled = true;

            Assert.False(_tabItem.Selected);
        }
    }
}