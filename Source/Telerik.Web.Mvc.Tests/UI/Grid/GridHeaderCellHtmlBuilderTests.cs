namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public class GridHeaderCellHtmlBuilderTests
    {
        [Fact]
        public void Should_apply_adorners()
        {
            var adorner = new Mock<IHtmlAdorner>();
            adorner.Setup(a => a.ApplyTo(It.IsAny<IHtmlNode>()));

            var builder = new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object);

            builder.Adorners.Add(adorner.Object);
            builder.Build();

            adorner.VerifyAll();
        }

        [Fact]
        public void Should_return_th()
        {
            Assert.Equal("th", new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object).Build().TagName);
        }

        [Fact]
        public void Should_apply_scope()
        {
            Assert.Equal("col", new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object).Build().Attribute("scope"));
        }

        [Fact]
        public void Should_apply_css_classes()
        {
            Assert.Equal("t-header", new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object).Build().Attribute("class"));
        }

        [Fact]
        public void Should_apply_last_header_css_classe_if_the_column_is_last()
        {
            var column = new Mock<IGridColumn>();
            column.SetupGet(c => c.IsLast).Returns(true);

            Assert.Equal("t-header", new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object).Build().Attribute("class"));
        }

        [Fact]
        public void Should_apply_header_html_attributes()
        {
            var column = new Mock<IGridColumn>();
            var headerAttributes = new Dictionary<string, object>();
            column.SetupGet(c => c.HeaderHtmlAttributes).Returns(headerAttributes);
            new GridHeaderCellHtmlBuilder(column.Object).Build();
            column.VerifyAll();
        }

        [Fact]
        public void Should_throw_if_column_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GridHeaderCellHtmlBuilder(null));
        }

        [Fact]
        public void Should_set_title()
        {
            var column = new Mock<IGridColumn>();
            column.SetupGet(c => c.Title).Returns("Title");

            Assert.Equal(column.Object.Title, new GridHeaderCellHtmlBuilder(column.Object).Build().InnerHtml);
        }

        [Fact]
        public void Should_not_encode_html_in_title()
        {
            var column = new Mock<IGridColumn>();
            column.SetupGet(c => c.Title).Returns("<strong>Title</strong>");

            Assert.Equal(column.Object.Title, new GridHeaderCellHtmlBuilder(column.Object).Build().InnerHtml);
        }

        [Fact]
        public void Should_set_nbsp_if_title_is_null()
        {
            Assert.Equal("&nbsp;", new GridHeaderCellHtmlBuilder(new Mock<IGridColumn>().Object).Build().InnerHtml);
        }

        [Fact]
        public void Should_set_nbsp_if_title_is_empty_string()
        {
            var column = new Mock<IGridColumn>();

            column.SetupGet(c => c.Title).Returns(() => "");

            Assert.Equal("&nbsp;", new GridHeaderCellHtmlBuilder(column.Object).Build().InnerHtml);
        }
    }
}