namespace Telerik.Web.Mvc.UI.UnitTest
{

    using System.IO;
    using System.Web.UI;
    using Moq;
    using UI;
    using Xunit;
    using System;

    public class PanelBarRenderingTest
    {
        private readonly PanelBar panelBar;
        private readonly Mock<IPanelBarRenderer> renderer;

        public PanelBarRenderingTest()
        {
            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(textWriter.Object);

            renderer = new Mock<IPanelBarRenderer>();

            panelBar = PanelBarTestHelper.CretePanelBar(writer.Object, renderer.Object);
            panelBar.Name = "PanelBar1";

            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem1", RouteName = "ProductList" });
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem2", RouteName = "ProductList" });
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem3", RouteName = "ProductList" });
        }

        [Fact]
        public void Render_should_not_output_anything_in_case_of_empty_data_source()
        {
            panelBar.Items.Clear();

            renderer.Setup(r => r.PanelBarStart());

            panelBar.Render();

            renderer.Verify(r => r.PanelBarStart(), Times.Never());
        }

        [Fact]
        public void Render_should_output_once_PanelBar_begin_tag_if_items_are_not_zero()
        {
            renderer.Setup(r => r.PanelBarStart());

            panelBar.Render();

            renderer.Verify(r => r.PanelBarStart(), Times.AtMostOnce());
        }

        [Fact]
        public void Render_should_output_once_PanelBar_end_tag_if_items_are_not_zero()
        {
            renderer.Setup(r => r.PanelBarEnd());

            panelBar.Render();

            renderer.Verify(r => r.PanelBarEnd(), Times.AtMostOnce());
        }

        [Fact]
        public void Render_should_output_as_many_header_items_as_count_of_Items()
        {
            renderer.Setup(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

			renderer.Verify(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count));
        }

        [Fact]
        public void Render_should_output_as_many_item_contents_as_count_of_Items()
        {
            renderer.Setup(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

			renderer.Verify(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count));
        }

        [Fact]
        public void Render_should_output_once_GroupList_begin_tag_if_items_are_not_zero_and_first_item_has_Items()
        {
            PanelBarItem item = new PanelBarItem { Text = "Item1", RouteName = "ProductList" };
            item.Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });

            panelBar.Items.Clear();
            panelBar.Items.Add(item);

            renderer.Setup(r => r.ListGroupStart(item));

            panelBar.Render();

