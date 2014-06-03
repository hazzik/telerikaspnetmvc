namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;

    public class GridHeaderHierarchyAdornerTests
    {
        [Fact]
        public void Should_create_a_cell_with_appropriate_class()
        {
            var row = new HtmlTag("tr");

            var adorner = new GridHeaderHierarchyAdorner();

            adorner.ApplyTo(row);

            Assert.Equal("th", row.Children.First().TagName);
            Assert.Equal("t-header t-hierarchy-cell", row.Children.First().Attribute("class"));
        }
    }
}
