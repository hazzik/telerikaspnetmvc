using Telerik.Web.Mvc.Infrastructure;

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.UI;
    using Moq;
    using Xunit;

    public class TabStripRendererTests
    {
        private TabStrip tabStrip;
        private TabStripItem item;
        private Mock<HtmlTextWriter> writer;
        private TabStripRenderer renderer;

        public TabStripRendererTests()
        {
            writer = new Mock<HtmlTextWriter>(TextWriter.Null);

            tabStrip = TabStripTestHelper.CreteTabStrip(writer.Object, null);
            tabStrip.Name = "tabStrip1";

            item = new TabStripItem();
            item.Visible = true;

            renderer = new TabStripRenderer(tabStrip, writer.Object, new Mock<IActionMethodCache>().Object);
        }

        [Fact]
        public void TabStripStart_should_render_div_wrapper()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.TabStripStart();

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Exactly(1));
        }

        [Fact]
        public void TabStripStart_should_render_id()
        {
            tabStrip.HtmlAttributes.Clear();

            renderer.TabStripStart();

            Assert.Equal(tabStrip.Id, tabStrip.HtmlAttributes["id"]);
        }

        public void TabStripStart_should_render_class()
        {
            renderer.TabStripStart();

            Assert.Equal("t-widget t-tabstrip t-header", tabStrip.HtmlAttributes["class"]);
        }

        [Fact]
        public void TabStripEnd_should_render_div_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.TabStripEnd();

            writer.Verify(w => w.RenderEndTag());
        }

        [Fact]
        public void ItemStart_should_render_ul_start_tag_and_class()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.ResetStyle)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Ul)).Verifiable();

            renderer.ItemsStart();

            writer.VerifyAll();
        }

        [Fact]
        public void ItemEnd_should_render_ul_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ItemsEnd();

            writer.Verify(w => w.RenderEndTag());
        }

        [Fact]
        public void ItemContent_should_render_Item_class()
        {
            item.HtmlAttributes.Clear();

            renderer.ItemContent(item);

            Assert.Contains(UIPrimitives.Item.ToString(), item.HtmlAttributes["class"].ToString());
        }

        [Fact]
        public void ItemContent_should_render_ActiveState_class_if_the_item_is_active()
        {
            item.Selected = true;
            item.HtmlAttributes.Clear();

            renderer.ItemContent(item);

            Assert.Contains(UIPrimitives.ActiveState.ToString(), item.HtmlAttributes["class"].ToString());
        }

        [Fact]
        public void ItemContent_should_render_DisabledState_class_if_the_item_is_disabled()
        {
            item.Enabled = false;
            item.HtmlAttributes.Clear();

            renderer.ItemContent(item);

            Assert.Contains(UIPrimitives.DisabledState.ToString(), item.HtmlAttributes["class"].ToString());
        }

        [Fact]
        public void ItemContent_should_render_DefaultState_class_if_the_item_enabled_and_not_active()
        {
            item.HtmlAttributes.Clear();

            renderer.ItemContent(item);

            Assert.Contains(UIPrimitives.DefaultState.ToString(), item.HtmlAttributes["class"].ToString());
        }
        
        [Fact]
        public void ItemContent_should_render_Li_start_tag()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Li)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Li));
        }

        [Fact]
        public void ItemContent_should_render_Link_start_tag_with_Link_class()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Link)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.A)).Verifiable();

            renderer.ItemContent(item);

            writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_link_with_Ds_href_if_contentUrl_is_null()
        {
			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, "#", true)).Verifiable();

            renderer.ItemContent(item);

			writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_link_with_url_set_to_contentUrl()
        {
            const string url = "test";
            item.ContentUrl = url;

            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url, true)).Verifiable();

            renderer.ItemContent(item);

			writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_link_with_generated_url_Content_is_not_null()
        {
            const string id = "id";
            const string url = "#id";
            item.Content = () => { };
            item.ContentHtmlAttributes["id"] = id;

			writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Href, url, true)).Verifiable();

            renderer.ItemContent(item);

			writer.VerifyAll();
        }

        [Fact]
        public void ItemContent_should_render_encoded_text()
        {
            const string text = "text";
            item.Text = text;

            writer.Setup(w => w.WriteEncodedText(text)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.WriteEncodedText(text));
        }

        [Fact]
        public void ItemContent_should_render_two_end_tags()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.ItemContent(item);

            writer.VerifyAll();
        }

        [Fact]
        public void TabContent_should_not_render_Item_content_if_it_is_not_visible()
        {
            item.Visible = false;
            item.ContentUrl = "url";
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.TabContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Never());
        }

        [Fact]
        public void TabContent_should_not_render_anything_if_no_ContentUrl_or_Content()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.TabContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Never());
        }

        [Fact]
        public void TabContent_should_render_content_class_if_contentUrl_or_Content_is_set() 
        {
            item.ContentUrl = "url";

            renderer.TabContent(item);

            Assert.Contains(UIPrimitives.Content.ToString(), item.ContentHtmlAttributes["class"].ToString());
        }


        [Fact]
        public void TabContent_should_render_id_attribute_if_contentUrl_or_Content_is_set()
        {
            const string id = "tabStrip1-0"; //generated Id From GetTabContentId method
            item.ContentUrl = "url";

            renderer.TabContent(item);

            Assert.Contains(id, item.ContentHtmlAttributes["id"].ToString());
        }

        [Fact]
        public void TabContent_should_render_ActiveState_class_if_the_item_is_active()
        {
            item.ContentUrl = "url";
            item.Selected = true;
            item.ContentHtmlAttributes.Clear();

            renderer.TabContent(item);

            Assert.Contains(UIPrimitives.ActiveState.ToString(), item.ContentHtmlAttributes["class"].ToString());
        }

        [Fact]
        public void TabContent_should_render_start_div_tag()
        {
            item.Content = () => { };

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.TabContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Div), Times.Exactly(1));
        }

        [Fact]
        public void TabContent_should_render_end_div_tag()
        {
            item.ContentUrl = "url";

            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.TabContent(item);

            writer.Verify(w => w.RenderEndTag(), Times.Exactly(1));
        }

        [Fact]
        public void ItemContent_should_call_WriteImage() 
        {
            const string url = "testUrl";

            item.ImageUrl = url;

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Img)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Img), Times.Exactly(1));
        }

        [Fact]
        public void ItemContent_should_call_WriteSprite()
        {
            const string sprite = "sprite";

            item.SpriteCssClasses = sprite;

            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Span)).Verifiable();

            renderer.ItemContent(item);

            writer.Verify(w => w.RenderBeginTag(HtmlTextWriterTag.Span), Times.Exactly(1));
        }
    }
}
