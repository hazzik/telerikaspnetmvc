
namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    
    public static class GridTestHelper
    {
        public static ControllerBase Controller(IDictionary<string, ValueProviderResult> valueProvider, ViewDataDictionary viewData)
        {
            return new ControllerTestDouble(valueProvider, viewData);
        }

        public static Grid<T> CreateGrid<T>(HtmlTextWriter writer, IClientSideObjectWriter objectWriter) where T : class
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            if (writer != null)
            {
                httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(writer);
            }

            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();
            Mock<IUrlGenerator> urlGenerator = new Mock<IUrlGenerator>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary());

            ViewContext viewContext = TestHelper.CreateViewContext();
            
            Mock<IClientSideObjectWriterFactory> clientSideObjectFactory = new Mock<IClientSideObjectWriterFactory>();
            
            clientSideObjectFactory.Setup(f=>f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(objectWriter);

            Grid<T> grid = new Grid<T>(viewContext, clientSideObjectFactory.Object, urlGenerator.Object) { Name = "Grid" };

            return grid;
        }
        
        public static Grid<T> CreateGrid<T>(HtmlTextWriter writer) where T : class
        {
            return CreateGrid<T>(writer, null);
        }

        public static Grid<T> CreateGrid<T>() where T : class
        {
            return CreateGrid<T>(new HtmlTextWriter(TextWriter.Null));
        }
        
        public static void Add(this IDictionary<string, ValueProviderResult> valueProvider, string key, object value)
        {
            valueProvider[key] = new ValueProviderResult(value, value.ToString(), CultureInfo.InvariantCulture);
        }
    }
}