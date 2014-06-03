namespace Telerik.Web.Mvc.UI
{
    using Moq;
    using System.Linq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridFilterAdornerTests
    {
        [Fact]
        public void Should_create_filtering_icon_if_column_is_filterable()
        {
            var cell = new HtmlTag("td");

            Mock<IGridBoundColumn> column = new Mock<IGridBoundColumn>();
            column.Setup(c => c.Grid).Returns(GridTestHelper.CreateGrid<Customer>());

            new GridFilterAdorner(column.Object).ApplyTo(cell);

            var filter = cell.Children.Last();
            Assert.Equal("div", filter.TagName);
            Assert.Equal("t-grid-filter t-state-default", filter.Attribute("class"));
            Assert.Equal(1, filter.Children.Count);
            Assert.Equal("t-icon t-filter", filter.Children[0].Attribute("class"));
            Assert.Equal("span", filter.Children[0].TagName);
        }
    }
}
