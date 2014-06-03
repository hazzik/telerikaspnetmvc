namespace Telerik.Web.Mvc.UnitTest.Menu
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

	public class MenuBuilderTests
	{
		private readonly Menu menu;
		private readonly MenuBuilder builder;

		public MenuBuilderTests()
		{
			Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(TextWriter.Null);
			menu = MenuTestHelper.CreteMenu(writer.Object, null);
			builder = new MenuBuilder(menu);
		}

        [Fact]
        public void OpenOnClick_sets_the_open_on_click_property()
        {
            builder.OpenOnClick(true);

            Assert.True(menu.OpenOnClick);
        }

		[Fact]
		public void Setting_items_sets_menu_items()
		{
			builder.Items(root => root.Add().Text("Menu Item"));

			Assert.NotEmpty(menu.Items);
		}

		[Fact]
		public void Setting_orientation_sets_menu_orientation()
		{
			builder.Orientation(MenuOrientation.Vertical);

			Assert.Equal(MenuOrientation.Vertical, menu.Orientation);
		}

		[Fact]
		public void Setting_theme_sets_menu_theme()
		{
			builder.Theme("Lovely");

			Assert.Equal("Lovely", menu.Theme);
		}

		[Fact]
		public void Effects_creates_fx_factory()
		{
			var fxFacCreated = false;

			builder.Effects(fx =>
			{
				fxFacCreated = fx != null;
			});

			Assert.True(fxFacCreated);
		}

        [Fact]
        public void BintTo_for_SiteMap_should_get_SiteMap_and_create_items()
        {
            const string viewDataKey = "sample";

            Action<MenuItem, SiteMapNode> action = (item, node) => { if (!string.IsNullOrEmpty(node.RouteName)) { item.RouteName = node.RouteName; } };
            builder.BindTo(viewDataKey, action);

            Assert.Equal(2, menu.Items.Count);
        }

        [Fact]
        public void BintTo_for_SiteMap_should_return_builder()
        {
            const string viewDataKey = "sample";

            Action<MenuItem, SiteMapNode> action = (item, node) => { if (!string.IsNullOrEmpty(node.RouteName)) { item.RouteName = node.RouteName; } };
            var returnedBuilder = builder.BindTo(viewDataKey, action);

            Assert.IsType(typeof(MenuBuilder), returnedBuilder);
        }

        [Fact]
        public void BindTo_with_viewDataKey_only_should_get_SiteMap_and_create_items()
        {
            const string viewDataKey = "sample";

            builder.BindTo(viewDataKey);

            Assert.Equal(2, menu.Items.Count);
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

            Assert.IsType(typeof(MenuBuilder), returnedBuilder);
        }

        [Fact]
        public void ItemAction_should_set_ItemAction_property_of_panelBar()
        {
            Action<MenuItem> action = (item) => { };
            builder.ItemAction(action);

            Assert.Equal(action, menu.ItemAction);
        }

        [Fact]
        public void ItemAction_should_return_builder()
        {
            Action<MenuItem> action = (item) => { };
            var returnedBuilder = builder.ItemAction(action);

            Assert.IsType(typeof(MenuBuilder), returnedBuilder);
        }

        [Fact]
        public void SelectedIndex_should_set_SelectedIndex_property_of_PanelBar()
        {
            const int value = 0;

            builder.SelectedIndex(value);

            Assert.Equal(value, menu.SelectedIndex);
        }

        [Fact]
        public void SelectedIndex_should_return_builder()
        {
            const int value = 0;
            var returnedBuilder = builder.SelectedIndex(value);

            Assert.IsType(typeof(MenuBuilder), returnedBuilder);
        }

        [Fact]
        public void HighlightPath_should_set_HighlightPath_property_of_PanelBar()
        {
            const bool value = true;

            builder.HighlightPath(value);

            Assert.Equal(value, menu.HighlightPath);
        }

        [Fact]
        public void HighlightPath_should_return_builder()
        {
            const bool value = true;
            var returnedBuilder = builder.HighlightPath(value);

            Assert.IsType(typeof(MenuBuilder), returnedBuilder);
        }
	}
}