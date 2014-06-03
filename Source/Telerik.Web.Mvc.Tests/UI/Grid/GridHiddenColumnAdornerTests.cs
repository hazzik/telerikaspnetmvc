namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridHiddenColumnAdornerTests
    {
        [Fact]
        public void Should_apply_display_none()
        {
            var td = new HtmlTag("td");
            var adorner = new GridHiddenColumnAdorner();
            adorner.ApplyTo(td);

            Assert.Contains("display:none", td.Attribute("style"));
        }
    }
}
