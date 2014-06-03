namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System.IO;
    using System.Collections.Generic;
    using Xunit;
    using Telerik.Web.Mvc.Infrastructure;

    public class EditorRenderingTests
    {
        private readonly Editor editor;
        private readonly Mock<IHtmlNode> rootTag;
        private readonly Mock<IHtmlNode> textarea;
        Mock<TextWriter> textWriter;

        public EditorRenderingTests()
        {
            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);

            rootTag = new Mock<IHtmlNode>();
            rootTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            textarea = new Mock<IHtmlNode>();
            textarea.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            editor = EditorTestHelper.CreateEditor();
            editor.Name = "Editor";

            textWriter = new Mock<TextWriter>();
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_Start_method()
        {
            EditorTestHelper.clientSideObjectWriter.Setup(w => w.Start());

            editor.WriteInitializationScript(textWriter.Object);

            EditorTestHelper.clientSideObjectWriter.Verify(w => w.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            EditorTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            editor.WriteInitializationScript(textWriter.Object);

            EditorTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }
    }
}