namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridTemplateCellHtmlBuilderTests
    {
        [Fact]
        public void Should_return_td()
        {
            var builder = new GridTemplateCellHtmlBuilder<Customer>(ArrangeCell());
            Assert.Equal("td", builder.Build().TagName);
        }
        
        [Fact]
        public void Should_set_template()
        {
            var builder = new GridTemplateCellHtmlBuilder<Customer>(ArrangeCell());
            
            Assert.NotNull(builder.Build().Template());
        }

        private GridCell<Customer> ArrangeCell()
        {
            var column = new GridTemplateColumn<Customer>(GridTestHelper.CreateGrid<Customer>(), c => { });
            return new GridCell<Customer>(column, new Customer())
                {
                    Content = delegate { }
                };
        }
    }
}
