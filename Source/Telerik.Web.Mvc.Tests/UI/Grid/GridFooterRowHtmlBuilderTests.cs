namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Moq;
    using Xunit;

    public class GridFooterRowHtmlBuilderTests
    {
        [Fact]
        public void Should_render_table_row_if_no_scrolling()
        {
            var builder = new GridFooterRowHtmlBuilder(Enumerable.Empty<IGridColumn>(), c => new Mock<IHtmlBuilder>().Object);

            var result = builder.Build();

            result.TagName.ShouldEqual("tr");
        }

        [Fact]
        public void Should_call_child_buider_factory_for_every_column()
        {
            const int expectedCallTimes = 2;

            int isCalled = 0;

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(c => c.Build())
                       .Returns(() => new HtmlTag("td"))
                       .Callback(() => ++isCalled);

            new GridFooterRowHtmlBuilder(GetColumns(expectedCallTimes), column => cellBuilder.Object).Build();

            isCalled.ShouldEqual(expectedCallTimes);
        }

        [Fact]
        public void Should_append_child_builder_content_to_the_build_node()
        {
            const int expectedChildCount = 2;

            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(c => c.Build()).Returns(() => new HtmlTag("td"));

            var footerNode = new GridFooterRowHtmlBuilder(GetColumns(expectedChildCount), column => cellBuilder.Object).Build();

            footerNode.Children.Count.ShouldEqual(expectedChildCount);
        }

        [Fact]
        public void Should_call_child_builder_only_for_visible_columns()
        {
            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(c => c.Build()).Returns(() => new HtmlTag("td"));

            var columns = new[]
                              {
                                  CreateColumn(),
                                  CreateColumn(false)
                              };

            new GridFooterRowHtmlBuilder(columns, column => cellBuilder.Object).Build();
            cellBuilder.Verify(b => b.Build(), Times.Once());
        }
        
        [Fact]
        public void Should_add_adorner_for_group_count()
        {
            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(c => c.Build()).Returns(() => new HtmlTag("td"));

            var columns = new[]
                              {
                                  CreateColumn(),
                                  CreateColumn(false)
                              };

            var builder = new GridFooterRowHtmlBuilder(columns, column => cellBuilder.Object)
                              {
                                  RepeatingAdornerCount = 2
                              };
            var node = builder.Build();
            node.Children.Count.ShouldEqual(3);
        }

        private IEnumerable<IGridColumn> GetColumns(int howMany)
        {
            return Enumerable.Range(0, howMany).Select(i => CreateColumn());
        }

        private IGridColumn CreateColumn()
        {
            return CreateColumn(true);
        }

        private IGridColumn CreateColumn(bool isVisible)
        {
            var column = new Mock<IGridColumn>();
            column.SetupGet(c => c.Visible).Returns(isVisible);
            return column.Object;
        }
    }
}