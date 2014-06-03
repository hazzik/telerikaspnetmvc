namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Xunit;
    
    public class GridPagerButtonHtmlBuilderTests
    {
        [Fact]
        public void Should_create_link()
        {
            var builder = new GridPagerButtonHtmlBuilder("first");

            var a = builder.Build();

            Assert.Equal("a", a.TagName);
            Assert.Equal(UIPrimitives.Link, a.Attribute("class"));
        }

        [Fact]
        public void Should_create_span_inside_link()
        {
            var builder = new GridPagerButtonHtmlBuilder("first");

            var a = builder.Build();
            var span = a.Children[0];

            Assert.Equal("span", span.TagName);
            Assert.Equal(UIPrimitives.Icon + " t-arrow-first", span.Attribute("class"));
        }

        [Fact]
        public void Should_add_disabled_state_when_the_button_is_disabled()
        {
            var builder = new GridPagerButtonHtmlBuilder("first")
            {
                Enabled = false
            };

            var a = builder.Build();

            Assert.Contains("t-state-disabled", a.Attribute("class"));
        }

        [Fact]
        public void Should_set_url()
        {
            var builder = new GridPagerButtonHtmlBuilder("first")
            {
                Url = "test"
            };

            var a = builder.Build();
            Assert.Equal("test", a.Attribute("href"));
        }

        [Fact]
        public void Should_set_hash_as_url_if_disabled()
        {
            var builder = new GridPagerButtonHtmlBuilder("first")
            {
                Url = "test",
                Enabled = false
            };

            var a = builder.Build();
            Assert.Equal("#", a.Attribute("href"));
        }

    }
}
