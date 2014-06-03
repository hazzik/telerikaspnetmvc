// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class ViewComponentFactoryTests
    {
        private readonly ViewComponentFactory _factory;
        private readonly HtmlHelper htmlHelper;

        public ViewComponentFactoryTests()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IGridRendererFactory>()).Returns(new Mock<IGridRendererFactory>().Object);
            locator.Setup(l => l.Resolve<IMenuRendererFactory>()).Returns(new Mock<IMenuRendererFactory>().Object);
            locator.Setup(l => l.Resolve<ITabStripRendererFactory>()).Returns(new Mock<ITabStripRendererFactory>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            ViewContext viewContext = new ViewContext
                                          {
                                              HttpContext = TestHelper.CreateMockedHttpContext().Object,
                                              ViewData = new ViewDataDictionary()
                                          };

            StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, new Mock<IWebAssetItemMerger>().Object);
            StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(styleSheetRegistrar);

            ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object, new Mock<ScriptWrapperBase>().Object);
            ScriptRegistrarBuilder scriptRegistrarBuilder = new ScriptRegistrarBuilder(scriptRegistrar);
            htmlHelper = TestHelper.CreateHtmlHelper();
            _factory = new ViewComponentFactory(htmlHelper, new Mock<IClientSideObjectWriterFactory>().Object, styleSheetRegistrarBuilder, scriptRegistrarBuilder);
        }

        [Fact]
        public void StyleSheetManager_should_return_the_same_instace()
        {
            StyleSheetRegistrarBuilder sm1 = _factory.StyleSheetRegistrar();
            StyleSheetRegistrarBuilder sm2 = _factory.StyleSheetRegistrar();

            Assert.Same(sm1, sm2);
        }

        [Fact]
        public void ScriptManager_should_return_the_same_instace()
        {
            ScriptRegistrarBuilder sm1 = _factory.ScriptRegistrar();
            ScriptRegistrarBuilder sm2 = _factory.ScriptRegistrar();

            Assert.Same(sm1, sm2);
        }

        [Fact]
        public void Menu_should_return_new_instance()
        {
            Menu m1 = _factory.Menu();
            Menu m2 = _factory.Menu();

            Assert.NotSame(m1, m2);
        }

        [Fact]
        public void TabStrip_should_return_new_instance()
        {
            Assert.NotNull(_factory.TabStrip());
        }

        [Fact]
        public void Grid_should_return_new_instance()
        {
            Assert.NotNull(_factory.Grid<Customer>());
        }

        [Fact]
        public void Grid_should_set_data_source()
        {
            IEnumerable<Customer> dataSource = new[] { new Customer() };
            GridBuilder<Customer> builder = _factory.Grid(dataSource);
            Assert.Same(dataSource, builder.Component.DataSource);
        }

        [Fact]
        public void Grid_should_set_data_source_from_view_data()
        {
            IEnumerable<Customer> dataSource = new[] { new Customer() };
            htmlHelper.ViewContext.ViewData["dataSource"] = dataSource;
            GridBuilder<Customer> builder = _factory.Grid<Customer>("dataSource");
            Assert.Same(dataSource, builder.Component.DataSource);
        }

        [Fact]
        public void PanelBar_should_return_new_instance()
        {
            Assert.NotNull(_factory.PanelBar());
        }
    }
}