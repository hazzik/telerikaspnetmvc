// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class EditorHtmlBuilderTests
    {

        private Editor editor;
        private EditorHtmlBuilder renderer;

        public EditorHtmlBuilderTests()
        {
            editor = EditorTestHelper.CreateEditor();
            renderer = new EditorHtmlBuilder(editor);
        }

        [Fact]
        public void EditorTag_should_output_start_tag()
        {
            IHtmlNode tag = renderer.CreateEditor();

            Assert.Equal("table", tag.TagName);
        }

        [Fact]
        public void EditorTag_should_output_render_id()
        {
            const string id = "testName";
            editor.Name = id;

            IHtmlNode tag = renderer.CreateEditor();

            Assert.Equal(id, tag.Attribute("id"));
        }

        [Fact]
        public void EditorTag_should_output_render_css_classes()
        {
            const string css = "t-widget t-editor t-header";
            
            IHtmlNode tag = renderer.CreateEditor();

            Assert.Equal(css, tag.Attribute("class"));
        }

        [Fact]
        public void EditorTag_should_render_html_attributes()
        {
            editor.HtmlAttributes.Add("title", "genericEditor");

            IHtmlNode tag = renderer.CreateEditor();

            Assert.Equal("genericEditor", tag.Attribute("title"));
        }

        [Fact]
        public void TextareaTag_should_output_start_tag()
        {
            IHtmlNode tag = renderer.CreateTextArea();

            Assert.Equal("textarea", tag.TagName);
        }
    }
}
