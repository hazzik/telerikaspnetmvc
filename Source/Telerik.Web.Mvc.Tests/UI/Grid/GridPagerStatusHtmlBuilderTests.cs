namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridPagerStatusHtmlBuilderTests
    {
        public GridPagerStatusHtmlBuilderTests()
        {
            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);
        }
        [Fact]
        public void Should_create_div()
        {
            var builder = new GridPagerStatusHtmlBuilder(new GridLocalization());
            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Equal("t-status-text", div.Attribute("class"));
            Assert.Equal("Displaying items 0 - 0 of 0", div.InnerHtml);
        }
    }
}