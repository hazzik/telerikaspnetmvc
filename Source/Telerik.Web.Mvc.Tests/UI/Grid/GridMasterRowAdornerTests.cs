namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridMasterRowAdornerTests
    {
        [Fact]
        public void Should_apply_class()
        {
            var adorner = new GridMasterRowAdorner();
            var tr = new HtmlTag("tr");
            adorner.ApplyTo(tr);

            Assert.Equal("t-master-row", tr.Attribute("class"));
        }
    }
}