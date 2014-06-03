namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using System.IO;
    using System.Web.UI;
    using System.Web;

    public static class ComboBoxTestHelper
    {
        public static Mock<INavigationItemAuthorization> authorization;
        public static Mock<IClientSideObjectWriter> clientSideObjectWriter;
        public static UrlGenerator urlGenerator;

        public static ViewContext viewContext;

        public static ComboBox CreateComboBox()
        {

            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            urlGenerator = new UrlGenerator();
            authorization = new Mock<INavigationItemAuthorization>();

            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();

            var viewDataDinctionary = new ViewDataDictionary();
            //viewDataDinctionary.Add("sample", TestHelper.CreateXmlSiteMap());

            viewDataContainer.SetupGet(container => container.ViewData).Returns(viewDataDinctionary);

            // needed for testing serialization
            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            viewContext = TestHelper.CreateViewContext();
            viewContext.ViewData = viewDataDinctionary;

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            authorization.Setup(a => a.IsAccessibleToUser(viewContext.RequestContext, It.IsAny<INavigatable>())).Returns(true);

            ComboBox comboBox = new ComboBox(viewContext, clientSideObjectWriterFactory.Object, urlGenerator);

            return comboBox;
        }
    }
}
