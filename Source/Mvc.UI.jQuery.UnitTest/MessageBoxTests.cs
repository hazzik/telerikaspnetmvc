// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class MessageBoxTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;

        private readonly MessageBox _messageBox;

        public MessageBoxTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _messageBox = new MessageBox(_viewContext, new Mock<IClientSideObjectWriterFactory>().Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _messageBox.Name = "myProgressBar";
            _messageBox.HtmlAttributes.Add("style", "border:1px solid #222");
            _messageBox.Content = delegate { };
            _messageBox.Theme = "custom";

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _messageBox.Render();

            _httpContext.Verify();
        }
    }
}