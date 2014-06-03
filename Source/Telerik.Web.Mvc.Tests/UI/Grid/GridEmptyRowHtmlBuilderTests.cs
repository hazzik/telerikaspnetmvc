namespace Telerik.Web.Mvc.UI.Tests.UI.Grid
{
    using Telerik.Web.Mvc.UI.Html;
    using Xunit;

    public class GridEmptyRowHtmlBuilderTests
    {
        private GridLocalization localization;

        public GridEmptyRowHtmlBuilderTests()
        {
            localization = GridTestHelper.CreateLocalization();
        }

        [Fact]
        public void Should_return_td()
        {
            var template = new HtmlTemplate();

            var builder = new GridEmptyRowHtmlBuilder(1, template);

            Assert.Equal("td", builder.Build().Children[0].TagName);
        }

        [Fact]
        public void Should_return_td_with_empty_message()
        {
            var template = new HtmlTemplate();

            template.Html = "empty";

            var builder = new GridEmptyRowHtmlBuilder(1, template);

            Assert.Equal(template.Html, builder.Build().Children[0].InnerHtml);
        }
    }
}