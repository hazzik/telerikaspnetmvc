namespace Telerik.Web.Mvc.UI.Html.Test
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridDetailRowHtmlBuilderTests
    {
        private Grid<Customer> grid;
        private GridRow<Customer> row;

        public GridDetailRowHtmlBuilderTests()
        {
            grid = GridTestHelper.CreateGrid<Customer>();
            grid.DetailView = new GridDetailView<Customer>();
            row = new GridRow<Customer>(grid, new Customer(), 0);
            row.DetailRow = new GridDetailRow<Customer>();
        }

        [Fact]
        public void Shoul_create_tr()
        {
            var builder = Arrange();
            Assert.Equal("tr", builder.Build().TagName);
        }

        [Fact]
        public void Should_set_details_class()
        {
            var builder = Arrange();
            Assert.Equal("t-detail-row", builder.Build().Attribute("class"));
        }

        [Fact]
        public void Should_create_cell()
        {
            var builder = Arrange();
            var tr = builder.Build();
            Assert.Equal("td", tr.Children[0].TagName);
            Assert.Equal("t-detail-cell", tr.Children[0].Attribute("class"));
        }

        [Fact]
        public void Should_apply_colspan()
        {
            var builder = Arrange();
            var tr = builder.Build();
            Assert.Equal("0", tr.Children[0].Attribute("colspan"));
        }

        [Fact]
        public void Should_set_template_to_null_when_template_is_not_specified()
        {
            var builder = Arrange();
            var tr = builder.Build();
            Assert.Null(tr.Children[0].Template());
        }

        [Fact]
        public void Should_pass_data_item()
        {
            Customer argument = null;

            var customer = new Customer();
            row.DataItem = customer;

            grid.DetailView.Template.CodeBlockTemplate = (c) => argument = c;

            var builder = new GridDetailRowHtmlBuilder<Customer>(row);

            builder.Build().ToString();

            Assert.Same(customer, argument);
        }

        [Fact]
        public void Should_apply_html_attributes()
        {
            row.DetailRow.HtmlAttributes.Add("test", "test");

            var builder = new GridDetailRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();
            Assert.Equal("test", tr.Attribute("test"));
        }

        [Fact]
        public void Should_prefer_html_to_template()
        {
            grid.DetailView.Template.CodeBlockTemplate = delegate { };
            row.DetailRow.Html = "test";

            var builder = new GridDetailRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();

            Assert.Null(tr.Children[0].Template());
            Assert.Equal("test", tr.Children[0].InnerHtml);
        }

        [Fact]
        public void Should_hide_the_row_if_not_expanded()
        {
            var builder = new GridDetailRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();
            Assert.Equal("display:none", tr.Attribute("style"));
        }
        
        [Fact]
        public void Should_show_the_row_if_expanded()
        {
            row.DetailRow.Expanded = true;
            var builder = new GridDetailRowHtmlBuilder<Customer>(row);

            var tr = builder.Build();
            Assert.False(tr.Attributes().ContainsKey("style"));
        }
        
        private GridDetailRowHtmlBuilder<Customer> Arrange()
        {
            return new GridDetailRowHtmlBuilder<Customer>(row);
        }
    }
}