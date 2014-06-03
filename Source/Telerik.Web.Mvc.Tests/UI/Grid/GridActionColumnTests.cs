
namespace Telerik.Web.Mvc.UI.Tests
{
    using Telerik.Web.Mvc.UI.Html;
    using Xunit;

    public class GridActionColumnTests
    {
        [Fact]
        public void Should_create_grid_action_cell_html_builder()
        {
            var column = new GridActionColumn<Customer>(GridTestHelper.CreateGrid<Customer>());
            Assert.IsType<GridActionCellHtmlBuilder<Customer>>(column.CreateDisplayHtmlBuilder(null));
        }
    }
}
