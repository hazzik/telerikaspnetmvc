namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using Xunit;

    public class GridHeaderRowHtmlBuilderTests
    {
        [Fact]
        public void Should_return_table_row()
        {
            var grid = new Mock<IGrid>();
            var processor = new Mock<GridDataProcessor>(new Mock<IGridBindingContext>().Object);

            processor.SetupGet(p => p.GroupDescriptors).Returns(new[] { new GroupDescriptor() });

            grid.SetupGet(g => g.DataProcessor).Returns(processor.Object);
            grid.SetupGet(g => g.Grouping).Returns(new GridGroupingSettings(grid.Object));

            IHtmlNode result = new GridHeaderRowHtmlBuilder(grid.Object).Build();

            Assert.Equal("tr", result.TagName);
        }

        [Fact]
        public void Should_use_cell_builders_to_create_cells_in_row()
        {
            var grid = ArrangeGrid(false, false, false);

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(() => new HtmlTag("td"));

            var builder = new GridHeaderRowHtmlBuilder(grid.Object);

            builder.ChildBuilderCreator = (column) => cellBuilder.Object;

            builder.Build();

            cellBuilder.VerifyAll();
        }

        [Fact]
        public void Should_use_adorner_if_details_view_is_present()
        {
            var grid = ArrangeGrid(true, false, false);

            grid.SetupGet(g => g.HasDetailView).Returns(true);

            var builder = new GridHeaderRowHtmlBuilder(grid.Object);

            var adorner = builder.Adorners[0] as GridTagRepeatingAdorner;

            Assert.Equal("th", adorner.TagName);
            Assert.Equal("t-header", adorner.CssClasses[0]);
            Assert.Equal("t-hierarchy-cell", adorner.CssClasses[1]);
        }

        [Fact]
        public void Should_create_sort_adorners_if_sorting_is_enabled_and_column_is_sortable()
        {
            var grid = ArrangeGrid(true, false, false);

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(() => new HtmlTag("td"));
            cellBuilder.Setup(b => b.Adorners.Add(It.IsAny<GridSortAdorner>()));

            var builder = new GridHeaderRowHtmlBuilder(grid.Object);
            builder.ChildBuilderCreator = (column) => cellBuilder.Object;

            builder.Build();

            cellBuilder.VerifyAll();
        }

        [Fact]
        public void Should_create_filter_adorners_if_filtering_is_enabled_and_column_is_filterable()
        {
            var grid = ArrangeGrid(false, true, false);

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(() => new HtmlTag("td"));
            cellBuilder.Setup(b => b.Adorners.Add(It.IsAny<GridFilterAdorner>()));

            var builder = new GridHeaderRowHtmlBuilder(grid.Object);
            builder.ChildBuilderCreator = (column) => cellBuilder.Object;

            builder.Build();

            cellBuilder.VerifyAll();
        }

        [Fact]
        public void Should_create_grouping_adorners_if_grouping_is_enabled()
        {
            var grid = ArrangeGrid(false, true, true);


            var cellBuilder = new Mock<IHtmlBuilder>();

            var builder = new GridHeaderRowHtmlBuilder(grid.Object)
            {
                ChildBuilderCreator = (column) => cellBuilder.Object
            };

            var adorner = builder.Adorners[0] as GridTagRepeatingAdorner;

            Assert.Equal("th", adorner.TagName);
            Assert.Equal("t-header", adorner.CssClasses[0]);
            Assert.Equal("t-group-cell", adorner.CssClasses[1]);
        }

        [Fact]
        public void Should_create_hidden_adorners_if_the_column_is_hidden()
        {
            var grid = new Mock<IGrid>();
            var processor = new Mock<GridDataProcessor>(new Mock<IGridBindingContext>().Object);

            processor.SetupGet(p => p.GroupDescriptors).Returns(() => new[] { new GroupDescriptor() });
            grid.SetupGet(g => g.Sorting).Returns(new GridSortSettings(grid.Object));
            grid.SetupGet(g => g.Filtering).Returns(new GridFilteringSettings());
            grid.SetupGet(g => g.Grouping).Returns(new GridGroupingSettings(grid.Object));
            grid.SetupGet(g => g.DataProcessor).Returns(() => processor.Object);

            var hidden = new Mock<IGridBoundColumn>();
            hidden.SetupGet(c => c.Visible).Returns(true);
            hidden.SetupGet(c => c.Hidden).Returns(true);
            grid.SetupGet(g => g.Columns).Returns(() => new[] { hidden.Object });

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(() => new HtmlTag("td"));
            cellBuilder.Setup(b => b.Adorners.Add(It.IsAny<GridHiddenColumnAdorner>()));

            var builder = new GridHeaderRowHtmlBuilder(grid.Object)
            {
                ChildBuilderCreator = (column) => cellBuilder.Object
            };

            builder.Build();

            cellBuilder.VerifyAll();
        }

        private static Mock<IGrid> ArrangeGrid(bool sort, bool filtering, bool grouping)
        {
            var grid = new Mock<IGrid>();
            var processor = new Mock<GridDataProcessor>(new Mock<IGridBindingContext>().Object);

            processor.SetupGet(p => p.GroupDescriptors).Returns(() => new[] { new GroupDescriptor() });
            grid.SetupGet(g => g.Sorting).Returns(new GridSortSettings(grid.Object) { Enabled = sort });
            grid.SetupGet(g => g.Filtering).Returns(new GridFilteringSettings() { Enabled = filtering });
            grid.SetupGet(g => g.Grouping).Returns(new GridGroupingSettings(grid.Object) { Enabled = grouping });
            grid.SetupGet(g => g.DataProcessor).Returns(() => processor.Object);

            var visibleColumn = new Mock<IGridBoundColumn>();
            visibleColumn.SetupGet(c => c.Visible).Returns(true);
            visibleColumn.SetupGet(c => c.Sortable).Returns(true);
            visibleColumn.SetupGet(c => c.Filterable).Returns(true);
            visibleColumn.SetupGet(c => c.Groupable).Returns(true);
            grid.SetupGet(g => g.Columns).Returns(() => new[] { visibleColumn.Object });

            return grid;
        }
    }
}
