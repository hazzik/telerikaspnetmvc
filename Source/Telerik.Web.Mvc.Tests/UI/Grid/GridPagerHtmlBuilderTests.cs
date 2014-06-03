namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Xunit;

    using Telerik.Web.Mvc.UI.Tests;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridPagerHtmlBuilderTests
    {
        private readonly Grid<Customer> grid;

        public GridPagerHtmlBuilderTests()
        {
            grid = GridTestHelper.CreateGrid<Customer>();

            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);
        }

        [Fact]
        public void Should_create_div()
        {
            var builder = new GridPagerHtmlBuilder<Customer>(grid);
            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Equal("t-pager t-reset", div.Attribute("class"));
        }

        [Fact]
        public void PagerTextBox_should_have_input_value_and_TotalPages()
        {
            grid.Paging.Style = GridPagerStyles.PageInput;
            grid.Localization = new GridLocalization();
            var builder = new GridPagerHtmlBuilder<Customer>(grid);
            var div = builder.Build().Children[0];

            Assert.Equal("1", div.Children[1].Attribute("value"));
            Assert.Equal("of 1", div.Children[2].ToString());
        }
    }
}