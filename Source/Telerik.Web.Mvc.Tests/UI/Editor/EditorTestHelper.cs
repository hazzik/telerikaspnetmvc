namespace Telerik.Web.Mvc.UI.Tests
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using Moq;

    public static class EditorTestHelper
    {
        public static Mock<IClientSideObjectWriter> clientSideObjectWriter;

        public static ViewContext viewContext;

        public static Editor CreateEditor()
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));


            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();

            viewDataContainer.SetupGet(container => container.ViewData).Returns(new ViewDataDictionary());

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            viewContext = TestHelper.CreateViewContext();
            viewContext.ViewData = new ViewDataDictionary();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            viewContext = TestHelper.CreateViewContext();
            viewContext.ViewData = new ViewDataDictionary();

            return new Editor(viewContext, clientSideObjectWriterFactory.Object)
            {
                Name = "Editor"
            };
        }
    }
}
