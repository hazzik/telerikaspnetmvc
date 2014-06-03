namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.UI;
    using Xunit;
    
    public class GridColGroupHtmlBuilderTests
    {
        [Fact]
        public void Should_return_colgroup_tag()
        {
            var builder = new GridColgroupHtmlBuilder(ArrangeGrid(false, false));

            Assert.Equal("colgroup", builder.Build().TagName);
        }

        [Fact]
        public void Should_throw_if_null_is_passed_as_argument()
        {
            Assert.Throws<ArgumentNullException>(() => new GridColgroupHtmlBuilder(null));
        }

        [Fact]
        public void Should_create_adorners_for_grouping()
        {
            var builder = new GridColgroupHtmlBuilder(ArrangeGrid(true, false));

            var adorner = builder.Adorners.First() as GridTagRepeatingAdorner;
            
            Assert.Equal(1, adorner.RepeatCount);
            Assert.Equal("col", adorner.TagName);
            Assert.Equal(TagRenderMode.SelfClosing, adorner.RenderMode);
            Assert.Equal("t-group-col", adorner.CssClasses[0]);
        }

        [Fact]
        public void Should_create_adorners_for_details()
        {
            var builder = new GridColgroupHtmlBuilder(ArrangeGrid(false, true));

            var adorner = builder.Adorners.First() as GridTagRepeatingAdorner;

            Assert.Equal(1, adorner.RepeatCount);
            Assert.Equal("col", adorner.TagName);
            Assert.Equal("t-hierarchy-col", adorner.CssClasses[0]);
        }

        [Fact]
        public void Should_create_hidden_adorner_for_hidden_columns()
        {
            var grid = new Mock<IGrid>();

            var processor = new Mock<GridDataProcessor>(new Mock<IGridBindingContext>().Object);

            processor.SetupGet(p => p.GroupDescriptors).Returns(() => new[] { new GroupDescriptor() });
            grid.SetupGet(g => g.Grouping).Returns(new GridGroupingSettings(grid.Object));
            grid.SetupGet(g => g.DataProcessor).Returns(() => processor.Object);

            var hiddenColumn = new Mock<IGridBoundColumn>();
            hiddenColumn.SetupGet(c => c.Visible).Returns(true);
            hiddenColumn.SetupGet(c => c.Hidden).Returns(true);
            
            grid.SetupGet(g => g.Columns).Returns(() => new[] { hiddenColumn.Object });
            
            var builder = new GridColgroupHtmlBuilder(grid.Object);
            IHtmlBuilder colBuilder = null;
            builder.ChildBuilderCreator = (column) => colBuilder = new GridColHtmlBuilder(column);
            builder.Build();

            Assert.IsType<GridHiddenColumnAdorner>(colBuilder.Adorners.First());
        }

        private static IGrid ArrangeGrid(bool grouping, bool details)
        {
            var grid = new Mock<IGrid>();
            var processor = new Mock<GridDataProcessor>(new Mock<IGridBindingContext>().Object);

            processor.SetupGet(p => p.GroupDescriptors).Returns(() => new[] { new GroupDescriptor() });
            grid.SetupGet(g => g.Grouping).Returns(new GridGroupingSettings(grid.Object) { Enabled = grouping });
            grid.SetupGet(g => g.DataProcessor).Returns(() => processor.Object);
            grid.SetupGet(g => g.HasDetailView).Returns(details);

            var visibleColumn = new Mock<IGridBoundColumn>();
            visibleColumn.SetupGet(c => c.Visible).Returns(true);
            grid.SetupGet(g => g.Columns).Returns(() => new[] { visibleColumn.Object });

            return grid.Object;
        }
    }
}
