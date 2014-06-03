// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.UI;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class PanelBarRendererTest
    {
        private PanelBar panelBar;
        private PanelBarItem item;
        private Mock<HtmlTextWriter> writer;
        private PanelBarRenderer renderer;

        public PanelBarRendererTest()
        {
            writer = new Mock<HtmlTextWriter>(TextWriter.Null);

            panelBar = PanelBarTestHelper.CretePanelBar(writer.Object, null);
            panelBar.Name = "PanelBar1";

            item = new PanelBarItem();

            renderer = new PanelBarRenderer(panelBar, writer.Object, new Mock<IActionMethodCache>().Object);
        }

        [Fact]
        public void PanelBarStart_should_render_ul()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Ul)).Verifiable();

            renderer.PanelBarStart();

            writer.VerifyAll();
        }

        [Fact]
        public void PanelBarStart_should_render_id()
        {
            panelBar.HtmlAttributes.Clear();

            renderer.PanelBarStart();

            Assert.Equal(panelBar.Id, panelBar.HtmlAttributes["id"]);
        }

        public void PanelBarStart_should_render_class()
        {
            renderer.PanelBarStart();

            Assert.Equal("t-widget t-panelbar t-reset", panelBar.HtmlAttributes["class"]);
        }

        [Fact]
        public void PanelBarEnd_should_render_ul_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.PanelBarEnd();

            writer.Verify();
        }

        [Fact]
        public void ListGroupStart_should_render_ul_with_class()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-group", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Ul)).Verifiable();

            renderer.ListGroupStart(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ListGroupStart_should_hide_group_if_expanded_property_is_false()
        {
            const string url = "#";
            const string text = "panelBarItem1";

            item.Url = url;
            item.Text = text;
            item.Enabled = true;
            item.Expanded = false;

            item.Items.Add(new PanelBarItem { Text = "subItem", Enabled = true });

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;", true)).Verifiable();

            renderer.ListGroupStart(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ListGroupStart_should_show_group_if_expanded_property_is_true()
        {
            const string url = "#";
            const string text = "panelBarItem1";

            item.Url = url;
            item.Text = text;
            item.Enabled = true;
            item.Expanded = true;

            item.Items.Add(new PanelBarItem { Text = "subItem", Enabled = true });

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Style, "display:block;", true)).Verifiable();

            renderer.ListGroupStart(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ListGroupEnd_should_render_ul_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ListGroupEnd();

            writer.Verify();
        }

        [Fact]
        public void ListItemStart_should_render_li()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Li)).Verifiable();

            renderer.ListItemStart(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ListItemStart_should_render_header_class_if_is_enabled_and_not_selected()
        {
            item.Enabled = true;
            item.Selected = false;

            renderer.ListItemStart(item);

            Assert.Equal("t-item t-state-default", item.HtmlAttributes["class"]);
		}

		[Fact]
		public void ItemContent_should_render_selected_class_if_item_is_selected()
		{
			item.Selected = true;

			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-link t-state-selected", true)).Verifiable();

			renderer.ItemContent(item);

			writer.VerifyAll();
		}

        [Fact]
        public void ListItemStart_should_render_item_in_active_state_if_it_is_enabled_and_expanded()
        {
            item.Enabled = true;
			item.Expanded = true;

            renderer.ListItemStart(item);

            Assert.Equal("t-item t-state-active", item.HtmlAttributes["class"]);
        }

        [Fact]
        public void ListItemStart_should_not_render_item_in_active_state_if_it_is_not_enabled_and_not_expanded()
        {
            item.Enabled = false;
			item.Expanded = false;

            renderer.ListItemStart(item);

            Assert.Equal("t-item t-state-disabled", item.HtmlAttributes["class"]);
        }

        [Fact]
        public void ListItemEnd_should_render_ul_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ListItemEnd();

            writer.Verify();
        }

        [Fact]
        public void ItemContent_should_render_a_with_class_and_href_and_text_content_and_end_tags_when_item_is_enabled()
        {
            const string url = "#";
            const string text = "panelBarItem1";

            item.Url = url;
            item.Text = text;
            item.Enabled = true;

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-link", true)).Verifiable();
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url, true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.A)).Verifiable();
            writer.Setup(w => w.WriteEncodedText(text)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ItemContent(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_span_with_expanded_css_class_if_expanded_property_is_true()
        {
            const string url = "#";
            const string text = "panelBarItem1";

            item.Url = url;
            item.Text = text;
            item.Enabled = true;
            item.Expanded = true;

            item.Items.Add(new PanelBarItem { Text = "subItem", Enabled = true });

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-up", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ItemContent(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_span_with_collapsed_css_class_if_expanded_property_is_false()
        {
            const string url = "#";
            const string text = "panelBarItem1";

            item.Url = url;
            item.Text = text;
            item.Enabled = true;
            item.Expanded = false;

            item.Items.Add(new PanelBarItem { Text = "subItem", Enabled = true });

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ItemContent(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_not_render_href_when_item_is_disabled()
        {
            const string url = "#";

            item.Url = url;
            item.Enabled = false;

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url, true));

            renderer.ItemContent(item);

            writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url, true), Times.Never());
        }

        [Fact]
        public void ItemContent_should_not_render_span_if_items_are_0_and_content_or_contentUrl_are_not_set()
        {
            item.Items.Add(new PanelBarItem { Text = "subItem", Enabled = true });

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
        }

        [Fact]
        public void ItemContent_should_render_span_if_content_is_set_and_items_are_0_or_contentUrl_is_empty()
        {
            item.Items.Clear();

            item.Content = () => { };

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
        }

        [Fact]
        public void ItemContent_should_render_span_if_contentUrl_is_set_and_items_are_0_or_content_is_null()
        {
            item.Items.Clear();

            item.ContentUrl = "testUrl";

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
        }

        [Fact]
        public void ItemContent_should_not_render_span_if_contentUrl__items_and_content_is_null()
        {
            item.Items.Clear();

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Never());
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
            item.ContentUrl = "url";
            item.Content = () => { };

            renderer.WriteContent(item);

            Assert.Contains(UIPrimitives.Content, item.ContentHtmlAttributes["class"].ToString());
            Assert.NotNull(item.ContentHtmlAttributes["id"]);
        }

        [Fact]
        public void WriteContent_should_render_start_div_tag()
        {
            item.ContentUrl = "url";
            item.Content = () => { };

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.WriteContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Exactly(1));
        }

        [Fact]
        public void WriteContent_should_render_end_div_tag()
        {
            item.ContentUrl = "url";
            item.Content = () => { };

            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.WriteContent(item);

            writer.Verify(w => w.RenderEndTag(), Times.Exactly(1));
        }

        [Fact]
        public void WriteContent_should_hide_content_if_expanded_property_is_false()
        {
            item.Content = () => { };
            item.Expanded = false;
            item.Enabled = true;

            renderer.WriteContent(item);

            Assert.Contains("display:none", item.ContentHtmlAttributes["style"].ToString());
        }


        [Fact]
        public void WriteContent_should_hide_content_if_enabled_property_is_false()
        {
            item.Content = () => { };
            item.Expanded = true;
            item.Enabled = false;

            renderer.WriteContent(item);

            Assert.Contains("display:none", item.ContentHtmlAttributes["style"].ToString());
        }

        [Fact]
        public void WriteContent_should_show_content_if_expanded_and_enabled_properties_are_true()
        {
            item.Content = () => { };
            item.Expanded = true;
            item.Enabled = true;

            renderer.WriteContent(item);

            Assert.Contains("display:block", item.ContentHtmlAttributes["style"].ToString());
        }

        [Fact]
        public void WriteContent_should_call_WriteImage()
        {
            const string url = "testUrl";

            item.ImageUrl = url;

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Img)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Img), Times.Exactly(1));
        }

        [Fact]
        public void WriteContent_should_call_WriteSprite()
        {
            const string sprite = "sprite";

            item.SpriteCssClasses = sprite;
            item.Items.Clear();

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
        }

        [Fact]
        public void CommonItemRender_should_not_render_up_arrow_is_disabled() 
        {
            item.Enabled = false;
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-up", true)).Verifiable();

            renderer.HeaderItemContent(item);

            writer.Verify(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-up", true), Times.Never());
        }
    }
}
