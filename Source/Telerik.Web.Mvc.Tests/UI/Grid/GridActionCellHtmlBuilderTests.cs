#if MVC2 || MVC3
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridActionCellHtmlBuilderTests
    {
        [Fact]
        public void Should_pass_rendering_context()
        {
            var command = new Mock<GridActionCommandBase>();
            command.Setup(c => c.BoundModeHtml<Customer>(It.IsAny<IHtmlNode>(), It.IsAny<IGridRenderingContext<Customer>>()));

            var builder = new GridActionCellHtmlBuilder<Customer>(ArrangeCell(new[] { command.Object }));
            builder.Build();

            command.VerifyAll();
        }

        [Fact]
        public void Should_call_edit_html_if_in_edit_mode()
        {
            var command = new Mock<GridActionCommandBase>();
            command.Setup(c => c.EditModeHtml<Customer>(It.IsAny<IHtmlNode>(), It.IsAny<IGridRenderingContext<Customer>>()));

            var cell = ArrangeCell(new[] { command.Object });
            cell.InEditMode = true;
            var builder = new GridActionCellHtmlBuilder<Customer>(cell);
            
            builder.Build();

            command.VerifyAll();
        }

        [Fact]
        public void Should_call_insert_html_if_in_insert_mode()
        {
            var command = new Mock<GridActionCommandBase>();
            command.Setup(c => c.InsertModeHtml<Customer>(It.IsAny<IHtmlNode>(), It.IsAny<IGridRenderingContext<Customer>>()));

            var cell = ArrangeCell(new[] { command.Object });
            cell.InInsertMode = true;
            
            var builder = new GridActionCellHtmlBuilder<Customer>(cell);

            builder.Build();

            command.VerifyAll();
        }

        [Fact]
        public void Should_call_bound_mode_if_popup_editing_is_enabled()
        {
            var command = new Mock<GridActionCommandBase>();
            command.Setup(c => c.BoundModeHtml<Customer>(It.IsAny<IHtmlNode>(), It.IsAny<IGridRenderingContext<Customer>>()));
            var cell = ArrangeCell(new[] { command.Object });
            
            cell.InEditMode = true;
            cell.Grid.Editing.Mode = GridEditMode.PopUp;

            var builder = new GridActionCellHtmlBuilder<Customer>(cell);

            builder.Build();

            command.VerifyAll();
        }

        [Fact]
        public void Should_return_td()
        {
            var builder = new GridActionCellHtmlBuilder<Customer>(ArrangeCell(new GridActionCommandBase[0]));
            var td = builder.Build();

            Assert.Equal("td", td.TagName);
        }

        private GridCell<Customer> ArrangeCell(GridActionCommandBase[] commands)
        {
            var column = new GridActionColumn<Customer>(GridTestHelper.CreateGrid<Customer>());
            column.Commands.AddRange(commands);

            return new GridCell<Customer>(column, new Customer());
        }
    }
}
#endif