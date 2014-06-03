namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public class GridFooterCellHtmlBuilderTests
    {
        private GridFooterCellHtmlBuilder builder;
        private readonly Mock<IGridColumn> column;

        public GridFooterCellHtmlBuilderTests()
        {
            column = new Mock<IGridColumn>();
            builder = new GridFooterCellHtmlBuilder(column.Object);
        }

        [Fact]
        public void Should_return_not_null_html_node()
        {
            builder.Build().ShouldNotBeNull();
        }

        [Fact]
        public void Should_return_html_node_with_tag_name()
        {
            builder.Build().TagName.ShouldEqual("td");
        }

        [Fact]
        public void Should_apply_header_html_attributes()
        {
            
            var footerAttributes = new Dictionary<string, object>();
            column.SetupGet(c => c.FooterHtmlAttributes).Returns(footerAttributes);
            builder.Build();
            column.VerifyAll();
        }

        [Fact]
        public void Should_render_column_footer_template_if_declared()
        {
            const string expectedContent = "template content";
            
            column.SetupGet(c => c.FooterTemplate)
                .Returns(() => new HtmlTemplate
                                   {
                                       Html = expectedContent
                                   });
            
            var node = builder.Build();
            
            column.VerifyAll();
            node.InnerHtml.ShouldEqual(expectedContent);
        }

        [Fact]
        public void Should_render_nbsp_if_no_footer_template()
        {
            builder.Build().InnerHtml.ShouldEqual("&nbsp;");
        }
    }
}