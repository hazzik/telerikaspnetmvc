namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using Xunit;

    using Telerik.Web.Mvc.UI;

    public class TabStripRenderingTests
    {

        private readonly TabStrip tabStrip;
        private readonly Mock<ITabStripRenderer> renderer;

        public TabStripRenderingTests()
        {
            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(textWriter.Object);

            renderer = new Mock<ITabStripRenderer>();

            tabStrip = TabStripTestHelper.CreteTabStrip(writer.Object, renderer.Object);
            tabStrip.Name = "TabStrip1";

            tabStrip.Items.Add(new TabStripItem { Text = "TabStripItem1", RouteName = "ProductList" });
            tabStrip.Items.Add(new TabStripItem { Text = "TabStripItem2", RouteName = "ProductList" });
            tabStrip.Items.Add(new TabStripItem { Text = "TabStripItem3", RouteName = "ProductList" });
        }

        [Fact]
        public void Render_should_not_output_anything_in_case_of_empty_data_source()
        {
            tabStrip.Items.Clear();

            renderer.Setup(r => r.TabStripStart());

            tabStrip.Render();

            renderer.Verify(r => r.TabStripStart(), Times.Never());
        }

        [Fact]
        public void Render_should_output_start_div_if_items_are_not_zero()
        {
            renderer.Setup(r => r.TabStripStart());

            tabStrip.Render();

            renderer.Verify(r => r.TabStripStart(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_end_div_if_items_are_not_zero()
        {
            renderer.Setup(r => r.TabStripEnd());

            tabStrip.Render();

            renderer.Verify(r => r.TabStripEnd(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_start_ul_tag()
        {
            renderer.Setup(r => r.ItemsStart());

            tabStrip.Render();

            renderer.Verify(r => r.ItemsStart(), Times.Exactly(1));
        }


        [Fact]
        public void Render_should_output_end_ul_tag()
        {
            renderer.Setup(r => r.ItemsEnd());

            tabStrip.Render();

            renderer.Verify(r => r.ItemsEnd(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_call_ItemContent_as_many_times_as_items_count()
        {
            renderer.Setup(r => r.ItemContent(It.IsAny<TabStripItem>()));

            tabStrip.Render();

            renderer.Verify(r => r.ItemContent(It.IsAny<TabStripItem>()), Times.Exactly(tabStrip.Items.Count));
        }

        [Fact]
        public void Render_should_call_ItemContent_before_TabContent()
        {
            renderer.Setup(r => r.ItemContent(It.IsAny<TabStripItem>())).Callback(() => renderer.Setup(r => r.TabContent(It.IsAny<TabStripItem>())).Verifiable()).Verifiable();

            tabStrip.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_should_call_TabContent_as_many_times_as_items_count()
        {
            renderer.Setup(r => r.TabContent(It.IsAny<TabStripItem>()));

            tabStrip.Render();

            renderer.Verify(r => r.TabContent(It.IsAny<TabStripItem>()), Times.Exactly(tabStrip.Items.Count));
        }

        [Fact]
        public void Render_should_call_TabContent_before_TabStripEnd()
        {
            renderer.Setup(r => r.TabContent(It.IsAny<TabStripItem>())).Callback(() => renderer.Setup(r => r.TabStripEnd()).Verifiable()).Verifiable();

            tabStrip.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_should_not_ItemContent_if_it_is_not_visible()
        {
            tabStrip.Items.Clear();
            tabStrip.Items.Add(new TabStripItem { Text = "TabStripItem1", RouteName = "ProductList", Visible = false });

            renderer.Setup(r => r.ItemContent(It.IsAny<TabStripItem>())).Verifiable();

            tabStrip.Render();

            renderer.Verify(r => r.ItemContent(It.IsAny<TabStripItem>()), Times.Never());            
        }

        [Fact]
        public void Render_should_not_ItemContent_if_it_is_not_accessible()
        {
            tabStrip.Items.Clear();
            tabStrip.Items.Add(new TabStripItem { Text = "TabStripItem1", RouteName = "ProductList", Visible = true });

            TabStripTestHelper.authorization.Setup(a => a.IsAccessibleToUser(TabStripTestHelper.viewContext.RequestContext, It.IsAny<INavigatable>())).Returns(false);

            renderer.Setup(r => r.ItemContent(It.IsAny<TabStripItem>())).Verifiable();

            tabStrip.Render();

            renderer.Verify(r => r.ItemContent(It.IsAny<TabStripItem>()), Times.Never());
        }

        public void When_urlGenerator_returns_null_url_should_be_ds() 
        {
            TabStripTestHelper.urlGenerator.Setup(g => g.Generate(TabStripTestHelper.viewContext.RequestContext, It.IsAny<INavigatable>())).Returns(()=>null);

            tabStrip.Render();

            Assert.Equal("#", tabStrip.Items[0].Url);
        }

        [Fact]
        public void ItemAction_should_set_items_Css_sprite_images()
        {
            const string value = "test";
            tabStrip.ItemAction = (item) =>
            {
                item.SpriteCssClasses = value;
            };

            tabStrip.Render();

            Assert.Equal(value, tabStrip.Items[0].SpriteCssClasses);
        }

        [Fact]
        public void Render_should_select_first_item()
        {
            tabStrip.HighlightPath = true;

            tabStrip.ViewContext.RouteData.Values["controller"] = "Grid";
            tabStrip.ViewContext.RouteData.Values["action"] = "Basic";

            tabStrip.Items[0].ActionName = "Basic";
            tabStrip.Items[0].ControllerName = "Grid";
            tabStrip.Render();

            Assert.True(tabStrip.Items[0].Selected);
        }

        [Fact]
        public void Render_should_output_selected_item_if_selectedIndex_is_in_range()
        {
            tabStrip.SelectedIndex = 1;

            tabStrip.Render();

            Assert.True(tabStrip.Items[1].Selected);
        }
    }
}
