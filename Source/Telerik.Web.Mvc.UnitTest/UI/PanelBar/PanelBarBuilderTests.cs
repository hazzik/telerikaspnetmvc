namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.UI;

    using Moq;
    using Xunit;
    using System;

    public class PanelBarBuilderTests
    {
        private readonly PanelBar panelBar;
        private readonly PanelBarBuilder builder;

        public PanelBarBuilderTests()
        {
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(TextWriter.Null);
            panelBar = PanelBarTestHelper.CretePanelBar(writer.Object, null);
            builder = new PanelBarBuilder(panelBar);
        }

        [Fact]
        public void Items_call_PanelBarItemFactory_to_add_item()
        {
            panelBar.Items.Clear();
            builder.Items(c => c.Add());

            Assert.Equal(1, panelBar.Items.Count);
        }

        [Fact]
        public void Items_should_return_builder()
        {
            var returnedBuilder = builder.Items(c => c.Add());

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void Theme_should_set_Theme_property_of_panelBar()
        {
            const string theme = "theme";
            builder.Theme(theme);

            Assert.Equal(theme, panelBar.Theme);
        }

        [Fact]
        public void Theme_should_return_builder()
        {
            const string theme = "theme";
            var returnedBuilder = builder.Theme(theme);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void BintTo_for_SiteMap_should_get_SiteMap_and_create_items() 
        {
            const string viewDataKey = "sample";

            Action<PanelBarItem, SiteMapNode> action = (item, node) => { if (!string.IsNullOrEmpty(node.RouteName)) { item.RouteName = node.RouteName; } };
            builder.BindTo(viewDataKey, action);
            
            Assert.Equal(2, panelBar.Items.Count);
        }

        [Fact]
        public void BintTo_for_SiteMap_should_return_builder()
        {
            const string viewDataKey = "sample";

            Action<PanelBarItem, SiteMapNode> action = (item, node) => { if (!string.IsNullOrEmpty(node.RouteName)) { item.RouteName = node.RouteName; } };
            var returnedBuilder = builder.BindTo(viewDataKey, action);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void BindTo_with_viewDataKey_only_should_get_SiteMap_and_create_items() 
        {
            const string viewDataKey = "sample";

            builder.BindTo(viewDataKey);

            Assert.Equal(2, panelBar.Items.Count);
        }

        [Fact]
        public void BindTo_with_viewDataKey_only_should_throw_exception_if_siteMap_is_loaded() 
        {
            const string viewDataKey = "unexistingSiteMap";

            Assert.Throws(typeof(NotSupportedException), () => { builder.BindTo(viewDataKey); });
        }

        [Fact]
        public void BintTo_for_viewDataKey_only_should_return_builder()
        {
            const string viewDataKey = "sample";

            var returnedBuilder = builder.BindTo(viewDataKey);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void ItemAction_should_set_ItemAction_property_of_panelBar()
        {
            Action<PanelBarItem> action = (item) => { };
            builder.ItemAction(action);

            Assert.Equal(action, panelBar.ItemAction);
        }

        [Fact]
        public void ItemAction_should_return_builder()
        {
            Action<PanelBarItem> action = (item) => { };
            var returnedBuilder = builder.ItemAction(action);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void HighlightPath_should_set_HighlightPath_property_of_PanelBar()
        {
            const bool value = true;

            builder.HighlightPath(value);

            Assert.Equal(value, panelBar.HighlightPath);
        }

        [Fact]
        public void HighlightPath_should_return_builder()
        {
            const bool value = true;
            var returnedBuilder = builder.HighlightPath(value);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void ExpandMode_should_set_ExpandMode_property_of_PanelBar()
        {
            builder.ExpandMode(PanelBarExpandMode.Single);

            Assert.Equal(PanelBarExpandMode.Single, panelBar.ExpandMode);
        }

        [Fact]
        public void ExpandMode_should_return_builder()
        {
            var returnedBuilder = builder.ExpandMode(PanelBarExpandMode.Multiple);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void ExpandAll_should_set_HighlightPath_property_of_PanelBar()
        {
            const bool value = true;

            builder.ExpandAll(value);

            Assert.Equal(value, panelBar.ExpandAll);
        }

        [Fact]
        public void ExpandAll_should_return_builder()
        {
            const bool value = true;
            var returnedBuilder = builder.ExpandAll(value);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }

        [Fact]
        public void SelectedIndex_should_set_SelectedIndex_property_of_PanelBar()
        {
            const int value = 0;

            builder.SelectedIndex(value);

            Assert.Equal(value, panelBar.SelectedIndex);
        }

        [Fact]
        public void SelectedIndex_should_return_builder()
        {
            const int value = 0;
            var returnedBuilder = builder.SelectedIndex(value);

            Assert.IsType(typeof(PanelBarBuilder), returnedBuilder);
        }
    }
}