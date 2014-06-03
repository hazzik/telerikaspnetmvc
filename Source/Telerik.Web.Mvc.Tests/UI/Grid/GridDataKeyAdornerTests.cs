namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI;
    using Xunit;
    
    public class GridDataKeyAdornerTests
    {
        [Fact]
        public void Should_create_hidden_fields()
        {
            var adorner = new GridDataKeyAdorner();
        }
    }
    
    public class GridDataKeyAdorner : IHtmlAdorner
    {
        public void ApplyTo(IHtmlNode target)
        {

        }
    }
}
