#if MVC2
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridInlineEditRowHtmlBuilderTests
    {
        private readonly GridRow<Customer> row;
        private readonly GridEditRowHtmlBuilder<Customer> builder;

        public GridInlineEditRowHtmlBuilderTests()
        {
            var grid = GridTestHelper.CreateGrid<Customer>();
            grid.Columns.Add(new GridTemplateColumn<Customer>(grid, c => { }));

            row = new GridRow<Customer>(grid, new Customer(), 0);
            builder = new GridEditRowHtmlBuilder<Customer>(row);
        }

        [Fact]
        public void Should_create_td_with_colspan()
        {
            builder.Colspan = 1;

            var td = builder.Build().Children[0];

            Assert.Equal("td", td.TagName);
            Assert.Equal("1", td.Attribute("colspan"));
        }

        [Fact]
        public void Should_set_class()
        {
            var td = builder.Build().Children[0];
            Assert.Equal(UIPrimitives.Grid.EditingContainer, td.Attribute("class"));
        }

        [Fact]
        public void Should_create_form_inside_td()
        {
            builder.ActionUrl = "test";
            builder.ID = "test";
            var td = builder.Build().Children[0];
            var form = td.Children[0];

            Assert.Equal("form", form.TagName);
            Assert.Equal("test", form.Attribute("id"));
            Assert.Equal("post", form.Attribute("method"));
            Assert.Equal("test", form.Attribute("action"));
            Assert.Equal(UIPrimitives.Grid.EditingForm, form.Attribute("class"));
        }

        [Fact]
        public void Should_create_table_inside_form()
        {
            var td = builder.Build().Children[0];
            var form = td.Children[0];
            var table = form.Children[0];

            Assert.Equal("table", table.TagName);
            Assert.Equal("0", table.Attribute("cellspacing"));
        }

        [Fact]
        public void Should_create_colgoup_inside_table()
        {
            var td = builder.Build().Children[0];
            var form = td.Children[0];
            var table = form.Children[0];
            var colgroup = table.Children[0];

            Assert.Equal("colgroup", colgroup.TagName);
        }

        [Fact]
        public void Should_create_tbody_inside_table()
        {
            var td = builder.Build().Children[0];
            var form = td.Children[0];
            var table = form.Children[0];
            var tbody = table.Children[1];

            Assert.Equal("tbody", tbody.TagName);
        }

        [Fact]
        public void Should_create_tr_inside_tbody()
        {
            var td = builder.Build().Children[0];
            var form = td.Children[0];
            var table = form.Children[0];
            var tbody = table.Children[1];
            var tr = tbody.Children[0];

            Assert.Equal("tr", tr.TagName);
        }

        [Fact]
        public void Should_create_cells_inside_row()
        {
            var column = new Mock<GridColumnBase<Customer>>(GridTestHelper.CreateGrid<Customer>());
            var cellBuilder = new Mock<IHtmlBuilder>();
            cellBuilder.Setup(b => b.Build()).Returns(new HtmlTag("td"));
            column.Setup(c => c.CreateEditorHtmlBuilder(It.IsAny<GridCell<Customer>>())).Returns(cellBuilder.Object);
            row.Grid.Columns.Clear();
            row.Grid.Columns.Add(column.Object);
            
            builder.Build();

            column.VerifyAll();
        }
    }
}
#endif