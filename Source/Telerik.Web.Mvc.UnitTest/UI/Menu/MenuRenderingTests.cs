namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using System.IO;
    using System.Web.UI;
    using Moq;
    using Telerik.Web.Mvc.UI;
    using Xunit;

    public class MenuRenderingTests
    {
        private readonly Menu menu;
        private readonly Mock<IMenuRenderer> renderer;

        public MenuRenderingTests()
        {
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(TextWriter.Null);

            renderer = new Mock<IMenuRenderer>();

            menu = MenuTestHelper.CreteMenu(writer.Object, renderer.Object);
            menu.Name = "Menu";

            menu.Items.Add(new MenuItem { Text = "MenuItem1" });
            menu.Items.Add(new MenuItem { Text = "MenuItem2" });
            menu.Items.Add(new MenuItem { Text = "MenuItem3" });
        }

        [Fact]
        public void Render_should_not_output_anything_when_there_are_no_items()
        {
            menu.Items.Clear();

            renderer.Setup(r => r.MenuStart());

            menu.Render();

            renderer.Verify(r => r.MenuStart(), Times.Never());
        }

        [Fact]
        public void Render_should_output_once_Menu_begin_tag_if_items_are_not_zero()
        {
            renderer.Setup(r => r.MenuStart());

            menu.Render();

            renderer.Verify(r => r.MenuStart(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_once_Menu_end_tag_if_items_are_not_zero()
        {
            renderer.Setup(r => r.MenuEnd());

            menu.Render();

            renderer.Verify(r => r.MenuEnd(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_the_same_amount_of_items_as_there_are()
        {
            renderer.Setup(r => r.ItemStart(It.IsAny<MenuItem>()));
            renderer.Setup(r => r.ItemEnd());

            menu.Render();

            renderer.Verify(r => r.ItemStart(It.IsAny<MenuItem>()), Times.Exactly(menu.Items.Count));
            renderer.Verify(r => r.ItemEnd(), Times.Exactly(menu.Items.Count));
        }

        [Fact]
        public void Render_should_output_item_links()
        {
            renderer.Setup(r => r.Link(It.IsAny<MenuItem>()));

            menu.Render();

            renderer.Verify(r => r.Link(It.IsAny<MenuItem>()), Times.Exactly(3));
        }

        [Fact]
        public void Render_should_output_child_groups_if_any()
        {
            menu.Items[0].Items.Add(new MenuItem() { Text = "A Lovely Child Item" });
            menu.Items[1].Items.Add(new MenuItem() { Text = "Another Lovely Child Item" });

            renderer.Setup(r => r.GroupStart());
            renderer.Setup(r => r.GroupEnd());

            menu.Render();

            renderer.Verify(r => r.GroupStart(), Times.Exactly(2));
            renderer.Verify(r => r.GroupEnd(), Times.Exactly(2));
        }


        [Fact]
        public void Render_should_output_content_if_there_is_any() 
        {
            menu.Items[0].Content = () => { };

            renderer.Setup(r => r.WriteContent(It.IsAny<MenuItem>()));

            menu.Render();

            renderer.Verify(r => r.WriteContent(It.IsAny<MenuItem>()), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_group_Ul_and_one_LI_if_content_is_set()
        {
            menu.Items[0].Content = () => { };

            renderer.Setup(r => r.GroupStart());
            renderer.Setup(r => r.ItemStart(It.IsAny<MenuItem>()));

            menu.Render();

            renderer.Verify(r => r.GroupStart(), Times.Exactly(1));
            renderer.Verify(r => r.ItemStart(It.IsAny<MenuItem>()), Times.Exactly(menu.Items.Count + 1));
        }

        [Fact]
        public void ItemAction_should_set_items_Css_sprite_images()
        {
            const string value = "test";
            menu.ItemAction = (item) =>
            {
                item.SpriteCssClasses = value;
            };

            menu.Render();

            Assert.Equal(value, menu.Items[0].SpriteCssClasses);
        }

        [Fact]
        public void Render_should_select_first_child_item()
        {
            menu.HighlightPath = true;

            menu.ViewContext.RouteData.Values["controller"] = "Grid";
            menu.ViewContext.RouteData.Values["action"] = "Basic";
            menu.Items[0].Text = "Grid";
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            menu.Render();

            Assert.True(menu.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_select_first_item_in_the_third_level()
        {
            menu.HighlightPath = true;

            menu.ViewContext.RouteData.Values["controller"] = "Grid";
            menu.ViewContext.RouteData.Values["action"] = "FirstBasic";
            menu.Items[0].Text = "Grid";
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic", Enabled = true });
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            menu.Items[0].Items[0].Items.Add(new MenuItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            menu.Render();

            Assert.True(menu.Items[0].Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_not_expand_first_item_if_HighlightPath_is_false()
        {
            menu.ViewContext.RouteData.Values["controller"] = "Grid";
            menu.ViewContext.RouteData.Values["action"] = "Basic";

            menu.HighlightPath = false;

            menu.Items[0].Text = "Grid";
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            menu.Render();

            Assert.False(menu.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_output_selected_item_if_selectedIndex_is_in_range()
        {
            menu.SelectedIndex = 1;

            menu.Render();

            Assert.True(menu.Items[1].Selected);
        }
    }
}