            renderer.Verify(r => r.ListGroupStart(item), Times.AtMostOnce());
        }

        [Fact]
        public void Render_should_output_once_GroupList_end_tag_before_list_end_tag()
        {
            PanelBarItem item = new PanelBarItem { Text = "Item1", RouteName = "ProductList",Enabled=true };
            item.Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });

            panelBar.Items.Clear();
            panelBar.Items.Add(item);

            renderer.Setup(r => r.ListGroupEnd()).Callback(() => renderer.Setup(r => r.ListItemEnd()).Verifiable()).Verifiable();

            panelBar.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_should_output_as_many_SubItems_as_first_item_has()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            renderer.Setup(r => r.ListItemStart(It.IsAny<PanelBarItem>()));

            panelBar.Render();

			renderer.Verify(r => r.ListItemStart(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count + panelBar.Items[0].Items.Count));
        }

        [Fact]
        public void Render_should_output_content_instead_of_group_items()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].Content = () => { };

            renderer.Setup(r => r.ListGroupStart(It.IsAny<PanelBarItem>()));
            renderer.Setup(r => r.WriteContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            renderer.Verify(r => r.ListGroupStart(It.IsAny<PanelBarItem>()), Times.Never());
            renderer.Verify(r => r.WriteContent(It.IsAny<PanelBarItem>()), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_content_of_2_level_child_item()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList", Enabled = true });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList", Enabled = true });

            panelBar.Items[0].Items[0].Content = () => { };

            renderer.Setup(r => r.WriteContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            renderer.Verify(r => r.WriteContent(It.IsAny<PanelBarItem>()), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_not_output_childrens_if_contentUrl_is_set()
        {
            const string contentUrl = "testUrl";

            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].ContentUrl = contentUrl;

            renderer.Setup(r => r.ListGroupStart(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            renderer.Verify(r => r.ListGroupStart(It.IsAny<PanelBarItem>()), Times.Never());
        }

        [Fact]
        public void If_ContentURL_is_set_should_call_WriteContent()
        {
            const string contentUrl = "testUrl";

            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].ContentUrl = contentUrl;

            renderer.Setup(r => r.WriteContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            renderer.Verify(r => r.WriteContent(It.IsAny<PanelBarItem>()), Times.Exactly(1));
        }

        [Fact]
        public void If_header_item_visible_property_is_false_should_not_render_this_item()
        {
            panelBar.Items.Clear();
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem1", RouteName = "ProductList", Visible = false });

            renderer.Setup(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()));

            panelBar.Render();

			renderer.Verify(r => r.HeaderItemContent(It.IsAny<PanelBarItem>()), Times.Never());
        }

        [Fact]
        public void If_list_item_visible_property_is_false_should_not_render_this_item()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList", Visible = false });

            renderer.Setup(r => r.ListItemStart(It.IsAny<PanelBarItem>()));

            panelBar.Render();

			renderer.Verify(r => r.ListItemStart(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count));
        }

        [Fact]
        public void Render_should_call_objectWriter_start_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            PanelBarTestHelper.clientSideObjectWriter.Setup(ow => ow.Start()).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(ow => ow.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_append_Expand_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnExpand = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onExpand", panelBar.ClientEvents.OnExpand)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onExpand", panelBar.ClientEvents.OnExpand));
        }

        [Fact]
        public void ObjectWriter_should_append_Collapse_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnCollapse = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onCollapse", panelBar.ClientEvents.OnCollapse)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onCollapse", panelBar.ClientEvents.OnCollapse));
        }

        [Fact]
        public void ObjectWriter_should_append_SelectedItem_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnSelect = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onSelect", panelBar.ClientEvents.OnSelect)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onSelect", panelBar.ClientEvents.OnSelect));
        }

        [Fact]
        public void ObjectWriter_should_append_Loaded_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnLoad = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onLoad", panelBar.ClientEvents.OnLoad)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onLoad", panelBar.ClientEvents.OnLoad));
        }

        [Fact]
        public void ItemAction_should_set_items_Css_sprite_images() 
        {
            const string value = "test";
            panelBar.ItemAction = (item) =>
            {
                item.SpriteCssClasses = value;
            };

            panelBar.Render();

            Assert.Equal(value, panelBar.Items[0].SpriteCssClasses);
        }

        [Fact]
        public void Render_should_expand_first_item_and_select_first_child_item() 
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
            Assert.True(panelBar.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_expand_first_nestedItem_and_select_first_item_in_the_third_level()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "FirstBasic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic", Enabled = true });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            panelBar.Items[0].Items[0].Items.Add(new PanelBarItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Items[0].Expanded);
            Assert.True(panelBar.Items[0].Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_not_expand_first_item_if_HighlightPath_is_false()
        {
            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";

            panelBar.HighlightPath = false;

            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            panelBar.Render();

            Assert.False(panelBar.Items[0].Expanded);
            Assert.False(panelBar.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_expand_all_items_ExpandAll_is_set_to_true_and_MultiExpandMode() 
        {
            panelBar.Items.Clear();

            panelBar.Items.Add(new PanelBarItem { Text = "Item1", RouteName = "route", Enabled = true });
            panelBar.Items.Add(new PanelBarItem { Text = "Item2", RouteName = "route", Enabled = true });
            panelBar.Items.Add(new PanelBarItem { Text = "Item3", RouteName = "route", Enabled = true });

            panelBar.ExpandMode = PanelBarExpandMode.Multiple;

            panelBar.ExpandAll = true;

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
            Assert.True(panelBar.Items[1].Expanded);
            Assert.True(panelBar.Items[2].Expanded);
        }

        [Fact]
        public void Render_should_output_selected_item_if_selectedIndex_is_in_range() 
        {
            panelBar.SelectedIndex = 1;

            panelBar.Render();

            Assert.True(panelBar.Items[1].Selected);
        }

        [Fact]
        public void SelectedItem_should_be_expanded()
        {
            panelBar.SelectedIndex = 1;
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem1" });
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem2" });

            panelBar.Render();

            Assert.True(panelBar.Items[1].Expanded);
        }


        [Fact]
        public void Render_on_initial_load_should_expand_first_Item_if_HighlightPath_is_true_but_no_correct_item_is_found()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "NoSuchController";
            panelBar.ViewContext.RouteData.Values["action"] = "FirstBasic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic", Enabled = true });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            panelBar.Items[0].Items[0].Items.Add(new PanelBarItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            panelBar.SelectedIndex = 0;

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
        }
    }
}
