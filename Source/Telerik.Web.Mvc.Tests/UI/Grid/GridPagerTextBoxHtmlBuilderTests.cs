
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;
    
    public class GridPagerTextBoxHtmlBuilderTests
    {
        public GridPagerTextBoxHtmlBuilderTests()
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
            var localization = new GridLocalization();
            var builder = new GridPagerTextBoxHtmlBuilder(localization);

            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Contains("Page ", div.InnerHtml);
            Assert.Contains("of", div.InnerHtml);
        }

        [Fact]
        public void Should_create_input()
        {
            var builder = new GridPagerTextBoxHtmlBuilder(new GridLocalization())
            {
                Value = "1",
                TotalPages = 20
            };

            var input = builder.Build().Children[1];

            Assert.Equal("input", input.TagName);
            Assert.Equal("text", input.Attribute("type"));
            Assert.Equal("1", input.Attribute("value"));
            Assert.Equal("of 20", builder.Build().Children[2].ToString());
        }
    }
}