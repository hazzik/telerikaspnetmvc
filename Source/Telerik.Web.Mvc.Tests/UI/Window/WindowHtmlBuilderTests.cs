namespace Telerik.Web.Mvc.UI.Tests
{
    using Xunit;
    
    using Infrastructure;
    using Moq;

    public class WindowHtmlBuilderTests
    {
        private IWindowHtmlBuilder renderer;
        private Window window;

        public WindowHtmlBuilderTests()
        {
            window = WindowTestHelper.CreateWindow(null);
            renderer = new WindowHtmlBuilder(window);
            window.Name = "Window";
        }

        [Fact]
        public void WindowTag_should_render_div_tag() 
        {
            IHtmlNode tag = renderer.WindowTag();

            Assert.Equal(tag.TagName, "div");
        }

        [Fact]
        public void WindowTag_should_render_classes()
        {
            IHtmlNode tag = renderer.WindowTag();

            Assert.Equal(UIPrimitives.Widget.ToString() + " t-window", tag.Attribute("class"));
        }

        [Fact]
        public void WindowTag_should_render_html_attributes()
        {
            window.HtmlAttributes.Add("title", "genericInput");

            IHtmlNode tag = renderer.WindowTag();

            Assert.Equal("genericInput", tag.Attribute("title"));
        }

        [Fact]
        public void WindowTag_should_render_id()
        {
            window.Name = "TestName";

            IHtmlNode tag = renderer.WindowTag();

            Assert.Equal("TestName", tag.Attribute("id"));
        }

        [Fact]
        public void WindowTag_should_render_style_display_none_if_visible_false() 
        {
            window.Visible = false;

            IHtmlNode tag = renderer.WindowTag();

            Assert.Contains("display:none", tag.Attribute("style"));            
        }

        [Fact]
        public void HeaderTag_should_render_div_tag()
        {
            IHtmlNode tag = renderer.HeaderTag();

            Assert.Equal(tag.TagName, "div");
        }

        [Fact]
        public void HeaderTag_should_render_classes()
        {
            IHtmlNode tag = renderer.HeaderTag();

            Assert.Equal("t-window-titlebar " + UIPrimitives.Header.ToString(), tag.Attribute("class"));
        }

        [Fact]
        public void IconTag_should_render_span_wrapper()
        {
            const string iconPath = "/Content/Icon.png";
            window.IconUrl = iconPath;

            IHtmlNode tag = renderer.IconTag();

            Assert.Equal("img", tag.TagName);
            Assert.Equal(iconPath, tag.Attribute("src"));
            Assert.Contains("t-window-icon", tag.Attribute("class"));
            Assert.Contains(UIPrimitives.Image, tag.Attribute("class"));
        }

        [Fact]
        public void IconTag_should_render_defoult_alternative_text()
        {
            const string iconPath = "/Content/Icon.png";

            window.IconUrl = iconPath;
            window.IconAlternativeText = "";

            IHtmlNode tag = renderer.IconTag();

            Assert.Equal("icon", tag.Attribute("alt"));
        }

        [Fact]
        public void TitleTag_should_render_span_with_title_text()
        {
            const string title = "WindowTitle";

            window.Title = title;

            IHtmlNode tag = renderer.TitleTag();

            Assert.Equal("span", tag.TagName);
            Assert.Contains("t-window-title", tag.Attribute("class"));
            Assert.Contains(title, tag.Children[0].InnerHtml);
        }

        [Fact]
        public void TitleTag_should_render_component_name_if_Title_is_empty()
        {
            window.Name = "Window";

            IHtmlNode tag = renderer.TitleTag();

            Assert.Equal("span", tag.TagName);
            Assert.Contains("t-window-title", tag.Attribute("class"));
            Assert.Contains("Window", tag.Children[0].InnerHtml);
        }

        [Fact]
        public void ButtonTag_should_render_link_with_span_in_it()
        {
            IHtmlNode linkTag = renderer.ButtonTag(window.Buttons.Container[0]);
            IHtmlNode spanTag = linkTag.Children[0];

            Assert.Equal("a", linkTag.TagName);
            Assert.Contains("#", linkTag.Attribute("href"));
            Assert.Contains("t-window-action", linkTag.Attribute("class"));
            Assert.Contains(UIPrimitives.Link, linkTag.Attribute("class"));

            Assert.Equal("span", spanTag.TagName);
            Assert.Equal("Close", spanTag.InnerHtml);
            Assert.Contains(UIPrimitives.Icon + " t-close", spanTag.Attribute("class"));
        }

        [Fact]
        public void ContentTag_should_render_div_and_class()
        {
            IHtmlNode tag = renderer.ContentTag();

            Assert.Equal("div", tag.TagName);
            Assert.Equal("t-window-content " + UIPrimitives.Content, tag.Attribute("class"));
        }

        [Fact]
        public void ContentTag_should_render_div_tag_with_style()
        {
            window.Height = 300;
            window.Width = 300;

            IHtmlNode tag = renderer.ContentTag();

            Assert.Equal("overflow:auto;width:300px;height:300px", tag.Attribute("style"));
        }

        [Fact]
        public void ContentTag_should_render_div_tag_with_overflow_hidden_when_scrollable_is_false()
        {
            window.Height = 300;
            window.Width = 300;
            window.Scrollable = false;

            IHtmlNode tag = renderer.ContentTag();

            Assert.Equal("overflow:hidden;width:300px;height:300px", tag.Attribute("style"));
        }

        [Fact]
        public void ContentTag_should_render_IFrame_if_ContentUrl_is_remote() 
        {
            window.ContentUrl = "http://www.abv.bg";

            IHtmlNode content = renderer.ContentTag();

            Assert.Equal("iframe", content.Children[0].TagName);
        }

        [Fact]
        public void ContentTag_should_render_IFrame_with_url_if_ContentUrl_is_remote()
        {
            window.ContentUrl = "http://www.abv.bg";

            IHtmlNode content = renderer.ContentTag();

            Assert.Equal(window.ContentUrl, content.Children[0].Attribute("src"));
        }

        [Fact]
        public void ContentTag_should_not_render_IFrame_if_ContentUrl_is_null()
        {
            IHtmlNode content = renderer.ContentTag();

            Assert.Equal(0, content.Children.Count);
        }

        [Fact]
        public void ContentTag_should_not_render_IFrame_if_ContentUrl_is_local()
        {
            window.ContentUrl = "/aspnet-mvc-beta/Window/Content";

            IHtmlNode content = renderer.ContentTag();

            Assert.Equal(0, content.Children.Count);
        }
    }
}