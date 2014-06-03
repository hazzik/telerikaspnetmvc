namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI;
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridRowHtmlBuilderTests
    {
        [Fact]
        public void Should_invoke_cell_action()
        {
            Grid<Customer> grid = GridTestHelper.CreateGrid<Customer>();
            var called = false;

            grid.CellAction = delegate
            {
                called = true;
            };

            var row = new GridRow<Customer>(grid, new Customer(), 0);
            grid.Columns.Add(new GridBoundColumn<Customer, bool>(grid, c => c.IsActive));
            using (new MockViewEngine())
            {
                var builder = new GridRowHtmlBuilder<Customer>(row);
                builder.Build();
            }

            Assert.True(called);

        }

        [Fact]
        public void Should_return_tr()
        {
            var row = new GridRow<Customer>(GridTestHelper.CreateGrid<Customer>(), new Customer(), 0);
            var builder = new GridRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();
            Assert.Equal("tr", tr.TagName);
            Assert.False(tr.Attributes().ContainsKey("class"));
        }

        [Fact]
        public void Should_apply_attributes()
        {
            var row = new GridRow<Customer>(GridTestHelper.CreateGrid<Customer>(), new Customer(), 0);
            row.HtmlAttributes["test"] = "test";
            var builder = new GridRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();

            Assert.Equal("test", tr.Attribute("test"));
        }

        [Fact]
        public void Should_apply_selected_state()
        {
            var row = new GridRow<Customer>(GridTestHelper.CreateGrid<Customer>(), new Customer(), 0);
            row.Selected = true;
            var builder = new GridRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();

            Assert.Equal(UIPrimitives.SelectedState, tr.Attribute("class"));
        }

        [Fact]
        public void Should_apply_alt_class_for_odd_index_rows()
        {
            var row = new GridRow<Customer>(GridTestHelper.CreateGrid<Customer>(), new Customer(), 1);
            var builder = new GridRowHtmlBuilder<Customer>(row);

            Assert.Equal("t-alt", builder.Build().Attribute("class"));
        }

        [Fact]
        public void Should_create_cells_builders_for_visible_columns()
        {
            Grid<Customer> grid = GridTestHelper.CreateGrid<Customer>();
            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(new HtmlTag("td"));
            
            var column = new Mock<GridColumnBase<Customer>>(grid);
            
            column.Setup(c => c.CreateDisplayHtmlBuilder(It.IsAny<GridCell<Customer>>())).Returns(cellBuilder.Object);
            
            grid.Columns.Add(column.Object);
            
            var row = new GridRow<Customer>(grid, new Customer(), 1);
            
            var builder = new GridRowHtmlBuilder<Customer>(row);

            builder.Build();

            column.VerifyAll();
        }
    }
}
