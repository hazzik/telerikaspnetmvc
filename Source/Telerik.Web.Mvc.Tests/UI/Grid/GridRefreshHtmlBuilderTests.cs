namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Xunit;

    public class GridRefreshHtmlBuilderTests
    {
        [Fact]
        public void Should_create_div()
        {
            var builder = new GridRefreshHtmlBuilder("test");

            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Equal("t-status", div.Attribute("class"));
        }
        
        [Fact]
        public void Should_create_link_inside_div()
        {
            var builder = new GridRefreshHtmlBuilder("test");

            var div = builder.Build();

            var link = div.Children[0];

            Assert.Equal("a", link.TagName);
            Assert.Equal("t-icon t-refresh", link.Attribute("class"));
            Assert.Equal("test", link.Attribute("href"));
        }
    }
}