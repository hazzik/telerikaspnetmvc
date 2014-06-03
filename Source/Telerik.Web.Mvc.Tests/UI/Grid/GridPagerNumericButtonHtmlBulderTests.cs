namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Xunit;
    
    public class GridPagerNumericButtonHtmlBulderTests
    {
        [Fact]
        public void Should_create_link()
        {
            var builder = new GridPagerNumericButtonHtmlBulder("1");
            var a = builder.Build();

            Assert.Equal("a", a.TagName);
            Assert.Equal(UIPrimitives.Link, a.Attribute("class"));
            Assert.Equal("1", a.InnerHtml);
        }

        [Fact]
        public void Should_apply_url()
        {
            var builder = new GridPagerNumericButtonHtmlBulder("1")
            {
                Url = "test"
            };
            var a = builder.Build();

            Assert.Equal("test", a.Attribute("href"));
        }

        [Fact]
        public void Shoul_create_span_if_active()
        {
            var builder = new GridPagerNumericButtonHtmlBulder("1")
            {
                Active = true
            };

            var span = builder.Build();

            Assert.Equal("span", span.TagName);
            Assert.Equal(UIPrimitives.ActiveState, span.Attribute("class"));
            Assert.Equal("1", span.InnerHtml);
        }
    }
}
