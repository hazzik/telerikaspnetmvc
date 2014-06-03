namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridPagerTextBoxHtmlBuilderTests
    {
        public GridPagerTextBoxHtmlBuilderTests()
        {
        }

        [Fact]
        public void Should_create_div()
        {
            var localization = GridTestHelper.CreateLocalization();
            var builder = new GridPagerTextBoxHtmlBuilder(localization);

            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Contains("Page ", div.InnerHtml);
            Assert.Contains("of", div.InnerHtml);
        }

        [Fact]
        public void Should_create_input()
        {
            var builder = new GridPagerTextBoxHtmlBuilder(GridTestHelper.CreateLocalization())
            {
                Value = "1",
                TotalPages = 20
            };

            var input = builder.Build().Children[1];

            Assert.Equal("input", input.TagName);
            Assert.Equal("text", input.Attribute("type"));
            Assert.Equal("1", input.Attribute("value"));
            Assert.Equal("of 20", builder.Build().Children[2].ToString());
        }
    }
}