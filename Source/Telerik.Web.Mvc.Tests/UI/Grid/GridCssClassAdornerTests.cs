namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridCssClassAdornerTests
    {
        [Fact]
        public void Should_apply_css_class()
        {
            var target = new HtmlTag("td");
            var adorner = new GridCssClassAdorner()
            {
                CssClasses = { UIPrimitives.Last }
            };
            
            adorner.ApplyTo(target);

            Assert.Equal(UIPrimitives.Last, target.Attribute("class"));
        }
    }
}
