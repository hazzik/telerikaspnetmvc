namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Infrastructure;
    using Moq;
    using Xunit;

    public class GridFooterRowHtmlScrollingBuilderTests
    {
        private GridFooterRowHtmlScrollingBuilder builder;
        private Mock<IHtmlBuilder> footerRowBuilder;
        private Mock<IHtmlBuilder> colGroupBuilder;

        public GridFooterRowHtmlScrollingBuilderTests()
        {
            footerRowBuilder = new Mock<IHtmlBuilder>();
            footerRowBuilder.Setup(d => d.Build()).Returns(() => new HtmlTag("tr"));

            colGroupBuilder = new Mock<IHtmlBuilder>();
            colGroupBuilder.Setup(d => d.Build()).Returns(() => new HtmlTag("tr"));
            builder = new GridFooterRowHtmlScrollingBuilder(footerRowBuilder.Object, colGroupBuilder.Object);
        }

        [Fact]
        public void Should_render_div()
        {
            var result = builder.Build();
            result.TagName.ShouldEqual("div");
        }

        [Fact]
        public void Should_use_decorated_builder_for_inner_elements()
        {
            builder.Build();
            footerRowBuilder.Verify(b => b.Build(), Times.Once());
            colGroupBuilder.Verify(b => b.Build(), Times.Once());
        }
    }
}