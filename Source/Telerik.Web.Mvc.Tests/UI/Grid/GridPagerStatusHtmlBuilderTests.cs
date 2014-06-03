namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridPagerStatusHtmlBuilderTests
    {
        [Fact]
        public void Should_create_div()
        {
            var builder = new GridPagerStatusHtmlBuilder(GridTestHelper.CreateLocalization());
            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Equal("t-status-text", div.Attribute("class"));
            Assert.Equal("Displaying items 0 - 0 of 0", div.InnerHtml);
        }
    }
}