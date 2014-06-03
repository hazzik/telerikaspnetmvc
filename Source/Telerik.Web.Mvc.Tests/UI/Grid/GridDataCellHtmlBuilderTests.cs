namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridDataCellHtmlBuilderTests
    {
        [Fact]
        public void Should_append_html_attributes()
        {
            var cell = ArrangeCell();
            var builder = new GridDataCellHtmlBuilder<Customer>(cell);

            cell.HtmlAttributes["test"] = "test";

            Assert.Equal("test", builder.Build().Attribute("test"));
        }

        [Fact]
        public void Should_return_td()
        {
            var builder = new GridDataCellHtmlBuilder<Customer>(ArrangeCell());

            Assert.Equal("td", builder.Build().TagName);
        }

        [Fact]
        public void Should_not_encode_content()
        {
            var cell = ArrangeCell();
            cell.Column.Encoded = false;
            var td = new GridDataCellHtmlBuilder<Customer>(cell) { Value = "<" }.Build();
            Assert.Equal("<", td.InnerHtml);
        }

        [Fact]
        public void Should_encode_content_if_column_is_encoded()
        {
            GridCell<Customer> cell = ArrangeCell();
            cell.Column.Encoded = true;

            var td = new GridDataCellHtmlBuilder<Customer>(cell)
            {
                Value = "<",
            }.Build();
            
            Assert.Equal("&lt;", td.InnerHtml);
        }

        [Fact]
        public void Should_output_nbsp_if_value_is_null()
        {
            var td = new GridDataCellHtmlBuilder<Customer>(ArrangeCell()).Build();

            Assert.Equal("&nbsp;", td.InnerHtml);
        }

        [Fact]
        public void Should_apply_format()
        {
            var cell = ArrangeCell();
            cell.Column.Format = "test{0}";

            var td = new GridDataCellHtmlBuilder<Customer>(cell)
            {
                Value = 1,
            }.Build();

            Assert.Equal("test1", td.InnerHtml);
        }

        private GridCell<Customer> ArrangeCell()
        {
            var column = new GridBoundColumn<Customer, string>(GridTestHelper.CreateGrid<Customer>(), c => c.Address);
            return new GridCell<Customer>(column, new Customer());
        }

    }
}
