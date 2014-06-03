#if MVC2
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridDisplayForCellHtmlBuilderTests
    {
        [Fact]
        public void Should_use_display_for()
        {
            using (new MockViewEngine())
            {
                var cell = new GridCell<Customer>(new GridBoundColumn<Customer, bool>(GridTestHelper.CreateGrid<Customer>(), c => c.IsActive), new Customer());

                var builder = new GridDisplayForCellHtmlBuilder<Customer, bool>(cell, c => c.IsActive);

                var td = builder.Build();

                Assert.Equal("<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\" />", td.InnerHtml.ToString());
            }
        }
    }
}
#endif