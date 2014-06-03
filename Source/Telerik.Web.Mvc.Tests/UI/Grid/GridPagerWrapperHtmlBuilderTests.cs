namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;
    
    public class GridPagerWrapperHtmlBuilderTests
    {
        private readonly Grid<Customer> grid;
        
        public GridPagerWrapperHtmlBuilderTests()
        {
            grid = GridTestHelper.CreateGrid<Customer>();

            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);
        }

        [Fact]
        public void Should_return_div()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid);

            var div = builder.Build();

            Assert.Equal("div", div.TagName);
            Assert.Equal("t-pager-wrapper", div.Attribute("class"));
        }

        [Fact]
        public void Should_return_td_when_told_so()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid)
            {
                TagName = "td"
            };

            var td= builder.Build();

            Assert.Equal("td", td.TagName);
            Assert.Equal("t-pager-wrapper", td.Attribute("class"));
        }
        
        [Fact]
        public void Should_add_refresh()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid);
            var wrapper = builder.Build();

            var refresh = wrapper.Children[0];
            Assert.Equal("t-status", refresh.Attribute("class"));
        }

        [Fact]
        public void Should_add_pager_if_pager_is_enabled()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid)
            {
                OutputPager = true
            };
            var wrapper = builder.Build();

            var pager = wrapper.Children[1];
            Assert.Equal("t-pager t-reset", pager.Attribute("class"));
        }

        [Fact]
        public void Should_add_pager_status_if_pager_is_enabled()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid)
            {
                OutputPager = true
            };
            var wrapper = builder.Build();

            var pager = wrapper.Children[2];
            Assert.Equal("t-status-text", pager.Attribute("class"));
        }

        [Fact]
        public void Should_not_add_pager_by_default()
        {
            var builder = new GridPagerWrapperHtmlBuilder<Customer>(grid);
            var wrapper = builder.Build();

            Assert.Equal(1, wrapper.Children.Count);
        }
    }
}
