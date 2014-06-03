namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System;
    using System.ComponentModel;
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;

    public class GridSortAdornerTests
    {
        [Fact]
        public void Should_throw_if_column_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GridSortAdorner(null));
        }
        
        [Fact]
        public void Should_preserve_text()
        {
            var adorner = ArrangeAdorner("url");

            var cell = new HtmlTag("th").Html("text");

            adorner.ApplyTo(cell);
            Assert.Equal("text", cell.Children[0].InnerHtml);
        }

        [Fact]
        public void Should_add_link()
        {
            var adorner = ArrangeAdorner("url");

            var cell = new HtmlTag("th").Html("text");

            adorner.ApplyTo(cell);

            IHtmlNode link = cell.Children[0];
            
            Assert.Equal(1, link.Children.Count);
            Assert.Equal("a", link.TagName);
            Assert.Equal("t-link", link.Attribute("class"));
            Assert.Equal("url", link.Attribute("href"));
        }

        [Fact]
        public void Should_create_ascending_sort_icon()
        {
            IHtmlNode arrow = ArrangeAdornerForSortedColumn(ListSortDirection.Ascending);

            Assert.Equal("t-icon t-arrow-up", arrow.Attribute("class"));
        }

        [Fact]
        public void Should_create_descending_sort_icon()
        {
            IHtmlNode arrow = ArrangeAdornerForSortedColumn(ListSortDirection.Descending);

            Assert.Equal("t-icon t-arrow-down", arrow.Attribute("class"));
        }

        [Fact]
        public void Should_place_sort_icon_after_text()
        {
            var column = new Mock<IGridBoundColumn>();

            column.Setup(c => c.GetSortUrl()).Returns(() => "url");
            column.SetupGet(c => c.SortDirection).Returns(ListSortDirection.Descending);
            GridSortAdorner adorner = new GridSortAdorner(column.Object);

            var cell = new HtmlTag("th").Html("text");

            adorner.ApplyTo(cell);

            var link = cell.Children[0];
            var text = link.Children[0];
            var icon = link.Children[1];
            Assert.Equal("text", text.InnerHtml);
            Assert.Equal("span", icon.TagName);
        }

        private IHtmlNode ArrangeAdornerForSortedColumn(ListSortDirection sortDirection)
        {
            var column = new Mock<IGridBoundColumn>();

            column.Setup(c => c.GetSortUrl()).Returns(() => "url");
            column.SetupGet(c => c.SortDirection).Returns(sortDirection);
            GridSortAdorner adorner = new GridSortAdorner(column.Object);

            var cell = new HtmlTag("th").Html("text");

            adorner.ApplyTo(cell);

            var link = cell.Children[0];
            var arrow = link.Children[1];
            return arrow;
        }

        private GridSortAdorner ArrangeAdorner(string url)
        {
            var column = new Mock<IGridBoundColumn>();

            column.Setup(c => c.GetSortUrl()).Returns(() => url);

            GridSortAdorner adorner = new GridSortAdorner(column.Object);
            return adorner;
        }
    }
}
