// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using System.IO;
    using System.Web.UI;

    using Infrastructure;
    using UI;

    using Moq;
    using Xunit;

    public class MenuRendererTests
    {
        private Menu menu;
        private MenuItem item;
        private Mock<HtmlTextWriter> writer;
        private MenuRenderer renderer;

        public MenuRendererTests()
        {
            writer = new Mock<HtmlTextWriter>(TextWriter.Null);

            menu = MenuTestHelper.CreteMenu(writer.Object, null);
            menu.Name = "PanelBar1";

            item = new MenuItem();

            renderer = new MenuRenderer(menu, writer.Object, new Mock<IActionMethodCache>().Object);
        }

        [Fact]
        public void Render_should_output_menu_div_with_id_and_css_classes()
        {
            writer.Setup(w => w.AddAttribute("id", menu.Name, true)).Verifiable();
            writer.Setup(w => w.AddAttribute("class", "t-widget t-reset t-header t-menu", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Ul)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.MenuStart();
            renderer.MenuEnd();

            writer.VerifyAll();
        }

        [Fact]
        public void Render_should_output_custom_menu_css_class_when_set()
        {
            menu.HtmlAttributes.Add("class", "custom");
            writer.Setup(w => w.AddAttribute("class", "t-widget t-reset t-header t-menu custom", true)).Verifiable();

            renderer.MenuStart();

            writer.VerifyAll();
        }

        [Fact]
        public void Render_should_output_menu_item_when_items_are_defined()
        {
            item.Enabled = true;
            writer.Setup(w => w.AddAttribute("class", "t-item t-state-default", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Li)).Verifiable();

            writer.Setup(w => w.RenderEndTag()).Verifiable();


            renderer.ItemStart(item);
            renderer.ItemEnd();

            writer.VerifyAll();
        }

        [Fact]
        public void ItemStart_should_render_disabled_class_if_item_is_disabled() 
        {
            item.Enabled = false;
            renderer.ItemStart(item);

            Assert.Equal("t-item t-state-disabled", item.HtmlAttributes["class"]);
        }

        [Fact]
        public void ItemStart_should_render_selected_class_if_item_is_selected()
        {
            item.Enabled = true;
            item.Selected = true;
            renderer.ItemStart(item);

            Assert.Equal("t-item t-state-selected", item.HtmlAttributes["class"]);
        }

        [Fact]
        public void ItemStart_should_render_default_class_if_item_is_not_selected_and_is_enabled()
        {
            item.Enabled = true;
            renderer.ItemStart(item);

            Assert.Equal("t-item t-state-default", item.HtmlAttributes["class"]);
        }

        [Fact]
        public void Render_should_output_links_when_items_are_defined()
        {
            const string text = "test-text";
            const string url = "#";

            item.Text = text;
            item.Url = url;

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Link)).Verifiable();
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.A)).Verifiable();
            writer.Setup(w => w.Write(text)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.Link(item);

            writer.VerifyAll();
        }

        [Fact]
        public void Render_should_output_child_groups_if_any()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Group)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Ul)).Verifiable();

            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.GroupStart();
            renderer.GroupEnd();

            writer.VerifyAll();
        }

        [Fact]
        public void Vertical_menus_should_have_their_css_class()
        {
            menu.Orientation = MenuOrientation.Vertical;

            writer.Setup(w => w.AddAttribute("class", "t-widget t-reset t-header t-menu t-menu-vertical", true)).Verifiable();

            renderer.MenuStart();

            writer.VerifyAll();
        }

        [Fact]
        public void Link_should_call_WriteImage()
        {
            const string url = "testUrl";

            item.ImageUrl = url;

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Img)).Verifiable();

            renderer.Link(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Img), Times.Exactly(1));
        }

        [Fact]
        public void Link_should_call_WriteSprite()
        {
            const string sprite = "sprite";

            item.SpriteCssClasses = sprite;
            item.Items.Clear();

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.Link(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
		}

		[Fact]
		public void Link_should_output_an_expand_arrow_for_root_items_with_children()
		{
			item.Items.Add(new MenuItem() { Text = "My lovely child item" });

			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down")).Verifiable();
			writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

			renderer.Link(item);

			writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down"), Times.Exactly(1));
		}

        [Fact]
        public void Link_should_output_an_expand_arrow_for_root_items_with_content()
        {
            item.Items.Clear();
            item.Content = () => { };

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down")).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.Link(item);

            writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down"), Times.Exactly(1));
        }

		[Fact]
		public void Link_should_output_an_expand_arrow_for_child_items_with_children()
		{
			item.Items.Add(new MenuItem() { Text = "My lovely child item 1" });
			item.Items.Add(new MenuItem() { Text = "My lovely child item 2" });
			item.Items[0].Items.Add(new MenuItem() { Text = "My lovely grand child item" });

			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-next")).Verifiable();
			writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

			foreach (var child in item.Items) renderer.Link(child);

			writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-next"), Times.Exactly(1));
		}

		[Fact]
		public void Link_should_output_an_horizontal_expand_arrow_for_root_items_with_children_in_vertical_menus()
		{
			item.Items.Add(new MenuItem() { Text = "My lovely child item" });

			menu.Orientation = MenuOrientation.Vertical;

			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-next")).Verifiable();
			writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

			renderer.Link(item);
			renderer.Link(new MenuItem() { });

			writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-next"), Times.Exactly(1));
		}
        [Fact]
        public void WriteContent_should_not_render_anything_if_no_ContentUrl_or_Content()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.WriteContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Never());
        }

        [Fact]
        public void WriteContent_should_render_content_css_class_and_id_attr()
        {
            item.Content = () => { };

            renderer.WriteContent(item);

            Assert.Contains(UIPrimitives.Content, item.ContentHtmlAttributes["class"].ToString());
            Assert.NotNull(item.ContentHtmlAttributes["id"]);
        }

        [Fact]
        public void WriteContent_should_render_start_div_tag()
        {
            item.Content = () => { };

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.WriteContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Exactly(1));
        }

        [Fact]
        public void WriteContent_should_render_end_div_tag()
        {
            item.Content = () => { };

            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.WriteContent(item);

            writer.Verify(w => w.RenderEndTag(), Times.Exactly(1));
        }
    }
}
