namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.Collections.Generic;
    using System.IO;
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Web.Routing;

    using Moq;

    using UI;

    public static class GridTestHelper
    {
        private class ControllerTestDouble : Controller
        {
            public ControllerTestDouble(IDictionary<string, ValueProviderResult> valueProvider, ViewDataDictionary viewData)
            {
                ValueProvider = valueProvider;
                ViewData = viewData;
            }
        }

        public static ControllerBase Controller(IDictionary<string, ValueProviderResult> valueProvider, ViewDataDictionary viewData)
        {
            return new ControllerTestDouble(valueProvider, viewData);
        }

        public static Grid<T> CreateGrid<T>(HtmlTextWriter writer, IGridRenderer<T> renderer, IClientSideObjectWriter objectWriter) where T : class
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            if (writer != null)
            {
                httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(writer);
            }

            Mock<IGridRendererFactory> gridRendererFactory = new Mock<IGridRendererFactory>();
            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();
            Mock<IUrlGenerator> urlGenerator = new Mock<IUrlGenerator>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary());

            ViewContext viewContext = new ViewContext(new ControllerContext(new RequestContext(httpContext.Object, new RouteData()), new Mock<ControllerBase>().Object), new Mock<IView>().Object, viewDataContainer.Object.ViewData, new TempDataDictionary());
            
            Mock<IClientSideObjectWriterFactory> clientSideObjectFactory = new Mock<IClientSideObjectWriterFactory>();
            
            clientSideObjectFactory.Setup(f=>f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(objectWriter);

            Grid<T> grid = new Grid<T>(viewContext, clientSideObjectFactory.Object, urlGenerator.Object, gridRendererFactory.Object) { Name = "Grid" };

            renderer = renderer ?? new GridRenderer<T>(grid, writer);
            gridRendererFactory.Setup(f => f.Create(It.IsAny<Grid<T>>(), It.IsAny<HtmlTextWriter>())).Returns(renderer);

            return grid;
        }
        
        public static Grid<T> CreateGrid<T>(HtmlTextWriter writer, IGridRenderer<T> renderer) where T : class
        {
            return CreateGrid<T>(writer, renderer, null);
        }
        
        public static void Add(this IDictionary<string, ValueProviderResult> valueProvider, string key, object value)
        {
            valueProvider[key] = new ValueProviderResult(value, value.ToString(), CultureInfo.InvariantCulture);
        }
    }
}